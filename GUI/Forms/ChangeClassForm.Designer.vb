<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeClassForm
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
        Me.ItemClassListBox = New System.Windows.Forms.ListBox
        Me.myCancelButton = New System.Windows.Forms.Button
        Me.myOKButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Pick a new class for this item:"
        '
        'ItemClassListBox
        '
        Me.ItemClassListBox.FormattingEnabled = True
        Me.ItemClassListBox.Location = New System.Drawing.Point(4, 19)
        Me.ItemClassListBox.Name = "ItemClassListBox"
        Me.ItemClassListBox.Size = New System.Drawing.Size(186, 134)
        Me.ItemClassListBox.TabIndex = 0
        '
        'myCancelButton
        '
        Me.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.myCancelButton.Location = New System.Drawing.Point(100, 159)
        Me.myCancelButton.Name = "myCancelButton"
        Me.myCancelButton.Size = New System.Drawing.Size(90, 24)
        Me.myCancelButton.TabIndex = 8
        Me.myCancelButton.Text = "Cancel"
        Me.myCancelButton.UseVisualStyleBackColor = True
        Me.myCancelButton.Visible = False
        '
        'myOKButton
        '
        Me.myOKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.myOKButton.Location = New System.Drawing.Point(4, 159)
        Me.myOKButton.Name = "myOKButton"
        Me.myOKButton.Size = New System.Drawing.Size(90, 24)
        Me.myOKButton.TabIndex = 7
        Me.myOKButton.Text = "OK"
        Me.myOKButton.UseVisualStyleBackColor = True
        Me.myOKButton.Visible = False
        '
        'ChangeClassForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(193, 187)
        Me.Controls.Add(Me.myCancelButton)
        Me.Controls.Add(Me.myOKButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ItemClassListBox)
        Me.Name = "ChangeClassForm"
        Me.Text = "Change Item Class"
        Me.Controls.SetChildIndex(Me.ItemClassListBox, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.myOKButton, 0)
        Me.Controls.SetChildIndex(Me.myCancelButton, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ItemClassListBox As System.Windows.Forms.ListBox
    Friend WithEvents myCancelButton As System.Windows.Forms.Button
    Friend WithEvents myOKButton As System.Windows.Forms.Button
End Class
