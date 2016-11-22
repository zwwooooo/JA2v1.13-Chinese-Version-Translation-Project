<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewItemForm
    Inherits SimpleFormBase

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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ItemClassComboBox = New System.Windows.Forms.ComboBox
        Me.ItemNameTextBox = New System.Windows.Forms.TextBox
        Me.myCancelButton = New System.Windows.Forms.Button
        Me.myOKButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Type in a name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Select a Class:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ItemClassComboBox
        '
        Me.ItemClassComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ItemClassComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ItemClassComboBox.FormattingEnabled = True
        Me.ItemClassComboBox.Location = New System.Drawing.Point(91, 29)
        Me.ItemClassComboBox.Name = "ItemClassComboBox"
        Me.ItemClassComboBox.Size = New System.Drawing.Size(214, 21)
        Me.ItemClassComboBox.TabIndex = 1
        '
        'ItemNameTextBox
        '
        Me.ItemNameTextBox.Location = New System.Drawing.Point(91, 3)
        Me.ItemNameTextBox.MaxLength = 80
        Me.ItemNameTextBox.Name = "ItemNameTextBox"
        Me.ItemNameTextBox.Size = New System.Drawing.Size(214, 20)
        Me.ItemNameTextBox.TabIndex = 0
        '
        'myCancelButton
        '
        Me.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.myCancelButton.Location = New System.Drawing.Point(215, 56)
        Me.myCancelButton.Name = "myCancelButton"
        Me.myCancelButton.Size = New System.Drawing.Size(90, 24)
        Me.myCancelButton.TabIndex = 12
        Me.myCancelButton.Text = "Cancel"
        Me.myCancelButton.UseVisualStyleBackColor = True
        Me.myCancelButton.Visible = False
        '
        'myOKButton
        '
        Me.myOKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.myOKButton.Location = New System.Drawing.Point(119, 56)
        Me.myOKButton.Name = "myOKButton"
        Me.myOKButton.Size = New System.Drawing.Size(90, 24)
        Me.myOKButton.TabIndex = 11
        Me.myOKButton.Text = "OK"
        Me.myOKButton.UseVisualStyleBackColor = True
        Me.myOKButton.Visible = False
        '
        'NewItemForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 81)
        Me.Controls.Add(Me.myCancelButton)
        Me.Controls.Add(Me.myOKButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ItemNameTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ItemClassComboBox)
        Me.Name = "NewItemForm"
        Me.Text = "Create New Item"
        Me.Controls.SetChildIndex(Me.ItemClassComboBox, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.ItemNameTextBox, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.myOKButton, 0)
        Me.Controls.SetChildIndex(Me.myCancelButton, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ItemClassComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ItemNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents myCancelButton As System.Windows.Forms.Button
    Friend WithEvents myOKButton As System.Windows.Forms.Button
End Class
