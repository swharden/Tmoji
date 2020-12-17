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
using System.Windows.Shapes;

namespace Tmoji
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            LoadEmoji();
        }

        private void LoadEmoji()
        {
            List<ButtonGroup> groups = new List<ButtonGroup>();

            groups.Add(new EmojiGroup("Emoji", "😜😝😎🤓😂🤣🤔💁⚠️❓💡💀☠️☝️😈"));
            groups.Add(new SymbolGroup("Symbols", "±ΔµΩστλ↑↓←→⤙"));

            ResetButtonLayout(groups);
        }

        /// <summary>
        /// Delete old buttons and create all new ones based on the latest configuration
        /// </summary>
        private void ResetButtonLayout(List<ButtonGroup> groups, float buttonSize = 32, float fontSize = 18)
        {
            ButtonHolder.Children.Clear();

            foreach (var group in groups)
            {
                var groupBox = new GroupBox()
                {
                    Header = group.Name,
                    Margin = new Thickness(5, 0, 5, 0),
                };
                ButtonHolder.Children.Add(groupBox);

                var wrapPanel = new WrapPanel();
                groupBox.Content = wrapPanel;

                foreach (string emoji in group.Labels)
                {
                    BitmapSource bmp = Emoji.Wpf.EmojiRenderer.RenderBitmap(emoji, buttonSize, Brushes.Transparent);
                    var btn = new Button()
                    {
                        Name = $"Button{ButtonHolder.Children.Count}",
                        Width = buttonSize,
                        Height = buttonSize,
                        Margin = new Thickness(2),
                        FontSize = fontSize,
                        FontFamily = new FontFamily("Segoe UI")
                    };

                    if (group is EmojiGroup)
                        btn.Content = new Image() { Source = bmp };
                    else
                        btn.Content = emoji;

                    wrapPanel.Children.Add(btn);
                }
            }
        }
    }
}
