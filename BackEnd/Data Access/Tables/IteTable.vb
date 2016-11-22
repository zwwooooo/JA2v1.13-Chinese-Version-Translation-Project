Public Class IteTable
    Inherits DefaultTable

    Public Sub New(table As DataTable, manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    Protected Overrides Sub WriteXml(table As System.Data.DataTable, fileName As String)
        'Do nothing.  This is an internal table that we don't want to create an xml file for.
    End Sub

    Public Sub AfterLoadData(sender As DataManager) Handles _dm.AfterLoadWorkingData, _dm.AfterLoadData
        _table.Rows.Clear()

        'needed to make sure we have some good ubClassIndex values
        With DirectCast(_dm.Database.Table(Tables.Items.Name).GetTableHandler, ItemTable)
            .CopyToExtendedTable(_dm.Database.Table(Tables.Explosives.Name), Tables.Items.Fields.ItemClass & "=" & ItemClass.Grenade & " OR " & Tables.Items.Fields.ItemClass & "=" & ItemClass.Bomb, Tables.Items.Fields.ID)
        End With
        _dm.Database.Table(Tables.Explosives.Name).Clear()

        ReallyLoadData()
    End Sub

    Protected Sub ReallyLoadData()
        Dim itemTable As DataTable = _dm.Database.Table(Tables.Items.Name)

        For Each itemRow As DataRow In From r In itemTable.AsEnumerable Where r(Tables.Items.Fields.ItemClass) = ItemClass.Grenade Or r(Tables.Items.Fields.ItemClass) = ItemClass.Bomb Select r
            Dim row As DataRow = _table.NewRow
            row(Tables.Items.Fields.ID) = itemRow(Tables.Items.Fields.ID)
            If _table.Rows.Count = 0 Then
                row(Tables.Items.Fields.ShortName) = "None/" & itemRow(Tables.Items.Fields.ShortName)
            Else
                row(Tables.Items.Fields.ShortName) = itemRow(Tables.Items.Fields.ShortName)
            End If
            _table.Rows.Add(row)
        Next

    End Sub

    Public Overrides Sub LoadData()
        'do nothing!  don't load this table until afterwards!
    End Sub

End Class
