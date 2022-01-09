using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ResourceMonitor.Models;
using ResourceMonitor.Models.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResourceMonitor.Views
{
    /// <summary>
    /// SettingsView.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Config.Instance.SaveAll();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Topmost = Config.Instance.General.Topmost;
            this.Owner.Show();
        }
    }
}
