Imports System.ComponentModel

Public Class PrintQR
    Private Sub PrintQR_Load(sender As Object, e As EventArgs) Handles Me.Load
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim img As Bitmap = New Bitmap(qr_pic.Image)
            img.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
            Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class