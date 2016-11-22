Public Class LookupFile
    Private Shared Sub AddLookupData(ByVal t As DataTable, ByVal id As Integer, ByVal name As String)
        Dim r As DataRow = t.NewRow
        r("id") = id
        r("name") = name
        t.Rows.Add(r)
    End Sub

    Public Shared Sub AddLookupData(ByVal fileName As String, ByVal lookupTable As DataTable)
        Dim xr As New Xml.XmlTextReader(fileName)
        Dim curNode, curValue As String

        curNode = ""

        Dim name As String = Nothing
        Dim id As Integer = Integer.MinValue

        While xr.Read
            If xr.NodeType = Xml.XmlNodeType.Element Then
                curNode = xr.Name
            ElseIf xr.NodeType = Xml.XmlNodeType.Text Then
                curValue = xr.Value
                Select Case curNode
                    Case "id"
                        id = Integer.Parse(curValue)
                    Case "name"
                        name = curValue
                End Select
            End If

            ' Both variables are set
            If name <> Nothing And id <> Integer.MinValue Then
                AddLookupData(lookupTable, id, name)

                'Reset
                name = Nothing
                id = Integer.MinValue
            End If

        End While
        xr.Close()
    End Sub

End Class
