Imports DevExpress.Spreadsheet

Public Class udcXLSX

    Public Sub CreatePivot(File2Load As String)
        Me.dscXLSX.LoadDocument(File2Load)

        My.Application.DoEvents()

        Dim sourceWorksheet As Worksheet = Me.dscXLSX.Document.Worksheets(0)
        Dim worksheet As Worksheet = Me.dscXLSX.Document.Worksheets.Add()
        Me.dscXLSX.Document.Worksheets.ActiveWorksheet = worksheet

        ' Create a pivot table using the used cell range as the data source.
        Dim pivotTable As PivotTable = worksheet.PivotTables.Add(sourceWorksheet.GetUsedRange(), worksheet("A2"))
        pivotTable.Style = Me.dscXLSX.Document.TableStyles(BuiltInPivotStyleId.PivotStyleMedium6)

        ' Add the "ADM Name" field to the row axis area.
        pivotTable.RowFields.Add(pivotTable.Fields("adm_name"))
        ' Add the "Kategorie" field to the row axis area.
        pivotTable.ColumnFields.Add(pivotTable.Fields("kategorie"))
        ' Add the "Schluesse" field to the data area.
        pivotTable.DataFields.Add(pivotTable.Fields("schluessel"))
        'Add Filter fields
        pivotTable.PageFields.Add(pivotTable.Fields("jahr"))
        pivotTable.PageFields.Add(pivotTable.Fields("monat"))
        ' Set the pivot table style.

        Me.Refresh()

        My.Application.DoEvents()

        Me.dscXLSX.Document.SaveDocument(File2Load, DocumentFormat.Xlsx)

    End Sub

End Class
