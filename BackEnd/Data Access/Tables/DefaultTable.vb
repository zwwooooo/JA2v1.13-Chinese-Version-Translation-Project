Public Class DefaultTable
    Implements IDisposable

    Protected _table As DataTable
    Protected WithEvents _dm As DataManager

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        _table = table
        _dm = manager
    End Sub

    Public ReadOnly Property Table() As DataTable
        Get
            Return _table
        End Get
    End Property

    Protected Function GetFilePath(fileName As String) As String
        Dim filePath As String = _dm.TableDirectory & fileName
        If Localization.IsLanguageSpecificFile(fileName) Then
            filePath = _dm.GetLanguageSpecificTableDirectory(fileName) & fileName
        End If

        Return filePath
    End Function

    Public Overridable Sub LoadData()
        Const Temp As String = "temp"

        Dim t As DataTable = Nothing
        _table.BeginLoadData()
        _table.Clear()
        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        Dim sourceName As String = _table.GetStringProperty(TableProperty.SourceTableName)
        If sourceName Is Nothing Then
            'JMich Adding support for different rank levels of equipment
            If _table.TableName().Contains("GUNCHOICES") Then
                _table.TableName = "ENEMYGUNCHOICES"
            ElseIf _table.TableName().Contains("ITEMCHOICES") And _table.TableName() <> "IMPITEMCHOICES" Then
                _table.TableName() = "ENEMYITEMCHOICES"
            End If
            _table.ReadXml(filePath)
            If fileName = "Inventory\EnemyGunChoices.xml" Then
                _table.TableName = "ENEMYGUNCHOICESDEFAULT"
            ElseIf fileName = "Inventory\GunChoices_Enemy_Admin.xml" Then
                _table.TableName = "ENEMYGUNCHOICESADMIN"
            ElseIf fileName = "Inventory\GunChoices_Enemy_Regular.xml" Then
                _table.TableName = "ENEMYGUNCHOICESREGULAR"
            ElseIf fileName = "Inventory\GunChoices_Enemy_Elite.xml" Then
                _table.TableName = "ENEMYGUNCHOICESELITE"
            ElseIf fileName = "Inventory\GunChoices_Militia_Green.xml" Then
                _table.TableName = "MILITIAGUNCHOICESGREEN"
            ElseIf fileName = "Inventory\GunChoices_Militia_Regular.xml" Then
                _table.TableName = "MILITIAGUNCHOICESREGULAR"
            ElseIf fileName = "Inventory\GunChoices_Militia_Elite.xml" Then
                _table.TableName = "MILITIAGUNCHOICESELITE"
            ElseIf fileName = "Inventory\EnemyItemChoices.xml" Then
                _table.TableName = "ENEMYITEMCHOICESDEFAULT"
            ElseIf fileName = "Inventory\ItemChoices_Enemy_Admin.xml" Then
                _table.TableName = "ENEMYITEMCHOICESADMIN"
            ElseIf fileName = "Inventory\ItemChoices_Enemy_Regular.xml" Then
                _table.TableName = "ENEMYITEMCHOICESREGULAR"
            ElseIf fileName = "Inventory\ItemChoices_Enemy_Elite.xml" Then
                _table.TableName = "ENEMYITEMCHOICESELITE"
            ElseIf fileName = "Inventory\ItemChoices_Militia_Green.xml" Then
                _table.TableName = "MILITIAITEMCHOICESGREEN"
            ElseIf fileName = "Inventory\ItemChoices_Militia_Regular.xml" Then
                _table.TableName = "MILITIAITEMCHOICESREGULAR"
            ElseIf fileName = "Inventory\ItemChoices_Militia_Elite.xml" Then
                _table.TableName = "MILITIAITEMCHOICESELITE"
            End If
        Else
            Dim tableName As String = _table.TableName
            For Each t In _table.DataSet.Tables
                If t.TableName = sourceName Then
                    t.TableName = Temp
                    Exit For
                End If
            Next

            _table.TableName = sourceName
            _table.ReadXml(filePath)
            _table.TableName = tableName
            If t IsNot Nothing AndAlso t.TableName = Temp Then t.TableName = sourceName
        End If
        _table.EndLoadData()
    End Sub

    Public Overridable Sub ClearData()
        _table.Clear()
    End Sub

    Public Overridable Sub SaveData()
        'JMich Adding support for different rank levels of equipment
        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        If _table.TableName().Contains("GUNCHOICES") Then
            _table.TableName = "ENEMYGUNCHOICES"
        ElseIf _table.TableName().Contains("ITEMCHOICES") And _table.TableName() <> "IMPITEMCHOICES" Then
            _table.TableName() = "ENEMYITEMCHOICES"
        End If
        SaveData(_table)
        If fileName = "Inventory\EnemyGunChoices.xml" Then
            _table.TableName = "ENEMYGUNCHOICESDEFAULT"
        ElseIf fileName = "Inventory\GunChoices_Enemy_Admin.xml" Then
            _table.TableName = "ENEMYGUNCHOICESADMIN"
        ElseIf fileName = "Inventory\GunChoices_Enemy_Regular.xml" Then
            _table.TableName = "ENEMYGUNCHOICESREGULAR"
        ElseIf fileName = "Inventory\GunChoices_Enemy_Elite.xml" Then
            _table.TableName = "ENEMYGUNCHOICESELITE"
        ElseIf fileName = "Inventory\GunChoices_Militia_Green.xml" Then
            _table.TableName = "MILITIAGUNCHOICESGREEN"
        ElseIf fileName = "Inventory\GunChoices_Militia_Regular.xml" Then
            _table.TableName = "MILITIAGUNCHOICESREGULAR"
        ElseIf fileName = "Inventory\GunChoices_Militia_Elite.xml" Then
            _table.TableName = "MILITIAGUNCHOICESELITE"
        ElseIf fileName = "Inventory\EnemyItemChoices.xml" Then
            _table.TableName = "ENEMYITEMCHOICESDEFAULT"
        ElseIf fileName = "Inventory\ItemChoices_Enemy_Admin.xml" Then
            _table.TableName = "ENEMYITEMCHOICESADMIN"
        ElseIf fileName = "Inventory\ItemChoices_Enemy_Regular.xml" Then
            _table.TableName = "ENEMYITEMCHOICESREGULAR"
        ElseIf fileName = "Inventory\ItemChoices_Enemy_Elite.xml" Then
            _table.TableName = "ENEMYITEMCHOICESELITE"
        ElseIf fileName = "Inventory\ItemChoices_Militia_Green.xml" Then
            _table.TableName = "MILITIAITEMCHOICESGREEN"
        ElseIf fileName = "Inventory\ItemChoices_Militia_Regular.xml" Then
            _table.TableName = "MILITIAITEMCHOICESREGULAR"
        ElseIf fileName = "Inventory\ItemChoices_Militia_Elite.xml" Then
            _table.TableName = "MILITIAITEMCHOICESELITE"
        End If
    End Sub

    Protected Overridable Sub SaveData(ByVal table As DataTable)
        If Not table.ExtendedProperties.Contains(TableProperty.SourceTableName) Then

            Dim fileName = table.GetStringProperty(TableProperty.FileName)
            Dim filePath As String = GetFilePath(fileName)

            WriteXml(table, filePath)
        Else
            Dim t As DataTable = table.Copy
            t.TableName = table.ExtendedProperties(TableProperty.SourceTableName)

            Dim fileName As String = t.GetStringProperty(TableProperty.FileName)
            Dim filePath As String = GetFilePath(fileName)

            WriteXml(t, filePath)
            t.Dispose()
        End If
    End Sub

    'trim property = true results in all 0/blank values not being written to xml, except for the first entry, which is preserved for reference
    Protected Overridable Sub WriteXml(ByVal table As DataTable, ByVal fileName As String)
        'the stupid table.WriteXml method doesn't let you sort the data first
        Dim view As New DataView(table, "", table.Columns(0).ColumnName, DataViewRowState.CurrentRows)
        Dim trim As Boolean = table.GetBooleanProperty(TableProperty.Trim)
        Dim sourceDSName = table.GetStringProperty(TableProperty.DataSetName)
        If sourceDSName Is Nothing Then
            If table.DataSet IsNot Nothing Then
                sourceDSName = table.DataSet.DataSetName
            Else
                sourceDSName = _dm.Database.SchemaName
            End If
        End If

        Dim xw As New Xml.XmlTextWriter(fileName, Text.Encoding.UTF8)
        xw.WriteStartDocument()
        xw.WriteWhitespace(vbLf)
        xw.WriteStartElement(sourceDSName)
        xw.WriteWhitespace(vbLf)

        For i As Long = 0 To view.Count - 1
            xw.WriteString(vbTab)
            xw.WriteStartElement(table.TableName)
            xw.WriteString(vbLf)

            Dim dcIndex As Integer = -1

            For Each c As DataColumn In table.Columns
                dcIndex = dcIndex + 1
                Dim xmlColName As String = c.GetStringProperty(ColumnProperty.SourceColumnName)
                If String.IsNullOrEmpty(xmlColName) Then xmlColName = c.ColumnName

                If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> 0) _
                    OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then

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
            xw.WriteString(vbTab)
            xw.WriteEndElement()
            xw.WriteString(vbLf)
        Next
        xw.WriteEndElement()
        xw.Close()
        view.Dispose()

        table.AcceptChanges()
    End Sub

    'this just works on single pk tables for now
    Public Overridable Function GetNextPrimaryKeyValue() As Decimal
        Dim pk As String = _table.PrimaryKey(0).ColumnName
        If _table.Rows.Count > 0 Then
            Return _table.Compute("MAX(" & pk & ")", Nothing) + 1
        Else
            Return 0
        End If
    End Function

    'only works w/single key values
    Public Overridable Function NewRow() As DataRow
        Dim row As DataRow = _table.NewRow
        row(_table.PrimaryKey(0)) = GetNextPrimaryKeyValue()
        _table.Rows.Add(row)
        Return row
    End Function

    Public Overridable Sub DeleteRow(ByVal key As Decimal)
        Dim row As DataRow = _table.Rows.Find(key)
        If row IsNot Nothing Then row.Delete()
    End Sub

    'only works when there's a single key table at the top of the relation
    Public Overridable Function DuplicateRow(ByVal key As Decimal) As DataRow
        Dim row As DataRow = _table.Rows.Find(key)
        If row IsNot Nothing Then
            Return DuplicateRows(New DataRow() {row}, Nothing, key)
        Else
            Return Nothing
        End If
    End Function

    Protected Function DuplicateRows(ByVal rows() As DataRow, ByVal parentRow As DataRow, ByVal baseKey As Decimal) As DataRow
        Dim dupeRow As DataRow = Nothing
        Dim canAddRow As Boolean
        For Each row As DataRow In rows
            canAddRow = False
            dupeRow = row.Table.NewRow
            For Each c As DataColumn In row.Table.Columns
                If Not c.ReadOnly Then dupeRow(c) = row(c)
            Next

            If dupeRow.Table.PrimaryKey.Length = 1 Then
                dupeRow(dupeRow.Table.PrimaryKey(0)) = row.Table.GetTableHandler.GetNextPrimaryKeyValue()
                If parentRow IsNot Nothing Then dupeRow.SetParentRow(parentRow)
                canAddRow = True
            ElseIf parentRow IsNot Nothing Then
                For i As Integer = 0 To dupeRow.Table.PrimaryKey.Length - 1
                    If dupeRow(dupeRow.Table.PrimaryKey(i)) = baseKey Then
                        dupeRow(dupeRow.Table.PrimaryKey(i)) = parentRow(parentRow.Table.PrimaryKey(0))
                        canAddRow = True
                    End If
                Next
            End If
            If canAddRow Then
                Try
                    row.Table.Rows.Add(dupeRow)
                Catch ex As Exception

                End Try
            End If

            For Each dr As DataRelation In row.Table.ChildRelations
                If Not dr.ChildTable.ExtendedProperties.Contains(TableProperty.NoRowDuplicate) Then
                    DuplicateRows(row.GetChildRows(dr), dupeRow, baseKey)
                End If
            Next
        Next
        Return dupeRow 'return last row copied
    End Function

    'TODO:this may also only work for tables with a single primary key....
    Public Overridable Function CopyRows(ByVal rows As DataRow(), source As XmlDB, Optional allowOverwrite As Boolean = True) As DataRow
        Return CopyRows(rows, Nothing, 0, source, allowOverwrite)
    End Function

    Protected Enum RowCompareResult
        Skip
        Add
        Overwrite
    End Enum

    Protected Function CopyRows(ByVal rows As DataRow(), ByVal parentRow As DataRow, ByVal baseKey As Decimal, source As XmlDB, overWriteExistingRows As Boolean) As DataRow
        Dim dupeRow As DataRow = Nothing
        Dim canAddRow As Boolean
        Dim compareResult As RowCompareResult = RowCompareResult.Skip
        Dim destTable As DataTable
        For Each row As DataRow In rows
            canAddRow = False

            destTable = _dm.Database.Table(row.Table.TableName)
            dupeRow = destTable.NewRow

            For Each c As DataColumn In row.Table.Columns
                If destTable.Columns.Contains(c.ColumnName) AndAlso Not destTable.Columns(c.ColumnName).ReadOnly Then dupeRow(c.ColumnName) = row(c)
            Next

            Dim keys(destTable.PrimaryKey.GetUpperBound(0)) As Object
            For i As Integer = 0 To destTable.PrimaryKey.GetUpperBound(0)
                keys(i) = dupeRow(destTable.PrimaryKey(i))
            Next

            Dim existingRow As DataRow = destTable.Rows.Find(keys)
            If existingRow IsNot Nothing Then
                compareResult = CompareRows(existingRow, dupeRow, overWriteExistingRows)
            End If

            If existingRow Is Nothing OrElse compareResult = RowCompareResult.Add Then
                compareResult = AlternateRowSearch(destTable, dupeRow, existingRow, overWriteExistingRows)
            End If

            If compareResult = RowCompareResult.Overwrite Then
                OverwriteRow(existingRow, dupeRow, row, source)
            End If

            If existingRow Is Nothing OrElse compareResult = RowCompareResult.Add Then
                canAddRow = False
                If dupeRow.Table.PrimaryKey.Length = 1 Then
                    dupeRow(destTable.PrimaryKey(0)) = destTable.GetTableHandler.GetNextPrimaryKeyValue()
                    If parentRow IsNot Nothing Then dupeRow.SetParentRow(parentRow)
                    canAddRow = True
                ElseIf parentRow IsNot Nothing Then
                    For i As Integer = 0 To destTable.PrimaryKey.Length - 1
                        If dupeRow(destTable.PrimaryKey(i)) = baseKey Then
                            dupeRow(destTable.PrimaryKey(i)) = parentRow(parentRow.Table.PrimaryKey(0))
                            canAddRow = True
                        End If
                    Next
                End If
            End If

            If canAddRow Then
                Try
                    AddNewRow(destTable, dupeRow, row, source, overWriteExistingRows)
                Catch ex As Exception
                    'TODO: temporary
                    ErrorHandler.ShowWarning("Unable to copy row: " & vbCrLf & ex.ToString)
                End Try
            End If

            For Each dr As DataRelation In row.Table.ChildRelations
                Dim childRows As DataRow() = row.GetChildRows(dr)
                If childRows.Length > 0 Then
                    _dm.Database.Table(childRows(0).Table.TableName).GetTableHandler.CopyRows(childRows, dupeRow, row(row.Table.PrimaryKey(0)), source, False)
                End If
            Next
        Next

        'return last row added
        Return dupeRow
    End Function

    'TODO: this check will need to be split out to a table by table basis, also need to decide whether to overwrite existing row
    Protected Overridable Function CompareRows(existingRow As DataRow, newRow As DataRow, overWriteExisting As Boolean) As RowCompareResult
        Dim compareField As String = existingRow.Table.GetStringProperty(TableProperty.ComparisonField)
        If String.IsNullOrEmpty(compareField) Then
            For Each c As DataColumn In existingRow.Table.Columns
                If existingRow(c) <> newRow(c.ColumnName) Then
                    Return RowCompareResult.Add
                End If
            Next
        Else
            If existingRow(compareField) = newRow(compareField) Then
                If overWriteExisting Then Return RowCompareResult.Overwrite Else Return RowCompareResult.Skip
            Else
                Return RowCompareResult.Add
            End If
        End If
        Return RowCompareResult.Skip
    End Function

    Protected Overridable Function AlternateRowSearch(table As DataTable, newRow As DataRow, ByRef existingRow As DataRow, overWriteExisting As Boolean) As RowCompareResult
        Dim compareField As String = table.GetStringProperty(TableProperty.ComparisonField)
        If Not String.IsNullOrEmpty(compareField) Then
            existingRow = (From r As DataRow In table.AsEnumerable Where r.RowState <> DataRowState.Deleted AndAlso r(compareField) = newRow(compareField) Select r).FirstOrDefault
            If existingRow IsNot Nothing Then
                If overWriteExisting Then Return RowCompareResult.Overwrite Else Return RowCompareResult.Skip
            Else
                Return RowCompareResult.Add
            End If
        End If
        Return RowCompareResult.Skip
    End Function


    'will also need to be varied by table; in some cases may require adding/overwriting additional rows
    Protected Overridable Sub OverwriteRow(existingRow As DataRow, newRow As DataRow, sourceRow As DataRow, sourceDB As XmlDB)
        For Each c As DataColumn In From dc As DataColumn In existingRow.Table.Columns Where Not existingRow.Table.PrimaryKey.Contains(dc) And Not dc.ReadOnly Select dc
            existingRow(c) = newRow(c.ColumnName)
        Next
    End Sub

    Protected Overridable Sub AddNewRow(table As DataTable, newRow As DataRow, sourceRow As DataRow, sourceDB As XmlDB, displayErrors As Boolean)
        Dim addRow As Boolean = True
        For Each dc As DataColumn In table.Columns
            Dim c As DataColumn = dc

            'why do this with the lookup_table property instead of relations?
            'because the relations are not set up on the extended items table, and may not always work on other tables either
            'but lookup properties are required for dropdowns.
            'And this way we can compare the text values as well as the numeric ones, which is very important
            Dim lookupTable As String = c.GetStringProperty(ColumnProperty.Lookup_Table)
            If Not String.IsNullOrEmpty(lookupTable) Then
                Dim lookupRow As DataRow = sourceDB.Table(lookupTable).Rows.Find(sourceRow(c.ColumnName))
                If lookupRow IsNot Nothing Then
                    Dim lookupText As String = lookupRow(c.GetStringProperty(ColumnProperty.Lookup_TextColumn))

                    Dim newLookupRow As DataRow = (From r In _dm.Database.Table(lookupTable).AsEnumerable Where r.RowState <> DataRowState.Deleted AndAlso r(c.GetStringProperty(ColumnProperty.Lookup_TextColumn)) = lookupText Select r).FirstOrDefault
                    If newLookupRow IsNot Nothing Then
                        newRow(c.ColumnName) = newLookupRow(c.GetStringProperty(ColumnProperty.Lookup_ValueColumn))
                    Else
                        'missing data... don't necessarily want to copy over items from this point because it may mean adding stuff we don't want, better to not add the row
                        'only add if the field absolutely requires a reference to the other item (ie: default attachments).
                        'this can get messy if all the items have different names and ids across data sets, so it's not currently used
                        'itemTable implements a version of this however
                        If c.GetBooleanProperty(ColumnProperty.ReferenceRequired) Then
                            Dim copiedRow As DataRow = _dm.Database.CopyRows(lookupTable, New DataRow() {lookupRow}, sourceDB)
                            If copiedRow Is Nothing Then
                                'copy failed, so quit
                                If displayErrors Then ErrorHandler.ShowWarning("Row(s) failed to copy.  An unknown error has occurred.", "Row Copy Failed", MessageBoxIcon.Exclamation)
                                addRow = False
                                Exit For
                            Else
                                If lookupTable = table.TableName Then 'just added a row to the table to which we were adding a row, so our primary key has been stolen!
                                    If table.PrimaryKey.Length = 1 Then 'again, must only have one key in the table
                                        newRow(table.PrimaryKey(0)) = table.GetTableHandler.GetNextPrimaryKeyValue()
                                    End If
                                End If
                            End If
                        Else
                            If lookupTable <> table.TableName Then 'ignore cases like default attachments, which reference the same table
                                'missing data, so quit
                                If displayErrors Then ErrorHandler.ShowWarning("Row(s) failed to copy.  Missing lookup data in destination data set:" & vbCrLf & "Table: " & lookupTable & vbCrLf & "Value: " & lookupText, "Row Copy Failed", MessageBoxIcon.Exclamation)
                                addRow = False
                                Exit For
                            Else
                                newRow(c.ColumnName) = c.DefaultValue
                            End If
                        End If
                    End If
                Else
                    If displayErrors Then ErrorHandler.ShowWarning("Row(s) failed to copy.  No matching record in source data set. Source column: " & c.ColumnName, "Row Copy Failed", MessageBoxIcon.Exclamation)
                    addRow = False
                    Exit For
                    'quit?  bad data
                End If
            End If
        Next
        Try
            If addRow Then table.Rows.Add(newRow)
        Catch
            'cancel copy
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                _dm = Nothing
                _table = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
