Public Class AutoIncrementTable
    Inherits DefaultTable

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
        AddHandler _table.TableNewRow, AddressOf TableNewRowHandler
    End Sub

    Protected Sub TableNewRowHandler(ByVal sender As Object, ByVal e As DataTableNewRowEventArgs)
        e.Row(_table.PrimaryKey(0)) = GetNextPrimaryKeyValue()
    End Sub

    'I was going to set this up so it only reindexes when the table has been changed,
    'but it makes more sense to always do it, and therefore catch any mistakes made
    'by anyone who edits the files by hand
    Protected Sub ReindexTable(sender As DataManager) Handles _dm.BeforeSaveData
        Dim vw As New DataView(_table, "", _table.PrimaryKey(0).ColumnName, DataViewRowState.CurrentRows)
        For i As Integer = 0 To vw.Count - 1
            vw(i)(_table.PrimaryKey(0).ColumnName) = i
        Next
    End Sub

End Class
