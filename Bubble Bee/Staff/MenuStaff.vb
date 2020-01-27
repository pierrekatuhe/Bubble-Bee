Imports QRCoder

Public Class MenuStaff
    Dim rs As New Resizer
    Dim time_limit As Integer
    Public cmd8 As Odbc.OdbcCommand
    Public rd8 As Odbc.OdbcDataReader
    Private Sub UserToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FormUser.Show()
    End Sub

    Private Sub RouletteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FormRoulette.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Login.Show()
    End Sub

    Private Sub MenuStaff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Login.Close()
        rs.FindAllControls(Me)
        WindowState = FormWindowState.Maximized
        FormUser.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        btn_finish.Enabled = False
        FormStaffKoin.Visible = False
        FormHome.Visible = True
        FormBonus.Visible = False
        panelbutton.Height = btnHome.Height
        panelbutton.Top = btnHome.Top
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub MenuStaff_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    'Form Home
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnHome.Click
        panelbutton.Height = btnHome.Height
        panelbutton.Top = btnHome.Top
        FormUser.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        Panel4.Visible = False
        FormBonus.Visible = False
        FormListUser.Visible = False
        FormHome.Visible = True
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    'Button Close Form Staff
    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles btn_close.Click
        If btn_finish.Enabled = True Then
            MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
        ElseIf Timer1.Enabled = True Then
            MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
        ElseIf Timer1.Enabled = False Then
            If btn_start.Enabled = False Then
                MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
            Else
                Ubahs("UPDATE tb_user SET status = 'Available' where username = '" & txt_user.Text & "'")
                Me.Close()
            End If
        Else
            Ubahs("UPDATE tb_user SET status = 'Available' where username = '" & txt_user.Text & "'")
            Me.Close()
        End If
    End Sub

    'Form Manajemen User
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUser.Click
        panelbutton.Height = btnUser.Height
        panelbutton.Top = btnUser.Top
        FormUser.Visible = True
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        Panel4.Visible = False
        FormHome.Visible = False
        FormBonus.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        Reload()
        conn.Close()
    End Sub

    Private Sub txt_searchAddUser_KeyUp(sender As Object, e As KeyEventArgs) Handles txt_searchAddUser.KeyUp
        Try
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, nama AS Nama, hak_akses AS 'Hak Akses', status AS Status FROM tb_user WHERE (hak_akses = 'Customer' OR hak_akses = 'Staff Koin') AND username LIKE '%" & txt_searchAddUser.Text & "%'", BunifuCustomDataGrid1)
            Else
                Read("SELECT username AS Username, nama AS Nama, hak_akses AS 'Hak Akses', status AS Status FROM tb_user WHERE username LIKE '%" & txt_searchAddUser.Text & "%'", BunifuCustomDataGrid1)
            End If
            bersih()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Reload()
        Try
            Dim CurrentRow As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, nama AS Nama, hak_akses AS 'Hak Akses', status AS Status FROM tb_user WHERE hak_akses = 'Customer'", BunifuCustomDataGrid1)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_user where hak_akses = 'Customer'", conn)
            Else
                Read("SELECT username AS Username, nama AS Nama, hak_akses AS 'Hak Akses', status AS Status FROM tb_user", BunifuCustomDataGrid1)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_user", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid1.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                CurrentRow += 1
            End While
            bersih()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BunifuCustomDataGrid1_Click(sender As Object, e As EventArgs) Handles BunifuCustomDataGrid1.Click
        txt_username.Text = BunifuCustomDataGrid1.CurrentRow.Cells(1).Value
        txt_username.Enabled = False
        txt_nama.Text = BunifuCustomDataGrid1.CurrentRow.Cells(2).Value
        Dim akses_tabel = BunifuCustomDataGrid1.CurrentRow.Cells(3).Value
        If akses_tabel.Equals("Customer") Then
            cmb_akses.selectedIndex = 0
        ElseIf akses_tabel.Equals("Staff Koin") Then
            cmb_akses.selectedIndex = 1
        ElseIf akses_tabel.Equals("Operator") Then
            cmb_akses.selectedIndex = 2
        ElseIf akses_tabel.Equals("Kasir") Then
            cmb_akses.selectedIndex = 3
        End If
        Dim stat = BunifuCustomDataGrid1.CurrentRow.Cells(4).Value
        If stat.Equals("Available") Then
            btn_available.Enabled = False
            btn_suspend.Enabled = True
        Else
            btn_available.Enabled = True
            btn_suspend.Enabled = False
        End If
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            Create("INSERT INTO tb_user (username, nama, password, hak_akses, koin, created_by, status) VALUES ('" & txt_username.Text & "','" _
                   & txt_nama.Text & "','" & txt_password.Text & "','" _
                   & cmb_akses.selectedValue & "','0','" & txt_user.Text & "', 'Available')")
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Tambah User','" _
                   & txt_user.Text & "',NOW())")
            Reload()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Try
            Ubah("UPDATE tb_user SET nama = '" & txt_nama.Text & "', hak_akses = '" & cmb_akses.selectedValue _
                 & "' where username = '" & txt_username.Text & "'")
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Update User','" _
                   & txt_user.Text & "',NOW())")
            Reload()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        If txt_username.Text = "" Then
            If MsgBox("Anda yakin ingin menghapus seluruh user?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Delete("DELETE FROM tb_user WHERE hak_akses = 'Customer'")
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Delete All User','" _
                           & txt_user.Text & "',NOW())")
                    Reload()
                    conn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            If MsgBox("Anda yakin ingin menghapus user " & txt_username.Text & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Delete("DELETE FROM tb_user WHERE username = '" & txt_username.Text & "'")
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Delete User','" _
                           & txt_user.Text & "',NOW())")
                    Reload()
                    conn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub bersih()
        txt_username.Enabled = True
        txt_username.Text = ""
        txt_password.Text = ""
        txt_nama.Text = ""
        cmb_akses.selectedIndex = 0
        btn_available.Enabled = True
        btn_suspend.Enabled = True
    End Sub

    Private Sub btn_batal_Click(sender As Object, e As EventArgs) Handles btn_batal.Click
        bersih()
    End Sub

    Private Sub btn_available_Click(sender As Object, e As EventArgs) Handles btn_available.Click
        If txt_username.Text = "" Then
            Try
                Ubah("UPDATE tb_user SET status = 'Available' where hak_akses = 'Customer'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                Reload()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Try
                Ubah("UPDATE tb_user SET status = 'Available' where username = '" & txt_username.Text & "'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                Reload()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btn_suspend_Click(sender As Object, e As EventArgs) Handles btn_suspend.Click
        If txt_username.Text = "" Then
            Try
                Ubah("UPDATE tb_user SET status = 'Suspend' where hak_akses = 'Customer'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                Reload()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Try
                Ubah("UPDATE tb_user SET status = 'Suspend' where username = '" & txt_username.Text & "'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                Reload()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        conn.Close()
    End Sub

    'Form Ganti Password
    Private Sub btn_ganti_password_Click(sender As Object, e As EventArgs) Handles btn_ganti_password.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = True
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub btn_print_Click(sender As Object, e As EventArgs) Handles btn_print.Click
        If txt_username.Text = "" Then
            MsgBox("Please select a user!")
        Else
            Call Koneksi()
            Try
                cmd = New Odbc.OdbcCommand("SELECT username, password FROM tb_user WHERE username LIKE '" & txt_username.Text & "'", conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows = True Then
                    Dim qrData = rd!username & "," & rd!password
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    PrintQR.qr_pic.Image = code.GetGraphic(6)
                    PrintQR.Show()
                Else
                    MsgBox("User not found!")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Close()
        End If

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Ubah("UPDATE tb_user SET password = '" & TextBox1.Text _
                 & "' where username = '" & TextBox2.Text & "'")
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Password User','" _
                   & txt_user.Text & "',NOW())")
            TextBox1.Text = ""
            TextBox2.Text = ""
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        FormUser.Visible = True
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
    End Sub

    'Form List User
    Private Sub btn_listUser_Click(sender As Object, e As EventArgs) Handles btn_listUser.Click
        panelbutton.Height = btn_listUser.Height
        panelbutton.Top = btn_listUser.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        Panel4.Visible = False
        FormHome.Visible = False
        FormBonus.Visible = False
        FormKoin.Visible = False
        FormListUser.Visible = True
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadUser()
        conn.Close()
    End Sub

    Private Sub txt_searchUser_KeyUp(sender As Object, e As EventArgs) Handles txt_searchUser.KeyUp
        Try
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where (hak_akses = 'Customer' OR hak_akses = 'Staff Koin') AND username LIKE '%" & txt_searchUser.Text & "%'", BunifuCustomDataGrid4)
            Else
                Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where username LIKE '%" & txt_searchUser.Text & "%'", BunifuCustomDataGrid4)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub ReloadUser()
        Try
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where (hak_akses = 'Customer' OR hak_akses = 'Staff Koin')", BunifuCustomDataGrid4)
                If Not BunifuDropdown1.Items.Contains("Customer") And Not BunifuDropdown1.Items.Contains("Staff Koin") Then
                    BunifuDropdown1.AddItem("Customer")
                    BunifuDropdown1.AddItem("Staff Koin")
                End If
            Else
                Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user", BunifuCustomDataGrid4)
                If Not BunifuDropdown1.Items.Contains("Customer") And Not BunifuDropdown1.Items.Contains("Staff Koin") And Not BunifuDropdown1.Items.Contains("Operator") And Not BunifuDropdown1.Items.Contains("Kasir") Then
                    BunifuDropdown1.AddItem("Customer")
                    BunifuDropdown1.AddItem("Staff Koin")
                    BunifuDropdown1.AddItem("Operator")
                    BunifuDropdown1.AddItem("Kasir")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Form Manajemen Koin
    Private Sub btn_koin_Click(sender As Object, e As EventArgs) Handles btn_koin.Click
        panelbutton.Height = btn_koin.Height
        panelbutton.Top = btn_koin.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        FormBonus.Visible = False
        FormListUser.Visible = False
        FormHome.Visible = False
        FormKoin.Visible = True
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        ReloadKoin()
        txt_koin_user.Enabled = False
        txt_koinUser.Enabled = False
        FormTimer.Visible = False
        conn.Close()
    End Sub

    Private Sub BunifuThinButton215_Click(sender As Object, e As EventArgs) Handles BunifuThinButton215.Click
        ReloadKoin()
    End Sub

    Private Sub txt_searchKoin_KeyUp(sender As Object, e As EventArgs) Handles txt_searchKoin.KeyUp
        Try
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, koin AS Koin, koin_update AS 'Koin Update', koin_update_by AS 'Last Update' FROM tb_user where (hak_akses = 'Customer' OR hak_akses = 'Staff Koin') AND username LIKE '%" & txt_searchKoin.Text & "%'", BunifuCustomDataGrid3)
            Else
                Read("SELECT username AS Username, koin AS Koin, koin_update AS 'Koin Update', koin_update_by AS 'Last Update' FROM tb_user where username LIKE '%" & txt_searchKoin.Text & "%'", BunifuCustomDataGrid3)
            End If
            BersihKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub ReloadKoin()
        Try
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username AS Username, koin AS Koin, koin_update AS 'Koin Update', koin_update_by AS 'Last Update' FROM tb_user where (hak_akses = 'Customer' OR hak_akses = 'Staff Koin')", BunifuCustomDataGrid3)
            Else
                Read("SELECT username AS Username, koin AS Koin, koin_update AS 'Koin Update', koin_update_by AS 'Last Update' FROM tb_user", BunifuCustomDataGrid3)
            End If
            BersihKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BunifuCustomDataGrid3_Click(sender As Object, e As EventArgs) Handles BunifuCustomDataGrid3.Click
        txt_koinUser.Text = BunifuCustomDataGrid3.CurrentRow.Cells(0).Value
        txt_koinUser.Enabled = False
        txt_tempKoin.Text = BunifuCustomDataGrid3.CurrentRow.Cells(1).Value
    End Sub

    Private Sub btn_tambahKoin_Click(sender As Object, e As EventArgs) Handles btn_tambahKoin.Click
        Try
            If txt_koinUser.Text = "" Then
                MsgBox("Pilih User terlebih dahulu!", MsgBoxStyle.Information)
            Else
                If txt_akses.Text = "Kasir" Then
                    If Val(txt_koin_user.Text) < Val(txt_koin.Text) Then
                        MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                    Else
                        Dim total_koin = Val(txt_tempKoin.Text) + Val(txt_koin.Text)
                        Dim sisa_koin_kasir = Val(txt_koin_user.Text) - Val(txt_koin.Text)
                        Ubah("UPDATE tb_user SET koin = '" & total_koin _
                             & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                        Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                             & "' where username = '" & txt_user.Text & "'")
                        Creates("INSERT INTO tb_koin (in_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                        txt_koin_user.Text = sisa_koin_kasir
                        txt_saldo.Text = sisa_koin_kasir
                    End If
                Else
                    Dim total_koin = Val(txt_tempKoin.Text) + Val(txt_koin.Text)
                    Ubah("UPDATE tb_user SET koin = '" & total_koin _
                         & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                    Creates("INSERT INTO tb_koin (in_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                End If
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Tambah Koin User','" _
                           & txt_user.Text & "',NOW())")
                ReloadKoin()
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_kurangKoin_Click(sender As Object, e As EventArgs) Handles btn_kurangKoin.Click
        If txt_koinUser.Text = "" Then
            MsgBox("Pilih User terlebih dahulu!", MsgBoxStyle.Information)
        Else
            Call Koneksi()
            Try
                cmd = New Odbc.OdbcCommand("SELECT tb_permainan.status AS status, tb_permainan.keterangan AS keterangan FROM tb_bet, tb_permainan WHERE username='" & txt_koinUser.Text & "' AND tb_bet.id_permainan=tb_permainan.id_permainan AND tb_bet.id_permainan IN (select max(id_permainan) FROM tb_permainan)", conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows = True Then
                    If rd!status = 1 Or rd!status = 5 Or rd!status = 4 Then
                        MsgBox("User '" & txt_koinUser.Text & "' sedang bermain!", MsgBoxStyle.Information)
                    ElseIf rd!status = 2 Then
                        If rd!keterangan = "Bonus" Then
                            MsgBox("User '" & txt_koinUser.Text & "' sedang bermain!", MsgBoxStyle.Information)
                        Else
                            If txt_akses.Text = "Kasir" Then
                                If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                                    MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                                Else
                                    Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                                    Dim sisa_koin_kasir = Val(txt_koin_user.Text) + Val(txt_koin.Text)
                                    Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                         & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                                    Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                         & "' where username = '" & txt_user.Text & "'")
                                    Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                                    ReloadKoin()
                                    txt_koin_user.Text = sisa_koin_kasir
                                    txt_saldo.Text = sisa_koin_kasir
                                End If
                            Else
                                If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                                    MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                                Else
                                    Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                                    Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                         & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                                    Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                                    ReloadKoin()
                                End If
                            End If
                            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                                       & txt_user.Text & "',NOW())")
                        End If
                    Else
                        If txt_akses.Text = "Kasir" Then
                            If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                                MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                            Else
                                Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                                Dim sisa_koin_kasir = Val(txt_koin_user.Text) + Val(txt_koin.Text)
                                Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                     & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                                Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                     & "' where username = '" & txt_user.Text & "'")
                                Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                                ReloadKoin()
                                txt_koin_user.Text = sisa_koin_kasir
                                txt_saldo.Text = sisa_koin_kasir
                            End If
                        Else
                            If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                                MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                            Else
                                Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                                Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                     & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                                Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                                ReloadKoin()
                            End If
                        End If
                        Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                                   & txt_user.Text & "',NOW())")
                    End If
                Else
                    If txt_akses.Text = "Kasir" Then
                        If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                            MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                        Else
                            Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                            Dim sisa_koin_kasir = Val(txt_koin_user.Text) + Val(txt_koin.Text)
                            Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                 & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                            Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                 & "' where username = '" & txt_user.Text & "'")
                            Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                            ReloadKoin()
                            txt_koin_user.Text = sisa_koin_kasir
                            txt_saldo.Text = sisa_koin_kasir
                        End If
                    Else
                        If Val(txt_tempKoin.Text) < Val(txt_koin.Text) Then
                            MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                        Else
                            Dim total_koin = Val(txt_tempKoin.Text) - Val(txt_koin.Text)
                            Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                 & "', koin_update = NOW(), koin_update_by = '" & txt_user.Text & "' where username = '" & txt_koinUser.Text & "'")
                            Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_koin.Text & "', NOW(), NOW(), '" & txt_user.Text & "', '" & txt_koinUser.Text & "')")
                            ReloadKoin()
                        End If
                    End If
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                               & txt_user.Text & "',NOW())")
                End If
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BersihKoin()
        txt_koinUser.Text = ""
        txt_koin.Text = 0
    End Sub

    'Form Report
    Private Sub btn_report_Click(sender As Object, e As EventArgs) Handles btn_report.Click
        panelbutton.Height = btn_report.Height
        panelbutton.Top = btn_report.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        FormBonus.Visible = False
        FormListUser.Visible = False
        FormHome.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = True
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadReport()
        conn.Close()
    End Sub

    Private Sub txt_searchReport_KeyUp(sender As Object, e As EventArgs) Handles txt_searchReport.KeyUp
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username_receiver AS 'Username', in_koin AS 'In Koin', out_koin AS 'Out Koin', time AS 'Time', username_giver AS 'Create' FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') AND username_receiver LIKE '%" & txt_searchReport.Text & "%'", BunifuCustomDataGrid5)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') AND username_receiver LIKE '%" & txt_searchReport.Text & "%'", conn)
            Else
                Read("SELECT in_koin AS 'In Koin', out_koin AS 'Out Koin', date AS Date FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver LIKE '%" & txt_searchReport.Text & "%'", BunifuCustomDataGrid5)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver LIKE '%" & txt_searchReport.Text & "%'", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid5.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(1) Then
                    total_in += rd!in_koin
                End If
                If Not rd.IsDBNull(2) Then
                    total_out += rd!out_koin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalInReport.Text = total_in
            txt_totalOutReport.Text = total_out
            txt_totalReport.Text = total_koin
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub ReloadReport()
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username_receiver AS 'Username', in_koin AS 'In Koin', out_koin AS 'Out Koin', time AS 'Time', username_giver AS 'Create' FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer')", BunifuCustomDataGrid5)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer')", conn)
            Else
                Read("SELECT in_koin AS 'In Koin', out_koin AS 'Out Koin', date AS Date FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin)", BunifuCustomDataGrid5)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin)", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid5.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(1) Then
                    total_in += rd!in_koin
                End If
                If Not rd.IsDBNull(2) Then
                    total_out += rd!out_koin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalInReport.Text = total_in
            txt_totalOutReport.Text = total_out
            txt_totalReport.Text = total_koin
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_detailReport_Click(sender As Object, e As EventArgs) Handles btn_detailReport.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = True
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadDetailReport()
        conn.Close()
    End Sub

    Private Sub txt_searchDetailReport_KeyUp(sender As Object, e As EventArgs) Handles txt_searchDetailReport.KeyUp
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username_receiver AS 'Username', in_koin AS 'In Koin', out_koin AS 'Out Koin', time AS 'Time', username_giver AS 'Create' FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') AND username_receiver LIKE '%" & txt_searchDetailReport.Text & "%' GROUP BY username_receiver", BunifuCustomDataGrid6)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') AND username_receiver LIKE '%" & txt_searchDetailReport.Text & "%'", conn)
            Else
                Read("SELECT in_koin AS 'In Koin', out_koin AS 'Out Koin', date AS Date FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver LIKE '%" & txt_searchDetailReport.Text & "%' GROUP BY username_receiver", BunifuCustomDataGrid6)
                cmd = New Odbc.OdbcCommand("SELECT * FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver LIKE '%" & txt_searchDetailReport.Text & "%'", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid6.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(1) Then
                    total_in += rd!in_koin
                End If
                If Not rd.IsDBNull(2) Then
                    total_out += rd!out_koin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalDetail.Text = total_koin
            txt_totalInDetail.Text = total_in
            txt_totalOutDetail.Text = total_out
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub ReloadDetailReport()
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username_receiver AS 'Username', SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', time AS 'Time', username_giver AS 'Create' FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') GROUP BY username_receiver", BunifuCustomDataGrid6)
                cmd = New Odbc.OdbcCommand("SELECT SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin' FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer' GROUP BY username_receiver)", conn)
            Else
                Read("SELECT username_receiver AS 'Username', SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', time AS 'Time', username_giver AS 'Create' FROM tb_koin WHERE date = (SELECT max(date) FROM tb_koin) GROUP BY username_receiver", BunifuCustomDataGrid6)
                cmd = New Odbc.OdbcCommand("SELECT SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin' FROM tb_koin WHERE date = (SELECT max(date) from tb_koin GROUP BY username_receiver)", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid6.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(0) Then
                    total_in += rd!InKoin
                End If
                If Not rd.IsDBNull(1) Then
                    total_out += rd!OutKoin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalDetail.Text = total_koin
            txt_totalInDetail.Text = total_in
            txt_totalOutDetail.Text = total_out
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_backReport_Click(sender As Object, e As EventArgs) Handles btn_backReport.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = True
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub btn_totalPeriode_Click(sender As Object, e As EventArgs) Handles btn_totalPeriode.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = True
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub btn_detailTotalPeriode_Click(sender As Object, e As EventArgs) Handles btn_detailTotalPeriode.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = True
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub btn_prosesPeriode_Click(sender As Object, e As EventArgs) Handles btn_prosesPeriode.Click
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT date AS Date, SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker1.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker2.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') GROUP BY date", BunifuCustomDataGrid7)
                cmd = New Odbc.OdbcCommand("SELECT date AS Date, SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker1.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker2.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') GROUP BY date", conn)
            Else
                Read("SELECT date AS Date, SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker1.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker2.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Kasir') GROUP BY date", BunifuCustomDataGrid7)
                cmd = New Odbc.OdbcCommand("SELECT date AS Date, SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker1.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker2.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Kasir') GROUP BY date", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid7.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(1) Then
                    total_in += rd!InKoin
                End If
                If Not rd.IsDBNull(2) Then
                    total_out += rd!OutKoin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalPeriode.Text = total_koin
            txt_totalInPeriode.Text = total_in
            txt_totalOutPeriode.Text = total_out
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_prosesDetail_Click(sender As Object, e As EventArgs) Handles btn_prosesDetail.Click
        Try
            Dim CurrentRow As Integer = 0
            Dim total_in As Integer = 0
            Dim total_out As Integer = 0
            Dim total_koin As Integer = 0
            If txt_akses.Text = "Kasir" Then
                Read("SELECT username_receiver AS User, SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker3.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker4.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') GROUP BY username_receiver", BunifuCustomDataGrid8)
                cmd = New Odbc.OdbcCommand("SELECT username_receiver AS User, SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker3.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker4.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer') GROUP BY username_receiver", conn)
            Else
                Read("SELECT username_receiver AS User, SUM(in_koin) AS 'In Koin', SUM(out_koin) AS 'Out Koin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker3.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker4.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Kasir') GROUP BY username_receiver", BunifuCustomDataGrid8)
                cmd = New Odbc.OdbcCommand("SELECT username_receiver AS User, SUM(in_koin) AS 'InKoin', SUM(out_koin) AS 'OutKoin', SUM(in_koin)-SUM(out_koin) AS 'Total' FROM tb_koin WHERE date BETWEEN STR_TO_DATE('" & DateTimePicker3.Value.ToShortDateString & "','%m/%d/%Y') AND STR_TO_DATE('" & DateTimePicker4.Value.ToShortDateString & "','%m/%d/%Y') AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Kasir') GROUP BY username_receiver", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid8.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                If Not rd.IsDBNull(1) Then
                    total_in += rd!InKoin
                End If
                If Not rd.IsDBNull(2) Then
                    total_out += rd!OutKoin
                End If
                CurrentRow += 1
            End While
            total_koin = total_in - total_out
            txt_totalPeriodeDetail.Text = total_koin
            txt_inPeriodeDetail.Text = total_in
            txt_outPeriodeDetail.Text = total_out
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_backPeriode_Click(sender As Object, e As EventArgs) Handles btn_backPeriode.Click
        FormUser.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = True
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub btn_proses_Click(sender As Object, e As EventArgs) Handles btn_proses.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = True
        Label_date.Text = System.DateTime.Now.ToString("yyyy/MM/dd")
        Label_time.Text = System.DateTime.Now.ToString("HH:mm:ss")
        FormTimer.Visible = False
        ReloadProses()
        conn.Close()
    End Sub

    Private Sub ReloadProses()
        Call Koneksi()
        Try
            If txt_akses.Text = "Kasir" Then
                cmd = New Odbc.OdbcCommand("SELECT SUM(in_koin) AS 'in', SUM(out_koin) AS 'out', username_giver FROM tb_koin WHERE date = (SELECT max(date) from tb_koin) AND username_receiver IN (SELECT username FROM tb_user WHERE hak_akses='Customer')", conn)
            Else
                cmd = New Odbc.OdbcCommand("SELECT SUM(in_koin) AS 'in', SUM(out_koin) AS 'out', username_giver FROM tb_koin WHERE date = (SELECT max(date) from tb_koin)", conn)
            End If
            rd = cmd.ExecuteReader
            While rd.Read()
                txt_prosesIn.Text = rd!in
                txt_prosesOut.Text = rd!out
                txt_prosesTotal.Text = Val(txt_prosesIn.Text) - Val(txt_prosesOut.Text)
                txt_prosesKasir.Text = rd!username_giver
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_backProses_Click(sender As Object, e As EventArgs) Handles btn_backProses.Click
        panelbutton.Height = btn_report.Height
        panelbutton.Top = btn_report.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        FormBonus.Visible = False
        FormListUser.Visible = False
        FormHome.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = True
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadReport()
        conn.Close()
    End Sub

    'Form Roulette
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnRoulette.Click
        panelbutton.Height = btnRoulette.Height
        panelbutton.Top = btnRoulette.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = True
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        If FormBonus.Visible = True Then
            FormRoulette.Visible = False
            FormBonus.Visible = True
        Else
            FormRoulette.Visible = True
            FormBonus.Visible = False
        End If
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormHome.Visible = False
        FormReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        GetSetting()
    End Sub

    Private Sub GetSetting()
        Call Koneksi()
        Try
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_setting", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                time_limit = rd!time_limit
                Label7.Text = time_limit
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub bersih2()
        btn_0.Enabled = False
        btn_1.Enabled = False
        btn_2.Enabled = False
        btn_3.Enabled = False
        btn_4.Enabled = False
        btn_5.Enabled = False
        btn_6.Enabled = False
        btn_7.Enabled = False
        btn_8.Enabled = False
        btn_9.Enabled = False
        btn_10.Enabled = False
        btn_11.Enabled = False
        btn_12.Enabled = False
        btn_13.Enabled = False
        btn_14.Enabled = False
        btn_15.Enabled = False
        btn_16.Enabled = False
        btn_17.Enabled = False
        btn_18.Enabled = False
        btn_19.Enabled = False
        btn_20.Enabled = False
        btn_21.Enabled = False
        btn_22.Enabled = False
        btn_23.Enabled = False
        btn_24.Enabled = False
        btn_25.Enabled = False
        btn_26.Enabled = False
        btn_27.Enabled = False
        btn_28.Enabled = False
        btn_29.Enabled = False
        btn_30.Enabled = False
        btn_31.Enabled = False
        btn_32.Enabled = False
        btn_33.Enabled = False
        btn_34.Enabled = False
        btn_35.Enabled = False
        btn_36.Enabled = False
        btn_start.Enabled = True
        btn_finish.Enabled = False
        btn_bonus.Enabled = False
        btn_lock.Enabled = False
        Label7.Text = time_limit
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label7.Text = Val(Label7.Text) - 1
        If Label7.Text <= 0 Then
            Label7.Text = 0
            btn_0.Enabled = True
            btn_1.Enabled = True
            btn_2.Enabled = True
            btn_3.Enabled = True
            btn_4.Enabled = True
            btn_5.Enabled = True
            btn_6.Enabled = True
            btn_7.Enabled = True
            btn_8.Enabled = True
            btn_9.Enabled = True
            btn_10.Enabled = True
            btn_11.Enabled = True
            btn_12.Enabled = True
            'btn_13.Enabled = True
            'btn_14.Enabled = True
            'btn_15.Enabled = True
            'btn_16.Enabled = True
            'btn_17.Enabled = True
            'btn_18.Enabled = True
            'btn_19.Enabled = True
            'btn_20.Enabled = True
            'btn_21.Enabled = True
            'btn_22.Enabled = True
            'btn_23.Enabled = True
            'btn_24.Enabled = True
            'btn_25.Enabled = True
            'btn_26.Enabled = True
            'btn_27.Enabled = True
            'btn_28.Enabled = True
            'btn_29.Enabled = True
            'btn_30.Enabled = True
            'btn_31.Enabled = True
            'btn_32.Enabled = True
            'btn_33.Enabled = True
            'btn_34.Enabled = True
            'btn_35.Enabled = True
            'btn_36.Enabled = True
            btn_finish.Enabled = True
            btn_bonus.Enabled = True
        End If
    End Sub

    Private Sub btn_start_Click(sender As Object, e As EventArgs) Handles btn_start.Click
        Try
            btn_start.Enabled = False
            'btn_lock.Enabled = True
            txt_game.Text = ""
            Creates("INSERT INTO tb_permainan (status) VALUES (1)")
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_permainan WHERE status = 2", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                Ubahs("UPDATE tb_permainan SET status = 0 WHERE status = 2")
            End If
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_permainan WHERE status = 3", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                Ubahs("UPDATE tb_permainan SET status = 0 WHERE status = 3")
            End If
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_permainan WHERE status = 5", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                Ubahs("UPDATE tb_permainan SET status = 0 WHERE status = 5")
            End If
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Mulai Permainan','" _
                   & txt_user.Text & "',NOW())")
            Timer1.Enabled = True
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_lock_Click(sender As Object, e As EventArgs) Handles btn_lock.Click
        If Timer1.Enabled = True Then
            Timer1.Enabled = False
            btn_0.Enabled = False
            btn_1.Enabled = False
            btn_2.Enabled = False
            btn_3.Enabled = False
            btn_4.Enabled = False
            btn_5.Enabled = False
            btn_6.Enabled = False
            btn_7.Enabled = False
            btn_8.Enabled = False
            btn_9.Enabled = False
            btn_10.Enabled = False
            btn_11.Enabled = False
            btn_12.Enabled = False
            btn_13.Enabled = False
            btn_14.Enabled = False
            btn_15.Enabled = False
            btn_16.Enabled = False
            btn_17.Enabled = False
            btn_18.Enabled = False
            btn_19.Enabled = False
            btn_20.Enabled = False
            btn_21.Enabled = False
            btn_22.Enabled = False
            btn_23.Enabled = False
            btn_24.Enabled = False
            btn_25.Enabled = False
            btn_26.Enabled = False
            btn_27.Enabled = False
            btn_28.Enabled = False
            btn_29.Enabled = False
            btn_30.Enabled = False
            btn_31.Enabled = False
            btn_32.Enabled = False
            btn_33.Enabled = False
            btn_34.Enabled = False
            btn_35.Enabled = False
            btn_36.Enabled = False
            btn_start.Enabled = False
            btn_finish.Enabled = False
            btn_bonus.Enabled = False
            Try
                Ubahs("UPDATE tb_permainan SET status = 5 WHERE status = 1")
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Timer1.Enabled = True
            Try
                Ubahs("UPDATE tb_permainan SET status = 1 WHERE status = 5")
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btn_finish_Click(sender As Object, e As EventArgs) Handles btn_finish.Click
        Try
            If txt_game.Text = "" Then
                MsgBox("Pilih angka terlebih dahulu!", MsgBoxStyle.Information)
            Else
                Ubahs("UPDATE tb_permainan SET status = 2, angka = '" & txt_game.Text _
                 & "', keterangan = 'Normal', time = NOW() WHERE status = 6")
                btn_start.Enabled = True
                btn_finish.Enabled = False
                Timer1.Enabled = False
                bersih2()
            End If
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Selesai Permainan','" _
                   & txt_user.Text & "',NOW())")
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_1_Click(sender As Object, e As EventArgs) Handles btn_1.Click
        input_game()
        btn_1.Enabled = False
        txt_game.Text = "1"
    End Sub

    Private Sub input_game()
        btn_0.Enabled = True
        btn_1.Enabled = True
        btn_2.Enabled = True
        btn_3.Enabled = True
        btn_4.Enabled = True
        btn_5.Enabled = True
        btn_6.Enabled = True
        btn_7.Enabled = True
        btn_8.Enabled = True
        btn_9.Enabled = True
        btn_10.Enabled = True
        btn_11.Enabled = True
        btn_12.Enabled = True
        'btn_13.Enabled = True
        'btn_14.Enabled = True
        'btn_15.Enabled = True
        'btn_16.Enabled = True
        'btn_17.Enabled = True
        'btn_18.Enabled = True
        'btn_19.Enabled = True
        'btn_20.Enabled = True
        'btn_21.Enabled = True
        'btn_22.Enabled = True
        'btn_23.Enabled = True
        'btn_24.Enabled = True
        'btn_25.Enabled = True
        'btn_26.Enabled = True
        'btn_27.Enabled = True
        'btn_28.Enabled = True
        'btn_29.Enabled = True
        'btn_30.Enabled = True
        'btn_31.Enabled = True
        'btn_32.Enabled = True
        'btn_33.Enabled = True
        'btn_34.Enabled = True
        'btn_35.Enabled = True
        'btn_36.Enabled = True
    End Sub

    Private Sub btn_2_Click(sender As Object, e As EventArgs) Handles btn_2.Click
        input_game()
        btn_2.Enabled = False
        txt_game.Text = "2"
    End Sub

    Private Sub btn_3_Click(sender As Object, e As EventArgs) Handles btn_3.Click
        input_game()
        btn_3.Enabled = False
        txt_game.Text = "3"
    End Sub

    Private Sub btn_4_Click(sender As Object, e As EventArgs) Handles btn_4.Click
        input_game()
        btn_4.Enabled = False
        txt_game.Text = "4"
    End Sub

    Private Sub btn_5_Click(sender As Object, e As EventArgs) Handles btn_5.Click
        input_game()
        btn_5.Enabled = False
        txt_game.Text = "5"
    End Sub

    Private Sub btn_6_Click(sender As Object, e As EventArgs) Handles btn_6.Click
        input_game()
        btn_6.Enabled = False
        txt_game.Text = "6"
    End Sub

    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click
        input_game()
        btn_7.Enabled = False
        txt_game.Text = "7"
    End Sub

    Private Sub btn_8_Click(sender As Object, e As EventArgs) Handles btn_8.Click
        input_game()
        btn_8.Enabled = False
        txt_game.Text = "8"
    End Sub

    Private Sub btn_9_Click(sender As Object, e As EventArgs) Handles btn_9.Click
        input_game()
        btn_9.Enabled = False
        txt_game.Text = "9"
    End Sub

    Private Sub btn_10_Click(sender As Object, e As EventArgs) Handles btn_10.Click
        input_game()
        btn_10.Enabled = False
        txt_game.Text = "10"
    End Sub

    Private Sub btn_11_Click(sender As Object, e As EventArgs) Handles btn_11.Click
        input_game()
        btn_11.Enabled = False
        txt_game.Text = "11"
    End Sub

    Private Sub btn_12_Click(sender As Object, e As EventArgs) Handles btn_12.Click
        input_game()
        btn_12.Enabled = False
        txt_game.Text = "12"
    End Sub

    Private Sub btn_13_Click(sender As Object, e As EventArgs) Handles btn_13.Click
        input_game()
        btn_13.Enabled = False
        txt_game.Text = "13"
    End Sub

    Private Sub btn_14_Click(sender As Object, e As EventArgs) Handles btn_14.Click
        input_game()
        btn_14.Enabled = False
        txt_game.Text = "14"
    End Sub

    Private Sub btn_15_Click(sender As Object, e As EventArgs) Handles btn_15.Click
        input_game()
        btn_15.Enabled = False
        txt_game.Text = "15"
    End Sub

    Private Sub btn_16_Click(sender As Object, e As EventArgs) Handles btn_16.Click
        input_game()
        btn_16.Enabled = False
        txt_game.Text = "16"
    End Sub

    Private Sub btn_17_Click(sender As Object, e As EventArgs) Handles btn_17.Click
        input_game()
        btn_17.Enabled = False
        txt_game.Text = "17"
    End Sub

    Private Sub btn_18_Click(sender As Object, e As EventArgs) Handles btn_18.Click
        input_game()
        btn_18.Enabled = False
        txt_game.Text = "18"
    End Sub

    Private Sub btn_19_Click(sender As Object, e As EventArgs) Handles btn_19.Click
        input_game()
        btn_19.Enabled = False
        txt_game.Text = "19"
    End Sub

    Private Sub btn_20_Click(sender As Object, e As EventArgs) Handles btn_20.Click
        input_game()
        btn_20.Enabled = False
        txt_game.Text = "20"
    End Sub

    Private Sub btn_21_Click(sender As Object, e As EventArgs) Handles btn_21.Click
        input_game()
        btn_21.Enabled = False
        txt_game.Text = "21"
    End Sub

    Private Sub btn_22_Click(sender As Object, e As EventArgs) Handles btn_22.Click
        input_game()
        btn_22.Enabled = False
        txt_game.Text = "22"
    End Sub

    Private Sub btn_23_Click(sender As Object, e As EventArgs) Handles btn_23.Click
        input_game()
        btn_23.Enabled = False
        txt_game.Text = "23"
    End Sub

    Private Sub btn_24_Click(sender As Object, e As EventArgs) Handles btn_24.Click
        input_game()
        btn_24.Enabled = False
        txt_game.Text = "24"
    End Sub

    Private Sub btn_25_Click(sender As Object, e As EventArgs) Handles btn_25.Click
        input_game()
        btn_25.Enabled = False
        txt_game.Text = "25"
    End Sub

    Private Sub btn_26_Click(sender As Object, e As EventArgs) Handles btn_26.Click
        input_game()
        btn_26.Enabled = False
        txt_game.Text = "26"
    End Sub

    Private Sub btn_27_Click(sender As Object, e As EventArgs) Handles btn_27.Click
        input_game()
        btn_27.Enabled = False
        txt_game.Text = "27"
    End Sub

    Private Sub btn_28_Click(sender As Object, e As EventArgs) Handles btn_28.Click
        input_game()
        btn_28.Enabled = False
        txt_game.Text = "28"
    End Sub

    Private Sub btn_29_Click(sender As Object, e As EventArgs) Handles btn_29.Click
        input_game()
        btn_29.Enabled = False
        txt_game.Text = "29"
    End Sub

    Private Sub btn_30_Click(sender As Object, e As EventArgs) Handles btn_30.Click
        input_game()
        btn_30.Enabled = False
        txt_game.Text = "30"
    End Sub

    Private Sub btn_31_Click(sender As Object, e As EventArgs) Handles btn_31.Click
        input_game()
        btn_31.Enabled = False
        txt_game.Text = "31"
    End Sub

    Private Sub btn_32_Click(sender As Object, e As EventArgs) Handles btn_32.Click
        input_game()
        btn_32.Enabled = False
        txt_game.Text = "32"
    End Sub

    Private Sub btn_33_Click(sender As Object, e As EventArgs) Handles btn_33.Click
        input_game()
        btn_33.Enabled = False
        txt_game.Text = "33"
    End Sub

    Private Sub btn_34_Click(sender As Object, e As EventArgs) Handles btn_34.Click
        input_game()
        btn_34.Enabled = False
        txt_game.Text = "34"
    End Sub

    Private Sub btn_35_Click(sender As Object, e As EventArgs) Handles btn_35.Click
        input_game()
        btn_35.Enabled = False
        txt_game.Text = "35"
    End Sub

    Private Sub btn_36_Click(sender As Object, e As EventArgs) Handles btn_36.Click
        input_game()
        btn_36.Enabled = False
        txt_game.Text = "36"
    End Sub

    'Form Bonus
    Private Sub input_bonus()
        btn_bonus_0.Enabled = True
        btn_bonus_1.Enabled = True
        btn_bonus_2.Enabled = True
        btn_bonus_3.Enabled = True
        btn_bonus_4.Enabled = True
        btn_bonus_5.Enabled = True
        btn_bonus_6.Enabled = True
        btn_bonus_7.Enabled = True
        btn_bonus_8.Enabled = True
        btn_bonus_9.Enabled = True
        btn_bonus_10.Enabled = True
        btn_bonus_11.Enabled = True
        btn_bonus_12.Enabled = True
        'btn_bonus_13.Enabled = True
        'btn_bonus_14.Enabled = True
        'btn_bonus_15.Enabled = True
        'btn_bonus_16.Enabled = True
        'btn_bonus_17.Enabled = True
        'btn_bonus_18.Enabled = True
        'btn_bonus_19.Enabled = True
        'btn_bonus_20.Enabled = True
        'btn_bonus_21.Enabled = True
        'btn_bonus_22.Enabled = True
        'btn_bonus_23.Enabled = True
        'btn_bonus_24.Enabled = True
        'btn_bonus_25.Enabled = True
        'btn_bonus_26.Enabled = True
        'btn_bonus_27.Enabled = True
        'btn_bonus_28.Enabled = True
        'btn_bonus_29.Enabled = True
        'btn_bonus_30.Enabled = True
        'btn_bonus_31.Enabled = True
        'btn_bonus_32.Enabled = True
        'btn_bonus_33.Enabled = True
        'btn_bonus_34.Enabled = True
        'btn_bonus_35.Enabled = True
        'btn_bonus_36.Enabled = True
        'btn_anjing.Enabled = True
        'btn_ayam.Enabled = True
        'btn_monyet.Enabled = True
        'btn_kambing.Enabled = True
        'btn_kuda.Enabled = True
        'btn_ular.Enabled = True
        'btn_naga.Enabled = True
        'btn_kelinci.Enabled = True
        'btn_macan.Enabled = True
        'btn_kerbau.Enabled = True
        'btn_tikus.Enabled = True
        'btn_babi.Enabled = True
        btn_input_bonus.Enabled = True
    End Sub

    Private Sub btn_bonus_1_Click(sender As Object, e As EventArgs) Handles btn_bonus_1.Click
        input_bonus()
        btn_bonus_1.Enabled = False
        txt_bonus.Text = "1"
    End Sub

    Private Sub btn_bonus_2_Click(sender As Object, e As EventArgs) Handles btn_bonus_2.Click
        input_bonus()
        btn_bonus_2.Enabled = False
        txt_bonus.Text = "2"
    End Sub

    Private Sub btn_bonus_3_Click(sender As Object, e As EventArgs) Handles btn_bonus_3.Click
        input_bonus()
        btn_bonus_3.Enabled = False
        txt_bonus.Text = "3"
    End Sub

    Private Sub btn_bonus_4_Click(sender As Object, e As EventArgs) Handles btn_bonus_4.Click
        input_bonus()
        btn_bonus_4.Enabled = False
        txt_bonus.Text = "4"
    End Sub

    Private Sub btn_bonus_5_Click(sender As Object, e As EventArgs) Handles btn_bonus_5.Click
        input_bonus()
        btn_bonus_5.Enabled = False
        txt_bonus.Text = "5"
    End Sub

    Private Sub btn_bonus_6_Click(sender As Object, e As EventArgs) Handles btn_bonus_6.Click
        input_bonus()
        btn_bonus_6.Enabled = False
        txt_bonus.Text = "6"
    End Sub

    Private Sub btn_bonus_7_Click(sender As Object, e As EventArgs) Handles btn_bonus_7.Click
        input_bonus()
        btn_bonus_7.Enabled = False
        txt_bonus.Text = "7"
    End Sub

    Private Sub btn_bonus_8_Click(sender As Object, e As EventArgs) Handles btn_bonus_8.Click
        input_bonus()
        btn_bonus_8.Enabled = False
        txt_bonus.Text = "8"
    End Sub

    Private Sub btn_bonus_9_Click(sender As Object, e As EventArgs) Handles btn_bonus_9.Click
        input_bonus()
        btn_bonus_9.Enabled = False
        txt_bonus.Text = "9"
    End Sub

    Private Sub btn_bonus_10_Click(sender As Object, e As EventArgs) Handles btn_bonus_10.Click
        input_bonus()
        btn_bonus_10.Enabled = False
        txt_bonus.Text = "10"
    End Sub

    Private Sub btn_bonus_11_Click(sender As Object, e As EventArgs) Handles btn_bonus_11.Click
        input_bonus()
        btn_bonus_11.Enabled = False
        txt_bonus.Text = "11"
    End Sub

    Private Sub btn_bonus_12_Click(sender As Object, e As EventArgs) Handles btn_bonus_12.Click
        input_bonus()
        btn_bonus_12.Enabled = False
        txt_bonus.Text = "12"
    End Sub

    Private Sub btn_bonus_13_Click(sender As Object, e As EventArgs) Handles btn_bonus_13.Click
        input_bonus()
        btn_bonus_13.Enabled = False
        txt_bonus.Text = "13"
    End Sub

    Private Sub btn_bonus_14_Click(sender As Object, e As EventArgs) Handles btn_bonus_14.Click
        input_bonus()
        btn_bonus_14.Enabled = False
        txt_bonus.Text = "14"
    End Sub

    Private Sub btn_bonus_15_Click(sender As Object, e As EventArgs) Handles btn_bonus_15.Click
        input_bonus()
        btn_bonus_15.Enabled = False
        txt_bonus.Text = "15"
    End Sub

    Private Sub btn_bonus_16_Click(sender As Object, e As EventArgs) Handles btn_bonus_16.Click
        input_bonus()
        btn_bonus_16.Enabled = False
        txt_bonus.Text = "16"
    End Sub

    Private Sub btn_bonus_17_Click(sender As Object, e As EventArgs) Handles btn_bonus_17.Click
        input_bonus()
        btn_bonus_17.Enabled = False
        txt_bonus.Text = "17"
    End Sub

    Private Sub btn_bonus_18_Click(sender As Object, e As EventArgs) Handles btn_bonus_18.Click
        input_bonus()
        btn_bonus_18.Enabled = False
        txt_bonus.Text = "18"
    End Sub

    Private Sub btn_bonus_19_Click(sender As Object, e As EventArgs) Handles btn_bonus_19.Click
        input_bonus()
        btn_bonus_19.Enabled = False
        txt_bonus.Text = "19"
    End Sub

    Private Sub btn_bonus_20_Click(sender As Object, e As EventArgs) Handles btn_bonus_20.Click
        input_bonus()
        btn_bonus_20.Enabled = False
        txt_bonus.Text = "20"
    End Sub

    Private Sub btn_bonus_21_Click(sender As Object, e As EventArgs) Handles btn_bonus_21.Click
        input_bonus()
        btn_bonus_21.Enabled = False
        txt_bonus.Text = "21"
    End Sub

    Private Sub btn_bonus_22_Click(sender As Object, e As EventArgs) Handles btn_bonus_22.Click
        input_bonus()
        btn_bonus_22.Enabled = False
        txt_bonus.Text = "22"
    End Sub

    Private Sub btn_bonus_23_Click(sender As Object, e As EventArgs) Handles btn_bonus_23.Click
        input_bonus()
        btn_bonus_23.Enabled = False
        txt_bonus.Text = "23"
    End Sub

    Private Sub btn_bonus_24_Click(sender As Object, e As EventArgs) Handles btn_bonus_24.Click
        input_bonus()
        btn_bonus_24.Enabled = False
        txt_bonus.Text = "24"
    End Sub

    Private Sub btn_bonus_25_Click(sender As Object, e As EventArgs) Handles btn_bonus_25.Click
        input_bonus()
        btn_bonus_25.Enabled = False
        txt_bonus.Text = "25"
    End Sub

    Private Sub btn_bonus_26_Click(sender As Object, e As EventArgs) Handles btn_bonus_26.Click
        input_bonus()
        btn_bonus_26.Enabled = False
        txt_bonus.Text = "26"
    End Sub

    Private Sub btn_bonus_27_Click(sender As Object, e As EventArgs) Handles btn_bonus_27.Click
        input_bonus()
        btn_bonus_27.Enabled = False
        txt_bonus.Text = "27"
    End Sub

    Private Sub btn_bonus_28_Click(sender As Object, e As EventArgs) Handles btn_bonus_28.Click
        input_bonus()
        btn_bonus_28.Enabled = False
        txt_bonus.Text = "28"
    End Sub

    Private Sub btn_bonus_29_Click(sender As Object, e As EventArgs) Handles btn_bonus_29.Click
        input_bonus()
        btn_bonus_29.Enabled = False
        txt_bonus.Text = "29"
    End Sub

    Private Sub btn_bonus_30_Click(sender As Object, e As EventArgs) Handles btn_bonus_30.Click
        input_bonus()
        btn_bonus_30.Enabled = False
        txt_bonus.Text = "30"
    End Sub

    Private Sub btn_bonus_31_Click(sender As Object, e As EventArgs) Handles btn_bonus_31.Click
        input_bonus()
        btn_bonus_31.Enabled = False
        txt_bonus.Text = "31"
    End Sub

    Private Sub btn_bonus_32_Click(sender As Object, e As EventArgs) Handles btn_bonus_32.Click
        input_bonus()
        btn_bonus_32.Enabled = False
        txt_bonus.Text = "32"
    End Sub

    Private Sub btn_bonus_33_Click(sender As Object, e As EventArgs) Handles btn_bonus_33.Click
        input_bonus()
        btn_bonus_33.Enabled = False
        txt_bonus.Text = "33"
    End Sub

    Private Sub btn_bonus_34_Click(sender As Object, e As EventArgs) Handles btn_bonus_34.Click
        input_bonus()
        btn_bonus_34.Enabled = False
        txt_bonus.Text = "34"
    End Sub

    Private Sub btn_bonus_35_Click(sender As Object, e As EventArgs) Handles btn_bonus_35.Click
        input_bonus()
        btn_bonus_35.Enabled = False
        txt_bonus.Text = "35"
    End Sub

    Private Sub btn_bonus_36_Click(sender As Object, e As EventArgs) Handles btn_bonus_36.Click
        input_bonus()
        btn_bonus_36.Enabled = False
        txt_bonus.Text = "36"
    End Sub

    Private Sub btn_anjing_Click(sender As Object, e As EventArgs) Handles btn_anjing.Click
        input_bonus()
        btn_anjing.Enabled = False
        txt_bonus.Text = "Anjing"
    End Sub

    Private Sub btn_ayam_Click(sender As Object, e As EventArgs) Handles btn_ayam.Click
        input_bonus()
        btn_ayam.Enabled = False
        txt_bonus.Text = "Ayam"
    End Sub

    Private Sub btn_monyet_Click(sender As Object, e As EventArgs) Handles btn_monyet.Click
        input_bonus()
        btn_monyet.Enabled = False
        txt_bonus.Text = "Monyet"
    End Sub

    Private Sub btn_kambing_Click(sender As Object, e As EventArgs) Handles btn_kambing.Click
        input_bonus()
        btn_kambing.Enabled = False
        txt_bonus.Text = "Kambing"
    End Sub

    Private Sub btn_kuda_Click(sender As Object, e As EventArgs) Handles btn_kuda.Click
        input_bonus()
        btn_kuda.Enabled = False
        txt_bonus.Text = "Kuda"
    End Sub

    Private Sub btn_ular_Click(sender As Object, e As EventArgs) Handles btn_ular.Click
        input_bonus()
        btn_ular.Enabled = False
        txt_bonus.Text = "Ular"
    End Sub

    Private Sub btn_naga_Click(sender As Object, e As EventArgs) Handles btn_naga.Click
        input_bonus()
        btn_naga.Enabled = False
        txt_bonus.Text = "Naga"
    End Sub

    Private Sub btn_kelinci_Click(sender As Object, e As EventArgs) Handles btn_kelinci.Click
        input_bonus()
        btn_kelinci.Enabled = False
        txt_bonus.Text = "Kelinci"
    End Sub

    Private Sub btn_macan_Click(sender As Object, e As EventArgs) Handles btn_macan.Click
        input_bonus()
        btn_macan.Enabled = False
        txt_bonus.Text = "Macan"
    End Sub

    Private Sub btn_kerbau_Click(sender As Object, e As EventArgs) Handles btn_kerbau.Click
        input_bonus()
        btn_kerbau.Enabled = False
        txt_bonus.Text = "Kerbau"
    End Sub

    Private Sub btn_tikus_Click(sender As Object, e As EventArgs) Handles btn_tikus.Click
        input_bonus()
        btn_tikus.Enabled = False
        txt_bonus.Text = "Tikus"
    End Sub

    Private Sub btn_babi_Click(sender As Object, e As EventArgs) Handles btn_babi.Click
        input_bonus()
        btn_babi.Enabled = False
        txt_bonus.Text = "Babi"
    End Sub

    Private Sub btn_input_bonus_Click(sender As Object, e As EventArgs) Handles btn_input_bonus.Click
        Try
            If txt_history_bonus.Text = "" Then
                txt_history_bonus.Text += txt_bonus.Text
            Else
                txt_history_bonus.Text += ", " + txt_bonus.Text
            End If
            Creates("INSERT INTO tb_permainan (status, bonus, time) VALUES (4, '" & txt_bonus.Text & "', NOW())")
            btn_finish_bonus.Enabled = True
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_bonus_Click(sender As Object, e As EventArgs) Handles btn_bonus.Click
        Try
            Ubahs("UPDATE tb_permainan SET status = 2, keterangan = 'Bonus', time = NOW() WHERE status = 6")
            btn_start.Enabled = True
            btn_finish.Enabled = False
            Timer1.Enabled = False
            bersih2()
            FormRoulette.Visible = False
            FormHome.Visible = False
            Panel4.Visible = False
            FormBonus.Visible = True
            txt_history_bonus.Text = ""
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Bonus Permainan','" _
                   & txt_user.Text & "',NOW())")
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_finish_bonus_Click(sender As Object, e As EventArgs) Handles btn_finish_bonus.Click
        Ubahs("UPDATE tb_permainan SET status = 3 WHERE status = 4")
        txt_bonus.Text = ""
        btn_input_bonus.Enabled = False
        btn_finish_bonus.Enabled = False
        FormBonus.Visible = False
        FormRoulette.Visible = True
        Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Selesai Bonus','" _
                   & txt_user.Text & "',NOW())")
        conn.Close()
    End Sub

    'Form History
    Private Sub history_Click(sender As Object, e As EventArgs) Handles history.Click
        panelbutton.Height = history.Height
        panelbutton.Top = history.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = True
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormReport.Visible = False
        FormKoin.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        Reload2()
        conn.Close()
    End Sub

    Private Sub Reload2()
        Try
            Dim CurrentRow As Integer = 0
            Read("SELECT time AS Time, angka AS Angka, bonus AS Bonus, keterangan AS Keterangan FROM tb_permainan WHERE DATE(time) = CURDATE() ORDER BY time DESC", BunifuCustomDataGrid2)
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_permainan", conn)
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid2.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                CurrentRow += 1
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Logout
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If btn_finish.Enabled = True Then
            MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
        ElseIf Timer1.Enabled = True Then
            MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
        ElseIf Timer1.Enabled = False Then
            If btn_start.Enabled = False Then
                MsgBox("Silahkan akhiri permainan terlebih dahulu!", MsgBoxStyle.Information)
            Else
                Ubahs("UPDATE tb_user SET status = 'Available' where username = '" & txt_user.Text & "'")
                Hide()
                Login.Panel4.Visible = False
                Login.Panel3.Visible = False
                Login.Show()
            End If
        Else
            Ubahs("UPDATE tb_user SET status = 'Available' where username = '" & txt_user.Text & "'")
            Hide()
            Login.Panel4.Visible = False
            Login.Panel3.Visible = False
            Login.Show()
        End If
    End Sub

    'Setting
    Private Sub btn_setting_Click(sender As Object, e As EventArgs) Handles btn_setting.Click
        panelbutton.Height = btn_setting.Height
        panelbutton.Top = btn_setting.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormReport.Visible = False
        FormKoin.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = True
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadSetting()
        conn.Close()
    End Sub

    Private Sub ReloadSetting()
        Call Koneksi()
        Try
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_setting", conn)
            rd = cmd.ExecuteReader
            While rd.Read()
                txt_hadiah2D.Text = rd!hadiah_2d
                txt_maxbet2D.Text = rd!maxbet_2d
                txt_hadiahHewan.Text = rd!hadiah_hewan
                txt_maxbetHewan.Text = rd!maxbet_hewan
                txt_hadiahBK.Text = rd!hadiah_bk
                txt_maxbetBK.Text = rd!maxbet_bk
                txt_hadiahGG.Text = rd!hadiah_gg
                txt_maxbetGG.Text = rd!maxbet_gg
                txt_hadiahWarna.Text = rd!hadiah_warna
                txt_maxbetWarna.Text = rd!maxbet_warna
                txt_hadiah4Warna.Text = rd!hadiah_4warna
                txt_maxbet4Warna.Text = rd!maxbet_4warna
                txt_hadiah12Item.Text = rd!hadiah_12item
                txt_maxbet12Item.Text = rd!maxbet_12item
                txt_timeLimit.Text = rd!time_limit
                txt_hadiahBonus.Text = rd!bonus
                txt_bonusAngka.Text = rd!bonus_angka
                txt_bonusHewan.Text = rd!bonus_hewan
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_submit_Click(sender As Object, e As EventArgs) Handles btn_submit.Click
        Try
            Ubah("UPDATE tb_setting SET hadiah_2d = '" & txt_hadiah2D.Text & "', maxbet_2d = '" & txt_maxbet2D.Text _
                 & "', hadiah_hewan = '" & txt_hadiahHewan.Text & "', maxbet_hewan = '" & txt_maxbetHewan.Text _
                 & "', hadiah_bk = '" & txt_hadiahBK.Text & "', maxbet_bk = '" & txt_maxbetBK.Text _
                 & "', hadiah_gg = '" & txt_hadiahGG.Text & "', maxbet_gg = '" & txt_maxbetGG.Text _
                 & "', hadiah_warna = '" & txt_hadiahWarna.Text & "', maxbet_warna = '" & txt_maxbetWarna.Text _
                 & "', hadiah_4warna = '" & txt_hadiah4Warna.Text & "', maxbet_4warna = '" & txt_maxbet4Warna.Text _
                 & "', hadiah_12item = '" & txt_hadiah12Item.Text & "', maxbet_12item = '" & txt_maxbet12Item.Text _
                 & "', time_limit = '" & txt_timeLimit.Text & "', bonus = '" & txt_hadiahBonus.Text _
                 & "', bonus_angka = '" & txt_bonusAngka.Text & "', bonus_hewan = '" & txt_bonusHewan.Text & "'")
            ReloadSetting()
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Setting','" _
                   & txt_user.Text & "',NOW())")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    'Log Activity
    Private Sub btn_log_Click(sender As Object, e As EventArgs) Handles btn_log.Click
        panelbutton.Height = btn_log.Height
        panelbutton.Top = btn_log.Top
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        Panel4.Visible = False
        FormGantiPassword.Visible = False
        FormHome.Visible = False
        FormReport.Visible = False
        FormKoin.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = True
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
        ReloadLog()
        conn.Close()
    End Sub

    Private Sub ReloadLog()
        Try
            Dim CurrentRow As Integer = 0
            Read("SELECT activity AS Activity, username AS Username, date AS Date FROM tb_log_activity ORDER BY date DESC", BunifuCustomDataGrid9)
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_log_activity", conn)
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid9.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                CurrentRow += 1
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn_0_Click(sender As Object, e As EventArgs) Handles btn_0.Click
        input_game()
        btn_0.Enabled = False
        txt_game.Text = "0"
    End Sub

    Private Sub btn_bonus_0_Click(sender As Object, e As EventArgs) Handles btn_bonus_0.Click
        input_bonus()
        btn_bonus_0.Enabled = False
        txt_bonus.Text = "0"
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        txt_datetime.Text = Date.Now.ToString("dd-MMM-yyyy hh:mm:ss")
        If txt_akses.Text = "Kasir" Then
            Call Koneksi()
            Try
                cmd8 = New Odbc.OdbcCommand("SELECT * FROM tb_user WHERE username = '" & txt_user.Text & "'", conn)
                Using rd8 As Odbc.OdbcDataReader = cmd8.ExecuteReader
                    While rd8.Read()
                        txt_koin_user.Text = rd8!koin
                        txt_saldo.Text = rd8!koin
                    End While
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Close()
        End If
    End Sub

    Private Sub BunifuThinButton23_Click(sender As Object, e As EventArgs) Handles BunifuThinButton23.Click
        txt_koin.Text += 100
    End Sub

    Private Sub BunifuThinButton24_Click(sender As Object, e As EventArgs) Handles BunifuThinButton24.Click
        txt_koin.Text += 500
    End Sub

    Private Sub BunifuThinButton25_Click(sender As Object, e As EventArgs) Handles BunifuThinButton25.Click
        txt_koin.Text += 1000
    End Sub

    Private Sub BunifuThinButton26_Click(sender As Object, e As EventArgs) Handles BunifuThinButton26.Click
        txt_koin.Text += 10000
    End Sub

    Private Sub BunifuThinButton213_Click(sender As Object, e As EventArgs) Handles BunifuThinButton213.Click
        FormStaffKoin.Visible = True
        ReloadStaffKoin()
    End Sub

    Private Sub BunifuThinButton214_Click(sender As Object, e As EventArgs) Handles BunifuThinButton214.Click
        FormStaffKoin.Visible = False
    End Sub

    Private Sub ReloadStaffKoin()
        Try
            Dim CurrentRow As Integer = 0
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, status AS Status FROM tb_user WHERE hak_akses = 'Staff Koin'", BunifuCustomDataGrid10)
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_user where hak_akses = 'Staff Koin'", conn)
            rd = cmd.ExecuteReader
            While rd.Read()
                BunifuCustomDataGrid10.Rows(CurrentRow).Cells(0).Value = CurrentRow + 1
                CurrentRow += 1
            End While
            bersihStaffKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bersihStaffKoin()
        txt_usernameStaffKoin.Enabled = True
        txt_usernameStaffKoin.Text = ""
        txt_passwordStaffKoin.Text = ""
        txt_namaStaffKoin.Text = ""
        cmb_aksesStaffKoin.selectedIndex = 0
        BunifuThinButton21.Enabled = True
        BunifuThinButton22.Enabled = True
    End Sub

    Private Sub BunifuMaterialTextbox1_KeyUp(sender As Object, e As KeyEventArgs) Handles BunifuMaterialTextbox1.KeyUp
        Try
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, status AS Status, koin_update AS 'Koin Update' FROM tb_user WHERE hak_akses = 'Staff Koin' AND username LIKE '%" & txt_searchAddUser.Text & "%'", BunifuCustomDataGrid10)
            bersihStaffKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid10_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid10.CellContentClick
        If Not IsNothing(BunifuCustomDataGrid10.CurrentRow.Cells(1).Value) Then
            txt_usernameStaffKoin.Text = BunifuCustomDataGrid10.CurrentRow.Cells(1).Value
            txt_usernameStaffKoin.Enabled = False
            txt_namaStaffKoin.Text = BunifuCustomDataGrid10.CurrentRow.Cells(2).Value
            cmb_aksesStaffKoin.selectedIndex = 0
            Dim stat = BunifuCustomDataGrid10.CurrentRow.Cells(4).Value
            If stat.Equals("Available") Then
                BunifuThinButton21.Enabled = False
                BunifuThinButton22.Enabled = True
            Else
                BunifuThinButton21.Enabled = True
                BunifuThinButton22.Enabled = False
            End If
        End If
    End Sub

    Private Sub BunifuThinButton212_Click(sender As Object, e As EventArgs) Handles BunifuThinButton212.Click
        Try
            Create("INSERT INTO tb_user (username, nama, password, hak_akses, koin, created_by, status) VALUES ('" & txt_usernameStaffKoin.Text & "','" _
                   & txt_namaStaffKoin.Text & "','" & txt_passwordStaffKoin.Text & "','" _
                   & cmb_aksesStaffKoin.selectedValue & "','0','" & txt_user.Text & "', 'Available')")
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Tambah User','" _
                   & txt_user.Text & "',NOW())")
            ReloadStaffKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub BunifuThinButton211_Click(sender As Object, e As EventArgs) Handles BunifuThinButton211.Click
        Try
            Ubah("UPDATE tb_user SET nama = '" & txt_namaStaffKoin.Text & "', hak_akses = '" & cmb_aksesStaffKoin.selectedValue _
                 & "' where username = '" & txt_usernameStaffKoin.Text & "'")
            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Update User','" _
                   & txt_user.Text & "',NOW())")
            ReloadStaffKoin()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub BunifuThinButton210_Click(sender As Object, e As EventArgs) Handles BunifuThinButton210.Click
        If txt_usernameStaffKoin.Text = "" Then
            If MsgBox("Anda yakin ingin menghapus seluruh user?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Delete("DELETE FROM tb_user WHERE hak_akses = 'Staff Koin'")
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Delete All User','" _
                           & txt_user.Text & "',NOW())")
                    ReloadStaffKoin()
                    conn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            If MsgBox("Anda yakin ingin menghapus user " & txt_usernameStaffKoin.Text & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Delete("DELETE FROM tb_user WHERE username = '" & txt_usernameStaffKoin.Text & "'")
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Delete User','" _
                           & txt_user.Text & "',NOW())")
                    ReloadStaffKoin()
                    conn.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles BunifuThinButton21.Click
        If txt_usernameStaffKoin.Text = "" Then
            Try
                Ubah("UPDATE tb_user SET status = 'Available' where hak_akses = 'Staff Koin'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                ReloadStaffKoin()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Try
                Ubah("UPDATE tb_user SET status = 'Available' where username = '" & txt_usernameStaffKoin.Text & "'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                ReloadStaffKoin()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BunifuThinButton22_Click(sender As Object, e As EventArgs) Handles BunifuThinButton22.Click
        If txt_usernameStaffKoin.Text = "" Then
            Try
                Ubah("UPDATE tb_user SET status = 'Suspend' where hak_akses = 'Staff Koin'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                ReloadStaffKoin()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            Try
                Ubah("UPDATE tb_user SET status = 'Suspend' where username = '" & txt_usernameStaffKoin.Text & "'")
                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Ubah Status User','" _
                       & txt_user.Text & "',NOW())")
                ReloadStaffKoin()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        conn.Close()
    End Sub

    Private Sub BunifuThinButton27_Click(sender As Object, e As EventArgs) Handles BunifuThinButton27.Click
        bersihStaffKoin()
    End Sub

    Private Sub BunifuThinButton29_Click(sender As Object, e As EventArgs) Handles BunifuThinButton29.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = True
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = False
    End Sub

    Private Sub BunifuThinButton28_Click(sender As Object, e As EventArgs) Handles BunifuThinButton28.Click
        If txt_usernameStaffKoin.Text = "" Then
            MsgBox("Please select a user!")
        Else
            Call Koneksi()
            Try
                cmd = New Odbc.OdbcCommand("SELECT username, password FROM tb_user WHERE username LIKE '" & txt_usernameStaffKoin.Text & "'", conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows = True Then
                    Dim qrData = rd!username & "," & rd!password
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    PrintQR.qr_pic.Image = code.GetGraphic(6)
                    PrintQR.Show()
                Else
                    MsgBox("User not found!")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Close()
        End If
    End Sub

    Private Sub BunifuDropdown1_onItemSelected(sender As Object, e As EventArgs) Handles BunifuDropdown1.onItemSelected
        If BunifuDropdown1.selectedIndex = 0 Then
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where hak_akses = 'Customer'", BunifuCustomDataGrid4)
        ElseIf BunifuDropdown1.selectedIndex = 1 Then
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where hak_akses = 'Staff Koin'", BunifuCustomDataGrid4)
        ElseIf BunifuDropdown1.selectedIndex = 2 Then
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where hak_akses = 'Operator'", BunifuCustomDataGrid4)
        ElseIf BunifuDropdown1.selectedIndex = 3 Then
            Read("SELECT username AS Username, nama AS Nama, koin AS Koin, hak_akses AS 'Hak Akses', last_login AS 'Last Login', created_by AS 'Created By', status AS Status FROM tb_user where hak_akses = 'Kasir'", BunifuCustomDataGrid4)
        End If
    End Sub

    Private Sub txt_searchKoin_KeyUp(sender As Object, e As KeyEventArgs) Handles txt_searchKoin.KeyUp

    End Sub

    Private Sub btn_timer_Click(sender As Object, e As EventArgs) Handles btn_timer.Click
        FormUser.Visible = False
        FormStaffKoin.Visible = False
        FormRoulette.Visible = False
        FormGantiPassword.Visible = True
        FormHome.Visible = False
        FormListUser.Visible = False
        FormKoin.Visible = False
        FormReport.Visible = False
        FormDetailReport.Visible = False
        Form_DetailPeriode.Visible = False
        Form_Log.Visible = False
        Form_Setting.Visible = False
        FormTotalPeriode.Visible = False
        Form_Proses.Visible = False
        FormTimer.Visible = True
        Timer3.Enabled = True
        lbl_timer.Text = time_limit
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Call Koneksi()
        Try
            cmd = New Odbc.OdbcCommand("SELECT * FROM tb_permainan WHERE status = 1", conn)
            Using rd As Odbc.OdbcDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    While rd.Read()
                        Timer4.Enabled = True
                        Timer3.Enabled = False
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        lbl_timer.Text = Val(lbl_timer.Text) - 1
        If lbl_timer.Text <= 0 Then
            lbl_timer.Text = 0
            Timer4.Enabled = False
            Timer3.Enabled = True
            Ubahs("UPDATE tb_permainan SET status = 6 WHERE status = 1")
        End If
    End Sub
End Class