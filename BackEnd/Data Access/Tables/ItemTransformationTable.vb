Imports System.Xml
Imports System.IO

Public Class ItemTransformationTable
    Inherits DefaultTable

    Public Sub New(ByVal table As DataTable, ByVal manager As DataManager)
        MyBase.New(table, manager)
    End Sub

    Public Overrides Sub LoadData()
        _table.BeginLoadData()
        _table.Clear()
        Dim fileName As String = _table.GetStringProperty(TableProperty.FileName)
        Dim filePath As String = GetFilePath(fileName)

        LoadTransformData(fileName, filePath)

        _table.EndLoadData()
    End Sub

    Protected Sub LoadTransformData(ByVal fileName As String, ByVal filePath As String)
        Dim xmldoc As New XmlDataDocument()
        Dim xmlnode As XmlNode
        Dim xmlnode2 As XmlNodeList
        Dim i As Integer
        Dim x As Integer
        Dim da As Integer
        Dim uiComments As Integer = 0
        Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
        xmldoc.Load(fs)
        xmlnode = xmldoc.GetElementsByTagName("TRANSFORMATIONS_LIST").Item(0)
        For i = 0 To xmlnode.ChildNodes.Count - 1
            If xmlnode.ChildNodes.Item(i).Name = "#comment" Then
                uiComments = uiComments + 1
                Continue For
            End If
            _table.Rows.Add()
            da = 1
            xmlnode2 = xmlnode.ChildNodes.Item(i).ChildNodes
            For x = 0 To xmlnode2.Count - 1
                If xmlnode2.Item(x).Name = "#comment" Then Continue For
                If xmlnode2.Item(x).Name = "usResult" Then
                    If da < 11 Then
                        _table.Rows(i - uiComments).Item(xmlnode2.Item(x).Name & da) = xmlnode2.Item(x).InnerText.Trim
                        da = da + 1
                    End If
                Else
                    _table.Rows(i - uiComments).Item(xmlnode2.Item(x).Name) = xmlnode2.Item(x).InnerText.Trim
                End If
            Next
        Next
    End Sub

    Protected Sub WriteXml_ItemTransform(ByVal table As DataTable, ByVal fileName As String)
        Dim view As New DataView(table, "", table.Columns(0).ColumnName, DataViewRowState.CurrentRows)
        Dim xw As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        Dim i As Integer
        Dim x As Integer
        Dim da As Integer
        Dim trim As Boolean = GetBooleanProperty(table, TableProperty.Trim)
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
                    If Not trim OrElse (i = 0 OrElse c Is table.PrimaryKey(0) OrElse ((c.DataType.Equals(GetType(Boolean)) OrElse c.DataType.Equals(GetType(Decimal)) OrElse c.DataType.Equals(GetType(ULong)) OrElse c.DataType.Equals(GetType(Integer)) OrElse c.DataType.Equals(GetType(Long))) AndAlso view(i)(c.ColumnName) <> 0) _
                                    OrElse (c.DataType.Equals(GetType(String)) AndAlso view(i)(c.ColumnName) <> "")) Then
                        xw.WriteString(vbTab)
                        xw.WriteString(vbTab)
                        If Not c.DataType.Equals(GetType(Boolean)) Then
                            If c.ColumnName.Contains("usResult") Then
                                xw.WriteElementString("usResult", view(i)(c.ColumnName))
                            Else
                                xw.WriteElementString(c.ColumnName, view(i)(c.ColumnName))
                            End If
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
                xw.WriteEndElement()
                xw.WriteString(vbLf)
            End If
        Next
        xw.WriteEndElement()
        xw.WriteEndDocument()
        xw.Close()

    End Sub
End Class
