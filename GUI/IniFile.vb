Public Class IniFile
    Protected Const MaxDataDir As Integer = 9
    Protected Shared dataDir(MaxDataDir) As String
    Protected Shared apMax As Integer
    Protected Shared useWorkingDir As Boolean
    Protected Shared HideNothingItems As Boolean = True

    Protected Shared languageSpecificRussianGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificPolishGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificGermanGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificItalianGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificFrenchGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificDutchGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificChineseGameDirPath(MaxDataDir) As String
    Protected Shared languageSpecificTaiwaneseGameDirPath(MaxDataDir) As String

    Public Shared ReadOnly Property DataDirectory(index As Integer) As String
        Get
            Return dataDir(index)
        End Get
    End Property

    Public Shared ReadOnly Property UseWorkingDirectory As Boolean
        Get
            Return useWorkingDir
        End Get
    End Property

    Public Shared ReadOnly Property APMaximum() As Integer
        Get
            Return apMax
        End Get
    End Property

    Public Shared ReadOnly Property Hide_Nothing_Items() As Boolean
        Get
            Return HideNothingItems
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Russian_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificRussianGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Polish_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificPolishGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_German_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificGermanGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Italian_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificItalianGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_French_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificFrenchGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Chinese_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificChineseGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Taiwanese_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificTaiwaneseGameDirPath(index)
        End Get
    End Property

    Public Shared ReadOnly Property LanguageSpecific_Dutch_GameDirPath(index As Integer) As String
        Get
            Return languageSpecificDutchGameDirPath(index)
        End Get
    End Property

    Public Shared Sub ReadFile(ByVal fileName As String)
        Dim xr As New Xml.XmlTextReader(fileName)
        Dim curNode As String = ""
        Dim curValue As String = ""
        While xr.Read
            If xr.NodeType = Xml.XmlNodeType.Element Then
                curNode = xr.Name
            ElseIf xr.NodeType = Xml.XmlNodeType.Text Then
                curValue = xr.Value
                Select Case curNode
                    Case "AP_Maximum"
                        apMax = curValue
                    Case "Use_Working_Directory"
                        useWorkingDir = CBool(curValue)
                    Case "Hide_Nothing_Items"
                        HideNothingItems = CBool(curValue)
                    Case Else
                        If IsNumeric(curNode.Substring(curNode.Length - 1)) Then
                            Dim index As Integer = CInt(curNode.Substring(curNode.LastIndexOf("_") + 1)) - 1
                            If curNode.StartsWith("Data_Directory") Then
                                dataDir(index) = curValue
                                If Not dataDir(index).EndsWith("\") Then dataDir(index) &= "\"
                            ElseIf curNode.StartsWith("GameDir_Russian_Path") Then
                                languageSpecificRussianGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_German_Path") Then
                                languageSpecificGermanGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_Polish_Path") Then
                                languageSpecificPolishGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_French_Path") Then
                                languageSpecificFrenchGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_Italian_Path") Then
                                languageSpecificItalianGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_Chinese_Path") Then
                                languageSpecificChineseGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_Dutch_Path") Then
                                languageSpecificDutchGameDirPath(index) = curValue
                            ElseIf curNode.StartsWith("GameDir_Taiwanese_Path") Then
                                languageSpecificTaiwaneseGameDirPath(index) = curValue
                            End If
                        End If
                End Select
            End If
        End While
        xr.Close()
    End Sub

End Class
