Imports System.Windows.Forms

Public Class MainForm
    Private WithEvents _client As MdiClient
    Private _activeDataSet As DataManager = GameData(0)

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = String.Format(DisplayText.MainFormTitle, _activeDataSet.Name)

        ' RoWa21: Remove unused language specific ammo strings
        RemoveUnusedLanguageSpecificAmmoStringMenuItems()

        With My.Settings

            Me.BackgroundImageToolStripMenuItem.Checked = .View_Background
            Me.ToolBarToolStripMenuItem.Checked = .View_Toolbar
            Me.StatusBarToolStripMenuItem.Checked = .View_StatusBar

            If .MainForm_Size <> New Size(0, 0) Then Me.Size = .MainForm_Size
            If .MainForm_Location <> New Point(0, 0) Then Me.Location = .MainForm_Location
            Me.WindowState = .MainForm_State
        End With

        For Each item As ToolStripMenuItem In Me.SelectDataToolStripMenuItem.DropDownItems
            If item.Tag > GameDataCount - 1 Then
                item.Visible = False
            Else
                item.Text = String.Format(item.Text, GameData(CInt(item.Tag)).Name)
            End If
        Next

        For Each ctl As Control In Me.Controls
            If TypeOf ctl Is MdiClient Then
                _client = ctl
                Exit For
            End If
        Next
        If Me.BackgroundImageToolStripMenuItem.Checked Then
            _client.BackgroundImage = My.Resources.DESKTOP
            _client.BackgroundImageLayout = ImageLayout.Stretch
        End If
        StatusLabel.Text = DisplayText.Welcome
    End Sub

    Private Sub RemoveUnusedLanguageSpecificAmmoStringMenuItems()
        Dim baseAmmoFolder As String = "Items\"
        Dim baseAmmoStrings As String = "AmmoStrings.xml"

        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.German & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(GermanToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.Russian & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(RussianToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.Polish & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(PolishToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.Italian & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(ItalianToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.French & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(FrenchToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.Chinese & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(ChineseToolStripMenuItem)
        End If
        If LanguageSpecificFileExist(baseAmmoFolder & OtherText.Dutch & "." & baseAmmoStrings) = False Then
            Me.CalibersToolStripMenuItem.DropDownItems.Remove(DutchToolStripMenuItem)
        End If

    End Sub

    Private Function LanguageSpecificFileExist(ByVal filename As String) As Boolean
        Dim exists As Boolean = True

        If System.IO.File.Exists(_activeDataSet.TableDirectory & filename) = False Then
            exists = False
        End If

        Return exists

    End Function

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        With My.Settings
            .View_Background = Me.BackgroundImageToolStripMenuItem.Checked
            .View_Toolbar = Me.ToolBarToolStripMenuItem.Checked
            .View_StatusBar = Me.StatusBarToolStripMenuItem.Checked

            If Me.WindowState = FormWindowState.Normal Then
                .MainForm_Location = Me.Location
                .MainForm_Size = Me.Size
            End If
            .MainForm_State = Me.WindowState
            .Save()
        End With
    End Sub

    Private Sub client_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles _client.Paint
        If _client.BackgroundImage IsNot Nothing Then
            Static flag As Boolean
            If Not flag Then
                Dim g As Graphics = e.Graphics
                g.DrawImage(_client.BackgroundImage, New RectangleF(0, 0, _client.Size.Width, _client.Size.Height))
            Else
                flag = True
                _client.Invalidate()
            End If
        End If
    End Sub

    Private Sub client_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles _client.Resize
        _client.Invalidate()
    End Sub

    Private Sub ChildForm_Resized(ByVal sender As Object, ByVal e As EventArgs)
        _client.Invalidate()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Splash.Show()
        Splash.UpdateLoadingText(DisplayText.ReloadingData)
        Me.Hide()
        Application.DoEvents()
        For i As Integer = 0 To GameDataCount - 1
            Splash.UpdateCurrentDirectory(GameData(i).Name)
            GameData(i).LoadData()
        Next
        Windows.Forms.Cursor.Current = Cursors.Arrow
        Splash.Hide()
        Me.Show()
    End Sub

    Friend Function FormOpen(ByVal formText As String) As Boolean
        For Each f As Form In MdiChildren
            If f.Text = formText Then
                f.Activate()
                Return True
            End If
        Next
        Return False
    End Function

    Friend Sub ShowForm(ByVal frm As Form)
        frm.MdiParent = Me
        AddHandler frm.Resize, AddressOf ChildForm_Resized
        frm.Show()
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Function FormatFormText(text As String) As String
        Return _activeDataSet.Name & ": " & text
    End Function

    Private Sub ShowItemGridForm(dm As DataManager, ByVal text As String, ByVal filter As String, ByVal subTable As String, Optional ByVal customFilter As String = Nothing)
        text = FormatFormText(text)
        If Not FormOpen(text) Then
            Dim frm As New ItemGridForm(dm, text, Tables.Items.Name, filter, , , subTable)
            If Not String.IsNullOrEmpty(customFilter) Then frm.Filter = customFilter
            ShowForm(frm)
        End If
    End Sub

    Private Sub ShowLookupGridForm(dm As DataManager, ByVal text As String, ByVal tableName As String)
        text = FormatFormText(text)
        If Not FormOpen(text) Then
            Dim frm As New LookupGridForm(dm, text, tableName)
            ShowForm(frm)
        End If
    End Sub

    Private Sub ShowMercGearGridForm(dm As DataManager, ByVal text As String, Optional ByVal filter As String = Nothing, Optional ByVal customFilter As String = Nothing)
        text = FormatFormText(text)
        If Not FormOpen(text) Then
            Dim frm As New MercGearGridForm(dm, text, Tables.MercStartingGear.Name, filter)
            If Not String.IsNullOrEmpty(customFilter) Then frm.Filter = customFilter
            ShowForm(frm)
        End If
    End Sub

    Private Sub ShowDataGridForm(dm As DataManager, ByVal text As String, ByVal tableName As String)
        text = FormatFormText(text)
        If Not FormOpen(text) Then
            Dim frm As New DataGridForm(dm, text, tableName)
            ShowForm(frm)
        End If
    End Sub

    Private Sub ShowChildDataGridForm(dm As DataManager, ByVal text As String, ByVal tableName As String)
        text = FormatFormText(text)
        If Not FormOpen(text) Then
            Dim frm As New ChildDataGridForm(dm, text, tableName)
            ShowForm(frm)
        End If
    End Sub

    Private Sub AllItemsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllItemsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, DisplayText.Items, "", Nothing)
    End Sub

    Private Function FormatItemClassText(itemClassName As String)
        Return DisplayText.Items & " - " & itemClassName
    End Function

    Private Function FormatDataText(dataTableName As String)
        Return DisplayText.Data & " - " & dataTableName
    End Function

    Private Sub GunsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GunsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Guns), Tables.Items.Fields.ItemClass & "=" & ItemClass.Gun & " AND " & Tables.Items.Fields.RocketLauncher & "= 0", Tables.Weapons.Name)
    End Sub

    Private Sub AmmoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmoToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Ammo), Tables.Items.Fields.ItemClass & "=" & ItemClass.Ammo, Tables.Magazines.Name)
    End Sub

    Private Sub LaunchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaunchersToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Launchers), Tables.Items.Fields.ItemClass & "=" & ItemClass.Launcher & " OR " & Tables.Items.Fields.RocketLauncher & "= 1", Tables.Weapons.Name)
    End Sub

    Private Sub GrenadesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrenadesToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Grenades), Tables.Items.Fields.ItemClass & "=" & ItemClass.Grenade, Tables.Explosives.Name)
    End Sub

    Private Sub ExplosivesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplosivesToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Explosives), Tables.Items.Fields.ItemClass & "=" & ItemClass.Bomb, Tables.Explosives.Name)
    End Sub

    Private Sub KnivesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KnivesToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Knives), Tables.Items.Fields.ItemClass & "=" & ItemClass.Knife & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.ThrowingKnife, Tables.Weapons.Name)
    End Sub

    Private Sub OtherWeaponsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtherWeaponsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.OtherWeapons), Tables.Items.Fields.ItemClass & "=" & ItemClass.Thrown & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Punch & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Tentacle, Tables.Weapons.Name)
    End Sub

    Private Sub ArmourToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArmourToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Armours), Tables.Items.Fields.ItemClass & "=" & ItemClass.Armour, Tables.Armours.Name)
    End Sub

    Private Sub FaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FaceToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.FaceGear), Tables.Items.Fields.ItemClass & "=" & ItemClass.Face, Nothing)
    End Sub

    Private Sub KitsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KitsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Kits), Tables.Items.Fields.ItemClass & "=" & ItemClass.MedKit & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Kit, Nothing)
    End Sub

    Private Sub KeysToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeysToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Keys), Tables.Items.Fields.ItemClass & "=" & ItemClass.Key, Nothing)
    End Sub

    Private Sub LoadBearingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBearingToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.LoadBearing), Tables.Items.Fields.ItemClass & "=" & ItemClass.LBE, Nothing)
    End Sub

    Private Sub MiscToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Misc), Tables.Items.Fields.ItemClass & "=" & ItemClass.Misc & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Money, Nothing)
    End Sub

    Private Sub NoneItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoneItemToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.None), Tables.Items.Fields.ItemClass & "=" & ItemClass.None, Nothing)
    End Sub

    Private Sub AttachmentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Attachments), Tables.Items.Fields.Attachment & "= 1", Nothing)
    End Sub

    Private Sub TonsOfGunsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TonsOfGunsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.TonsOfGunsMode), Tables.Items.Fields.TonsOfGuns & "= 1", Nothing)
    End Sub

    Private Sub NormalGunsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalGunsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.NormalGunsMode), Tables.Items.Fields.TonsOfGuns & "= 0", Nothing)
    End Sub

    Private Sub SciFiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SciFiToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.SciFiMode), Tables.Items.Fields.SciFi & "= 1", Nothing)
    End Sub

    Private Sub NonSciFiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonSciFiToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.RealisticMode), Tables.Items.Fields.SciFi & "= 0", Nothing)
    End Sub

    Private Sub ArmourClassesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArmourClassesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.ArmourClasses), Tables.ArmourClasses)
    End Sub

    Private Sub LBEClassesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBEClassesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.LbeClasses), Tables.LbeClasses)
    End Sub

    Private Sub InventorySilhouettesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventorySilhouettesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.InventorySilhouettes), Tables.Silhouettes)
    End Sub

    Private Sub NasAttachmentClassesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NasAttachmentClassesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.NasAttachmentClasses), Tables.NasAttachmentClasses)
    End Sub

    Private Sub CommonAttachmentPointsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CommonAttachmentPointsToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentPoints), Tables.AttachmentPoints)
    End Sub

    Private Sub AttachmentClassCodeMakerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AttachmentClassCodeMakerToolStripMenuItem.Click
        Dim text = FormatFormText("NAS Attachment Class Code Maker")
        If Not FormOpen(text) Then
            Dim frm As New BitCodeMakerForm(_activeDataSet, text, Tables.AttachmentPoints)
            ShowForm(frm)
        End If
    End Sub

    Private Sub AttachmentPointCodeMakerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AttachmentPointCodeMakerToolStripMenuItem.Click
        Dim text = FormatFormText("Attachment Point Code Maker")
        If Not FormOpen(text) Then
            Dim frm As New BitCodeMakerForm(_activeDataSet, text, Tables.AttachmentPoints)
            ShowForm(frm)
        End If
    End Sub

    Private Sub AttachmentClassesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AttachmentClassesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentClasses), Tables.AttachmentClasses)
    End Sub

    Private Sub AttachmentSystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentSystemToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentSystem), Tables.AttachmentSystem)
    End Sub

    Private Sub CursorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CursorsToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.Cursors), Tables.Cursors)
    End Sub

    Private Sub ExplosionTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplosionTypesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.ExplosionTypes), Tables.ExplosionTypes)
    End Sub

    Private Sub ItemClassesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemClassesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.ItemClasses), Tables.ItemClasses)
    End Sub

    Private Sub MergeTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MergeTypesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.MergeTypes), Tables.MergeTypes)
    End Sub

    Private Sub SkillChecksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkillChecksToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.SkillChecks), Tables.SkillCheckTypes)
    End Sub

    Private Sub WeaponClassesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaponClassesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.WeaponClasses), Tables.WeaponClasses)
    End Sub

    Private Sub WeaponTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeaponTypesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, FormatDataText(DisplayText.WeaponTypes), Tables.WeaponTypes)
    End Sub

    Private Sub ExplosionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplosionsToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.Explosions), Tables.ExplosionData)
    End Sub

    Private Sub TypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoTypes), Tables.AmmoTypes)
    End Sub

    Private Sub StandardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StandardToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.Sounds), Tables.Sounds)
    End Sub

    Private Sub BurstToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BurstToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.BurstSounds), Tables.BurstSounds)
    End Sub

    Private Sub PocketTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PocketTypesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.PocketTypes), Tables.Pockets)
    End Sub

    Private Sub NasAttachmentSlotsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NasAttachmentSlotsToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentSlots), Tables.AttachmentSlots)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim frm As New EnterItemIDForm(_activeDataSet)
        If Not FormOpen(frm.Text) Then ShowForm(frm)
    End Sub

    Private Sub CreateNewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewToolStripMenuItem.Click
        Dim frm As New NewItemForm(_activeDataSet)
        If Not FormOpen(frm.Text) Then ShowForm(frm)
    End Sub

    Private Sub StandardToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StandardToolStripMenuItem1.Click
        ShowDataGridForm(_activeDataSet, DisplayText.StandardMerges, Tables.Merges)
    End Sub

    Private Sub AttachmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.AttachmentMerges, Tables.AttachmentComboMerges)
    End Sub

    Private Sub ListingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListingToolStripMenuItem.Click
        ShowChildDataGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentList), Tables.Attachments.Name)
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AttachmentInfo), Tables.AttachmentInfo.Name)
    End Sub

    Private Sub IncompatibleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncompatibleToolStripMenuItem.Click
        ShowChildDataGridForm(_activeDataSet, FormatDataText(DisplayText.IncompatibleAttachments), Tables.IncompatibleAttachments.Name)
    End Sub

    Private Sub LaunchablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaunchablesToolStripMenuItem.Click
        ShowChildDataGridForm(_activeDataSet, FormatDataText(DisplayText.Launchables), Tables.Launchables.Name)
    End Sub

    Private Sub CompatibleFaceItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompatibleFaceItemsToolStripMenuItem.Click
        ShowChildDataGridForm(_activeDataSet, FormatDataText(DisplayText.CompatibleFaceItems), Tables.CompatibleFaceItems.Name)
    End Sub

    Private Sub TransformationsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TransformationsToolStripMenuItem.Click
        ShowChildDataGridForm(_activeDataSet, FormatDataText(DisplayText.ItemTransformations), Tables.ItemTransformations)
    End Sub

    Private Sub EnglishToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnglishToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.English & ")"), Tables.AmmoStrings)
    End Sub

    Private Sub GermanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GermanToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.German & ")"), Tables.GermanAmmoStrings)
    End Sub

    Private Sub RussianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RussianToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.Russian & ")"), Tables.RussianAmmoStrings)
    End Sub

    Private Sub PolishToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RussianToolStripMenuItem.Click, PolishToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.Polish & ")"), Tables.PolishAmmoStrings)
    End Sub

    Private Sub FrenchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RussianToolStripMenuItem.Click, PolishToolStripMenuItem.Click, FrenchToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.French & ")"), Tables.FrenchAmmoStrings)
    End Sub

    Private Sub ItalianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItalianToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.Italian & ")"), Tables.ItalianAmmoStrings)
    End Sub

    Private Sub DutchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DutchToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.Dutch & ")"), Tables.DutchAmmoStrings)
    End Sub

    Private Sub ChineseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChineseToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatDataText(DisplayText.AmmoCalibers & "(" & OtherText.Chinese & ")"), Tables.ChineseAmmoStrings)
    End Sub

    Private Function FormatInvText(inventoryTableName As String)
        Return DisplayText.Inventory & " - " & inventoryTableName
    End Function

    Private Sub AlbertoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlbertoToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Alberto), Tables.AlbertoInventory)
    End Sub

    Private Sub ArnieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArnieToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Arnie), Tables.ArnieInventory)
    End Sub

    Private Sub CarloToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarloToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Carlo), Tables.CarloInventory)
    End Sub

    Private Sub DevinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DevinToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Devin), Tables.DevinInventory)
    End Sub

    Private Sub ElginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ElginToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Elgin), Tables.ElginInventory)
    End Sub

    Private Sub FrankToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrankToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Frank), Tables.FrankInventory)
    End Sub

    Private Sub FranzToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FranzToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Franz), Tables.FranzInventory)
    End Sub

    Private Sub FredoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FredoToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Fredo), Tables.FredoInventory)
    End Sub

    Private Sub GabbyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GabbyToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Gabby), Tables.GabbyInventory)
    End Sub

    Private Sub HerveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HerveToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Herve), Tables.HerveInventory)
    End Sub

    Private Sub HowardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HowardToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Howard), Tables.HowardInventory)
    End Sub

    Private Sub JakeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JakeToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Jake), Tables.JakeInventory)
    End Sub

    Private Sub KeithToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeithToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Keith), Tables.KeithInventory)
    End Sub

    Private Sub MannyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MannyToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Manny), Tables.MannyInventory)
    End Sub

    Private Sub MickeyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MickeyToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Mickey), Tables.MickeyInventory)
    End Sub

    Private Sub PerkoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerkoToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Perko), Tables.PerkoInventory)
    End Sub

    Private Sub PeterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PeterToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Peter), Tables.PeterInventory)
    End Sub

    Private Sub SamToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SamToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Sam), Tables.SamInventory)
    End Sub

    Private Sub TonyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TonyToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, FormatInvText(DisplayText.Tony), Tables.TonyInventory)
    End Sub

    Private Sub IMPItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPItemsToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.ImpItems, Tables.IMPItems)
    End Sub

    Private Sub MercStartingGearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MercStartingGearToolStripMenuItem.Click
        ShowMercGearGridForm(_activeDataSet, DisplayText.MercStartingGear)
    End Sub

    Private Sub CustomFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomFilterToolStripMenuItem.Click
        Dim frm As New CustomFilterForm(_activeDataSet)
        frm.ShowDialog(Me)
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.CustomFilter), Nothing, Nothing, frm.Filter)
    End Sub
    Private Sub EnemyGunsDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnemyGunsDefaultToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyGuns, Tables.EnemyGuns)
    End Sub
    Private Sub EnemyGunsAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnemyGunsAdminToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyGuns, Tables.EnemyGunsAdmin)
    End Sub
    Private Sub EnemyGunsRegularToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnemyGunsRegularToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyGuns, Tables.EnemyGunsRegular)
    End Sub
    Private Sub EnemyGunsEliteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnemyGunsEliteToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyGuns, Tables.EnemyGunsElite)
    End Sub
    Private Sub EnemyItemsDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnemyItemsDefaultToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyItems, Tables.EnemyItems)
    End Sub
    Private Sub EnemyItemsAdminToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EnemyItemsAdminToolStripMenuItem1.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyItems, Tables.EnemyItemsAdmin)
    End Sub
    Private Sub EnemyItemsRegularToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EnemyItemsRegularToolStripMenuItem1.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyItems, Tables.EnemyItemsRegular)
    End Sub
    Private Sub EnemyItemsEliteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EnemyItemsEliteToolStripMenuItem1.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyItems, Tables.EnemyItemsElite)
    End Sub
    Private Sub EnemyAmmoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyAmmoToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.EnemyAmmo, Tables.EnemyAmmo)
    End Sub

    Private Sub EnemyWeaponDropRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyWeaponDropRateToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.WeaponDropRates, Tables.EnemyWeaponDrops)
    End Sub

    Private Sub EnemyAmmoDropRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyAmmoDropRateToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.AmmoDropRates, Tables.EnemyAmmoDrops)
    End Sub

    Private Sub EnemyArmourDropRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyArmourDropRateToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.ArmourDropRates, Tables.EnemyArmourDrops)
    End Sub

    Private Sub EnemyExplosiveDropRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyExplosiveDropRateToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.ExplosiveDropRates, Tables.EnemyExplosiveDrops)
    End Sub

    Private Sub EnemyMiscDropRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnemyMiscDropRateToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MiscItemDropRates, Tables.EnemyMiscDrops)
    End Sub
    Private Sub MilitiaGunsGreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaGunsGreenToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaGuns, Tables.MilitiaGunsGreen)
    End Sub

    Private Sub MilitiaGunsRegularToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaGunsRegularToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaGuns, Tables.MilitiaGunsRegular)
    End Sub

    Private Sub MilitiaGunsEliteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaGunsEliteToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaGuns, Tables.MilitiaGunsElite)
    End Sub

    Private Sub MilitiaItemsGreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaItemsGreenToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaItems, Tables.MilitiaItemsGreen)
    End Sub

    Private Sub MilitiaItemsRegularToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaItemsRegularToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaItems, Tables.MilitiaItemsRegular)
    End Sub

    Private Sub MilitiaItemsEliteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MilitiaItemsEliteToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.MilitiaItems, Tables.MilitiaItemsElite)
    End Sub
    Private Sub BackgroundImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackgroundImageToolStripMenuItem.Click
        If BackgroundImageToolStripMenuItem.Checked Then
            _client.BackgroundImage = My.Resources.DESKTOP
            _client.BackgroundImageLayout = ImageLayout.Stretch
        Else
            _client.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub DataToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Data1ToolStripMenuItem.Click, Data2ToolStripMenuItem.Click, Data3ToolStripMenuItem.Click, Data4ToolStripMenuItem.Click, Data5ToolStripMenuItem.Click, Data6ToolStripMenuItem.Click, Data7ToolStripMenuItem.Click, Data8ToolStripMenuItem.Click, Data9ToolStripMenuItem.Click, Data10ToolStripMenuItem.Click
        Dim menuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim index As Integer = CInt(menuItem.Tag)
        If index < GameDataCount Then
            _activeDataSet = GameData(index)
            Me.Text = String.Format(DisplayText.MainFormTitle, _activeDataSet.Name)

            For Each item As ToolStripMenuItem In Me.SelectDataToolStripMenuItem.DropDownItems
                If item IsNot sender Then
                    item.Checked = False
                End If
            Next
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        Save(False)
    End Sub

    Private Sub SaveAllToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles SaveAllToolStripMenuItem.Click, SaveAllToolStripButton.Click
        Save(True)
    End Sub

    Protected Sub Save(saveAll As Boolean)
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        LoadingForm.Show(True)
        Application.DoEvents()
        If saveAll Then
            For i As Integer = 0 To GameDataCount - 1
                LoadingForm.SetDataName(GameData(i).Name)
                GameData(i).SaveData()
            Next
        Else
            LoadingForm.SetDataName(_activeDataSet.Name)
            _activeDataSet.SaveData()
        End If

        'conditional to ensure that there's no accidental loading of working data when some of the data sets aren't up to date
        If saveAll OrElse GameDataCount = 1 Then
            With My.Settings
                .Last_Version_Major = My.Application.Info.Version.Major
                .Last_Version_Minor = My.Application.Info.Version.Minor
                .Save()
            End With
        End If

        Windows.Forms.Cursor.Current = Cursors.Arrow
        LoadingForm.Close()
    End Sub

    Private Sub DrugTypesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DrugTypesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.DrugTypes, Tables.DrugTypes)
    End Sub

    'Private Sub DrugsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DrugsToolStripMenuItem.Click
    '    ShowDataGridForm(_activeDataSet, DisplayText.Drugs, Tables.Drugs)
    'End Sub

    Private Sub DrugsItemFilterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DrugsItemFilterToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.Drugs), Tables.Items.Fields.Medical & "= 1", Nothing)
    End Sub

    Private Sub SeparabilityStatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SeparabilityStatesToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.Separability, Tables.SeparabilityStates)
    End Sub

    Private Sub FoodToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FoodToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.Food, Tables.Food)
    End Sub

    Private Sub ClothesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClothesToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.Clothes, Tables.Clothes)
    End Sub

    Private Sub RandToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RandToolStripMenuItem.Click
        ShowDataGridForm(_activeDataSet, DisplayText.RandomItems, Tables.RandomItems)
    End Sub

    Private Sub ItemFlagsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ItemFlagsToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.ItemFlags, Tables.ItemFlags)
    End Sub

    Private Sub AmmoFlagsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AmmoFlagsToolStripMenuItem.Click
        ShowLookupGridForm(_activeDataSet, DisplayText.AmmoFlags, Tables.AmmoFlags)
    End Sub

    Private Sub RandomItemsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RandomItemsToolStripMenuItem.Click
        ShowItemGridForm(_activeDataSet, FormatItemClassText(DisplayText.RandomItems), Tables.Items.Fields.ItemClass & "=" & ItemClass.RandomItem, Nothing)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutForm.Visible = True
    End Sub

End Class
