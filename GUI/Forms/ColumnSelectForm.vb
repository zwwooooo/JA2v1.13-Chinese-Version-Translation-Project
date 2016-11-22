Public Class ColumnSelectForm
    Inherits SimpleFormBase

    Private _table As DataTable
    Private _grid As DataGridView
    Private _subTable As String

    Public Sub New(manager As DataManager, ByVal grid As DataGridView, Optional ByVal subTable As String = Nothing)
        MyBase.New(manager)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.btnOK.Top = myOKButton.Top
        Me.btnCancel.Top = myCancelButton.Top

        Me.btnOK.Left = myOKButton.Left
        Me.btnCancel.Left = myCancelButton.Left

        _grid = grid
        _subTable = subTable
        _table = DirectCast(_grid.DataSource, DataView).Table

        GetColumnProperties()
    End Sub

    Protected Overrides Function OkAction() As Boolean
        SetColumnProperties()
        'rewrite the schema so it'll remember for next time the app starts
        _dm.Database.SaveSchema()
        Return True
    End Function

    Private Sub GetColumnProperties()
        For Each c As DataColumn In _table.Columns
            If Not c.GetBooleanProperty(ColumnProperty.SubTable) OrElse (Not String.IsNullOrEmpty(_subTable) AndAlso c.ColumnName.StartsWith(_subTable)) Then
                ColumnCheckList.Items.Add(c.Caption, Not c.GetBooleanProperty(ColumnProperty.Grid_Hidden))
            End If
        Next
    End Sub

    Private Sub SetColumnProperties()
        For Each c As DataColumn In _table.Columns
            If Not c.GetBooleanProperty(ColumnProperty.SubTable) OrElse (Not String.IsNullOrEmpty(_subTable) AndAlso c.ColumnName.StartsWith(_subTable)) Then
                If ColumnCheckList.CheckedItems.Contains(c.Caption) Then
                    _table.Columns(c.ColumnName).RemoveProperty(ColumnProperty.Grid_Hidden)

                    If c.GetBooleanProperty(ColumnProperty.SubTable) Then
                        _dm.Database.Table(_subTable).Columns(c.ColumnName.Remove(0, _subTable.Length)).RemoveProperty(ColumnProperty.Grid_Hidden)
                    End If
                Else
                    c.SetProperty(ColumnProperty.Grid_Hidden, True)
                    If c.GetBooleanProperty(ColumnProperty.SubTable) Then
                        _dm.Database.Table(_subTable).Columns(c.ColumnName.Remove(0, _subTable.Length)).SetProperty(ColumnProperty.Grid_Hidden, True)
                    End If
                End If
            End If
        Next

    End Sub

End Class