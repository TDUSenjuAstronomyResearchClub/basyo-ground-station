import json
import string
from tkinter import *
import tkinter as tk
from tkinter import ttk
from tkinter import messagebox
from threading import Thread
import serial
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import folium
from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as ec
from PIL import Image, ImageTk
import time as t
import io
import os
from datetime import datetime
import pandas as pd

"""COMportを確認し適時変更する
"""
port = "COM8"

comand = ("コマンド一覧\ndestination:サンプル採取地点または\nゴール地点の緯度、経度の変更\nfall:機体の落下開始判定\n"
          "landing:機体の着地判定\nmanual:手動制御\n***以降manualで使用***\npicture:写真撮影\nsoil:土壌水分測定\n"
          "forward:前進\nleft:左旋回\nright:右旋回\nreverse:後退\nend:終了")

ex_columns = ["data_type", "time", "gps.latitude", "gps.longitude", "gps.altitude",
              "gps.distance.sample", "gps.distance.goal", "gps.azimuth.sample", "gps.azimuth.goal",
              "nine_axis.acceleration.x", "nine_axis.acceleration.y", "nine_axis.acceleration.z",
              "nine_axis.angular_velocity.x", "nine_axis.angular_velocity.y", "nine_axis.angular_velocity.z",
              "nine_axis.azimuth",
              "bme280.temperature", "bme280.humidity", "bme280.pressure",
              "lps25hb.temperature", "lps25hb.pressure", "lps25hb.altitude",
              "battery", "distance", "camera", "soil_moisture", "message"]


class App(tk.Tk):
    def __init__(self):
        """GUI設定、保存関数
        """

        super().__init__()

        self.excel_file_name = None
        self.title("Serial Communication")

        # 通信ボタン
        self.button = tk.Button(self, text="Start Communication", command=self.toggle_communication)
        self.button.pack(anchor='ne', padx=10, pady=10)
        self.serial_port = None
        self.is_serial_connected = False

        # tab
        self.notebook = ttk.Notebook(self)
        tab1 = Frame(self)  # main用
        tab2 = Frame(self)  # graph用
        tab3 = Frame(self)  # soil用
        self.notebook.add(tab1, text="main")
        self.notebook.add(tab2, text="graph")
        self.notebook.add(tab3, text="soil")
        self.notebook.pack(expand=True, fill="both")

        # 右側コマンド系frame
        self.right = tk.Canvas(tab1, width=280, height=200, borderwidth=0, highlightthickness=0)
        self.command_frame = tk.Frame(self.right)
        self.right.pack(side=tk.RIGHT, fill=tk.BOTH)
        self.right.create_window((0, 0), window=self.command_frame, anchor=tk.NW)

        # 左側データ表示用frame
        self.leftcanvas = tk.Canvas(tab1, width=400, height=200, borderwidth=0, highlightthickness=0)
        self.scrollable_lframe = tk.Frame(self.leftcanvas)

        self.vsb = tk.Scrollbar(self.scrollable_lframe, orient=tk.VERTICAL, command=self.leftcanvas.yview)
        self.leftcanvas.configure(yscrollcommand=self.vsb.set)

        self.vsb.pack(side=tk.RIGHT, fill=tk.Y, )
        self.leftcanvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        self.leftcanvas.create_window((0, 0), window=self.scrollable_lframe, anchor=tk.NW)

        self.scrollable_lframe.bind("<Configure>", self.on_frame_configure)

        # データ表示用
        self.time_label = tk.Label(self.scrollable_lframe, text="Time:      ", font=("Arial", 11))
        self.time_label.pack(anchor='nw', pady=10)

        self.latitude_label = tk.Label(self.scrollable_lframe, text="Latitude:      ",
                                       font=("Arial", 11))
        self.latitude_label.pack(anchor='nw', pady=10)

        self.longitude_label = tk.Label(self.scrollable_lframe, text="Longitude:        ",
                                        font=("Arial", 11))
        self.longitude_label.pack(anchor='nw', pady=10)

        self.altitude_label = tk.Label(self.scrollable_lframe, text="Altitude:      ",
                                       font=("Arial", 11))
        self.altitude_label.pack(anchor='nw', pady=10)

        self.sample_distance_label = tk.Label(self.scrollable_lframe, text="Sample Distance:        ",
                                              font=("Arial", 11))
        self.sample_distance_label.pack(anchor='nw', pady=10)

        self.sample_azimuth_label = tk.Label(self.scrollable_lframe, text="Sample Azimuth:      ",
                                             font=("Arial", 11))
        self.sample_azimuth_label.pack(anchor='nw', pady=10)

        self.goal_distance_label = tk.Label(self.scrollable_lframe, text="Goal Distance:        ",
                                            font=("Arial", 11))
        self.goal_distance_label.pack(anchor='nw', pady=10)

        self.goal_azimuth_label = tk.Label(self.scrollable_lframe, text="Goal Azimuth:      ",
                                           font=("Arial", 11))
        self.goal_azimuth_label.pack(anchor='nw', pady=10)

        self.acceleration_x_label = tk.Label(self.scrollable_lframe, text="Acceleration X:      ",
                                             font=("Arial", 11))
        self.acceleration_x_label.pack(anchor='nw', pady=10)

        self.acceleration_y_label = tk.Label(self.scrollable_lframe, text="Acceleration Y:      ",
                                             font=("Arial", 11))
        self.acceleration_y_label.pack(anchor='nw', pady=10)

        self.acceleration_z_label = tk.Label(self.scrollable_lframe, text="Acceleration Z:      ",
                                             font=("Arial", 11))
        self.acceleration_z_label.pack(anchor='nw', pady=10)

        self.angular_velocity_x_label = tk.Label(self.scrollable_lframe, text="Angular Velocity X:      ",
                                                 font=("Arial", 11))
        self.angular_velocity_x_label.pack(anchor='nw', pady=10)

        self.angular_velocity_y_label = tk.Label(self.scrollable_lframe, text="Angular Velocity Y:      ",
                                                 font=("Arial", 11))
        self.angular_velocity_y_label.pack(anchor='nw', pady=10)

        self.angular_velocity_z_label = tk.Label(self.scrollable_lframe, text="Angular Velocity Z:      ",
                                                 font=("Arial", 11))
        self.angular_velocity_z_label.pack(anchor='nw', pady=10)

        self.nine_axis_azimuth_label = tk.Label(self.scrollable_lframe, text="Nine Axis Azimuth:        ",
                                                font=("Arial", 11))
        self.angular_velocity_z_label.pack(anchor='nw', pady=10)

        self.temperature_label = tk.Label(self.scrollable_lframe, text="Temperature:        ",
                                          font=("Arial", 11))
        self.temperature_label.pack(anchor='nw', pady=10)

        self.humidity_label = tk.Label(self.scrollable_lframe, text="Humidity:      ",
                                       font=("Arial", 11))
        self.humidity_label.pack(anchor='nw', pady=10)

        self.pressure_label = tk.Label(self.scrollable_lframe, text="Pressure:      ",
                                       font=("Arial", 11))
        self.pressure_label.pack(anchor='nw', pady=10)

        self.distance_label = tk.Label(self.scrollable_lframe, text="Distance:      ",
                                       font=("Arial", 11))
        self.distance_label.pack(anchor='nw', pady=10)

        self.soil_label = tk.Label(self.scrollable_lframe, text="soil:      ",
                                   font=("Arial", 11))
        self.soil_label.pack(anchor='nw', pady=10)

        # command送信用
        self.sendframe = tk.Frame(self.command_frame)
        self.sendframe.pack(anchor='ne')

        self.send_button = tk.Button(self.sendframe, text="Send Data", command=self.send_data)
        self.send_button.pack(side='right', pady=10, expand=True)
        self.entry = tk.Entry(self.sendframe)
        self.entry.pack(side='right', pady=10, expand=True)

        self.comand_label = tk.Label(self.command_frame, text=comand, font=("Arial", 12))
        self.comand_label.pack(pady=10)

        # メッセージ表示用
        self.data_text = tk.Entry(self.command_frame, width=40)
        self.data_text.pack(ipady=10, pady=10, expand=True)

        # 地図写真用frame
        self.center = tk.Frame(tab1)
        self.center.pack(side=tk.TOP, fill=tk.BOTH)

        # 写真用キャンバス
        # self.cvs = tk.Canvas(self.center, width=500, height=200)
        # self.cvs.pack(expand=True)

        # 地図表示用のフレーム
        self.map_frame = tk.Canvas(self.center, width=700, height=500)
        self.map_frame.pack(expand=True)

        # グラフ
        self.canvasgraph = tk.Canvas(tab2)
        self.scrollable_graph = tk.Frame(self.canvasgraph, width=1000, height=800)
        self.canvasgraph.pack(fill=tk.BOTH, expand=True)
        self.canvasgraph.create_window((0, 0), window=self.scrollable_graph, anchor=tk.NW)
        self.scrollable_graph.pack(fill=tk.BOTH, expand=True)

        fig, (self.acx, self.acy, self.acz, self.avx, self.avy) \
            = plt.subplots(5, 1, sharex='all')
        plt.xlabel('Time')
        self.acx.set_ylabel("Acc X")
        self.acy.set_ylabel("Acc Y")
        self.acz.set_ylabel("Acc Z")
        self.avx.set_ylabel("ang X")
        self.avy.set_ylabel("ang Y")
        fig.tight_layout()

        fig2, (self.tem, self.hum, self.pre, self.dis, self.avz) \
            = plt.subplots(5, 1, sharex='all')
        plt.xlabel('Time')
        self.avz.set_ylabel("ang Z")
        self.tem.set_ylabel("Tem")
        self.hum.set_ylabel("Hum")
        self.pre.set_ylabel("Pre")
        self.dis.set_ylabel("Dis")
        fig2.tight_layout()

        fig3, (self.moi) = plt.subplots(1, 1)
        plt.xlabel('Time')
        self.moi.set_ylabel("Moi")
        fig3.tight_layout()

        """
        self.acx=fig.add_subplot(6,2,1)
        self.acx.set_xlabel("Time")
        self.acx.set_ylabel("Acc X")
        self.acy=fig.add_subplot(6,2,2)
        self.acy.set_xlabel("Time")
        self.acy.set_ylabel("Acc Y")
        self.acz=fig.add_subplot(6,2,3)
        self.acz.set_xlabel("Time")
        self.acz.set_ylabel("Acc Z")
        self.avx=fig.add_subplot(6,2,4)
        self.avx.set_xlabel("Time")
        self.avx.set_ylabel("ang X")
        self.avy=fig.add_subplot(6,2,5)
        self.avy.set_xlabel("Time")
        self.avy.set_ylabel("ang Y")
        self.avz=fig.add_subplot(6,2,6)
        self.avz.set_xlabel("Time")
        self.avz.set_ylabel("ang Z")
        self.tem=fig.add_subplot(6,2,7)
        self.tem.set_xlabel("Time")
        self.tem.set_ylabel("Temperature")
        self.hum=fig.add_subplot(6,2,8)
        self.hum.set_xlabel("Time")
        self.hum.set_ylabel("Humidity")
        self.pre=fig.add_subplot(6,2,9)
        self.pre.set_xlabel("Time")
        self.pre.set_ylabel("Pressure")
        self.bat=fig.add_subplot(6,2,10)
        self.bat.set_xlabel("Time")
        self.bat.set_ylabel("Battery")
        self.dis=fig.add_subplot(6,2,11)
        self.dis.set_xlabel("Time")
        self.dis.set_ylabel("Distance")
        """

        self.fig_canvas = FigureCanvasTkAgg(fig, self.scrollable_graph)
        self.fig_canvas.get_tk_widget().pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        self.fig_canvas2 = FigureCanvasTkAgg(fig2, self.scrollable_graph)
        self.fig_canvas2.get_tk_widget().pack(side=tk.RIGHT, fill=tk.BOTH, expand=True)
        self.fig_canvas3 = FigureCanvasTkAgg(fig3, tab3)
        self.fig_canvas3.get_tk_widget().pack(fill=tk.BOTH, expand=True)

        self.protocol("WM_DELETE_WINDOW", self.close)

        # グラフ表示データ用
        self.time_data = []
        self.time_data_soil = []
        self.acceleration_x_data = []
        self.acceleration_y_data = []
        self.acceleration_z_data = []
        self.angularvelocity_x_data = []
        self.angularvelocity_y_data = []
        self.angularvelocity_z_data = []
        self.Temperature_data = []
        self.Humidity_data = []
        self.Pressure_data = []
        self.coordinates = []
        self.battery_data = []
        self.distance_data = []
        self.moisture_data = []
        # excelデータ保存用
        self.ex_row: int = 1

    def on_frame_configure(self, event):
        self.leftcanvas.configure(scrollregion=self.leftcanvas.bbox("all"))
        self.canvasgraph.configure(scrollregion=self.canvasgraph.bbox("all"))

    def toggle_communication(self):
        """通信処理関数
        """
        if not self.is_serial_connected:
            try:
                self.serial_port = serial.Serial(port, 9600)
                self.is_serial_connected = True
                self.button.configure(text="Stop Communication")
                self.read_serial_data()
            except serial.SerialException as e:
                messagebox.showerror("Error", str(e))
        else:
            self.is_serial_connected = False
            self.serial_port.close()
            self.button.configure(text="Start Communication")

    def read_serial_data(self):
        """シリアル通信動作関数
        """
        self.ex_row = 1

        def read_data():
            self.filename()
            while self.is_serial_connected:
                try:
                    data = self.serial_port.readline().decode('utf-8').strip()
                    json_data = json.loads(data)
                    self.save_to_excel(json_data)
                    self.update_data(json_data)

                except Exception as e:
                    print(str(e))

        Thread(target=read_data, daemon=True).start()

    def send_data(self):
        """ コマンド送信関数
        """
        if self.is_serial_connected:
            try:
                data = self.entry.get().strip()
                data = data + '\n'
                encoded_data = data.encode('utf-8')
                self.serial_port.write(encoded_data)
            except Exception as e:
                messagebox.showerror("Error", str(e))
        else:
            messagebox.showerror("Error", "Serial connection is not established.")

    def update_data(self, data):
        """ 定期通信用データ更新関数
        """
        img = PhotoImage(file='map.png')
        self.map_frame.create_image(0, 0, anchor='nw', image=img)
        data_type = data.get("data_type")
        if data_type == "only_sensor_data":
            self.sensor_data(data)
        elif data_type == "only_message_data":
            self.text_data(data)
        elif data_type == "only_picture_data":
            self.picture_data(data)
        elif data_type == "only_soil_data":
            self.soil_data(data)
        else:
            pass

    def sensor_data(self, data):
        """センサデータ表示処理
        """
        time = data.get("time")
        gps = data.get("gps")
        nine_axis = data.get("nine_axis")
        bme280 = data.get("bme280")
        distance = data.get("distance")
        soil = data.get("soil_moisture")

        # データ表示
        self.time_label.configure(text=f"Time: {time}")
        self.latitude_label.configure(text=f"Latitude: {gps['latitude']}")
        self.longitude_label.configure(text=f"Longitude: {gps['longitude']}")
        self.altitude_label.configure(text=f"Altitude: {gps['altitude']}")
        self.sample_distance_label.configure(text=f"Sample Distance: {gps['distance']['sample']}")
        self.sample_azimuth_label.configure(text=f"Sample Azimuth: {gps['azimuth']['sample']}")
        self.goal_distance_label.configure(text=f"Goal Distance: {gps['distance']['goal']}")
        self.goal_azimuth_label.configure(text=f"Goal Azimuth: {gps['azimuth']['goal']}")
        self.acceleration_x_label.configure(text=f"Acceleration X: {nine_axis['acceleration']['x']}")
        self.acceleration_y_label.configure(text=f"Acceleration Y: {nine_axis['acceleration']['y']}")
        self.acceleration_z_label.configure(text=f"Acceleration Z: {nine_axis['acceleration']['z']}")
        self.angular_velocity_x_label.configure(text=f"Angular Velocity X: {nine_axis['angular_velocity']['x']}")
        self.angular_velocity_y_label.configure(text=f"Angular Velocity Y: {nine_axis['angular_velocity']['y']}")
        self.angular_velocity_z_label.configure(text=f"Angular Velocity Z: {nine_axis['angular_velocity']['z']}")
        self.nine_axis_azimuth_label.configure(text=f"Nine Axis Azimuth : {nine_axis['azimuth']}")
        self.temperature_label.configure(text=f"Temperature: {bme280['temperature']}")
        self.humidity_label.configure(text=f"Humidity: {bme280['humidity']}")
        self.pressure_label.configure(text=f"Pressure: {bme280['pressure']}")
        self.distance_label.configure(text=f"Distance: {distance}")
        self.soil_label.configure(text=f"soil: {soil}")

        # データを取得、追加
        self.time_data.append(time)
        acceleration_x = nine_axis['acceleration']['x']
        self.acceleration_x_data.append(acceleration_x)
        acceleration_y = nine_axis['acceleration']['y']
        self.acceleration_y_data.append(acceleration_y)
        acceleration_z = nine_axis['acceleration']['z']
        self.acceleration_z_data.append(acceleration_z)
        angularvelocity_x = nine_axis['angular_velocity']['x']
        self.angularvelocity_x_data.append(angularvelocity_x)
        angularvelocity_y = nine_axis['angular_velocity']['y']
        self.angularvelocity_y_data.append(angularvelocity_y)
        angularvelocity_z = nine_axis['angular_velocity']['z']
        self.angularvelocity_z_data.append(angularvelocity_z)
        temperature = bme280['temperature']
        self.Temperature_data.append(temperature)
        humidity = bme280['humidity']
        self.Humidity_data.append(humidity)
        pressure = bme280['pressure']
        self.Pressure_data.append(pressure)
        latitude = gps['latitude']
        longitude = gps['longitude']
        self.coordinates.append((latitude, longitude))
        self.distance_data.append(distance)
        # self.moisture_data.append(soil)

        # グラフにデータをプロット
        self.acx.clear()
        self.acx.plot(self.time_data, self.acceleration_x_data)
        self.acy.clear()
        self.acy.plot(self.time_data, self.acceleration_y_data)
        self.acz.clear()
        self.acz.plot(self.time_data, self.acceleration_z_data)
        self.avx.clear()
        self.avx.plot(self.time_data, self.angularvelocity_x_data)
        self.avy.clear()
        self.avy.plot(self.time_data, self.angularvelocity_y_data)
        self.avz.clear()
        self.avz.plot(self.time_data, self.angularvelocity_z_data)
        self.tem.clear()
        self.tem.plot(self.time_data, self.Temperature_data)
        self.hum.clear()
        self.hum.plot(self.time_data, self.Humidity_data)
        self.pre.clear()
        self.pre.plot(self.time_data, self.Pressure_data)
        self.dis.clear()
        self.dis.plot(self.time_data, self.distance_data)
        # self.moi.clear()
        # self.moi.plot(self.time_data, self.moisture_data)
        self.fig_canvas.draw()
        self.fig_canvas2.draw()
        # self.fig_canvas3.draw()

        # 地図を作成
        m = folium.Map(location=[30.3747779, 130.95862], zoom_start=50)
        #   座標にピンを立てる
        for coord in self.coordinates:
            folium.Marker(coord).add_to(m)
        # 座標を線で結ぶ
        folium.PolyLine(self.coordinates, color='blue').add_to(m)
        # 地図をHTMLファイルに保存
        map_file = "map.html"
        m.save(map_file)

        # Seleniumを使用してHTMLをブラウザで開き、スクリーンショットを取得
        options = webdriver.ChromeOptions()
        options.add_argument('--headless')  # ヘッドレスモードでブラウザを起動
        browser = webdriver.Chrome(options=options)
        tmpurl = 'file://{path}/{mapfile}'.format(path=os.getcwd(), mapfile=map_file)
        browser.get(tmpurl)
        # マップが生成されるのを待つ
        wait = WebDriverWait(driver=browser, timeout=15)
        wait.until(ec.presence_of_all_elements_located)
        t.sleep(2)
        # マップ画像の生成
        browser.save_screenshot("map.png")
        # ブラウザを閉じる
        browser.close()
        browser.quit()

    def text_data(self, data):
        """メッセージ処理
        """
        # time: string = str(data.get("time"))
        message: string = str(data.get("message"))
        # self.data_text.insert(tk.END, "time: " + time + "_")
        self.data_text.insert(tk.END, "message: " + message + "\n")

    def filename(self):
        """Excelファイル名を生成
        """
        # 通信開始時刻をファイル名にする
        now = datetime.now()
        self.excel_file_name = "start_" + now.strftime("%Y-%m-%d_%H-%M") + ".xlsx"
        # ファイルがない場合生成
        if not os.path.isfile(self.excel_file_name):
            pd.DataFrame(columns=ex_columns).to_excel(self.excel_file_name, index=False)

    def save_to_excel(self, data):
        """データ保存処理
        """
        # データをDataFrameに追加
        row_data = [
            self.get_value(data, 'data_type'),
            self.get_value(data, 'time'),
            self.get_nested_value(data, ['gps', 'latitude']),
            self.get_nested_value(data, ['gps', 'longitude']),
            self.get_nested_value(data, ['gps', 'altitude']),
            self.get_nested_value(data, ['gps', 'distance', 'sample']),
            self.get_nested_value(data, ['gps', 'distance', 'goal']),
            self.get_nested_value(data, ['gps', 'azimuth', 'sample']),
            self.get_nested_value(data, ['gps', 'azimuth', 'goal']),
            self.get_nested_value(data, ['nine_axis', 'acceleration', 'x']),
            self.get_nested_value(data, ['nine_axis', 'acceleration', 'y']),
            self.get_nested_value(data, ['nine_axis', 'acceleration', 'z']),
            self.get_nested_value(data, ['nine_axis', 'angular_velocity', 'x']),
            self.get_nested_value(data, ['nine_axis', 'angular_velocity', 'y']),
            self.get_nested_value(data, ['nine_axis', 'angular_velocity', 'z']),
            self.get_nested_value(data, ['nine_axis', 'azimuth']),
            self.get_nested_value(data, ['bme280', 'temperature']),
            self.get_nested_value(data, ['bme280', 'humidity']),
            self.get_nested_value(data, ['bme280', 'pressure']),
            self.get_nested_value(data, ['lps25hb', 'temperature']),
            self.get_nested_value(data, ['lps25hb', 'pressure']),
            self.get_nested_value(data, ['lps25hb', 'altitude']),
            self.get_value(data, 'battery'),
            self.get_value(data, 'distance'),
            self.get_value(data, 'camera'),
            self.get_value(data, 'soil_moisture'),
            self.get_value(data, 'message')
        ]

        row = pd.DataFrame([row_data], columns=ex_columns)
        # Excelファイルに追記保存
        with pd.ExcelWriter(self.excel_file_name, engine='openpyxl',
                            mode='a', if_sheet_exists='overlay') as writer:
            row.to_excel(writer, sheet_name='Sheet1', startrow=self.ex_row,
                         index=False, header=False)
            self.ex_row = self.ex_row + 1  # 保存するrowの設定用

    def get_nested_value(self, dictionary, keys):
        try:
            for key in keys:
                dictionary = dictionary[key]
            return str(dictionary)
        except (KeyError, TypeError):
            return ''

    def get_value(self, dictionary, key):
        return str(dictionary.get(key, ''))

    def picture_data(self, data):
        """写真処理
        """
        picture = data.get("camera")
        img_stream = io.BytesIO(picture)
        img = Image.open(img_stream)
        img = img.resize((200, 150))
        photo = ImageTk.PhotoImage(img)
        # self.cvs.create_image(200, 150, image=photo, tag="mytest")
        # self.cvs.image = photo

    def soil_data(self, data):
        """土壌水分データ処理
        """
        time = data.get("time")
        soil = data.get("soil_moisture")
        self.soil_label.configure(text=f"soil: {soil}")
        self.time_data_soil.append(time)
        self.moisture_data.append(soil)
        self.moi.clear()
        self.moi.plot(self.time_data_soil, self.moisture_data)
        self.fig_canvas3.draw()

    def close(self):
        """終了処理
        """
        self.is_serial_connected = False
        if self.serial_port:
            self.serial_port.close()
        self.destroy()

    class ScrollableFrame(tk.Frame):
        """スクロールバー
        """

        def __init__(self, parent, *args, **kwargs):
            super().__init__(parent, *args, **kwargs)

            self.canvas = tk.Canvas(self, width=300, height=0, borderwidth=0, highlightthickness=0)
            self.scrollable_frame = tk.Frame(self.canvas)

            self.vsb = tk.Scrollbar(self, orient=tk.VERTICAL, command=self.canvas.yview)
            self.canvas.configure(yscrollcommand=self.vsb.set)

            self.vsb.pack(side=tk.RIGHT, fill=tk.Y)
            self.canvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
            self.canvas.create_window((0, 0), window=self.scrollable_frame, anchor=tk.NW)

            self.scrollable_frame.bind("<Configure>", self.on_frame_configure)


app = App()
app.mainloop()
