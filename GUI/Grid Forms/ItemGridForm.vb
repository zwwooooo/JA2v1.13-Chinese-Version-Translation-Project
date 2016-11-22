Public Class ItemGridForm
    Inherits GridForm
    Protected deletingRow As DataRow
    Dim enableEditingMenuItem As ToolStripMenuItem

    Protected Const EnableEditingText As String = "Enable &Editing"
    Protected Const DuplicateText As String = "&Duplicate"
    Protected Const NewText As String = "&New"
    Protected Const ViewText As String = "&View Details"

    Public Sub New(manager As DataManager, ByVal formText As String, tableName As String, Optional rowFilter As String = Nothing, Optional sort As String = Nothing, Optional rowStateFilter As DataViewRowState = DataViewRowState.CurrentRows, Optional subTable As String = Nothing)
        MyBase.New(manager, formText, tableName, rowFilter, sort, rowStateFilter, subTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formText
        With Grid
            .AllowUserToDeleteRows = True
            .RowTemplate.Height = _dm.ItemImages.GreatestBigImageHeight
            .RowHeadersVisible = True
        End With

        enableEditingMenuItem = New ToolStripMenuItem(EnableEditingText, Nothing, AddressOf EnableEditingMenuItem_Click)
        enableEditingMenuItem.CheckState = CheckState.Unchecked

        GridContextMenu.Items.Add(ViewText, Nothing, AddressOf ViewMenuItem_Click)
        GridContextMenu.Items.Add(NewText, Nothing, AddressOf NewMenuItem_Click)
        GridContextMenu.Items.Add(DuplicateText, Nothing, AddressOf DuplicateMenuItem_Click)
        GridContextMenu.Items.Add(enableEditingMenuItem)
    End Sub

    Protected Sub Grid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid.CellMouseDoubleClick
        If e.RowIndex <> -1 Then
            ItemDataForm.Open(_dm, Grid.Rows(e.RowIndex).Cells(Tables.Items.Fields.ID).Value, Grid.Rows(e.RowIndex).Cells(Tables.Items.Fields.Name).Value)
        End If
    End Sub

    Private Sub Grid_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles Grid.UserDeletingRow
        deletingRow = _view.Table.Rows.Find(e.Row.Cells(Tables.Items.Fields.ID).Value)
    End Sub

    Private Sub Grid_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Grid.UserDeletedRow
        If deletingRow IsNot Nothing AndAlso deletingRow.RowState = DataRowState.Deleted Then
            deletingRow.RejectChanges()
            _dm.Database.DeleteRow(_view.Table, deletingRow(Tables.Items.Fields.ID))
        End If
    End Sub

    Private Sub ViewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Grid.SelectedRows.Count > 0 Then
            ItemDataForm.Open(_dm, Grid.SelectedRows(0).Cells(Tables.Items.Fields.ID).Value, Grid.SelectedRows(0).Cells(Tables.Items.Fields.Name).Value)
        End If
    End Sub

    Private Sub NewMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New NewItemForm(_dm)
        If Not MainWindow.FormOpen(frm.Text) Then MainWindow.ShowForm(frm)
    End Sub

    Private Sub DuplicateMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Grid.SelectedRows.Count > 0 Then
            Dim key As Integer = Grid.SelectedRows(0).Cells(Tables.Items.Fields.ID).Value
            Dim row As DataRow = _dm.Database.DuplicateRow(_view.Table, key)

            ItemDataForm.Open(_dm, row(Tables.Items.Fields.ID), row(Tables.Items.Fields.Name))
        End If
    End Sub

    Private Sub EnableEditingMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        enableEditingMenuItem.Checked = Not enableEditingMenuItem.Checked
        With Grid
            .ReadOnly = Not enableEditingMenuItem.Checked
            If Not .ReadOnly Then
                .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            Else
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End If
        End With

        EnableContextMenuItems()
    End Sub

End Class