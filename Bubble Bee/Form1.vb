Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing.Common
Imports ZXing
Imports ZXing.QrCode
Imports System.Data.Odbc
Imports System.ComponentModel

Public Class Form1
    Dim VideoCaptureSource As VideoCaptureDevice
    Dim VideoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
    Dim image As Bitmap

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MySingleDevice As FilterInfo
        Panel6.Visible = False

        If VideoDevices.Count = 0 Then
            ComboBox1.Items.Add("No Video Devices")
        Else
            For Each MySingleDevice In VideoDevices
                ComboBox1.Items.Add(MySingleDevice.Name)
            Next
        End If

        VideoCaptureSource = New VideoCaptureDevice(VideoDevices(0).MonikerString)
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        VideoCaptureSource.SignalToStop()
        VideoCaptureSource.WaitForStop()
        VideoDevices = Nothing
        VideoCaptureSource = Nothing
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem <> "No Video Devices" Then
            If VideoCaptureSource.IsRunning Then
                VideoCaptureSource.SignalToStop()
                VideoCaptureSource.WaitForStop()
            End If

            VideoCaptureSource = New VideoCaptureDevice(VideoDevices(ComboBox1.SelectedIndex).MonikerString)
            AddHandler VideoCaptureSource.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            VideoCaptureSource.Start()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Reader As New BarcodeReader()
        Dim result As Result = Reader.Decode(PictureBox1.Image)
        Try
            Dim decoded As String = result.ToString().Trim()
            Dim user_key() As String = Split(decoded, ",")
            If user_key.Length = 2 Then
                Login.txt_username.Text = user_key(0)
                Login.txt_password.Text = user_key(1)
                Hide()
                Login.Button1_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            Panel6.Visible = True
        End Try
    End Sub

    Private Sub Captured(sender As Object, eventArgs As NewFrameEventArgs)
        image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        PictureBox1.Image = image
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim Reader As New BarcodeReader()
            Dim result As Result = Reader.Decode(PictureBox1.Image)
            Dim decoded As String = result.ToString().Trim()
            Dim user_key() As String = Split(decoded, ",")
            If user_key.Length = 2 Then
                Login.txt_username.Text = user_key(0)
                Login.txt_password.Text = user_key(1)
                Hide()
                Login.Button1_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            Panel6.Visible = True
        End Try
    End Sub
End Class