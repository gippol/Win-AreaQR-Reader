# ScreenQRCodeReader

画面上の任意の領域を選択してQRコードを読み取るWindows向けデスクトップアプリです。

## 機能

- 画面の任意の矩形領域をドラッグで選択
- 選択した領域内のQRコードを自動デコード
- 読み取り結果をクリップボードにコピー

## 動作環境

- Windows 10 / 11
- .NET 10 (Windows)

## ビルド・実行

```bash
dotnet build
dotnet run --project ScreenQRCodeReader
```

## 使い方

1. アプリを起動する
2. **「QRコード取得」** ボタンをクリック（メインウィンドウが非表示になる）
3. 画面全体にオーバーレイが表示されるので、QRコードが含まれる領域をドラッグで選択する
4. 選択を確定するとメインウィンドウに戻り、デコード結果がテキストボックスに表示される
5. **「コピー」** ボタンで結果をクリップボードにコピーできる

## 技術構成

| 項目 | 内容 |
|---|---|
| フレームワーク | WPF (.NET 10, net10.0-windows) |
| バーコードデコード | ZXing.Net v0.16.11 + ZXing.Net.Bindings.Windows.Compatibility |
| 画面キャプチャ | System.Drawing (GDI+) |

## ファイル構成

| ファイル | 役割 |
|---|---|
| `MainWindow.xaml/.cs` | メインUI（キャプチャ起動・結果表示・クリップボードコピー） |
| `SelectionWindow.xaml/.cs` | フルスクリーンのドラッグ選択オーバーレイ |
| `BitmapUtil.cs` | `System.Drawing.Bitmap` → WPF `BitmapSource` 変換（GDIハンドル解放含む） |
