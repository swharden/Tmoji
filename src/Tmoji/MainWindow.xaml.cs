using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tmoji
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Windows.Forms.NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            Hide();

            string notifyIconPath = System.IO.Path.GetFullPath("Tmoji.ico");
            if (!System.IO.File.Exists(notifyIconPath))
                throw new InvalidOperationException($"cannot locate icon file: {notifyIconPath}");
            var icon = new System.Drawing.Icon(notifyIconPath);
            notifyIcon = new System.Windows.Forms.NotifyIcon() { Visible = true, Icon = icon };
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(Icon_Click);
            notifyIcon.DoubleClick += new EventHandler(Icon_DoubleClick);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            Clipboard.SetText(content);
            Hide();
            Debug.WriteLine($"Copied {content}");
        }

        private void BalloonNotifyCopied(string symbol)
        {
            notifyIcon.ShowBalloonTip(1000, "Tmoji", $"copied: {symbol}", System.Windows.Forms.ToolTipIcon.Info);
        }

        void Icon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Visibility == Visibility.Visible)
                {
                    Hide();
                }
                else
                {
                    // position window near the taskbar
                    Top = SystemParameters.PrimaryScreenHeight - Height - 5;
                    Left = SystemParameters.PrimaryScreenWidth - Width - 5;
                    Show();
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBoxResult result = MessageBox.Show("Exit Tmoji?", "Tmoji", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    Close();
            }
        }

        void Icon_DoubleClick(object sender, EventArgs e)
        {
        }
    }
}
