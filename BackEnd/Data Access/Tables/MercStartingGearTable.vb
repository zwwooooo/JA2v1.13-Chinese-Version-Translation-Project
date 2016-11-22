Imports System.Xml
Imports System.IO

Public Class MercStartingGearTable
    Inherits DefaultTable

    Public Sub New(table As DataTable, manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    Public Overrides Sub LoadData()
        _table.BeginLoadData()
        _table.Clear()
        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        LoadMSGData(fileName, filePath)

        _table.EndLoadData()
    End Sub

    Protected Overridable Sub LoadMSGData(ByVal fileName As String, ByVal filePath As String)
        Dim xmldoc As New XmlDataDocument()
        Dim xmlnode As XmlNode
        Dim xmlnode2 As XmlNodeList
        Dim xmlnode3 As XmlNodeList
        Dim i As Integer
        Dim x As Integer
        Dim y As Integer
        Dim a As Integer
        Dim uiComments As Integer = 0
        Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
        xmldoc.Load(fs)
        xmlnode = xmldoc.GetElementsByTagName("MERCGEARLIST").Item(0)
        For i = 0 To xmlnode.ChildNodes.Count - 1
            If xmlnode.ChildNodes.Item(i).Name = "#comment" Then
                uiComments = uiComments + 1
                Continue For
            End If
            _table.Rows.Add()
            a = 0
            xmlnode2 = xmlnode.ChildNodes.Item(i).ChildNodes
            For x = 0 To xmlnode2.Count - 1
                If xmlnode2.Item(x).Name = "#comment" Then Continue For
                If xmlnode2.Item(x).Name = "GEARKIT" Then
                    a = a + 1
                    xmlnode3 = xmlnode2.Item(x).ChildNodes
                    For y = 0 To xmlnode3.Count - 1
                        If xmlnode3.Item(y).Name = "#comment" Then Continue For

                        If _table.Columns.Contains(xmlnode3.Item(y).Name & a) Then
                            If _table.Columns(xmlnode3.Item(y).Name & a).DataType.Name = "Boolean" Then
                                _table.Rows(i - uiComments).Item(xmlnode3.Item(y).Name & a) = IIf(xmlnode3.Item(y).InnerText.Trim = 1, True, False)
                            Else
                                _table.Rows(i - uiComments).Item(xmlnode3.Item(y).Name & a) = xmlnode3.Item(y).InnerText.Trim
                            End If
                        End If
                    Next
                Else
                    If _table.Columns.Contains(xmlnode2.Item(x).Name) Then
                        If _table.Columns(xmlnode2.Item(x).Name).DataType.Name = "Boolean" Then
                            _table.Rows(i - uiComments).Item(xmlnode2.Item(x).Name) = IIf(xmlnode2.Item(x).InnerText.Trim = 1, True, False)
                        Else
                            _table.Rows(i - uiComments).Item(xmlnode2.Item(x).Name) = xmlnode2.Item(x).InnerText.Trim
                        End If
                    End If
                End If
            Next
        Next
        fs.Close()
        fs.Dispose()
    End Sub

    Protected Overrides Sub WriteXml(ByVal table As DataTable, ByVal fileName As String)
        'the stupid table.WriteXml method doesn't let you sort the data first
        Dim view As New DataView(table, "", table.Columns(0).ColumnName, DataViewRowState.CurrentRows)
        'Dim trim As Boolean = table.GetBooleanProperty(TableProperty.Trim)
        Dim trim As Boolean = False
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
            If Not table.Rows(i).RowState = DataRowState.Deleted Then
                xw.WriteString(vbTab)
                xw.WriteStartElement(table.TableName)
                xw.WriteString(vbLf)

                'Dim dcIndex As Integer = -1
                Dim isGearKit As Boolean = False
                Dim gkIndex As String = ""
                trim = False

                For Each c As DataColumn In table.Columns

                    ' Close the <GEARKIT> element.  We want to do this here because we want to check for closing the <GEARKIT> tag after we've cycled columns
                    If Not gkIndex = c.ColumnName.Substring(c.ColumnName.Length - 1) And isGearKit = True Then
                        isGearKit = False
                        trim = True
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteEndElement()
                        xw.WriteString(vbLf)
                    End If

                    If Not trim OrElse (c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> 0) _
                        OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then

                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)

                        ' Open the <GEARKIT> element
                        If (c.ColumnName.EndsWith("1") OrElse c.ColumnName.EndsWith("2") OrElse c.ColumnName.EndsWith("3") OrElse c.ColumnName.EndsWith("4") OrElse c.ColumnName.EndsWith("5")) _
                        And isGearKit = False Then
                            isGearKit = True
                            gkIndex = c.ColumnName.Substring(c.ColumnName.Length - 1)

                            xw.WriteStartElement("GEARKIT")
                            xw.WriteString(vbLf)
                            xw.WriteString(vbTab)
                            xw.WriteString(vbTab)
                        End If

                        If isGearKit = True Then
                            xw.WriteString(vbTab)
                        End If

                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            If gkIndex = c.ColumnName.Substring(c.ColumnName.Length - 1) Then
                                xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), view(i)(c.ColumnName))
                            Else
                                xw.WriteElementString(c.ColumnName, view(i)(c.ColumnName))
                            End If
                        Else
                            If gkIndex = c.ColumnName.Substring(c.ColumnName.Length - 1) Then
                                If view(i)(c.ColumnName) Then
                                    xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 1)
                                Else
                                    xw.WriteElementString(c.ColumnName.Remove(c.ColumnName.Length - 1, 1), 0)
                                End If
                            Else
                                If view(i)(c.ColumnName) Then
                                    xw.WriteElementString(c.ColumnName, 1)
                                Else
                                    xw.WriteElementString(c.ColumnName, 0)
                                End If
                            End If
                        End If

                        xw.WriteString(vbLf)
                    End If
                Next
                xw.WriteString(vbTab)
                If isGearKit = True Then
                    xw.WriteEndElement()
                    xw.WriteString(vbLf)
                    xw.WriteString(vbTab)
                End If
                xw.WriteEndElement()
                xw.WriteString(vbLf)
            End If
        Next
        xw.WriteEndElement()
        xw.Close()
        view.Dispose()

        table.AcceptChanges()
    End Sub


End Class
