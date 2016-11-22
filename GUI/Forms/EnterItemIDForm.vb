Public Class EnterItemIDForm
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

    End Sub

    Protected Overrides Function OkAction() As Boolean
        If Not IsNumeric(ItemIDTextBox.Text) Then
            ErrorHandler.ShowWarning("Item ID must be a number.")
            Return False
        End If

        Dim r As DataRow = _dm.Database.Table(Tables.Items.Name).Rows.Find(ItemIDTextBox.Text)
        If r Is Nothing Then
            ErrorHandler.ShowWarning("Unrecognized item ID.", MessageBoxIcon.Hand)
            Return False
        End If

        ItemDataForm.Open(_dm, CInt(ItemIDTextBox.Text), r(Tables.Items.Fields.Name))

        Return True
    End Function


End Class