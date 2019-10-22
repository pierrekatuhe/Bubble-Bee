<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintQR
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
        Me.qr_pic = New System.Windows.Forms.PictureBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        CType(Me.qr_pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'qr_pic
        '
        Me.qr_pic.Location = New System.Drawing.Point(9, 10)
        Me.qr_pic.Margin = New System.Windows.Forms.Padding(2)
        Me.qr_pic.Name = "qr_pic"
        Me.qr_pic.Size = New System.Drawing.Size(300, 300)
        Me.qr_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.qr_pic.TabIndex = 0
        Me.qr_pic.TabStop = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "PNG Image|*.png"
        '
        'PrintQR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(323, 324)
        Me.Controls.Add(Me.qr_pic)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "PrintQR"
        Me.Text = "PrintQR"
        CType(Me.qr_pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents qr_pic As PictureBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
