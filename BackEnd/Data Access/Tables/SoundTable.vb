Public Class SoundTable
    Inherits AutoIncrementTable

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
    End Sub


    Public Overrides Sub LoadData()

        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        _table.Clear()
        Dim xr As New Xml.XmlTextReader(filePath)
        While xr.Read
            If xr.NodeType = Xml.XmlNodeType.Text Then
                Dim r As DataRow = _table.NewRow
                r(Tables.LookupTableFields.Name) = xr.Value
                _table.Rows.Add(r)
            End If
        End While
        xr.Close()
    End Sub

    Public Overrides Sub SaveData()

        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        Dim xw As New Xml.XmlTextWriter(filePath, Text.Encoding.UTF8)
        xw.WriteStartElement(_table.GetStringProperty(TableProperty.DataSetName))
        xw.WriteString(vbLf)
        For Each r As DataRow In _table.Rows
            xw.WriteString(vbTab)
            xw.WriteElementString(Tables.Sounds, r(Tables.LookupTableFields.Name))
            xw.WriteString(vbLf)
        Next
        xw.WriteEndElement()
        xw.WriteString(vbLf)
        xw.Close()
        _table.AcceptChanges()
    End Sub
End Class
