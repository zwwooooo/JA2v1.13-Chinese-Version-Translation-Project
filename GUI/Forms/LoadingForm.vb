Public Class LoadingForm
    Private _saving As Boolean

    Public Shadows Sub Show(Optional ByVal saving As Boolean = False)
        _saving = saving
        If _saving Then
            InfoLabel.Text = DisplayText.Saving
        Else
            InfoLabel.Text = DisplayText.Loading
        End If
        MyBase.Show()
    End Sub

    Public Sub SetDataName(dataName As String)
        If _saving Then
            InfoLabel.Text = DisplayText.Saving & " " & dataName
        Else
            InfoLabel.Text = DisplayText.Loading & " " & dataName
        End If
        Application.DoEvents()
    End Sub

End Class