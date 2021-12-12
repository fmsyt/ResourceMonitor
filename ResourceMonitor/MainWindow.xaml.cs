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

namespace ResourceMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected SettingsWindow? settingsWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (settingsWindow != null)
            {
                settingsWindow.Close();
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            if (this.settingsWindow == null)
            {
                this.settingsWindow = new SettingsWindow();
                this.settingsWindow.Show();
            } 
            else
            {
                this.settingsWindow.Activate();
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
