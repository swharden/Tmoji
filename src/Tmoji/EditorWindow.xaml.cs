using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private readonly ObservableCollection<ButtonGroup> Groups = new ObservableCollection<ButtonGroup>();

        public EditorWindow()
        {
            InitializeComponent();
            LoadSettings();
            GroupList.Items.Clear();
            GroupList.ItemsSource = Groups;
            GroupList.SelectedIndex = 0;
        }

        private void LoadSettings()
        {
            var settings = new Settings();
            settings.Load();
            foreach (var group in settings.ButtonGroups)
                Groups.Add(group);
        }

        private void SaveSettings()
        {
            var settings = new Settings();
            foreach (var group in Groups)
                settings.Add(group);
            settings.Save();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e) =>
            Groups.Add(new ButtonGroup($"Group {Groups.Count + 1}", 10));

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (GroupList.Items.Count == 1 || GroupList.SelectedIndex < 0)
                return;

            int originalIndex = GroupList.SelectedIndex;
            Groups.RemoveAt(originalIndex);
            GroupList.SelectedIndex = Math.Min(originalIndex, Groups.Count - 1);
        }

        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            int originalIndex = GroupList.SelectedIndex;
            var group = Groups[originalIndex];
            Groups.RemoveAt(originalIndex);
            int newIndex = Math.Max(originalIndex - 1, 0);
            Groups.Insert(newIndex, group);
            GroupList.SelectedIndex = newIndex;
        }

        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            int originalIndex = GroupList.SelectedIndex;
            var group = Groups[originalIndex];
            Groups.RemoveAt(originalIndex);
            int newIndex = Math.Min(originalIndex + 1, Groups.Count);
            Groups.Insert(newIndex, group);
            GroupList.SelectedIndex = newIndex;
        }
    }
}
