Imports System.Data.Odbc
Public Class MenuCustomer
    Public cmd2 As OdbcCommand
    Public rd2 As OdbcDataReader
    Public cmd3 As OdbcCommand
    Public rd3 As OdbcDataReader
    Public cmd4 As OdbcCommand
    Public rd4 As OdbcDataReader
    Public cmd5 As OdbcCommand
    Public rd5 As OdbcDataReader
    Public cmd6 As OdbcCommand
    Public rd6 As OdbcDataReader
    Public cmd7 As OdbcCommand
    Public rd7 As OdbcDataReader
    Dim rs As New Resizer
    Dim jumlahBet = 0
    Dim temp As Integer
    Dim listHistory As Integer = 1
    Dim listHistory2 As Integer = 1
    Dim kali_binatang As Integer
    Dim kali_warna As Double
    Dim kali_angka As Double
    Dim kali_grup1 As Double
    Dim kali_grup2 As Double
    Dim kali_besar_kecil As Double
    Dim kali_genap_ganjil As Double
    Dim maxbet_binatang As Integer
    Dim maxbet_warna As Integer
    Dim maxbet_angka As Integer
    Dim maxbet_grup1 As Integer
    Dim maxbet_grup2 As Integer
    Dim maxbet_besar_kecil As Integer
    Dim maxbet_genap_ganjil As Integer
    Dim kali_bonus As Double
    Dim kali_bonusAngka As Double
    Dim kali_bonusHewan As Double
    Dim time_limit As Integer
    Dim temp_timer As String = 0
    Dim temp_notif As Integer = 0
    Dim bonus_music As Integer = 0
    Dim win_music As Integer = 0
    Dim bonus_loop As Integer = 0
    Dim win_loop As Integer = 0
    Dim bonus_panel As New List(Of Panel)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call Koneksi()
        Try
            cmd2 = New OdbcCommand("SELECT * FROM tb_permainan WHERE status = 6", conn)
            Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                If rd2.HasRows = True Then
                    While rd2.Read()
                        Label6.Text = 0
                        Timer1.Enabled = False
                        Timer3.Enabled = True
                        btn_bet_10.Enabled = False
                        btn_bet_100.Enabled = False
                        btn_bet_500.Enabled = False
                        btn_bet_1000.Enabled = False
                        Panel70.Visible = False
                        btn_cancel.Visible = False
                        btn_repeat.Visible = False
                        noBet()
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub MenuCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = time_limit
        angka.Visible = False
        shio.Visible = False
        resetBonus()
        history.Visible = False
        txt_credit.Text = 0
        Login.Close()
        rs.FindAllControls(Me)
        reset()
        GetSetting()
        Timer7.Enabled = True
        WindowState = FormWindowState.Maximized
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(
            "C:\Users\Public\Music\Sound Effects\",
            Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*bonus*")
            bonus_music += 1
        Next
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(
            "C:\Users\Public\Music\Sound Effects\",
            Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*result*")
            win_music += 1
        Next
        bonus_panel.Add(Bonus1)
        bonus_panel.Add(Bonus2)
        bonus_panel.Add(Bonus3)
        bonus_panel.Add(Bonus4)
        bonus_panel.Add(Bonus5)
        bonus_panel.Add(Bonus6)
        bonus_panel.Add(Bonus7)
        bonus_panel.Add(Bonus8)
        bonus_panel.Add(Bonus9)
        bonus_panel.Add(Bonus10)
        Panel65.Visible = False
        Panel68.Visible = True
        Panel66.Visible = False
        Panel70.Visible = False
    End Sub

    Private Sub GetSetting()
        Call Koneksi()
        Try
            cmd3 = New OdbcCommand("SELECT * FROM tb_setting", conn)
            Using rd3 As OdbcDataReader = cmd3.ExecuteReader
                If rd3.HasRows = True Then
                    While rd3.Read()
                        kali_binatang = rd3!hadiah_hewan
                        kali_warna = rd3!hadiah_warna
                        kali_angka = rd3!hadiah_2d
                        kali_grup1 = rd3!hadiah_12item
                        kali_grup2 = rd3!hadiah_4warna
                        kali_besar_kecil = rd3!hadiah_bk
                        kali_genap_ganjil = rd3!hadiah_gg
                        maxbet_binatang = rd3!maxbet_hewan + 1
                        maxbet_warna = rd3!maxbet_warna + 1
                        maxbet_angka = rd3!maxbet_2d + 1
                        maxbet_grup1 = rd3!maxbet_12item + 1
                        maxbet_grup2 = rd3!maxbet_4warna + 1
                        maxbet_besar_kecil = rd3!maxbet_bk + 1
                        maxbet_genap_ganjil = rd3!maxbet_gg + 1
                        kali_bonus = rd3!bonus
                        kali_bonusAngka = rd3!bonus_angka
                        kali_bonusHewan = rd3!bonus_hewan
                        time_limit = rd3!time_limit
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub GetBet()
        Call Koneksi()
        Try
            cmd3 = New OdbcCommand("SELECT * FROM tb_bet WHERE username = '" & txt_user.Text & "'", conn)
            Using rd3 As OdbcDataReader = cmd3.ExecuteReader
                If rd3.HasRows = True Then
                    btn_repeat.Visible = True
                Else
                    btn_repeat.Visible = False
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub MenuCustomer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Timer7.Enabled = False
        Ubahs("UPDATE tb_user SET status = 'Available' where username = '" & txt_user.Text & "'")
        Hide()
        Login.Panel4.Visible = False
        Login.Panel3.Visible = False
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\button.wav")
        Login.Show()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Call Koneksi()
        Try
            cmd2 = New OdbcCommand("SELECT * FROM tb_permainan WHERE status = 1", conn)
            Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                If rd2.HasRows = True Then
                    While rd2.Read()
                        reset()
                        mulai_permainan()
                        GetBet()
                        Label6.Text = time_limit
                        temp_timer = 0
                        Timer5.Enabled = False
                        Timer6.Enabled = False
                        Timer1.Enabled = True
                        Timer8.Enabled = True
                        Timer2.Enabled = False
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub mulai_permainan()
        btn_bet_10.Enabled = True
        btn_bet_100.Enabled = True
        btn_bet_500.Enabled = True
        btn_bet_1000.Enabled = True
        Panel70.Visible = True
        btn_repeat.Visible = True
        btn_logout.Visible = False
        angka.Visible = False
        shio.Visible = False
        txt_win.Text = 0
        btn_bonus.Visible = True
        btn_red.Visible = True
        btn_black.Visible = True
        btn_ganjil.Visible = True
        btn_genap.Visible = True
        btn_grup_1.Visible = True
        btn_grup_2.Visible = True
        btn_grup_3.Visible = True
        btn_grup_4.Visible = True
        btn_1_12.Visible = True
        btn_13_24.Visible = True
        btn_25_36.Visible = True
        btn_besar.Visible = True
        btn_kecil.Visible = True
        btn_1.Visible = True
        btn_2.Visible = True
        btn_3.Visible = True
        btn_4.Visible = True
        btn_5.Visible = True
        btn_6.Visible = True
        btn_7.Visible = True
        btn_8.Visible = True
        btn_9.Visible = True
        btn_10.Visible = True
        btn_11.Visible = True
        btn_12.Visible = True
        btn_13.Visible = True
        btn_14.Visible = True
        btn_15.Visible = True
        btn_16.Visible = True
        btn_17.Visible = True
        btn_18.Visible = True
        btn_19.Visible = True
        btn_20.Visible = True
        btn_21.Visible = True
        btn_22.Visible = True
        btn_23.Visible = True
        btn_24.Visible = True
        btn_25.Visible = True
        btn_26.Visible = True
        btn_27.Visible = True
        btn_28.Visible = True
        btn_29.Visible = True
        btn_30.Visible = True
        btn_31.Visible = True
        btn_32.Visible = True
        btn_33.Visible = True
        btn_34.Visible = True
        btn_35.Visible = True
        btn_36.Visible = True
        btn_kelinci.Visible = True
        btn_naga.Visible = True
        btn_ular.Visible = True
        btn_macan.Visible = True
        btn_kerbau.Visible = True
        btn_tikus.Visible = True
        btn_babi.Visible = True
        btn_anjing.Visible = True
        btn_ayam.Visible = True
        btn_monyet.Visible = True
        btn_kambing.Visible = True
        btn_kuda.Visible = True
        noBet()
        resetBonus()
        Panel70.Visible = True
    End Sub

    Private Sub reset()
        jumlahBet = 0
        btn_logout.Visible = True
        btn_bet_10.Enabled = False
        btn_bet_100.Enabled = False
        btn_bet_500.Enabled = False
        btn_bet_1000.Enabled = False
        btn_repeat.Visible = False
        btn_cancel.Visible = False
        txt_bet.Text = 0
        Label8.Text = 0
        Label9.Text = 0
        Label10.Text = 0
        Label11.Text = 0
        Label12.Text = 0
        Label13.Text = 0
        Label14.Text = 0
        Label15.Text = 0
        Label16.Text = 0
        Label17.Text = 0
        Label18.Text = 0
        Label19.Text = 0
        Label20.Text = 0
        Label21.Text = 0
        Label22.Text = 0
        Label23.Text = 0
        Label24.Text = 0
        Label25.Text = 0
        Label26.Text = 0
        Label27.Text = 0
        Label28.Text = 0
        Label29.Text = 0
        Label30.Text = 0
        Label31.Text = 0
        Label32.Text = 0
        Label33.Text = 0
        Label34.Text = 0
        Label35.Text = 0
        Label36.Text = 0
        Label37.Text = 0
        Label38.Text = 0
        Label39.Text = 0
        Label40.Text = 0
        Label41.Text = 0
        Label42.Text = 0
        Label43.Text = 0
        Label44.Text = 0
        Label45.Text = 0
        Label46.Text = 0
        Label47.Text = 0
        Label48.Text = 0
        Label49.Text = 0
        Label50.Text = 0
        Label51.Text = 0
        Label52.Text = 0
        Label53.Text = 0
        Label54.Text = 0
        Label55.Text = 0
        Label56.Text = 0
        Label57.Text = 0
        Label58.Text = 0
        Label59.Text = 0
        Label60.Text = 0
        Label61.Text = 0
        Label62.Text = 0
        Label63.Text = 0
        Label64.Text = 0
        Label65.Text = 0
        Label66.Text = 0
        Label67.Text = 0
        Label68.Text = 0
        Label69.Text = 0
        noBet()
    End Sub

    Private Sub bet()
        btn_repeat.Visible = False
        btn_bonus.Enabled = True
        btn_red.Enabled = True
        btn_black.Enabled = True
        btn_ganjil.Enabled = True
        btn_genap.Enabled = True
        btn_grup_1.Enabled = True
        btn_grup_2.Enabled = True
        btn_grup_3.Enabled = True
        btn_grup_4.Enabled = True
        btn_1_12.Enabled = True
        btn_13_24.Enabled = True
        btn_25_36.Enabled = True
        btn_besar.Enabled = True
        btn_kecil.Enabled = True
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
        btn_13.Enabled = True
        btn_14.Enabled = True
        btn_15.Enabled = True
        btn_16.Enabled = True
        btn_17.Enabled = True
        btn_18.Enabled = True
        btn_19.Enabled = True
        btn_20.Enabled = True
        btn_21.Enabled = True
        btn_22.Enabled = True
        btn_23.Enabled = True
        btn_24.Enabled = True
        btn_25.Enabled = True
        btn_26.Enabled = True
        btn_27.Enabled = True
        btn_28.Enabled = True
        btn_29.Enabled = True
        btn_30.Enabled = True
        btn_31.Enabled = True
        btn_32.Enabled = True
        btn_33.Enabled = True
        btn_34.Enabled = True
        btn_35.Enabled = True
        btn_36.Enabled = True
        btn_kelinci.Enabled = True
        btn_naga.Enabled = True
        btn_ular.Enabled = True
        btn_macan.Enabled = True
        btn_kerbau.Enabled = True
        btn_tikus.Enabled = True
        btn_babi.Enabled = True
        btn_anjing.Enabled = True
        btn_ayam.Enabled = True
        btn_monyet.Enabled = True
        btn_kambing.Enabled = True
        btn_kuda.Enabled = True
        Label8.Enabled = True
        Label9.Enabled = True
        Label10.Enabled = True
        Label11.Enabled = True
        Label12.Enabled = True
        Label13.Enabled = True
        Label14.Enabled = True
        Label15.Enabled = True
        Label16.Enabled = True
        Label17.Enabled = True
        Label18.Enabled = True
        Label19.Enabled = True
        Label20.Enabled = True
        Label21.Enabled = True
        Label22.Enabled = True
        Label23.Enabled = True
        Label24.Enabled = True
        Label25.Enabled = True
        Label26.Enabled = True
        Label27.Enabled = True
        Label28.Enabled = True
        Label29.Enabled = True
        Label30.Enabled = True
        Label31.Enabled = True
        Label32.Enabled = True
        Label33.Enabled = True
        Label34.Enabled = True
        Label35.Enabled = True
        Label36.Enabled = True
        Label37.Enabled = True
        Label38.Enabled = True
        Label39.Enabled = True
        Label40.Enabled = True
        Label41.Enabled = True
        Label42.Enabled = True
        Label43.Enabled = True
        Label44.Enabled = True
        Label45.Enabled = True
        Label46.Enabled = True
        Label47.Enabled = True
        Label48.Enabled = True
        Label49.Enabled = True
        Label50.Enabled = True
        Label51.Enabled = True
        Label52.Enabled = True
        Label53.Enabled = True
        Label54.Enabled = True
        Label55.Enabled = True
        Label56.Enabled = True
        Label57.Enabled = True
        Label58.Enabled = True
        Label59.Enabled = True
        Label60.Enabled = True
        Label61.Enabled = True
        Label62.Enabled = True
        Label63.Enabled = True
        Label64.Enabled = True
        Label65.Enabled = True
        Label66.Enabled = True
        Label67.Enabled = True
        Label68.Enabled = True
        Label69.Enabled = True
        Panel10.Enabled = True
        Panel9.Enabled = True
        Panel3.Enabled = True
        Panel6.Enabled = True
        Panel12.Enabled = True
        Panel8.Enabled = True
        Panel7.Enabled = True
        Panel13.Enabled = True
        Panel4.Enabled = True
        Panel11.Enabled = True
        Panel1.Enabled = True
        Panel5.Enabled = True
        Panel16.Enabled = True
        Panel19.Enabled = True
        Panel18.Enabled = True
        Panel17.Enabled = True
        Panel15.Enabled = True
        Panel14.Enabled = True
        Panel25.Enabled = True
        Panel24.Enabled = True
        Panel22.Enabled = True
        Panel21.Enabled = True
        Panel20.Enabled = True
        Panel23.Enabled = True
        Panel26.Enabled = True
        Panel31.Enabled = True
        Panel33.Enabled = True
        Panel34.Enabled = True
        Panel35.Enabled = True
        Panel32.Enabled = True
        Panel37.Enabled = True
        Panel36.Enabled = True
        Panel29.Enabled = True
        Panel28.Enabled = True
        Panel27.Enabled = True
        Panel30.Enabled = True
        Panel44.Enabled = True
        Panel45.Enabled = True
        Panel40.Enabled = True
        Panel39.Enabled = True
        Panel38.Enabled = True
        Panel41.Enabled = True
        Panel46.Enabled = True
        Panel47.Enabled = True
        Panel49.Enabled = True
        Panel42.Enabled = True
        Panel43.Enabled = True
        Panel48.Enabled = True
        Panel60.Enabled = True
        Panel61.Enabled = True
        Panel59.Enabled = True
        Panel50.Enabled = True
        Panel51.Enabled = True
        Panel52.Enabled = True
        Panel53.Enabled = True
        Panel54.Enabled = True
        Panel55.Enabled = True
        Panel56.Enabled = True
        Panel57.Enabled = True
        Panel58.Enabled = True
        Panel62.Enabled = True
        Panel63.Enabled = True
    End Sub

    Private Sub noBet()
        btn_bonus.Enabled = False
        btn_red.Enabled = False
        btn_black.Enabled = False
        btn_ganjil.Enabled = False
        btn_genap.Enabled = False
        btn_grup_1.Enabled = False
        btn_grup_2.Enabled = False
        btn_grup_3.Enabled = False
        btn_grup_4.Enabled = False
        btn_1_12.Enabled = False
        btn_13_24.Enabled = False
        btn_25_36.Enabled = False
        btn_besar.Enabled = False
        btn_kecil.Enabled = False
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
        btn_kelinci.Enabled = False
        btn_naga.Enabled = False
        btn_ular.Enabled = False
        btn_macan.Enabled = False
        btn_kerbau.Enabled = False
        btn_tikus.Enabled = False
        btn_babi.Enabled = False
        btn_anjing.Enabled = False
        btn_ayam.Enabled = False
        btn_monyet.Enabled = False
        btn_kambing.Enabled = False
        btn_kuda.Enabled = False
        Label8.Enabled = False
        Label9.Enabled = False
        Label10.Enabled = False
        Label11.Enabled = False
        Label12.Enabled = False
        Label13.Enabled = False
        Label14.Enabled = False
        Label15.Enabled = False
        Label16.Enabled = False
        Label17.Enabled = False
        Label18.Enabled = False
        Label19.Enabled = False
        Label20.Enabled = False
        Label21.Enabled = False
        Label22.Enabled = False
        Label23.Enabled = False
        Label24.Enabled = False
        Label25.Enabled = False
        Label26.Enabled = False
        Label27.Enabled = False
        Label28.Enabled = False
        Label29.Enabled = False
        Label30.Enabled = False
        Label31.Enabled = False
        Label32.Enabled = False
        Label33.Enabled = False
        Label34.Enabled = False
        Label35.Enabled = False
        Label36.Enabled = False
        Label37.Enabled = False
        Label38.Enabled = False
        Label39.Enabled = False
        Label40.Enabled = False
        Label41.Enabled = False
        Label42.Enabled = False
        Label43.Enabled = False
        Label44.Enabled = False
        Label45.Enabled = False
        Label46.Enabled = False
        Label47.Enabled = False
        Label48.Enabled = False
        Label49.Enabled = False
        Label50.Enabled = False
        Label51.Enabled = False
        Label52.Enabled = False
        Label53.Enabled = False
        Label54.Enabled = False
        Label55.Enabled = False
        Label56.Enabled = False
        Label57.Enabled = False
        Label58.Enabled = False
        Label59.Enabled = False
        Label60.Enabled = False
        Label61.Enabled = False
        Label62.Enabled = False
        Label63.Enabled = False
        Label64.Enabled = False
        Label65.Enabled = False
        Label66.Enabled = False
        Label67.Enabled = False
        Label68.Enabled = False
        Label69.Enabled = False
        Panel10.Enabled = False
        Panel9.Enabled = False
        Panel3.Enabled = False
        Panel6.Enabled = False
        Panel12.Enabled = False
        Panel8.Enabled = False
        Panel7.Enabled = False
        Panel13.Enabled = False
        Panel4.Enabled = False
        Panel11.Enabled = False
        Panel1.Enabled = False
        Panel5.Enabled = False
        Panel16.Enabled = False
        Panel19.Enabled = False
        Panel18.Enabled = False
        Panel17.Enabled = False
        Panel15.Enabled = False
        Panel14.Enabled = False
        Panel25.Enabled = False
        Panel24.Enabled = False
        Panel22.Enabled = False
        Panel21.Enabled = False
        Panel20.Enabled = False
        Panel23.Enabled = False
        Panel26.Enabled = False
        Panel31.Enabled = False
        Panel33.Enabled = False
        Panel34.Enabled = False
        Panel35.Enabled = False
        Panel32.Enabled = False
        Panel37.Enabled = False
        Panel36.Enabled = False
        Panel29.Enabled = False
        Panel28.Enabled = False
        Panel27.Enabled = False
        Panel30.Enabled = False
        Panel44.Enabled = False
        Panel45.Enabled = False
        Panel40.Enabled = False
        Panel39.Enabled = False
        Panel38.Enabled = False
        Panel41.Enabled = False
        Panel46.Enabled = False
        Panel47.Enabled = False
        Panel49.Enabled = False
        Panel42.Enabled = False
        Panel43.Enabled = False
        Panel48.Enabled = False
        Panel60.Enabled = False
        Panel61.Enabled = False
        Panel59.Enabled = False
        Panel50.Enabled = False
        Panel51.Enabled = False
        Panel52.Enabled = False
        Panel53.Enabled = False
        Panel54.Enabled = False
        Panel55.Enabled = False
        Panel56.Enabled = False
        Panel57.Enabled = False
        Panel58.Enabled = False
        Panel62.Enabled = False
        Panel63.Enabled = False
    End Sub

    Private Sub txt_bet_TextChanged(sender As Object, e As EventArgs) Handles txt_bet.TextChanged
        If txt_bet.Text = "0" Then
            btn_cancel.Visible = False
        Else
            btn_cancel.Visible = True
        End If
    End Sub

    Private Sub btn_bet_10_Click(sender As Object, e As EventArgs) Handles btn_bet_10.Click
        btn_bet_10.Enabled = False
        btn_bet_100.Enabled = True
        btn_bet_500.Enabled = True
        btn_bet_1000.Enabled = True
        bet()
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pergantian_koin.wav")
        Panel70.Visible = False
    End Sub

    Private Sub btn_bet_100_Click(sender As Object, e As EventArgs) Handles btn_bet_100.Click
        btn_bet_10.Enabled = True
        btn_bet_100.Enabled = False
        btn_bet_500.Enabled = True
        btn_bet_1000.Enabled = True
        bet()
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pergantian_koin.wav")
        Panel70.Visible = False
    End Sub

    Private Sub btn_bet_500_Click(sender As Object, e As EventArgs) Handles btn_bet_500.Click
        btn_bet_10.Enabled = True
        btn_bet_100.Enabled = True
        btn_bet_500.Enabled = False
        btn_bet_1000.Enabled = True
        bet()
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pergantian_koin.wav")
        Panel70.Visible = False
    End Sub

    Private Sub btn_bet_5000_Click(sender As Object, e As EventArgs) Handles btn_bet_1000.Click
        btn_bet_10.Enabled = True
        btn_bet_100.Enabled = True
        btn_bet_500.Enabled = True
        btn_bet_1000.Enabled = False
        bet()
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pergantian_koin.wav")
        Panel70.Visible = False
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        txt_credit.Text += Val(Label18.Text)
        txt_credit.Text += Val(Label10.Text)
        txt_credit.Text += Val(Label13.Text)
        txt_credit.Text += Val(Label16.Text)
        txt_credit.Text += Val(Label19.Text)
        txt_credit.Text += Val(Label14.Text)
        txt_credit.Text += Val(Label11.Text)
        txt_credit.Text += Val(Label8.Text)
        txt_credit.Text += Val(Label17.Text)
        txt_credit.Text += Val(Label9.Text)
        txt_credit.Text += Val(Label12.Text)
        txt_credit.Text += Val(Label15.Text)
        txt_credit.Text += Val(Label58.Text)
        txt_credit.Text += Val(Label57.Text)
        txt_credit.Text += Val(Label56.Text)
        txt_credit.Text += Val(Label25.Text)
        txt_credit.Text += Val(Label24.Text)
        txt_credit.Text += Val(Label23.Text)
        txt_credit.Text += Val(Label22.Text)
        txt_credit.Text += Val(Label21.Text)
        txt_credit.Text += Val(Label20.Text)
        txt_credit.Text += Val(Label30.Text)
        txt_credit.Text += Val(Label29.Text)
        txt_credit.Text += Val(Label28.Text)
        txt_credit.Text += Val(Label31.Text)
        txt_credit.Text += Val(Label27.Text)
        txt_credit.Text += Val(Label26.Text)
        txt_credit.Text += Val(Label36.Text)
        txt_credit.Text += Val(Label35.Text)
        txt_credit.Text += Val(Label34.Text)
        txt_credit.Text += Val(Label37.Text)
        txt_credit.Text += Val(Label33.Text)
        txt_credit.Text += Val(Label32.Text)
        txt_credit.Text += Val(Label42.Text)
        txt_credit.Text += Val(Label41.Text)
        txt_credit.Text += Val(Label40.Text)
        txt_credit.Text += Val(Label43.Text)
        txt_credit.Text += Val(Label39.Text)
        txt_credit.Text += Val(Label38.Text)
        txt_credit.Text += Val(Label48.Text)
        txt_credit.Text += Val(Label47.Text)
        txt_credit.Text += Val(Label46.Text)
        txt_credit.Text += Val(Label49.Text)
        txt_credit.Text += Val(Label45.Text)
        txt_credit.Text += Val(Label44.Text)
        txt_credit.Text += Val(Label54.Text)
        txt_credit.Text += Val(Label53.Text)
        txt_credit.Text += Val(Label52.Text)
        txt_credit.Text += Val(Label55.Text)
        txt_credit.Text += Val(Label51.Text)
        txt_credit.Text += Val(Label50.Text)

        Try
            Deletes("DELETE FROM tb_bet WHERE username = '" & txt_user.Text & "' AND id_permainan = (select x.id FROM (SELECT MAX(id_permainan) AS id FROM tb_bet t WHERE username = '" & txt_user.Text & "') x)")
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        reset()
        mulai_permainan()
        btn_repeat.Visible = False
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\button.wav")
    End Sub

    Private Sub Label8_TextChanged(sender As Object, e As EventArgs) Handles Label8.TextChanged
        If Label8.Text <> 0 Then
            Label8.Visible = True
            Panel10.Visible = True
        Else
            Label8.Visible = False
            Panel10.Visible = False
        End If
    End Sub

    Private Sub Label9_TextChanged(sender As Object, e As EventArgs) Handles Label9.TextChanged
        If Label9.Text <> 0 Then
            Label9.Visible = True
            Panel9.Visible = True
        Else
            Label9.Visible = False
            Panel9.Visible = False
        End If
    End Sub

    Private Sub Label10_TextChanged(sender As Object, e As EventArgs) Handles Label10.TextChanged
        If Label10.Text <> 0 Then
            Label10.Visible = True
            Panel3.Visible = True
        Else
            Label10.Visible = False
            Panel3.Visible = False
        End If
    End Sub

    Private Sub Label11_TextChanged(sender As Object, e As EventArgs) Handles Label11.TextChanged
        If Label11.Text <> 0 Then
            Label11.Visible = True
            Panel6.Visible = True
        Else
            Label11.Visible = False
            Panel6.Visible = False
        End If
    End Sub

    Private Sub Label12_TextChanged(sender As Object, e As EventArgs) Handles Label12.TextChanged
        If Label12.Text <> 0 Then
            Label12.Visible = True
            Panel12.Visible = True
        Else
            Label12.Visible = False
            Panel12.Visible = False
        End If
    End Sub

    Private Sub Label13_TextChanged(sender As Object, e As EventArgs) Handles Label13.TextChanged
        If Label13.Text <> 0 Then
            Label13.Visible = True
            Panel8.Visible = True
        Else
            Label13.Visible = False
            Panel8.Visible = False
        End If
    End Sub

    Private Sub Label14_TextChanged(sender As Object, e As EventArgs) Handles Label14.TextChanged
        If Label14.Text <> 0 Then
            Label14.Visible = True
            Panel7.Visible = True
        Else
            Label14.Visible = False
            Panel7.Visible = False
        End If
    End Sub

    Private Sub Label15_TextChanged(sender As Object, e As EventArgs) Handles Label15.TextChanged
        If Label15.Text <> 0 Then
            Label15.Visible = True
            Panel13.Visible = True
        Else
            Label15.Visible = False
            Panel13.Visible = False
        End If
    End Sub

    Private Sub Label16_TextChanged(sender As Object, e As EventArgs) Handles Label16.TextChanged
        If Label16.Text <> 0 Then
            Label16.Visible = True
            Panel4.Visible = True
        Else
            Label16.Visible = False
            Panel4.Visible = False
        End If
    End Sub

    Private Sub Label17_TextChanged(sender As Object, e As EventArgs) Handles Label17.TextChanged
        If Label17.Text <> 0 Then
            Label17.Visible = True
            Panel11.Visible = True
        Else
            Label17.Visible = False
            Panel11.Visible = False
        End If
    End Sub

    Private Sub Label18_TextChanged(sender As Object, e As EventArgs) Handles Label18.TextChanged
        If Label18.Text <> 0 Then
            Label18.Visible = True
            Panel1.Visible = True
        Else
            Label18.Visible = False
            Panel1.Visible = False
        End If
    End Sub

    Private Sub Label19_TextChanged(sender As Object, e As EventArgs) Handles Label19.TextChanged
        If Label19.Text <> 0 Then
            Label19.Visible = True
            Panel5.Visible = True
        Else
            Label19.Visible = False
            Panel5.Visible = False
        End If
    End Sub

    Private Sub Labe20_TextChanged(sender As Object, e As EventArgs) Handles Label20.TextChanged
        If Label20.Text <> 0 Then
            Label20.Visible = True
            Panel16.Visible = True
        Else
            Label20.Visible = False
            Panel16.Visible = False
        End If
    End Sub

    Private Sub Label21_TextChanged(sender As Object, e As EventArgs) Handles Label21.TextChanged
        If Label21.Text <> 0 Then
            Label21.Visible = True
            Panel19.Visible = True
        Else
            Label21.Visible = False
            Panel19.Visible = False
        End If
    End Sub

    Private Sub Label22_TextChanged(sender As Object, e As EventArgs) Handles Label22.TextChanged
        If Label22.Text <> 0 Then
            Label22.Visible = True
            Panel18.Visible = True
        Else
            Label22.Visible = False
            Panel18.Visible = False
        End If
    End Sub

    Private Sub Label23_TextChanged(sender As Object, e As EventArgs) Handles Label23.TextChanged
        If Label23.Text <> 0 Then
            Label23.Visible = True
            Panel17.Visible = True
        Else
            Label23.Visible = False
            Panel17.Visible = False
        End If
    End Sub

    Private Sub Label24_TextChanged(sender As Object, e As EventArgs) Handles Label24.TextChanged
        If Label24.Text <> 0 Then
            Label24.Visible = True
            Panel15.Visible = True
        Else
            Label24.Visible = False
            Panel15.Visible = False
        End If
    End Sub

    Private Sub Label25_TextChanged(sender As Object, e As EventArgs) Handles Label25.TextChanged
        If Label25.Text <> 0 Then
            Label25.Visible = True
            Panel14.Visible = True
        Else
            Label25.Visible = False
            Panel14.Visible = False
        End If
    End Sub

    Private Sub Label26_TextChanged(sender As Object, e As EventArgs) Handles Label26.TextChanged
        If Label26.Text <> 0 Then
            Label26.Visible = True
            Panel25.Visible = True
        Else
            Label26.Visible = False
            Panel25.Visible = False
        End If
    End Sub

    Private Sub Label27_TextChanged(sender As Object, e As EventArgs) Handles Label27.TextChanged
        If Label27.Text <> 0 Then
            Label27.Visible = True
            Panel24.Visible = True
        Else
            Label27.Visible = False
            Panel24.Visible = False
        End If
    End Sub

    Private Sub Label28_TextChanged(sender As Object, e As EventArgs) Handles Label28.TextChanged
        If Label28.Text <> 0 Then
            Label28.Visible = True
            Panel22.Visible = True
        Else
            Label28.Visible = False
            Panel22.Visible = False
        End If
    End Sub

    Private Sub Label29_TextChanged(sender As Object, e As EventArgs) Handles Label29.TextChanged
        If Label29.Text <> 0 Then
            Label29.Visible = True
            Panel21.Visible = True
        Else
            Label29.Visible = False
            Panel21.Visible = False
        End If
    End Sub

    Private Sub Label30_TextChanged(sender As Object, e As EventArgs) Handles Label30.TextChanged
        If Label30.Text <> 0 Then
            Label30.Visible = True
            Panel20.Visible = True
        Else
            Label30.Visible = False
            Panel20.Visible = False
        End If
    End Sub

    Private Sub Label31_TextChanged(sender As Object, e As EventArgs) Handles Label31.TextChanged
        If Label31.Text <> 0 Then
            Label31.Visible = True
            Panel23.Visible = True
        Else
            Label31.Visible = False
            Panel23.Visible = False
        End If
    End Sub

    Private Sub Label32_TextChanged(sender As Object, e As EventArgs) Handles Label32.TextChanged
        If Label32.Text <> 0 Then
            Label32.Visible = True
            Panel26.Visible = True
        Else
            Label32.Visible = False
            Panel26.Visible = False
        End If
    End Sub

    Private Sub Label33_TextChanged(sender As Object, e As EventArgs) Handles Label33.TextChanged
        If Label33.Text <> 0 Then
            Label33.Visible = True
            Panel31.Visible = True
        Else
            Label33.Visible = False
            Panel31.Visible = False
        End If
    End Sub

    Private Sub Label34_TextChanged(sender As Object, e As EventArgs) Handles Label34.TextChanged
        If Label34.Text <> 0 Then
            Label34.Visible = True
            Panel33.Visible = True
        Else
            Label34.Visible = False
            Panel33.Visible = False
        End If
    End Sub

    Private Sub Label35_TextChanged(sender As Object, e As EventArgs) Handles Label35.TextChanged
        If Label35.Text <> 0 Then
            Label35.Visible = True
            Panel34.Visible = True
        Else
            Label35.Visible = False
            Panel34.Visible = False
        End If
    End Sub

    Private Sub Label36_TextChanged(sender As Object, e As EventArgs) Handles Label36.TextChanged
        If Label36.Text <> 0 Then
            Label36.Visible = True
            Panel35.Visible = True
        Else
            Label36.Visible = False
            Panel35.Visible = False
        End If
    End Sub

    Private Sub Label37_TextChanged(sender As Object, e As EventArgs) Handles Label37.TextChanged
        If Label37.Text <> 0 Then
            Label37.Visible = True
            Panel32.Visible = True
        Else
            Label37.Visible = False
            Panel32.Visible = False
        End If
    End Sub

    Private Sub Label38_TextChanged(sender As Object, e As EventArgs) Handles Label38.TextChanged
        If Label38.Text <> 0 Then
            Label38.Visible = True
            Panel37.Visible = True
        Else
            Label38.Visible = False
            Panel37.Visible = False
        End If
    End Sub

    Private Sub Label39_TextChanged(sender As Object, e As EventArgs) Handles Label39.TextChanged
        If Label39.Text <> 0 Then
            Label39.Visible = True
            Panel36.Visible = True
        Else
            Label39.Visible = False
            Panel36.Visible = False
        End If
    End Sub

    Private Sub Label40_TextChanged(sender As Object, e As EventArgs) Handles Label40.TextChanged
        If Label40.Text <> 0 Then
            Label40.Visible = True
            Panel29.Visible = True
        Else
            Label40.Visible = False
            Panel29.Visible = False
        End If
    End Sub

    Private Sub Label41_TextChanged(sender As Object, e As EventArgs) Handles Label41.TextChanged
        If Label41.Text <> 0 Then
            Label41.Visible = True
            Panel28.Visible = True
        Else
            Label41.Visible = False
            Panel28.Visible = False
        End If
    End Sub

    Private Sub Label42_TextChanged(sender As Object, e As EventArgs) Handles Label42.TextChanged
        If Label42.Text <> 0 Then
            Label42.Visible = True
            Panel27.Visible = True
        Else
            Label42.Visible = False
            Panel27.Visible = False
        End If
    End Sub

    Private Sub Label43_TextChanged(sender As Object, e As EventArgs) Handles Label43.TextChanged
        If Label43.Text <> 0 Then
            Label43.Visible = True
            Panel30.Visible = True
        Else
            Label43.Visible = False
            Panel30.Visible = False
        End If
    End Sub

    Private Sub Label44_TextChanged(sender As Object, e As EventArgs) Handles Label44.TextChanged
        If Label44.Text <> 0 Then
            Label44.Visible = True
            Panel44.Visible = True
        Else
            Label44.Visible = False
            Panel44.Visible = False
        End If
    End Sub

    Private Sub Label45_TextChanged(sender As Object, e As EventArgs) Handles Label45.TextChanged
        If Label45.Text <> 0 Then
            Label45.Visible = True
            Panel45.Visible = True
        Else
            Label45.Visible = False
            Panel45.Visible = False
        End If
    End Sub

    Private Sub Label46_TextChanged(sender As Object, e As EventArgs) Handles Label46.TextChanged
        If Label46.Text <> 0 Then
            Label46.Visible = True
            Panel40.Visible = True
        Else
            Label46.Visible = False
            Panel40.Visible = False
        End If
    End Sub

    Private Sub Label47_TextChanged(sender As Object, e As EventArgs) Handles Label47.TextChanged
        If Label47.Text <> 0 Then
            Label47.Visible = True
            Panel39.Visible = True
        Else
            Label47.Visible = False
            Panel39.Visible = False
        End If
    End Sub

    Private Sub Label48_TextChanged(sender As Object, e As EventArgs) Handles Label48.TextChanged
        If Label48.Text <> 0 Then
            Label48.Visible = True
            Panel38.Visible = True
        Else
            Label48.Visible = False
            Panel38.Visible = False
        End If
    End Sub

    Private Sub Label49_TextChanged(sender As Object, e As EventArgs) Handles Label49.TextChanged
        If Label49.Text <> 0 Then
            Label49.Visible = True
            Panel41.Visible = True
        Else
            Label49.Visible = False
            Panel41.Visible = False
        End If
    End Sub

    Private Sub Label50_TextChanged(sender As Object, e As EventArgs) Handles Label50.TextChanged
        If Label50.Text <> 0 Then
            Label50.Visible = True
            Panel46.Visible = True
        Else
            Label50.Visible = False
            Panel46.Visible = False
        End If
    End Sub

    Private Sub Label51_TextChanged(sender As Object, e As EventArgs) Handles Label51.TextChanged
        If Label51.Text <> 0 Then
            Label51.Visible = True
            Panel47.Visible = True
        Else
            Label51.Visible = False
            Panel47.Visible = False
        End If
    End Sub

    Private Sub Label52_TextChanged(sender As Object, e As EventArgs) Handles Label52.TextChanged
        If Label52.Text <> 0 Then
            Label52.Visible = True
            Panel49.Visible = True
        Else
            Label52.Visible = False
            Panel49.Visible = False
        End If
    End Sub

    Private Sub Label53_TextChanged(sender As Object, e As EventArgs) Handles Label53.TextChanged
        If Label53.Text <> 0 Then
            Label53.Visible = True
            Panel42.Visible = True
        Else
            Label53.Visible = False
            Panel42.Visible = False
        End If
    End Sub

    Private Sub Label54_TextChanged(sender As Object, e As EventArgs) Handles Label54.TextChanged
        If Label54.Text <> 0 Then
            Label54.Visible = True
            Panel43.Visible = True
        Else
            Label54.Visible = False
            Panel43.Visible = False
        End If
    End Sub

    Private Sub Label55_TextChanged(sender As Object, e As EventArgs) Handles Label55.TextChanged
        If Label55.Text <> 0 Then
            Label55.Visible = True
            Panel48.Visible = True
        Else
            Label55.Visible = False
            Panel48.Visible = False
        End If
    End Sub

    Private Sub Label56_TextChanged(sender As Object, e As EventArgs) Handles Label56.TextChanged
        If Label56.Text <> 0 Then
            Label56.Visible = True
            Panel60.Visible = True
        Else
            Label56.Visible = False
            Panel60.Visible = False
        End If
    End Sub

    Private Sub Label57_TextChanged(sender As Object, e As EventArgs) Handles Label57.TextChanged
        If Label57.Text <> 0 Then
            Label57.Visible = True
            Panel61.Visible = True
        Else
            Label57.Visible = False
            Panel61.Visible = False
        End If
    End Sub

    Private Sub Label58_TextChanged(sender As Object, e As EventArgs) Handles Label58.TextChanged
        If Label58.Text <> 0 Then
            Label58.Visible = True
            Panel59.Visible = True
        Else
            Label58.Visible = False
            Panel59.Visible = False
        End If
    End Sub

    Private Sub Label59_TextChanged(sender As Object, e As EventArgs) Handles Label59.TextChanged
        If Label59.Text <> 0 Then
            Label59.Visible = True
            Panel50.Visible = True
        Else
            Label59.Visible = False
            Panel50.Visible = False
        End If
    End Sub

    Private Sub Label60_TextChanged(sender As Object, e As EventArgs) Handles Label60.TextChanged
        If Label60.Text <> 0 Then
            Label60.Visible = True
            Panel51.Visible = True
        Else
            Label60.Visible = False
            Panel51.Visible = False
        End If
    End Sub

    Private Sub Label61_TextChanged(sender As Object, e As EventArgs) Handles Label61.TextChanged
        If Label61.Text <> 0 Then
            Label61.Visible = True
            Panel52.Visible = True
        Else
            Label61.Visible = False
            Panel52.Visible = False
        End If
    End Sub

    Private Sub Label62_TextChanged(sender As Object, e As EventArgs) Handles Label62.TextChanged
        If Label62.Text <> 0 Then
            Label62.Visible = True
            Panel53.Visible = True
        Else
            Label62.Visible = False
            Panel53.Visible = False
        End If
    End Sub

    Private Sub Label63_TextChanged(sender As Object, e As EventArgs) Handles Label63.TextChanged
        If Label63.Text <> 0 Then
            Label63.Visible = True
            Panel54.Visible = True
        Else
            Label63.Visible = False
            Panel54.Visible = False
        End If
    End Sub

    Private Sub Label64_TextChanged(sender As Object, e As EventArgs) Handles Label64.TextChanged
        If Label64.Text <> 0 Then
            Label64.Visible = True
            Panel55.Visible = True
        Else
            Label64.Visible = False
            Panel55.Visible = False
        End If
    End Sub

    Private Sub Label65_TextChanged(sender As Object, e As EventArgs) Handles Label65.TextChanged
        If Label65.Text <> 0 Then
            Label65.Visible = True
            Panel56.Visible = True
        Else
            Label65.Visible = False
            Panel56.Visible = False
        End If
    End Sub

    Private Sub Label66_TextChanged(sender As Object, e As EventArgs) Handles Label66.TextChanged
        If Label66.Text <> 0 Then
            Label66.Visible = True
            Panel57.Visible = True
        Else
            Label66.Visible = False
            Panel57.Visible = False
        End If
    End Sub

    Private Sub Label67_TextChanged(sender As Object, e As EventArgs) Handles Label67.TextChanged
        If Label67.Text <> 0 Then
            Label67.Visible = True
            Panel58.Visible = True
        Else
            Label67.Visible = False
            Panel58.Visible = False
        End If
    End Sub

    Private Sub Label68_TextChanged(sender As Object, e As EventArgs) Handles Label68.TextChanged
        If Label68.Text <> 0 Then
            Label68.Visible = True
            Panel62.Visible = True
        Else
            Label68.Visible = False
            Panel62.Visible = False
        End If
    End Sub

    Private Sub Label69_TextChanged(sender As Object, e As EventArgs) Handles Label69.TextChanged
        If Label69.Text <> 0 Then
            Label69.Visible = True
            Panel63.Visible = True
        Else
            Label69.Visible = False
            Panel63.Visible = False
        End If
    End Sub

    Private Sub btn_kelinci_Click(sender As Object, e As EventArgs) Handles btn_kelinci.Click, Panel10.Click, Label8.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label8.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label8.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Kelinci','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_naga_Click(sender As Object, e As EventArgs) Handles btn_naga.Click, Panel6.Click, Label11.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label11.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label11.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Naga','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_ular_Click(sender As Object, e As EventArgs) Handles btn_ular.Click, Panel7.Click, Label14.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label14.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label14.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Ular','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_macan_Click(sender As Object, e As EventArgs) Handles btn_macan.Click, Panel11.Click, Label17.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label17.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label17.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Macan','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_kerbau_Click(sender As Object, e As EventArgs) Handles btn_kerbau.Click, Panel9.Click, Label9.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label9.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label9.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Kerbau','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_ayam_Click(sender As Object, e As EventArgs) Handles btn_ayam.Click, Panel3.Click, Label10.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label10.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label10.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Ayam','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_tikus_Click(sender As Object, e As EventArgs) Handles btn_tikus.Click, Panel12.Click, Label12.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label12.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label12.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Tikus','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_monyet_Click(sender As Object, e As EventArgs) Handles btn_monyet.Click, Panel8.Click, Label13.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label13.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label13.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Monyet','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_babi_Click(sender As Object, e As EventArgs) Handles btn_babi.Click, Panel13.Click, Label15.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label15.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label15.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Babi','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_kambing_Click(sender As Object, e As EventArgs) Handles btn_kambing.Click, Panel4.Click, Label16.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label16.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label16.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Kambing','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_anjing_Click(sender As Object, e As EventArgs) Handles btn_anjing.Click, Panel1.Click, Label18.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label18.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label18.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Anjing','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_kuda_Click(sender As Object, e As EventArgs) Handles btn_kuda.Click, Panel5.Click, Label19.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label19.Text) + jumlahBet
            If temp > maxbet_binatang Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label19.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, binatang, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Kuda','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_bonus_Click(sender As Object, e As EventArgs) Handles btn_bonus.Click, Panel60.Click, Label56.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label56.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label56.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Bonus','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_red_Click(sender As Object, e As EventArgs) Handles btn_red.Click, Panel61.Click, Label57.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label57.Text) + jumlahBet
            If temp > maxbet_warna Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label57.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, warna, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Merah','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_black_Click(sender As Object, e As EventArgs) Handles btn_black.Click, Panel59.Click, Label58.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label58.Text) + jumlahBet
            If temp > maxbet_warna Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label58.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, warna, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Hitam','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_1_Click(sender As Object, e As EventArgs) Handles btn_1.Click, Panel14.Click, Label25.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label25.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label25.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'1','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_2_Click(sender As Object, e As EventArgs) Handles btn_2.Click, Panel15.Click, Label24.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label24.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label24.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'2','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_3_Click(sender As Object, e As EventArgs) Handles btn_3.Click, Panel17.Click, Label23.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label23.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label23.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'3','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_4_Click(sender As Object, e As EventArgs) Handles btn_4.Click, Panel18.Click, Label22.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label22.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label22.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'4','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_5_Click(sender As Object, e As EventArgs) Handles btn_5.Click, Panel19.Click, Label21.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label21.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label21.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'5','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_6_Click(sender As Object, e As EventArgs) Handles btn_6.Click, Panel16.Click, Label20.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label20.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label20.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'6','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_12_Click(sender As Object, e As EventArgs) Handles btn_12.Click, Panel25.Click, Label26.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label26.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label26.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'12','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_11_Click(sender As Object, e As EventArgs) Handles btn_11.Click, Panel24.Click, Label27.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label27.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label27.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'11','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_9_Click(sender As Object, e As EventArgs) Handles btn_9.Click, Panel22.Click, Label28.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label28.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label28.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'9','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_8_Click(sender As Object, e As EventArgs) Handles btn_8.Click, Panel21.Click, Label29.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label29.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label29.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'8','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_7_Click(sender As Object, e As EventArgs) Handles btn_7.Click, Panel20.Click, Label30.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label30.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label30.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'7','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_10_Click(sender As Object, e As EventArgs) Handles btn_10.Click, Panel23.Click, Label31.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label31.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label31.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'10','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_18_Click(sender As Object, e As EventArgs) Handles btn_18.Click, Panel26.Click, Label32.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label32.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label32.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'18','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_17_Click(sender As Object, e As EventArgs) Handles btn_17.Click, Panel31.Click, Label33.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label33.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label33.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'17','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_16_Click(sender As Object, e As EventArgs) Handles btn_16.Click, Panel32.Click, Label37.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label37.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label37.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'16','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_15_Click(sender As Object, e As EventArgs) Handles btn_15.Click, Panel33.Click, Label34.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label34.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label34.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'15','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_14_Click(sender As Object, e As EventArgs) Handles btn_14.Click, Panel34.Click, Label35.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label35.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label35.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'14','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_13_Click(sender As Object, e As EventArgs) Handles btn_13.Click, Panel35.Click, Label36.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label36.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label36.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'13','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_24_Click(sender As Object, e As EventArgs) Handles btn_24.Click, Panel37.Click, Label38.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label38.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label38.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'24','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_23_Click(sender As Object, e As EventArgs) Handles btn_23.Click, Panel36.Click, Label39.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label39.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label39.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'23','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_21_Click(sender As Object, e As EventArgs) Handles btn_21.Click, Panel29.Click, Label40.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label40.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label40.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'21','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_20_Click(sender As Object, e As EventArgs) Handles btn_20.Click, Panel28.Click, Label41.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label41.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label41.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'20','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_19_Click(sender As Object, e As EventArgs) Handles btn_19.Click, Panel27.Click, Label42.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label42.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label42.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'19','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_22_Click(sender As Object, e As EventArgs) Handles btn_22.Click, Panel30.Click, Label43.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label43.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label43.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'22','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_30_Click(sender As Object, e As EventArgs) Handles btn_30.Click, Panel44.Click, Label44.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label44.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label44.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'30','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_29_Click(sender As Object, e As EventArgs) Handles btn_29.Click, Panel45.Click, Label45.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label45.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label45.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'29','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_27_Click(sender As Object, e As EventArgs) Handles btn_27.Click, Panel40.Click, Label46.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label46.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label46.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'27','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_26_Click(sender As Object, e As EventArgs) Handles btn_26.Click, Panel39.Click, Label47.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label47.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label47.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'26','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_25_Click(sender As Object, e As EventArgs) Handles btn_25.Click, Panel38.Click, Label48.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label48.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label48.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'25','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_28_Click(sender As Object, e As EventArgs) Handles btn_28.Click, Panel41.Click, Label49.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label49.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label49.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'28','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_36_Click(sender As Object, e As EventArgs) Handles btn_36.Click, Panel46.Click, Label50.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label50.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label50.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'36','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_35_Click(sender As Object, e As EventArgs) Handles btn_35.Click, Panel47.Click, Label51.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label51.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label51.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'35','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_33_Click(sender As Object, e As EventArgs) Handles btn_33.Click, Panel49.Click, Label52.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label52.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label52.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'33','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_32_Click(sender As Object, e As EventArgs) Handles btn_32.Click, Panel42.Click, Label53.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label53.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label53.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'32','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_31_Click(sender As Object, e As EventArgs) Handles btn_31.Click, Panel43.Click, Label54.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label54.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label54.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'31','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_34_Click(sender As Object, e As EventArgs) Handles btn_34.Click, Panel48.Click, Label55.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label55.Text) + jumlahBet
            If temp > maxbet_angka Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label55.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, angka, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'34','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If

    End Sub

    Private Sub btn_1_12_Click(sender As Object, e As EventArgs) Handles btn_1_12.Click, Panel50.Click, Label59.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label59.Text) + jumlahBet
            If temp > maxbet_grup1 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label59.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'1-12','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_13_24_Click(sender As Object, e As EventArgs) Handles btn_13_24.Click, Panel51.Click, Label60.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label60.Text) + jumlahBet
            If temp > maxbet_grup1 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label60.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'13-24','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_25_36_Click(sender As Object, e As EventArgs) Handles btn_25_36.Click, Panel52.Click, Label61.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label61.Text) + jumlahBet
            If temp > maxbet_grup1 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label61.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'25-36','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_grup_1_Click(sender As Object, e As EventArgs) Handles btn_grup_1.Click, Panel53.Click, Label62.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label62.Text) + jumlahBet
            If temp > maxbet_grup2 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label62.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Grup 1','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_grup_2_Click(sender As Object, e As EventArgs) Handles btn_grup_2.Click, Panel54.Click, Label63.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label63.Text) + jumlahBet
            If temp > maxbet_grup2 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label63.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Grup 2','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_grup_3_Click(sender As Object, e As EventArgs) Handles btn_grup_3.Click, Panel55.Click, Label64.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label64.Text) + jumlahBet
            If temp > maxbet_grup2 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label64.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Grup 3','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_grup_4_Click(sender As Object, e As EventArgs) Handles btn_grup_4.Click, Panel56.Click, Label65.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label65.Text) + jumlahBet
            If temp > maxbet_grup2 Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label65.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Grup 4','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_genap_Click(sender As Object, e As EventArgs) Handles btn_genap.Click, Panel57.Click, Label66.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label66.Text) + jumlahBet
            If temp > maxbet_genap_ganjil Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label66.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Genap','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_ganjil_Click(sender As Object, e As EventArgs) Handles btn_ganjil.Click, Panel58.Click, Label67.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label67.Text) + jumlahBet
            If temp > maxbet_genap_ganjil Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label67.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Ganjil','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_besar_Click(sender As Object, e As EventArgs) Handles btn_besar.Click, Panel62.Click, Label68.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label68.Text) + jumlahBet
            If temp > maxbet_besar_kecil Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label68.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Besar','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_kecil_Click(sender As Object, e As EventArgs) Handles btn_kecil.Click, Panel63.Click, Label69.Click
        If btn_bet_10.Enabled = False Then
            jumlahBet = 10
        ElseIf btn_bet_100.Enabled = False Then
            jumlahBet = 100
        ElseIf btn_bet_500.Enabled = False Then
            jumlahBet = 500
        ElseIf btn_bet_1000.Enabled = False Then
            jumlahBet = 5000
        End If
        If txt_credit.Text >= jumlahBet Then
            temp = Val(Label69.Text) + jumlahBet
            If temp > maxbet_besar_kecil Then
                'MsgBox("Max bet melampaui batas")
            Else
                Label69.Text += jumlahBet
                txt_bet.Text += jumlahBet
                txt_credit.Text -= jumlahBet
                Creates("INSERT INTO tb_bet (username, id_permainan, keterangan, bet) VALUES ('" & txt_user.Text & "',(SELECT id_permainan FROM tb_permainan WHERE status = 1),'Kecil','" & jumlahBet & "')")
                conn.Close()
                My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
            End If
        Else
            Timer10.Enabled = True
        End If
    End Sub

    Private Sub btn_repeat_Click(sender As Object, e As EventArgs) Handles btn_repeat.Click
        Call Koneksi()
        cmd2 = New OdbcCommand("SELECT * FROM tb_bet where username = '" & txt_user.Text & "' AND id_permainan = (select max(id_permainan) from tb_bet where username = '" & txt_user.Text & "')", conn)
        Using rd2 As OdbcDataReader = cmd2.ExecuteReader
            While rd2.Read()
                If rd2!bet <= txt_credit.Text Then
                    If Not rd2.IsDBNull(3) Then
                        If rd2!binatang = "Anjing" Then
                            Label18.Text += rd2!bet
                        ElseIf rd2!binatang = "Ayam" Then
                            Label10.Text += rd2!bet
                        ElseIf rd2!binatang = "Monyet" Then
                            Label13.Text += rd2!bet
                        ElseIf rd2!binatang = "Kambing" Then
                            Label16.Text += rd2!bet
                        ElseIf rd2!binatang = "Kuda" Then
                            Label19.Text += rd2!bet
                        ElseIf rd2!binatang = "Ular" Then
                            Label14.Text += rd2!bet
                        ElseIf rd2!binatang = "Naga" Then
                            Label11.Text += rd2!bet
                        ElseIf rd2!binatang = "Kelinci" Then
                            Label8.Text += rd2!bet
                        ElseIf rd2!binatang = "Macan" Then
                            Label17.Text += rd2!bet
                        ElseIf rd2!binatang = "Kerbau" Then
                            Label9.Text += rd2!bet
                        ElseIf rd2!binatang = "Tikus" Then
                            Label12.Text += rd2!bet
                        ElseIf rd2!binatang = "Babi" Then
                            Label15.Text += rd2!bet
                        End If
                    ElseIf Not rd2.IsDBNull(4) Then
                        If rd2!angka = "1" Then
                            Label25.Text += rd2!bet
                        ElseIf rd2!angka = "2" Then
                            Label24.Text += rd2!bet
                        ElseIf rd2!angka = "3" Then
                            Label23.Text += rd2!bet
                        ElseIf rd2!angka = "4" Then
                            Label22.Text += rd2!bet
                        ElseIf rd2!angka = "5" Then
                            Label21.Text += rd2!bet
                        ElseIf rd2!angka = "6" Then
                            Label20.Text += rd2!bet
                        ElseIf rd2!angka = "7" Then
                            Label30.Text += rd2!bet
                        ElseIf rd2!angka = "8" Then
                            Label29.Text += rd2!bet
                        ElseIf rd2!angka = "9" Then
                            Label28.Text += rd2!bet
                        ElseIf rd2!angka = "10" Then
                            Label31.Text += rd2!bet
                        ElseIf rd2!angka = "11" Then
                            Label27.Text += rd2!bet
                        ElseIf rd2!angka = "12" Then
                            Label26.Text += rd2!bet
                        ElseIf rd2!angka = "13" Then
                            Label36.Text += rd2!bet
                        ElseIf rd2!angka = "14" Then
                            Label35.Text += rd2!bet
                        ElseIf rd2!angka = "15" Then
                            Label34.Text += rd2!bet
                        ElseIf rd2!angka = "16" Then
                            Label37.Text += rd2!bet
                        ElseIf rd2!angka = "17" Then
                            Label33.Text += rd2!bet
                        ElseIf rd2!angka = "18" Then
                            Label32.Text += rd2!bet
                        ElseIf rd2!angka = "19" Then
                            Label42.Text += rd2!bet
                        ElseIf rd2!angka = "20" Then
                            Label41.Text += rd2!bet
                        ElseIf rd2!angka = "21" Then
                            Label40.Text += rd2!bet
                        ElseIf rd2!angka = "22" Then
                            Label43.Text += rd2!bet
                        ElseIf rd2!angka = "23" Then
                            Label39.Text += rd2!bet
                        ElseIf rd2!angka = "24" Then
                            Label38.Text += rd2!bet
                        ElseIf rd2!angka = "25" Then
                            Label48.Text += rd2!bet
                        ElseIf rd2!angka = "26" Then
                            Label47.Text += rd2!bet
                        ElseIf rd2!angka = "27" Then
                            Label46.Text += rd2!bet
                        ElseIf rd2!angka = "28" Then
                            Label49.Text += rd2!bet
                        ElseIf rd2!angka = "29" Then
                            Label45.Text += rd2!bet
                        ElseIf rd2!angka = "30" Then
                            Label44.Text += rd2!bet
                        ElseIf rd2!angka = "31" Then
                            Label54.Text += rd2!bet
                        ElseIf rd2!angka = "32" Then
                            Label53.Text += rd2!bet
                        ElseIf rd2!angka = "33" Then
                            Label52.Text += rd2!bet
                        ElseIf rd2!angka = "34" Then
                            Label55.Text += rd2!bet
                        ElseIf rd2!angka = "35" Then
                            Label51.Text += rd2!bet
                        ElseIf rd2!angka = "36" Then
                            Label50.Text += rd2!bet
                        End If
                    ElseIf Not rd2.IsDBNull(5) Then
                        If rd2!warna = "Hitam" Then
                            Label58.Text += rd2!bet
                        ElseIf rd2!warna = "Merah" Then
                            Label57.Text += rd2!bet
                        End If
                    ElseIf Not rd2.IsDBNull(6) Then
                        If rd2!keterangan = "Bonus" Then
                            Label56.Text += rd2!bet
                        ElseIf rd2!keterangan = "1-12" Then
                            Label59.Text += rd2!bet
                        ElseIf rd2!keterangan = "13-24" Then
                            Label60.Text += rd2!bet
                        ElseIf rd2!keterangan = "25-36" Then
                            Label61.Text += rd2!bet
                        ElseIf rd2!keterangan = "Grup 1" Then
                            Label62.Text += rd2!bet
                        ElseIf rd2!keterangan = "Grup 2" Then
                            Label63.Text += rd2!bet
                        ElseIf rd2!keterangan = "Grup 3" Then
                            Label64.Text += rd2!bet
                        ElseIf rd2!keterangan = "Grup 4" Then
                            Label65.Text += rd2!bet
                        ElseIf rd2!keterangan = "Genap" Then
                            Label66.Text += rd2!bet
                        ElseIf rd2!keterangan = "Ganjil" Then
                            Label67.Text += rd2!bet
                        ElseIf rd2!keterangan = "Besar" Then
                            Label68.Text += rd2!bet
                        ElseIf rd2!keterangan = "Kecil" Then
                            Label69.Text += rd2!bet
                        End If
                    End If
                    txt_credit.Text -= rd2!bet
                    txt_bet.Text += rd2!bet
                End If
            End While
        End Using
        conn.Close()
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\pasang_koin.wav")
        btn_repeat.Visible = False
        btn_cancel.Visible = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Call Koneksi()
        Try
            cmd2 = New OdbcCommand("SELECT * FROM tb_permainan where status = 2", conn)
            Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                While rd2.Read()
                    Dim win_binatang As Double = 0
                    Dim win_warna As Double = 0
                    Dim win_angka As Double = 0
                    Dim win_bonus As Double = 0
                    Dim win_genap_ganjil As Double = 0
                    Dim win_besar_kecil As Double = 0
                    Dim win_grup1 As Double = 0
                    Dim win_grup2 As Double = 0
                    Dim list_shio As Integer

                    If rd2!keterangan = "Bonus" Then
                        win_bonus = Val(Label56.Text) * kali_bonus
                        shio.Visible = True
                        shio.BackgroundImage = My.Resources.ResourceManager.GetObject("JP")
                        Timer7.Enabled = False
                        txt_credit.Text += Val(Label58.Text) + Val(Label57.Text) + Val(Label59.Text) + Val(Label60.Text) + Val(Label61.Text) + Val(Label62.Text) + Val(Label63.Text) + Val(Label64.Text) + Val(Label65.Text) + Val(Label66.Text) + Val(Label67.Text) + Val(Label68.Text) + Val(Label69.Text)
                        txt_bet.Text -= Val(Label58.Text) + Val(Label57.Text) + Val(Label59.Text) + Val(Label60.Text) + Val(Label61.Text) + Val(Label62.Text) + Val(Label63.Text) + Val(Label64.Text) + Val(Label65.Text) + Val(Label66.Text) + Val(Label67.Text) + Val(Label68.Text) + Val(Label69.Text)
                        txt_win.Text += win_bonus
                        txt_credit.Text += win_bonus
                        Ubahs("UPDATE tb_user SET koin = '" & txt_credit.Text & "' WHERE username = '" & txt_user.Text & "'")
                        Timer7.Enabled = True
                        Label57.Text = 0
                        Label58.Text = 0
                        Label59.Text = 0
                        Label60.Text = 0
                        Label61.Text = 0
                        Label62.Text = 0
                        Label63.Text = 0
                        Label64.Text = 0
                        Label65.Text = 0
                        Label66.Text = 0
                        Label67.Text = 0
                        Label68.Text = 0
                        Label69.Text = 0
                        If bonus_music > 0 Then
                            bonus_loop += 1
                            My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\" & bonus_loop & "bonus.wav")
                            If bonus_loop = bonus_music Then
                                bonus_loop = 0
                            End If
                        End If
                        Timer3.Enabled = False
                        Timer4.Enabled = True
                    Else
                        If rd2!angka = 1 Then
                            win_angka = Val(Label25.Text) * kali_angka
                            win_binatang = Val(Label15.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Babi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_1")
                            list_shio = 48
                        ElseIf rd2!angka = 2 Then
                            win_angka = Val(Label24.Text) * kali_angka
                            win_binatang = Val(Label18.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Anjing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_2")
                            list_shio = 37
                        ElseIf rd2!angka = 3 Then
                            win_angka = Val(Label23.Text) * kali_angka
                            win_binatang = Val(Label10.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ayam")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_3")
                            list_shio = 38
                        ElseIf rd2!angka = 4 Then
                            win_angka = Val(Label22.Text) * kali_angka
                            win_binatang = Val(Label13.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Monyet")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_4")
                            list_shio = 39
                        ElseIf rd2!angka = 5 Then
                            win_angka = Val(Label21.Text) * kali_angka
                            win_binatang = Val(Label16.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kambing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_5")
                            list_shio = 40
                        ElseIf rd2!angka = 6 Then
                            win_angka = Val(Label20.Text) * kali_angka
                            win_binatang = Val(Label19.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kuda")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_6")
                            list_shio = 41
                        ElseIf rd2!angka = 7 Then
                            win_angka = Val(Label30.Text) * kali_angka
                            win_binatang = Val(Label14.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ular")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_7")
                            list_shio = 42
                        ElseIf rd2!angka = 8 Then
                            win_angka = Val(Label29.Text) * kali_angka
                            win_binatang = Val(Label11.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Naga")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_8")
                            list_shio = 43
                        ElseIf rd2!angka = 9 Then
                            win_angka = Val(Label28.Text) * kali_angka
                            win_binatang = Val(Label8.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kelinci")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_9")
                            list_shio = 44
                        ElseIf rd2!angka = 10 Then
                            win_angka = Val(Label31.Text) * kali_angka
                            win_binatang = Val(Label17.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Harimau")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_10")
                            list_shio = 45
                        ElseIf rd2!angka = 11 Then
                            win_angka = Val(Label27.Text) * kali_angka
                            win_binatang = Val(Label9.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Sapi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_11")
                            list_shio = 46
                        ElseIf rd2!angka = 12 Then
                            win_angka = Val(Label26.Text) * kali_angka
                            win_binatang = Val(Label12.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label59.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Tikus")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_12")
                            list_shio = 47
                        ElseIf rd2!angka = 13 Then
                            win_angka = Val(Label36.Text) * kali_angka
                            win_binatang = Val(Label15.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Babi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_13")
                            list_shio = 48
                        ElseIf rd2!angka = 14 Then
                            win_angka = Val(Label35.Text) * kali_angka
                            win_binatang = Val(Label18.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Anjing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_14")
                            list_shio = 37
                        ElseIf rd2!angka = 15 Then
                            win_angka = Val(Label34.Text) * kali_angka
                            win_binatang = Val(Label10.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ayam")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_15")
                            list_shio = 38
                        ElseIf rd2!angka = 16 Then
                            win_angka = Val(Label37.Text) * kali_angka
                            win_binatang = Val(Label13.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Monyet")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_16")
                            list_shio = 39
                        ElseIf rd2!angka = 17 Then
                            win_angka = Val(Label33.Text) * kali_angka
                            win_binatang = Val(Label16.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kambing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_17")
                            list_shio = 40
                        ElseIf rd2!angka = 18 Then
                            win_angka = Val(Label32.Text) * kali_angka
                            win_binatang = Val(Label19.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label69.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kuda")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_18")
                            list_shio = 41
                        ElseIf rd2!angka = 19 Then
                            win_angka = Val(Label42.Text) * kali_angka
                            win_binatang = Val(Label14.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ular")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_19")
                            list_shio = 42
                        ElseIf rd2!angka = 20 Then
                            win_angka = Val(Label41.Text) * kali_angka
                            win_binatang = Val(Label11.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Naga")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_20")
                            list_shio = 43
                        ElseIf rd2!angka = 21 Then
                            win_angka = Val(Label40.Text) * kali_angka
                            win_binatang = Val(Label8.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kelinci")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_21")
                            list_shio = 44
                        ElseIf rd2!angka = 22 Then
                            win_angka = Val(Label43.Text) * kali_angka
                            win_binatang = Val(Label17.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Harimau")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_22")
                            list_shio = 45
                        ElseIf rd2!angka = 23 Then
                            win_angka = Val(Label39.Text) * kali_angka
                            win_binatang = Val(Label9.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Sapi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_23")
                            list_shio = 46
                        ElseIf rd2!angka = 24 Then
                            win_angka = Val(Label38.Text) * kali_angka
                            win_binatang = Val(Label12.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label60.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Tikus")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_24")
                            list_shio = 47
                        ElseIf rd2!angka = 25 Then
                            win_angka = Val(Label48.Text) * kali_angka
                            win_binatang = Val(Label15.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Babi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_25")
                            list_shio = 48
                        ElseIf rd2!angka = 26 Then
                            win_angka = Val(Label47.Text) * kali_angka
                            win_binatang = Val(Label18.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Anjing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_26")
                            list_shio = 37
                        ElseIf rd2!angka = 27 Then
                            win_angka = Val(Label46.Text) * kali_angka
                            win_binatang = Val(Label10.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label62.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ayam")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_27")
                            list_shio = 38
                        ElseIf rd2!angka = 28 Then
                            win_angka = Val(Label49.Text) * kali_angka
                            win_binatang = Val(Label13.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Monyet")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_28")
                            list_shio = 39
                        ElseIf rd2!angka = 29 Then
                            win_angka = Val(Label45.Text) * kali_angka
                            win_binatang = Val(Label16.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kambing")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_29")
                            list_shio = 40
                        ElseIf rd2!angka = 30 Then
                            win_angka = Val(Label44.Text) * kali_angka
                            win_binatang = Val(Label19.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label63.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kuda")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_30")
                            list_shio = 41
                        ElseIf rd2!angka = 31 Then
                            win_angka = Val(Label54.Text) * kali_angka
                            win_binatang = Val(Label14.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Ular")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_31")
                            list_shio = 42
                        ElseIf rd2!angka = 32 Then
                            win_angka = Val(Label53.Text) * kali_angka
                            win_binatang = Val(Label11.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Naga")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_32")
                            list_shio = 43
                        ElseIf rd2!angka = 33 Then
                            win_angka = Val(Label52.Text) * kali_angka
                            win_binatang = Val(Label8.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label64.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Kelinci")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_33")
                            list_shio = 44
                        ElseIf rd2!angka = 34 Then
                            win_angka = Val(Label55.Text) * kali_angka
                            win_binatang = Val(Label17.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Harimau")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_34")
                            list_shio = 45
                        ElseIf rd2!angka = 35 Then
                            win_angka = Val(Label51.Text) * kali_angka
                            win_binatang = Val(Label9.Text) * kali_binatang
                            win_warna = Val(Label57.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label67.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Sapi")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_35")
                            list_shio = 46
                        ElseIf rd2!angka = 36 Then
                            win_angka = Val(Label50.Text) * kali_angka
                            win_binatang = Val(Label12.Text) * kali_binatang
                            win_warna = Val(Label58.Text) * kali_warna
                            win_grup1 = Val(Label61.Text) * kali_grup1
                            win_grup2 = Val(Label65.Text) * kali_grup2
                            win_genap_ganjil = Val(Label66.Text) * kali_genap_ganjil
                            win_besar_kecil = Val(Label68.Text) * kali_besar_kecil
                            shio.BackgroundImage = My.Resources.ResourceManager.GetObject("Tikus")
                            angka.BackgroundImage = My.Resources.ResourceManager.GetObject("angka_36")
                            list_shio = 47
                        End If

                        txt_win.Text = (win_angka + win_binatang + win_warna + win_genap_ganjil + win_besar_kecil + win_grup1 + win_grup2)
                        txt_credit.Text += Val(txt_win.Text)

                        shio.Visible = True
                        angka.Visible = True

                        If rd2!angka = 0 Then
                            shio.Visible = False
                            angka.Visible = False
                        End If

                        Ubahs("UPDATE tb_user SET koin = '" & txt_credit.Text & "' WHERE username = '" & txt_user.Text & "'")

                        ListView1.Items.Add("", rd2!angka)
                        If listHistory > 6 Then
                            ListView2.Items.Add(ListView1.Items(0).Clone())
                            If listHistory > 12 Then
                                ListView3.Items.Add(ListView2.Items(0).Clone())
                                If listHistory > 18 Then
                                    ListView4.Items.Add(ListView3.Items(0).Clone())
                                    If listHistory > 24 Then
                                        ListView4.Items.RemoveAt(0)
                                    End If
                                    ListView3.Items.RemoveAt(0)
                                End If
                                ListView2.Items.RemoveAt(0)
                            End If
                            ListView1.Items.RemoveAt(0)
                        End If
                        listHistory += 1

                        ListView5.Items.Add("", list_shio)
                        If listHistory2 > 6 Then
                            ListView6.Items.Add(ListView5.Items(0).Clone())
                            If listHistory2 > 12 Then
                                ListView7.Items.Add(ListView6.Items(0).Clone())
                                If listHistory2 > 18 Then
                                    ListView8.Items.Add(ListView7.Items(0).Clone())
                                    If listHistory2 > 24 Then
                                        ListView8.Items.RemoveAt(0)
                                    End If
                                    ListView7.Items.RemoveAt(0)
                                End If
                                ListView6.Items.RemoveAt(0)
                            End If
                            ListView5.Items.RemoveAt(0)
                        End If
                        listHistory2 += 1

                        Timer3.Enabled = False
                        Timer2.Enabled = True
                        If win_music > 0 Then
                            win_loop += 1
                            My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\" & win_loop & "result.wav")
                            If win_loop = win_music Then
                                win_loop = 0
                            End If
                        End If
                        Timer5.Enabled = True
                        btn_logout.Visible = True
                    End If
                End While
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Call Koneksi()
        Try
            cmd4 = New OdbcCommand("SELECT * FROM tb_permainan where status = 3", conn)
            Dim temp As Integer = 0
            Dim countBonusIndex As Integer = 0
            Using rd4 As OdbcDataReader = cmd4.ExecuteReader
                While rd4.Read()
                    Timer7.Enabled = False
                    Dim win_bonus As Double = 0
                    Dim list_bonus As Integer
                    Dim image_bonus As String = ""
                    If Not rd4.IsDBNull(5) Then
                        If rd4!bonus = "1" Then
                            win_bonus = Val(Label25.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_1"
                        End If
                        If rd4!bonus = "2" Then
                            win_bonus = Val(Label24.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_2"
                        End If
                        If rd4!bonus = "3" Then
                            win_bonus = Val(Label23.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_3"
                        End If
                        If rd4!bonus = "4" Then
                            win_bonus = Val(Label22.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_4"
                        End If
                        If rd4!bonus = "5" Then
                            win_bonus = Val(Label21.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_5"
                        End If
                        If rd4!bonus = "6" Then
                            win_bonus = Val(Label20.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_6"
                        End If
                        If rd4!bonus = "7" Then
                            win_bonus = Val(Label30.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_7"
                        End If
                        If rd4!bonus = "8" Then
                            win_bonus = Val(Label29.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_8"
                        End If
                        If rd4!bonus = "9" Then
                            win_bonus = Val(Label28.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_9"
                        End If
                        If rd4!bonus = "10" Then
                            win_bonus = Val(Label31.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_10"
                        End If
                        If rd4!bonus = "11" Then
                            win_bonus = Val(Label27.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_11"
                        End If
                        If rd4!bonus = "12" Then
                            win_bonus = Val(Label26.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_12"
                        End If
                        If rd4!bonus = "13" Then
                            win_bonus = Val(Label36.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_13"
                        End If
                        If rd4!bonus = "14" Then
                            win_bonus = Val(Label35.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_14"
                        End If
                        If rd4!bonus = "15" Then
                            win_bonus = Val(Label34.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_15"
                        End If
                        If rd4!bonus = "16" Then
                            win_bonus = Val(Label37.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_16"
                        End If
                        If rd4!bonus = "17" Then
                            win_bonus = Val(Label33.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_17"
                        End If
                        If rd4!bonus = "18" Then
                            win_bonus = Val(Label32.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_18"
                        End If
                        If rd4!bonus = "19" Then
                            win_bonus = Val(Label42.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_19"
                        End If
                        If rd4!bonus = "20" Then
                            win_bonus = Val(Label41.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_20"
                        End If
                        If rd4!bonus = "21" Then
                            win_bonus = Val(Label40.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_21"
                        End If
                        If rd4!bonus = "22" Then
                            win_bonus = Val(Label43.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_22"
                        End If
                        If rd4!bonus = "23" Then
                            win_bonus = Val(Label39.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_23"
                        End If
                        If rd4!bonus = "24" Then
                            win_bonus = Val(Label38.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_24"
                        End If
                        If rd4!bonus = "25" Then
                            win_bonus = Val(Label48.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_25"
                        End If
                        If rd4!bonus = "26" Then
                            win_bonus = Val(Label47.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_26"
                        End If
                        If rd4!bonus = "27" Then
                            win_bonus = Val(Label46.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_27"
                        End If
                        If rd4!bonus = "28" Then
                            win_bonus = Val(Label49.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_28"
                        End If
                        If rd4!bonus = "29" Then
                            win_bonus = Val(Label45.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_29"
                        End If
                        If rd4!bonus = "30" Then
                            win_bonus = Val(Label44.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_30"
                        End If
                        If rd4!bonus = "31" Then
                            win_bonus = Val(Label54.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_31"
                        End If
                        If rd4!bonus = "32" Then
                            win_bonus = Val(Label53.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_32"
                        End If
                        If rd4!bonus = "33" Then
                            win_bonus = Val(Label52.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_33"
                        End If
                        If rd4!bonus = "34" Then
                            win_bonus = Val(Label55.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_34"
                        End If
                        If rd4!bonus = "35" Then
                            win_bonus = Val(Label51.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_35"
                        End If
                        If rd4!bonus = "36" Then
                            win_bonus = Val(Label50.Text) * kali_bonusAngka
                            list_bonus = rd4!bonus
                            image_bonus = "angka_36"
                        End If
                        If rd4!bonus = "Anjing" Then
                            win_bonus = Val(Label18.Text) * kali_bonusHewan
                            list_bonus = 37
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Ayam" Then
                            win_bonus = Val(Label10.Text) * kali_bonusHewan
                            list_bonus = 38
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Monyet" Then
                            win_bonus = Val(Label13.Text) * kali_bonusHewan
                            list_bonus = 39
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Kambing" Then
                            win_bonus = Val(Label16.Text) * kali_bonusHewan
                            list_bonus = 40
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Kuda" Then
                            win_bonus = Val(Label19.Text) * kali_bonusHewan
                            list_bonus = 41
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Ular" Then
                            win_bonus = Val(Label14.Text) * kali_bonusHewan
                            list_bonus = 42
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Naga" Then
                            win_bonus = Val(Label11.Text) * kali_bonusHewan
                            list_bonus = 43
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Kelinci" Then
                            win_bonus = Val(Label8.Text) * kali_bonusHewan
                            list_bonus = 44
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Macan" Then
                            win_bonus = Val(Label17.Text) * kali_bonusHewan
                            list_bonus = 45
                            image_bonus = "Harimau"
                        End If
                        If rd4!bonus = "Kerbau" Then
                            win_bonus = Val(Label9.Text) * kali_bonusHewan
                            list_bonus = 46
                            image_bonus = "Sapi"
                        End If
                        If rd4!bonus = "Tikus" Then
                            win_bonus = Val(Label12.Text) * kali_bonusHewan
                            list_bonus = 47
                            image_bonus = rd4!bonus
                        End If
                        If rd4!bonus = "Babi" Then
                            win_bonus = Val(Label15.Text) * kali_bonusHewan
                            list_bonus = 48
                            image_bonus = rd4!bonus
                        End If
                        bonus_panel(countBonusIndex).Visible = True
                        bonus_panel(countBonusIndex).BackgroundImage = My.Resources.ResourceManager.GetObject(image_bonus)
                        countBonusIndex += 1
                    End If
                    If Label56.Text = 0 Then
                        win_bonus = 0
                        txt_win.Text = 0
                    End If

                    txt_win.Text += win_bonus
                    txt_credit.Text += win_bonus

                    ListView1.Items.Add("", 0)
                    If listHistory > 6 Then
                        ListView2.Items.Add(ListView1.Items(0).Clone())
                        If listHistory > 12 Then
                            ListView3.Items.Add(ListView2.Items(0).Clone())
                            If listHistory > 18 Then
                                ListView4.Items.Add(ListView3.Items(0).Clone())
                                If listHistory > 24 Then
                                    ListView4.Items.RemoveAt(0)
                                End If
                                ListView3.Items.RemoveAt(0)
                            End If
                            ListView2.Items.RemoveAt(0)
                        End If
                        ListView1.Items.RemoveAt(0)
                    End If
                    listHistory += 1

                    ListView5.Items.Add("", list_bonus)
                    If listHistory2 > 6 Then
                        ListView6.Items.Add(ListView5.Items(0).Clone())
                        If listHistory2 > 12 Then
                            ListView7.Items.Add(ListView6.Items(0).Clone())
                            If listHistory2 > 18 Then
                                ListView8.Items.Add(ListView7.Items(0).Clone())
                                If listHistory2 > 24 Then
                                    ListView8.Items.RemoveAt(0)
                                End If
                                ListView7.Items.RemoveAt(0)
                            End If
                            ListView6.Items.RemoveAt(0)
                        End If
                        ListView5.Items.RemoveAt(0)
                    End If
                    listHistory2 += 1

                    temp += 1
                    shio.Visible = False
                End While
                If temp > 0 Then
                    Ubahs("UPDATE tb_user SET koin = '" & txt_credit.Text & "' WHERE username = '" & txt_user.Text & "'")
                    Timer2.Enabled = True
                    Timer4.Enabled = False
                    Timer6.Enabled = True
                    Timer7.Enabled = True
                    btn_logout.Visible = True
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Call Koneksi()
        Try
            cmd3 = New OdbcCommand("SELECT * FROM tb_permainan where status = 2", conn)
            Using rd3 As OdbcDataReader = cmd3.ExecuteReader
                While rd3.Read()
                    If Not rd3.IsDBNull(2) Then
                        If rd3!angka = 1 Then
                            If btn_1.Visible = False Then
                                btn_1.Visible = True
                                btn_babi.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_1.Visible = True Then
                                btn_1.Visible = False
                                btn_babi.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 2 Then
                            If btn_2.Visible = False Then
                                btn_2.Visible = True
                                btn_anjing.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_1.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_2.Visible = True Then
                                btn_2.Visible = False
                                btn_anjing.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_1.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 3 Then
                            If btn_3.Visible = False Then
                                btn_3.Visible = True
                                btn_ayam.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_3.Visible = True Then
                                btn_3.Visible = False
                                btn_ayam.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 4 Then
                            If btn_4.Visible = False Then
                                btn_4.Visible = True
                                btn_monyet.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_4.Visible = True Then
                                btn_4.Visible = False
                                btn_monyet.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 5 Then
                            If btn_5.Visible = False Then
                                btn_5.Visible = True
                                btn_kambing.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_2.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_5.Visible = True Then
                                btn_5.Visible = False
                                btn_kambing.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_2.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 6 Then
                            If btn_6.Visible = False Then
                                btn_6.Visible = True
                                btn_kuda.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_6.Visible = True Then
                                btn_6.Visible = False
                                btn_kuda.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 7 Then
                            If btn_7.Visible = False Then
                                btn_7.Visible = True
                                btn_ular.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_7.Visible = True Then
                                btn_7.Visible = False
                                btn_ular.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 8 Then
                            If btn_8.Visible = False Then
                                btn_8.Visible = True
                                btn_naga.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_3.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_8.Visible = True Then
                                btn_8.Visible = False
                                btn_naga.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_3.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 9 Then
                            If btn_9.Visible = False Then
                                btn_9.Visible = True
                                btn_kelinci.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_9.Visible = True Then
                                btn_9.Visible = False
                                btn_kelinci.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 10 Then
                            If btn_10.Visible = False Then
                                btn_10.Visible = True
                                btn_macan.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_10.Visible = True Then
                                btn_10.Visible = False
                                btn_macan.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 11 Then
                            If btn_11.Visible = False Then
                                btn_11.Visible = True
                                btn_kerbau.Visible = True
                                btn_red.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_4.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_11.Visible = True Then
                                btn_11.Visible = False
                                btn_kerbau.Visible = False
                                btn_red.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_4.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 12 Then
                            If btn_12.Visible = False Then
                                btn_12.Visible = True
                                btn_tikus.Visible = True
                                btn_black.Visible = True
                                btn_1_12.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_12.Visible = True Then
                                btn_12.Visible = False
                                btn_tikus.Visible = False
                                btn_black.Visible = False
                                btn_1_12.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 13 Then
                            If btn_13.Visible = False Then
                                btn_13.Visible = True
                                btn_babi.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_13.Visible = True Then
                                btn_13.Visible = False
                                btn_babi.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 14 Then
                            If btn_14.Visible = False Then
                                btn_14.Visible = True
                                btn_anjing.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_1.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_14.Visible = True Then
                                btn_14.Visible = False
                                btn_anjing.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_1.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 15 Then
                            If btn_15.Visible = False Then
                                btn_15.Visible = True
                                btn_ayam.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_15.Visible = True Then
                                btn_15.Visible = False
                                btn_ayam.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 16 Then
                            If btn_16.Visible = False Then
                                btn_16.Visible = True
                                btn_monyet.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_16.Visible = True Then
                                btn_16.Visible = False
                                btn_monyet.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 17 Then
                            If btn_17.Visible = False Then
                                btn_17.Visible = True
                                btn_kambing.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_2.Visible = True
                                btn_ganjil.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_17.Visible = True Then
                                btn_17.Visible = False
                                btn_kambing.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_2.Visible = False
                                btn_ganjil.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 18 Then
                            If btn_18.Visible = False Then
                                btn_18.Visible = True
                                btn_kuda.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_kecil.Visible = True
                            ElseIf btn_18.Visible = True Then
                                btn_18.Visible = False
                                btn_kuda.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_kecil.Visible = False
                            End If
                        ElseIf rd3!angka = 19 Then
                            If btn_19.Visible = False Then
                                btn_19.Visible = True
                                btn_ular.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_19.Visible = True Then
                                btn_19.Visible = False
                                btn_ular.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 20 Then
                            If btn_20.Visible = False Then
                                btn_20.Visible = True
                                btn_naga.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_3.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_20.Visible = True Then
                                btn_20.Visible = False
                                btn_naga.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_3.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 21 Then
                            If btn_21.Visible = False Then
                                btn_21.Visible = True
                                btn_kelinci.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_21.Visible = True Then
                                btn_21.Visible = False
                                btn_kelinci.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 22 Then
                            If btn_22.Visible = False Then
                                btn_22.Visible = True
                                btn_macan.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_22.Visible = True Then
                                btn_22.Visible = False
                                btn_macan.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 23 Then
                            If btn_23.Visible = False Then
                                btn_23.Visible = True
                                btn_kerbau.Visible = True
                                btn_red.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_4.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_23.Visible = True Then
                                btn_23.Visible = False
                                btn_kerbau.Visible = False
                                btn_red.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_4.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 24 Then
                            If btn_24.Visible = False Then
                                btn_24.Visible = True
                                btn_tikus.Visible = True
                                btn_black.Visible = True
                                btn_13_24.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_24.Visible = True Then
                                btn_24.Visible = False
                                btn_tikus.Visible = False
                                btn_black.Visible = False
                                btn_13_24.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 25 Then
                            If btn_25.Visible = False Then
                                btn_25.Visible = True
                                btn_babi.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_25.Visible = True Then
                                btn_25.Visible = False
                                btn_babi.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 26 Then
                            If btn_26.Visible = False Then
                                btn_26.Visible = True
                                btn_anjing.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_1.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_26.Visible = True Then
                                btn_26.Visible = False
                                btn_anjing.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_1.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 27 Then
                            If btn_27.Visible = False Then
                                btn_27.Visible = True
                                btn_ayam.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_1.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_27.Visible = True Then
                                btn_27.Visible = False
                                btn_ayam.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_1.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 28 Then
                            If btn_28.Visible = False Then
                                btn_28.Visible = True
                                btn_monyet.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_28.Visible = True Then
                                btn_28.Visible = False
                                btn_monyet.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 29 Then
                            If btn_29.Visible = False Then
                                btn_29.Visible = True
                                btn_kambing.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_2.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_29.Visible = True Then
                                btn_29.Visible = False
                                btn_kambing.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_2.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 30 Then
                            If btn_30.Visible = False Then
                                btn_30.Visible = True
                                btn_kuda.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_2.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_30.Visible = True Then
                                btn_30.Visible = False
                                btn_kuda.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_2.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 31 Then
                            If btn_31.Visible = False Then
                                btn_31.Visible = True
                                btn_ular.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_31.Visible = True Then
                                btn_31.Visible = False
                                btn_ular.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 32 Then
                            If btn_32.Visible = False Then
                                btn_32.Visible = True
                                btn_naga.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_3.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_32.Visible = True Then
                                btn_32.Visible = False
                                btn_naga.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_3.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 33 Then
                            If btn_33.Visible = False Then
                                btn_33.Visible = True
                                btn_kelinci.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_3.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_33.Visible = True Then
                                btn_33.Visible = False
                                btn_kelinci.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_3.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 34 Then
                            If btn_34.Visible = False Then
                                btn_34.Visible = True
                                btn_macan.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_34.Visible = True Then
                                btn_34.Visible = False
                                btn_macan.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 35 Then
                            If btn_35.Visible = False Then
                                btn_35.Visible = True
                                btn_kerbau.Visible = True
                                btn_red.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_4.Visible = True
                                btn_ganjil.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_35.Visible = True Then
                                btn_35.Visible = False
                                btn_kerbau.Visible = False
                                btn_red.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_4.Visible = False
                                btn_ganjil.Visible = False
                                btn_besar.Visible = False
                            End If
                        ElseIf rd3!angka = 36 Then
                            If btn_36.Visible = False Then
                                btn_36.Visible = True
                                btn_tikus.Visible = True
                                btn_black.Visible = True
                                btn_25_36.Visible = True
                                btn_grup_4.Visible = True
                                btn_genap.Visible = True
                                btn_besar.Visible = True
                            ElseIf btn_36.Visible = True Then
                                btn_36.Visible = False
                                btn_tikus.Visible = False
                                btn_black.Visible = False
                                btn_25_36.Visible = False
                                btn_grup_4.Visible = False
                                btn_genap.Visible = False
                                btn_besar.Visible = False
                            End If
                        End If
                        If temp_timer > 5 Then
                            angka.Visible = False
                            shio.Visible = False
                        End If
                        temp_timer += 1
                    End If
                End While
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        Call Koneksi()
        Try
            cmd3 = New OdbcCommand("SELECT * FROM tb_permainan where status = 3 GROUP BY bonus", conn)
            Using rd3 As OdbcDataReader = cmd3.ExecuteReader
                While rd3.Read()
                    If Not rd3.IsDBNull(5) Then
                        'Timer4.Enabled = False
                        If rd3!bonus = "1" Then
                            If btn_1.Visible = True Then
                                btn_1.Visible = False
                            ElseIf btn_1.Visible = False Then
                                btn_1.Visible = True
                            End If
                        End If
                        If rd3!bonus = "2" Then
                            If btn_2.Visible = True Then
                                btn_2.Visible = False
                            ElseIf btn_2.Visible = False Then
                                btn_2.Visible = True
                            End If
                        End If
                        If rd3!bonus = "3" Then
                            If btn_3.Visible = True Then
                                btn_3.Visible = False
                            ElseIf btn_3.Visible = False Then
                                btn_3.Visible = True
                            End If
                        End If
                        If rd3!bonus = "4" Then
                            If btn_4.Visible = True Then
                                btn_4.Visible = False
                            ElseIf btn_4.Visible = False Then
                                btn_4.Visible = True
                            End If
                        End If
                        If rd3!bonus = "5" Then
                            If btn_5.Visible = True Then
                                btn_5.Visible = False
                            ElseIf btn_5.Visible = False Then
                                btn_5.Visible = True
                            End If
                        End If
                        If rd3!bonus = "6" Then
                            If btn_6.Visible = True Then
                                btn_6.Visible = False
                            ElseIf btn_6.Visible = False Then
                                btn_6.Visible = True
                            End If
                        End If
                        If rd3!bonus = "7" Then
                            If btn_7.Visible = True Then
                                btn_7.Visible = False
                            ElseIf btn_7.Visible = False Then
                                btn_7.Visible = True
                            End If
                        End If
                        If rd3!bonus = "8" Then
                            If btn_8.Visible = True Then
                                btn_8.Visible = False
                            ElseIf btn_8.Visible = False Then
                                btn_8.Visible = True
                            End If
                        End If
                        If rd3!bonus = "9" Then
                            If btn_9.Visible = True Then
                                btn_9.Visible = False
                            ElseIf btn_9.Visible = False Then
                                btn_9.Visible = True
                            End If
                        End If
                        If rd3!bonus = "10" Then
                            If btn_10.Visible = True Then
                                btn_10.Visible = False
                            ElseIf btn_10.Visible = False Then
                                btn_10.Visible = True
                            End If
                        End If
                        If rd3!bonus = "11" Then
                            If btn_11.Visible = True Then
                                btn_11.Visible = False
                            ElseIf btn_11.Visible = False Then
                                btn_11.Visible = True
                            End If
                        End If
                        If rd3!bonus = "12" Then
                            If btn_12.Visible = True Then
                                btn_12.Visible = False
                            ElseIf btn_12.Visible = False Then
                                btn_12.Visible = True
                            End If
                        End If
                        If rd3!bonus = "13" Then
                            If btn_13.Visible = True Then
                                btn_13.Visible = False
                            ElseIf btn_13.Visible = False Then
                                btn_13.Visible = True
                            End If
                        End If
                        If rd3!bonus = "14" Then
                            If btn_14.Visible = True Then
                                btn_14.Visible = False
                            ElseIf btn_14.Visible = False Then
                                btn_14.Visible = True
                            End If
                        End If
                        If rd3!bonus = "15" Then
                            If btn_15.Visible = True Then
                                btn_15.Visible = False
                            ElseIf btn_15.Visible = False Then
                                btn_15.Visible = True
                            End If
                        End If
                        If rd3!bonus = "16" Then
                            If btn_16.Visible = True Then
                                btn_16.Visible = False
                            ElseIf btn_16.Visible = False Then
                                btn_16.Visible = True
                            End If
                        End If
                        If rd3!bonus = "17" Then
                            If btn_17.Visible = True Then
                                btn_17.Visible = False
                            ElseIf btn_17.Visible = False Then
                                btn_17.Visible = True
                            End If
                        End If
                        If rd3!bonus = "18" Then
                            If btn_18.Visible = True Then
                                btn_18.Visible = False
                            ElseIf btn_18.Visible = False Then
                                btn_18.Visible = True
                            End If
                        End If
                        If rd3!bonus = "19" Then
                            If btn_19.Visible = True Then
                                btn_19.Visible = False
                            ElseIf btn_19.Visible = False Then
                                btn_19.Visible = True
                            End If
                        End If
                        If rd3!bonus = "20" Then
                            If btn_20.Visible = True Then
                                btn_20.Visible = False
                            ElseIf btn_20.Visible = False Then
                                btn_20.Visible = True
                            End If
                        End If
                        If rd3!bonus = "21" Then
                            If btn_21.Visible = True Then
                                btn_21.Visible = False
                            ElseIf btn_21.Visible = False Then
                                btn_21.Visible = True
                            End If
                        End If
                        If rd3!bonus = "22" Then
                            If btn_22.Visible = True Then
                                btn_22.Visible = False
                            ElseIf btn_22.Visible = False Then
                                btn_22.Visible = True
                            End If
                        End If
                        If rd3!bonus = "23" Then
                            If btn_23.Visible = True Then
                                btn_23.Visible = False
                            ElseIf btn_23.Visible = False Then
                                btn_23.Visible = True
                            End If
                        End If
                        If rd3!bonus = "24" Then
                            If btn_24.Visible = True Then
                                btn_24.Visible = False
                            ElseIf btn_24.Visible = False Then
                                btn_24.Visible = True
                            End If
                        End If
                        If rd3!bonus = "25" Then
                            If btn_25.Visible = True Then
                                btn_25.Visible = False
                            ElseIf btn_25.Visible = False Then
                                btn_25.Visible = True
                            End If
                        End If
                        If rd3!bonus = "26" Then
                            If btn_26.Visible = True Then
                                btn_26.Visible = False
                            ElseIf btn_26.Visible = False Then
                                btn_26.Visible = True
                            End If
                        End If
                        If rd3!bonus = "27" Then
                            If btn_27.Visible = True Then
                                btn_27.Visible = False
                            ElseIf btn_27.Visible = False Then
                                btn_27.Visible = True
                            End If
                        End If
                        If rd3!bonus = "28" Then
                            If btn_28.Visible = True Then
                                btn_28.Visible = False
                            ElseIf btn_28.Visible = False Then
                                btn_28.Visible = True
                            End If
                        End If
                        If rd3!bonus = "29" Then
                            If btn_29.Visible = True Then
                                btn_29.Visible = False
                            ElseIf btn_29.Visible = False Then
                                btn_29.Visible = True
                            End If
                        End If
                        If rd3!bonus = "30" Then
                            If btn_30.Visible = True Then
                                btn_30.Visible = False
                            ElseIf btn_30.Visible = False Then
                                btn_30.Visible = True
                            End If
                        End If
                        If rd3!bonus = "31" Then
                            If btn_31.Visible = True Then
                                btn_31.Visible = False
                            ElseIf btn_31.Visible = False Then
                                btn_31.Visible = True
                            End If
                        End If
                        If rd3!bonus = "32" Then
                            If btn_32.Visible = True Then
                                btn_32.Visible = False
                            ElseIf btn_32.Visible = False Then
                                btn_32.Visible = True
                            End If
                        End If
                        If rd3!bonus = "33" Then
                            If btn_33.Visible = True Then
                                btn_33.Visible = False
                            ElseIf btn_33.Visible = False Then
                                btn_33.Visible = True
                            End If
                        End If
                        If rd3!bonus = "34" Then
                            If btn_34.Visible = True Then
                                btn_34.Visible = False
                            ElseIf btn_34.Visible = False Then
                                btn_34.Visible = True
                            End If
                        End If
                        If rd3!bonus = "35" Then
                            If btn_35.Visible = True Then
                                btn_35.Visible = False
                            ElseIf btn_35.Visible = False Then
                                btn_35.Visible = True
                            End If
                        End If
                        If rd3!bonus = "36" Then
                            If btn_36.Visible = True Then
                                btn_36.Visible = False
                            ElseIf btn_36.Visible = False Then
                                btn_36.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Anjing" Then
                            If btn_anjing.Visible = True Then
                                btn_anjing.Visible = False
                            ElseIf btn_anjing.Visible = False Then
                                btn_anjing.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Ayam" Then
                            If btn_ayam.Visible = True Then
                                btn_ayam.Visible = False
                            ElseIf btn_ayam.Visible = False Then
                                btn_ayam.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Monyet" Then
                            If btn_monyet.Visible = True Then
                                btn_monyet.Visible = False
                            ElseIf btn_monyet.Visible = False Then
                                btn_monyet.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Kambing" Then
                            If btn_kambing.Visible = True Then
                                btn_kambing.Visible = False
                            ElseIf btn_kambing.Visible = False Then
                                btn_kambing.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Kuda" Then
                            If btn_kuda.Visible = True Then
                                btn_kuda.Visible = False
                            ElseIf btn_kuda.Visible = False Then
                                btn_kuda.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Ular" Then
                            If btn_ular.Visible = True Then
                                btn_ular.Visible = False
                            ElseIf btn_ular.Visible = False Then
                                btn_ular.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Naga" Then
                            If btn_naga.Visible = True Then
                                btn_naga.Visible = False
                            ElseIf btn_naga.Visible = False Then
                                btn_naga.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Kelinci" Then
                            If btn_kelinci.Visible = True Then
                                btn_kelinci.Visible = False
                            ElseIf btn_kelinci.Visible = False Then
                                btn_kelinci.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Macan" Then
                            If btn_macan.Visible = True Then
                                btn_macan.Visible = False
                            ElseIf btn_macan.Visible = False Then
                                btn_macan.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Kerbau" Then
                            If btn_kerbau.Visible = True Then
                                btn_kerbau.Visible = False
                            ElseIf btn_kerbau.Visible = False Then
                                btn_kerbau.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Tikus" Then
                            If btn_tikus.Visible = True Then
                                btn_tikus.Visible = False
                            ElseIf btn_tikus.Visible = False Then
                                btn_tikus.Visible = True
                            End If
                        End If
                        If rd3!bonus = "Babi" Then
                            If btn_babi.Visible = True Then
                                btn_babi.Visible = False
                            ElseIf btn_babi.Visible = False Then
                                btn_babi.Visible = True
                            End If
                        End If
                        If temp_timer > 5 Then
                            angka.Visible = False
                            shio.Visible = False
                            resetBonus()
                        End If
                        temp_timer += 1
                    End If
                End While
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btn_history_Click(sender As Object, e As EventArgs) Handles btn_history.Click
        If history.Visible = False Then
            history.Visible = True
            Panel2.Visible = False
        Else
            history.Visible = False
            Panel2.Visible = True
        End If
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\button.wav")
    End Sub

    Private Sub MenuCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 112 Then
            btn_babi.PerformClick()
        ElseIf e.KeyCode = 113 Then
            btn_anjing.PerformClick()
        ElseIf e.KeyCode = 114 Then
            btn_ayam.PerformClick()
        ElseIf e.KeyCode = 115 Then
            btn_monyet.PerformClick()
        ElseIf e.KeyCode = 116 Then
            btn_kambing.PerformClick()
        ElseIf e.KeyCode = 117 Then
            btn_kuda.PerformClick()
        ElseIf e.KeyCode = 118 Then
            btn_ular.PerformClick()
        ElseIf e.KeyCode = 119 Then
            btn_naga.PerformClick()
        ElseIf e.KeyCode = 120 Then
            btn_kelinci.PerformClick()
        ElseIf e.KeyCode = 121 Then
            btn_macan.PerformClick()
        ElseIf e.KeyCode = 122 Then
            btn_kerbau.PerformClick()
        ElseIf e.KeyCode = 123 Then
            btn_tikus.PerformClick()
        ElseIf e.KeyCode = 20 Then
            btn_25.PerformClick()
        ElseIf e.KeyCode = 100 Then
            'btn_grup_3.PerformClick()
        ElseIf e.KeyCode = 104 Then
            'btn_grup_2.PerformClick()
        ElseIf e.KeyCode = 83 Then
            btn_27.PerformClick()
        ElseIf e.KeyCode = 69 Then
            btn_15.PerformClick()
        ElseIf e.KeyCode = 68 Then
            btn_28.PerformClick()
        ElseIf e.KeyCode = 70 Then
            btn_29.PerformClick()
        ElseIf e.KeyCode = 71 Then
            btn_30.PerformClick()
        ElseIf e.KeyCode = 73 Then
            btn_20.PerformClick()
        ElseIf e.KeyCode = 72 Then
            btn_31.PerformClick()
        ElseIf e.KeyCode = 74 Then
            btn_32.PerformClick()
        ElseIf e.KeyCode = 75 Then
            btn_33.PerformClick()
        ElseIf e.KeyCode = 101 Then
            'btn_grup_4.PerformClick()
        ElseIf e.KeyCode = 79 Then
            btn_21.PerformClick()
        ElseIf e.KeyCode = 80 Then
            btn_22.PerformClick()
        ElseIf e.KeyCode = 81 Then
            btn_13.PerformClick()
        ElseIf e.KeyCode = 82 Then
            btn_16.PerformClick()
        ElseIf e.KeyCode = 65 Then
            btn_26.PerformClick()
        ElseIf e.KeyCode = 84 Then
            btn_17.PerformClick()
        ElseIf e.KeyCode = 85 Then
            btn_19.PerformClick()
        ElseIf e.KeyCode = 87 Then
            btn_14.PerformClick()
        ElseIf e.KeyCode = 103 Then
            'btn_grup_1.PerformClick()
        ElseIf e.KeyCode = 89 Then
            btn_18.PerformClick()
        ElseIf e.KeyCode = 222 Then
            btn_36.PerformClick()
        ElseIf e.KeyCode = 90 Then
            btn_1_12.PerformClick()
        ElseIf e.KeyCode = 88 Then
            btn_13_24.PerformClick()
        ElseIf e.KeyCode = 67 Then
            btn_25_36.PerformClick()
        ElseIf e.KeyCode = 66 Then
            btn_besar.PerformClick()
        ElseIf e.KeyCode = 86 Then
            btn_kecil.PerformClick()
        ElseIf e.KeyCode = 188 Then
            btn_genap.PerformClick()
        ElseIf e.KeyCode = 190 Then
            btn_ganjil.PerformClick()
        ElseIf e.KeyCode = 77 Then
            btn_black.PerformClick()
        ElseIf e.KeyCode = 109 Then
            btn_cancel.PerformClick()
        ElseIf e.KeyCode = 107 Then
            btn_repeat.PerformClick()
        ElseIf e.KeyCode = 46 Then
            btn_bet_10.PerformClick()
        ElseIf e.KeyCode = 35 Then
            btn_bet_100.PerformClick()
        ElseIf e.KeyCode = 34 Then
            btn_bet_500.PerformClick()
        ElseIf e.KeyCode = 38 Then
            btn_bet_1000.PerformClick()
        ElseIf e.KeyCode = 96 Then
            If history.Visible = False Then
                btn_history.PerformClick()
            Else
                Button1.PerformClick()
            End If
        ElseIf e.KeyCode = 106 Then
            btn_logout.PerformClick()
        ElseIf e.KeyCode = 49 Then
            btn_1.PerformClick()
        ElseIf e.KeyCode = 50 Then
            btn_2.PerformClick()
        ElseIf e.KeyCode = 51 Then
            btn_3.PerformClick()
        ElseIf e.KeyCode = 52 Then
            btn_4.PerformClick()
        ElseIf e.KeyCode = 53 Then
            btn_5.PerformClick()
        ElseIf e.KeyCode = 54 Then
            btn_6.PerformClick()
        ElseIf e.KeyCode = 55 Then
            btn_7.PerformClick()
        ElseIf e.KeyCode = 56 Then
            btn_8.PerformClick()
        ElseIf e.KeyCode = 57 Then
            btn_9.PerformClick()
        ElseIf e.KeyCode = 48 Then
            btn_10.PerformClick()
        ElseIf e.KeyCode = 189 Then
            btn_11.PerformClick()
        ElseIf e.KeyCode = 187 Then
            btn_12.PerformClick()
        ElseIf e.KeyCode = 219 Then
            btn_23.PerformClick()
        ElseIf e.KeyCode = 221 Then
            btn_24.PerformClick()
        ElseIf e.KeyCode = 76 Then
            btn_34.PerformClick()
        ElseIf e.KeyCode = 186 Then
            btn_35.PerformClick()
        ElseIf e.KeyCode = 78 Then
            btn_red.PerformClick()
        ElseIf e.KeyCode = 27 Then
            btn_bonus.PerformClick()
        ElseIf e.KeyCode = 192 Then
            Label6_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        history.Visible = False
        Panel2.Visible = True
        My.Computer.Audio.Play("C:\Users\Public\Music\Sound Effects\button.wav")
    End Sub

    Private Sub testClick()
        Label6_Click(Nothing, Nothing)
    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick
        Call Koneksi()
        Try
            cmd5 = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & txt_user.Text & "'", conn)
            Using rd5 As OdbcDataReader = cmd5.ExecuteReader
                While rd5.Read()
                    If Timer5.Enabled = True Or Timer6.Enabled = True Or Timer4.Enabled = True Then
                        txt_credit.Text = rd5!koin
                    Else
                        txt_credit.Text = rd5!koin - Val(txt_bet.Text) + Val(txt_win.Text)
                    End If
                End While
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer8_Tick(sender As Object, e As EventArgs) Handles Timer8.Tick
        Call Koneksi()
        Try
            cmd6 = New OdbcCommand("SELECT * FROM tb_permainan WHERE status = 5", conn)
            Using rd6 As OdbcDataReader = cmd6.ExecuteReader
                If rd6.HasRows = True Then
                    While rd6.Read()
                        Timer1.Enabled = False
                        Timer9.Enabled = True
                        btn_bet_10.Enabled = False
                        btn_bet_100.Enabled = False
                        btn_bet_500.Enabled = False
                        btn_bet_1000.Enabled = False
                        btn_cancel.Visible = False
                        btn_repeat.Visible = False
                        noBet()
                        Timer8.Enabled = False
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer9_Tick(sender As Object, e As EventArgs) Handles Timer9.Tick
        Call Koneksi()
        Try
            cmd6 = New OdbcCommand("SELECT * FROM tb_permainan WHERE status = 1", conn)
            Using rd6 As OdbcDataReader = cmd6.ExecuteReader
                If rd6.HasRows = True Then
                    While rd6.Read()
                        Timer1.Enabled = True
                        Timer8.Enabled = True
                        Timer9.Enabled = False
                    End While
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick
        Panel64.Visible = True
        If temp_notif > 2 Then
            Panel64.Visible = False
            Timer10.Enabled = False
            temp_notif = 0
        End If
        temp_notif += 1
    End Sub

    Private Sub resetBonus()
        Bonus1.Visible = False
        Bonus2.Visible = False
        Bonus3.Visible = False
        Bonus4.Visible = False
        Bonus5.Visible = False
        Bonus6.Visible = False
        Bonus7.Visible = False
        Bonus8.Visible = False
        Bonus9.Visible = False
        Bonus10.Visible = False
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Panel65.Visible = True
        Panel68.Visible = False
        txt_username.Text = ""
        txt_password.Text = ""
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Panel65.Visible = True
        Panel68.Visible = False
        txt_username.Text = ""
        txt_password.Text = ""
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Panel65.Visible = False
    End Sub

    Public Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Call Koneksi()
        Try
            cmd = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & txt_username.Text & "' AND password = '" & txt_password.Text & "' AND hak_akses = 'Staff Koin'", conn)
            Using rd As OdbcDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    Panel68.Visible = False
                    Panel65.Visible = False
                    username_player.Text = txt_user.Text
                    username_kasir.Text = rd!username
                    koin.Text = rd!koin
                    txt_isi_koin.Text = 0
                    txt_username.Text = ""
                    txt_password.Text = ""
                    Panel66.Visible = True
                    Timer11.Enabled = true
                Else
                    txt_username.Text = ""
                    txt_password.Text = ""
                    txt_username.Select()
                    Panel68.Visible = True
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel66.Visible = False
        Timer11.Enabled = False
    End Sub

    Private Sub BunifuThinButton22_Click(sender As Object, e As EventArgs) Handles BunifuThinButton22.Click
        If Val(koin.Text) < Val(txt_isi_koin.Text) Then
            MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
        Else
            Call Koneksi()
            cmd = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & username_player.Text & "'", conn)
            Using rd As OdbcDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    Dim total_koin = rd!koin + Val(txt_isi_koin.Text)
                    Dim sisa_koin_kasir = Val(koin.Text) - Val(txt_isi_koin.Text)
                    Ubah("UPDATE tb_user SET koin = '" & total_koin _
                         & "', koin_update = NOW() where username = '" & username_player.Text & "'")
                    Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                         & "' where username = '" & username_kasir.Text & "'")
                    Creates("INSERT INTO tb_koin (in_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_isi_koin.Text & "', NOW(), NOW(), '" & username_kasir.Text & "', '" & username_player.Text & "')")
                    koin.Text = sisa_koin_kasir
                    txt_isi_koin.Text = 0
                    Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Tambah Koin User','" _
                       & username_kasir.Text & "',NOW())")
                Else
                    MsgBox("Error!", MsgBoxStyle.Information)
                End If
            End Using
            conn.Close()
        End If
    End Sub

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles BunifuThinButton21.Click
        Call Koneksi()
        cmd = New Odbc.OdbcCommand("SELECT tb_permainan.status AS status, tb_permainan.keterangan AS keterangan FROM tb_bet, tb_permainan WHERE username='" & username_player.Text & "' AND tb_bet.id_permainan=tb_permainan.id_permainan AND tb_bet.id_permainan IN (select max(id_permainan) FROM tb_permainan)", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows = True Then
            If rd!status = 1 Or rd!status = 5 Or rd!status = 4 Then
                MsgBox("User '" & username_player.Text & "' sedang bermain!", MsgBoxStyle.Information)
            ElseIf rd!status = 2 Then
                If rd!keterangan = "Bonus" Then
                    MsgBox("User '" & username_player.Text & "' sedang bermain!", MsgBoxStyle.Information)
                Else
                    cmd2 = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & username_player.Text & "'", conn)
                    Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                        If rd2.HasRows = True Then
                            If rd2!koin < Val(txt_isi_koin.Text) Then
                                MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                            Else
                                Dim total_koin = rd2!koin - Val(txt_isi_koin.Text)
                                Dim sisa_koin_kasir = Val(koin.Text) + Val(txt_isi_koin.Text)
                                Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                     & "', koin_update = NOW() where username = '" & username_player.Text & "'")
                                Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                     & "' where username = '" & username_kasir.Text & "'")
                                Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_isi_koin.Text & "', NOW(), NOW(), '" & username_kasir.Text & "', '" & username_player.Text & "')")
                                koin.Text = sisa_koin_kasir
                                txt_isi_koin.Text = 0
                                Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                                   & txt_user.Text & "',NOW())")
                            End If
                        Else
                            MsgBox("Error!", MsgBoxStyle.Information)
                        End If
                    End Using
                End If
            Else
                cmd2 = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & username_player.Text & "'", conn)
                Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                    If rd2.HasRows = True Then
                        If rd2!koin < Val(txt_isi_koin.Text) Then
                            MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                        Else
                            Dim total_koin = rd2!koin - Val(txt_isi_koin.Text)
                            Dim sisa_koin_kasir = Val(koin.Text) + Val(txt_isi_koin.Text)
                            Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                     & "', koin_update = NOW() where username = '" & username_player.Text & "'")
                            Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                     & "' where username = '" & username_kasir.Text & "'")
                            Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_isi_koin.Text & "', NOW(), NOW(), '" & username_kasir.Text & "', '" & username_player.Text & "')")
                            koin.Text = sisa_koin_kasir
                            txt_isi_koin.Text = 0
                            Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                                   & txt_user.Text & "',NOW())")
                        End If
                    Else
                        MsgBox("Error!", MsgBoxStyle.Information)
                    End If
                End Using
            End If
        Else
            cmd2 = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & username_player.Text & "'", conn)
            Using rd2 As OdbcDataReader = cmd2.ExecuteReader
                If rd2.HasRows = True Then
                    If rd2!koin < Val(txt_isi_koin.Text) Then
                        MsgBox("Koin tidak Cukup!", MsgBoxStyle.Information)
                    Else
                        Dim total_koin = rd2!koin - Val(txt_isi_koin.Text)
                        Dim sisa_koin_kasir = Val(koin.Text) + Val(txt_isi_koin.Text)
                        Ubah("UPDATE tb_user SET koin = '" & total_koin _
                                     & "', koin_update = NOW() where username = '" & username_player.Text & "'")
                        Ubahs("UPDATE tb_user SET koin = '" & sisa_koin_kasir _
                                     & "' where username = '" & username_kasir.Text & "'")
                        Creates("INSERT INTO tb_koin (out_koin, date, time, username_giver, username_receiver) VALUES ('" & txt_isi_koin.Text & "', NOW(), NOW(), '" & username_kasir.Text & "', '" & username_player.Text & "')")
                        koin.Text = sisa_koin_kasir
                        txt_isi_koin.Text = 0
                        Creates("INSERT INTO tb_log_activity (activity, username, date) VALUES ('Kurang Koin User','" _
                                   & txt_user.Text & "',NOW())")
                    End If
                Else
                    MsgBox("Error!", MsgBoxStyle.Information)
                End If
            End Using
        End If
        conn.Close()
    End Sub

    Private Sub BunifuThinButton23_Click(sender As Object, e As EventArgs) Handles BunifuThinButton23.Click
        txt_isi_koin.Text += 100
    End Sub

    Private Sub BunifuThinButton24_Click(sender As Object, e As EventArgs) Handles BunifuThinButton24.Click
        txt_isi_koin.Text += 500
    End Sub

    Private Sub BunifuThinButton25_Click(sender As Object, e As EventArgs) Handles BunifuThinButton25.Click
        txt_isi_koin.Text += 1000
    End Sub

    Private Sub BunifuThinButton26_Click(sender As Object, e As EventArgs) Handles BunifuThinButton26.Click
        txt_isi_koin.Text += 10000
    End Sub

    Private Sub btn_qr_Click(sender As Object, e As EventArgs) Handles btn_qr.Click
        ScanQR.Show()
    End Sub

    Private Sub Timer11_Tick(sender As Object, e As EventArgs) Handles Timer11.Tick
        Call Koneksi()
        Try
            cmd7 = New OdbcCommand("SELECT * FROM tb_user WHERE username = '" & username_kasir.Text & "'", conn)
            Using rd7 As OdbcDataReader = cmd7.ExecuteReader
                While rd7.Read()
                    koin.Text = rd7!koin
                End While
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Panel24_Paint(sender As Object, e As PaintEventArgs) Handles Panel24.Paint

    End Sub
End Class