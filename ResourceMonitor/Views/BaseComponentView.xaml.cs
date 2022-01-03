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

            var list = new List<List<ContentControl?>>();

            list.Add(new List<ContentControl?>() { cpu.Label, cpu.Current, cpu.Chart });
            list.Add(new List<ContentControl?>() { memory.Label, memory.Current, memory.Chart });

            int col = 0, row = 0;
            foreach (var rows in list)
            {
                col = 0;
                foreach (var control in rows)
                {
                    if (control != null)
                    {
                        var wrapedControl = Wrap(control);
                        Grid.SetRow(wrapedControl, row);
                        Grid.SetColumn(wrapedControl, col);

                        this.Panel.Children.Add(wrapedControl);
                    }

                    col++;
                }
                row++;
            }
        }

        private static Border Wrap(ContentControl? control)
        {
            var border = new Border();
            if (control != null)
            {
                border.Child = control;
            }

            return border;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.cpu.SetupTimer();
            this.memory.SetupTimer();
        }
    }
}
