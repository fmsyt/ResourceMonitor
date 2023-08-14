﻿using ResourceMonitor.Models;
using ResourceMonitor.Models.Resources;
using ResourceMonitor.Properties;
using ResourceMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
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
        protected DispatcherTimer? _timer = null;
        private readonly List<MonitorViewModel> monitor;

        public BaseComponentView()
        {
            InitializeComponent();

            this.monitor = new()
            {
                new MonitorViewModel(new Cpu()),
                new MonitorViewModel(new Memory()),
                new MonitorViewModel(new MemorySwap()),
                new MonitorViewModel(new Gpu()),
            };

            //// @see https://learn.microsoft.com/ja-jp/windows/win32/cimwin32prov/win32-videocontroller
            //var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            //foreach (var obj in searcher.Get())
            //{
            //    if (obj == null)
            //    {
            //        continue;
            //    }

            //    var resource = new Resource
            //    {
            //        Label = (string)obj["SystemName"],
            //        CurrentHandler = () => (float)(1 - (int)obj["AdapterRAM"] / (int)obj["Availability"])
            //    };

            //    monitor.Add(new MonitorViewModel(resource));
            //}

            this.Loaded += new RoutedEventHandler(UserControl_Loaded);

            var list = new List<List<ContentControl?>>();

            foreach (var item in this.monitor)
            {
                list.Add(new List<ContentControl?>() { item.Label, item.Current, item.Chart });
            }

            int col = 0, row = 0;
            foreach (var rows in list)
            {
                col = 0;
                foreach (var control in rows)
                {
                    this.Panel.RowDefinitions.Add(new RowDefinition());

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

        public void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler((obj, s) =>
            {
                if (this.monitor == null)
                {
                    return;
                }

                foreach (var item in this.monitor)
                {
                    item.UpdateCurrentContent();
                }
            });

            _timer.Start();
        }

        public void StopTimer()
        {
            _timer?.Stop();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetupTimer();
        }
    }
}
