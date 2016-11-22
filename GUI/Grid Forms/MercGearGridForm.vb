Public Class MercGearGridForm
    Inherits GridForm
    Protected Const ViewText As String = "&View Details"

    Public Sub New(manager As DataManager, ByVal formText As String, tableName As String, Optional rowFilter As String = Nothing, Optional sort As String = Nothing, Optional rowStateFilter As DataViewRowState = DataViewRowState.CurrentRows)
        MyBase.New(manager, formText, tableName, rowFilter, sort, rowStateFilter)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formText
        With Me.Grid
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = True
        End With

        GridContextMenu.Items.Add(ViewText, Nothing, AddressOf ViewMenuItem_Click)
        EnableContextMenuItems()
    End Sub

    Private Sub ViewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Grid.SelectedRows.Count > 0 Then
            MercGearDataForm.Open(_dm, Grid.SelectedRows(0).Cells(Tables.MercStartingGear.Fields.ID).Value, Grid.SelectedRows(0).Cells(Tables.MercStartingGear.Fields.Name).Value)
        End If
    End Sub

    Protected Sub Grid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If e.RowIndex <> -1 Then
            MercGearDataForm.Open(_dm, Grid.Rows(e.RowIndex).Cells(Tables.MercStartingGear.Fields.ID).Value, Grid.Rows(e.RowIndex).Cells(Tables.MercStartingGear.Fields.Name).Value)
        End If
    End Sub
End Class