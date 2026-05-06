# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
dotnet build
dotnet run --project ScreenQRCodeReader
```

No test projects exist in this solution.

## Architecture

**ScreenQRCodeReader** is a WPF desktop app (net10.0-windows) that captures a user-selected screen region and decodes any QR code found in it.

### Flow

1. User clicks "QRコード取得" in `MainWindow` → window hides
2. `SelectionWindow` opens as a fullscreen borderless overlay — user drags to select a region
3. On close, `MainWindow.CaptureArea()` screenshots the selected rect via `Graphics.CopyFromScreen`
4. `MainWindow.DecodeQR()` passes the bitmap to ZXing's `BarcodeReader`
5. Result (or error) is shown in the TextBox; "コピー" copies it to clipboard

### Key files

| File | Role |
|---|---|
| `MainWindow.xaml/.cs` | Primary UI: capture trigger, result display, clipboard copy |
| `SelectionWindow.xaml/.cs` | Fullscreen mouse-drag overlay; exposes selected `Rect` |
| `BitmapUtil.cs` | P/Invoke helper — converts `System.Drawing.Bitmap` → WPF `BitmapSource` (calls `DeleteObject` to release GDI handle) |

### Dependencies

- **ZXing.Net** (`v0.16.11`) + **ZXing.Net.Bindings.Windows.Compatibility** — QR/barcode decoding
- `System.Drawing` (GDI+) — screen capture and bitmap manipulation
- `System.Windows.Forms` — used alongside WPF for form-level helpers

### Conventions

- Single namespace: `ScreenQRCodeReader`
- No MVVM — plain code-behind with event handlers
- UI labels are in Japanese (Japanese-language app)
