Public Module DatabaseCode
    'JMich
    Public ItemSizesRead As Boolean = False
    Public ItemSizeMax As Integer

    Public Function MakeDB(baseDir As String, tableDir As String, schemaName As String, schemaFileName As String) As DataSet
        Dim ds As New DataSet(schemaName)

        'standard tables
        Dim items As DataTable = MakeItemsTable()
        Dim weapons As DataTable = MakeWeaponsTable()
        Dim merges As DataTable = MakeMergeTable()
        Dim magazines As DataTable = MakeMagazineTable()
        Dim launchables As DataTable = MakeLaunchableTable()
        Dim ammoTypes As DataTable = MakeAmmoTypesTable()
        Dim ammoStrings As DataTable = MakeAmmoStringsTable()
        Dim attachments As DataTable = MakeAttachmentTable()
        Dim attachmentInfo As DataTable = MakeAttachmentInfoTable()
        Dim attachmentComboMerges As DataTable = MakeAttachmentComboMergeTable()
        Dim armours As DataTable = MakeArmourTable()
        Dim compatibleFaceItems As DataTable = MakeCompatibleFaceItemTable()
        Dim incompatibleAttachments As DataTable = MakeIncompatibleAttachmentTable()
        Dim explosionData As DataTable = MakeExplosionDataTable()
        Dim explosives As DataTable = MakeExplosiveTable()
        Dim germanAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("German")
        Dim russianAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Russian")
        Dim polishAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Polish")
        Dim frenchAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("French")
        Dim italianAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Italian")
        Dim dutchAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Dutch")
        Dim chineseAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Chinese")
        Dim germanItems As DataTable = MakeLocalizedItemsTable("German")
        Dim russianItems As DataTable = MakeLocalizedItemsTable("Russian")
        Dim polishItems As DataTable = MakeLocalizedItemsTable("Polish")
        Dim frenchItems As DataTable = MakeLocalizedItemsTable("French")
        Dim italianItems As DataTable = MakeLocalizedItemsTable("Italian")
        Dim dutchItems As DataTable = MakeLocalizedItemsTable("Dutch")
        Dim chineseItems As DataTable = MakeLocalizedItemsTable("Chinese")
        Dim sounds As DataTable = MakeSoundsTable()
        Dim burstSounds As DataTable = MakeBurstSoundsTable()
        Dim impItems As DataTable = MakeIMPItemsTable()
        Dim enemyGuns As DataTable = MakeEnemyGunsTable()
        Dim enemyGunsAdmin As DataTable = MakeEnemyGunsTableAdmin()
        Dim enemyGunsRegular As DataTable = MakeEnemyGunsTableRegular()
        Dim enemyGunsElite As DataTable = MakeEnemyGunsTableElite()
        Dim militiaGunsGreen As DataTable = MakeMilitiaGunsTableGreen()
        Dim militiaGunsRegular As DataTable = MakeMilitiaGunsTableRegular()
        Dim militiaGunsElite As DataTable = MakeMilitiaGunsTableElite()
        Dim enemyItems As DataTable = MakeEnemyItemsTable()
        Dim enemyItemsAdmin As DataTable = MakeEnemyItemsTableAdmin()
        Dim enemyItemsRegular As DataTable = MakeEnemyItemsTableRegular()
        Dim enemyItemsElite As DataTable = MakeEnemyItemsTableElite()
        Dim militiaItemsGreen As DataTable = MakeMilitiaItemsTableGreen()
        Dim militiaItemsRegular As DataTable = MakeMilitiaItemsTableRegular()
        Dim militiaItemsElite As DataTable = MakeMilitiaItemsTableElite()
        Dim enemyAmmo As DataTable = MakeEnemyAmmoTable()
        Dim enemyAmmoDrop As DataTable = MakeEnemyAmmoDropTable()
        Dim enemyArmourDrop As DataTable = MakeEnemyArmourDropTable()
        Dim enemyExplosiveDrop As DataTable = MakeEnemyExplosiveDropTable()
        Dim enemyMiscDrop As DataTable = MakeEnemyMiscItemDropTable()
        Dim enemyWeaponDrop As DataTable = MakeEnemyWeaponDropTable()
        Dim loadBearingEquipment As DataTable = MakeLoadBearingEquipmentTable()
        Dim pockets As DataTable = MakePocketsTable(baseDir)
        Dim silhouettes As DataTable = MakeSilhouetteTable()
        Dim mercStartingGear As DataTable = MakeMercStartingGearTable()
        Dim attachmentSlots As DataTable = MakeAttachmentSlotsTable()
        Dim nasAttachmentClasses As DataTable = MakeNasAttachmentClassTable()
        Dim attachmentPoints As DataTable = MakeAttachmentPointTable()
        Dim itemsToExplosives As DataTable = MakeITETable()
        Dim transform As DataTable = MakeTransformTable()
        'Dim drugs As DataTable = MakeDrugsTable()
        Dim food As DataTable = MakeFoodTable()
        Dim clothes As DataTable = MakeClothesTable()
        Dim randomitems As DataTable = MakeRandomTable()

        Dim albertoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Alberto)
        Dim arnieControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Arnie)
        Dim carloControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Carlo)
        Dim devinControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Devin)
        Dim elginControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Elgin)
        Dim frankControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Frank)
        Dim franzControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Franz)
        Dim fredoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Fredo)
        Dim gabbyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Gabby)
        Dim herveControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Herve)
        Dim howardControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Howard)
        Dim jakeControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Jake)
        Dim keithControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Keith)
        Dim mannyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Manny)
        Dim mickeyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Mickey)
        Dim perkoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Perko)
        Dim peterControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Peter)
        Dim samControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Sam)
        Dim tonyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Tony)

        Dim alberto As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Alberto)
        Dim arnie As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Arnie)
        Dim carlo As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Carlo)
        Dim devin As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Devin)
        Dim elgin As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Elgin)
        Dim frank As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Frank)
        Dim franz As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Franz)
        Dim fredo As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Fredo)
        Dim gabby As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Gabby)
        Dim herve As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Herve)
        Dim howard As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Howard)
        Dim jake As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Jake)
        Dim keith As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Keith)
        Dim manny As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Manny)
        Dim mickey As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Mickey)
        Dim perko As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Perko)
        Dim peter As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Peter)
        Dim sam As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Sam)
        Dim tony As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Tony)

        ds.Tables.Add(items)
        ds.Tables.Add(weapons)
        ds.Tables.Add(merges)
        ds.Tables.Add(magazines)
        ds.Tables.Add(launchables)
        ds.Tables.Add(ammoTypes)
        ds.Tables.Add(ammoStrings)
        ds.Tables.Add(attachments)
        ds.Tables.Add(attachmentInfo)
        ds.Tables.Add(attachmentComboMerges)
        ds.Tables.Add(armours)
        ds.Tables.Add(compatibleFaceItems)
        ds.Tables.Add(incompatibleAttachments)
        ds.Tables.Add(explosionData)
        ds.Tables.Add(explosives)
        ds.Tables.Add(germanAmmoStrings)
        ds.Tables.Add(russianAmmoStrings)
        ds.Tables.Add(polishAmmoStrings)
        ds.Tables.Add(frenchAmmoStrings)
        ds.Tables.Add(italianAmmoStrings)
        ds.Tables.Add(dutchAmmoStrings)
        ds.Tables.Add(chineseAmmoStrings)
        ds.Tables.Add(germanItems)
        ds.Tables.Add(russianItems)
        ds.Tables.Add(polishItems)
        ds.Tables.Add(frenchItems)
        ds.Tables.Add(italianItems)
        ds.Tables.Add(dutchItems)
        ds.Tables.Add(chineseItems)
        ds.Tables.Add(sounds)
        ds.Tables.Add(burstSounds)
        ds.Tables.Add(albertoControl)
        ds.Tables.Add(arnieControl)
        ds.Tables.Add(carloControl)
        ds.Tables.Add(devinControl)
        ds.Tables.Add(elginControl)
        ds.Tables.Add(frankControl)
        ds.Tables.Add(franzControl)
        ds.Tables.Add(fredoControl)
        ds.Tables.Add(gabbyControl)
        ds.Tables.Add(herveControl)
        ds.Tables.Add(howardControl)
        ds.Tables.Add(jakeControl)
        ds.Tables.Add(keithControl)
        ds.Tables.Add(mannyControl)
        ds.Tables.Add(mickeyControl)
        ds.Tables.Add(perkoControl)
        ds.Tables.Add(peterControl)
        ds.Tables.Add(samControl)
        ds.Tables.Add(tonyControl)
        ds.Tables.Add(alberto)
        ds.Tables.Add(arnie)
        ds.Tables.Add(carlo)
        ds.Tables.Add(devin)
        ds.Tables.Add(elgin)
        ds.Tables.Add(frank)
        ds.Tables.Add(franz)
        ds.Tables.Add(fredo)
        ds.Tables.Add(gabby)
        ds.Tables.Add(herve)
        ds.Tables.Add(howard)
        ds.Tables.Add(jake)
        ds.Tables.Add(keith)
        ds.Tables.Add(manny)
        ds.Tables.Add(mickey)
        ds.Tables.Add(perko)
        ds.Tables.Add(peter)
        ds.Tables.Add(sam)
        ds.Tables.Add(tony)
        ds.Tables.Add(impItems)
        ds.Tables.Add(enemyGuns)
        ds.Tables.Add(enemyGunsAdmin)
        ds.Tables.Add(enemyGunsRegular)
        ds.Tables.Add(enemyGunsElite)
        ds.Tables.Add(militiaGunsGreen)
        ds.Tables.Add(militiaGunsRegular)
        ds.Tables.Add(militiaGunsElite)
        ds.Tables.Add(enemyItems)
        ds.Tables.Add(enemyItemsAdmin)
        ds.Tables.Add(enemyItemsRegular)
        ds.Tables.Add(enemyItemsElite)
        ds.Tables.Add(militiaItemsGreen)
        ds.Tables.Add(militiaItemsRegular)
        ds.Tables.Add(militiaItemsElite)
        ds.Tables.Add(enemyAmmo)
        ds.Tables.Add(enemyAmmoDrop)
        ds.Tables.Add(enemyArmourDrop)
        ds.Tables.Add(enemyExplosiveDrop)
        ds.Tables.Add(enemyMiscDrop)
        ds.Tables.Add(enemyWeaponDrop)
        ds.Tables.Add(loadBearingEquipment)
        ds.Tables.Add(pockets)
        ds.Tables.Add(silhouettes)
        ds.Tables.Add(mercStartingGear)
        ds.Tables.Add(attachmentSlots)
        ds.Tables.Add(nasAttachmentClasses)
        ds.Tables.Add(itemsToExplosives)
        ds.Tables.Add(transform)
        ds.Tables.Add(attachmentPoints)
        'ds.Tables.Add(drugs)
        ds.Tables.Add(food)
        ds.Tables.Add(clothes)
        ds.Tables.Add(randomitems)

        'lookup tables
        Dim mergeTypes As DataTable = MakeLookupTable(Of Integer)("MergeType")
        Dim explosionTypes As DataTable = MakeLookupTable(Of Integer)("ExplosionType")
        Dim explosionSize As DataTable = MakeLookupTable(Of Integer)("ExplosionSize")
        Dim itemClasses As DataTable = MakeLookupTable(Of Integer)("ItemClass")
        Dim skillCheckTypes As DataTable = MakeLookupTable(Of Integer)("SkillCheckType")
        Dim armourClasses As DataTable = MakeLookupTable(Of Integer)("ArmourClass")
        Dim weaponTypes As DataTable = MakeLookupTable(Of Integer)("WeaponType")
        Dim weaponClasses As DataTable = MakeLookupTable(Of Integer)("WeaponClass")
        Dim cursors As DataTable = MakeLookupTable(Of Integer)("Cursor")
        Dim lbeClasses As DataTable = MakeLookupTable(Of Integer)("LBEClass")
        Dim pocketSizes As DataTable = MakeLookupTable(Of Integer)("PocketSize")
        Dim attachmentSystem As DataTable = MakeLookupTable(Of Integer)("AttachmentSystem")
        Dim magazineType As DataTable = MakeLookupTable(Of Integer)("MagazineType")
        Dim attachmentClass As DataTable = MakeLookupTable(Of ULong)("AttachmentClass")
        Dim drugType As DataTable = MakeLookupTable(Of ULong)("DrugType")
        Dim separability As DataTable = MakeLookupTable(Of Integer)("Separability")
        Dim itemFlags As DataTable = MakeLookupTable(Of ULong)("ItemFlag")
        Dim ammoFlags As DataTable = MakeLookupTable(Of ULong)("AmmoFlag")
        Dim AmmoChoices As DataTable = MakeLookupTable(Of Integer)("AmmoChoices")

        ds.Tables.Add(mergeTypes)
        ds.Tables.Add(explosionTypes)
        ds.Tables.Add(explosionSize)
        ds.Tables.Add(itemClasses)
        ds.Tables.Add(skillCheckTypes)
        ds.Tables.Add(armourClasses)
        ds.Tables.Add(weaponTypes)
        ds.Tables.Add(weaponClasses)
        ds.Tables.Add(cursors)
        ds.Tables.Add(lbeClasses)
        ds.Tables.Add(pocketSizes)
        ds.Tables.Add(attachmentSystem)
        ds.Tables.Add(magazineType)
        ds.Tables.Add(attachmentClass)
        ds.Tables.Add(drugType)
        ds.Tables.Add(separability)
        ds.Tables.Add(itemFlags)
        ds.Tables.Add(ammoFlags)
        ds.Tables.Add(AmmoChoices)

        ' -------------------------
        'relations
        ' -------------------------
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "firstItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "secondItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "firstResultingItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "secondResultingItemIndex"))
        ds.Relations.Add(MakeRelation(mergeTypes, merges, "id", "mergeType"))

        ds.Relations.Add(MakeRelation(items, transform, "uiIndex", "usItem"))
        For i As Integer = 1 To 10
            ds.Relations.Add(MakeRelation(items, transform, "uiIndex", "usResult" & i))
        Next

        ds.Relations.Add(MakeRelation(ammoTypes, magazines, "uiIndex", "ubAmmoType"))
        ds.Relations.Add(MakeRelation(ammoStrings, magazines, "uiIndex", "ubCalibre"))

        ds.Relations.Add(MakeRelation(items, launchables, "uiIndex", "launchableIndex"))
        ds.Relations.Add(MakeRelation(items, launchables, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(items, incompatibleAttachments, "uiIndex", "itemIndex"))
        ds.Relations.Add(MakeRelation(items, incompatibleAttachments, "uiIndex", "incompatibleattachmentIndex"))

        ds.Relations.Add(MakeRelation(explosionData, explosives, "uiIndex", "ubAnimationID"))
        ds.Relations.Add(MakeRelation(explosionTypes, explosives, "id", "ubType"))
        ds.Relations.Add(MakeRelation(explosionSize, ammoTypes, "id", "explosionSize"))

        ds.Relations.Add(MakeRelation(sounds, explosionData, "id", "ExplosionSoundID"))

        ds.Relations.Add(MakeRelation(items, compatibleFaceItems, "uiIndex", "compatiblefaceitemIndex"))
        ds.Relations.Add(MakeRelation(items, compatibleFaceItems, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(items, attachmentInfo, "uiIndex", "usItem"))
        ds.Relations.Add(MakeRelation(itemClasses, attachmentInfo, "id", "uiItemClass"))
        ds.Relations.Add(MakeRelation(skillCheckTypes, attachmentInfo, "id", "bAttachmentSkillCheck"))

        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usItem"))
        For i As Integer = 1 To 20
            ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usAttachment" & i))
        Next
        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usResult"))

        ds.Relations.Add(MakeRelation(items, attachments, "uiIndex", "attachmentIndex"))
        ds.Relations.Add(MakeRelation(items, attachments, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(armourClasses, armours, "id", "ubArmourClass"))

        ds.Relations.Add(MakeRelation(weaponTypes, weapons, "id", "ubWeaponType"))
        ds.Relations.Add(MakeRelation(weaponClasses, weapons, "id", "ubWeaponClass"))
        ds.Relations.Add(MakeRelation(ammoStrings, weapons, "uiIndex", "ubCalibre"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "SilencedSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sReloadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sLocknLoadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "ManualReloadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sSilencedBurstSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sBurstSound"))

        ds.Relations.Add(MakeRelation(itemClasses, items, "id", "usItemClass"))
        ds.Relations.Add(MakeRelation(cursors, items, "id", "ubCursor"))

        ds.Relations.Add(MakeRelation(lbeClasses, loadBearingEquipment, "id", "lbeClass"))
        ds.Relations.Add(MakeRelation(silhouettes, pockets, "id", "pSilhouette"))

        ds.Relations.Add(MakeRelation(items, alberto, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, arnie, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, carlo, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, devin, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, elgin, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, frank, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, franz, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, fredo, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, gabby, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, herve, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, howard, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, jake, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, keith, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, manny, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, mickey, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, perko, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, peter, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, sam, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, tony, "uiIndex", "sItemIndex"))

        ds.Relations.Add(MakeRelation(ammoTypes, enemyAmmoDrop, "uiIndex", "uiType"))
        ds.Relations.Add(MakeRelation(armourClasses, enemyArmourDrop, "id", "ubArmourClass"))
        ds.Relations.Add(MakeRelation(explosionTypes, enemyExplosiveDrop, "id", "ubType"))
        ds.Relations.Add(MakeRelation(itemClasses, enemyMiscDrop, "id", "usItemClass"))
        ds.Relations.Add(MakeRelation(weaponTypes, enemyWeaponDrop, "id", "ubWeaponType"))

        'Random Items Relations
        ds.Relations.Add(MakeRelation(randomitems, items, "uiIndex", "randomitem"))
        For i As Integer = 1 To 10
            ds.Relations.Add(MakeRelation(items, randomitems, "uiIndex", "item" & i))
            ds.Relations.Add(MakeRelation(randomitems, randomitems, "uiIndex", "randomitem" & i))
        Next
        ds.Relations.Add(MakeRelation(items, clothes, "uiIndex", "uiIndex"))

        'Merc Starting Gear Relations
        For x As Integer = 1 To 5
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mHelmet" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mVest" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mLeg" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mWeapon" & x, True, False))
            For i As Integer = 0 To 3
                ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mBig" & i & x, True, False))
            Next
            For i As Integer = 0 To 7
                ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "mSmall" & i & x, True, False))
            Next
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "lVest" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "lLeftThigh" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "lRightThigh" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "lCPack" & x, True, False))
            ds.Relations.Add(MakeRelation(items, mercStartingGear, "uiIndex", "lBPack" & x, True, False))
        Next

        'Relations for item choices
        For x As Integer = 1 To 50
            ds.Relations.Add(MakeRelation(items, impItems, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyGuns, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyGunsAdmin, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyGunsRegular, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyGunsElite, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaGunsGreen, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaGunsRegular, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaGunsElite, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyItems, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyItemsAdmin, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyItemsRegular, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyItemsElite, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaItemsGreen, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaItemsRegular, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, militiaItemsElite, "uiIndex", "bItemNo" & x, True, False))
            ds.Relations.Add(MakeRelation(items, enemyAmmo, "uiIndex", "bItemNo" & x, True, False))
        Next

        ' -------------------------
        'populate Lookup Tables
        ' -------------------------
        Dim lookupFilename As String

        ' SkillCheckTypes
        lookupFilename = tableDir + "\\Lookup\\" + skillCheckTypes.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(skillCheckTypes, 0, "None")
            AddLookupData(skillCheckTypes, 1, "Lockpick")
            AddLookupData(skillCheckTypes, 2, "Elec. Lockpick")
            AddLookupData(skillCheckTypes, 3, "Attach Timed Detonator")
            AddLookupData(skillCheckTypes, 4, "Attach Remote Detonator")
            AddLookupData(skillCheckTypes, 5, "Plant Timed Bomb")
            AddLookupData(skillCheckTypes, 6, "Plant Remote Bomb")
            AddLookupData(skillCheckTypes, 7, "Open With Crowbar")
            AddLookupData(skillCheckTypes, 8, "Smash Door")
            AddLookupData(skillCheckTypes, 9, "Disarm Trap")
            AddLookupData(skillCheckTypes, 10, "Unjam Gun")
            AddLookupData(skillCheckTypes, 11, "Notice Dart")
            AddLookupData(skillCheckTypes, 12, "Lie to Queen")
            AddLookupData(skillCheckTypes, 13, "Attach Special Item")
            AddLookupData(skillCheckTypes, 14, "Attach Special Elec. Item")
            AddLookupData(skillCheckTypes, 15, "Disarm Elec. Trap")
        Else
            LookupFile.AddLookupData(lookupFilename, skillCheckTypes)
        End If

        ' MergeTypes
        lookupFilename = tableDir + "\\Lookup\\" + mergeTypes.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(mergeTypes, 0, "Destruction")
            AddLookupData(mergeTypes, 1, "Combine")
            AddLookupData(mergeTypes, 2, "Treat Armour")
            AddLookupData(mergeTypes, 3, "Explosive (Hard)")
            AddLookupData(mergeTypes, 4, "Easy")
            AddLookupData(mergeTypes, 5, "Electronic")
            AddLookupData(mergeTypes, 6, "Use Item")
            AddLookupData(mergeTypes, 7, "Use Item (Hard)")
            AddLookupData(mergeTypes, 8, "Swap Barrel")
            AddLookupData(mergeTypes, 9, "Explosive (Easy)")
            AddLookupData(mergeTypes, 10, "Mechanical (Easy)")
            AddLookupData(mergeTypes, 11, "Mechanical (Hard)")
            AddLookupData(mergeTypes, 12, "Tripwire Roll")
            AddLookupData(mergeTypes, 13, "Use (Drain Status)")
        Else
            LookupFile.AddLookupData(lookupFilename, mergeTypes)
        End If

        'ExplosionTypes
        lookupFilename = tableDir + "\\Lookup\\" + explosionTypes.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(explosionTypes, 0, "Normal")
            AddLookupData(explosionTypes, 1, "Stun")
            AddLookupData(explosionTypes, 2, "Tear Gas")
            AddLookupData(explosionTypes, 3, "Mustard Gas")
            AddLookupData(explosionTypes, 4, "Flare")
            AddLookupData(explosionTypes, 5, "Noise")
            AddLookupData(explosionTypes, 6, "Smoke")
            AddLookupData(explosionTypes, 7, "Creature Gas")
            AddLookupData(explosionTypes, 8, "Fire")
            AddLookupData(explosionTypes, 9, "Flashbang")
            AddLookupData(explosionTypes, 10, "Signal Shell")
        Else
            LookupFile.AddLookupData(lookupFilename, explosionTypes)
        End If

        'ExplosionSize
        lookupFilename = tableDir + "\\Lookup\\" + explosionSize.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(explosionSize, 0, "None")
            AddLookupData(explosionSize, 1, "Standard")
            AddLookupData(explosionSize, 2, "HighExplosive")
        Else
            LookupFile.AddLookupData(lookupFilename, explosionSize)
        End If

        'ArmourClasses
        lookupFilename = tableDir + "\\Lookup\\" + armourClasses.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(armourClasses, 0, "Helmet")
            AddLookupData(armourClasses, 1, "Vest")
            AddLookupData(armourClasses, 2, "Leggings")
            AddLookupData(armourClasses, 3, "Plate")
            AddLookupData(armourClasses, 4, "Monster")
            AddLookupData(armourClasses, 5, "Vehicle")
        Else
            LookupFile.AddLookupData(lookupFilename, armourClasses)
        End If

        'WeaponClasses
        lookupFilename = tableDir + "\\Lookup\\" + weaponClasses.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(weaponClasses, 0, "None")
            AddLookupData(weaponClasses, 1, "Handgun")
            AddLookupData(weaponClasses, 2, "Submachinegun")
            AddLookupData(weaponClasses, 3, "Rifle")
            AddLookupData(weaponClasses, 4, "Machinegun")
            AddLookupData(weaponClasses, 5, "Shotgun")
            AddLookupData(weaponClasses, 6, "Knife")
            AddLookupData(weaponClasses, 7, "Monster")
        Else
            LookupFile.AddLookupData(lookupFilename, weaponClasses)
        End If

        'WeaponTypes
        lookupFilename = tableDir + "\\Lookup\\" + weaponTypes.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(weaponTypes, 0, "None")
            AddLookupData(weaponTypes, 1, "Pistol")
            AddLookupData(weaponTypes, 2, "Machine Pistol")
            AddLookupData(weaponTypes, 3, "Submachinegun")
            AddLookupData(weaponTypes, 4, "Rifle")
            AddLookupData(weaponTypes, 5, "Sniper Rifle")
            AddLookupData(weaponTypes, 6, "Assault Rifle")
            AddLookupData(weaponTypes, 7, "Light Machinegun")
            AddLookupData(weaponTypes, 8, "Shotgun")
        Else
            LookupFile.AddLookupData(lookupFilename, weaponTypes)
        End If

        'Cursors
        lookupFilename = tableDir + "\\Lookup\\" + cursors.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(cursors, 0, "Invalid")
            AddLookupData(cursors, 1, "Quest")
            AddLookupData(cursors, 2, "Punch")
            AddLookupData(cursors, 3, "Target")
            AddLookupData(cursors, 4, "Knife")
            AddLookupData(cursors, 5, "Aid")
            AddLookupData(cursors, 6, "Toss")
            AddLookupData(cursors, 8, "Mine")
            AddLookupData(cursors, 9, "Lockpick")
            AddLookupData(cursors, 10, "Metal Detector")
            AddLookupData(cursors, 11, "Crowbar")
            AddLookupData(cursors, 12, "Surveillance Camera")
            AddLookupData(cursors, 13, "Camera")
            AddLookupData(cursors, 14, "Key")
            AddLookupData(cursors, 15, "Saw")
            AddLookupData(cursors, 16, "Wirecutter")
            AddLookupData(cursors, 17, "Remote")
            AddLookupData(cursors, 18, "Bomb")
            AddLookupData(cursors, 19, "Repair")
            AddLookupData(cursors, 20, "Trajectory")
            AddLookupData(cursors, 21, "Jar")
            AddLookupData(cursors, 22, "Tin can")
            AddLookupData(cursors, 23, "Refuel")
        Else
            LookupFile.AddLookupData(lookupFilename, cursors)
        End If

        'ItemClasses
        lookupFilename = tableDir + "\\Lookup\\" + itemClasses.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(itemClasses, 1, "Nothing")
            AddLookupData(itemClasses, 2, "Gun")
            AddLookupData(itemClasses, 4, "Knife")
            AddLookupData(itemClasses, 8, "Throwing Knife")
            AddLookupData(itemClasses, 16, "Launcher")
            AddLookupData(itemClasses, 32, "Tentacle")
            AddLookupData(itemClasses, 64, "Thrown Weapon")
            AddLookupData(itemClasses, 128, "Blunt Weapon")
            AddLookupData(itemClasses, 256, "Grenade")
            AddLookupData(itemClasses, 512, "Bomb")
            AddLookupData(itemClasses, 1024, "Ammo")
            AddLookupData(itemClasses, 2048, "Armour")
            AddLookupData(itemClasses, 4096, "Medkit")
            AddLookupData(itemClasses, 8192, "Kit")
            'AddLookupData(itemClasses, 16384, "(Unused)")
            AddLookupData(itemClasses, 32768, "Face Item")
            AddLookupData(itemClasses, 65536, "Key")
            AddLookupData(itemClasses, 131072, "Load Bearing Equipment")
            AddLookupData(itemClasses, 16777216, "MOLLE")
            AddLookupData(itemClasses, 268435456, "Misc")
            AddLookupData(itemClasses, 268435712, "Grenade / Misc")
            AddLookupData(itemClasses, 268601344, "Armour/Face Item/LBE/Misc")
            AddLookupData(itemClasses, 536870912, "Money")
            AddLookupData(itemClasses, 1073741824, "Random Item")
        Else
            LookupFile.AddLookupData(lookupFilename, itemClasses)
        End If

        'LBEClasses
        lookupFilename = tableDir + "\\Lookup\\" + lbeClasses.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(lbeClasses, 0, "Nothing")
            AddLookupData(lbeClasses, 1, "Thigh Pack")
            AddLookupData(lbeClasses, 2, "Vest")
            AddLookupData(lbeClasses, 3, "Combat Pack")
            AddLookupData(lbeClasses, 4, "Backpack")
        Else
            LookupFile.AddLookupData(lookupFilename, lbeClasses)
        End If

        'PocketSizes
        lookupFilename = tableDir + "\\Lookup\\" + pocketSizes.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(pocketSizes, 0, "None")
            AddLookupData(pocketSizes, 1, "Small")
            AddLookupData(pocketSizes, 2, "Medium")
            AddLookupData(pocketSizes, 3, "Large")
        Else
            LookupFile.AddLookupData(lookupFilename, pocketSizes)
        End If

        'AttachmentSystem
        lookupFilename = tableDir + "\\Lookup\\" + attachmentSystem.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(attachmentSystem, 0, "Any")
            AddLookupData(attachmentSystem, 1, "OAS Only")
            AddLookupData(attachmentSystem, 2, "NAS Only")
        Else
            LookupFile.AddLookupData(lookupFilename, attachmentSystem)
        End If

        'MagazineType
        lookupFilename = tableDir + "\\Lookup\\" + magazineType.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(magazineType, 0, "Magazine")
            AddLookupData(magazineType, 1, "Bullet(s)")
            AddLookupData(magazineType, 2, "Box")
            AddLookupData(magazineType, 3, "Crate")
        Else
            LookupFile.AddLookupData(lookupFilename, magazineType)
        End If

        'AttachmentClass
        lookupFilename = tableDir + "\\Lookup\\" + attachmentClass.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(attachmentClass, 0, "Default")
            AddLookupData(attachmentClass, 1, "Bipod")
            AddLookupData(attachmentClass, 2, "Suppressor")
            AddLookupData(attachmentClass, 4, "Laser")
            AddLookupData(attachmentClass, 8, "Sight")
            AddLookupData(attachmentClass, 12, "Laser + Sight")
            AddLookupData(attachmentClass, 16, "Scope")
            AddLookupData(attachmentClass, 20, "Laser + Scope")
            AddLookupData(attachmentClass, 24, "Sight + Scope")
            AddLookupData(attachmentClass, 28, "Laser + Sight + Scope")
            AddLookupData(attachmentClass, 32, "Stock")
            AddLookupData(attachmentClass, 64, "Magwell")
            AddLookupData(attachmentClass, 128, "Internal")
            AddLookupData(attachmentClass, 256, "External")
            AddLookupData(attachmentClass, 512, "Underbarrel")
            AddLookupData(attachmentClass, 513, "Bipod + Underbarrel")
            AddLookupData(attachmentClass, 1024, "Grenade")
            AddLookupData(attachmentClass, 2048, "Rocket")
            AddLookupData(attachmentClass, 4096, "Foregrip")
            AddLookupData(attachmentClass, 4097, "Bipod + Foregrip")
            AddLookupData(attachmentClass, 4608, "Foregrip + Underbarrel")
            AddLookupData(attachmentClass, 8192, "Helmet")
            AddLookupData(attachmentClass, 16384, "Vest")
            AddLookupData(attachmentClass, 32768, "Pants")
            AddLookupData(attachmentClass, 65536, "Detonator")
            AddLookupData(attachmentClass, 131072, "Battery")
            AddLookupData(attachmentClass, 262144, "Extender")
            AddLookupData(attachmentClass, 524288, "Sling")
            AddLookupData(attachmentClass, 1048576, "Remote Detonator")
            AddLookupData(attachmentClass, 2097152, "Defuser")
            AddLookupData(attachmentClass, 3145728, "Remote Detonator + Defuser")
            AddLookupData(attachmentClass, 4194304, "Iron Sight")
            AddLookupData(attachmentClass, 8388608, "Feeder")
            AddLookupData(attachmentClass, 16777216, "Modular Pouch")
            AddLookupData(attachmentClass, 33554432, "Rifle Grenade")
        Else
            LookupFile.AddLookupData(lookupFilename, attachmentClass)
        End If

        ' TODO.RW: AttachmentPoint.xml is missing!
        ' TODO.RW: NASAttachmentClass.xml is missing!
        ' TODO.RW: NASLayoutClass.xml is missing!
        ' TODO.RW: Silhouette.xml is missing!

        'DrugType
        lookupFilename = tableDir + "\\Lookup\\" + drugType.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(drugType, 0, "None")
            AddLookupData(drugType, 1, "Adrenaline")
            AddLookupData(drugType, 2, "Alcohol")
            AddLookupData(drugType, 4, "Regeneration")
            AddLookupData(drugType, 8, "Damage Resistance")
            AddLookupData(drugType, 16, "Strength")
            AddLookupData(drugType, 32, "Agility")
            AddLookupData(drugType, 64, "Dexterity")
            AddLookupData(drugType, 128, "Wisdom")
            AddLookupData(drugType, 256, "Perception")
            AddLookupData(drugType, 512, "Psychosis")
            AddLookupData(drugType, 1024, "Nervousness")
            AddLookupData(drugType, 1280, "Reflex")
            AddLookupData(drugType, 2048, "Claustrophobia")
            AddLookupData(drugType, 4096, "Heat Intolerance")
            AddLookupData(drugType, 8192, "Fear of Insects")
            AddLookupData(drugType, 16384, "Forgetfulness")
            AddLookupData(drugType, 30824, "Stim")
            AddLookupData(drugType, 32768, "Blindness")
            AddLookupData(drugType, 65536, "Unconsciousness")
            AddLookupData(drugType, 66064, "Barrage")
            AddLookupData(drugType, 131072, "Vision")
            AddLookupData(drugType, 262144, "Tunnel Vision")
            AddLookupData(drugType, 425984, "Occulin")
            AddLookupData(drugType, 1048576, "Cure")
            AddLookupData(drugType, 1081324, "Cure+Regen+Dmg Resist+Agl+Dex+Wis+Per+Psycho+Nerv+Claus+Heat Int+Fear Ins+Forget")
        Else
            LookupFile.AddLookupData(lookupFilename, drugType)
        End If

        'Separability
        lookupFilename = tableDir + "\\Lookup\\" + separability.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(separability, 0, "Removable")
            AddLookupData(separability, 1, "Inseparable")
            AddLookupData(separability, 2, "Replaceable")

        Else
            LookupFile.AddLookupData(lookupFilename, separability)
        End If

        'ItemFlag
        lookupFilename = tableDir + "\\Lookup\\" + itemFlags.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(itemFlags, 0, "None")
            AddLookupData(itemFlags, 1, "Empty Sandbag")
            AddLookupData(itemFlags, 2, "Full Sandbag")
            AddLookupData(itemFlags, 4, "Shovel")
            AddLookupData(itemFlags, 8, "Concertina")
            AddLookupData(itemFlags, 16, "Water Drum")
            AddLookupData(itemFlags, 32, "Bloodcat Meat")
            AddLookupData(itemFlags, 64, "Cow Meat")
            AddLookupData(itemFlags, 128, "Belt Fed")
            AddLookupData(itemFlags, 256, "Ammo Belt")
            AddLookupData(itemFlags, 512, "Ammo Belt Vest")
            AddLookupData(itemFlags, 1024, "Camo Remover")
            AddLookupData(itemFlags, 2048, "Gun Cleaner")
            AddLookupData(itemFlags, 4096, "Interesting Item (AI)")
            AddLookupData(itemFlags, 8192, "Garotte")
            AddLookupData(itemFlags, 16384, "Covert Item")
            AddLookupData(itemFlags, 24576, "Garotte + Covert Item")
            AddLookupData(itemFlags, 32768, "Corpse")
            AddLookupData(itemFlags, 65536, "Bloodcat Skin")
            AddLookupData(itemFlags, 131072, "No Metal Detection")
            AddLookupData(itemFlags, 262144, "Jumping Grenade")
            AddLookupData(itemFlags, 524288, "Handcuffs")
            AddLookupData(itemFlags, 1048576, "Taser")
            AddLookupData(itemFlags, 2097152, "Scuba Bottle")
            AddLookupData(itemFlags, 4194304, "Scuba Mask")
            AddLookupData(itemFlags, 8388608, "Scuba Fins")
            AddLookupData(itemFlags, 16777216, "Tripwire Roll")
            AddLookupData(itemFlags, 33554432, "Radio Set")
            AddLookupData(itemFlags, 67108864, "Signal Shell")
            AddLookupData(itemFlags, 134217728, "Power Pack")

        Else
            LookupFile.AddLookupData(lookupFilename, itemFlags)
        End If

        'AmmoFlag
        lookupFilename = tableDir + "\\Lookup\\" + ammoFlags.TableName + ".xml"
        If Not System.IO.File.Exists(lookupFilename) Then
            AddLookupData(ammoFlags, 0, "None")
            AddLookupData(ammoFlags, 1, "Neurotoxin")
            AddLookupData(ammoFlags, 2, "Pepper Spray")
        Else
            LookupFile.AddLookupData(lookupFilename, ammoFlags)
        End If

        'AmmoChoices for inventories
        AddLookupData(AmmoChoices, 19, "Default Enemy Ammo")
        AddLookupData(AmmoChoices, 29, "Admin Enemy Ammo")
        AddLookupData(AmmoChoices, 39, "Regular Enemy Ammo")
        AddLookupData(AmmoChoices, 49, "Elite Enemy Ammo")
        AddLookupData(AmmoChoices, 59, "Green Militia Ammo")
        AddLookupData(AmmoChoices, 69, "Regular MilitiaAmmo")
        AddLookupData(AmmoChoices, 79, "Elite Militia Ammo")



        'write out to xml
        If Not System.IO.File.Exists(schemaFileName) Then
            ds.WriteXmlSchema(schemaFileName)
        End If

        If Not IO.Directory.Exists(tableDir & "Lookup") Then IO.Directory.CreateDirectory(tableDir & "Lookup")

        For Each t As DataTable In ds.Tables
            Dim lookupFile As String = tableDir + CStr(t.ExtendedProperties(TableProperty.FileName))
            ' RoWa21: Only create the file if it does not exist
            If Not System.IO.File.Exists(lookupFile) Then
                If t.Rows.Count > 0 Then
                    t.WriteXml(tableDir & CStr(t.ExtendedProperties(TableProperty.FileName)))
                End If
            End If
        Next

        Return ds
    End Function

    Private Function MakeColumn(ByVal columnName As String, ByVal caption As String, ByVal type As Type, Optional ByVal defaultValue As Integer = 0, _
    Optional ByVal lookup_Table As String = Nothing, Optional ByVal lookup_ValueColumn As String = Nothing, Optional ByVal lookup_TextColumn As String = Nothing, _
    Optional ByVal lookup_AddBlank As Boolean = False, Optional ByVal lookup_Filter As String = Nothing, Optional ByVal hideInGrid As Boolean = False, _
    Optional ByVal maxLength As Integer = 0, Optional ByVal lookup_Sort As String = Nothing, Optional ByVal tooltipText As String = Nothing, Optional ReferenceRequired As Boolean = False,
    Optional lookup_FirstValuePrefix As String = Nothing, Optional sourceColumnName As String = Nothing) As DataColumn
        Dim c As DataColumn
        c = New DataColumn(columnName)
        With c
            .Caption = caption
            .DataType = type
            .AllowDBNull = False

            If type.Equals(GetType(Integer)) Or type.Equals(GetType(Boolean)) Or type.Equals(GetType(ULong)) Or type.Equals(GetType(Decimal)) Or type.Equals(GetType(Byte)) Or type.Equals(GetType(Long)) Then
                .DefaultValue = defaultValue
            Else
                .DefaultValue = ""
                If maxLength > 0 Then .MaxLength = maxLength
            End If
            If Not String.IsNullOrEmpty(lookup_Table) Then .ExtendedProperties.Add(ColumnProperty.Lookup_Table, lookup_Table)
            If Not String.IsNullOrEmpty(lookup_ValueColumn) Then .ExtendedProperties.Add(ColumnProperty.Lookup_ValueColumn, lookup_ValueColumn)
            If Not String.IsNullOrEmpty(lookup_TextColumn) Then .ExtendedProperties.Add(ColumnProperty.Lookup_TextColumn, lookup_TextColumn)
            If Not String.IsNullOrEmpty(lookup_Filter) Then .ExtendedProperties.Add(ColumnProperty.Lookup_Filter, lookup_Filter)
            If Not String.IsNullOrEmpty(lookup_Sort) Then .ExtendedProperties.Add(ColumnProperty.Lookup_Sort, lookup_Sort)
            If Not String.IsNullOrEmpty(lookup_AddBlank) Then .ExtendedProperties.Add(ColumnProperty.Lookup_AddBlank, lookup_AddBlank)
            If Not String.IsNullOrEmpty(lookup_FirstValuePrefix) Then .ExtendedProperties.Add(ColumnProperty.Lookup_FirstValuePrefix, lookup_FirstValuePrefix)
            If hideInGrid Then .ExtendedProperties.Add(ColumnProperty.Grid_Hidden, True)
            If ReferenceRequired Then .ExtendedProperties.Add(ColumnProperty.ReferenceRequired, True)

            If Not String.IsNullOrEmpty(tooltipText) Then .ExtendedProperties.Add(ColumnProperty.ToolTip, tooltipText)
            If Not String.IsNullOrEmpty(sourceColumnName) Then .ExtendedProperties.Add(ColumnProperty.SourceColumnName, sourceColumnName)
        End With
        Return c
    End Function

    Private Function MakeLookupTable(Of idType)(ByVal tableName As String) As DataTable
        Dim t As New DataTable(tableName)
        t.ExtendedProperties.Add(TableProperty.FileName, "Lookup\" & tableName & ".xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(idType)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        t.Columns("id").ReadOnly = True

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeSilhouetteTable() As DataTable
        Dim t As New DataTable("Silhouette")
        t.ExtendedProperties.Add(TableProperty.FileName, "Lookup\Silhouette.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeNasAttachmentClassTable() As DataTable
        Dim t As New DataTable("NasAttachmentClass")
        t.ExtendedProperties.Add(TableProperty.FileName, "Lookup\NasAttachmentClass.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(ULong)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk
        Return t
    End Function

    Private Function MakeAttachmentPointTable() As DataTable
        Dim t As New DataTable("AttachmentPoint")
        t.ExtendedProperties.Add(TableProperty.FileName, "Lookup\AttachmentPoint.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(ULong)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk
        Return t
    End Function

    Private Function MakeMergeTable() As DataTable
        Dim t As New DataTable("MERGE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Merges.xml")

        t.Columns.Add(MakeColumn("firstItemIndex", "First Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("secondItemIndex", "Second Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("firstResultingItemIndex", "First Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("secondResultingItemIndex", "Second Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("mergeType", "Type", GetType(Integer), , "MergeType", "id", "name"))
        t.Columns.Add(MakeColumn("APCost", "AP Cost", GetType(Integer)))

        AddConstraint(t, New String() {"firstItemIndex", "secondItemIndex", "firstResultingItemIndex", "secondResultingItemIndex"}, True)

        Return t
    End Function

    Private Function MakeMagazineTable() As DataTable
        Dim t As New DataTable("MAGAZINE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Magazines.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubCalibre", "Caliber", GetType(Integer), , "AMMO", "uiIndex", "AmmoCaliber"))
        t.Columns.Add(MakeColumn("ubMagSize", "Mag Size", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubAmmoType", "Ammo Type", GetType(Integer), , "AMMOTYPE", "uiIndex", "name"))
        t.Columns.Add(MakeColumn("ubMagType", "Mag Type", GetType(Integer), , "MagazineType", "id", "name"))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        'AddConstraint(t, New String() {"ubCalibre", "ubMagSize", "ubAmmoType"}, False)

        Return t
    End Function

    Private Function MakeLaunchableTable() As DataTable
        Dim t As New DataTable("LAUNCHABLE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Launchables.xml")

        t.Columns.Add(MakeColumn("launchableIndex", "Launchable", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 256 or usItemClass = 512", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Launcher", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 16", , , "szLongItemName"))

        AddConstraint(t, New String() {"launchableIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeIncompatibleAttachmentTable() As DataTable
        Dim t As New DataTable("INCOMPATIBLEATTACHMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\IncompatibleAttachments.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "DuplicateEntryTable")

        t.Columns.Add(MakeColumn("itemIndex", "Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("incompatibleattachmentIndex", "Incompatible Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))

        AddConstraint(t, New String() {"itemIndex", "incompatibleattachmentIndex"}, True)

        Return t
    End Function

    Private Function MakeExplosionDataTable() As DataTable
        Dim t As New DataTable("EXPDATA")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\ExplosionData.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("TransKeyFrame", "Trans Key Frame", GetType(Integer)))
        t.Columns.Add(MakeColumn("DamageKeyFrame", "Damage Key Frame", GetType(Integer)))
        t.Columns.Add(MakeColumn("ExplosionSoundID", "Sound", GetType(Integer), , "SOUND", "id", "name"))
        t.Columns.Add(MakeColumn("AltExplosionSoundID", "Alt. Sound", GetType(Integer), , "SOUND", "id", "name"))
        t.Columns.Add(MakeColumn("BlastFilename", "Blast File", GetType(String)))
        t.Columns.Add(MakeColumn("BlastSpeed", "Blast Speed", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeExplosiveTable() As DataTable
        Dim t As New DataTable("EXPLOSIVE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Explosives.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubType", "Type", GetType(Integer), , "ExplosionType", "id", "name"))
        t.Columns.Add(MakeColumn("ubDamage", "Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubStunDamage", "Stun Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubRadius", "Potential Radius", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubVolume", "Volume", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubVolatility", "Volatility", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubAnimationID", "Animation", GetType(Integer), , "EXPDATA", "uiIndex", "name"))
        t.Columns.Add(MakeColumn("ubDuration", "Duration", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubStartRadius", "Initial Radius", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMagSize", "Magazine Size", GetType(Integer))) 'NAS
        t.Columns.Add(MakeColumn("fExplodeOnImpact", "Impact Explosive", GetType(Boolean))) 'Impact Explosive
        t.Columns.Add(MakeColumn("usNumFragments", "Fragments", GetType(Integer)))          'Number of Fragments
        t.Columns.Add(MakeColumn("ubFragType", "Fragment Type", GetType(Integer), , "AMMOTYPE", "uiIndex", "Name", , , , , "Name"))          'Fragment Type
        t.Columns.Add(MakeColumn("ubFragDamage", "Fragment Damage", GetType(Integer)))      'Fragment Damage
        t.Columns.Add(MakeColumn("ubFragRange", "Fragment Range", GetType(Integer)))        'Fragment Range
        t.Columns.Add(MakeColumn("ubHorizontalDegree", "Horizontal Degrees", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubVerticalDegree", "Vertical Degrees", GetType(Integer)))
        t.Columns.Add(MakeColumn("bIndoorModifier", "Indoor Modifier", GetType(Decimal)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeCompatibleFaceItemTable() As DataTable
        Dim t As New DataTable("COMPATIBLEFACEITEM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\CompatibleFaceItems.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "DuplicateEntryTable")

        t.Columns.Add(MakeColumn("compatiblefaceitemIndex", "Face Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 32768", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Face Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 32768", , , "szLongItemName"))

        AddConstraint(t, New String() {"compatiblefaceitemIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeAttachmentInfoTable() As DataTable
        Dim t As New DataTable("ATTACHMENTINFO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\AttachmentInfo.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "AutoIncrementTable")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , False))
        t.Columns.Add(MakeColumn("usItem", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("uiItemClass", "Attachable To", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("bAttachmentSkillCheck", "Skill", GetType(Integer), , "SkillCheckType", "id", "name"))
        t.Columns.Add(MakeColumn("bAttachmentSkillCheckMod", "Modifier", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        AddConstraint(t, New String() {"usItem", "uiItemClass"}, False)

        Return t
    End Function

    Private Function MakeAttachmentComboMergeTable() As DataTable
        Dim t As New DataTable("ATTACHMENTCOMBOMERGE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\AttachmentComboMerges.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("usItem", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        For i As Integer = 1 To 20
            t.Columns.Add(MakeColumn("usAttachment" & i, "Attachment " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass<>1", , , "szLongItemName"))
        Next
        t.Columns.Add(MakeColumn("usResult", "Resulting Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Dim s(20) As String
        s(0) = "usItem"
        For i As Integer = 1 To 20
            s(i) = "usAttachment" & i
        Next

        AddConstraint(t, s, False)

        Return t
    End Function

    Private Function MakeAttachmentTable() As DataTable
        Dim t As New DataTable("ATTACHMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Attachments.xml")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("attachmentIndex", "Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("APCost", "AP Cost", GetType(Integer), 20))
        t.Columns.Add(MakeColumn("NASOnly", "NAS Only", GetType(Boolean)))

        AddConstraint(t, New String() {"attachmentIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeArmourTable() As DataTable
        Dim t As New DataTable("ARMOUR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Armours.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubArmourClass", "Type", GetType(Integer), , "ArmourClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubProtection", "Protection", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubCoverage", "Coverage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDegradePercent", "Degradation", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAmmoTypesTable() As DataTable
        Dim t As New DataTable("AMMOTYPE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\AmmoTypes.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("fontColour", "Font Colour", GetType(Integer)))
        t.Columns.Add(MakeColumn("grayed", "Grayed Colour", GetType(Integer)))
        t.Columns.Add(MakeColumn("offNormal", "Off Image", GetType(Integer)))
        t.Columns.Add(MakeColumn("onNormal", "On Image", GetType(Integer)))
        t.Columns.Add(MakeColumn("structureImpactReductionMultiplier", "Structure Impact Reduction Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("structureImpactReductionDivisor", "Structure Impact Reduction Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("armourImpactReductionMultiplier", "Armour Impact Reduction Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("armourImpactReductionDivisor", "Armour Impact Reduction Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("beforeArmourDamageMultiplier", "Before Armour Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("beforeArmourDamageDivisor", "Before Armour Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("afterArmourDamageMultiplier", "After Armour Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("afterArmourDamageDivisor", "After Armour Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("zeroMinimumDamage", "Zero Min. Damage", GetType(Boolean)))
        t.Columns.Add(MakeColumn("canGoThrough", "Can Go Through", GetType(Boolean)))
        t.Columns.Add(MakeColumn("standardIssue", "Std. Issue", GetType(Boolean)))
        t.Columns.Add(MakeColumn("numberOfBullets", "# Bullets", GetType(Integer)))
        t.Columns.Add(MakeColumn("multipleBulletDamageMultiplier", "Multiple Bullet Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("multipleBulletDamageDivisor", "Multiple Bullet Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("highExplosive", "High Explosive", GetType(Integer), , "ITEMTOEXPLOSIVE", "uiIndex", "szItemName", , , , , "szItemName"))
        t.Columns.Add(MakeColumn("explosionSize", "Explosion Size", GetType(Integer), , "ExplosionSize", "id", "name", , , , , "id"))
        t.Columns.Add(MakeColumn("antiTank", "Anti-Tank", GetType(Boolean)))
        t.Columns.Add(MakeColumn("dart", "Dart", GetType(Boolean)))
        t.Columns.Add(MakeColumn("knife", "Knife", GetType(Boolean)))
        t.Columns.Add(MakeColumn("monsterSpit", "Spit", GetType(Boolean)))
        t.Columns.Add(MakeColumn("acidic", "Acidic", GetType(Boolean)))
        t.Columns.Add(MakeColumn("ignoreArmour", "Ignore Armour", GetType(Boolean)))
        t.Columns.Add(MakeColumn("lockBustingPower", "Lock Buster Power", GetType(Integer)))
        t.Columns.Add(MakeColumn("tracerEffect", "Tracer Effect", GetType(Boolean)))
        t.Columns.Add(MakeColumn("spreadPattern", "Spread Pattern", GetType(String)))
        t.Columns.Add(MakeColumn("temperatureModificator", "Temperature Modifier", GetType(Decimal), 0))
        t.Columns.Add(MakeColumn("PoisonPercentage", "Poison Power", GetType(Integer), 0))
        t.Columns.Add(MakeColumn("dirtModificator", "Dirt Modifier", GetType(Decimal)))
        t.Columns.Add(MakeColumn("ammoflag", "Flag(s)", GetType(ULong), , "AmmoFlag", "id", "name", , , , , "name"))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAmmoStringsTable() As DataTable
        Dim t As New DataTable("AMMO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\AmmoStrings.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "AmmoCaliber")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("AmmoCaliber", "Caliber", GetType(String), , , , , , , , 20))
        t.Columns.Add(MakeColumn("BRCaliber", "BR Caliber", GetType(String), , , , , , , , 20))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLocalizedAmmoStringsTable(ByVal language As String) As DataTable
        Dim t As New DataTable(language & "Ammo")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\" & language & ".AmmoStrings.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "AMMO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "AMMOLIST")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "AmmoCaliber")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("AmmoCaliber", "Caliber", GetType(String), , , , , , , , 20))
        t.Columns.Add(MakeColumn("BRCaliber", "BR Caliber", GetType(String), , , , , , , , 20))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeWeaponsTable() As DataTable
        Dim t As New DataTable("WEAPON")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Weapons.xml")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("szWeaponName", "Name", GetType(String), , , , , , , True))
        t.Columns.Add(MakeColumn("ubWeaponClass", "Class", GetType(Integer), , "WeaponClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubWeaponType", "Type", GetType(Integer), , "WeaponType", "id", "name"))
        t.Columns.Add(MakeColumn("ubCalibre", "Caliber", GetType(Integer), , "AMMO", "uiIndex", "AmmoCaliber"))
        t.Columns.Add(MakeColumn("ubReadyTime", "Ready APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubShotsPer4Turns", "Shots Per 4 Turns", GetType(Decimal), , , , , , , , , , "If 2 values result in the same AP value, then the lower value will result in the desired AP value more often."))
        t.Columns.Add(MakeColumn("ubShotsPerBurst", "Shots Per Burst", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubBurstPenalty", "Burst Penalty", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubBulletSpeed", "Bullet Speed", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubImpact", "Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDeadliness", "Deadliness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bAccuracy", "Accuracy", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMagSize", "Magazine Size", GetType(Integer)))
        t.Columns.Add(MakeColumn("usRange", "Range", GetType(Integer)))
        t.Columns.Add(MakeColumn("usReloadDelay", "Reload Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstAniDelay", "Burst Animation Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubAttackVolume", "Volume", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubHitVolume", "Hit Volume", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("sSound", "Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sBurstSound", "Burst Sound", GetType(Integer), , "BURSTSOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sSilencedBurstSound", "Silenced Burst Sound", GetType(Integer), , "BURSTSOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sReloadSound", "Reload Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sLocknLoadSound", "Lock N Load Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("SilencedSound", "Silienced Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("bBurstAP", "Burst APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("bAutofireShotsPerFiveAP", "Autofire Shots per 5 APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("APsToReload", "APs to Reload", GetType(Integer)))
        t.Columns.Add(MakeColumn("SwapClips", "Swap Clips", GetType(Boolean)))
        t.Columns.Add(MakeColumn("MaxDistForMessyDeath", "Max Dist for Messy Death", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AutoPenalty", "Auto Penalty", GetType(Integer)))
        t.Columns.Add(MakeColumn("NoSemiAuto", "No Semi Auto", GetType(Boolean)))
        t.Columns.Add(MakeColumn("EasyUnjam", "Easy UnJam", GetType(Boolean)))
        t.Columns.Add(MakeColumn("APsToReloadManually", "Manual Reload APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("ManualReloadSound", "Manual Reload Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("nAccuracy", "NCTH Accuracy", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bRecoilX", "Recoil X", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("bRecoilY", "Recoil Y", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("ubAimLevels", "Default Aim Levles", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubRecoilDelay", "Recoil Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Handling", "Weapon Handling", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("usOverheatingJamThreshold", "Overheating Jam Threshold", GetType(Decimal), 4000))
        t.Columns.Add(MakeColumn("usOverheatingDamageThreshold", "Overheating Damage Threshold", GetType(Decimal), 5000))
        t.Columns.Add(MakeColumn("usOverheatingSingleShotTemperature", "Overheating Single Shot Temperature ", GetType(Decimal), 80))
        t.Columns.Add(MakeColumn("HeavyWeapon", "Heavy Weapon", GetType(Boolean)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeItemsTable() As DataTable
        Dim t As New DataTable("ITEM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Items.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "ItemTable")
        t.ExtendedProperties.Add(TableProperty.Trim, True)
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "szLongItemName")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))

        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , True, 80))
        t.Columns.Add(MakeColumn("szLongItemName", "Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szItemDesc", "Description", GetType(String), , , , , , , True, 400))
        t.Columns.Add(MakeColumn("szBRName", "BR Name", GetType(String), , , , , , , True, 80))
        t.Columns.Add(MakeColumn("szBRDesc", "BR Desc", GetType(String), , , , , , , True, 400))
        t.Columns.Add(MakeColumn("usItemClass", "Class", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("AttachmentClass", "Built-in Att. Class", GetType(ULong), , "AttachmentClass", "id", "name", , , True, , "name"))
        t.Columns.Add(MakeColumn("nasAttachmentClass", "Custom Att. Class", GetType(ULong), , "NasAttachmentClass", "id", "name", , , True, , "name")) 'NAS
        t.Columns.Add(MakeColumn("nasLayoutClass", "Layout Class", GetType(ULong), , , , , , , True)) 'NAS
        For i As Integer = 0 To 9
            t.Columns.Add(MakeColumn("AvailableAttachmentPoint" & i, "Avail. Att. Point " & i + 1, GetType(ULong), , "AttachmentPoint", "id", "name", , , True, , "name", , , , "AvailableAttachmentPoint"))
        Next
        't.Columns.Add(MakeColumn("AvailableAttachmentPoint", "Avail. Att. Point", GetType(ULong), , "AttachmentPoint", "id", "name", , , True, , "name"))
        t.Columns.Add(MakeColumn("AttachmentPoint", "Att. Point", GetType(ULong), , "AttachmentPoint", "id", "name", , , True, , "name"))
        t.Columns.Add(MakeColumn("AttachToPointAPCost", "Att. AP Cost", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubClassIndex", "Foreign Key", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ItemFlag", "Flag(s)", GetType(Integer), , "ItemFlag", "id", "name", , , True))
        t.Columns.Add(MakeColumn("ubCursor", "Cursor", GetType(Integer), , "Cursor", "id", "name", , , True))
        t.Columns.Add(MakeColumn("bSoundType", "Sound Type", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("ubGraphicType", "Graphic Type", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubGraphicNum", "Graphic #", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubWeight", "Weight", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubPerPocket", "# per Pocket", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ItemSize", "Size", GetType(Integer))) 'TODO: limit to 0-34 and 99
        t.Columns.Add(MakeColumn("ItemSizeBonus", "Size Adjustment", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("usPrice", "Price", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubCoolness", "Coolness", GetType(Integer)))
        t.Columns.Add(MakeColumn("bReliability", "Reliability", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bRepairEase", "Repair Ease", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Damageable", "Damageable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Repairable", "Repairable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("WaterDamages", "Water Damages", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Metal", "Metal", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Sinks", "Sinks", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ShowStatus", "Show Status", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("HiddenAddon", "Hidden Addon", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("TwoHanded", "Two Handed", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("NotBuyable", "Not Buyable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Attachment", "Attachment", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("HiddenAttachment", "Hidden Attachment", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BlockIronSight", "Block Iron Sight", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BigGunList", "Tons of Guns Mode", GetType(Boolean)))
        t.Columns.Add(MakeColumn("SciFi", "Sci-Fi", GetType(Boolean)))
        t.Columns.Add(MakeColumn("NotInEditor", "Not In Editor", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("DefaultUndroppable", "Undroppable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Unaerodynamic", "Unaerodynamic", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Electronic", "Electronic", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Inseparable", "Inseparable", GetType(Integer), , "Separability", "id", "name", , , True))
        t.Columns.Add(MakeColumn("BR_NewInventory", "BR New Inventory", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BR_UsedInventory", "BR Used Inventory", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BR_ROF", "BR Rate of Fire", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentNoiseReduction", "% Noise Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("HideMuzzleFlash", "Hide Muzzle Flash", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Bipod", "Bipod", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RangeBonus", "Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentRangeBonus", "Percent Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ToHitBonus", "To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BestLaserRange", "Best Laser Range", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AimBonus", "Aim Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MinRangeForAimBonus", "Min. Range For Aim Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MagSizeBonus", "Mag. Size Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RateOfFireBonus", "Rate of Fire Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BulletSpeedBonus", "Bullet Speed Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstSizeBonus", "Burst Size Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstToHitBonus", "Burst To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AutoFireToHitBonus", "Autofire To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("APBonus", "Bonus APs", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentBurstFireAPReduction", "% Burst Fire AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAutofireAPReduction", "% Autofire AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentReadyTimeAPReduction", "% Ready AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentReloadTimeAPReduction", "% Reload AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAPReduction", "% AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentStatusDrainReduction", "% Status Drain Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DamageBonus", "Damage Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MeleeDamageBonus", "Melee Damage Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("GrenadeLauncher", "Grenade Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Duckbill", "Duckbill", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GLGrenade", "GL Grenade", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Mine", "Mine", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Mortar", "Mortar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RocketLauncher", "Rocket Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("SingleShotRocketLauncher", "Single-Shot Rocket Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("DiscardedLauncherItem", "Discarded Launcher Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex=0 OR usItemClass=268435456", True, , "szLongItemName"))
        t.Columns.Add(MakeColumn("RocketRifle", "Rocket Rifle", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Cannon", "Cannon", GetType(Boolean), , , , , , , True))
        For i As Integer = 0 To 9
            t.Columns.Add(MakeColumn("DefaultAttachment" & i, "Default Attachment " & i + 1, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex=0 OR (Attachment=1 AND usItemClass <> 1)", True, , "szLongItemName", , , , "DefaultAttachment"))
        Next
        t.Columns.Add(MakeColumn("BrassKnuckles", "Brass Knuckles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Crowbar", "Crowbar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BloodiedItem", "Bloodied Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 8 OR uiIndex=0", True, , "szLongItemName"))
        t.Columns.Add(MakeColumn("Rock", "Rock", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("CamoBonus", "Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("UrbanCamoBonus", "Urban Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DesertCamoBonus", "Desert Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("SnowCamoBonus", "Snow Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("StealthBonus", "Stealth Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("FlakJacket", "Flak Jacket", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LeatherJacket", "Leather Jacket", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Directional", "Directional", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RemoteTrigger", "Remote Trigger", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LockBomb", "LockBomb", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Flare", "Flare", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RobotRemoteControl", "Robot Remote Control", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Walkman", "Walkman", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("HearingRangeBonus", "Hearing Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("VisionRangeBonus", "Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("NightVisionRangeBonus", "Night Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DayVisionRangeBonus", "Day Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("CaveVisionRangeBonus", "Cave Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BrightLightVisionRangeBonus", "Bright Light Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentTunnelVision", "Percent Tunnel Vision", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("FlashLightRange", "Flash Light Range", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ThermalOptics", "Thermal Optics", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GasMask", "Gas Mask", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Alcohol", "Alcohol", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Hardware", "Hardware", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Medical", "Medical", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("DrugType", "Drug Type", GetType(ULong), , "DrugType", "id", "name", , , True))
        t.Columns.Add(MakeColumn("CamouflageKit", "Camouflage Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LocksmithKit", "Locksmith Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Toolkit", "Toolkit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("FirstAidKit", "First Aid Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("MedicalKit", "Medical Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("WireCutters", "Wire Cutters", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Canteen", "Canteen", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GasCan", "Gas Can", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Marbles", "Marbles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("CanAndString", "Can And String", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Jar", "Jar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("XRay", "XRay", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Batteries", "Batteries", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("NeedsBatteries", "Needs Batteries", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ContainsLiquid", "Contains Liquid", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("MetalDetector", "Metal Detector", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("usSpotting", "Spotting Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("FingerPrintID", "Finger Print ID", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("TripWireActivation", "Trip Wire Activator", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("TripWire", "Trip Wire", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("NewInv", "New Inventory Only", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("AttachmentSystem", "Attachment System", GetType(Integer), , "AttachmentSystem", "id", "name", , , True)) 'NAS
        t.Columns.Add(MakeColumn("ScopeMagFactor", "Scope Magnification Factor", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("ProjectionFactor", "Laser Projection Factor", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("RecoilModifierX", "Recoil X Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("RecoilModifierY", "Recoil Y Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentRecoilModifier", "Recoil Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAccuracyModifier", "Accuracy Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("spreadPattern", "Spread Pattern", GetType(String), , , , , , , True))
        t.Columns.Add(MakeColumn("barrel", "Barrel", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("usOverheatingCooldownFactor", "Overheating Cooldown Factor", GetType(Decimal), 100, , , , , , True))
        t.Columns.Add(MakeColumn("overheatTemperatureModificator", "Overheating Temperature Modifier", GetType(Decimal), 0, , , , , , True))
        t.Columns.Add(MakeColumn("overheatCooldownModificator", "Overheating Cooldown Modifier", GetType(Decimal), 0, , , , , , True))
        t.Columns.Add(MakeColumn("overheatJamThresholdModificator", "Overheating Jam Threshold Modifier", GetType(Decimal), 0, , , , , , True))
        t.Columns.Add(MakeColumn("overheatDamageThresholdModificator", "Overheating Damage Threshold Modifier", GetType(Decimal), 0, , , , , , True))
        t.Columns.Add(MakeColumn("PoisonPercentage", "Poison Power", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("FoodType", "Food Type", GetType(Integer), 0, "Food", "uiIndex", "szName", , , True))
        t.Columns.Add(MakeColumn("LockPickModifier", "Lockpicking Modifier", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("CrowbarModifier", "Crowbar Bonus", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("DisarmModifier", "Disarm Bonus", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("RepairModifier", "Repair Kit Effectiveness Modifier", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("DamageChance", "Dirt Damage Chance", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DirtIncreaseFactor", "Dirt Increase Factor", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("clothestype", "Clothes Type", GetType(Integer), 0, "Clothes", "uiIndex", "szName", , , True, , "szName"))
        t.Columns.Add(MakeColumn("usActionItemFlag", "Action Item Flag", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("randomitem", "Random Item", GetType(Integer), , "RANDOMITEM", "uiIndex", "szName", , , True, , "szName"))
        t.Columns.Add(MakeColumn("randomitemcoolnessmodificator", "Random Item Coolness Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ItemChoiceTimeSetting", "Item Time Choice", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("buddyitem", "Buddy Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex=0", True, , "szLongItemName"))
        t.Columns.Add(MakeColumn("SleepModifier", "Sleep Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("sBackpackWeightModifier", "Backpack Weight Modifier", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("fAllowClimbing", "Allow Climbing", GetType(Boolean), 0, , , , , , True))
        'JMich: Attempting to add unknown tag support, 20 should be enough
        For i As Integer = 0 To 20
            t.Columns.Add(MakeColumn("ItemUnknownTag" & i, "ItemUnknownTag" & i, GetType(String), , , , , , , True))
        Next

        'CHRISL: Add new tags above this point.  Do not have new tags in the above section end in 1, 2 or 3 or the xml will not load correctly
        '   Only add new tags below this point if the tags are in the STAND_MODIFIERS, CROUCH_MODIFIERS and PRONE_MODIFIERS sections
        'Start STAND_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase1", "Standing Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase1", "Standing Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim1", "Standing Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap1", "Standing Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling1", "Standing Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed1", "Standing Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation1", "Standing Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce1", "Standing CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy1", "Standing CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency1", "Standing CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels1", "Standing Aim Bonus", GetType(Integer), -101, , , , , , True))
        'Start CROUCH_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase2", "Crouching Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase2", "Crouching Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim2", "Crouching Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap2", "Crouching Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling2", "Crouching Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed2", "Crouching Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation2", "Crouching Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce2", "Crouching CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy2", "Crouching CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency2", "Crouching CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels2", "Crouching Aim Bonus", GetType(Integer), -101, , , , , , True))
        'Start PRONE_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase3", "Prone Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase3", "Prone Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim3", "Prone Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap3", "Prone Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling3", "Prone Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed3", "Prone Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation3", "Prone Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce3", "Prone CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy3", "Prone CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency3", "Prone CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels3", "Prone Aim Bonus", GetType(Integer), -101, , , , , , True))


        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLocalizedItemsTable(ByVal language As String) As DataTable
        Dim t As New DataTable(language & "Item")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ITEMLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\" & language & ".Items.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "ITEM")
        t.ExtendedProperties.Add(TableProperty.Trim, True)
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "szLongItemName")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))

        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szLongItemName", "Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szItemDesc", "Description", GetType(String), , , , , , , , 400))
        t.Columns.Add(MakeColumn("szBRName", "BR Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szBRDesc", "BR Desc", GetType(String), , , , , , , , 400))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeSoundsTable() As DataTable
        Dim t As New DataTable("SOUND")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Sounds\Sounds.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "SoundTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeBurstSoundsTable() As DataTable
        Dim t As New DataTable("BURSTSOUND")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "SOUNDLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Sounds\BurstSounds.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "SOUND")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "SoundTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeShopKeeperInventoryTable(ByVal shopKeeperName As String) As DataTable
        Dim t As New DataTable(shopKeeperName & "Inventory")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "INVENTORYLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "NPCInventory\" & shopKeeperName & "Inventory.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "INVENTORY")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "InventoryTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "sItemIndex")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("sItemIndex", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("ubOptimalNumber", "Amount", GetType(Integer), 0))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeShopKeeperControlTable(ByVal shopKeeperName As String) As DataTable
        Dim t As New DataTable(shopKeeperName & "Control")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "ControlTable")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "INVENTORYLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "NPCInventory\" & shopKeeperName & "Inventory.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "CONTROL")

        t.Columns.Add(MakeColumn("ARMSDEALERINDEX", "AD Index", GetType(Integer)))
        t.Columns.Add(MakeColumn("SHOPKEEPERID", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("BUYCOSTMODIFIER", "Buy Cost Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("SELLCOSTMODIFIER", "Sell Cost Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("REPAIRSPEED", "Repair Speed", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("REPAIRCOST", "Repair Cost", GetType(Decimal), , , , , , , True))
        'REORDERDAYSDELAY
        t.Columns.Add(MakeColumn("REORDERMINIMUM", "Minimum Reorder", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("REORDERMAXIMUM", "Maximum Reorder", GetType(Integer), , , , , , , True))
        'CASH
        t.Columns.Add(MakeColumn("INITIAL", "Initial Cash", GetType(Integer), , , , , , , True))
        'DAILY
        t.Columns.Add(MakeColumn("INCREMENT", "Cash Increment", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("CASHMAXIMUM", "Maximum Cash", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RETAINED", "Cash Retained", GetType(Integer), , , , , , , True))
        'COOLNESS
        t.Columns.Add(MakeColumn("COOLMINIMUM", "Minimum Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("COOLMAXIMUM", "Maximum Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("COOLADD", "Add Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PROGRESSRATE", "Coolness Progress", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("USEBOBBYRAYSETTING", "Use BR Settings", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ALLINVENTORYALWAYSAVAILBLE", "All Inventory", GetType(Boolean), , , , , , , True))
        'BASICDEALERFLAGS
        t.Columns.Add(MakeColumn("ARMS_DEALER_HANDGUNCLASS", "Handguns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SMGCLASS", "SMGs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_RIFLECLASS", "Rifles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MGCLASS", "MGs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SHOTGUNCLASS", "Shotguns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_KNIFECLASS", "Knives", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BLADE", "Blades", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_LAUNCHER", "Launchers", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ARMOUR", "Armor", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MEDKIT", "Medkits", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MISC", "Misc", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_AMMO", "Ammo", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_GRENADE", "Grenades", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BOMB", "Bombs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_EXPLOSV", "Explosives", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_KIT", "Kits", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_FACE", "Face Gear", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_DETONATORS", "Detonators", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ATTACHMENTS", "Attachments", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALCOHOL", "Alcohol", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ELECTRONICS", "Electronics", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_HARDWARE", "Hardware", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MEDICAL", "Medical", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_CREATURE_PARTS", "Creature Parts", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ROCKET_RIFLE", "Rocket Rifles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ONLY_USED_ITEMS", "Only Used Items", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_GIVES_CHANGE", "Gives Change", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ACCEPTS_GIFTS", "Accepts Gifts", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SOME_USED_ITEMS", "Used Items", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_HAS_NO_INVENTORY", "No Inventory", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALL_GUNS", "All Guns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BIG_GUNS", "Big Guns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALL_WEAPONS", "All Weapons", GetType(Boolean), , , , , , , True))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("ARMSDEALERINDEX")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeRelation(ByVal parentTable As DataTable, ByVal childTable As DataTable, ByVal parentKey As String, ByVal childKey As String, Optional ByVal cascadeUpdates As Boolean = True, Optional ByVal cascadeDeletes As Boolean = True) As DataRelation
        Dim parentCol As DataColumn = parentTable.Columns(parentKey)
        Dim childCol As DataColumn = childTable.Columns(childKey)
        Dim dr As New DataRelation(parentTable.TableName & childTable.TableName & "_" & parentCol.ColumnName & childCol.ColumnName, parentCol, childCol, True)

        'add cascading updates and deletes to the child table
        Dim fkc As New ForeignKeyConstraint(parentCol, childCol)
        fkc.AcceptRejectRule = AcceptRejectRule.None
        fkc.UpdateRule = Rule.None
        fkc.DeleteRule = Rule.SetDefault
        If cascadeUpdates Then fkc.UpdateRule = Rule.Cascade
        If cascadeDeletes Then fkc.DeleteRule = Rule.Cascade
        childTable.Constraints.Add(fkc)

        Return dr
    End Function

    Private Sub AddConstraint(ByVal t As DataTable, ByVal columnNames() As String, ByVal primaryKey As Boolean)
        Dim cols(columnNames.GetUpperBound(0)) As DataColumn
        For i As Integer = 0 To columnNames.GetUpperBound(0)
            cols(i) = t.Columns(columnNames(i))
        Next
        Dim uc As UniqueConstraint = New UniqueConstraint("unique_constraint", cols, primaryKey)
        t.Constraints.Add(uc)
    End Sub

    Private Sub AddLookupData(ByVal t As DataTable, ByVal id As Integer, ByVal name As String)
        Dim r As DataRow = t.NewRow
        r("id") = id
        r("name") = name
        t.Rows.Add(r)
    End Sub

    Private Function MakeIMPItemsTable() As DataTable
        Dim t As New DataTable("IMPITEMCHOICES")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\IMPItemChoices.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubNumItems", "# Items", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyGunsTable() As DataTable
        Dim t As New DataTable("ENEMYGUNCHOICESDEFAULT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyGunChoices.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyGunsTableAdmin() As DataTable
        Dim t As New DataTable("ENEMYGUNCHOICESADMIN")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Enemy_Admin.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyGunsTableRegular() As DataTable
        Dim t As New DataTable("ENEMYGUNCHOICESREGULAR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Enemy_Regular.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyGunsTableElite() As DataTable
        Dim t As New DataTable("ENEMYGUNCHOICESELITE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Enemy_Elite.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMilitiaGunsTableGreen() As DataTable
        Dim t As New DataTable("MILITIAGUNCHOICESGREEN")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Militia_Green.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMilitiaGunsTableRegular() As DataTable
        Dim t As New DataTable("MILITIAGUNCHOICESREGULAR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Militia_Regular.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMilitiaGunsTableElite() As DataTable
        Dim t As New DataTable("MILITIAGUNCHOICESELITE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYGUNCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\GunChoices_Militia_Elite.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyItemsTable() As DataTable
        Dim t As New DataTable("ENEMYITEMCHOICESDEFAULT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyItemChoices.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function
    Private Function MakeEnemyItemsTableAdmin() As DataTable
        Dim t As New DataTable("ENEMYITEMCHOICESADMIN")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Enemy_Admin.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyItemsTableRegular() As DataTable
        Dim t As New DataTable("ENEMYITEMCHOICESREGULAR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Enemy_Regular.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function
    Private Function MakeEnemyItemsTableElite() As DataTable
        Dim t As New DataTable("ENEMYITEMCHOICESELITE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Enemy_Elite.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMilitiaItemsTableGreen() As DataTable
        Dim t As New DataTable("MILITIAITEMCHOICESGREEN")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Militia_Green.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMilitiaItemsTableRegular() As DataTable
        Dim t As New DataTable("MILITIAITEMCHOICESREGULAR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Militia_Regular.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function
    Private Function MakeMilitiaItemsTableElite() As DataTable
        Dim t As New DataTable("MILITIAITEMCHOICESELITE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ENEMYITEMCHOICESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\ItemChoices_Militia_Elite.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyAmmoTable() As DataTable
        Dim t As New DataTable("ENEMYAMMOCHOICES")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "name")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , "AmmoChoices", "id", "name"))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Ammo Type " & i, GetType(Integer), 0, "AMMOTYPE", "uiIndex", "name"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyAmmoDropTable() As DataTable
        Dim t As New DataTable("EnemyAmmoDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "AMMODROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyAmmoDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("uiType", "Type", GetType(Integer), , "AMMOTYPE", "uiIndex", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyArmourDropTable() As DataTable
        Dim t As New DataTable("EnemyArmourDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ARMOURDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyArmourDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "ubArmourClass")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubArmourClass", "Type", GetType(Integer), , "ArmourClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyExplosiveDropTable() As DataTable
        Dim t As New DataTable("EnemyExplosiveDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "EXPLOSIVEDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyExplosiveDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "ubType")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubType", "Type", GetType(Integer), , "ExplosionType", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyWeaponDropTable() As DataTable
        Dim t As New DataTable("EnemyWeaponDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "WEAPONDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyWeaponDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "ubWeaponType")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubWeaponType", "Type", GetType(Integer), , "WeaponType", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyMiscItemDropTable() As DataTable
        Dim t As New DataTable("EnemyMiscDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "MISCDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\EnemyMiscDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "usItemClass")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("usItemClass", "Type", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLoadBearingEquipmentTable() As DataTable
        Dim t As New DataTable("LOADBEARINGEQUIPMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\LoadBearingEquipment.xml")

        t.Columns.Add(MakeColumn("lbeIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("lbeClass", "Type", GetType(Integer), , "LBEClass", "id", "name"))
        t.Columns.Add(MakeColumn("lbeCombo", "Combo #", GetType(Integer)))
        t.Columns.Add(MakeColumn("lbeFilledSize", "Filled Size", GetType(Integer)))
        t.Columns.Add(MakeColumn("lbeAvailableVolume", "Available Volume", GetType(Integer), 0, , , , True))
        t.Columns.Add(MakeColumn("lbePocketsAvailable", "Pockets Available", GetType(Integer), 0, , , , True))
        For i As Integer = 1 To 12
            t.Columns.Add(MakeColumn("lbePocketIndex" & i, "Pocket #" & i, GetType(Integer), , "POCKET", "pIndex", "pName"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("lbeIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Sub ReadItemSizes(baseDir As String)
        Dim ja2() As String = System.IO.File.ReadAllLines(baseDir & "ja2_options.ini")
        For Each line As String In ja2
            Dim arr As String() = line.Split("=")
            If arr(0).Trim = "MAX_ITEM_SIZE" Then
                ItemSizeMax = arr(1)
                ItemSizesRead = True
            End If
        Next
        If Not ItemSizesRead Then ItemSizeMax = 99
    End Sub

    Private Function MakePocketsTable(baseDir As String) As DataTable
        Dim t As New DataTable("POCKET")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Pockets.xml")
        t.ExtendedProperties.Add(TableProperty.Trim, True)
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "pName")

        t.Columns.Add(MakeColumn("pIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("pName", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("pSilhouette", "Silhouette", GetType(Integer), , "Silhouette", "id", "name"))
        t.Columns.Add(MakeColumn("pType", "Type", GetType(Integer), , "PocketSize", "id", "name"))
        t.Columns.Add(MakeColumn("pRestriction", "Restriction", GetType(Integer), 0, "ItemClass", "id", "name", True))
        t.Columns.Add(MakeColumn("pVolume", "Pocket Volume", GetType(Integer)))

        If Not ItemSizesRead Then ReadItemSizes(baseDir)
        For i As Integer = 0 To ItemSizeMax
            If ItemSizeMax < 100 Then
                t.Columns.Add(MakeColumn("ItemCapacityPerSize" & i, "Size " & i, GetType(Integer), , , , , , , , , , "Capacity for Size " & i))
            Else
                t.Columns.Add(MakeColumn("ItemCapacityPerSize" & i, "Size " & i, GetType(Integer), , , , , , , True, , , "Capacity for Size " & i))
            End If
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("pIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMercStartingGearTable() As DataTable
        Dim t As New DataTable("MERCGEAR")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "MercStartingGearTable")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Inventory\MercStartingGear.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "mName")
        t.ExtendedProperties.Add(TableProperty.NoRowDuplicate, "NoRowDuplicate")

        t.Columns.Add(MakeColumn("mIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("mName", "Name", GetType(String)))
        For x As Integer = 1 To 5
            t.Columns.Add(MakeColumn("mPriceMod" & x, "Price Mod", GetType(Integer)))
            t.Columns.Add(MakeColumn("mGearKitName" & x, "Gearkit Name", GetType(String)))
            t.Columns.Add(MakeColumn("mAbsolutePrice" & x, "Absolute Price", GetType(Integer), -1))
            t.Columns.Add(MakeColumn("mHelmet" & x, "Helmet", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mHelmetDrop" & x, "Helmet Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mHelmetStatus" & x, "Helmet Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mVest" & x, "Vest", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mVestDrop" & x, "Vest Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mVestStatus" & x, "Vest Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mLeg" & x, "Legs", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mLegDrop" & x, "Legs Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mLegStatus" & x, "Legs Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mWeapon" & x, "Weapon", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
            t.Columns.Add(MakeColumn("mWeaponDrop" & x, "Weapon Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mWeaponStatus" & x, "Weapon Status", GetType(Integer), , , , , , , True))
            For i As Integer = 0 To 3
                t.Columns.Add(MakeColumn("mBig" & i & x, "Big Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Status" & x, "Big Item " & i & " Status", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Quantity" & x, "Big Item " & i & " Quantity", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Drop" & x, "Big Item " & i & " Droppable", GetType(Boolean), , , , , , , True))
            Next
            For i As Integer = 0 To 7
                t.Columns.Add(MakeColumn("mSmall" & i & x, "Small Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
                t.Columns.Add(MakeColumn("mSmall" & i & "Status" & x, "Small Item " & i & " Status", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mSmall" & i & "Quantity" & x, "Small Item " & i & " Quantity", GetType(Integer), , , , , , , True))
            Next
            t.Columns.Add(MakeColumn("lVest" & x, "LBE Vest", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lVestStatus" & x, "LBE Vest Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lLeftThigh" & x, "LBE Left Thigh", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lLeftThighStatus" & x, "LBE Left Thigh Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lRightThigh" & x, "LBE Right Thigh", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lRightThighStatus" & x, "LBE Right Thigh Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lCPack" & x, "LBE Combat Pack", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lCPackStatus" & x, "LBE Combat Pack Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lBPack" & x, "LBE Backpack", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lBPackStatus" & x, "LBE Backpack Status", GetType(Integer), , , , , , , True))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("mIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAttachmentSlotsTable() As DataTable
        Dim t As New DataTable("ATTACHMENTSLOT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\AttachmentSlots.xml")
        t.ExtendedProperties.Add(TableProperty.ComparisonField, "szSlotName")

        t.Columns.Add(MakeColumn("uiSlotIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("szSlotName", "Name", GetType(String), , , , , , , , 200))
        t.Columns.Add(MakeColumn("nasAttachmentClass", "Custom Attachment Class", GetType(ULong), , "NasAttachmentClass", "id", "name")) 'NAS
        t.Columns.Add(MakeColumn("nasLayoutClass", "Layout Class", GetType(Decimal))) 'NAS
        t.Columns.Add(MakeColumn("usDescPanelPosX", "X Coord", GetType(Integer)))
        t.Columns.Add(MakeColumn("usDescPanelPosY", "Y Coord", GetType(Integer)))
        t.Columns.Add(MakeColumn("fMultiShot", "Multi-Shot", GetType(Boolean)))
        t.Columns.Add(MakeColumn("fBigSlot", "Big Slot", GetType(Boolean)))
        t.Columns.Add(MakeColumn("ubPocketMapping", "Pocket Mapping", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiSlotIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeITETable() As DataTable
        Dim t As New DataTable("ITEMTOEXPLOSIVE")

        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "IteTable")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , True, 80))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeTransformTable() As DataTable
        Dim t As New DataTable("TRANSFORM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "TRANSFORMATIONS_LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Item_Transformations.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "ItemTransformationTable")

        t.Columns.Add(MakeColumn("usItem", "Item to Transform", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usResult1", "1st Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usResult2", "2nd Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usResult3", "3rd Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        For x As Integer = 4 To 10
            t.Columns.Add(MakeColumn("usResult" & x, x & "th Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        Next
        t.Columns.Add(MakeColumn("usAPCost", "AP Cost", GetType(Integer)))
        t.Columns.Add(MakeColumn("iBPCost", "BP Cost", GetType(Integer)))
        t.Columns.Add(MakeColumn("szMenuRowText", "Menu Description", GetType(String), , , , , , , , 50))
        t.Columns.Add(MakeColumn("szTooltipText", "Tooltip", GetType(String), , , , , , , , 300))

        AddConstraint(t, New String() {"usItem", "usResult1", "usResult2", "usResult3", "usResult4", "usResult5", "usResult6", "usResult7", "usResult8", "usResult9", "usResult10"}, True)

        Return t
    End Function

    Private Function MakeDrugsTable() As DataTable
        Dim t As New DataTable("DRUG")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "DRUGSLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Drugs.xml")

        t.Columns.Add(MakeColumn("ubType", "Type", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("ubDrugTravelRate", "Travel Rate", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDrugWearoffRate", "Wear Off Rate", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDrugEffect", "Effect", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDrugSideEffect", "Side Effect", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDrugSideEffectRate", "Side Effect Rate", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMoralBacklash", "Morale Hit", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("ubType")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeFoodTable() As DataTable
        Dim t As New DataTable("FOOD")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "FOODSLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Food.xml")

        t.Columns.Add(MakeColumn("uiIndex", "Index", GetType(Integer)))
        t.Columns.Add(MakeColumn("szName", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("bFoodPoints", "Food Points", GetType(Integer)))
        t.Columns.Add(MakeColumn("bDrinkPoints", "Drink Points", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubPortionSize", "Portion Size", GetType(Integer), 100))
        t.Columns.Add(MakeColumn("bMoraleMod", "Morale Modifier", GetType(Integer)))
        t.Columns.Add(MakeColumn("usDecayRate", "Decay Rate", GetType(Decimal)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeClothesTable() As DataTable
        Dim t As New DataTable("CLOTHES")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "CLOTHESLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\Clothes.xml")

        t.Columns.Add(MakeColumn("uiIndex", "Index", GetType(Integer)))
        t.Columns.Add(MakeColumn("szName", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("Vest", "Vest Color", GetType(String)))
        t.Columns.Add(MakeColumn("Pants", "Pants Color", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeRandomTable() As DataTable
        Dim t As New DataTable("RANDOMITEM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "RANDOMITEMLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items\RandomItem.xml")

        t.Columns.Add(MakeColumn("uiIndex", "Random Item", GetType(Integer)))
        t.Columns.Add(MakeColumn("szName", "Random Category", GetType(String)))
        For i As Integer = 1 To 10
            t.Columns.Add(MakeColumn("randomitem" & i, "Random Item " & i, GetType(Integer), , "RANDOMITEM", "uiIndex", "szName", , , , , "szName"))
        Next
        For i As Integer = 1 To 10
            t.Columns.Add(MakeColumn("item" & i, "Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        Next
        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk
        Return t
    End Function
End Module
