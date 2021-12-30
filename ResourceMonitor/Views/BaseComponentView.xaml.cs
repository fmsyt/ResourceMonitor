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
        private DispatcherTimer? _timer = null;

        private MemoryViewModel memory;

        private Label label = new Label();
        private Label current = new Label();

        public BaseComponentView()
        {
            InitializeComponent();

            this.memory = new MemoryViewModel();

            this.Loaded += new RoutedEventHandler(UserControl_Loaded);

            label.Content = "CPU";
            current.Content = Cpu.Instance.Current();

            Grid.SetColumn(label, 0);
            Grid.SetColumn(current, 1);

            Grid.SetColumn(memory.Label, 2);
            Grid.SetColumn(memory.Current, 3);

            this.Panel.Children.Add(label);
            this.Panel.Children.Add(current);

            this.Panel.Children.Add(memory.Label);
            this.Panel.Children.Add(memory.Current);
        }

        private void BaseComponentView_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            StopTimer();
        }

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(UpdateCurrentContent);
            _timer.Start();

            var window = Window.GetWindow(this);
            window.Closing += new CancelEventHandler(BaseComponentView_Closing);
        }

        

        private void UpdateCurrentContent(object sender, EventArgs e)
        {
            current.Content = Cpu.Instance.Current();
        }

        private void StopTimer()
        {
            if (_timer != null) { _timer.Stop(); }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetupTimer();
            this.memory.SetupTimer();
        }
    }
}
