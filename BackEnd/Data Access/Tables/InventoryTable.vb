Imports System.Xml

Public Class InventoryTable
    Inherits AutoIncrementTable

    Public Sub New(table As DataTable, manager As DataManager)
        MyBase.New(table, manager)
    End Sub


    Protected Overrides Sub WriteXml(ByVal table As DataTable, ByVal fileName As String)
        Dim view As New DataView(table, "", table.Columns(0).ColumnName, DataViewRowState.CurrentRows)
        Dim xw As XmlTextWriter
        Dim control As Boolean = False
        Dim endTag As Byte() = System.Text.Encoding.UTF8.GetBytes("</" & table.ExtendedProperties("DataSetName").ToString & ">")
        If System.IO.File.Exists(fileName) Then
            Dim fs As System.IO.FileStream = System.IO.File.OpenWrite(fileName)
            fs.Seek(-endTag.Length, System.IO.SeekOrigin.End)
            xw = New XmlTextWriter(fs, System.Text.Encoding.UTF8)
            control = True
        Else
            xw = New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
        End If
        Dim i As Integer
        Dim x As Integer
        Dim trim As Boolean = True

        If control = False Then
            xw.WriteStartDocument()
            xw.WriteWhitespace(vbLf)
            xw.WriteStartElement(table.ExtendedProperties("DataSetName").ToString)
            xw.WriteWhitespace(vbLf)
        End If
        For i = 0 To view.Count - 1
            If Not table.Rows(i).RowState = DataRowState.Deleted Then
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
                xw.WriteEndElement()
                xw.WriteString(vbLf)
            End If
        Next
        If control = False Then
            xw.WriteEndElement()
            xw.WriteEndDocument()
        Else
            xw.Flush()
            xw.WriteRaw("</" & table.ExtendedProperties("DataSetName").ToString & ">")
        End If
        xw.Close()

    End Sub

End Class
