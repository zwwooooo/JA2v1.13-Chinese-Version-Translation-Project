Imports System.Xml
Imports System.IO

Public Class ItemTable
    Inherits DefaultTable

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    Protected Sub AfterLoad(sender As DataManager) Handles _dm.AfterLoadData
        If sender.HideNothingItems Then
            RemoveNothingItems()
        End If
        ExtendPrimaryItemTable()
        AddImageColumn()
        PopulateImageColumn()
    End Sub

    Protected Sub BeforeSave(sender As DataManager) Handles _dm.BeforeSaveData
        RebuildExtendedItemTables()
    End Sub

    Protected Sub AfterSave(sender As DataManager) Handles _dm.AfterSaveData
        ClearExtendedItemTables()
    End Sub

    Protected Sub BeforeWorkingData(sender As DataManager) Handles _dm.BeforeSaveWorkingData
        RemoveImageColumn(_table)
    End Sub

    Protected Sub AfterWorkingData(sender As DataManager) Handles _dm.AfterLoadWorkingData, _dm.AfterSaveWorkingData
        AddImageColumn()
        PopulateImageColumn()
    End Sub

    Protected Sub AddImageColumn()
        If Not _table.Columns.Contains(Tables.Items.Fields.ItemImage) Then
            Dim c As New DataColumn
            c.ColumnName = Tables.Items.Fields.ItemImage
            c.DataType = GetType(System.Drawing.Image)
            c.Caption = "Image"
            _table.Columns.Add(c)
            c.SetOrdinal(_table.Columns(Tables.Items.Fields.Name).Ordinal + 1)
        End If

    End Sub

    Public Sub PopulateImageColumn()
        Dim warningMessage As New Text.StringBuilder()
        For Each r As DataRow In _table.Rows
            Application.DoEvents()
            Dim imageType As Integer = r(Tables.Items.Fields.GraphicType)
            Dim imageIndex As Integer = r(Tables.Items.Fields.GraphicIndex)
            If _dm.ItemImages.Exists(imageType, imageIndex) Then
                r(Tables.Items.Fields.ItemImage) = _dm.ItemImages.BigItemImage(imageType, imageIndex)
            Else
                warningMessage.AppendLine(r(Tables.Items.Fields.ID) & " - " & r(Tables.Items.Fields.Name))
                r(Tables.Items.Fields.GraphicType) = 0
                r(Tables.Items.Fields.GraphicIndex) = 0
            End If
        Next

        If warningMessage.Length > 0 Then
            warningMessage.Insert(0, "The following items had missing images and have had their graphics reset to the 'Nada' image:" & vbCrLf)
            ErrorHandler.ShowWarning(warningMessage.ToString, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Protected Sub RemoveImageColumn(ByVal table As DataTable)
        If table.Columns.Contains(Tables.Items.Fields.ItemImage) Then
            table.Columns.Remove(Tables.Items.Fields.ItemImage)
        End If
    End Sub

    Public Sub ExtendPrimaryItemTable()
        'store the values from the 1-to-1 related tables in the items table
        'this includes: magazines, weapons, armour, explosives, LBE
        With _dm.Database.DataSet

            Dim mags As DataTable = .Tables(Tables.Magazines.Name)
            Dim weapons As DataTable = .Tables(Tables.Weapons.Name)
            Dim armours As DataTable = .Tables(Tables.Armours.Name)
            Dim loadBearingEquipment As DataTable = .Tables(Tables.LoadBearingEquipment.Name)
            Dim explosives As DataTable = .Tables(Tables.Explosives.Name)
            Dim germanItems As DataTable = .Tables(Tables.GermanItems)
            Dim russianItems As DataTable = .Tables(Tables.RussianItems)
            Dim polishItems As DataTable = .Tables(Tables.PolishItems)
            Dim frenchItems As DataTable = .Tables(Tables.FrenchItems)
            Dim italianItems As DataTable = .Tables(Tables.ItalianItems)
            Dim dutchItems As DataTable = .Tables(Tables.DutchItems)
            Dim chineseItems As DataTable = .Tables(Tables.ChineseItems)

            CopyFromExtendedTable(weapons, Tables.Items.Fields.ItemClass & ">" & ItemClass.None & " AND " & Tables.Items.Fields.ItemClass & "<=" & ItemClass.Punch)
            CopyFromExtendedTable(mags, Tables.Items.Fields.ItemClass & "=" & ItemClass.Ammo)
            CopyFromExtendedTable(armours, Tables.Items.Fields.ItemClass & "=" & ItemClass.Armour)
            CopyFromExtendedTable(explosives, Tables.Items.Fields.ItemClass & "=" & ItemClass.Grenade & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Bomb)
            CopyFromExtendedTable(loadBearingEquipment, Tables.Items.Fields.ItemClass & "=" & ItemClass.LBE)
            CopyFromExtendedTable(germanItems, "", True)
            CopyFromExtendedTable(russianItems, "", True)
            CopyFromExtendedTable(polishItems, "", True)
            CopyFromExtendedTable(frenchItems, "", True)
            CopyFromExtendedTable(italianItems, "", True)
            CopyFromExtendedTable(dutchItems, "", True)
            CopyFromExtendedTable(chineseItems, "", True)

            'add expressions to table
            _table.Columns(Tables.Weapons.Name & Tables.Weapons.Fields.Name).Expression = Tables.Items.Fields.Name

        End With
    End Sub

    Public Sub RebuildExtendedItemTables()
        'restore the values to the 1-to-1 related tables from the items table
        With _dm.Database.DataSet

            Dim mags As DataTable = .Tables(Tables.Magazines.Name)
            Dim weapons As DataTable = .Tables(Tables.Weapons.Name)
            Dim armours As DataTable = .Tables(Tables.Armours.Name)
            Dim loadBearingEquipment As DataTable = .Tables(Tables.LoadBearingEquipment.Name)
            Dim explosives As DataTable = .Tables(Tables.Explosives.Name)
            Dim germanItems As DataTable = .Tables(Tables.GermanItems)
            Dim russianItems As DataTable = .Tables(Tables.RussianItems)
            Dim polishItems As DataTable = .Tables(Tables.PolishItems)
            Dim frenchItems As DataTable = .Tables(Tables.FrenchItems)
            Dim italianItems As DataTable = .Tables(Tables.ItalianItems)
            Dim dutchItems As DataTable = .Tables(Tables.DutchItems)
            Dim chineseItems As DataTable = .Tables(Tables.ChineseItems)

            'todo: test!
            CopyToExtendedTable(germanItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(russianItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(polishItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(frenchItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(italianItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(dutchItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(chineseItems, "", Tables.Items.Fields.ID, True, False)
            CopyToExtendedTable(explosives, Tables.Items.Fields.ItemClass & "=" & ItemClass.Grenade & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Bomb, Tables.Items.Fields.ID)
            CopyToExtendedTable(armours, Tables.Items.Fields.ItemClass & "=" & ItemClass.Armour, Tables.Items.Fields.ID)
            CopyToExtendedTable(loadBearingEquipment, Tables.Items.Fields.ItemClass & "=" & ItemClass.LBE, Tables.Items.Fields.ID)
            CopyToExtendedTable(mags, Tables.Items.Fields.ItemClass & "=" & ItemClass.Ammo, mags.TableName & Tables.Magazines.Fields.Caliber & "," & mags.TableName & Tables.Magazines.Fields.MagSize & "," & mags.TableName & Tables.Magazines.Fields.AmmoType)
            CopyToExtendedTable(weapons, Tables.Items.Fields.ItemClass & ">" & ItemClass.None & " AND " & Tables.Items.Fields.ItemClass & "<=" & ItemClass.Punch, Tables.Items.Fields.ID, True)

        End With
    End Sub

    Protected Sub ClearExtendedItemTables()
        With _dm.Database.DataSet
            .Tables(Tables.Magazines.Name).Clear()
            .Tables(Tables.Weapons.Name).Clear()
            .Tables(Tables.Armours.Name).Clear()
            .Tables(Tables.Explosives.Name).Clear()
            .Tables(Tables.GermanItems).Clear()
            .Tables(Tables.RussianItems).Clear()
            .Tables(Tables.PolishItems).Clear()
            .Tables(Tables.FrenchItems).Clear()
            .Tables(Tables.ItalianItems).Clear()
            .Tables(Tables.DutchItems).Clear()
            .Tables(Tables.ChineseItems).Clear()
            .Tables(Tables.LoadBearingEquipment.Name).Clear()
        End With
    End Sub

    Public Sub CopyFromExtendedTable(ByVal extTable As DataTable, ByVal filter As String, Optional ByVal matchIDs As Boolean = False)
        For Each c As DataColumn In extTable.Columns
            Application.DoEvents()
            If c.ColumnName <> Tables.Items.Fields.ID AndAlso Not _table.Columns.Contains(extTable.TableName & c.ColumnName) Then
                Dim newCol As DataColumn = c.Clone
                newCol.SetProperty(ColumnProperty.SubTable, True)
                newCol.ColumnName = extTable.TableName & newCol.ColumnName
                _table.Columns.Add(newCol)
            End If
        Next

        Dim itemView As New DataView(_table, filter, Tables.Items.Fields.FKey, DataViewRowState.CurrentRows)

        For i As Integer = 0 To itemView.Count - 1
            Application.DoEvents()
            Dim key As Integer
            If Not matchIDs Then
                key = itemView(i)(Tables.Items.Fields.FKey)
            Else
                key = itemView(i)(Tables.Items.Fields.ID)
            End If
            Dim r As DataRow = extTable.Rows.Find(key)
            If Not r Is Nothing Then
                For Each c As DataColumn In extTable.Columns
                    If c.ColumnName <> Tables.Items.Fields.ID AndAlso c.ColumnName <> extTable.PrimaryKey(0).ColumnName AndAlso Not _table.Columns(extTable.TableName & c.ColumnName).ReadOnly Then
                        itemView(i)(extTable.TableName & c.ColumnName) = r(c.ColumnName)
                    End If
                Next
                itemView(i)(Tables.Items.Fields.FKey) = 0
            Else
                'Stop
            End If
        Next

        extTable.Rows.Clear()
        itemView.Dispose()
    End Sub

    Public Sub CopyToExtendedTable(ByVal extTable As DataTable, ByVal filter As String, ByVal sort As String, Optional ByVal matchIDs As Boolean = False, Optional ByVal useForeignKey As Boolean = True)
        Dim itemView As New DataView(_table, filter, sort, DataViewRowState.CurrentRows)

        For i As Integer = 0 To itemView.Count - 1
            Application.DoEvents()
            Dim r As DataRow = extTable.NewRow()
            If Not matchIDs Then
                r(extTable.PrimaryKey(0)) = i
                itemView(i)(Tables.Items.Fields.FKey) = i
            Else
                r(extTable.PrimaryKey(0)) = itemView(i)(Tables.Items.Fields.ID)
                If useForeignKey Then itemView(i)(Tables.Items.Fields.FKey) = itemView(i)(Tables.Items.Fields.ID)
            End If

            For Each c As DataColumn In extTable.Columns
                If c.ColumnName <> Tables.Items.Fields.ID AndAlso c.ColumnName <> extTable.PrimaryKey(0).ColumnName Then
                    r(c) = itemView(i)(extTable.TableName & c.ColumnName)
                End If
            Next

            extTable.Rows.Add(r)
        Next

        itemView.Dispose()
    End Sub

    Protected Sub RemoveExtendedColumns(ByVal itemTable As DataTable)
        For i As Integer = itemTable.Columns.Count - 1 To 0 Step -1
            Application.DoEvents()
            If itemTable.Columns(i).GetBooleanProperty(ColumnProperty.SubTable) Then
                itemTable.Columns.RemoveAt(i)
            End If
        Next
    End Sub

    Protected Const NothingItemName As String = "Auto-generated Nothing Item"
    Protected Sub AddNothingItems(itemTable As DataTable)
        Dim missingItems As Boolean = True
        Dim view As New DataView(itemTable)

        view.RowStateFilter = DataViewRowState.CurrentRows
        view.Sort = Tables.Items.Fields.ID

        While (missingItems)
            Dim lastId As Integer = -1
            For i As Integer = 0 To view.Count - 1
                Application.DoEvents()
                Dim id As Integer = view(i)(Tables.Items.Fields.ID)
                If id - lastId > 1 Then
                    For j As Integer = lastId + 1 To id - 1
                        Dim r As DataRow = itemTable.NewRow
                        r(Tables.Items.Fields.ID) = j
                        r(Tables.Items.Fields.ItemClass) = ItemClass.None
                        r(Tables.Items.Fields.ShortName) = "Nada"
                        r(Tables.Items.Fields.Name) = NothingItemName
                        r(Tables.Items.Fields.Description) = "Index " & j
                        itemTable.Rows.Add(r)
                    Next
                    missingItems = True
                    Exit For
                End If
                lastId = id
                missingItems = False
            Next
        End While
    End Sub

    Protected Sub RemoveNothingItems()
        Dim rowCount As Integer = _table.Rows.Count

        For i As Integer = rowCount - 1 To 0 Step -1
            Application.DoEvents()
            If _table.Rows(i)(Tables.Items.Fields.Name) = NothingItemName Then
                _table.Rows.RemoveAt(i)
            End If
        Next
    End Sub

    Public Overrides Sub SaveData()
        Dim table As DataTable = _table.Copy
        RemoveImageColumn(table)
        RemoveExtendedColumns(table)
        AddNothingItems(table)

        SaveData(table)
        _table.AcceptChanges()
        table.Clear()
        table.Dispose()
    End Sub

    Public Overrides Sub DeleteRow(ByVal key As Decimal)
        'Dim row As DataRow = _table.Rows.Find(key)
        MyBase.DeleteRow(key)
        ''MM: no longer needed due to nothing items being added automatically on save
        'Dim nothingRow As DataRow = tbl.Rows.Find(0)
        'If row IsNot Nothing Then
        '    Dim maxID = tbl.Compute("MAX(" & tbl.PrimaryKey(0).ColumnName & ")", Nothing)
        '    If row(Tables.Items.Fields.ID) = maxID Then 'if this is the last record, then we can delete it
        'row.Delete()
        '    Else 'otherwise, we need to turn this into a nothing item, 
        ''because JA2 will stop looking for items once it hits this one's zero value itemclass
        'For Each c As DataColumn In tbl.Columns
        '    If Not c.ReadOnly AndAlso c IsNot tbl.PrimaryKey(0) Then row(c) = nothingRow(c)
        'Next
        'row(Tables.Items.Fields.ItemClass) = ItemClass.None
        'row(Tables.Items.Fields.Description) = "Index " & row(Tables.Items.Fields.ID)
        '    End If
        'End If
    End Sub

    Public Overrides Sub LoadData()
        _table.BeginLoadData()
        _table.Clear()

        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)
        LoadItemData(fileName, filePath)

        _table.EndLoadData()
    End Sub

    Protected Overridable Sub LoadItemData(ByVal fileName As String, ByVal filePath As String)
        Dim xmldoc As New XmlDataDocument()
        Dim xmlnode As XmlNode
        Dim xmlnode2 As XmlNodeList
        Dim xmlnode3 As XmlNodeList
        Dim i As Integer
        Dim x As Integer
        Dim y As Integer
        Dim a As Integer
        Dim da, aap, uit As Integer
        Dim uicomments As Integer = 0
        Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
        xmldoc.Load(fs)
        xmlnode = xmldoc.GetElementsByTagName("ITEMLIST").Item(0)
        For i = 0 To xmlnode.ChildNodes.Count - 1
            If xmlnode.ChildNodes.Item(i).Name = "#comment" Then
                uicomments = uicomments + 1
                Continue For
            End If
            _table.Rows.Add()
            a = 0
            da = 0
            aap = 0
            'Unknown Item Tag
            uit = 0
            xmlnode2 = xmlnode.ChildNodes.Item(i).ChildNodes
            For x = 0 To xmlnode2.Count - 1
                If xmlnode2.Item(x).Name = "#comment" Then Continue For
                If xmlnode2.Item(x).Name = "STAND_MODIFIERS" OrElse xmlnode2.Item(x).Name = "CROUCH_MODIFIERS" OrElse xmlnode2.Item(x).Name = "PRONE_MODIFIERS" Then
                    If xmlnode2.Item(x).Name = "STAND_MODIFIERS" Then a = 1
                    If xmlnode2.Item(x).Name = "CROUCH_MODIFIERS" Then a = 2
                    If xmlnode2.Item(x).Name = "PRONE_MODIFIERS" Then a = 3
                    xmlnode3 = xmlnode2.Item(x).ChildNodes
                    For y = 0 To xmlnode3.Count - 1
                        If xmlnode3.Item(y).Name = "#comment" Then Continue For

                        If _table.Columns.Contains(xmlnode3.Item(y).Name & a) Then
                            If _table.Columns(xmlnode3.Item(y).Name & a).DataType.Name = "Boolean" Then
                                _table.Rows(i - uicomments).Item(xmlnode3.Item(y).Name & a) = IIf(xmlnode3.Item(y).InnerText.Trim = 1, True, False)
                            Else
                                _table.Rows(i - uicomments).Item(xmlnode3.Item(y).Name & a) = xmlnode3.Item(y).InnerText.Trim
                            End If
                        End If
                    Next
                Else
                    If xmlnode2.Item(x).Name = "DefaultAttachment" Then
                        If da < 10 Then
                            If _table.Columns.Contains(xmlnode2.Item(x).Name & da) Then
                                _table.Rows(i - uicomments).Item(xmlnode2.Item(x).Name & da) = xmlnode2.Item(x).InnerText.Trim
                                da = da + 1
                            End If
                        End If
                    ElseIf xmlnode2.Item(x).Name = "AvailableAttachmentPoint" Then
                        If aap < 10 Then
                            If _table.Columns.Contains(xmlnode2.Item(x).Name & aap) Then
                                _table.Rows(i - uicomments).Item(xmlnode2.Item(x).Name & aap) = xmlnode2.Item(x).InnerText.Trim
                                aap = aap + 1
                            End If
                        End If
                    Else
                        If _table.Columns.Contains(xmlnode2.Item(x).Name) Then
                            If _table.Columns(xmlnode2.Item(x).Name).DataType.Name = "Boolean" Then
                                _table.Rows(i - uicomments).Item(xmlnode2.Item(x).Name) = IIf(xmlnode2.Item(x).InnerText.Trim = 1, True, False)
                            Else
                                _table.Rows(i - uicomments).Item(xmlnode2.Item(x).Name) = xmlnode2.Item(x).InnerText.Trim
                            End If
                            'JMich: Attempting to read Unknown Item Tags
                        Else
                            _table.Columns("ItemUnknownTag" & uit).Caption = xmlnode2.Item(x).Name
                            _table.Columns("ItemUnknownTag" & uit).ColumnName = xmlnode2.Item(x).Name
                            _table.Rows(i - uicomments).Item(xmlnode2.Item(x).Name) = xmlnode2.Item(x).InnerText.Trim
                            uit += 1
                        End If
                    End If
                End If
            Next
        Next
        fs.Close()
        fs.Dispose()
    End Sub

    Protected Overrides Sub WriteXml(ByVal table As DataTable, ByVal fileName As String)
        Dim view As New DataView(table, "", table.Columns(0).ColumnName, DataViewRowState.CurrentRows)
        Dim xw As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        Dim i As Integer
        Dim x As Integer
        Dim da As Integer
        Dim trim As Boolean = table.GetBooleanProperty(TableProperty.Trim)
        Dim indent As Boolean = True

        xw.WriteStartDocument()
        xw.WriteWhitespace(vbLf)
        xw.WriteStartElement(table.ExtendedProperties("DataSetName").ToString)
        xw.WriteWhitespace(vbLf)
        For i = 0 To view.Count - 1
            If Not table.Rows(i).RowState = DataRowState.Deleted Then
                indent = True
                da = 0
                xw.WriteString(vbTab)
                xw.WriteStartElement(table.TableName)
                xw.WriteString(vbLf)
                For x = 0 To table.Rows(i).ItemArray.Length - 1
                    Dim c As DataColumn = table.Columns(x)
                    Dim xmlColName As String = c.GetStringProperty(ColumnProperty.SourceColumnName) 'only used for defaultattachment and availableattachmentpoint ATM

                    'JMich: Attempting to write only changed Unknown Item Tags
                    If c.ColumnName.StartsWith("ItemUnknownTag") Then Continue For
                    If c.ColumnName.EndsWith("1") AndAlso String.IsNullOrEmpty(xmlColName) Then Exit For
                    If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> 0) _
                        OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then

                        If String.IsNullOrEmpty(xmlColName) Then xmlColName = c.ColumnName

                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)

                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            xw.WriteElementString(xmlColName, view(i)(c.ColumnName))
                        Else
                            If view(i)(c.ColumnName) Then
                                xw.WriteElementString(xmlColName, 1)
                            Else
                                xw.WriteElementString(xmlColName, 0)
                            End If
                        End If
                        xw.WriteString(vbLf)
                    End If
                Next
                'Start STAND_MODIFIERS section
                xw.WriteString(vbTab)
                xw.WriteString(vbTab)
                xw.WriteStartElement("STAND_MODIFIERS")
                For x = x To table.Rows(i).ItemArray.Length - 1
                    Dim c As DataColumn = table.Columns(x)
                    If c.ColumnName.EndsWith("2") Then Exit For
                    If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> -101) _
                       OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then
                        If indent = True Then
                            xw.WriteString(vbLf)
                            indent = False
                        End If
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), view(i)(c.ColumnName))
                        Else
                            If view(i)(c.ColumnName) Then
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 1)
                            Else
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 0)
                            End If
                        End If
                        xw.WriteString(vbLf)
                    End If
                Next
                If indent = False Then
                    xw.WriteString(vbTab)
                    xw.WriteString(vbTab)
                End If
                xw.WriteEndElement()
                xw.WriteString(vbLf)
                'End STAND_MODIFIERS section
                'Start CROUCH_MODIFIERS section
                xw.WriteString(vbTab)
                xw.WriteString(vbTab)
                xw.WriteStartElement("CROUCH_MODIFIERS")
                indent = True
                For x = x To table.Rows(i).ItemArray.Length - 1
                    Dim c As DataColumn = table.Columns(x)
                    If c.ColumnName.EndsWith("3") Then Exit For
                    If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> -101) _
                       OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then
                        If indent = True Then
                            xw.WriteString(vbLf)
                            indent = False
                        End If
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), view(i)(c.ColumnName))
                        Else
                            If view(i)(c.ColumnName) Then
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 1)
                            Else
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 0)
                            End If
                        End If
                        xw.WriteString(vbLf)
                    End If
                Next
                If indent = False Then
                    xw.WriteString(vbTab)
                    xw.WriteString(vbTab)
                End If
                xw.WriteEndElement()
                xw.WriteString(vbLf)
                'End CROUCH_MODIFIERS section
                'Start PRONE_MODIFIERS section
                xw.WriteString(vbTab)
                xw.WriteString(vbTab)
                xw.WriteStartElement("PRONE_MODIFIERS")
                indent = True
                For x = x To table.Rows(i).ItemArray.Length - 1
                    Dim c As DataColumn = table.Columns(x)
                    If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> -101) _
                       OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then
                        If indent = True Then
                            xw.WriteString(vbLf)
                            indent = False
                        End If
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), view(i)(c.ColumnName))
                        Else
                            If view(i)(c.ColumnName) Then
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 1)
                            Else
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 0)
                            End If
                        End If
                        xw.WriteString(vbLf)
                    End If
                Next
                If indent = False Then
                    xw.WriteString(vbTab)
                    xw.WriteString(vbTab)
                End If
                xw.WriteEndElement()
                xw.WriteString(vbLf)
                'End PRONE_MODIFIERS section

                xw.WriteString(vbTab)
                xw.WriteEndElement()
                xw.WriteString(vbLf)
            End If
        Next
        xw.WriteEndElement()
        xw.WriteEndDocument()
        xw.Close()

    End Sub

    Protected Overrides Sub OverwriteRow(existingRow As DataRow, newRow As DataRow, sourceRow As DataRow, sourceDB As XmlDB)
        CopyExtraRows(newRow, sourceRow, sourceDB)
        SetGraphics(newRow)
        MyBase.OverwriteRow(existingRow, newRow, sourceRow, sourceDB)
    End Sub

    Protected Overrides Sub AddNewRow(table As DataTable, newRow As DataRow, sourceRow As DataRow, sourceDB As XmlDB, displayErrors As Boolean)
        CopyExtraRows(newRow, sourceRow, sourceDB)
        SetGraphics(newRow)
        MyBase.AddNewRow(table, newRow, sourceRow, sourceDB, displayErrors)
    End Sub

    'this method will ensure that references are kept intact
    Protected Sub CopyExtraRows(newRow As DataRow, sourceRow As DataRow, sourceDB As XmlDB)
        'need to do this manually because there are no actual relations set up between the extended item table and the base tables
        Select Case newRow(Tables.Items.Fields.ItemClass)
            Case ItemClass.Ammo
                'magazines
                CopyExtraRow(Tables.AmmoTypes, newRow, sourceRow, Tables.Magazines.Name & Tables.Magazines.Fields.AmmoType, sourceDB)
                CopyExtraRow(Tables.AmmoStrings, newRow, sourceRow, Tables.Magazines.Name & Tables.Magazines.Fields.Caliber, sourceDB)
                CopyExtraRow(Tables.MagazineTypes, newRow, sourceRow, Tables.Magazines.Name & Tables.Magazines.Fields.MagType, sourceDB)

            Case ItemClass.Armour
                'armours
                CopyExtraRow(Tables.ArmourClasses, newRow, sourceRow, Tables.Armours.Name & Tables.Armours.Fields.ArmourClass, sourceDB)

            Case ItemClass.Bomb, ItemClass.Grenade
                'explosives
                CopyExtraRow(Tables.ExplosionData, newRow, sourceRow, Tables.Explosives.Name & Tables.Explosives.Fields.AnimationID, sourceDB)
                CopyExtraRow(Tables.ExplosionTypes, newRow, sourceRow, Tables.Explosives.Name & Tables.Explosives.Fields.Type, sourceDB)
                CopyExtraRow(Tables.AmmoTypes, newRow, sourceRow, Tables.Explosives.Name & Tables.Explosives.Fields.FragType, sourceDB)

            Case ItemClass.Gun, ItemClass.Launcher, ItemClass.Knife, ItemClass.ThrowingKnife, ItemClass.Thrown, ItemClass.Punch, ItemClass.Tentacle
                'weapons
                CopyExtraRow(Tables.AmmoStrings, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.Caliber, sourceDB)
                CopyExtraRow(Tables.WeaponClasses, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.WeaponClass, sourceDB)
                CopyExtraRow(Tables.WeaponTypes, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.WeaponType, sourceDB)
                CopyExtraRow(Tables.Sounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.Sound, sourceDB)
                CopyExtraRow(Tables.BurstSounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.BurstSound, sourceDB)
                CopyExtraRow(Tables.BurstSounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.SilencedBurstSound, sourceDB)
                CopyExtraRow(Tables.Sounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.SilencedSound, sourceDB)
                CopyExtraRow(Tables.Sounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.ManualReloadSound, sourceDB)
                CopyExtraRow(Tables.Sounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.LockNLoadSound, sourceDB)
                CopyExtraRow(Tables.Sounds, newRow, sourceRow, Tables.Weapons.Name & Tables.Weapons.Fields.ReloadSound, sourceDB)
            Case ItemClass.LBE
                'lbes
                'CopyExtraRow(Tables.Silhouettes, newRow, sourceRow, Tables.LoadBearingEquipment.Name & Tables.LoadBearingEquipment.Fields.Silhouette, sourceDB)
                CopyExtraRow(Tables.LbeClasses, newRow, sourceRow, Tables.LoadBearingEquipment.Name & Tables.LoadBearingEquipment.Fields.LbeClass, sourceDB)
        End Select

        'TODO: need to let user know that this happened somehow
    End Sub

    Protected Sub CopyExtraRow(tableName As String, newRow As DataRow, sourceRow As DataRow, fieldName As String, sourceDB As XmlDB)
        Dim row As DataRow = sourceDB.Table(tableName).Rows.Find(sourceRow(fieldName))

        If row IsNot Nothing Then
            Dim extraRow As DataRow = _dm.Database.CopyRows(tableName, New DataRow() {row}, sourceDB, False)
            'newRow(fieldName) = extraRow(_dm.Database.Table(tableName).PrimaryKey(0))
        End If
    End Sub

    Protected Sub SetGraphics(newRow As DataRow)
        Dim type As Integer = newRow(Tables.Items.Fields.GraphicType)
        Dim index As Integer = newRow(Tables.Items.Fields.GraphicIndex)

        If Not _dm.ItemImages.Exists(type, index) Then
            newRow(Tables.Items.Fields.GraphicType) = 0
            newRow(Tables.Items.Fields.GraphicIndex) = 0
        End If
    End Sub

End Class
