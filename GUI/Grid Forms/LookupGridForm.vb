Public Class LookupGridForm
    Inherits GridForm

    Public Sub New(manager As DataManager, ByVal formText As String, tableName As String, Optional rowFilter As String = Nothing, Optional sort As String = Nothing, Optional rowStateFilter As DataViewRowState = DataViewRowState.CurrentRows)
        MyBase.New(manager, formText, tableName, rowFilter, sort, rowStateFilter)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formText
        With Me.Grid
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .RowHeadersVisible = True
        End With
        EnableContextMenuItems()
    End Sub

End Class