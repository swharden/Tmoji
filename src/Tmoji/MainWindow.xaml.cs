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
        private readonly Settings Settings = new Settings();

        public MainWindow()
        {
            InitializeComponent();
            TrayIcon_Init();
            ResetLayout();
            Hide();

            if (Debugger.IsAttached)
            {
                new EditorWindow().ShowDialog();
                Close();
            }
        }

        private void TrayIcon_Init()
        {
            string notifyIconPath = System.IO.Path.GetFullPath("Tmoji.ico");
            if (!System.IO.File.Exists(notifyIconPath))
                throw new InvalidOperationException($"cannot locate icon file: {notifyIconPath}");
            var icon = new System.Drawing.Icon(notifyIconPath);
            notifyIcon = new System.Windows.Forms.NotifyIcon() { Visible = true, Icon = icon };
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(TrayIcon_Click);
        }

        void TrayIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Show();
            Activate();
        }

        /// <summary>
        /// Repopulate the layout with groupboxes and buttons based on Settings
        /// </summary>
        private void ResetLayout(float buttonSize = 32, float fontSize = 18)
        {
            Settings.Load();
            ButtonStackPanel.Children.Clear();
            foreach (var group in Settings.ButtonGroups)
            {
                var wrapPanel = new WrapPanel();
                var groupBox = new GroupBox()
                {
                    Header = group.Name,
                    Margin = new Thickness(5, 0, 5, 0),
                    Content = wrapPanel
                };
                ButtonStackPanel.Children.Add(groupBox);

                foreach (string label in group.Labels)
                {
                    var btn = new Button()
                    {
                        Content = label,
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

        private void Window_Layout_Updated(object sender, EventArgs e)
        {
            // reposition the window to be on the bottom-right corner of the screen
            Top = SystemParameters.PrimaryScreenHeight - ActualHeight - 45;
            Left = SystemParameters.PrimaryScreenWidth - ActualWidth - 10;
        }

        private void Button_Emoji_Click(object sender, RoutedEventArgs e)
        {
            string text = (sender as Button).Content.ToString();
            Clipboard.SetText(text);
            Hide();
        }

        private void Window_Deactivated(object sender, EventArgs e) => Hide();

        private void Button_Settings_Click(object sender, RoutedEventArgs e) => (sender as Button).ContextMenu.IsOpen = true;

        private void Button_X_Click(object sender, RoutedEventArgs e) => Hide();

        private void Button_Settings_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Exit the Tmoji tray app too?", "Tmoji", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
                Close();
            else if (result == MessageBoxResult.No)
                Hide();
            else if (result == MessageBoxResult.Cancel)
                Show();
        }

        private void Button_Settings_Load_Click(object sender, RoutedEventArgs e) => ResetLayout();

        private void Button_Settings_Save_Click(object sender, RoutedEventArgs e) => Settings.Save();

        private void Button_Settings_Edit_Click(object sender, RoutedEventArgs e) => Settings.Launch();

        private void Button_Settings_Reset_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Overwrite your settings file?", "Reset Tmoji", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Settings.LoadDefaultSettings();
                Settings.Save();
                ResetLayout();
                Show();
            }
        }
    }
}
