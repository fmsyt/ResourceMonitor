using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using ResourceMonitor.Models;
using ResourceMonitor.Models.Resources;
using ResourceMonitor.Views.Component;
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
    internal class MonitorViewModel
    {
        public Resource Resource { get; protected set; }

        public LabelControl Label { get; protected set; } = new();
        public Label Current { get; protected set; } = new();
        public CartesianChart Chart { get; protected set; } = new CartesianChart();
        protected LineSeries lineSeries = new LineSeries();
        public MonitorViewModel(Resource resource)
        {
            this.Resource = resource;
            Initialization();
        }

        protected void Initialization()
        {
            var resource = this.Resource;

            Label.label.Content = resource.Label;
            Label.PostLabelChanged = (label) => resource.SaveLabel(label);

            Current.Content = resource.Current().ToString(resource.Format);

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

            var mapper = Mappers.Xy<MeasureModel>().Y(x => x.Value);

            lineSeries.PointGeometrySize = 0;
            lineSeries.LineSmoothness = 0;

            Charting.For<MeasureModel>(mapper);
            lineSeries.Values = new ChartValues<MeasureModel>();

            Chart.Series.Add(lineSeries);

            var list = new List<MeasureModel>(resource.Capacity);
            for (int i = 0; i < resource.Capacity - 1; i++)
            {
                list.Add(new MeasureModel());
            }

            lineSeries.Values.AddRange(list);
        }



        public void UpdateCurrentContent()
        {
            var current = Resource.Current();
            Current.Content = current.ToString(Resource.Format);

            lineSeries.Values.Add(new MeasureModel(current * 100));
            lineSeries.Values.RemoveAt(0);
        }
    }

    class MeasureModel
    {
        public float Value { get; set; } = 0;
        public MeasureModel() { }
        public MeasureModel(float value) { Value = value; }
    }
}
