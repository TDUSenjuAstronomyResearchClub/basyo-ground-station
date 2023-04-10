Imports System.IO
Imports System.IO.Ports
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Text

Public Class Form1
    Dim WithEvents MyPort As New SerialPort("COM8", 9600)

    'フォームを読み込むときにポートを解放し、ハンドラを追加する
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim jsonSerializer As New DataContractJsonSerializer(GetType(SensorData))
        'データを受け取ったらデシリアライズするハンドラを追加
        AddHandler MyPort.DataReceived,
            Sub()
                Dim buffer As String = MyPort.ReadExisting()
                Dim data As Byte() = Encoding.UTF8.GetBytes(buffer)
                Dim stream As New MemoryStream(data)
                Dim sensorData As SensorData = DirectCast(jsonSerializer.ReadObject(stream), SensorData)

                ' sensorDataにデータが格納されているので値を取り出して使う

            End Sub

        MyPort.Open()
    End Sub

    'フォームが閉じるときにポートを閉める
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MyPort.Close()
    End Sub



    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint



    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click

    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class

'JSONマッピング用データクラス群
<DataContract>
Public Class GPSData
    <DataMember(Name:="緯度")>
    Public Property Latitude As Integer
    <DataMember(Name:="経度")>
    Public Property Longitude As Integer
    <DataMember(Name:="海抜")>
    Public Property Altitude As Integer
    <DataMember(Name:="サンプル")>
    Public Property Sample As SampleData
    <DataMember(Name:="ゴール")>
    Public Property Goal As GoalData
End Class

<DataContract>
Public Class SampleData
    <DataMember(Name:="直線距離")>
    Public Property Distance As Integer
    <DataMember(Name:="方位角")>
    Public Property Azimuth As Integer
End Class

<DataContract>
Public Class GoalData
    <DataMember(Name:="直線距離")>
    Public Property Distance As Integer
    <DataMember(Name:="方位角")>
    Public Property Azimuth As Integer
End Class

<DataContract>
Public Class NineAxisData
    <DataMember(Name:="加速度")>
    Public Property Acceleration As Integer
    <DataMember(Name:="角速度")>
    Public Property AngularVelocity As Integer
    <DataMember(Name:="方位角")>
    Public Property Azimuth As Integer
End Class

<DataContract>
Public Class TemperatureHumidityPressureData
    <DataMember(Name:="温度")>
    Public Property Temperature As Integer
    <DataMember(Name:="湿度")>
    Public Property Humidity As Integer
    <DataMember(Name:="気圧")>
    Public Property Pressure As Integer
End Class

<DataContract>
Public Class PressureData
    <DataMember(Name:="温度")>
    Public Property Temperature As Integer
    <DataMember(Name:="気圧")>
    Public Property Pressure As Integer
    <DataMember(Name:="高度")>
    Public Property Altitude As Integer
End Class

<DataContract>
Public Class SensorData
    <DataMember(Name:="GPS")>
    Public Property GPS As GPSData
    <DataMember(Name:="9軸")>
    Public Property NineAxis As NineAxisData
    <DataMember(Name:="温湿度気圧")>
    Public Property TemperatureHumidityPressure As TemperatureHumidityPressureData
    <DataMember(Name:="気圧")>
    Public Property Pressure As PressureData
    <DataMember(Name:="電池")>
    Public Property Battery As Integer
    <DataMember(Name:="距離")>
    Public Property Distance As Integer
End Class
