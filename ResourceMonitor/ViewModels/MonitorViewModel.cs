using ResourceMonitor.Models;
using System;
using System.Collections.Generic;
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
            Current.Content = resource.Current();
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
            Current.Content = resource.Current();
        }

        
    }
}
