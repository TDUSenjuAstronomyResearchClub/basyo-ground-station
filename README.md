# 弐号機使用の地上局プログラム

種子島ロケットコンテストで使用する地上局のプログラムです。

## ライブラリ
地上局を使用する上でインストールが必要なライブラリを以下示す。
ここに記述がないものは標準ライブラリとなっている。

| name       | pip_install_--- |
|------------|-----------------|
| serial     | pyserial        |
| matplotlib | matplotlib      |
| folium     | folium          |
| selenium   | selenium        |
| PIL        | Pillow          |
| pandas     | pandas          |

## 実行方法と機能
1. 使用するPCにXbeeを接続し、COMポートを確認する。
2. プログラムを開いてCOMポート番号を書き換え、プログラムを実行する。
3. プログラムを実行するとGUIが作成される。 
4. GUI右上にある "Start Communication" と書かれたボタンを押すと、機体との通信を開始する。
5. データを受信するとGUI上にデータを表示され、更新されていく。
6. "main" と "graph" を押すと、メインタブとグラフ表示用タブの変更ができる。
7. コマンド一覧の上部にある欄に送信コマンドを打ち込み、 "send" と書かれたボタンを押すと、機体にコマンドを送信する。
8. 通信終了をする場合は、通信開始ボタンが "Stop Communication" と書かれたボタンに変化するので、押すことで通信終了する。


## 機体作成に当たって使用している他のサイト
### SharePoint

<https://tdumedia.sharepoint.com/sites/astronomy>


