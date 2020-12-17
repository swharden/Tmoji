using System;
using System.Collections.Generic;
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

            var brush = Brushes.Transparent;
            float fontSize = 32;
            B1.ButtonImage.Source = Emoji.Wpf.EmojiRenderer.RenderBitmap("😁", fontSize, brush);
            B2.ButtonImage.Source = Emoji.Wpf.EmojiRenderer.RenderBitmap("🤦‍", fontSize, brush);
            B3.ButtonImage.Source = Emoji.Wpf.EmojiRenderer.RenderBitmap("💖", fontSize, brush);
        }
    }
}
