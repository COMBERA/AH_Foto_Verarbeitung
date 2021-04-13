Imports System.IO
Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Xpo.Metadata
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports log4net
Imports log4net.Config
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraPivotGrid
Imports DevExpress
Imports Ionic.Zip


Public Class frmMain

    Private oSession As Session

    Private colFotos2Copy As XPCollection(Of kunden.ah.adelholzener_vw_rep_fotos_2_copy_4_bo)
    Private colFotos4Pivot As XPCollection(Of kunden.ah.adelholzener_exp_fotoauswertung_pivot_src)
    Private colFilesCreated As New System.Collections.ObjectModel.Collection(Of String)



    Private MinDate As DateTime
    Private MaxDate As DateTime
    Private Fotos2Copy As Int32 = 0
    Private oLog As log4net.ILog
    Private IsAuto As Boolean = False

    Private tAuswertungsZeitraum As String = ""

    Private oMailer As New cb.mailer.cb_mailer



    Private Sub Args_Showing(ByVal sender As Object, ByVal e As XtraMessageShowingArgs)
        For Each control In e.Form.Controls
            Dim button As SimpleButton = TryCast(control, SimpleButton)
            If button IsNot Nothing Then
                button.ImageOptions.SvgImageSize = New Size(16, 16)
                'button.Height = 25; 
                Select Case button.DialogResult.ToString()
                    Case ("OK")
                        button.Text = "Jahr"
                        button.AutoSize = True
                    Case ("Cancel")
                        button.Text = "Abbrechen"
                        button.AutoSize = True
                    Case ("Retry")
                        button.Text = "Monat (" & tAuswertungsZeitraum & ")"
                        button.AutoSize = True
                End Select
            End If
        Next control
        e.Form.MaximumSize = New Size(900, 300)

    End Sub
    Sub InitSkins()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.UserSkins.BonusSkins.Register()
        UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful")

    End Sub
    Sub InitSession()
        If IsNothing(oSession) Then
            oSession = New Session
            oSession.ConnectionString = "XpoProvider=MSSqlServer;data source=DMSQL01;integrated security=SSPI;initial catalog=Kunden"
            oSession.TrackPropertiesModifications = True
            oSession.Connect()
        End If
    End Sub

    Private Function GetSundayInMonth() As Int16
        GetSundayInMonth = 0
        Try
            Dim iMonth As Int32 = Date.Today.Month
            Dim dtmFirstInMonth As Date = Convert.ToDateTime("01." & iMonth.ToString("00") & "." & Date.Today.Year)

            Do While dtmFirstInMonth.Month = iMonth
                If dtmFirstInMonth.DayOfWeek = DayOfWeek.Sunday Then
                    GetSundayInMonth = GetSundayInMonth + 1
                    If dtmFirstInMonth >= Date.Today Then
                        Exit Do
                    End If
                    dtmFirstInMonth = dtmFirstInMonth.AddDays(1)
                Else
                    dtmFirstInMonth = dtmFirstInMonth.AddDays(1)
                End If
            Loop

        Catch oErr As Exception
            oLog.Error("GetSundayInMonth", oErr)
        End Try
    End Function
    Private Sub AddLogText(Msg As String)
        oLog.Info(Msg)
        Me.dmeLog.Text = Me.dmeLog.Text & Msg & vbCrLf
        Me.dmeLog.SelectionStart = Int32.MaxValue
        Me.dmeLog.ScrollToCaret()
        My.Application.DoEvents()
    End Sub
    Private Function InsertPivotData() As Int32
        InsertPivotData = 0
        Try
            Dim txtSQL As String = ""
            txtSQL = " INSERT INTO Adelholzener.exp_fotoauswertung_pivot_src " +
                    " (id, adm_name, gebiet, besuchsdatum, besuchsdatum_auswertung, cid, schluessel, kategorie, copydate, jahr, monat) " +
                    " SELECT        ID, adm_name, gebiet, besuchsdatum, besuchsdatum_auswertung, cid, schluessel, kategorie, copydate, YEAR(besuchsdatum) AS jahr, MONTH(besuchsdatum) AS monat " +
                    " FROM            Adelholzener.exp_foto_4_pivot_src WHERE YEAR(besuchsdatum) > 2020"


            InsertPivotData = oSession.ExecuteNonQuery(txtSQL)

        Catch oErr As Exception
            oLog.Error("Send_Mail", oErr)
        End Try
    End Function

    Private Function FillGrid() As Int32



        colFotos2Copy = New XPCollection(Of kunden.ah.adelholzener_vw_rep_fotos_2_copy_4_bo)(oSession)
        colFotos2Copy.Sorting.Add(New SortProperty("gebiet", SortingDirection.Ascending))
        colFotos2Copy.Sorting.Add(New SortProperty("x10besuchsdatum", SortingDirection.Ascending))
        colFotos2Copy.Sorting.Add(New SortProperty("kategorie", SortingDirection.Ascending))
        colFotos2Copy.Load()

        If colFotos2Copy.Count <> 0 Then
            Me.grdFotos2Copy.DataSource = colFotos2Copy
            dgvFotos2Copy.BestFitColumns()
        End If

        Me.bsiFotos2Copy.Caption = colFotos2Copy.Count.ToString("#,##0") & " Fotos"

        FillGrid = colFotos2Copy.Count

    End Function
    Private Sub FillPivot()
        If IsNothing(oSession) Then
            oSession = New Session
            oSession.ConnectionString = "XpoProvider=MSSqlServer;data source=DMSQL01;integrated security=SSPI;initial catalog=Kunden"
            oSession.TrackPropertiesModifications = True
            oSession.Connect()
        End If

        colFotos4Pivot = New XPCollection(Of kunden.ah.adelholzener_exp_fotoauswertung_pivot_src)(oSession)

        colFotos4Pivot.Load()

        If colFotos4Pivot.Count <> 0 Then
            Me.dpgExport.DataSource = colFotos4Pivot
            Me.grdRohdaten.DataSource = colFotos4Pivot
            Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
            Me.dgvRohdaten.BestFitColumns()

            Me.dpgExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            Me.dpgExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.False
            Me.dpgExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False
            Me.dpgExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.False

            Dim oFieldADM As PivotGridField = New PivotGridField("adm_name", PivotArea.RowArea)
            oFieldADM.Caption = "ADM"


            Dim oFieldKategorie As PivotGridField = New PivotGridField("kategorie", PivotArea.ColumnArea)
            oFieldKategorie.Caption = "Kategorie"

            ' Create a column Pivot Grid Control field bound to the CategoryName datasource field.
            Dim oFieldYear As PivotGridField = New PivotGridField("jahr", PivotArea.FilterArea)
            oFieldYear.Caption = "Jahr"
            'oFieldYear.GroupInterval = PivotGroupInterval.Alphabetical
            Dim oFieldMonat As PivotGridField = New PivotGridField("monat", PivotArea.FilterArea)
            oFieldMonat.Caption = "Monat"


            ' Create a data Pivot Grid Control field bound to the 'Extended Price' datasource field.
            Dim oFieldCount As PivotGridField = New PivotGridField("schluessel", PivotArea.DataArea)
            oFieldCount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            oFieldCount.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count

            ' Add the fields to the control's field collection.         
            dpgExport.Fields.AddRange(New PivotGridField() {oFieldADM, oFieldKategorie, oFieldYear, oFieldMonat, oFieldCount})

            ' Arrange the row fields within the Row Header Area.
            oFieldADM.AreaIndex = 0


            ' Arrange the column fields within the Column Header Area.
            oFieldKategorie.AreaIndex = 0


            Me.dpgExport.BestFit()

        End If

    End Sub
    Private Function CopyFotos() As Int32
        CopyFotos = -1
        Try
            Dim oView As GridView = TryCast(Me.dgvFotos2Copy, GridView)

            Dim oFoto As kunden.ah.adelholzener_vw_rep_fotos_2_copy_4_bo
            Dim iRowHandle As Int32 = 0

            Dim txtDestfolderRoot As String = My.Settings.DestinationFolder
            If Not txtDestfolderRoot.EndsWith("\") Then
                txtDestfolderRoot = txtDestfolderRoot & "\"
            End If

            oSession.ExecuteNonQuery("Update Adelholzener.Tbl_BSS_Fotos SET  Name = Kunden_Android.Adelholzener.bss_fotos_temp.name From Adelholzener.Tbl_BSS_Fotos INNER Join Kunden_Android.Adelholzener.bss_fotos_temp ON Adelholzener.Tbl_BSS_Fotos.id_kunden_android = Kunden_Android.Adelholzener.bss_fotos_temp.id Where (Adelholzener.Tbl_BSS_Fotos.Name Is NULL)")



            CopyFotos = 0

            Me.pgbMain.Maximum = oView.RowCount
            Me.pgbMain.Minimum = 1
            Me.pgbMainItem.EditValue = 0

            For iRowHandle = 0 To oView.RowCount - 1
                Dim txtDestFolder2Copy As String = txtDestfolderRoot
                Dim txtPOSIFolder As String = ""
                Dim txtToReplaceFolder As String = ""



                oView.FocusedRowHandle = iRowHandle
                My.Application.DoEvents()
                oFoto = oView.GetRow(iRowHandle)
                If Not IsNothing(oFoto) Then
                    If IsNothing(oFoto.x10besuchsdatum) Then
                        oFoto.x10besuchsdatum = oFoto.datumintern
                        Dim txtSQL As String = ""
                        txtSQL = " UPDATE       Adelholzener.Tbl_BSS_Fotos " +
                                " SET                x10besuchsdatum = CONVERT(DATETIME, '" & oFoto.datumintern.ToString("yyyy-MM-dd") & "', 102) " +
                                " WHERE        (ID = " & oFoto.id & ") AND (foto_exported IS NULL) "
                        oSession.ExecuteNonQuery(txtSQL)
                        oFoto.Reload()
                    End If
                    If oFoto.x10besuchsdatum.Year = 1 Then
                        oFoto.x10besuchsdatum = oFoto.datumintern.ToShortDateString
                        Dim txtSQL As String = ""
                        txtSQL = " UPDATE       Adelholzener.Tbl_BSS_Fotos " +
                                " SET                x10besuchsdatum = CONVERT(DATETIME, '" & oFoto.datumintern.ToString("yyyy-MM-dd") & "', 102) " +
                                " WHERE        (ID = " & oFoto.id & ") AND (foto_exported IS NULL) "
                        oSession.ExecuteNonQuery(txtSQL)
                        oFoto.Reload()
                    End If
                    If MinDate.Year = 1 Then
                        MinDate = oFoto.x10besuchsdatum
                    End If
                    If MaxDate.Year = 1 Then
                        MaxDate = oFoto.x10besuchsdatum
                    End If
                    If oFoto.x10besuchsdatum < MinDate Then
                        MinDate = oFoto.x10besuchsdatum
                    End If
                    If oFoto.x10besuchsdatum > MaxDate Then
                        MaxDate = oFoto.x10besuchsdatum
                    End If
                    'DestFolder2Copy = DestfolderRoot & Jahr
                    txtDestFolder2Copy = txtDestFolder2Copy & oFoto.j

                    If Not My.Computer.FileSystem.DirectoryExists(txtDestFolder2Copy) Then
                        My.Computer.FileSystem.CreateDirectory(txtDestFolder2Copy)
                    End If
                    '\\dmfs04\all\Adelholzener\2019 ==> \\dmfs01\POS-Sales_Programme\Fotos_Temp
                    txtToReplaceFolder = txtDestFolder2Copy

                    If Not txtDestFolder2Copy.EndsWith("\") Then
                        txtDestFolder2Copy = txtDestFolder2Copy & "\"
                    End If
                    'DestFolder2Copy = DestfolderRoot & Jahr & Monat (00)
                    txtDestFolder2Copy = txtDestFolder2Copy & oFoto.m.ToString("00")

                    If Not My.Computer.FileSystem.DirectoryExists(txtDestFolder2Copy) Then
                        My.Computer.FileSystem.CreateDirectory(txtDestFolder2Copy)
                    End If
                    If Not txtDestFolder2Copy.EndsWith("\") Then
                        txtDestFolder2Copy = txtDestFolder2Copy & "\"
                    End If


                    Dim txtFile As String = oFoto.srcfile
                    Dim FileExists4Copy As Boolean = False
                    If My.Computer.FileSystem.FileExists(txtFile) Then
                        FileExists4Copy = True
                    Else
                        If oFoto.name.StartsWith("0_") Then
                            txtFile = String.Format("{0}\{1}", oFoto.srcfolder, oFoto.name.Replace("0_", oFoto.gebiet & "_"))
                        End If
                    End If


                    If My.Computer.FileSystem.FileExists(txtFile) Then
                        'DestFolder2Copy = DestfolderRoot & Jahr & Monat (00) & AdelholzenerGEBIET
                        txtDestFolder2Copy = String.Format("{0}adelholzener{1}", txtDestFolder2Copy, oFoto.gebiet)

                        If Not My.Computer.FileSystem.DirectoryExists(txtDestFolder2Copy) Then
                            My.Computer.FileSystem.CreateDirectory(txtDestFolder2Copy)
                        End If
                        If Not txtDestFolder2Copy.EndsWith("\") Then
                            txtDestFolder2Copy = txtDestFolder2Copy & "\"
                        End If




                        'DestFolder2Copy = DestfolderRoot & Jahr & Monat (00) & AdelholzenerGEBIET & Schluessel_Kategorie
                        txtDestFolder2Copy = String.Format("{0}{1}_{2}", txtDestFolder2Copy, oFoto.schluessel.Trim, oFoto.kategorie.Replace(" ", "_"))
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace("/", "_")
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace(":", "_")
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace(",", "_")
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace(";", "_")
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace("%", "_")
                        txtDestFolder2Copy = txtDestFolder2Copy.Replace("&", "_")
                        If Not My.Computer.FileSystem.DirectoryExists(txtDestFolder2Copy) Then
                            My.Computer.FileSystem.CreateDirectory(txtDestFolder2Copy)
                        End If
                        If Not txtDestFolder2Copy.EndsWith("\") Then
                            txtDestFolder2Copy = txtDestFolder2Copy & "\"
                        End If

                        If My.Computer.FileSystem.FileExists(txtDestFolder2Copy & oFoto.name) Then
                            oLog.Warn(String.Format("{0} exisitiert schon in: {1} (ID: {2})", oFoto.name, txtDestFolder2Copy, oFoto.id))
                            AddLogText(String.Format("{0} exisitiert schon in: {1} (ID: {2})", oFoto.name, txtDestFolder2Copy, oFoto.id))
                        Else
                            My.Computer.FileSystem.CopyFile(txtFile, txtDestFolder2Copy & oFoto.name)
                            AddLogText(String.Format("{0}{1} erstellt", txtDestFolder2Copy, oFoto.name))

                            Try
                                If My.Settings.DestinationFolder4POSI.Length <> 0 Then
                                    txtPOSIFolder = txtDestFolder2Copy.Replace(txtToReplaceFolder, My.Settings.DestinationFolder4POSI)
                                    If Not My.Computer.FileSystem.DirectoryExists(txtPOSIFolder) Then
                                        My.Computer.FileSystem.CreateDirectory(txtPOSIFolder)
                                    End If
                                    My.Computer.FileSystem.CopyFile(txtFile, txtPOSIFolder & oFoto.name)
                                    AddLogText(String.Format("{0}{1} erstellt", txtPOSIFolder, oFoto.name))
                                End If
                            Catch oErrCopy2POSI As Exception
                                AddLogText("ERROR: " & oErrCopy2POSI.Message)
                            End Try
                        End If

                        Dim txtSQL As String = ""
                        txtSQL = " UPDATE       Adelholzener.Tbl_BSS_Fotos " +
                                " SET                foto_exported = dbo.Only_Date(GETDATE()) " +
                                " WHERE        (ID = " & oFoto.id & ") AND (foto_exported IS NULL) "

                        If oSession.ExecuteNonQuery(txtSQL) > 0 Then
                            CopyFotos = CopyFotos + 1
                            Me.pgbMainItem.EditValue = CopyFotos

                        End If
                        My.Application.DoEvents()
                    Else
                        Dim txtSQL As String = ""
                        txtSQL = " UPDATE       Adelholzener.Tbl_BSS_Fotos " +
                                " SET                foto_exported = CONVERT(DATETIME, '2099-12-31', 102)  " +
                                " WHERE        (ID = " & oFoto.id & ") AND (foto_exported IS NULL) "

                        If oSession.ExecuteNonQuery(txtSQL) > 0 Then
                            CopyFotos = CopyFotos + 1
                            Me.pgbMainItem.EditValue = CopyFotos

                        End If
                        My.Application.DoEvents()
                    End If
                End If
            Next

        Catch oErr As Exception
            oLog.Error("CopyFotos", oErr)
        End Try
    End Function
    Private Sub ExportPivot(Optional Manuell As Boolean = False)
        Try
            Dim AktDate As Date = Date.Today
            Dim AktMonat As Int32 = AktDate.Month
            Dim AktJahr As Int32 = AktDate.Year
            Dim AuswertungsMonat As Int32 = 0
            Dim AuswertungsJahr As Int32 = 0
            Dim txtXLSFolder As String = My.Settings.ExcelFolder4Reports

            If Not Manuell Then
                AddLogText("Aktueller Tag:" & Date.Today.ToString("dddd dd.MM.yyyy"))
                If AktDate.DayOfWeek = DayOfWeek.Sunday Then
                    Dim iSunday As Int16 = 1
                    Dim iAktSunday As Int16 = GetSundayInMonth()
                    If AktDate.Day <= 3 Then
                        iSunday = 2
                    End If
                    AddLogText("Pivot Sonntag: " & iSunday)
                    AddLogText("aktueller Sonntag: " & iAktSunday)
                    If iAktSunday < iSunday Then
                        AddLogText("Kein Pivot Exort")
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            If IsNothing(colFilesCreated) Then
                colFilesCreated = New ObjectModel.Collection(Of String)
            Else
                colFilesCreated.Clear()
            End If

            If My.Computer.Name.ToLower = "lt-5195" Then
                txtXLSFolder = "D:\"
            End If
            If Not txtXLSFolder.EndsWith("\") Then
                txtXLSFolder = txtXLSFolder & "\"
            End If
            AddLogText("Ausgabefolder: " & txtXLSFolder)


            If AktMonat = 1 Then
                AuswertungsMonat = 12
                AuswertungsJahr = AktJahr - 1
            Else
                AuswertungsMonat = AktMonat - 1
                AuswertungsJahr = AktJahr
            End If

            Dim txtDtmFilter As String = ""
            txtDtmFilter = String.Format("{0}.{1:00}", AuswertungsJahr, AuswertungsMonat)
            AddLogText("Filter für Export: " & txtDtmFilter)

            tAuswertungsZeitraum = txtDtmFilter

            Dim args As New XtraMessageBoxArgs()
            args.Caption = "Export Pivot"
            args.Text = "Was soll ausgewertet werden?"
            args.Buttons = New DialogResult() {DialogResult.OK, DialogResult.Cancel, DialogResult.Retry}
            AddHandler args.Showing, AddressOf Args_Showing


            Dim oResult As DialogResult = XtraMessageBox.Show(args)

            Dim txtFolder As String = ""
            Dim o As New DevExpress.XtraPrinting.XlsxExportOptionsEx() With {.ExportType = DevExpress.Export.ExportType.WYSIWYG, .ExportMode = XtraPrinting.XlsxExportMode.SingleFile, .SheetName = "Jahr"}

            If oResult = DialogResult.Retry Then


                txtFolder = String.Format(txtXLSFolder & "Monatsuaswertung_Fotos_{0}.xlsx", tAuswertungsZeitraum)
                Dim oFilterAktJahr As CriteriaOperator
                oFilterAktJahr = GroupOperator.Combine(GroupOperatorType.And,
                            New BinaryOperator("jahr", AuswertungsJahr, BinaryOperatorType.Equal),
                            New BinaryOperator("monat", AuswertungsMonat, BinaryOperatorType.Equal))

                colFotos4Pivot.Filter = oFilterAktJahr
                Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
                Me.dpgExport.ExportToXlsx(txtFolder, o)
                colFilesCreated.Add(txtFolder)
                AddLogText("File created: " & txtFolder)
                colFotos4Pivot.Filter = Nothing

                o.SheetName = "Monat " & AuswertungsMonat.ToString("00")
            End If

            If oResult = DialogResult.OK Then


                txtFolder = String.Format(txtXLSFolder & "Jahresauswertung_Fotos_{0}.xlsx", AuswertungsJahr)
                Dim oFilterAktJahr As CriteriaOperator
                oFilterAktJahr = GroupOperator.Combine(GroupOperatorType.And,
                            New BinaryOperator("jahr", AuswertungsJahr, BinaryOperatorType.Equal),
                            New BinaryOperator("monat", AuswertungsMonat, BinaryOperatorType.LessOrEqual))

                colFotos4Pivot.Filter = oFilterAktJahr
                Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
                Me.dpgExport.ExportToXlsx(txtFolder, o)
                colFilesCreated.Add(txtFolder)
                AddLogText("File created: " & txtFolder)
                colFotos4Pivot.Filter = Nothing

                o.SheetName = "Monat " & AuswertungsMonat.ToString("00")
            End If

            Dim oFilter As BinaryOperator = New BinaryOperator("besuchsdatum_auswertung", txtDtmFilter, BinaryOperatorType.Equal)
            colFotos4Pivot.Filter = oFilter
            Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
            Me.dgvFotos2Copy.RefreshData()
            Me.grdRohdaten.DataSource = colFotos4Pivot
            txtFolder = String.Format(txtXLSFolder & "Monatsauswertung_Rohdaten_{0}_{1}.xlsx", AuswertungsJahr, AuswertungsMonat.ToString("00"))
            Me.dpgExport.ExportToXlsx(txtFolder, o)
            colFilesCreated.Add(txtFolder)
            AddLogText("File created: " & txtFolder)

        Catch oErr As Exception
            oLog.Error("ExportPivot", oErr)
        End Try

    End Sub
    Private Sub ExporFilteredtPivot()
        Try
            Dim AktDate As Date = Date.Today
            Dim AktMonat As Int32 = AktDate.Month
            Dim AktJahr As Int32 = AktDate.Year
            Dim AuswertungsMonat As Int32 = 0
            Dim AuswertungsJahr As Int32 = 0
            Dim txtXLSFolder As String = My.Settings.ExcelFolder4Reports



            If IsNothing(colFilesCreated) Then
                colFilesCreated = New ObjectModel.Collection(Of String)
            Else
                colFilesCreated.Clear()
            End If

            If My.Computer.Name.ToLower = "lt-5195" Then
                txtXLSFolder = "D:\"
            End If
            If Not txtXLSFolder.EndsWith("\") Then
                txtXLSFolder = txtXLSFolder & "\"
            End If
            AddLogText("Ausgabefolder: " & txtXLSFolder)


            Dim arrCO() As String = Me.dpgExport.ActiveFilterCriteria.LegacyToString.ToLower.Split("and")

            For Each oField As DevExpress.XtraPivotGrid.PivotGridField In Me.dpgExport.Fields
                If oField.FilterValues.Count = 1 Then
                    Debug.Print(oField.FieldName.ToLower)
                    If oField.FieldName.ToLower = "jahr" Then
                        AuswertungsJahr = Convert.ToInt32(oField.FilterValues.Values(0))
                    End If
                    If oField.FieldName.ToLower = "monat" Then
                        AuswertungsMonat = Convert.ToInt32(oField.FilterValues.Values(0))
                    End If
                End If
            Next


            Dim txtDtmFilter As String = ""
            txtDtmFilter = String.Format("{0}.{1:00}", AuswertungsJahr, AuswertungsMonat)
            AddLogText("Filter für Export: " & txtDtmFilter)

            tAuswertungsZeitraum = txtDtmFilter

            Dim args As New XtraMessageBoxArgs()
            args.Caption = "Export Pivot"
            args.Text = "Was soll ausgewertet werden?"
            args.Buttons = New DialogResult() {DialogResult.OK, DialogResult.Cancel, DialogResult.Retry}
            AddHandler args.Showing, AddressOf Args_Showing


            Dim oResult As DialogResult = XtraMessageBox.Show(args)

            Dim txtFolder As String = ""
            Dim o As New DevExpress.XtraPrinting.XlsxExportOptionsEx() With {.ExportType = DevExpress.Export.ExportType.WYSIWYG, .ExportMode = XtraPrinting.XlsxExportMode.SingleFile, .SheetName = "Jahr"}

            If oResult = DialogResult.Retry Then


                txtFolder = String.Format(txtXLSFolder & "Monatsuaswertung_Fotos_{0}.xlsx", tAuswertungsZeitraum)
                Dim oFilterAktJahr As CriteriaOperator
                oFilterAktJahr = GroupOperator.Combine(GroupOperatorType.And,
                            New BinaryOperator("jahr", AuswertungsJahr, BinaryOperatorType.Equal),
                            New BinaryOperator("monat", AuswertungsMonat, BinaryOperatorType.Equal))

                colFotos4Pivot.Filter = oFilterAktJahr
                Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
                Me.dpgExport.ExportToXlsx(txtFolder, o)
                colFilesCreated.Add(txtFolder)
                AddLogText("File created: " & txtFolder)
                colFotos4Pivot.Filter = Nothing

                o.SheetName = "Monat " & AuswertungsMonat.ToString("00")
            End If

            If oResult = DialogResult.OK Then


                txtFolder = String.Format(txtXLSFolder & "Jahresauswertung_Fotos_{0}.xlsx", AuswertungsJahr)
                Dim oFilterAktJahr As CriteriaOperator
                oFilterAktJahr = GroupOperator.Combine(GroupOperatorType.And,
                            New BinaryOperator("jahr", AuswertungsJahr, BinaryOperatorType.Equal),
                            New BinaryOperator("monat", AuswertungsMonat, BinaryOperatorType.LessOrEqual))

                colFotos4Pivot.Filter = oFilterAktJahr
                Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
                Me.dpgExport.ExportToXlsx(txtFolder, o)
                colFilesCreated.Add(txtFolder)
                AddLogText("File created: " & txtFolder)
                colFotos4Pivot.Filter = Nothing

                o.SheetName = "Monat " & AuswertungsMonat.ToString("00")
            End If

            Dim oFilter As BinaryOperator = New BinaryOperator("besuchsdatum_auswertung", txtDtmFilter, BinaryOperatorType.Equal)
            colFotos4Pivot.Filter = oFilter
            Me.dtpRohdaten.Text = "Rohdaten (" & colFotos4Pivot.Count.ToString("#,##0") & ")"
            Me.dgvFotos2Copy.RefreshData()
            Me.grdRohdaten.DataSource = colFotos4Pivot
            txtFolder = String.Format(txtXLSFolder & "Monatsauswertung_Rohdaten_{0}_{1}.xlsx", AuswertungsJahr, AuswertungsMonat.ToString("00"))
            Me.dpgExport.ExportToXlsx(txtFolder, o)
            colFilesCreated.Add(txtFolder)
            AddLogText("File created: " & txtFolder)

        Catch oErr As Exception
            oLog.Error("ExportPivot", oErr)
        End Try

    End Sub
    Private Sub Send_Mail(iFotos As Int32)

        Try

            oMailer.To = My.Settings.Mail_Reporting
            oMailer.CC = "hans-peter.bruns@combera.com"



            Dim oSB As New System.Text.StringBuilder

            oMailer.Subject = "AH Fotos kopieren vom: " & Date.Today.ToShortDateString

            oSB.AppendLine(String.Format("Es wurden {0:#,##0} Fotos vom München nach Dortmund kopiert.{1}{1}Besuchsdatum von {2:ddd dd.MM.yyyy} bis {3:ddd dd.MM.yyyy}", iFotos, vbCrLf, MinDate, MaxDate))

            oMailer.Attachments.Clear()

            If colFilesCreated.Count <> 0 Then
                oSB.AppendLine("")

                Dim txtFile As String
                For Each txtFile In colFilesCreated
                    oSB.AppendLine(String.Format("{0} erstellt", txtFile))
                    oMailer.Attachments.Add(txtFile)
                Next
            End If
            oSB.AppendLine("")
            oMailer.Body = oSB.ToString()

            'If oMailer.Create_Mail() Then
            '    oMailer.SendMail()
            'End If

            oMailer.sendMail()

        Catch oErr As Exception
            oLog.Error("Send_Mail", oErr)
        End Try

    End Sub
    Private Function Send_ZIP_Mail(iFotos As Int32, Monat As Int32, Jahr As Int32) As Boolean

        Try

            Dim dtmAuswertung As DateTime = Date.Parse("01." & Monat.ToString("00") & "." & Jahr.ToString("0000"))

            oMailer.To = My.Settings.web_foto_zip_mail_recipients
            oMailer.CC = "hans-peter.bruns@combera.com"
            oMailer.Subject = "Adelholzener Foto-ZIP für den Monat " & dtmAuswertung.ToString("MMMM yyyy")


            Dim oSB As New StringBuilder

            oSB.AppendLine("Sehr geehrte Damen und Herren")
            oSB.AppendLine("")
            oSB.AppendLine("Unter https: //www.combera.com/adelholzener/auswertungen/fotos/ liegen die Fotos für den Monat " & dtmAuswertung.ToString("MMMM yyyy") & " in der Datei '" & Monat.ToString("00") & ".zip' bereit.")
            oSB.AppendLine("Es sind " & iFotos.ToString("#,##0") & " Bilder in der Datei enthalten.")
            oSB.AppendLine("")
            oSB.AppendLine("Mit freundlichen Grüßen")
            oSB.AppendLine("Ihr COMBERA IT Team")
            oSB.AppendLine("")
            oMailer.Body = oSB.ToString()

            oMailer.sendMail()

            Return True

        Catch oErr As Exception
            MsgBox(oErr.Message)
            oLog.Error("Send_ZIP_Mail", oErr)
            Return False
        End Try

    End Function
    '*****************************************************************************************************************************************************
    'Form Events
    '*****************************************************************************************************************************************************
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        oLog = log4net.LogManager.GetLogger("ah_fotos")
        InitSkins()
        InitSession()

        If My.Application.CommandLineArgs.Count <> 0 Then
            For Each oCLA In My.Application.CommandLineArgs
                If oCLA.ToLower.Replace("/", "") = "auto" Then
                    IsAuto = True
                End If
            Next

        End If

        AddLogText("Start Insert Pivot Data")
        AddLogText("Rows:" & InsertPivotData())
        AddLogText("Ende Insert Pivot Data")

        AddLogText("Start FillGrid")
        Fotos2Copy = FillGrid()
        AddLogText("Ende FillGrid")

        FillPivot()

    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim i As Int32 = -99

        oLog.Info("Fotos2Copy: " & Fotos2Copy)

        If Fotos2Copy <> 0 Then
            If My.Computer.Name.ToLower = "lt-5195" Then
                Dim iFrage As Int32 = MsgBox("Sollen die Fotos kopiert werden?", vbYesNo)
                If iFrage = vbYes Then
                    AddLogText("Start CopyFotos (Manuell)")
                    i = CopyFotos()
                    AddLogText(String.Format("Ende CopyFotos {0:#,##0} (Manuell)", i))
                End If
            Else
                If IsAuto Then
                    AddLogText("Start CopyFotos (Auto)")
                    i = CopyFotos()
                    AddLogText(String.Format("Ende CopyFotos {0:#,##0} (Auto)", i))
                    AddLogText("Start ExportPivot (Auto)")
                    ExportPivot()
                    AddLogText(String.Format("Stop ExportPivot Files Created {0} (Auto)", colFilesCreated.Count))
                    Send_Mail(i)
                    End
                Else
                    Dim iFrage As Int32 = MsgBox("Sollen die Fotos kopiert werden?", vbYesNo)
                    If iFrage = vbYes Then
                        AddLogText("Start CopyFotos (Manuell)")
                        i = CopyFotos()
                        AddLogText(String.Format("Ende CopyFotos {0:#,##0} (Manuell)", i))
                    End If
                End If

            End If
        Else
            If IsAuto Then
                oLog.Info("FillPivot")
                FillPivot()
                oLog.Info("Is denn Sonntag?")
                If Date.Today.DayOfWeek = DayOfWeek.Sunday Then
                    oLog.Info("Jau, Is Sonntag!")
                    oLog.Info("Export Pivot")
                    ExportPivot()
                    oLog.Info("Send Mail")
                    Send_Mail(0)
                    oLog.Info("Und Tschüss!!!")
                Else
                    oLog.Info("Nix zu tun. Beende mich dann mal")
                End If
                End
            Else
                AddLogText("Keine Fotos zu kopieren!!!!!")
            End If
        End If

    End Sub
    '*****************************************************************************************************************************************************
    'GridView Events
    '*****************************************************************************************************************************************************
    Private Sub dgvRohdaten_ColumnFilterChanged(sender As Object, e As EventArgs) Handles dgvRohdaten.ColumnFilterChanged
        Me.dtpRohdaten.Caption = "Rohdaten (" & dgvRohdaten.RowCount.ToString("#,##0") & ")"
    End Sub

    '*****************************************************************************************************************************************************
    'Menü Events
    '*****************************************************************************************************************************************************
    Private Sub bbiExportPivot_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiExportPivot.ItemClick
        ExportPivot(True)
        Dim iFrage As Int32 = MsgBox("Soll die Mail gesendet werden?", vbYesNo)
        If iFrage = vbYes Then
            Send_Mail(0)
        End If
    End Sub
    Private Sub bbiExportRohdaten_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles bbiExportRohdaten.ItemClick
        Dim AktDate As Date = Date.Today
        Dim Monat As Int32 = AktDate.Month
        Dim Jahr As Int32 = AktDate.Year

        Dim txtXLSFolder As String = My.Settings.ExcelFolder4Reports
        If My.Computer.Name.ToLower = "lt-5195" Then
            txtXLSFolder = "D:\"
        End If
        If Not txtXLSFolder.EndsWith("\") Then
            txtXLSFolder = txtXLSFolder & "\"
        End If
        AddLogText("Ausgabefolder: " & txtXLSFolder)
        Dim txtFolder As String = String.Format(txtXLSFolder & "Rohdaten{0}.xlsx", Jahr)
        Me.grdRohdaten.ExportToXlsx(txtFolder)

        Me.dtpMain.SelectedPage = Me.dtpXLSX

        My.Application.DoEvents()

        Me.UdcXLSXExport.CreatePivot(txtFolder)



    End Sub
    Private Sub bbiExit_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles bbiExit.ItemClick
        End
    End Sub

    Private Sub bbiZipFotosAndCopy_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles bbiZipFotosAndCopy.ItemClick

        Dim AktDate As Date = Date.Today
        Dim AktMonat As Int32 = AktDate.Month
        Dim AktJahr As Int32 = AktDate.Year
        Dim AuswertungsMonat As Int32 = 0
        Dim AuswertungsJahr As Int32 = 0
        Dim txtWebFolder As String = My.Settings.web_foto_zip_folder
        Dim txtSrcFolder As String = My.Settings.DestinationFolder

        If Not txtWebFolder.EndsWith("\") Then
            txtWebFolder = txtWebFolder & "\"
        End If

        If AktMonat = 1 Then
            AuswertungsMonat = 12
            AuswertungsJahr = AktJahr - 1
        Else
            AuswertungsMonat = AktMonat - 1
            AuswertungsJahr = AktJahr
        End If

        '\\dmfs04\all\Adelholzener\2020\06
        If Not txtSrcFolder.EndsWith("\") Then
            txtSrcFolder = txtSrcFolder & "\"
        End If

        txtSrcFolder = txtSrcFolder & AuswertungsJahr & "\"
        txtSrcFolder = txtSrcFolder & AuswertungsMonat.ToString("00") & "\"


        Dim txtDtmFilter As String = ""
        txtDtmFilter = String.Format("{0}.{1:00}", AuswertungsJahr, AuswertungsMonat)
        tAuswertungsZeitraum = txtDtmFilter

        Dim args As New XtraMessageBoxArgs()
        args.Caption = "Zip Fotos"
        args.Text = "Was soll ausgewertet werden?"
        args.Buttons = New DialogResult() {DialogResult.OK, DialogResult.Cancel, DialogResult.Retry}
        AddHandler args.Showing, AddressOf Args_Showing


        Dim oResult As DialogResult = XtraMessageBox.Show(args)
        Dim iFies As Int32 = 0
        If oResult = DialogResult.Retry Then
            Using oZip As ZipFile = New ZipFile
                oZip.AddDirectory(txtSrcFolder, "")
                oZip.Save(txtWebFolder & AuswertungsMonat.ToString("00") & ".zip")
                iFies = oZip.Count
            End Using

            If Send_ZIP_Mail(iFies, AuswertungsMonat, AuswertungsJahr) Then
                MsgBox(txtWebFolder & AuswertungsMonat.ToString("00") & ".zip created sucessfully!", vbInformation + vbOKOnly, "ZIP files and copy")
            Else
                MsgBox("Someting went wohl wrong :-(", vbExclamation + vbOKOnly, "ZIP files and copy")
            End If


        End If

    End Sub

    Private Sub bbiExportFilterdPivot_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles bbiExportFilterdPivot.ItemClick
        ExporFilteredtPivot()
        Dim iFrage As Int32 = MsgBox("Soll die Mail gesendet werden?", vbYesNo)
        If iFrage = vbYes Then
            Send_Mail(0)
        End If
    End Sub
End Class