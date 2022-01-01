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

            this.chartCpu.Series.Clear();
            this.chartCpu.Hoverable = false;
            this.chartCpu.DisableAnimations = true;
            this.chartCpu.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
            });

            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.Count)
                .Y(x => x.Value);

            var seri = new LineSeries();
            seri.PointGeometrySize = 0;
            seri.LineSmoothness = 0;

            Charting.For<MeasureModel>(mapper);
            seri.Values = new ChartValues<MeasureModel>();

            this.chartCpu.Series.Add(seri);

            var sw = new Stopwatch();
            sw.Start();

            int count = 0;
            var list = new List<MeasureModel>(Config.Capacity);
            for (int i = 0; i < Config.Capacity - 1; i++)
            {
                list.Add(new MeasureModel { Count = count++ });
            }

            seri.Values.AddRange(list);

            Task.Run(() =>
            {
                while (true)
                {
                    seri.Values.Add(new MeasureModel
                    {
                        Count = count++,
                        Value = Cpu.Instance.Current() * 100
                    });

                    seri.Values.RemoveAt(0);
                    
                    Thread.Sleep(1000);
                }
            });

        }
    }
}

class MeasureModel
{
    public double Count { get; set; } = 0;
    public double Value { get; set; } = 0;
}