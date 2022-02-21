using ResourceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        protected int GWL_STYLE = -16;
        protected int WS_OVERLAPPEDWINDOW = 0;
        protected int WS_THICKFRAME = 0x00040000;

        protected SettingsView? settingsWindow = null;

        [DllImport("user32")]
        protected static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwLong);

        public MainView()
        {
            InitializeComponent();

            var view = new BaseComponentView();
            this.MainPanel.Children.Add(view);

            var isTopmost = Config.Instance.General.Topmost; ;
            this.Topmost = isTopmost;
            this.ToggleTopmost.IsChecked = isTopmost;

            this.Loaded += MainWindow_Loaded;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (settingsWindow != null)
            {
                settingsWindow.Close();
            }
        }

        protected void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _ = SetWindowLong(new WindowInteropHelper(this).Handle, GWL_STYLE, WS_OVERLAPPEDWINDOW ^ WS_THICKFRAME);
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
