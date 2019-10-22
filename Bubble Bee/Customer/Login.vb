Imports System.Data.Odbc

Public Class Login
    Public cmd2 As OdbcCommand

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Button1.PerformClick()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        MenuStaff.Close()
        MenuCustomer.Close()
        Panel5.Visible = False
        Panel4.Visible = False
        Panel3.Visible = False
    End Sub

    Private Sub Login_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call Koneksi()
        Try
            cmd = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & txt_username.Text & "' AND password = '" & txt_password.Text & "'", conn)
            Using rd As OdbcDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    While rd.Read()
                        If rd!status.Equals("Suspend") Then
                            Panel5.Visible = True
                            Panel4.Visible = False
                            Panel3.Visible = False
                        Else
                            txt_akses.Text = rd!hak_akses
                            If txt_akses.Text = "Customer" Then
                                cmd2 = New OdbcCommand("SELECT * FROM tb_permainan WHERE status = 1 OR status = 5", conn)
                                Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                                    If rd2.HasRows = True Then
                                        txt_username.Text = ""
                                        txt_password.Text = ""
                                        txt_akses.Text = ""
                                        Ubahs("UPDATE tb_user SET last_login = NOW(), status = 'Suspend' where username = '" & rd!username & "'")
                                        MenuCustomer.txt_user.Text = rd!username
                                        MenuCustomer.Timer2.Enabled = False
                                        MenuCustomer.Timer3.Enabled = True
                                        'Form2.Show()
                                        MenuCustomer.Show()
                                    Else
                                        txt_username.Text = ""
                                        txt_password.Text = ""
                                        txt_akses.Text = ""
                                        Ubahs("UPDATE tb_user SET last_login = NOW(), status = 'Suspend' where username = '" & rd!username & "'")
                                        MenuCustomer.txt_user.Text = rd!username
                                        'Form2.Show()
                                        MenuCustomer.Show()
                                    End If
                                End Using
                            ElseIf txt_akses.Text = "Kasir" Then
                                Ubahs("UPDATE tb_user SET last_login = NOW(), status = 'Suspend' where username = '" & rd!username & "'")
                                MenuStaff.txt_user.Text = rd!username
                                MenuStaff.txt_akses.Text = rd!hak_akses
                                MenuStaff.txt_koin_user.Text = rd!koin
                                MenuStaff.btnRoulette.Visible = False
                                MenuStaff.history.Visible = False
                                MenuStaff.btnUser.Visible = True
                                MenuStaff.FormUser.Visible = False
                                MenuStaff.FormRoulette.Visible = False
                                MenuStaff.FormGantiPassword.Visible = False
                                MenuStaff.FormListUser.Visible = False
                                MenuStaff.Panel4.Visible = False
                                MenuStaff.FormHome.Visible = True
                                MenuStaff.btn_listUser.Visible = True
                                MenuStaff.btn_koin.Visible = True
                                MenuStaff.FormKoin.Visible = False
                                MenuStaff.FormReport.Visible = False
                                MenuStaff.FormDetailReport.Visible = False
                                MenuStaff.btn_setting.Visible = False
                                MenuStaff.btn_log.Visible = False
                                MenuStaff.btn_report.Visible = True
                                MenuStaff.cmb_akses.Visible = False
                                MenuStaff.Form_DetailPeriode.Visible = False
                                MenuStaff.Form_Log.Visible = False
                                MenuStaff.Form_Setting.Visible = False
                                MenuStaff.FormTotalPeriode.Visible = False
                                MenuStaff.panelbutton.Height = MenuStaff.btnHome.Height
                                MenuStaff.panelbutton.Top = MenuStaff.btnHome.Top
                                txt_username.Text = ""
                                txt_password.Text = ""
                                txt_akses.Text = ""
                                MenuStaff.Show()
                            ElseIf txt_akses.Text = "Operator" Then
                                Ubahs("UPDATE tb_user SET last_login = NOW(), status = 'Suspend' where username = '" & rd!username & "'")
                                MenuStaff.txt_user.Text = rd!username
                                MenuStaff.txt_akses.Text = rd!hak_akses
                                MenuStaff.btnRoulette.Visible = True
                                MenuStaff.history.Visible = True
                                MenuStaff.btnUser.Visible = False
                                MenuStaff.FormUser.Visible = False
                                MenuStaff.FormRoulette.Visible = False
                                MenuStaff.FormGantiPassword.Visible = False
                                MenuStaff.btn_listUser.Visible = False
                                MenuStaff.Panel4.Visible = False
                                MenuStaff.FormListUser.Visible = False
                                MenuStaff.FormHome.Visible = True
                                MenuStaff.btn_koin.Visible = False
                                MenuStaff.FormKoin.Visible = False
                                MenuStaff.FormReport.Visible = False
                                MenuStaff.btn_setting.Visible = False
                                MenuStaff.btn_log.Visible = False
                                MenuStaff.btn_report.Visible = False
                                MenuStaff.Form_DetailPeriode.Visible = False
                                MenuStaff.Form_Log.Visible = False
                                MenuStaff.Form_Setting.Visible = False
                                MenuStaff.FormTotalPeriode.Visible = False
                                MenuStaff.FormDetailReport.Visible = False
                                MenuStaff.panelbutton.Height = MenuStaff.btnHome.Height
                                MenuStaff.panelbutton.Top = MenuStaff.btnHome.Top
                                txt_username.Text = ""
                                txt_password.Text = ""
                                txt_akses.Text = ""
                                MenuStaff.Show()
                            ElseIf txt_akses.Text = "Owner" Then
                                Ubahs("UPDATE tb_user SET last_login = NOW(), status = 'Suspend' where username = '" & rd!username & "'")
                                MenuStaff.txt_user.Text = rd!username
                                MenuStaff.txt_akses.Text = rd!hak_akses
                                MenuStaff.btnRoulette.Visible = False
                                MenuStaff.history.Visible = False
                                MenuStaff.btnUser.Visible = True
                                MenuStaff.FormUser.Visible = False
                                MenuStaff.FormRoulette.Visible = False
                                MenuStaff.FormGantiPassword.Visible = False
                                MenuStaff.FormListUser.Visible = False
                                MenuStaff.Panel4.Visible = False
                                MenuStaff.FormHome.Visible = True
                                MenuStaff.btn_listUser.Visible = True
                                MenuStaff.btn_koin.Visible = True
                                MenuStaff.FormKoin.Visible = False
                                MenuStaff.FormReport.Visible = False
                                MenuStaff.FormDetailReport.Visible = False
                                MenuStaff.btn_setting.Visible = True
                                MenuStaff.btn_log.Visible = True
                                MenuStaff.btn_report.Visible = True
                                MenuStaff.cmb_akses.Visible = True
                                MenuStaff.Form_DetailPeriode.Visible = False
                                MenuStaff.Form_Log.Visible = False
                                MenuStaff.Form_Setting.Visible = False
                                MenuStaff.FormTotalPeriode.Visible = False
                                MenuStaff.panelbutton.Height = MenuStaff.btnHome.Height
                                MenuStaff.panelbutton.Top = MenuStaff.btnHome.Top
                                txt_username.Text = ""
                                txt_password.Text = ""
                                txt_akses.Text = ""
                                MenuStaff.Show()
                            End If
                        End If
                    End While
                Else
                    txt_username.Text = ""
                    txt_password.Text = ""
                    txt_akses.Text = ""
                    txt_username.Select()
                    Panel5.Visible = False
                    Panel4.Visible = False
                    Panel3.Visible = True
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txt_username_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_username.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub txt_password_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_password.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub btn_qr_Click(sender As Object, e As EventArgs) Handles btn_qr.Click
        Form1.Show()
    End Sub
End Class
