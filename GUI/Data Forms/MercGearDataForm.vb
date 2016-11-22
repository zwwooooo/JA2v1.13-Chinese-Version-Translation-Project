Public Class MercGearDataForm
    Inherits BaseDataForm

    Public Sub New(manager As DataManager, ByVal recordID As Integer, ByVal formText As String)
        MyBase.New(manager, recordID, formText)
        InitializeComponent()
        Bind(Tables.MercStartingGear.Name, Tables.MercStartingGear.Fields.ID & "=" & _id)
    End Sub

#Region " Shared methods "
    Public Shared Sub Open(manager As DataManager, ByVal id As Integer, ByVal name As String)
        Dim formText As String = String.Format(DisplayText.MercGearDataFormText, manager.Name, name)
        If Not MainWindow.FormOpen(formText) Then
            Dim frm As New MercGearDataForm(manager, id, formText)
            MainWindow.ShowForm(frm)
        End If
    End Sub
#End Region

    Private Sub GearKitNameTextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles GearKitNameTextBox1.TextChanged
        If (GearKitNameTextBox1.Text.Length = 0) Then
            GearKitPage1.Text = "GEARKIT 1"
        Else
            GearKitPage1.Text = GearKitNameTextBox1.Text
        End If
    End Sub
    Private Sub GearKitNameTextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles GearKitNameTextBox2.TextChanged
        If (GearKitNameTextBox2.Text.Length = 0) Then
            GearKitPage2.Text = "GEARKIT 2"
        Else
            GearKitPage2.Text = GearKitNameTextBox2.Text
        End If
    End Sub
    Private Sub GearKitNameTextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles GearKitNameTextBox3.TextChanged
        If (GearKitNameTextBox3.Text.Length = 0) Then
            GearKitPage3.Text = "GEARKIT 3"
        Else
            GearKitPage3.Text = GearKitNameTextBox3.Text
        End If
    End Sub
    Private Sub GearKitNameTextBox4_TextChanged(sender As System.Object, e As System.EventArgs) Handles GearKitNameTextBox4.TextChanged
        If (GearKitNameTextBox4.Text.Length = 0) Then
            GearKitPage4.Text = "GEARKIT 4"
        Else
            GearKitPage4.Text = GearKitNameTextBox4.Text
        End If
    End Sub
    Private Sub GearKitNameTextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles GearKitNameTextBox5.TextChanged
        If (GearKitNameTextBox5.Text.Length = 0) Then
            GearKitPage5.Text = "GEARKIT 5"
        Else
            GearKitPage5.Text = GearKitNameTextBox5.Text
        End If
    End Sub
    Private Sub MercGearDataForm_Activated(sender As System.Object, e As System.EventArgs) Handles MyBase.Activated
        If _view.Table.Rows(_id).ItemArray(3).ToString.Length = 0 Then '3 is mGearkitName1
            GearKitPage1.Text = "GEARKIT 1"
        Else
            GearKitPage1.Text = _view.Table.Rows(_id).ItemArray(3).ToString
        End If
        If _view.Table.Rows(_id).ItemArray(68).ToString.Length = 0 Then '68 is mGearkitName2
            GearKitPage2.Text = "GEARKIT 2"
        Else
            GearKitPage2.Text = _view.Table.Rows(_id).ItemArray(68).ToString
        End If
        If _view.Table.Rows(_id).ItemArray(133).ToString.Length = 0 Then '133 is mGearkitName3
            GearKitPage3.Text = "GEARKIT 3"
        Else
            GearKitPage3.Text = _view.Table.Rows(_id).ItemArray(133).ToString
        End If
        If _view.Table.Rows(_id).ItemArray(198).ToString.Length = 0 Then '198 is mGearkitName4
            GearKitPage4.Text = "GEARKIT 4"
        Else
            GearKitPage4.Text = _view.Table.Rows(_id).ItemArray(198).ToString
        End If
        If _view.Table.Rows(_id).ItemArray(263).ToString.Length = 0 Then '263 is mGearkitName5
            GearKitPage5.Text = "GEARKIT 5"
        Else
            GearKitPage5.Text = _view.Table.Rows(_id).ItemArray(263).ToString
        End If
    End Sub
End Class
