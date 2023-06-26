Imports System.IO
Imports System.IO.Ports
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim WithEvents MyPort As New SerialPort("COM7", 9600)
    Dim isSerialConnected As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click   'ボタンを押すと通信開始
        If Not isSerialConnected Then ' シリアルポートが未接続の場合
            Try
                MyPort.Open() ' シリアルポートを開く
                isSerialConnected = True ' シリアルポート接続フラグをTrueにする
                Button1.Text = "通信停止" ' ボタンのテキストを変更する
            Catch ex As Exception
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MyPort.Close() ' シリアルポートを閉じる
            isSerialConnected = False ' シリアルポート接続フラグをFalseにする
            Button1.Text = "通信開始"
        End If
    End Sub

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
                Dim encodedData As String = MyPort.ReadLine()
                ' データをUTF-8からデコード
                Dim decodedData As String = Encoding.UTF8.GetString(Convert.FromBase64String(encodedData))
                ' JSONデシリアライズ
                Dim sensorData As SensorData = JsonConvert.DeserializeObject(Of SensorData)(decodedData)
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
                Dim Time As Integer = sensorData.Time
                TextBox3.Invoke(Sub() TextBox3.Text = Time.ToString())
                Dim Latitude As Integer = sensorData.GPS.Latitude
                TextBox3.Invoke(Sub() TextBox3.Text = Latitude.ToString())
                Dim Longitude As Integer = sensorData.GPS.Longitude
                TextBox3.Invoke(Sub() TextBox3.Text = Longitude.ToString())
                Dim Altitude As Integer = sensorData.GPS.Altitude
                TextBox3.Invoke(Sub() TextBox3.Text = Altitude.ToString())
                Dim sDis As Integer = sensorData.GPS.Sample.Distance
                TextBox3.Invoke(Sub() TextBox3.Text = sDis.ToString())
                Dim sAzi As Integer = sensorData.GPS.Sample.Azimuth
                TextBox3.Invoke(Sub() TextBox3.Text = sAzi.ToString())
                Dim gDis As Integer = sensorData.GPS.Goal.Distance
                TextBox3.Invoke(Sub() TextBox3.Text = gDis.ToString())
                Dim gAzi As Integer = sensorData.GPS.Goal.Azimuth
                TextBox3.Invoke(Sub() TextBox3.Text = gAzi.ToString())

            End Sub
    End Sub

    Private Function DeserializeJson(stream As Stream) As SensorData
        Dim jsonSettings As New DataContractJsonSerializerSettings()
        jsonSettings.UseSimpleDictionaryFormat = True ' 日本語のプロパティ名をサポート

        Dim serializer As New DataContractJsonSerializer(GetType(SensorData), jsonSettings)
        Return CType(serializer.ReadObject(stream), SensorData)
    End Function

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
    Public Property Acceleration As AccelerationData
    <DataMember(Name:="角速度")>
    Public Property AngularVelocity As AnggularVelocityData
    <DataMember(Name:="方位角")>
    Public Property Azimuth As Integer
End Class

<DataContract>
Public Class AccelerationData
    <DataMember(Name:="X")>
    Public Property AccX As Integer
    <DataMember(Name:="Y")>
    Public Property AccY As Integer
    <DataMember(Name:="Z")>
    Public Property AccZ As Integer
End Class

<DataContract>
Public Class AnggularVelocityData
    <DataMember(Name:="X")>
    Public Property AngX As Integer
    <DataMember(Name:="Y")>
    Public Property AngY As Integer
    <DataMember(Name:="Z")>
    Public Property AngZ As Integer
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
    <DataMember(Name:="時間")>
    Public Property Time As Integer
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