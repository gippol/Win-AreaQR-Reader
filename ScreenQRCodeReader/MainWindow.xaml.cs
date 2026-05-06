using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ZXing.Windows.Compatibility;

namespace ScreenQRCodeReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            try
            {
                var selector = new SelectionWindow();
                if (selector.ShowDialog() == true)
                {
                    string? text = null;
                    var bmp = CaptureArea(selector.X, selector.Y, selector.W, selector.H);

                    try
                    {
                        // デバッグ表示
                        this.DebugImage.Source = BitmapUtil.ConvertBitmap(bmp);
                        // QR解析
                        text = DecodeQR(bmp);
                    }
                    finally
                    {
                        bmp.Dispose();
                    }

                    ResultText.Text = text ?? "QRコードが見つかりません";
                }
            }
            finally
            {
                this.Show();
            }
        }

        private string? DecodeQR(Bitmap bitmap)
        {
            var reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            return result?.Text;
        }

        private Bitmap CaptureArea(int x, int y, int width, int height)
        {
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height));
            }

            return bmp;
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ResultText.Text))
            {
                System.Windows.Clipboard.SetText(ResultText.Text);
            }
        }
    }
}