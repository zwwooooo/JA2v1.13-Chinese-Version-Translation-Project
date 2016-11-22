<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColumnSelectForm
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
        Me.ColumnCheckList = New System.Windows.Forms.CheckedListBox
        Me.myCancelButton = New System.Windows.Forms.Button
        Me.myOKButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ColumnCheckList
        '
        Me.ColumnCheckList.CheckOnClick = True
        Me.ColumnCheckList.FormattingEnabled = True
        Me.ColumnCheckList.Location = New System.Drawing.Point(0, 0)
        Me.ColumnCheckList.Name = "ColumnCheckList"
        Me.ColumnCheckList.Size = New System.Drawing.Size(188, 184)
        Me.ColumnCheckList.TabIndex = 0
        '
        'myCancelButton
        '
        Me.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.myCancelButton.Location = New System.Drawing.Point(98, 190)
        Me.myCancelButton.Name = "myCancelButton"
        Me.myCancelButton.Size = New System.Drawing.Size(90, 24)
        Me.myCancelButton.TabIndex = 6
        Me.myCancelButton.Text = "Cancel"
        Me.myCancelButton.UseVisualStyleBackColor = True
        Me.myCancelButton.Visible = False
        '
        'myOKButton
        '
        Me.myOKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.myOKButton.Location = New System.Drawing.Point(2, 190)
        Me.myOKButton.Name = "myOKButton"
        Me.myOKButton.Size = New System.Drawing.Size(90, 24)
        Me.myOKButton.TabIndex = 5
        Me.myOKButton.Text = "OK"
        Me.myOKButton.UseVisualStyleBackColor = True
        Me.myOKButton.Visible = False
        '
        'ColumnSelectForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(189, 216)
        Me.Controls.Add(Me.myCancelButton)
        Me.Controls.Add(Me.myOKButton)
        Me.Controls.Add(Me.ColumnCheckList)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColumnSelectForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Visible Columns"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ColumnCheckList As System.Windows.Forms.CheckedListBox
    Friend WithEvents myCancelButton As System.Windows.Forms.Button
    Friend WithEvents myOKButton As System.Windows.Forms.Button
End Class
