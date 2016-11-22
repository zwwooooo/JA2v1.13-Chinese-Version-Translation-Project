Imports GUI.GUI

Public Class GridForm
    Protected _view As DataView
    Protected _subTable As String
    Protected _origFilter As String = Nothing
    Protected _customFilter As String = Nothing
    Protected _dm As DataManager

    Private _heightProperty As String
    Private _widthProperty As String
    Private _topProperty As String
    Private _leftProperty As String
    Private _stateProperty As String
    Private _openedBeforeProperty As String
    Private _mouseDown As Boolean
    Private _keyDown As Boolean

    Public Sub New(manager As DataManager, ByVal formText As String, tableName As String, Optional rowFilter As String = Nothing, Optional sort As String = Nothing, Optional rowStateFilter As DataViewRowState = DataViewRowState.CurrentRows, Optional ByVal subTable As String = Nothing)
        LoadingForm.Show()
        Application.DoEvents()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = formText
        Dim spacelessText As String = Replace(Me.Text, " ", "_")
        _heightProperty = spacelessText & "_Height"
        _widthProperty = spacelessText & "_Width"
        _topProperty = spacelessText & "_Top"
        _leftProperty = spacelessText & "_Left"
        _stateProperty = spacelessText & "_WindowState"
        _openedBeforeProperty = spacelessText & "_OpenedBefore"

        _dm = manager

        If String.IsNullOrEmpty(sort) Then
            sort = _dm.Database.Table(tableName).PrimaryKey(0).ColumnName
        End If

        _view = New DataView(_dm.Database.Table(tableName), rowFilter, sort, rowStateFilter)

        If Not String.IsNullOrEmpty(rowFilter) Then _origFilter = "(" & rowFilter & ")"
        _subTable = subTable
        Dim openedBefore As Boolean = CBool(SettingsUtility.GetSettingsValue(_openedBeforeProperty, False))

        InitializeGrid(_dm.Database, Grid, _view, subTable, Not openedBefore)

        EnableContextMenuItems()
    End Sub

    Public Property Filter() As String
        Get
            Return _customFilter
        End Get
        Set(ByVal value As String)
            _customFilter = Replace(value, Chr(34), "'")
            If Not String.IsNullOrEmpty(_customFilter) Then
                Try
                    If Not String.IsNullOrEmpty(_origFilter) Then
                        _view.RowFilter = _origFilter & " AND (" & _customFilter & ")"
                    Else
                        _view.RowFilter = _customFilter
                    End If
                Catch ex As Exception
                    ErrorHandler.ShowError("Invalid expression.  Check the tooltips in the column headings for the fieldnames.  A standard SQL expression is expected.", "Filter Error", ex)
                    _view.RowFilter = _origFilter
                End Try
            Else
                _view.RowFilter = _origFilter
            End If
        End Set
    End Property

    Private Sub Grid_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Grid.DragDrop
        If Not _mouseDown Then
            Dim c As GridCopyContainer = DirectCast(e.Data.GetData(GetType(GridCopyContainer)), GridCopyContainer)
            _dm.Database.CopyRows(_view.Table, c.Rows, c.Database)
        End If
    End Sub

    Private Sub Grid_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Grid.DragEnter
        If Not _mouseDown Then
            e.Effect = DragDropEffects.None
            If e.Data.GetDataPresent(GetType(GridCopyContainer)) Then
                Dim c As GridCopyContainer = DirectCast(e.Data.GetData(GetType(GridCopyContainer)), GridCopyContainer)
                If c.Database IsNot _dm.Database AndAlso c.Rows.Length > 0 AndAlso c.Rows(0).Table.TableName = _view.Table.TableName Then
                    e.Effect = DragDropEffects.Copy
                End If
            End If
        End If
    End Sub

    Private Sub Grid_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Grid.KeyDown
        If e.Alt Then _keyDown = True
    End Sub

    Private Sub Grid_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Grid.KeyUp
        _keyDown = False
    End Sub

    Private Sub Grid_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Grid.MouseDown
        _mouseDown = True
    End Sub

    Private Sub Grid_MouseLeave(sender As Object, e As System.EventArgs) Handles Grid.MouseLeave
        _mouseDown = False
    End Sub

    Private Sub Grid_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Grid.MouseMove
        Dim test As DataGridView.HitTestInfo = Grid.HitTest(e.X, e.Y)
        If _mouseDown AndAlso test.RowIndex >= 0 AndAlso Grid.SelectedRows.Count > 0 Then
            Dim c As New GridCopyContainer With {.Database = _dm.Database}
            ReDim c.Rows(Grid.SelectedRows.Count - 1)
            For i As Integer = 0 To Grid.SelectedRows.Count - 1
                c.Rows(i) = DirectCast(Grid.SelectedRows(i).DataBoundItem, DataRowView).Row
            Next

            Grid.DoDragDrop(c, DragDropEffects.Copy)
        End If
        _mouseDown = False
    End Sub

    Private Sub Grid_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Grid.MouseUp
        _mouseDown = False
    End Sub

    Private Sub Grid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.SelectionChanged
        UpdateStatusBar()
    End Sub

    Private Sub Grid_UserAddedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Grid.UserAddedRow
        UpdateStatusBar()
    End Sub

    Private Sub Grid_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Grid.UserDeletedRow
        UpdateStatusBar()
    End Sub

    Private Sub SelectColumnsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectColumnsToolStripMenuItem.Click
        Dim curState As FormWindowState = Me.WindowState
        Dim frm As New ColumnSelectForm(_dm, Me.Grid, _subTable)
        frm.ShowDialog(Me)
        Me.Hide()
        Me.WindowState = FormWindowState.Normal
        LoadingForm.Show()
        InitializeGrid(_dm.Database, Grid, _view, _subTable)
        LoadingForm.Close()
        Me.Show()
        Me.WindowState = curState
    End Sub

    Private Sub GridForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        UpdateStatusBar()
        Me.Refresh()
    End Sub

    Private Sub GridForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SettingsUtility.SetSettingsValue(_heightProperty, Me.Height)
        SettingsUtility.SetSettingsValue(_widthProperty, Me.Width)
        SettingsUtility.SetSettingsValue(_topProperty, Me.Top)
        SettingsUtility.SetSettingsValue(_leftProperty, Me.Left)
        SettingsUtility.SetSettingsValue(_stateProperty, Me.WindowState)
        SettingsUtility.SetSettingsValue(_openedBeforeProperty, True)
        SaveColumnWidths()
    End Sub

    Private Sub GridForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadingForm.Close()

        Dim savedTop As Integer = SettingsUtility.GetSettingsValue(_topProperty, 0)
        Dim savedLeft As Integer = SettingsUtility.GetSettingsValue(_leftProperty, 0)
        If savedLeft > 0 Then
            Me.Left = savedLeft
        Else
            Me.Left = 0
        End If
        If savedTop > 0 Then
            Me.Top = savedTop
        Else
            Me.Top = 0
        End If

        Dim savedHeight As Integer = SettingsUtility.GetSettingsValue(_heightProperty, 0)
        Dim savedWidth As Integer = SettingsUtility.GetSettingsValue(_widthProperty, 0)
        If savedHeight = 0 Or savedWidth = 0 Then
            Me.Width = Math.Min(MainWindow.ClientRectangle.Width - 50, GetActualGridWidth(Grid))
            Me.Height = Math.Min(MainWindow.ClientRectangle.Height - 100, GetActualGridHeight(Grid))
        Else
            Me.Width = Math.Min(MainWindow.ClientRectangle.Width - Me.Left - 50, savedWidth)
            Me.Height = Math.Min(MainWindow.ClientRectangle.Height - Me.Top - 100, savedHeight)
        End If

        Dim savedState As FormWindowState = SettingsUtility.GetSettingsValue(_stateProperty, 0)
        If savedState <> FormWindowState.Minimized Then Me.WindowState = savedState
    End Sub

    Private Sub FilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterToolStripMenuItem.Click
        Dim frm As New CustomFilterForm(_dm)
        frm.Filter = Me.Filter
        If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then Me.Filter = frm.Filter
    End Sub

    Protected Sub UpdateStatusBar()
        MainWindow.StatusLabel.Text = Me.Text & ": "
        If Grid.SelectedCells.Count > 0 Then
            MainWindow.StatusLabel.Text &= Grid.SelectedCells(0).RowIndex + 1 & " of "
        End If
        MainWindow.StatusLabel.Text &= _view.Count & " Rows"
    End Sub

    Private Sub Form_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Not Me.Visible Then MainWindow.StatusLabel.Text = ""
    End Sub

    Private Sub SaveColumnWidths()
        For Each dc As DataGridViewColumn In Grid.Columns
            SetColumnWidth(_dm.Database, dc, _view.Table, _subTable)
        Next
        _dm.Database.SaveSchema()
    End Sub

    Private Sub RemoveFilterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveFilterToolStripMenuItem.Click
        Me.Filter = Nothing
    End Sub

    Private Sub FilterBySelectedValueToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FilterBySelectedValueToolStripMenuItem.Click
        FilterByValue(False)
    End Sub

    Private Sub FilterExcludingSelectedValueToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FilterExcludingSelectedValueToolStripMenuItem.Click
        FilterByValue(True)
    End Sub

    Private Sub FilterByValue(excludeValue As Boolean)
        If Grid.SelectedCells.Count = 1 Then
            Dim filterValue As Object = Grid.SelectedCells(0).Value
            Dim columnName As String = Grid.Columns(Grid.SelectedCells(0).ColumnIndex).DataPropertyName
            Dim dataType As Type = _view.Table.Columns(columnName).DataType
            Dim expressionOperator As String = "="

            If excludeValue Then expressionOperator = "<>"

            If dataType Is GetType(String) Then
                filterValue = "'" & CStr(filterValue) & "'"
            End If

            Dim filterExpression As String = columnName & expressionOperator & CStr(filterValue)
            If String.IsNullOrEmpty(Me.Filter) Then
                Me.Filter = filterExpression
            Else
                Me.Filter = Me.Filter & " AND " & filterExpression
            End If
        Else
            ErrorHandler.ShowWarning("Please select a single cell first.")
        End If
    End Sub

    Private Enum ApplyValueAction
        Add
        Multiply
        [Set]
    End Enum

    Private Sub ValueAddToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ValueAddToolStripMenuItem.Click
        ApplyValueToColumn(ApplyValueAction.Add)
    End Sub

    Private Sub ValueMultiplyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ValueMultiplyToolStripMenuItem.Click
        ApplyValueToColumn(ApplyValueAction.Multiply)
    End Sub

    Private Sub ValueSetToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ValueSetToolStripMenuItem.Click
        ApplyValueToColumn(ApplyValueAction.Set)
    End Sub

    Private Sub ApplyValueToColumn(applyAction As ApplyValueAction)
        If Grid.SelectedCells.Count <> 1 Then
            ErrorHandler.ShowWarning("Please select a single cell first.")
            Exit Sub
        End If

        Dim columnName As String = Grid.Columns(Grid.SelectedCells(0).ColumnIndex).DataPropertyName
        Dim dataType As Type = _view.Table.Columns(columnName).DataType

        If applyAction <> ApplyValueAction.Set AndAlso (dataType Is GetType(String) OrElse dataType Is GetType(Boolean)) Then
            ErrorHandler.ShowWarning("Cannot perform arithmetic operations on string or boolean fields.")
            Exit Sub
        End If

        Dim value As String = PromptForValue(applyAction, _view.Table.Columns(columnName).Caption)

        If Not String.IsNullOrEmpty(value) Then
            If dataType IsNot GetType(String) AndAlso dataType IsNot GetType(Boolean) Then
                If Not IsNumeric(value) Then
                    ErrorHandler.ShowWarning("Value must be numeric.")
                    Exit Sub
                End If
            End If

            For Each drv As DataRowView In _view
                Select Case applyAction
                    Case ApplyValueAction.Set
                        drv(columnName) = value
                    Case ApplyValueAction.Add
                        drv(columnName) += value
                    Case ApplyValueAction.Multiply
                        drv(columnName) *= value
                End Select
            Next
        End If
    End Sub

    Private Function PromptForValue(applyAction As ApplyValueAction, columnCaption As String) As String
        Dim prompt As String = Nothing
        Dim defaultResponse As String = Nothing

        Select Case applyAction
            Case ApplyValueAction.Add
                prompt = "Add the following value to all visible rows in the " & columnCaption & " column:"
                defaultResponse = "0"
            Case ApplyValueAction.Multiply
                prompt = "Multiply all visible rows in the " & columnCaption & " column by the following value:"
                defaultResponse = "1"
            Case ApplyValueAction.Set
                prompt = "Set all visible rows in the " & columnCaption & " column to the following value:"
                defaultResponse = ""
        End Select

        Return InputBox(prompt, "Apply Value to " & columnCaption, defaultResponse)
    End Function

    Protected Sub EnableContextMenuItems()
        If Not Grid.ReadOnly AndAlso (Grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect OrElse Grid.SelectionMode = DataGridViewSelectionMode.CellSelect) Then
            Me.ApplyValueToSelectedColumnToolStripMenuItem.Visible = True
            Me.FilterBySelectedValueToolStripMenuItem.Visible = True
            Me.FilterExcludingSelectedValueToolStripMenuItem.Visible = True
        Else
            Me.ApplyValueToSelectedColumnToolStripMenuItem.Visible = False
            Me.FilterBySelectedValueToolStripMenuItem.Visible = False
            Me.FilterExcludingSelectedValueToolStripMenuItem.Visible = False
        End If
    End Sub

End Class