<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_akses = New System.Windows.Forms.TextBox()
        Me.pnl_atas = New System.Windows.Forms.Panel()
        Me.btn_close = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_password = New Bunifu.Framework.UI.BunifuMetroTextbox()
        Me.txt_username = New Bunifu.Framework.UI.BunifuMetroTextbox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btn_qr = New Bunifu.Framework.UI.BunifuThinButton2()
        Me.btn_login = New Bunifu.Framework.UI.BunifuThinButton2()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BunifuElipse1 = New Bunifu.Framework.UI.BunifuElipse(Me.components)
        Me.BunifuDragControl1 = New Bunifu.Framework.UI.BunifuDragControl(Me.components)
        Me.pnl_atas.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(29, 209)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Username"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(29, 270)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'txt_akses
        '
        Me.txt_akses.Location = New System.Drawing.Point(0, 386)
        Me.txt_akses.Name = "txt_akses"
        Me.txt_akses.Size = New System.Drawing.Size(19, 20)
        Me.txt_akses.TabIndex = 5
        Me.txt_akses.Visible = False
        '
        'pnl_atas
        '
        Me.pnl_atas.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnl_atas.Controls.Add(Me.btn_close)
        Me.pnl_atas.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_atas.Location = New System.Drawing.Point(0, 0)
        Me.pnl_atas.Name = "pnl_atas"
        Me.pnl_atas.Size = New System.Drawing.Size(312, 29)
        Me.pnl_atas.TabIndex = 6
        '
        'btn_close
        '
        Me.btn_close.BackgroundImage = Global.Bubble_Bee.My.Resources.Resources.btnClose
        Me.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_close.FlatAppearance.BorderSize = 0
        Me.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_close.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btn_close.Location = New System.Drawing.Point(283, 2)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(27, 23)
        Me.btn_close.TabIndex = 5
        Me.btn_close.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.Panel2.Controls.Add(Me.txt_password)
        Me.Panel2.Controls.Add(Me.txt_username)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.btn_qr)
        Me.Panel2.Controls.Add(Me.btn_login)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Location = New System.Drawing.Point(0, 27)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(312, 391)
        Me.Panel2.TabIndex = 7
        '
        'txt_password
        '
        Me.txt_password.BorderColorFocused = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txt_password.BorderColorIdle = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txt_password.BorderColorMouseHover = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txt_password.BorderThickness = 2
        Me.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_password.Font = New System.Drawing.Font("Century Gothic", 9.75!)
        Me.txt_password.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txt_password.isPassword = True
        Me.txt_password.Location = New System.Drawing.Point(32, 292)
        Me.txt_password.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(248, 34)
        Me.txt_password.TabIndex = 1
        Me.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txt_username
        '
        Me.txt_username.BorderColorFocused = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txt_username.BorderColorIdle = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txt_username.BorderColorMouseHover = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.txt_username.BorderThickness = 2
        Me.txt_username.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_username.Font = New System.Drawing.Font("Century Gothic", 9.75!)
        Me.txt_username.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txt_username.isPassword = False
        Me.txt_username.Location = New System.Drawing.Point(32, 230)
        Me.txt_username.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_username.Name = "txt_username"
        Me.txt_username.Size = New System.Drawing.Size(248, 34)
        Me.txt_username.TabIndex = 0
        Me.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Location = New System.Drawing.Point(32, 152)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(248, 50)
        Me.Panel5.TabIndex = 16
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Location = New System.Drawing.Point(32, 152)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(248, 50)
        Me.Panel4.TabIndex = 15
        Me.Panel4.Visible = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(280, 103)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(200, 100)
        Me.FlowLayoutPanel1.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Location = New System.Drawing.Point(32, 152)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(248, 45)
        Me.Panel3.TabIndex = 14
        Me.Panel3.Visible = False
        '
        'btn_qr
        '
        Me.btn_qr.ActiveBorderThickness = 1
        Me.btn_qr.ActiveCornerRadius = 20
        Me.btn_qr.ActiveFillColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btn_qr.ActiveForecolor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_qr.ActiveLineColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btn_qr.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_qr.BackgroundImage = CType(resources.GetObject("btn_qr.BackgroundImage"), System.Drawing.Image)
        Me.btn_qr.ButtonText = "QR Login"
        Me.btn_qr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_qr.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_qr.ForeColor = System.Drawing.Color.SeaGreen
        Me.btn_qr.IdleBorderThickness = 1
        Me.btn_qr.IdleCornerRadius = 20
        Me.btn_qr.IdleFillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_qr.IdleForecolor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_qr.IdleLineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_qr.Location = New System.Drawing.Point(164, 334)
        Me.btn_qr.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_qr.Name = "btn_qr"
        Me.btn_qr.Size = New System.Drawing.Size(117, 46)
        Me.btn_qr.TabIndex = 3
        Me.btn_qr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_login
        '
        Me.btn_login.ActiveBorderThickness = 1
        Me.btn_login.ActiveCornerRadius = 20
        Me.btn_login.ActiveFillColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btn_login.ActiveForecolor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_login.ActiveLineColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btn_login.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_login.BackgroundImage = CType(resources.GetObject("btn_login.BackgroundImage"), System.Drawing.Image)
        Me.btn_login.ButtonText = "Login"
        Me.btn_login.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_login.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_login.ForeColor = System.Drawing.Color.SeaGreen
        Me.btn_login.IdleBorderThickness = 1
        Me.btn_login.IdleCornerRadius = 20
        Me.btn_login.IdleFillColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_login.IdleForecolor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.btn_login.IdleLineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_login.Location = New System.Drawing.Point(32, 334)
        Me.btn_login.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(117, 46)
        Me.btn_login.TabIndex = 2
        Me.btn_login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.Bubble_Bee.My.Resources.Resources.logo3
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Location = New System.Drawing.Point(46, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(216, 141)
        Me.Panel1.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(202, 348)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 19)
        Me.Button1.TabIndex = 13
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BunifuElipse1
        '
        Me.BunifuElipse1.ElipseRadius = 5
        Me.BunifuElipse1.TargetControl = Me
        '
        'BunifuDragControl1
        '
        Me.BunifuDragControl1.Fixed = True
        Me.BunifuDragControl1.Horizontal = True
        Me.BunifuDragControl1.TargetControl = Me.pnl_atas
        Me.BunifuDragControl1.Vertical = True
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(312, 419)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_atas)
        Me.Controls.Add(Me.txt_akses)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.HelpButton = True
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bubble Bee"
        Me.pnl_atas.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_akses As TextBox
    Friend WithEvents pnl_atas As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BunifuElipse1 As Bunifu.Framework.UI.BunifuElipse
    Friend WithEvents BunifuDragControl1 As Bunifu.Framework.UI.BunifuDragControl
    Friend WithEvents btn_close As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btn_login As Bunifu.Framework.UI.BunifuThinButton2
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents btn_qr As Bunifu.Framework.UI.BunifuThinButton2
    Friend WithEvents txt_password As Bunifu.Framework.UI.BunifuMetroTextbox
    Friend WithEvents txt_username As Bunifu.Framework.UI.BunifuMetroTextbox
End Class

