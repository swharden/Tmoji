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
        private System.Windows.Forms.NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            Hide();
            InitTaskbarIcon();
            ResetLayout();
        }

        /// <summary>
        /// Show an icon in the windows tray and connect mouse click events
        /// </summary>
        private void InitTaskbarIcon()
        {
            string notifyIconPath = System.IO.Path.GetFullPath("Tmoji.ico");
            if (!System.IO.File.Exists(notifyIconPath))
                throw new InvalidOperationException($"cannot locate icon file: {notifyIconPath}");
            var icon = new System.Drawing.Icon(notifyIconPath);
            notifyIcon = new System.Windows.Forms.NotifyIcon() { Visible = true, Icon = icon };
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(TrayIcon_Click);
        }

        /// <summary>
        /// Dynamically create groupboxes and buttons based on emoji settings file
        /// </summary>
        private void ResetLayout(float buttonSize = 32, float fontSize = 18)
        {
            ButtonStackPanel.Children.Clear();

            List<ButtonGroup> buttonGroups = new List<ButtonGroup>();
            buttonGroups.Add(new ButtonGroup("Emoji", "😜,😝,😎,🤓,😂,🤣,🤔,💁,⚠️,❓,💡,💀,☠️,☝️,😈".Split(',')));
            buttonGroups.Add(new ButtonGroup("Symbols", "±,Δ,µ,Ω,σ,τ,λ,↑,↓,←,→,⤙".Split(',')));

            foreach (ButtonGroup grp in buttonGroups)
            {
                var wrapPanel = new WrapPanel();
                var groupBox = new GroupBox()
                {
                    Header = grp.Name,
                    Margin = new Thickness(5, 0, 5, 0),
                    Content = wrapPanel
                };
                ButtonStackPanel.Children.Add(groupBox);

                foreach (string lbl in grp.Labels)
                {
                    var btn = new Button()
                    {
                        Content = lbl,
                        Width = buttonSize,
                        Height = buttonSize,
                        Margin = new Thickness(2),
                        FontSize = fontSize,
                        FontFamily = new FontFamily("Segoe UI"),
                    };
                    btn.Click += new RoutedEventHandler(Button_Emoji_Click);
                    wrapPanel.Children.Add(btn);
                }
            }
        }

        private void Button_Emoji_Click(object sender, RoutedEventArgs e)
        {
            string text = (sender as Button).Content.ToString();
            Clipboard.SetText(text);
            Hide();
        }

        private void Button_X_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        void TrayIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBoxResult result = MessageBox.Show("Exit Tmoji?", "Tmoji", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    Close();
            }

            ShowWindowAtBottomRight();
        }

        private void ShowWindowAtBottomRight()
        {
            Show();
            Top = SystemParameters.PrimaryScreenHeight - ActualHeight - 45;
            Left = SystemParameters.PrimaryScreenWidth - ActualWidth - 10;
            Activate();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
