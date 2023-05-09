Imports System.IO
Imports System.IO.Ports
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Text

Public Class Form1
    Dim WithEvents MyPort As New SerialPort("COM8", 9600)

    'フォームを読み込むときにポートを解放し、ハンドラを追加する
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Chart1の設定
        Dim aw As String
        aw = 300
        Chart1.Series("Series1").Points.AddY(aw)
        Chart1.Series("Series1").IsValueShownAsLabel = False
        Chart1.ChartAreas("ChartArea1").AxisX.Enabled = DataVisualization.Charting.AxisEnabled.False

        'Chart2の設定
        Chart2.Series("Series1").Points.AddY(aw)
        Chart2.Series("Series1").IsValueShownAsLabel = False
        Chart2.ChartAreas("ChartArea1").AxisX.Enabled = DataVisualization.Charting.AxisEnabled.False

        'Chart3の設定
        Chart3.Series("Series1").Points.AddY(aw)
        Chart3.Series("Series1").IsValueShownAsLabel = False
        Chart3.ChartAreas("ChartArea1").AxisX.Enabled = DataVisualization.Charting.AxisEnabled.False

        Dim jsonSerializer As New DataContractJsonSerializer(GetType(SensorData))
        'データを受け取ったらデシリアライズするハンドラを追加
        AddHandler MyPort.DataReceived,
            Sub()
                Dim buffer As String = MyPort.ReadExisting()
                Dim data As Byte() = Encoding.UTF8.GetBytes(buffer)
                Dim stream As New MemoryStream(data)
                Dim sensorData As SensorData = DirectCast(jsonSerializer.ReadObject(stream), SensorData)

                ' sensorDataにデータが格納されているので値を取り出して使う

                'Chart1_温度
                Chart1.Series("Series1").Points.AddY(sensorData.TemperatureHumidityPressure.Temperature)
                If Chart1.Series(0).Points.Count = 10 Then '表示する分
                    Chart1.Series(0).Points.RemoveAt(0)
                End If
                Chart1.Invalidate()

                'Chart2_湿度
                Chart2.Series("Series1").Points.AddY(sensorData.TemperatureHumidityPressure.Humidity)
                If Chart2.Series(0).Points.Count = 10 Then '表示する分
                    Chart2.Series(0).Points.RemoveAt(0)
                End If
                Chart2.Invalidate()

                'Chart3_気圧
                Chart3.Series("Series1").Points.AddY(sensorData.TemperatureHumidityPressure.Humidity)
                If Chart3.Series(0).Points.Count = 10 Then '表示する分
                    Chart3.Series(0).Points.RemoveAt(0)
                End If
                Chart3.Invalidate()


            End Sub

        MyPort.Open()
    End Sub

    'フォームが閉じるときにポートを閉める
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MyPort.Close()
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
