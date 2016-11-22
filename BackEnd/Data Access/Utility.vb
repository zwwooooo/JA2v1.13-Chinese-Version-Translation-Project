Imports System.Runtime.CompilerServices

Public Module Utility
    Public Class TableProperty
        Public Const FileName As String = "FileName"
        Public Const DataSetName As String = "DataSetName"
        Public Const SourceTableName As String = "SourceTableName"
        Public Const TableHandler As String = "TableHandler"
        Public Const TableHandlerName As String = "TableHandlerName"
        Public Const Trim As String = "Trim"
        Public Const ComparisonField As String = "ComparisonField"
        Public Const NoRowDuplicate As String = "NoRowDuplicate"
    End Class

    Public Class ColumnProperty
        Public Const Sort As String = "Sort"
        Public Const SortDesc As String = "SortDesc"
        Public Const Locked As String = "Locked"
        Public Const Grid_Hidden As String = "Grid_Hidden"
        Public Const Form_Hidden As String = "Form_Hidden"
        Public Const Lookup_Table As String = "Lookup_Table"
        Public Const Lookup_ValueColumn As String = "Lookup_ValueColumn"
        Public Const Lookup_TextColumn As String = "Lookup_TextColumn"
        Public Const Lookup_AddBlank As String = "Lookup_AddBlank"
        Public Const Lookup_FirstValuePrefix As String = "Lookup_FirstValuePrefix"
        Public Const Lookup_Filter As String = "Lookup_Filter"
        Public Const Lookup_Sort As String = "Lookup_Sort"
        Public Const ReferenceRequired As String = "RefRequired"
        Public Const ToolTip As String = "ToolTip"
        Public Const SubTable As String = "SubTable"
        Public Const Width As String = "Width"
        Public Const SourceColumnName As String = "SourceColumnName"
    End Class

    Public Class ShopKeepers
        Public Const Alberto As String = "Alberto"
        Public Const Arnie As String = "Arnie"
        Public Const Carlo As String = "Carlo"
        Public Const Devin As String = "Devin"
        Public Const Elgin As String = "Elgin"
        Public Const Frank As String = "Frank"
        Public Const Franz As String = "Franz"
        Public Const Fredo As String = "Fredo"
        Public Const Gabby As String = "Gabby"
        Public Const Herve As String = "Herve"
        Public Const Howard As String = "Howard"
        Public Const Jake As String = "Jake"
        Public Const Keith As String = "Keith"
        Public Const Manny As String = "Manny"
        Public Const Mickey As String = "Mickey"
        Public Const Perko As String = "Perko"
        Public Const Peter As String = "Peter"
        Public Const Sam As String = "Sam"
        Public Const Tony As String = "Tony"
    End Class

    Public Enum ItemClass
        None = 1
        Gun = 2
        Knife = 4
        ThrowingKnife = 8
        Launcher = 16
        Tentacle = 32
        Thrown = 64
        Punch = 128
        Grenade = 256
        Bomb = 512
        Ammo = 1024
        Armour = 2048
        MedKit = 4096
        Kit = 8192
        'Unused = 16384
        Face = 32768
        Key = 65536
        LBE = 131072
        Misc = 268435456
        Money = 536870912
        RandomItem = 1073741824
    End Enum

    <Extension()>
    Friend Function GetTableHandler(ByVal table As DataTable) As DefaultTable
        Dim val As DefaultTable = Nothing
        If table IsNot Nothing AndAlso table.ExtendedProperties.Contains(TableProperty.TableHandler) Then
            val = CType(table.ExtendedProperties.Item(TableProperty.TableHandler), DefaultTable)
        End If
        Return val
    End Function

    <Extension()>
    Public Function GetStringProperty(ByVal table As DataTable, ByVal extendedProperty As String) As String
        Dim val As String = Nothing
        If table Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If table.ExtendedProperties.Contains(extendedProperty) Then
            val = CStr(table.ExtendedProperties.Item(extendedProperty))
        End If
        Return val
    End Function

    <Extension()>
    Public Function GetStringProperty(ByVal column As DataColumn, ByVal extendedProperty As String) As String
        Dim val As String = Nothing
        If column Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If column.ExtendedProperties.Contains(extendedProperty) Then
            val = CStr(column.ExtendedProperties.Item(extendedProperty))
        End If
        Return val
    End Function

    <Extension()>
    Public Function GetBooleanProperty(ByVal table As DataTable, ByVal extendedProperty As String) As Boolean
        Dim val As String = False
        If table Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If table.ExtendedProperties.Contains(extendedProperty) Then
            val = CBool(table.ExtendedProperties.Item(extendedProperty))
        End If
        Return val
    End Function

    <Extension()>
    Public Function GetBooleanProperty(ByVal column As DataColumn, ByVal extendedProperty As String) As Boolean
        Dim val As String = False
        If column Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If column.ExtendedProperties.Contains(extendedProperty) Then
            val = CBool(column.ExtendedProperties.Item(extendedProperty))
        End If
        Return val
    End Function

    <Extension()>
    Public Sub SetProperty(ByVal table As DataTable, ByVal extendedProperty As String, ByVal Value As Object)
        If table Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException

        If Not Value Is Nothing Then
            If table.ExtendedProperties.Contains(extendedProperty) Then
                table.ExtendedProperties.Item(extendedProperty) = Value
            Else
                table.ExtendedProperties.Add(extendedProperty, Value)
            End If
        End If
    End Sub

    <Extension()>
    Public Sub SetProperty(ByVal column As DataColumn, ByVal extendedProperty As String, ByVal Value As Object)
        If column Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If Not Value Is Nothing Then
            If column.ExtendedProperties.Contains(extendedProperty) Then
                column.ExtendedProperties.Item(extendedProperty) = Value
            Else
                column.ExtendedProperties.Add(extendedProperty, Value)
            End If
        End If
    End Sub

    <Extension()>
    Public Sub RemoveProperty(ByVal column As DataColumn, ByVal extendedProperty As String)
        If column Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If column.ExtendedProperties.Contains(extendedProperty) Then
            column.ExtendedProperties.Remove(extendedProperty)
        End If
    End Sub

    <Extension()>
    Public Sub RemoveProperty(ByVal table As DataTable, ByVal extendedProperty As String)
        If table Is Nothing Or extendedProperty Is Nothing Then Throw New ArgumentNullException
        If table.ExtendedProperties.Contains(extendedProperty) Then
            table.ExtendedProperties.Remove(extendedProperty)
        End If
    End Sub

    <Extension()>
    Friend Function Clone(ByVal column As DataColumn) As DataColumn
        Dim c As New DataColumn
        With column
            c.ColumnName = .ColumnName
            c.DataType = .DataType
            c.Caption = .Caption
            c.DefaultValue = .DefaultValue
            c.AllowDBNull = .AllowDBNull
            Dim e As IDictionaryEnumerator = .ExtendedProperties.GetEnumerator()
            While e.MoveNext
                c.ExtendedProperties.Add(e.Key, e.Value)
            End While
        End With
        Return c
    End Function

End Module
