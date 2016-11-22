Public Class ItemDataForm
    Inherits BaseDataForm

    Public Sub New(manager As DataManager, ByVal itemID As Integer, ByVal formText As String)
        MyBase.New(manager, itemID, formText)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Initialize()
    End Sub

    Private Sub RemoveUnusedLanguageSpecificItemTabs()
        Dim baseItemFolder As String = "Items\"
        Dim baseItemName As String = "Items.xml"

        If LanguageSpecificFileExist(baseItemFolder & OtherText.German & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.GermanPage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.Russian & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.RussianPage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.Polish & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.PolishPage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.Italian & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.ItalianPage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.French & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.FrenchPage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.Chinese & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.ChinesePage)
        End If
        If LanguageSpecificFileExist(baseItemFolder & OtherText.Dutch & "." & baseItemName) = False Then
            Me.TabControl2.Controls.Remove(Me.DutchPage)
        End If

    End Sub

    Private Function LanguageSpecificFileExist(ByVal filename As String) As Boolean
        Dim exists As Boolean = True

        Dim filePath = _dm.GetLanguageSpecificTableDirectory(filename) & filename

        If System.IO.File.Exists(filePath) = False Then
            exists = False
        End If

        Return exists

    End Function

    Protected Sub Initialize()

        ItemSizeUpDown.Maximum = ItemSizeMax

        ' RoWa21: Only add the language specific Tabs for XML-files that exist
        RemoveUnusedLanguageSpecificItemTabs()

        Bind(Tables.Items.Name, Tables.Items.Fields.ID & "=" & _id)

        ClassNameLabel.DataBindings.Add("Text", usItemClassComboBox, "Text")

        PopulateGraphicsTypeComboBox()

        GraphicTypeComboBox.SelectedIndex = _view(0)(Tables.Items.Fields.GraphicType)
        GraphicIndexUpDown.Maximum = _dm.ItemImages.SmallItems(GraphicTypeComboBox.SelectedIndex).Length - 1
        GraphicIndexUpDown.Value = _view(0)(Tables.Items.Fields.GraphicIndex)

        DisplayTabs()

        'Setting Item Flags
        Dim TempItemFlags As UInt32 = _view(0)("ItemFlag")
        Dim BitItemFlags As String = ToBinary(TempItemFlags).PadRight(32, "0")

        Dim TempChecklistBox As CheckedListBox = ItemTab.TabPages("FlagsTab").Controls.Item("GroupBox64").Controls.Item("ItemFlagsCheckedList")
        For i As Integer = 0 To 31
            TempChecklistBox.SetItemChecked(i, (BitItemFlags(i).ToString.Equals("1")))
        Next

        ''Setting Drugs
        'Dim TempDrugFlags As UInt32 = _view(0)("DrugType")
        'Dim BitDrugFlags As String = ToBinary(TempDrugFlags).PadRight(32, "0")

        ''TODO: Dynamically create the checkboxes from the entries in table "DRUG"

        'Dim TempDrugChecklistBox As CheckedListBox = ItemTab.TabPages("FlagsTab").Controls.Item("GroupBox65").Controls.Item("DrugsCheckedList")
        'For i As Integer = 0 To 31
        '    TempDrugChecklistBox.SetItemChecked(i, (BitDrugFlags(i).ToString.Equals("1")))
        'Next

        
        SetupGrid(AttachmentGrid, Tables.Attachments.Name, Tables.Attachments.Fields.ItemID)
        SetupGrid(AttachmentInfoGrid, Tables.AttachmentInfo.Name, Tables.AttachmentInfo.Fields.ItemID)
        SetupGrid(AttachToGrid, Tables.Attachments.Name, Tables.Attachments.Fields.AttachmentID)
        SetupGrid(IncompatibleAttachmentGrid, Tables.IncompatibleAttachments.Name, Tables.IncompatibleAttachments.Fields.ItemID)
        SetupGrid(LaunchableGrid, Tables.Launchables.Name, Tables.Launchables.Fields.ItemID)
        SetupGrid(LauncherGrid, Tables.Launchables.Name, Tables.Launchables.Fields.LaunchableID)
        SetupGrid(CompatibleFaceItemGrid, Tables.CompatibleFaceItems.Name, Tables.CompatibleFaceItems.Fields.ItemID)

        LoadInventoryData()
    End Sub

    Protected Sub PopulateGraphicsTypeComboBox()
        Me.GraphicTypeComboBox.Items.Clear()
        For i As Integer = 0 To _dm.ImageTypeCount
            If i = 0 Then
                Me.GraphicTypeComboBox.Items.Add("Guns")
            Else
                Me.GraphicTypeComboBox.Items.Add(String.Format("P{0}Items", i))
            End If
        Next
    End Sub

#Region " General Tab "
#End Region

#Region " Graphics Tab "
    Protected Sub LoadImages(ByVal type As Integer, ByVal index As Integer)
        SmallItemImage.Image = _dm.ItemImages.SmallItemImage(type, index)
        MediumItemImage.Image = _dm.ItemImages.MediumItemImage(type, index)
        BigItemImage.Image = _dm.ItemImages.BigItemImage(type, index)
    End Sub

    Private Sub GraphicIndexUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GraphicIndexUpDown.ValueChanged
        LoadImages(GraphicTypeComboBox.SelectedIndex, GraphicIndexUpDown.Value)
    End Sub

    Private Sub GraphicTypeCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GraphicTypeComboBox.SelectedIndexChanged
        GraphicIndexUpDown.Maximum = _dm.ItemImages.SmallItems(GraphicTypeComboBox.SelectedIndex).Length - 1
        LoadImages(GraphicTypeComboBox.SelectedIndex, GraphicIndexUpDown.Value)
        ImageListBox.DataSource = _dm.ItemImages.BigItems(GraphicTypeComboBox.SelectedIndex)
    End Sub

    Private Sub ImageListBox_MeasureItem(ByVal sender As Object, ByVal e As System.Windows.Forms.MeasureItemEventArgs) Handles ImageListBox.MeasureItem
        If e.Index < 0 Then Return

        Dim img As Image = CType(ImageListBox.Items(e.Index), Image)
        Dim hgt As Single = Math.Max(img.Height, e.Graphics.MeasureString("1", ImageListBox.Font).Height) * 1.1
        e.ItemHeight = hgt
        e.ItemWidth = ImageListBox.Width
    End Sub

    Private Sub ImageListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageListBox.SelectedIndexChanged
        GraphicIndexUpDown.Value = ImageListBox.SelectedIndex
    End Sub

    Private Sub ImageListBox_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ImageListBox.DrawItem
        Dim g As Graphics = e.Graphics
        If e.State = DrawItemState.Selected Then
            'fill the background of the item 
            g.FillRectangle(Brushes.Blue, e.Bounds)
            'draw the image from the image list control, offset it by 5 pixels and makes sure it's centered vertically 
            Dim myImage As Bitmap = CType(ImageListBox.Items(e.Index), Bitmap)
            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) \ 2)
        Else
            'this block does the same thing as above but uses different colors to represent the different state. 
            Dim myImage As Bitmap = CType(ImageListBox.Items(e.Index), Bitmap)
            g.FillRectangle(Brushes.White, e.Bounds)
            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) \ 2)
        End If
    End Sub
#End Region

#Region " Descriptions Tab "
    Private Sub DescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescriptionTextBox.TextChanged
        DescriptionCharsLeftLabel.Text = DescriptionTextBox.MaxLength - DescriptionTextBox.TextLength
    End Sub

    Private Sub BRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BRDescriptionTextBox.TextChanged
        BRDescriptionCharsLeftLabel.Text = BRDescriptionTextBox.MaxLength - BRDescriptionTextBox.TextLength
    End Sub

    Private Sub GermanDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GermanDescriptionTextBox.TextChanged
        GermanDescriptionCharsLeftLabel.Text = GermanDescriptionTextBox.MaxLength - GermanDescriptionTextBox.TextLength
    End Sub

    Private Sub GermanBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GermanBRDescriptionTextBox.TextChanged
        GermanBRDescriptionCharsLeftLabel.Text = GermanBRDescriptionTextBox.MaxLength - GermanBRDescriptionTextBox.TextLength
    End Sub

    Private Sub RussianDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RussianDescriptionTextBox.TextChanged
        RussianDescriptionCharsLeftLabel.Text = RussianDescriptionTextBox.MaxLength - RussianDescriptionTextBox.TextLength
    End Sub

    Private Sub RussianBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RussianBRDescriptionTextBox.TextChanged
        RussianBRDescriptionCharsLeftLabel.Text = RussianBRDescriptionTextBox.MaxLength - RussianBRDescriptionTextBox.TextLength
    End Sub

    Private Sub PolishDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PolishDescriptionTextBox.TextChanged
        PolishDescriptionCharsLeftLabel.Text = PolishDescriptionTextBox.MaxLength - PolishDescriptionTextBox.TextLength
    End Sub

    Private Sub PolishBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PolishBRDescriptionTextBox.TextChanged
        PolishBRDescriptionCharsLeftLabel.Text = PolishBRDescriptionTextBox.MaxLength - PolishBRDescriptionTextBox.TextLength
    End Sub

    Private Sub FrenchDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrenchDescriptionTextBox.TextChanged
        FrenchDescriptionCharsLeftLabel.Text = FrenchDescriptionTextBox.MaxLength - FrenchDescriptionTextBox.TextLength
    End Sub

    Private Sub FrenchBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrenchBRDescriptionTextBox.TextChanged
        FrenchBRDescriptionCharsLeftLabel.Text = FrenchBRDescriptionTextBox.MaxLength - FrenchBRDescriptionTextBox.TextLength
    End Sub

    Private Sub ItalianDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItalianDescriptionTextBox.TextChanged
        ItalianDescriptionCharsLeftLabel.Text = ItalianDescriptionTextBox.MaxLength - ItalianDescriptionTextBox.TextLength
    End Sub

    Private Sub ItalianBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItalianBRDescriptionTextBox.TextChanged
        ItalianBRDescriptionCharsLeftLabel.Text = ItalianBRDescriptionTextBox.MaxLength - ItalianBRDescriptionTextBox.TextLength
    End Sub

    Private Sub DutchDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DutchDescriptionTextBox.TextChanged
        DutchDescriptionCharsLeftLabel.Text = DutchDescriptionTextBox.MaxLength - DutchDescriptionTextBox.TextLength
    End Sub

    Private Sub DutchBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DutchBRDescriptionTextBox.TextChanged
        DutchBRDescriptionCharsLeftLabel.Text = DutchBRDescriptionTextBox.MaxLength - DutchBRDescriptionTextBox.TextLength
    End Sub

    Private Sub ChineseDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChineseDescriptionTextBox.TextChanged
        ChineseDescriptionCharsLeftLabel.Text = ChineseDescriptionTextBox.MaxLength - ChineseDescriptionTextBox.TextLength
    End Sub

    Private Sub ChineseBRDescriptionTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChineseBRDescriptionTextBox.TextChanged
        ChineseBRDescriptionCharsLeftLabel.Text = ChineseBRDescriptionTextBox.MaxLength - ChineseBRDescriptionTextBox.TextLength
    End Sub
#End Region

#Region " Weapons Tab "
    Private Sub ubShotsPer4TurnsUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ubShotsPer4TurnsUpDown.ValueChanged
        If ubShotsPer4TurnsUpDown.Value > 0 Then
            ' RoWa21: Generic formular for any AP system
            Dim maxAPs As Integer = IniFile.APMaximum()

            ' If not set in the "XMLEditorInit.xml", then set it here to default 100 APs
            If maxAPs <= 0 Then
                maxAPs = 100
            End If

            Dim multiplicator As Integer = Math.Round(maxAPs / 25, 0)
            Dim val As Integer = Math.Round((2 * multiplicator * 80 * 100) / ((100 + 80) * ubShotsPer4TurnsUpDown.Value), 0)

            ' RoWa21: This is the simplified old formular for the 25 AP system
            'Dim val As Integer = Math.Round(89 / ubShotsPer4TurnsUpDown.Value, 0)
            Me.APsShots4TurnsLabel.Text = "= " & val & " APs"
        Else
            Me.APsShots4TurnsLabel.Text = ""
        End If
    End Sub

    Private Sub ubReadyTimeUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ubReadyTimeUpDown.ValueChanged
        ' RoWa21: Generic formular for any AP system
        Dim maxAPs As Integer = IniFile.APMaximum()

        ' If not set in the "XMLEditorInit.xml", then set it here to default 25 APs
        If maxAPs <= 0 Then
            maxAPs = 25
        End If

        Dim val As Integer = Math.Round((ubReadyTimeUpDown.Value * maxAPs) / 100, 0)

        ' RoWa21: Simplified old formular for the 25 AP system
        Me.APsReadyLabel.Text = "= " & val & " APs"
    End Sub
#End Region

#Region " Data Binding "
    Protected Overrides Function CommitData() As Boolean
        Try
            Dim newID As Integer = _view(0)(Tables.Items.Fields.ID)
            Dim oldID As Integer = _view(0).Row(Tables.Items.Fields.ID, DataRowVersion.Current)
            Dim otherItem As DataRow = _view.Table.Rows.Find(newID)

            _view(0)(Tables.Items.Fields.ItemImage) = BigItemImage.Image
            _view(0)(Tables.Items.Fields.GraphicIndex) = GraphicIndexUpDown.Value
            _view(0)(Tables.Items.Fields.GraphicType) = GraphicTypeComboBox.SelectedIndex

            If otherItem IsNot Nothing AndAlso otherItem IsNot _view(0).Row Then
                If MessageBox.Show("The Item ID you have entered is already being used by """ & otherItem(Tables.Items.Fields.Name) & """.  Do you want to swap IDs?", "Swap IDs", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    'swap ids
                    otherItem(Tables.Items.Fields.ID) = -1
                    _view(0).EndEdit()
                    otherItem(Tables.Items.Fields.ID) = oldID
                    _id = newID
                    _view.RowFilter = Tables.Items.Fields.ID & "=" & _id
                    Me.Text = String.Format(DisplayText.ItemDataFormText, _dm.Name, _id, _view(0)(Tables.Items.Fields.Name))
                    SaveInventoryData()
                Else
                    ErrorHandler.ShowWarning("Please enter a different ID value.", MessageBoxIcon.Exclamation)
                    Return False
                End If
            Else
                _view(0).EndEdit()
                _id = newID
                _view.RowFilter = Tables.Items.Fields.ID & "=" & _id
                Me.Text = String.Format(DisplayText.ItemDataFormText, _dm.Name, _id, _view(0)(Tables.Items.Fields.Name))
                SaveInventoryData()
            End If

            'Saving Itemflags
            Dim TempItemFlags As UInt32 = 0
            Dim TempChecklistBox As CheckedListBox = ItemTab.TabPages("FlagsTab").Controls.Item("GroupBox64").Controls.Item("ItemFlagsCheckedList")
            For i As Integer = 0 To 31
                If TempChecklistBox.GetItemChecked(i) Then
                    TempItemFlags += 2 ^ i
                End If
            Next

            _view(0)("ItemFlag") = TempItemFlags

            ''Saving Drugs
            'Dim TempDrugFlags As UInt32 = 0
            'Dim TempDrugChecklistBox As CheckedListBox = ItemTab.TabPages("FlagsTab").Controls.Item("GroupBox65").Controls.Item("DrugsCheckedList")
            'For i As Integer = 0 To 31
            '    If TempDrugChecklistBox.GetItemChecked(i) Then
            '        TempDrugFlags += 2 ^ i
            '    End If
            'Next

            '_view(0)("DrugType") = TempDrugFlags

            _view(0).Row.AcceptChanges()
            AcceptGridChanges(AttachmentGrid)
            AcceptGridChanges(AttachToGrid)
            AcceptGridChanges(IncompatibleAttachmentGrid)
            AcceptGridChanges(LaunchableGrid)
            AcceptGridChanges(LauncherGrid)
            AcceptGridChanges(AttachmentInfoGrid)
            AcceptGridChanges(CompatibleFaceItemGrid)
            Return True
        Catch ex As ConstraintException
            ErrorHandler.ShowError("One or more values must be unique. Please enter a different value(s).", MessageBoxIcon.Exclamation, ex)
        Catch ex As Exception
            ErrorHandler.ShowError(ex)
        End Try

    End Function

    Protected Overrides Sub DoCancelData()
        CancelGridChanges(AttachmentGrid)
        CancelGridChanges(AttachToGrid)
        CancelGridChanges(IncompatibleAttachmentGrid)
        CancelGridChanges(LaunchableGrid)
        CancelGridChanges(LauncherGrid)
        CancelGridChanges(AttachmentInfoGrid)
        CancelGridChanges(CompatibleFaceItemGrid)
    End Sub
#End Region

#Region " Buttons "

    Protected Overrides Sub ApplyButtonClicked()
        GraphicIndexUpDown.Value = _view(0)(Tables.Items.Fields.GraphicIndex)
    End Sub

    Private Sub ChangeClassButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeClassButton.Click
        Dim frm As New ChangeClassForm(_dm, _view, Me)
        frm.ShowDialog()
    End Sub

#End Region

#Region " Grids "
    Protected Sub SetupGrid(ByVal grid As DataGridView, ByVal tableName As String, Optional ByVal itemIndexField As String = Nothing)
        Dim t As DataTable = _dm.Database.Table(tableName)
        Dim rowFilter As String = Nothing
        If itemIndexField IsNot Nothing Then
            rowFilter = itemIndexField & "=" & _id
        End If

        InitializeGrid(_dm.Database, grid, New DataView(t, rowFilter, "", DataViewRowState.CurrentRows), , True)

        grid.Tag = itemIndexField
        grid.Columns(itemIndexField).Visible = False

        AddHandler grid.DefaultValuesNeeded, AddressOf Grid_DefaultValuesNeeded
    End Sub

    Protected Sub Grid_DefaultValuesNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs)
        Dim grid As DataGridView = DirectCast(sender, DataGridView)
        Dim itemIndexField As String = grid.Tag
        e.Row.Cells(itemIndexField).Value = _id

        'set primary key for single key grids, like the attachmentinfo one
        Dim v As DataView = DirectCast(grid.DataSource, DataView)
        If v.Table.PrimaryKey.Length = 1 Then
            Dim key As String = v.Table.PrimaryKey(0).ColumnName
            Dim val As Decimal = _dm.Database.GetNextPrimaryKeyValue(v.Table)
            e.Row.Cells(key).Value = val
        End If
    End Sub

    Protected Sub CancelGridChanges(ByVal grid As DataGridView)
        Dim v As DataView = DirectCast(grid.DataSource, DataView)
        v.RowStateFilter = DataViewRowState.Added Or DataViewRowState.ModifiedCurrent Or DataViewRowState.Deleted
        For i As Integer = v.Count - 1 To 0 Step -1
            If v(i).Row.RowState <> DataRowState.Detached Then v(i).Row.RejectChanges()
        Next
        v.RowStateFilter = DataViewRowState.CurrentRows
    End Sub

    Protected Sub AcceptGridChanges(ByVal grid As DataGridView)
        Dim v As DataView = DirectCast(grid.DataSource, DataView)
        v.RowStateFilter = DataViewRowState.Added Or DataViewRowState.ModifiedCurrent Or DataViewRowState.Deleted
        For i As Integer = v.Count - 1 To 0 Step -1
            v(i).Row.AcceptChanges()
        Next
        v.RowStateFilter = DataViewRowState.CurrentRows
    End Sub

    Private Sub AttachToGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles AttachToGrid.RowHeaderMouseDoubleClick
        GridOpenItem(AttachToGrid, e.RowIndex, Tables.Attachments.Fields.ItemID)
    End Sub

    Private Sub AttachmentGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles AttachmentGrid.RowHeaderMouseDoubleClick
        GridOpenItem(AttachmentGrid, e.RowIndex, Tables.Attachments.Fields.AttachmentID)
    End Sub

    Private Sub IncompatibleAttachmentGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles IncompatibleAttachmentGrid.RowHeaderMouseDoubleClick
        GridOpenItem(IncompatibleAttachmentGrid, e.RowIndex, Tables.IncompatibleAttachments.Fields.IncompatibleItemID)
    End Sub

    Private Sub LaunchableGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles LaunchableGrid.RowHeaderMouseDoubleClick
        GridOpenItem(LaunchableGrid, e.RowIndex, Tables.Launchables.Fields.LaunchableID)
    End Sub

    Private Sub LauncherGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles LauncherGrid.RowHeaderMouseDoubleClick
        GridOpenItem(LauncherGrid, e.RowIndex, Tables.Launchables.Fields.ItemID)
    End Sub

    Private Sub CompatibleFaceItemGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CompatibleFaceItemGrid.RowHeaderMouseDoubleClick
        GridOpenItem(CompatibleFaceItemGrid, e.RowIndex, Tables.CompatibleFaceItems.Fields.CompatibleItemID)
    End Sub

    Private Sub GridOpenItem(ByVal grid As DataGridView, ByVal rowIndex As Integer, ByVal itemIDFieldName As String)
        Dim name As String = Nothing
        Dim id As Integer = grid.Rows(rowIndex).Cells(itemIDFieldName).Value
        Dim r As DataRow = _dm.Database.Table(Tables.Items.Name).Rows.Find(id)
        If r IsNot Nothing Then
            name = r(Tables.Items.Fields.Name)
            Open(_dm, id, name)
        End If
    End Sub
#End Region

#Region " Tabs "
    Protected Sub DisplayTabs()
        With ItemTab.TabPages
            Select Case _view(0)(Tables.Items.Fields.ItemClass)
                Case ItemClass.Ammo
                    .Remove(WeaponPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(AttachmentPage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Armour
                    .Remove(WeaponPage)
                    .Remove(AmmoPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Bomb, ItemClass.Grenade
                    .Remove(WeaponPage)
                    .Remove(ArmourPage)
                    .Remove(AmmoPage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Gun
                    WeaponTab.TabPages.Remove(LauncherPage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    '.Remove(AttachmentDataPage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                Case ItemClass.Launcher
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                Case ItemClass.Knife, ItemClass.ThrowingKnife, ItemClass.Thrown, ItemClass.Tentacle, ItemClass.Punch
                    WeaponTab.TabPages.Remove(LauncherPage)
                    WeaponTab.TabPages.Remove(GunPage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Face
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(WeaponPage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Kit, ItemClass.MedKit, ItemClass.Misc
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(WeaponPage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.Key
                    .Remove(AttachmentPage)
                    .Remove(AttachmentDataPage)
                    .Remove(NCTHPage)
                    .Remove(BonusesPage)
                    .Remove(AbilitiesPage)
                    .Remove(WeaponPage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.LBE
                    '.Remove(AttachmentPage) 'Commenting this out for MOLLE to work properly.
                    .Remove(AttachmentDataPage)
                    .Remove(NCTHPage)
                    .Remove(BonusesPage)
                    .Remove(WeaponPage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
                Case ItemClass.RandomItem
                    .Remove(NCTHPage)
                    .Remove(BonusesPage)
                    .Remove(AttachmentPage)
                    .Remove(AttachmentDataPage)
                    .Remove(ExplosivePage)
                    .Remove(InventoryPage)
                    .Remove(OverheatingTabPage)
                    .Remove(FacePage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(WeaponPage)
                    .Remove(LBEPage)
                Case Else 'ItemClass.None, ItemClass.Money
                    .Remove(AttachmentPage)
                    .Remove(AttachmentDataPage)
                    .Remove(NCTHPage)
                    .Remove(BonusesPage)
                    .Remove(AbilitiesPage)
                    .Remove(InventoryPage)
                    .Remove(WeaponPage)
                    .Remove(AmmoPage)
                    .Remove(ArmourPage)
                    .Remove(ExplosivePage)
                    .Remove(FacePage)
                    .Remove(LBEPage)
                    OverheatingTabPage.Controls.Remove(WeaponTemperatureGroupBox)
            End Select
        End With
    End Sub
#End Region

#Region " Shared methods "
    Public Shared Sub Open(manager As DataManager, ByVal id As Integer, ByVal name As String)
        Dim formText As String = String.Format(DisplayText.ItemDataFormText, manager.Name, id, name)
        If Not MainWindow.FormOpen(formText) Then
            Dim frm As New ItemDataForm(manager, id, formText)
            MainWindow.ShowForm(frm)
        End If
    End Sub
#End Region

#Region " Shopkeeper Inventories "

    Private Sub InventoryCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cb As CheckBox = DirectCast(sender, CheckBox)
        ShopkeepersPanel.Controls(cb.Text & "UpDown").Enabled = cb.Checked
    End Sub

    Protected Sub LoadInventoryData()
        LoadShopkeeperData(ShopKeepers.Alberto)
        LoadShopkeeperData(ShopKeepers.Arnie)
        LoadShopkeeperData(ShopKeepers.Carlo)
        LoadShopkeeperData(ShopKeepers.Devin)
        LoadShopkeeperData(ShopKeepers.Elgin)
        LoadShopkeeperData(ShopKeepers.Frank)
        LoadShopkeeperData(ShopKeepers.Franz)
        LoadShopkeeperData(ShopKeepers.Fredo)
        LoadShopkeeperData(ShopKeepers.Gabby)
        LoadShopkeeperData(ShopKeepers.Herve)
        LoadShopkeeperData(ShopKeepers.Howard)
        LoadShopkeeperData(ShopKeepers.Jake)
        LoadShopkeeperData(ShopKeepers.Keith)
        LoadShopkeeperData(ShopKeepers.Manny)
        LoadShopkeeperData(ShopKeepers.Mickey)
        LoadShopkeeperData(ShopKeepers.Perko)
        LoadShopkeeperData(ShopKeepers.Peter)
        LoadShopkeeperData(ShopKeepers.Sam)
        LoadShopkeeperData(ShopKeepers.Tony)
    End Sub

    Protected Sub SaveInventoryData()
        SaveShopkeeperData(ShopKeepers.Alberto)
        SaveShopkeeperData(ShopKeepers.Arnie)
        SaveShopkeeperData(ShopKeepers.Carlo)
        SaveShopkeeperData(ShopKeepers.Devin)
        SaveShopkeeperData(ShopKeepers.Elgin)
        SaveShopkeeperData(ShopKeepers.Frank)
        SaveShopkeeperData(ShopKeepers.Franz)
        SaveShopkeeperData(ShopKeepers.Fredo)
        SaveShopkeeperData(ShopKeepers.Gabby)
        SaveShopkeeperData(ShopKeepers.Herve)
        SaveShopkeeperData(ShopKeepers.Howard)
        SaveShopkeeperData(ShopKeepers.Jake)
        SaveShopkeeperData(ShopKeepers.Keith)
        SaveShopkeeperData(ShopKeepers.Manny)
        SaveShopkeeperData(ShopKeepers.Mickey)
        SaveShopkeeperData(ShopKeepers.Perko)
        SaveShopkeeperData(ShopKeepers.Peter)
        SaveShopkeeperData(ShopKeepers.Sam)
        SaveShopkeeperData(ShopKeepers.Tony)
    End Sub

    Protected Sub LoadShopkeeperData(ByVal shopKeeperName As String)
        Dim rows() As DataRow = _dm.Database.Table(shopKeeperName & Tables.Inventory).Select(Tables.InventoryTableFields.ItemID & "=" & _id)
        Dim cb As CheckBox = DirectCast(ShopkeepersPanel.Controls(shopKeeperName & "CheckBox"), CheckBox)
        Dim ud As NumericUpDown = DirectCast(ShopkeepersPanel.Controls(shopKeeperName & "UpDown"), NumericUpDown)

        If rows.Length > 0 Then
            cb.Checked = True
            ud.Value = rows(0)(Tables.InventoryTableFields.Quantity)
            ud.Enabled = True
        Else
            cb.Checked = False
            ud.Value = _dm.Database.Table(shopKeeperName & Tables.Inventory).Columns(Tables.InventoryTableFields.Quantity).DefaultValue
            ud.Enabled = False
        End If

        AddHandler cb.CheckedChanged, AddressOf InventoryCheckBox_CheckedChanged
    End Sub

    Protected Sub SaveShopkeeperData(ByVal shopKeeperName As String)
        Dim rows() As DataRow = _dm.Database.Table(shopKeeperName & Tables.Inventory).Select(Tables.InventoryTableFields.ItemID & "=" & _id)
        Dim cb As CheckBox = DirectCast(ShopkeepersPanel.Controls(shopKeeperName & "CheckBox"), CheckBox)
        Dim ud As NumericUpDown = DirectCast(ShopkeepersPanel.Controls(shopKeeperName & "UpDown"), NumericUpDown)

        If cb.Checked Then
            If rows.Length > 0 Then 'modify
                rows(0)(Tables.InventoryTableFields.Quantity) = ud.Value
            Else 'new
                Dim row As DataRow = _dm.Database.NewRow(shopKeeperName & Tables.Inventory)
                'key value is set automatically
                row(Tables.InventoryTableFields.ItemID) = _id
                row(Tables.InventoryTableFields.Quantity) = ud.Value
            End If
        Else 'delete
            If rows.Length > 0 Then rows(0).Delete()
        End If
    End Sub
#End Region

#Region " Enable / disable LBE Pockets "

    Private Sub DisableAllPockets()
        ' Disable pockets
        lbePocketIndex1ComboBox.Enabled = False
        lbePocketIndex2ComboBox.Enabled = False
        lbePocketIndex3ComboBox.Enabled = False
        lbePocketIndex4ComboBox.Enabled = False
        lbePocketIndex5ComboBox.Enabled = False
        lbePocketIndex6ComboBox.Enabled = False
        lbePocketIndex7ComboBox.Enabled = False
        lbePocketIndex8ComboBox.Enabled = False
        lbePocketIndex9ComboBox.Enabled = False
        lbePocketIndex10ComboBox.Enabled = False
        lbePocketIndex11ComboBox.Enabled = False
        lbePocketIndex12ComboBox.Enabled = False

        ' Set the values of the disabled pockets to "Nothing"
        lbePocketIndex1ComboBox.SelectedValue = 0
        lbePocketIndex2ComboBox.SelectedValue = 0
        lbePocketIndex3ComboBox.SelectedValue = 0
        lbePocketIndex4ComboBox.SelectedValue = 0
        lbePocketIndex5ComboBox.SelectedValue = 0
        lbePocketIndex6ComboBox.SelectedValue = 0
        lbePocketIndex7ComboBox.SelectedValue = 0
        lbePocketIndex8ComboBox.SelectedValue = 0
        lbePocketIndex9ComboBox.SelectedValue = 0
        lbePocketIndex10ComboBox.SelectedValue = 0
        lbePocketIndex11ComboBox.SelectedValue = 0
        lbePocketIndex12ComboBox.SelectedValue = 0
    End Sub

    Private Sub EnableThighPackPockets()
        ' Enable pockets
        lbePocketIndex1ComboBox.Enabled = True
        lbePocketIndex2ComboBox.Enabled = True
        lbePocketIndex3ComboBox.Enabled = True
        lbePocketIndex4ComboBox.Enabled = True
        lbePocketIndex5ComboBox.Enabled = True

        ' Disable pockets
        lbePocketIndex6ComboBox.Enabled = False
        lbePocketIndex7ComboBox.Enabled = False
        lbePocketIndex8ComboBox.Enabled = False
        lbePocketIndex9ComboBox.Enabled = False
        lbePocketIndex10ComboBox.Enabled = False
        lbePocketIndex11ComboBox.Enabled = False
        lbePocketIndex12ComboBox.Enabled = False

        ' Set the values of the disabled pockets to "Nothing"
        lbePocketIndex6ComboBox.SelectedValue = 0
        lbePocketIndex7ComboBox.SelectedValue = 0
        lbePocketIndex8ComboBox.SelectedValue = 0
        lbePocketIndex9ComboBox.SelectedValue = 0
        lbePocketIndex10ComboBox.SelectedValue = 0
        lbePocketIndex11ComboBox.SelectedValue = 0
        lbePocketIndex12ComboBox.SelectedValue = 0
    End Sub

    Private Sub EnableVestPackPockets()
        ' Enable pockets
        lbePocketIndex1ComboBox.Enabled = True
        lbePocketIndex2ComboBox.Enabled = True
        lbePocketIndex3ComboBox.Enabled = True
        lbePocketIndex4ComboBox.Enabled = True
        lbePocketIndex5ComboBox.Enabled = True
        lbePocketIndex6ComboBox.Enabled = True
        lbePocketIndex7ComboBox.Enabled = True
        lbePocketIndex8ComboBox.Enabled = True
        lbePocketIndex9ComboBox.Enabled = True
        lbePocketIndex10ComboBox.Enabled = True
        lbePocketIndex11ComboBox.Enabled = True
        lbePocketIndex12ComboBox.Enabled = True
    End Sub

    Private Sub EnableBackPackPockets()
        ' Enable pockets
        lbePocketIndex1ComboBox.Enabled = True
        lbePocketIndex2ComboBox.Enabled = True
        lbePocketIndex3ComboBox.Enabled = True
        lbePocketIndex4ComboBox.Enabled = True
        lbePocketIndex5ComboBox.Enabled = True
        lbePocketIndex6ComboBox.Enabled = True
        lbePocketIndex7ComboBox.Enabled = True
        lbePocketIndex8ComboBox.Enabled = True
        lbePocketIndex9ComboBox.Enabled = True
        lbePocketIndex10ComboBox.Enabled = True
        lbePocketIndex11ComboBox.Enabled = True
        lbePocketIndex12ComboBox.Enabled = True
    End Sub

    Private Sub EnableCombatPackPockets()
        ' Enable pockets
        lbePocketIndex1ComboBox.Enabled = True
        lbePocketIndex2ComboBox.Enabled = True
        lbePocketIndex3ComboBox.Enabled = True
        lbePocketIndex4ComboBox.Enabled = True
        lbePocketIndex5ComboBox.Enabled = True
        lbePocketIndex6ComboBox.Enabled = True
        lbePocketIndex7ComboBox.Enabled = True

        ' Disable pockets
        lbePocketIndex8ComboBox.Enabled = False
        lbePocketIndex9ComboBox.Enabled = False
        lbePocketIndex10ComboBox.Enabled = False
        lbePocketIndex11ComboBox.Enabled = False
        lbePocketIndex12ComboBox.Enabled = False

        ' Set the values of the disabled pockets to "Nothing"
        lbePocketIndex8ComboBox.SelectedValue = 0
        lbePocketIndex9ComboBox.SelectedValue = 0
        lbePocketIndex10ComboBox.SelectedValue = 0
        lbePocketIndex11ComboBox.SelectedValue = 0
        lbePocketIndex12ComboBox.SelectedValue = 0
    End Sub

    Private Sub lbeClassComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbeClassComboBox.SelectedIndexChanged
        Select Case lbeClassComboBox.SelectedValue
            ' Nothing
            Case 0
                DisableAllPockets()
            Case 1
                EnableThighPackPockets()
            Case 2
                EnableVestPackPockets()
            Case 3
                EnableCombatPackPockets()
            Case 4
                EnableBackPackPockets()
        End Select
    End Sub
#End Region

#Region " Status Bar "
    Private Sub uiIndexUpDown_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles uiIndexUpDown.ValueChanged
        UpdateStatusBar()
    End Sub
#End Region


    Private Sub FoodTableLayout_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class

