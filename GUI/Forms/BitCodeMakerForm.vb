Public Class BitCodeMakerForm
    Protected _dm As DataManager
    Protected _sourceTable As DataTable

    Public Sub New(manager As DataManager, formText As String, sourceTableName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _dm = manager
        Me.Text = formText
        _sourceTable = _dm.Database.Table(sourceTableName)

        BuildCheckBoxes()
    End Sub

    Protected Sub BuildCheckBoxes()
        Dim rowCount As Integer
        Dim colCount As Integer
        Dim lastText As String = Nothing

        CheckBoxPanel.Controls.Clear()
        Me.CheckBoxPanel.ColumnStyles.Clear()
        Me.CheckBoxPanel.RowStyles.Clear()

        For Each row As DataRow In (From r In _sourceTable.AsEnumerable Where r.RowState <> DataRowState.Deleted AndAlso r(0) > 0 Order By r(1) Select r)
            Dim text As String
            If CStr(row(1)).IndexOf(" ") > 0 Then
                text = CStr(row(1)).Substring(0, CStr(row(1)).IndexOf(" "))
            Else
                text = CStr(row(1))
            End If
            If Not String.IsNullOrEmpty(lastText) AndAlso lastText <> text Then
                colCount += 1
                rowCount = 0
            End If

            CreateCheckBox(CDec(row(0)), CStr(row(1)), rowCount, colCount)

            lastText = text
            rowCount += 1
        Next
    End Sub

    Protected Sub CreateCheckBox(value As Decimal, text As String, row As Integer, col As Integer)
        Dim cb As New CheckBox With {.Name = Replace(text, " ", "_") & "CheckBox", .Tag = value, .Text = text, .AutoSize = True, .Dock = DockStyle.Fill}
        AddHandler cb.CheckStateChanged, AddressOf Checkbox_Checked
        If row > Me.CheckBoxPanel.RowStyles.Count - 1 Then Me.CheckBoxPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        If col > Me.CheckBoxPanel.ColumnStyles.Count - 1 Then Me.CheckBoxPanel.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        Me.CheckBoxPanel.Controls.Add(cb, col, row)
    End Sub

    Protected Sub Checkbox_Checked(sender As Object, e As EventArgs)
        Dim cb As CheckBox = DirectCast(sender, CheckBox)

        If cb.Checked Then
            If Not String.IsNullOrEmpty(CodeTextBox.Text) Then CodeTextBox.Text = CDec(CodeTextBox.Text) + CDec(cb.Tag) Else CodeTextBox.Text = CDec(cb.Tag)
        Else
            If Not String.IsNullOrEmpty(CodeTextBox.Text) Then CodeTextBox.Text = CDec(CodeTextBox.Text) - CDec(cb.Tag)
        End If
    End Sub

    Protected Sub UncheckAll()
        For Each c As Control In CheckBoxPanel.Controls
            DirectCast(c, CheckBox).Checked = False
        Next
    End Sub

    Private Sub AddButton_Click(sender As System.Object, e As System.EventArgs) Handles AddButton.Click
        AddCode()
    End Sub

    Protected Sub AddCode()
        If Not String.IsNullOrEmpty(CodeTextBox.Text) AndAlso Not String.IsNullOrEmpty(NameTextBox.Text) Then
            Dim row As DataRow = _sourceTable.Rows.Find(CDec(CodeTextBox.Text))
            If row Is Nothing Then
                row = _sourceTable.NewRow
                row(0) = CDec(CodeTextBox.Text)
                row(1) = NameTextBox.Text
                _sourceTable.Rows.Add(row)
                CodeTextBox.Text = Nothing
                NameTextBox.Text = Nothing

                BuildCheckBoxes()
            End If
        End If
    End Sub

    Private Sub CloseButton_Click(sender As System.Object, e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

End Class