using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using ResourceMonitor.Models;
using ResourceMonitor.Models.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ResourceMonitor.ViewModels
{
    internal class MonitorViewModel<I> where I : SingletonResource<I>, IResource, new()
    {
        protected SingletonResource<I> resource = SingletonResource<I>.Instance;
        protected DispatcherTimer? _timer = null;

        public Label Label { get; protected set; } = new Label();
        public Label Current { get; protected set; } = new Label();
        public CartesianChart Chart { get; protected set; } = new CartesianChart();
        protected LineSeries lineSeries = new LineSeries();
        public MonitorViewModel()
        {
            Initialization();
        }


        public MonitorViewModel(string labelContent)
        {
            Initialization(labelContent);
        }

        protected void Initialization(string? labelContent = null)
        {
            Label.Content = labelContent;
            Current.Content = resource.Current().ToString(Config.Resource[resource].Format); ;

            Chart.Series.Clear();
            Chart.Hoverable = false;
            Chart.DisableAnimations = true;
            Chart.DataTooltip = null;

            Chart.AxisX.Clear();
            Chart.AxisX.Add(new Axis
            {
                LabelFormatter = (v) => null,
                ShowLabels = false,
                IsEnabled = false,
            });

            Chart.AxisY.Clear();
            Chart.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
                ShowLabels = false,
                IsEnabled = false,
            });

            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.Count)
                .Y(x => x.Value);

            lineSeries.PointGeometrySize = 0;
            lineSeries.LineSmoothness = 0;

            Charting.For<MeasureModel>(mapper);
            lineSeries.Values = new ChartValues<MeasureModel>();

            Chart.Series.Add(lineSeries);

            int count = 0;
            var list = new List<MeasureModel>(Config.Resource[resource].Capacity);
            for (int i = 0; i < Config.Resource[resource].Capacity - 1; i++)
            {
                list.Add(new MeasureModel { Count = count++ });
            }

            resource.Count = count;

            lineSeries.Values.AddRange(list);
        }


        public void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(UpdateCurrentContent);
            _timer.Start();
        }

        public void StopTimer()
        {
            if (_timer != null) { _timer.Stop(); }
        }

        public void UpdateCurrentContent(object sender, EventArgs e)
        {
            var current = resource.Current();
            Current.Content = current.ToString(Config.Resource[resource].Format);

            lineSeries.Values.Add(new MeasureModel
            {
                Count = resource.Count++,
                Value = current * 100
            });

            lineSeries.Values.RemoveAt(0);
        }
    }

    class MeasureModel
    {
        public float Count { get; set; } = 0;
        public float Value { get; set; } = 0;
    }
}