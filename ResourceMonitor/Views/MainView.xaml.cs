using ResourceMonitor.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResourceMonitor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        protected SettingsView? settingsWindow = null;

        public MainView()
        {
            InitializeComponent();

            var view = new BaseComponentView();
            this.MainPanel.Children.Add(view);

            var isTopmost = Config.Instance.General.Topmost; ;
            this.Topmost = isTopmost;
            this.ToggleTopmost.IsChecked = isTopmost;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (settingsWindow != null)
            {
                settingsWindow.Close();
            }
        }


        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            if (settingsWindow == null || !settingsWindow.IsActive)
            {
                this.settingsWindow = new SettingsView();
                this.settingsWindow.Owner = this;
                this.settingsWindow.Show();
            }
            else
            {
                this.settingsWindow.Activate();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToggleTopmost_Click(object sender, RoutedEventArgs e)
        {
            var isTopmost = this.ToggleTopmost.IsChecked;
            this.ToggleTopmost.IsChecked = isTopmost;
            this.Topmost = isTopmost;

            Config.Instance.General.Topmost = isTopmost;
            Config.Instance.General.Save();
        }
    }
}
