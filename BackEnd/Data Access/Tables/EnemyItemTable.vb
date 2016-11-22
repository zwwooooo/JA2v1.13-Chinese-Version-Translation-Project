Public Class EnemyItemTable
    Inherits DefaultTable
    Const AmmoIndex As Integer = 19 'the key of the ammo row
    Protected ammoTable As DataTable 'need a 2nd table to store the ammo data

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    Protected Sub AfterLoadAll(sender As DataManager) Handles _dm.AfterLoadData, _dm.AfterSaveData, _dm.AfterLoadWorkingData, _dm.AfterSaveWorkingData
        ammoTable = _table.DataSet.Tables(Tables.EnemyAmmo)
        Dim ammoRow As DataRow = _table.Rows.Find(AmmoIndex)
        If ammoRow IsNot Nothing Then
            Dim row As DataRow = ammoTable.NewRow
            row.ItemArray = ammoRow.ItemArray
            If _table.TableName() = "ENEMYITEMCHOICESDEFAULT" Then
                row("uiIndex") = 19
            ElseIf _table.TableName() = "ENEMYITEMCHOICESADMIN" Then
                row("uiIndex") = 29
            ElseIf _table.TableName() = "ENEMYITEMCHOICESREGULAR" Then
                row("uiIndex") = 39
            ElseIf _table.TableName() = "ENEMYITEMCHOICESELITE" Then
                row("uiIndex") = 49
            ElseIf _table.TableName() = "MILITIAITEMCHOICESGREEN" Then
                row("uiIndex") = 59
            ElseIf Table.TableName() = "MILITIAITEMCHOICESREGULAR" Then
                row("uiIndex") = 69
            ElseIf Table.TableName() = "MILITIAITEMCHOICESELITE" Then
                row("uiIndex") = 79
            End If
            ammoTable.Rows.Add(row)
            _table.Rows.Remove(ammoRow)
        End If
    End Sub

    Protected Sub BeforeSaveAll(sender As DataManager) Handles _dm.BeforeSaveData, _dm.BeforeSaveWorkingData
        Dim rowindex As Integer
        If ammoTable.Rows.Count > 0 Then
            Dim row As DataRow = _table.NewRow
            If _table.ExtendedProperties(TableProperty.FileName) = "ENEMYITEMCHOICES" Then
                rowindex = 19
            ElseIf _table.TableName() = "ENEMYITEMCHOICESADMIN" Then
                rowindex = 29
            ElseIf _table.TableName() = "ENEMYITEMCHOICESREGULAR" Then
                rowindex = 39
            ElseIf _table.TableName() = "ENEMYITEMCHOICESELITE" Then
                rowindex = 49
            ElseIf _table.TableName() = "MILITIAITEMCHOICESGREEN" Then
                rowindex = 59
            ElseIf Table.TableName() = "MILITIAITEMCHOICESREGULAR" Then
                rowindex = 69
            ElseIf Table.TableName() = "MILITIAITEMCHOICESELITE" Then
                rowindex = 79
            End If
            For i As Integer = 0 To ammoTable.Rows.Count
                If ammoTable.Rows(i).ItemArray.Contains(rowindex) Then
                    row.ItemArray = ammoTable.Rows(i).ItemArray
                    row("uiIndex") = 19
                    _table.Rows.Add(row)
                    ammoTable.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
            'row.ItemArray = ammoTable.Rows.Contains(rowindex)

            'row.ItemArray = ammoTable.Rows(rowindex).ItemArray
            'row("uiIndex") = 19
            '_table.Rows.Add(row)
            'ammoTable.Rows.RemoveAt(0)
        End If
    End Sub
End Class
