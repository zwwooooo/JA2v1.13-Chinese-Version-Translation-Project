Public Module Localization
    Public Function IsLanguageSpecificFile(ByVal filename As String) As Boolean
        Dim langSpec = False

        Dim language = GetLanguageFromFile(filename)
        If (language <> "") Then
            langSpec = True
        End If

        Return langSpec

    End Function

    Public Function GetLanguageFromFile(ByVal filename As String) As String

        Dim language = ""   ' English -> Language neutral

        If filename.Contains("German.") Then
            language = "German"
        ElseIf filename.Contains("Russian.") Then
            language = "Russian"
        ElseIf filename.Contains("Polish.") Then
            language = "Polish"
        ElseIf filename.Contains("Italian.") Then
            language = "Italian"
        ElseIf filename.Contains("French.") Then
            language = "French"
        ElseIf filename.Contains("Chinese.") Then
            language = "Chinese"
        ElseIf filename.Contains("Taiwanese.") Then
            language = "Taiwanese"
        ElseIf filename.Contains("Dutch.") Then
            language = "Dutch"
        End If

        Return language

    End Function

End Module
