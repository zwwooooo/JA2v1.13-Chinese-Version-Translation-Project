Public Class DataManager
    Protected _dataDirectory As String
    Protected _gameDirRussianPath As String
    Protected _gameDirPolishPath As String
    Protected _gameDirGermanPath As String
    Protected _gameDirFrenchPath As String
    Protected _gameDirItalianPath As String
    Protected _gameDirChinesePath As String
    Protected _gameDirDutchPath As String
    Protected _gameDirTaiwanesePath As String
    Protected _Hide_Nothing_Items As Boolean

    Protected WithEvents _itemImages As New ItemImageManager
    Protected WithEvents _db As XmlDB

    Protected Const TableData As String = "TableData"
    Protected Const WorkingDir As String = "XmlWorkingData"
    Protected Const WorkingFile As String = "EditorData.xml"
    Protected _useWorkingDir As Boolean

    Public Event BeforeLoadData(sender As DataManager)
    Public Event AfterLoadData(sender As DataManager)
    Public Event BeforeSaveData(sender As DataManager)
    Public Event AfterSaveData(sender As DataManager)

    Public Event BeforeLoadWorkingData(sender As DataManager)
    Public Event AfterLoadWorkingData(sender As DataManager)
    Public Event BeforeSaveWorkingData(sender As DataManager)
    Public Event AfterSaveWorkingData(sender As DataManager)

    Protected _imageTypeCount As Integer

    Public Sub New(schemaName As String, dataDirectory As String, gameDirRussianPath As String, gameDirPolishPath As String, gameDirGermanPath As String,
               gameDirFrenchPath As String, gameDirItalianPath As String, gameDirChinesePath As String, gameDirDutchPath As String,
               gameDirTaiwanesePath As String, Hide_Nothing_Items As Boolean, Optional useWorkingDirectory As Boolean = True)

        If String.IsNullOrEmpty(dataDirectory) Then
            Throw New ArgumentNullException("Missing data directory information - check XmlEditorInit.xml")
        End If

        _dataDirectory = dataDirectory
        _gameDirRussianPath = gameDirRussianPath
        _gameDirPolishPath = gameDirPolishPath
        _gameDirGermanPath = gameDirGermanPath
        _gameDirFrenchPath = gameDirFrenchPath
        _gameDirItalianPath = gameDirItalianPath
        _gameDirChinesePath = gameDirChinesePath
        _gameDirDutchPath = gameDirDutchPath
        _gameDirTaiwanesePath = gameDirTaiwanesePath
        _useWorkingDir = useWorkingDirectory
        _Hide_Nothing_Items = Hide_Nothing_Items

        If Not IO.Directory.Exists(Me.TableDirectory) Then IO.Directory.CreateDirectory(Me.TableDirectory)
        If Not IO.Directory.Exists(Me.WorkingDirectory) Then IO.Directory.CreateDirectory(Me.WorkingDirectory)

        If Not _useWorkingDir AndAlso IO.File.Exists(Me.WorkingDirectory & WorkingFile) Then IO.File.Delete(Me.WorkingDirectory & WorkingFile)

        Dim schemaFileName As String = Me.WorkingDirectory & schemaName & ".xsd"
        _db = New XmlDB(schemaName, schemaFileName, Me)
    End Sub

    Public ReadOnly Property Database As XmlDB
        Get
            Return _db
        End Get
    End Property

    Public ReadOnly Property ItemImages As ItemImageManager
        Get
            Return _itemImages
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _dataDirectory.Substring(0, _dataDirectory.LastIndexOf("\"c))
        End Get
    End Property

    Public ReadOnly Property BaseDirectory() As String
        Get
            Return _dataDirectory & "\"
        End Get
    End Property

    Public ReadOnly Property TableDirectory() As String
        Get
            Return _dataDirectory & TableData & "\"
        End Get
    End Property

    Public ReadOnly Property UseWorkingDirectory As Boolean
        Get
            Return _useWorkingDir
        End Get
    End Property

    Public ReadOnly Property HideNothingItems As Boolean
        Get
            Return _Hide_Nothing_Items
        End Get
    End Property

    Public ReadOnly Property WorkingDirectory() As String
        Get
            Return _dataDirectory & WorkingDir & "\"
        End Get
    End Property

    Public ReadOnly Property ImageTypeCount As Integer
        Get
            Return _imageTypeCount
        End Get
    End Property

    Public Sub LoadImages()
        _imageTypeCount = GetImageTypeCount()
        _itemImages.LoadAllImages(_dataDirectory, _imageTypeCount)
    End Sub

    Public Sub CreateDatabase()
        _db.DataSet = MakeDB(BaseDirectory, TableDirectory, _db.SchemaName, _db.SchemaFileName)
        _db.LoadProperties(_db.SchemaFileName)
    End Sub

    Public Sub LoadData()
        If _useWorkingDir AndAlso IO.File.Exists(Me.WorkingDirectory & WorkingFile) Then
            RaiseEvent BeforeLoadWorkingData(Me)
            _db.LoadWorkingData(Me.WorkingDirectory & WorkingFile)
            RaiseEvent AfterLoadWorkingData(Me)
        Else
            _db.LoadAllData()
        End If
    End Sub

    Public Sub SaveData()
        RaiseEvent BeforeSaveWorkingData(Me)
        _db.SaveWorkingData(Me.WorkingDirectory & WorkingFile) 'always save working data, it just gets deleted if turned off
        RaiseEvent AfterSaveWorkingData(Me)
        _db.SaveAllData()
    End Sub

    Public Function GetLanguageSpecificTableDirectory(ByVal filename As String) As String
        Dim path As String = Nothing
        Dim dataDir As String = Nothing
        Const tableDataDir As String = TableData & "\"

        ' TODO.RW: Sprachen ausbessern
        Dim language As String = GetLanguageFromFile(filename)
        If (Not String.IsNullOrEmpty(language)) Then
            Select Case language    ' e.g: C:\JA2_RUSSIAN_FILES\Data-1.13\TableData
                Case "Russian"
                    If Not String.IsNullOrEmpty(_gameDirRussianPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirRussianPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "German"
                    If Not String.IsNullOrEmpty(_gameDirGermanPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirGermanPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "Polish"
                    If Not String.IsNullOrEmpty(_gameDirPolishPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirPolishPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "Italian"
                    If Not String.IsNullOrEmpty(_gameDirItalianPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirItalianPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "French"
                    If Not String.IsNullOrEmpty(_gameDirFrenchPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirFrenchPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "Chinese"
                    If Not String.IsNullOrEmpty(_gameDirChinesePath) Then
                        dataDir = System.IO.Path.Combine(_gameDirChinesePath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "Dutch"
                    If Not String.IsNullOrEmpty(_gameDirDutchPath) Then
                        dataDir = System.IO.Path.Combine(_gameDirDutchPath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
                Case "Taiwanese"
                    If Not String.IsNullOrEmpty(_gameDirTaiwanesePath) Then
                        dataDir = System.IO.Path.Combine(_gameDirTaiwanesePath, _dataDirectory)
                        path = System.IO.Path.Combine(dataDir, tableDataDir)
                    End If
            End Select
        End If

        If String.IsNullOrEmpty(path) Then
            path = TableDirectory()  ' e.g: Data-1.13\TableData
        End If

        Return path

    End Function

    Private Sub _db_AfterLoadAll(sender As XmlDB) Handles _db.AfterLoadAll
        RaiseEvent AfterLoadData(Me)
    End Sub

    Private Sub _db_AfterSaveAll(sender As XmlDB) Handles _db.AfterSaveAll
        RaiseEvent AfterSaveData(Me)
    End Sub

    Private Sub _db_BeforeLoadAll(sender As XmlDB) Handles _db.BeforeLoadAll
        RaiseEvent BeforeLoadData(Me)
    End Sub

    Private Sub _db_BeforeSaveAll(sender As XmlDB) Handles _db.BeforeSaveAll
        RaiseEvent BeforeSaveData(Me)
    End Sub

    Protected Function GetImageTypeCount() As Integer
        Dim interfaceDir As String = Me.BaseDirectory & "Interface"
        Return IO.Directory.EnumerateFiles(interfaceDir, "mdp*.sti").Count
    End Function

End Class
