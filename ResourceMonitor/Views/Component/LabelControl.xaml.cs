using ResourceMonitor.Models;
using System;
using System.Collections.Generic;
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
using Windows.UI.WebUI;

namespace ResourceMonitor.Views.Component
{
    public delegate void HandleChange(string label);

    /// <summary>
    /// LabelControl.xaml の相互作用ロジック
    /// </summary>
    public partial class LabelControl : UserControl
    {
        protected bool isEditing = false;

        public Label label = new();
        public TextBox textBox = new();

        public HandleChange? PostLabelChanged;

     

        public LabelControl()
        {
            InitializeComponent();

            label.MouseDoubleClick += (sender, e) =>
            {
                isEditing = true;
                textBox.Text = label.Content.ToString();

                UpdateUI();
            };

            textBox.KeyDown += (sender, e) =>
            {
                isEditing = false;

                if (e.Key == Key.Enter)
                {
                    label.Content = textBox.Text;

                    PostLabelChanged?.Invoke(textBox.Text);
                    UpdateUI();

                } 
                else if (e.Key == Key.Escape)
                {
                    textBox.Text = label.Content.ToString();
                }

                UpdateUI();
            };

            UpdateUI();

            panel.Children.Add(label);
            panel.Children.Add(textBox);
        }


        protected void UpdateUI()
        {
            label.Visibility = isEditing ? Visibility.Collapsed : Visibility.Visible;
            textBox.Visibility = isEditing ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
