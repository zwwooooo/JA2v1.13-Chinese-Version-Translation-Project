'for incompatible attachments and compatible face items
Public Class DuplicateEntryTable
    Inherits DefaultTable

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    ''TODO: trim the item/zero indexes from compatiblefaceitems.xml after a new exe is built

    '1) each record should only have two columns
    '2) this procedure will ensure that there is always a duplicate record with the column values reversed
    Protected Sub VerifyData(sender As DataManager) Handles _dm.BeforeSaveData
        Dim view As New DataView(_table, "", "", DataViewRowState.CurrentRows)

        For i As Long = view.Count - 1 To 0 Step -1
            Dim row As DataRow = _table.Rows.Find(New Object() {view(i)(1), view(i)(0)})
            If row Is Nothing Then
                row = _table.NewRow()
                row(0) = view(i)(1)
                row(1) = view(i)(0)
                _table.Rows.Add(row)
            End If
        Next

        view.RowStateFilter = DataViewRowState.Deleted
        For i As Long = view.Count - 1 To 0 Step -1
            Dim row As DataRow = _table.Rows.Find(New Object() {view(i)(1), view(i)(0)})
            If row IsNot Nothing Then row.Delete()
        Next

    End Sub
End Class
