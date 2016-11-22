Public Class BaseDataForm
    Protected _view As DataView
    Protected _id As Integer
    Protected _dm As DataManager
    Public Function ToBinary(ByVal x As UInt32) As String

        'Convert an unsigned integer to a binary string

        Dim temp As String = ""
        Do
            If CBool(x Mod 2) Then
                temp = temp + "1"
            Else
                temp = temp + "0"
            End If
            x = CUInt(x \ 2)
            If x < 1 Then Exit Do
        Loop
        Return temp
    End Function
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(manager As DataManager, ByVal recordID As Integer, ByVal formText As String)
        LoadingForm.Show()
        Application.DoEvents()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.AcceptButton = OKButton
        _dm = manager
        _id = recordID
        If Not formText.Contains(_dm.Name & ":") Then Me.Text = _dm.Name & ": " & formText Else Me.Text = formText
    End Sub

#Region " Data Binding "
    Protected Sub Bind(ByVal tableName As String, ByVal filter As String)
        _view = New DataView(_dm.Database.Table(tableName), filter, "", DataViewRowState.CurrentRows)
        _view(0).BeginEdit()
        BindControls(CType(Me, Control))
    End Sub

    Protected Sub BindControls(ByVal parent As Control)
        For Each ctl As Control In parent.Controls
            If ctl.Tag IsNot Nothing Then
                Dim col As DataColumn = _view.Table.Columns(ctl.Tag)

                If TypeOf ctl Is TextBox OrElse TypeOf ctl Is Label OrElse TypeOf ctl Is MaskedTextBox Then
                    ctl.DataBindings.Add("Text", _view, ctl.Tag, True, DataSourceUpdateMode.OnPropertyChanged)
                ElseIf TypeOf ctl Is CheckBox OrElse TypeOf ctl Is RadioButton Then
                    ctl.DataBindings.Add("Checked", _view, ctl.Tag, True, DataSourceUpdateMode.OnPropertyChanged)
                ElseIf TypeOf ctl Is NumericUpDown Then
                    ctl.DataBindings.Add("Value", _view, ctl.Tag, True, DataSourceUpdateMode.OnPropertyChanged)
                ElseIf TypeOf ctl Is PictureBox Then
                    ctl.DataBindings.Add("Image", _view, ctl.Tag, True, DataSourceUpdateMode.OnPropertyChanged)
                ElseIf TypeOf ctl Is ComboBox Then
                    ctl.DataBindings.Add("SelectedValue", _view, ctl.Tag, True, DataSourceUpdateMode.OnPropertyChanged)

                    Dim lookupTable As String = col.GetStringProperty(ColumnProperty.Lookup_Table)
                    If lookupTable IsNot Nothing Then
                        Dim lookupTextField As String = col.GetStringProperty(ColumnProperty.Lookup_TextColumn)
                        Dim lookupValueField As String = col.GetStringProperty(ColumnProperty.Lookup_ValueColumn)
                        Dim lookupFilter As String = col.GetStringProperty(ColumnProperty.Lookup_Filter)
                        Dim lookupSort As String = col.GetStringProperty(ColumnProperty.Lookup_Sort)
                        If lookupSort Is Nothing Then lookupSort = lookupTextField

                        Dim cbo As ComboBox = DirectCast(ctl, ComboBox)
                        cbo.ValueMember = lookupValueField
                        cbo.DisplayMember = lookupTextField
                        cbo.DataSource = New DataView(_dm.Database.Table(lookupTable), lookupFilter, lookupSort, DataViewRowState.CurrentRows)
                    End If
                End If

                Dim tooltip As String = col.GetStringProperty(ColumnProperty.ToolTip)
                If tooltip IsNot Nothing Then DataToolTip.SetToolTip(ctl, tooltip)
            End If

            If ctl.Controls.Count > 0 Then
                BindControls(ctl)
            End If
        Next
    End Sub

    Protected Overridable Function CommitData() As Boolean
        _view(0).EndEdit()
        _view(0).Row.AcceptChanges()
        Return True
    End Function

    Protected Overridable Sub DoCancelData()
    End Sub

    Protected Sub CancelData()
        If _view IsNot Nothing Then
            If _view.Count > 0 Then _view(0).CancelEdit()
            DoCancelData()
            _view.Dispose()
        End If
    End Sub

#End Region

#Region " Buttons "

    Protected Overridable Sub ApplyButtonClicked()
    End Sub

    Private Sub ApplyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyButton.Click
        If CommitData() Then
            ApplyButtonClicked()
            _view(0).BeginEdit()
        End If
    End Sub

    Protected Overridable Sub OKButtonClicked()
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        If CommitData() Then
            OKButtonClicked()
            Close()
            _view.Dispose()
        End If
    End Sub

    Protected Overridable Sub CancelButtonClicked()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton1.Click
        CancelButtonClicked()
        CancelData()
        Close()
    End Sub

    Private Sub DataForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing AndAlso Me.DialogResult <> Windows.Forms.DialogResult.OK Then
            CancelButtonClicked()
            CancelData()
        End If
    End Sub

#End Region


#Region " Status Bar "
    Private Sub DataForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        UpdateStatusBar()
    End Sub

    Private Sub DataForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Normal
        LoadingForm.Close()
    End Sub

    Protected Sub UpdateStatusBar()
        If MainWindow IsNot Nothing Then MainWindow.StatusLabel.Text = "Editing " & Me.Text
    End Sub

    Private Sub Form_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If MainWindow IsNot Nothing Then
            If Not Me.Visible Then MainWindow.StatusLabel.Text = ""
        End If
    End Sub

#End Region


End Class