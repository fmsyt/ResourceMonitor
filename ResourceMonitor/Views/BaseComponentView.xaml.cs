using ResourceMonitor.Models;
using ResourceMonitor.Models.Resources;
using ResourceMonitor.ViewModels.Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace ResourceMonitor.Views
{
    /// <summary>
    /// BaseComponentView.xaml の相互作用ロジック
    /// </summary>
    public partial class BaseComponentView : UserControl
    {
        private readonly CpuViewModel cpu;
        private readonly MemoryViewModel memory;

        public BaseComponentView()
        {
            InitializeComponent();

            this.cpu = new CpuViewModel();
            this.memory = new MemoryViewModel();

            this.Loaded += new RoutedEventHandler(UserControl_Loaded);

            Grid.SetRow(cpu.Label, 0);
            Grid.SetRow(cpu.Current, 0);
            Grid.SetColumn(cpu.Label, 0);
            Grid.SetColumn(cpu.Current, 1);

            Grid.SetRow(memory.Label, 1);
            Grid.SetRow(memory.Current, 1);
            Grid.SetColumn(memory.Label, 0);
            Grid.SetColumn(memory.Current, 1);

            this.Panel.Children.Add(cpu.Label);
            this.Panel.Children.Add(cpu.Current);

            this.Panel.Children.Add(memory.Label);
            this.Panel.Children.Add(memory.Current);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.cpu.SetupTimer();
            this.memory.SetupTimer();
        }
    }
}
