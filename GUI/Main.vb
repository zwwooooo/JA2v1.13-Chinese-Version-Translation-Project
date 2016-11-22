Imports System.Globalization
Imports GUI.GUI
Imports System.Threading

Module Main
    'this file creates the xsd from scratch
    'we don't want a typed-dataset, because it's less flexible
    'once we have a flexible way to do the GUI, this format should pay off
    Private Const SchemaName As String = "JA2Data"

    Public GameData(9) As DataManager
    Public GameDataCount As Integer
    Public MainWindow As MainForm
    Public Splash As SplashForm

    Public Sub Main()
        If CheckForDuplicateProcess(My.Application.Info.AssemblyName) Then End

        Try
            AddHandler ErrorHandler.FatalError, AddressOf ExitDueToError

            ' RoWa21: Changed the thread of the application.
            ' This is used, so that the DECIMAL numeric up down control for the "ShotsPer4Turns"
            ' always displays a "." instead of a "," for the decimal delimiter.
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

            IniFile.ReadFile("XMLEditorInit.xml")
            Dim useWorkingDir As Boolean = IniFile.UseWorkingDirectory

            If My.Application.Info.Version.Major <> My.Settings.Last_Version_Major OrElse
                My.Application.Info.Version.Minor <> My.Settings.Last_Version_Minor Then
                useWorkingDir = False
            End If

            Splash = New SplashForm()
            Splash.Show()

            Splash.UpdateLoadingText(DisplayText.Initializing)

            For i As Integer = 0 To GameData.Length - 1
                If Not String.IsNullOrEmpty(IniFile.DataDirectory(i)) Then
                    Splash.UpdateCurrentDirectory(IniFile.DataDirectory(i))

                    GameData(i) = New DataManager(SchemaName, IniFile.DataDirectory(i), IniFile.LanguageSpecific_Russian_GameDirPath(i), IniFile.LanguageSpecific_Polish_GameDirPath(i), _
                                    IniFile.LanguageSpecific_German_GameDirPath(i), IniFile.LanguageSpecific_French_GameDirPath(i), _
                                    IniFile.LanguageSpecific_Italian_GameDirPath(i), IniFile.LanguageSpecific_Chinese_GameDirPath(i), _
                                    IniFile.LanguageSpecific_Dutch_GameDirPath(i), IniFile.LanguageSpecific_Taiwanese_GameDirPath(i), _
                                    IniFile.Hide_Nothing_Items(), useWorkingDir)
                    AddHandler GameData(i).Database.AfterLoadAll, AddressOf DB_AfterLoadAll
                    AddHandler GameData(i).Database.LoadingTable, AddressOf DB_LoadingTable

                    GameDataCount += 1

                    Splash.UpdateLoadingText(DisplayText.CreatingDatabase)
                    GameData(i).CreateDatabase()

                    Splash.UpdateLoadingText(DisplayText.LoadingImages)
                    GameData(i).LoadImages()

                    Splash.UpdateLoadingText(DisplayText.LoadingXmlFiles)
                    GameData(i).LoadData()

                    Splash.UpdateLoadingText(DisplayText.SearchingForOtherDirectories)
                End If
            Next

            Splash.UpdateLoadingText(DisplayText.LoadingSettings)
            SettingsUtility.LoadSettings()

            Splash.UpdateLoadingText(DisplayText.ApplicationStarting)
            Splash.Hide()

            MainWindow = New MainForm
            Application.Run(MainWindow)

            SettingsUtility.SaveSettings()
        Catch ex As Exception
            ErrorHandler.ShowError(DisplayText.UnhandledError, ex)
        End Try
    End Sub

    Private Sub DB_AfterLoadAll(sender As XmlDB)
        Splash.UpdateLoadingText(String.Format(DisplayText.BuildingItemTable, sender.DataManager.Name))
    End Sub

    Private Sub DB_LoadingTable(sender As XmlDB, ByVal fileName As String)
        If fileName.Contains("\") Then fileName = fileName.Remove(0, fileName.LastIndexOf("\") + 1)
        Splash.UpdateLoadingText(String.Format(DisplayText.LoadingTables, sender.DataManager.Name, fileName))
    End Sub

    Private Sub ExitDueToError()
        End
    End Sub


    'Determines if there already is a 'processName' running in the local host.
    'Returns true if it finds more than 'one processName' running
    Private Function CheckForDuplicateProcess(ByVal processName As String) As Boolean
        'function returns true if it finds more than one 'processName' running
        Dim Procs() As Process
        Dim proc As Process
        'get ALL processes running on this machine in all desktops
        'this also finds all services running as well.
        Procs = Process.GetProcesses()
        Dim count As Integer = 0
        For Each proc In Procs
            If proc.ProcessName.ToString.Equals(processName) Then
                count += 1
            End If
        Next proc
        If count > 1 Then
            Return True
        Else
            Return False
        End If
    End Function
End Module
