Imports System.Xml
Imports System.IO

Public Class ControlTable
    Inherits DefaultTable

    Public Sub New(table As DataTable, manager As DataManager)
        MyBase.New(table, manager)
    End Sub


    Public Overrides Sub LoadData()
        Const Temp As String = "temp"
        Dim t As DataTable = Nothing

        _table.BeginLoadData()
        _table.Clear()
        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        Dim sourceName As String = _table.GetStringProperty(TableProperty.SourceTableName)

        If sourceName Is Nothing Then
        Else
            Dim tableName As String = _table.TableName
            For Each t In _table.DataSet.Tables
                If t.TableName = sourceName Then
                    t.TableName = Temp
                    Exit For
                End If
            Next

            _table.TableName = sourceName
            LoadControlData(fileName, filePath)

            _table.TableName = tableName
            If t IsNot Nothing AndAlso t.TableName = Temp Then t.TableName = sourceName
        End If
        _table.EndLoadData()
    End Sub

    Protected Overridable Sub LoadControlData(ByVal fileName As String, ByVal filePath As String)
        Dim xmldoc As New XmlDataDocument()
        Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
        xmldoc.Load(fs)

        Dim xmlnode As XmlNode = xmldoc.GetElementsByTagName("INVENTORYLIST").Item(0)
        Dim xmlParentNode As XmlNodeList
        Dim xmlChildNode As XmlNodeList
        Dim xmlChild2Node As XmlNodeList
        For i As Integer = 0 To xmlnode.ChildNodes.Count - 1
            If xmlnode.ChildNodes.Item(i).Name = "INVENTORY" Then Continue For
            _table.Rows.Add()
            xmlParentNode = xmlnode.ChildNodes.Item(i).ChildNodes
            For x As Integer = 0 To xmlParentNode.Count - 1
                If xmlParentNode.Item(x).Name = "#comment" Then Continue For
                If xmlParentNode.Item(x).Name = "REORDERDAYSDELAY" OrElse xmlParentNode.Item(x).Name = "CASH" OrElse xmlParentNode.Item(x).Name = "COOLNESS" OrElse xmlParentNode.Item(x).Name = "BASICDEALERFLAGS" Then
                    xmlChildNode = xmlParentNode.Item(x).ChildNodes
                    For y As Integer = 0 To xmlChildNode.Count - 1
                        If xmlChildNode.Item(y).Name = "#comment" Then Continue For
                        If xmlChildNode.Item(y).Name = "DAILY" Then
                            xmlChild2Node = xmlChildNode.Item(y).ChildNodes
                            For z As Integer = 0 To xmlChild2Node.Count - 1
                                If _table.Columns.Contains(xmlChild2Node.Item(z).Name) Then
                                    If xmlChild2Node.Item(z).Name = "#comment" Then Continue For
                                    If _table.Columns(xmlChild2Node.Item(z).Name).DataType.Name = "Boolean" Then
                                        _table.Rows(_table.Rows.Count - 1).Item(xmlChild2Node.Item(z).Name) = IIf(xmlChild2Node.Item(z).InnerText.Trim = 1, True, False)
                                    Else
                                        _table.Rows(_table.Rows.Count - 1).Item(xmlChild2Node.Item(z).Name) = xmlChild2Node.Item(z).InnerText.Trim
                                    End If
                                End If
                            Next
                        Else
                            If _table.Columns.Contains(xmlChildNode.Item(y).Name) Then
                                If _table.Columns(xmlChildNode.Item(y).Name).DataType.Name = "Boolean" Then
                                    _table.Rows(_table.Rows.Count - 1).Item(xmlChildNode.Item(y).Name) = IIf(xmlChildNode.Item(y).InnerText.Trim = 1, True, False)
                                Else
                                    _table.Rows(_table.Rows.Count - 1).Item(xmlChildNode.Item(y).Name) = xmlChildNode.Item(y).InnerText.Trim
                                End If
                            End If
                        End If
                    Next
                Else
                    If _table.Columns.Contains(xmlParentNode.Item(x).Name) Then
                        If _table.Columns(xmlParentNode.Item(x).Name).DataType.Name = "Boolean" Then
                            _table.Rows(_table.Rows.Count - 1).Item(xmlParentNode.Item(x).Name) = IIf(xmlParentNode.Item(x).InnerText.Trim = 1, True, False)
                        Else
                            _table.Rows(_table.Rows.Count - 1).Item(xmlParentNode.Item(x).Name) = xmlParentNode.Item(x).InnerText.Trim
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
        Dim trim As Boolean = True
        Dim child1 As Boolean = False
        Dim child2 As Boolean = False

        xw.WriteStartDocument()
        xw.WriteWhitespace(vbLf)
        xw.WriteStartElement(table.ExtendedProperties("DataSetName").ToString)
        xw.WriteWhitespace(vbLf)
        For i = 0 To view.Count - 1
            child1 = False
            child2 = False
            If Not table.Rows(i).RowState = DataRowState.Deleted Then
                xw.WriteString(vbTab)
                xw.WriteStartElement(table.TableName)
                xw.WriteString(vbLf)
                For x = 0 To table.Rows(i).ItemArray.Length - 1
                    Dim c As DataColumn = table.Columns(x)
                    If c.ColumnName = "REORDERMINIMUM" Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteStartElement("REORDERDAYSDELAY")
                        xw.WriteWhitespace(vbLf)
                        child1 = True
                        child2 = False
                    ElseIf c.ColumnName = "INITIAL" Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteEndElement()
                        xw.WriteWhitespace(vbLf)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteStartElement("CASH")
                        xw.WriteWhitespace(vbLf)
                        child1 = True
                        child2 = False
                    ElseIf c.ColumnName = "INCREMENT" Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteStartElement("DAILY")
                        xw.WriteWhitespace(vbLf)
                        child1 = False
                        child2 = True
                    ElseIf c.ColumnName = "COOLMINIMUM" Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteEndElement()
                        xw.WriteWhitespace(vbLf)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteEndElement()
                        xw.WriteWhitespace(vbLf)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteStartElement("COOLNESS")
                        xw.WriteWhitespace(vbLf)
                        child1 = True
                        child2 = False
                    ElseIf c.ColumnName = "ARMS_DEALER_HANDGUNCLASS" Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteEndElement()
                        xw.WriteWhitespace(vbLf)
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        xw.WriteStartElement("BASICDEALERFLAGS")
                        xw.WriteWhitespace(vbLf)
                        child1 = True
                        child2 = False
                    End If
                    If Not trim OrElse (c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> 0) _
                        OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        If child1 = True Then xw.WriteString(vbTab)
                        If child2 = True Then
                            xw.WriteString(vbTab)
                            xw.WriteString(vbTab)
                        End If
                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            xw.WriteElementString(c.ColumnName, view(i)(c.ColumnName))
                        Else
                            If view(i)(c.ColumnName) Then
                                xw.WriteElementString(c.ColumnName, 1)
                            Else
                                xw.WriteElementString(c.ColumnName, 0)
                            End If
                        End If
                        xw.WriteString(vbLf)
                    End If
                Next

                xw.WriteString(vbTab)
                xw.WriteString(vbTab)
                xw.WriteEndElement()
                xw.WriteString(vbLf)

                xw.WriteString(vbTab)
                xw.WriteEndElement()
                xw.WriteString(vbLf)
            End If
        Next
        xw.WriteEndElement()
        xw.WriteEndDocument()
        xw.Close()

    End Sub

End Class
