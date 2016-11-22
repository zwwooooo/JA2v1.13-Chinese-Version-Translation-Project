Public Class ChangeClassForm
    Inherits SimpleFormBase

    Protected _view As DataView
    Protected _form As ItemDataForm

    'the view argument here needs to be the single-row view from the itemdataform
    Public Sub New(manager As DataManager, ByVal vw As DataView, ByVal itemForm As ItemDataForm)
        MyBase.New(manager)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.btnOK.Top = myOKButton.Top
        Me.btnCancel.Top = myCancelButton.Top

        Me.btnOK.Left = myOKButton.Left
        Me.btnCancel.Left = myCancelButton.Left

        _view = vw
        _form = itemForm

        With ItemClassListBox
            .DataSource = _dm.Database.Table(Tables.ItemClasses)
            .DisplayMember = Tables.LookupTableFields.Name
            .ValueMember = Tables.LookupTableFields.ID
            .SelectedValue = _view(0)(Tables.Items.Fields.ItemClass)
        End With

    End Sub

    Protected Overrides Function OkAction() As Boolean
        Dim id As Integer = _view(0)(Tables.Items.Fields.ID)
        _view(0)(Tables.Items.Fields.ItemClass) = ItemClassListBox.SelectedValue
        _view(0).EndEdit()

        _view = Nothing

        Dim frm As New ItemDataForm(_dm, id, _form.Text)
        frm.Top = _form.Top
        frm.Left = _form.Left

        _form.Close()
        _form = Nothing

        MainWindow.ShowForm(frm)
        Return True
    End Function
End Class