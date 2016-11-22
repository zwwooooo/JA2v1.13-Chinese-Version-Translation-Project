Public Class CustomFilterForm
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
        Return True
    End Function

    Public Property Filter() As String
        Get
            Return FilterTextBox.Text
        End Get
        Set(ByVal value As String)
            FilterTextBox.Text = value
        End Set
    End Property

End Class