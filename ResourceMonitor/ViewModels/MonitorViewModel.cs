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
    internal class MonitorViewModel
    {
        protected Resource resource;

        public Label Label { get; protected set; } = new Label();
        public Label Current { get; protected set; } = new Label();
        public CartesianChart Chart { get; protected set; } = new CartesianChart();
        protected LineSeries lineSeries = new LineSeries();
        public MonitorViewModel(Resource resource)
        {
            this.resource = resource;
            Initialization();
        }

        protected void Initialization()
        {
            var resource = this.resource;

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
            Label.Content = resource.Label;

            var current = resource.Current();
            Current.Content = current.ToString(resource.Format);

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
