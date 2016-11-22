Public Class DataGridForm
    Inherits GridForm

    Public Sub New(manager As DataManager, ByVal formText As String, tableName As String, Optional rowFilter As String = Nothing, Optional sort As String = Nothing, Optional rowStateFilter As DataViewRowState = DataViewRowState.CurrentRows)
        MyBase.New(manager, formText, tableName, rowFilter, sort, rowStateFilter)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formText
        With Me.Grid
            .AllowUserToDeleteRows = True
            .AllowUserToAddRows = True
            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .RowHeadersVisible = True
            .ReadOnly = False
        End With
        EnableContextMenuItems()
    End Sub

    Private Sub Grid_DefaultValuesNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Grid.DefaultValuesNeeded
        If _view.Table.PrimaryKey.Length = 1 Then 'only works w/single keyed tables
            Dim key As String = _view.Table.PrimaryKey(0).ColumnName
            Dim val As Decimal = _dm.Database.GetNextPrimaryKeyValue(_view.Table)
            e.Row.Cells(key).Value = val
        End If
    End Sub
End Class