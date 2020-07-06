<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiExit = New DevExpress.XtraBars.BarButtonItem()
        Me.bsiFotos2Copy = New DevExpress.XtraBars.BarStaticItem()
        Me.pgbMainItem = New DevExpress.XtraBars.BarEditItem()
        Me.pgbMain = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.bbiExportPivot = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiExportRohdaten = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiZipFotosAndCopy = New DevExpress.XtraBars.BarButtonItem()
        Me.drpStart = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgMain = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgExport = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgFiles = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.dtpMain = New DevExpress.XtraBars.Navigation.TabPane()
        Me.dtpFotos2Copy = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.grdFotos2Copy = New DevExpress.XtraGrid.GridControl()
        Me.dgvFotos2Copy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dtpPivotExport = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.dpgExport = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.dtpRohdaten = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.grdRohdaten = New DevExpress.XtraGrid.GridControl()
        Me.dgvRohdaten = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dtpXLSX = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.UdcXLSXExport = New AH_Foto_Verarbeitung.udcXLSX()
        Me.dtpLOG = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.dmeLog = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpMain.SuspendLayout()
        Me.dtpFotos2Copy.SuspendLayout()
        CType(Me.grdFotos2Copy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFotos2Copy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpPivotExport.SuspendLayout()
        CType(Me.dpgExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpRohdaten.SuspendLayout()
        CType(Me.grdRohdaten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRohdaten, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpXLSX.SuspendLayout()
        Me.dtpLOG.SuspendLayout()
        CType(Me.dmeLog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.bbiExit, Me.bsiFotos2Copy, Me.pgbMainItem, Me.bbiExportPivot, Me.bbiExportRohdaten, Me.bbiZipFotosAndCopy})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.drpStart})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.pgbMain})
        Me.RibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.RibbonControl.Size = New System.Drawing.Size(1090, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'bbiExit
        '
        Me.bbiExit.Caption = "Ende"
        Me.bbiExit.Id = 1
        Me.bbiExit.ImageOptions.Image = CType(resources.GetObject("bbiExit.ImageOptions.Image"), System.Drawing.Image)
        Me.bbiExit.ImageOptions.LargeImage = CType(resources.GetObject("bbiExit.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbiExit.Name = "bbiExit"
        '
        'bsiFotos2Copy
        '
        Me.bsiFotos2Copy.Caption = "BarStaticItem1"
        Me.bsiFotos2Copy.Id = 2
        Me.bsiFotos2Copy.Name = "bsiFotos2Copy"
        '
        'pgbMainItem
        '
        Me.pgbMainItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.pgbMainItem.Caption = "Copyprogress"
        Me.pgbMainItem.Edit = Me.pgbMain
        Me.pgbMainItem.Id = 3
        Me.pgbMainItem.Name = "pgbMainItem"
        Me.pgbMainItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'pgbMain
        '
        Me.pgbMain.Name = "pgbMain"
        '
        'bbiExportPivot
        '
        Me.bbiExportPivot.Caption = "Export Pivot"
        Me.bbiExportPivot.Id = 4
        Me.bbiExportPivot.ImageOptions.Image = CType(resources.GetObject("bbiExportPivot.ImageOptions.Image"), System.Drawing.Image)
        Me.bbiExportPivot.ImageOptions.LargeImage = CType(resources.GetObject("bbiExportPivot.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbiExportPivot.Name = "bbiExportPivot"
        '
        'bbiExportRohdaten
        '
        Me.bbiExportRohdaten.Caption = "Export Rohdaten"
        Me.bbiExportRohdaten.Id = 5
        Me.bbiExportRohdaten.ImageOptions.Image = CType(resources.GetObject("bbiExportRohdaten.ImageOptions.Image"), System.Drawing.Image)
        Me.bbiExportRohdaten.ImageOptions.LargeImage = CType(resources.GetObject("bbiExportRohdaten.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.bbiExportRohdaten.Name = "bbiExportRohdaten"
        '
        'bbiZipFotosAndCopy
        '
        Me.bbiZipFotosAndCopy.Caption = "Copy Zip Files"
        Me.bbiZipFotosAndCopy.Id = 6
        Me.bbiZipFotosAndCopy.ImageOptions.Image = Global.AH_Foto_Verarbeitung.My.Resources.Resources.File_Zip_Rar_Archive_icon_32
        Me.bbiZipFotosAndCopy.ImageOptions.LargeImage = Global.AH_Foto_Verarbeitung.My.Resources.Resources.File_Zip_Rar_Archive_icon_32
        Me.bbiZipFotosAndCopy.Name = "bbiZipFotosAndCopy"
        '
        'drpStart
        '
        Me.drpStart.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgMain, Me.rpgExport, Me.rpgFiles})
        Me.drpStart.Name = "drpStart"
        Me.drpStart.Text = "Start"
        '
        'rpgMain
        '
        Me.rpgMain.ItemLinks.Add(Me.bbiExit)
        Me.rpgMain.Name = "rpgMain"
        Me.rpgMain.Text = "Main"
        '
        'rpgExport
        '
        Me.rpgExport.ItemLinks.Add(Me.bbiExportPivot)
        Me.rpgExport.ItemLinks.Add(Me.bbiExportRohdaten)
        Me.rpgExport.Name = "rpgExport"
        Me.rpgExport.Text = "Export"
        '
        'rpgFiles
        '
        Me.rpgFiles.ItemLinks.Add(Me.bbiZipFotosAndCopy)
        Me.rpgFiles.Name = "rpgFiles"
        Me.rpgFiles.Text = "Files"
        '
        'RepositoryItemProgressBar1
        '
        Me.RepositoryItemProgressBar1.Name = "RepositoryItemProgressBar1"
        Me.RepositoryItemProgressBar1.ShowTitle = True
        Me.RepositoryItemProgressBar1.Step = 1
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.bsiFotos2Copy)
        Me.RibbonStatusBar.ItemLinks.Add(Me.pgbMainItem)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 661)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1090, 24)
        '
        'dtpMain
        '
        Me.dtpMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpMain.Controls.Add(Me.dtpFotos2Copy)
        Me.dtpMain.Controls.Add(Me.dtpPivotExport)
        Me.dtpMain.Controls.Add(Me.dtpRohdaten)
        Me.dtpMain.Controls.Add(Me.dtpXLSX)
        Me.dtpMain.Controls.Add(Me.dtpLOG)
        Me.dtpMain.Location = New System.Drawing.Point(0, 161)
        Me.dtpMain.Name = "dtpMain"
        Me.dtpMain.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.dtpFotos2Copy, Me.dtpPivotExport, Me.dtpRohdaten, Me.dtpXLSX, Me.dtpLOG})
        Me.dtpMain.RegularSize = New System.Drawing.Size(1090, 494)
        Me.dtpMain.SelectedPage = Me.dtpPivotExport
        Me.dtpMain.Size = New System.Drawing.Size(1090, 494)
        Me.dtpMain.TabIndex = 2
        Me.dtpMain.Text = "TabPane1"
        '
        'dtpFotos2Copy
        '
        Me.dtpFotos2Copy.Caption = "Fotos 2 Copy"
        Me.dtpFotos2Copy.Controls.Add(Me.grdFotos2Copy)
        Me.dtpFotos2Copy.Name = "dtpFotos2Copy"
        Me.dtpFotos2Copy.Size = New System.Drawing.Size(1072, 453)
        '
        'grdFotos2Copy
        '
        Me.grdFotos2Copy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFotos2Copy.Location = New System.Drawing.Point(0, 0)
        Me.grdFotos2Copy.MainView = Me.dgvFotos2Copy
        Me.grdFotos2Copy.MenuManager = Me.RibbonControl
        Me.grdFotos2Copy.Name = "grdFotos2Copy"
        Me.grdFotos2Copy.Size = New System.Drawing.Size(1072, 453)
        Me.grdFotos2Copy.TabIndex = 0
        Me.grdFotos2Copy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgvFotos2Copy})
        '
        'dgvFotos2Copy
        '
        Me.dgvFotos2Copy.GridControl = Me.grdFotos2Copy
        Me.dgvFotos2Copy.Name = "dgvFotos2Copy"
        Me.dgvFotos2Copy.OptionsBehavior.Editable = False
        Me.dgvFotos2Copy.OptionsView.ColumnAutoWidth = False
        Me.dgvFotos2Copy.OptionsView.ShowGroupPanel = False
        '
        'dtpPivotExport
        '
        Me.dtpPivotExport.Caption = "Pivot 2 Export"
        Me.dtpPivotExport.Controls.Add(Me.dpgExport)
        Me.dtpPivotExport.Name = "dtpPivotExport"
        Me.dtpPivotExport.Size = New System.Drawing.Size(1090, 465)
        '
        'dpgExport
        '
        Me.dpgExport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dpgExport.Location = New System.Drawing.Point(0, 0)
        Me.dpgExport.Name = "dpgExport"
        Me.dpgExport.Size = New System.Drawing.Size(1090, 465)
        Me.dpgExport.TabIndex = 5
        '
        'dtpRohdaten
        '
        Me.dtpRohdaten.Caption = "Rohdaten"
        Me.dtpRohdaten.Controls.Add(Me.grdRohdaten)
        Me.dtpRohdaten.Name = "dtpRohdaten"
        Me.dtpRohdaten.Size = New System.Drawing.Size(1090, 498)
        '
        'grdRohdaten
        '
        Me.grdRohdaten.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRohdaten.Location = New System.Drawing.Point(0, 0)
        Me.grdRohdaten.MainView = Me.dgvRohdaten
        Me.grdRohdaten.MenuManager = Me.RibbonControl
        Me.grdRohdaten.Name = "grdRohdaten"
        Me.grdRohdaten.Size = New System.Drawing.Size(1090, 498)
        Me.grdRohdaten.TabIndex = 1
        Me.grdRohdaten.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgvRohdaten})
        '
        'dgvRohdaten
        '
        Me.dgvRohdaten.GridControl = Me.grdRohdaten
        Me.dgvRohdaten.Name = "dgvRohdaten"
        Me.dgvRohdaten.OptionsBehavior.Editable = False
        Me.dgvRohdaten.OptionsView.ColumnAutoWidth = False
        Me.dgvRohdaten.OptionsView.ShowGroupPanel = False
        '
        'dtpXLSX
        '
        Me.dtpXLSX.Caption = "XLSX"
        Me.dtpXLSX.Controls.Add(Me.UdcXLSXExport)
        Me.dtpXLSX.Name = "dtpXLSX"
        Me.dtpXLSX.Size = New System.Drawing.Size(1090, 498)
        '
        'UdcXLSXExport
        '
        Me.UdcXLSXExport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UdcXLSXExport.Location = New System.Drawing.Point(0, 0)
        Me.UdcXLSXExport.Name = "UdcXLSXExport"
        Me.UdcXLSXExport.Size = New System.Drawing.Size(1090, 498)
        Me.UdcXLSXExport.TabIndex = 0
        '
        'dtpLOG
        '
        Me.dtpLOG.Caption = "Log"
        Me.dtpLOG.Controls.Add(Me.dmeLog)
        Me.dtpLOG.Name = "dtpLOG"
        Me.dtpLOG.Size = New System.Drawing.Size(1090, 498)
        '
        'dmeLog
        '
        Me.dmeLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dmeLog.Location = New System.Drawing.Point(0, 0)
        Me.dmeLog.MenuManager = Me.RibbonControl
        Me.dmeLog.Name = "dmeLog"
        Me.dmeLog.Size = New System.Drawing.Size(1090, 498)
        Me.dmeLog.TabIndex = 0
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 685)
        Me.Controls.Add(Me.dtpMain)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.IconOptions.Icon = CType(resources.GetObject("frmMain.IconOptions.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "AH FotoTool"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgbMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpMain.ResumeLayout(False)
        Me.dtpFotos2Copy.ResumeLayout(False)
        CType(Me.grdFotos2Copy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFotos2Copy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpPivotExport.ResumeLayout(False)
        CType(Me.dpgExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpRohdaten.ResumeLayout(False)
        CType(Me.grdRohdaten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRohdaten, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpXLSX.ResumeLayout(False)
        Me.dtpLOG.ResumeLayout(False)
        CType(Me.dmeLog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents drpStart As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpgMain As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents bbiExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dtpMain As DevExpress.XtraBars.Navigation.TabPane
    Friend WithEvents dtpFotos2Copy As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents dtpPivotExport As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents grdFotos2Copy As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgvFotos2Copy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dtpRohdaten As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents dtpXLSX As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents dtpLOG As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents bsiFotos2Copy As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents dmeLog As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents pgbMainItem As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents pgbMain As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents dpgExport As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents grdRohdaten As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgvRohdaten As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents bbiExportPivot As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgExport As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents UdcXLSXExport As udcXLSX
    Friend WithEvents bbiExportRohdaten As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiZipFotosAndCopy As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgFiles As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
