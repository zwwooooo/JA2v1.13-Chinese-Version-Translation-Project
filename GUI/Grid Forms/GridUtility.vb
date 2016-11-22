Module GridUtility
    Friend Const BlankLookupRowName As String = " "
    Friend Const BlankLookupRowValue As Integer = 0

    Private Const DefaultColumnWidth As Integer = 100

    Friend Class GridCopyContainer
        Public Property Database As XmlDB
        Public Property Rows As DataRow()
    End Class

    Friend Sub InitializeGrid(db As XmlDB, ByVal grid As DataGridView, ByVal view As DataView, Optional ByVal subTable As String = Nothing, Optional ByVal autoSizeColumns As Boolean = False)
        Dim viewCache As New Hashtable

        grid.AutoGenerateColumns = False
        grid.DataSource = view
        If grid.Columns.Count > 0 Then
            grid.Columns.Clear()
        End If

        AddHandler grid.DataError, AddressOf Grid_DataError

        For Each c As DataColumn In view.Table.Columns
            Application.DoEvents()
            Dim visible As Boolean = Not c.GetBooleanProperty(ColumnProperty.Grid_Hidden)
            Dim lookupTable As String = c.GetStringProperty(ColumnProperty.Lookup_Table)

            If c.GetBooleanProperty(ColumnProperty.SubTable) Then
                If subTable Is Nothing OrElse subTable.Length = 0 OrElse Not c.ColumnName.Contains(subTable) Then
                    visible = False
                End If
            End If

            If visible OrElse c Is view.Table.PrimaryKey(0) Then
                Dim dc As DataGridViewColumn

                If c.DataType Is GetType(Boolean) Then
                    dc = New DataGridViewCheckBoxColumn
                ElseIf c.DataType Is GetType(Image) Then
                    dc = New DataGridViewImageColumn
                ElseIf lookupTable IsNot Nothing Then
                    dc = New DataGridViewComboBoxColumn
                Else
                    dc = New DataGridViewTextBoxColumn
                End If

                With dc
                    If view.Table.TableName = "POCKET" And ItemSizesRead = True And ItemSizeMax > 250 Then
                        .FillWeight = 1
                    End If
                    .Name = c.ColumnName
                    .HeaderText = c.Caption
                    .DataPropertyName = c.ColumnName
                    .Resizable = DataGridViewTriState.True
                    .Visible = visible 'needed for primary key columns
                    .SortMode = DataGridViewColumnSortMode.Automatic

                    'If Not autoSizeColumns Then
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = GetColumnWidth(db, c, subTable)
                    'Else
                    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    'End If

                    If dc.GetType Is GetType(DataGridViewImageColumn) Then .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    .ToolTipText = c.ColumnName
                    Dim ttText = c.GetStringProperty(ColumnProperty.ToolTip)
                    If ttText IsNot Nothing Then
                        .ToolTipText &= vbCrLf & vbCrLf & ttText
                    End If

                    If lookupTable IsNot Nothing Then
                        Dim lookupValueColumn As String = c.GetStringProperty(ColumnProperty.Lookup_ValueColumn)
                        Dim lookupTextColumn As String = c.GetStringProperty(ColumnProperty.Lookup_TextColumn)
                        Dim lookupFilter As String = c.GetStringProperty(ColumnProperty.Lookup_Filter)
                        Dim lookupAddBlank As Boolean = c.GetBooleanProperty(ColumnProperty.Lookup_AddBlank)
                        Dim lookupFirstValuePrefix As String = c.GetStringProperty(ColumnProperty.Lookup_FirstValuePrefix)
                        Dim lookupSort As String = c.GetStringProperty(ColumnProperty.Lookup_Sort)
                        If String.IsNullOrEmpty(lookupSort) Then lookupSort = lookupTextColumn

                        With CType(dc, DataGridViewComboBoxColumn)
                            Dim dv As DataView
                            Dim viewKey As String = view.Table.DataSet.Tables(lookupTable).TableName & lookupFilter & lookupTextColumn
                            If Not viewCache.ContainsKey(viewKey) Then
                                dv = New DataView(view.Table.DataSet.Tables(lookupTable), lookupFilter, lookupSort, DataViewRowState.CurrentRows)
                                viewCache.Add(viewKey, dv)
                            Else
                                dv = CType(viewCache(viewKey), DataView)
                            End If
                            If Not lookupAddBlank AndAlso String.IsNullOrEmpty(lookupFirstValuePrefix) Then
                                .ValueMember = lookupValueColumn
                                .DisplayMember = lookupTextColumn
                                .DataSource = dv
                            Else
                                'this isn't used often enough to bother optimizing it
                                dv.Sort = Nothing

                                Dim dt As New DataTable
                                dt.Columns.Add(Tables.LookupTableFields.ID, view.Table.DataSet.Tables(lookupTable).Columns(lookupValueColumn).DataType)
                                dt.Columns.Add(Tables.LookupTableFields.Name, view.Table.DataSet.Tables(lookupTable).Columns(lookupTextColumn).DataType)

                                If lookupAddBlank Then
                                    Dim r As DataRow = dt.NewRow
                                    r(Tables.LookupTableFields.ID) = BlankLookupRowValue
                                    r(Tables.LookupTableFields.Name) = BlankLookupRowName
                                    dt.Rows.Add(r)
                                End If

                                For Each dvr As DataRowView In dv
                                    Dim r As DataRow = dt.NewRow
                                    r(Tables.LookupTableFields.ID) = dvr(lookupValueColumn)
                                    r(Tables.LookupTableFields.Name) = dvr(lookupTextColumn)
                                    dt.Rows.Add(r)
                                Next

                                If Not String.IsNullOrEmpty(lookupFirstValuePrefix) Then
                                    dt.Rows(0)(Tables.LookupTableFields.Name) = lookupFirstValuePrefix & dt.Rows(0)(Tables.LookupTableFields.Name)
                                End If

                                dt.DefaultView.Sort = Tables.LookupTableFields.Name
                                dv.Dispose()

                                .ValueMember = Tables.LookupTableFields.ID
                                .DisplayMember = Tables.LookupTableFields.Name
                                .DataSource = dt
                            End If
                            If grid.ReadOnly Then
                                .DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                            Else
                                .DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
                            End If
                            .AutoComplete = True
                        End With
                    End If
                End With
                grid.Columns.Add(dc)
            End If
        Next
    End Sub

    Friend Sub Grid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        If e.Exception Is Nothing Then
            'do nothing
        ElseIf e.Exception.GetType.Equals(GetType(ArgumentException)) Then
            e.Cancel = True 'lame I know, but it makes the comboboxes work
        ElseIf e.Exception.GetType.Equals(GetType(ConstraintException)) Then
            ErrorHandler.ShowError("This value has already been used. Please enter a different value.", MessageBoxIcon.Exclamation)
        Else
            ErrorHandler.ShowError(e.Exception.Message, MessageBoxIcon.Error)
        End If
    End Sub

    Friend Function GetActualGridHeight(ByVal grid As DataGridView) As Integer
        Dim height As Integer = grid.RowCount * grid.RowTemplate.Height
        height += grid.ColumnHeadersHeight + 30
        Return height
    End Function

    Friend Function GetActualGridWidth(ByVal grid As DataGridView) As Integer
        Dim width As Integer = grid.RowHeadersWidth + 10
        For Each c As DataGridViewColumn In grid.Columns
            If c.Visible Then width += c.Width
        Next
        Return width
    End Function

    Friend Sub SetColumnWidth(db As XmlDB, ByVal dc As DataGridViewColumn, ByVal table As DataTable, Optional ByVal subTable As String = Nothing)
        If table.Columns.Contains(dc.Name) Then
            Dim c As DataColumn = table.Columns(dc.Name)
            If c.GetBooleanProperty(ColumnProperty.SubTable) Then
                db.Table(subTable).Columns(c.ColumnName.Remove(0, subTable.Length)).SetProperty(ColumnProperty.Width, dc.Width)
            Else
                c.SetProperty(ColumnProperty.Width, dc.Width)
            End If
        End If
    End Sub

    Friend Function GetColumnWidth(db As XmlDB, ByVal c As DataColumn, Optional ByVal subTable As String = Nothing) As Integer
        Dim width As String = Nothing
        If c.GetBooleanProperty(ColumnProperty.SubTable) Then
            width = db.Table(subTable).Columns(c.ColumnName.Remove(0, subTable.Length)).GetStringProperty(ColumnProperty.Width)
        Else
            width = c.GetStringProperty(ColumnProperty.Width)
        End If
        If width IsNot Nothing Then
            Return CInt(width)
        Else
            Return DefaultColumnWidth
        End If
    End Function


End Module
