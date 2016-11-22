<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GridForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Grid = New System.Windows.Forms.DataGridView()
        Me.GridContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterBySelectedValueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterExcludingSelectedValueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveFilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApplyValueToSelectedColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValueAddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValueMultiplyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValueSetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowDrop = True
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Grid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Grid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.Grid.ContextMenuStrip = Me.GridContextMenu
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EnableHeadersVisualStyles = False
        Me.Grid.Location = New System.Drawing.Point(0, 0)
        Me.Grid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Name = "Grid"
        Me.Grid.ReadOnly = True
        Me.Grid.RowHeadersVisible = False
        Me.Grid.RowHeadersWidth = 24
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.Size = New System.Drawing.Size(644, 374)
        Me.Grid.TabIndex = 0
        '
        'GridContextMenu
        '
        Me.GridContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectColumnsToolStripMenuItem, Me.FilterToolStripMenuItem, Me.FilterBySelectedValueToolStripMenuItem, Me.FilterExcludingSelectedValueToolStripMenuItem, Me.RemoveFilterToolStripMenuItem, Me.ApplyValueToSelectedColumnToolStripMenuItem})
        Me.GridContextMenu.Name = "ContextMenu"
        Me.GridContextMenu.Size = New System.Drawing.Size(263, 136)
        '
        'SelectColumnsToolStripMenuItem
        '
        Me.SelectColumnsToolStripMenuItem.Name = "SelectColumnsToolStripMenuItem"
        Me.SelectColumnsToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.SelectColumnsToolStripMenuItem.Text = "Select &Columns"
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.FilterToolStripMenuItem.Text = "Custom &Filter"
        '
        'FilterBySelectedValueToolStripMenuItem
        '
        Me.FilterBySelectedValueToolStripMenuItem.Name = "FilterBySelectedValueToolStripMenuItem"
        Me.FilterBySelectedValueToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.FilterBySelectedValueToolStripMenuItem.Text = "Filter by Selected &Value"
        '
        'FilterExcludingSelectedValueToolStripMenuItem
        '
        Me.FilterExcludingSelectedValueToolStripMenuItem.Name = "FilterExcludingSelectedValueToolStripMenuItem"
        Me.FilterExcludingSelectedValueToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.FilterExcludingSelectedValueToolStripMenuItem.Text = "Filter E&xcluding Selected Value"
        '
        'RemoveFilterToolStripMenuItem
        '
        Me.RemoveFilterToolStripMenuItem.Name = "RemoveFilterToolStripMenuItem"
        Me.RemoveFilterToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.RemoveFilterToolStripMenuItem.Text = "&Remove Filter"
        '
        'ApplyValueToSelectedColumnToolStripMenuItem
        '
        Me.ApplyValueToSelectedColumnToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValueAddToolStripMenuItem, Me.ValueMultiplyToolStripMenuItem, Me.ValueSetToolStripMenuItem})
        Me.ApplyValueToSelectedColumnToolStripMenuItem.Name = "ApplyValueToSelectedColumnToolStripMenuItem"
        Me.ApplyValueToSelectedColumnToolStripMenuItem.Size = New System.Drawing.Size(262, 22)
        Me.ApplyValueToSelectedColumnToolStripMenuItem.Text = "&Apply Value to Selected Column"
        '
        'ValueAddToolStripMenuItem
        '
        Me.ValueAddToolStripMenuItem.Name = "ValueAddToolStripMenuItem"
        Me.ValueAddToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ValueAddToolStripMenuItem.Text = "&Add"
        '
        'ValueMultiplyToolStripMenuItem
        '
        Me.ValueMultiplyToolStripMenuItem.Name = "ValueMultiplyToolStripMenuItem"
        Me.ValueMultiplyToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ValueMultiplyToolStripMenuItem.Text = "&Multiply"
        '
        'ValueSetToolStripMenuItem
        '
        Me.ValueSetToolStripMenuItem.Name = "ValueSetToolStripMenuItem"
        Me.ValueSetToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ValueSetToolStripMenuItem.Text = "&Set"
        '
        'GridForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 374)
        Me.Controls.Add(Me.Grid)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "GridForm"
        Me.ShowIcon = False
        Me.Text = "Items"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents GridContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectColumnsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterBySelectedValueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveFilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApplyValueToSelectedColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValueAddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValueMultiplyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValueSetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilterExcludingSelectedValueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
