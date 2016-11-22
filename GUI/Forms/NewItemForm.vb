Public Class NewItemForm
    Inherits SimpleFormBase

    Public Sub New(manager As DataManager)
        MyBase.New(manager)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.btnOK.Top = myOKButton.Top
        Me.btnCancel.Top = myCancelButton.Top

        Me.btnOK.Left = myOKButton.Left
        Me.btnCancel.Left = myCancelButton.Left

        With ItemClassComboBox
            .DisplayMember = Tables.LookupTableFields.Name
            .ValueMember = Tables.LookupTableFields.ID
            .DataSource = _dm.Database.Table(Tables.ItemClasses)
        End With
    End Sub

    Protected Overrides Function OkAction() As Boolean
        If ItemClassComboBox.SelectedIndex >= 0 AndAlso ItemNameTextBox.Text.Length > 0 Then
            Try
                Dim id As Integer = _dm.Database.GetNextPrimaryKeyValue(Tables.Items.Name)
                Dim table As DataTable = _dm.Database.Table(Tables.Items.Name)
                Dim row As DataRow = table.NewRow
                row(Tables.Items.Fields.ID) = id
                row(Tables.Items.Fields.ItemClass) = ItemClassComboBox.SelectedValue
                row(Tables.Items.Fields.Name) = ItemNameTextBox.Text
                row(Tables.Items.Fields.ItemImage) = _dm.ItemImages.BigItemImage(0, 0)
                table.Rows.Add(row)
                ItemDataForm.Open(_dm, row(Tables.Items.Fields.ID), row(Tables.Items.Fields.Name))

                Return True
            Catch ex As Exception
                ErrorHandler.ShowError("Unable to add new item.", ex)
            End Try
        Else
            ErrorHandler.ShowWarning("Please enter a name and class for the item.")
            Return False
        End If
    End Function

End Class