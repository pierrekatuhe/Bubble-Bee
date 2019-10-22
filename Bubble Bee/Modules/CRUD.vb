Imports System.Data.Odbc

Module CRUD
    'Setting Koneksi
    Public conn As OdbcConnection
    Public cmd As OdbcCommand
    Public ds As New DataSet
    Public da As OdbcDataAdapter
    Public rd As OdbcDataReader
    Public dt As New DataTable
    Public LokasiData As String
    Public result As String
    Sub Koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver};Database=db_bubble;server=192.168.88.8;uid=root"
        'LokasiData = "Driver={MySQL ODBC 3.51 Driver};Database=db_bubble;server=127.0.0.1;uid=root"
        conn = New OdbcConnection(LokasiData)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
    'Method Insert Data ke Database
    Public Sub Create(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
            'Mengecek Eksekusi Query
            If result = 0 Then
                MsgBox("Gagal menyimpan data.", MsgBoxStyle.Information)
            Else
                MsgBox("Data berhasil disimpan.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Creates(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
            'Mengecek Eksekusi Query
            If result = 0 Then
                MsgBox("Gagal menyimpan data.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Method Membaca Data dari Database
    Public Sub Read(ByVal sql As String, ByVal DTG As Object)
        Call Koneksi()
        Try
            dt = New DataTable
            da = New OdbcDataAdapter(sql, conn)
            da.Fill(dt)
            DTG.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        da.Dispose()
    End Sub
    'Method Update Data di Database
    Public Sub Ubah(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
            If result = 0 Then
                MsgBox("Gagal mengubah data.", MsgBoxStyle.Information)
            Else
                MsgBox("Data berhasil diubah.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub Ubahs(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Method Delete Data dari Database
    Public Sub Delete(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
            If result = 0 Then
                MsgBox("Gagal Menghapus Data.", MsgBoxStyle.Critical)
            Else
                MsgBox("Data berhasil dihapus.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Method Delete Data dari Database
    Public Sub Deletes(ByVal sql As String)
        Call Koneksi()
        Try
            cmd = New OdbcCommand(sql, conn)
            result = cmd.ExecuteNonQuery
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module