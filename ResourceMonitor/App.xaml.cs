using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var icon = GetResourceStream(new Uri("Assets/icon.ico", UriKind.Relative)).Stream;
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click);

            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "ResourceMonitor",
                ContextMenuStrip = menu
            };

            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);

        }

        private void NotifyIcon_Click(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            Application.Current.MainWindow.Focus();
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            Shutdown();
        }
    }
}
