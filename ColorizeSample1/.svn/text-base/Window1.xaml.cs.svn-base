using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorizeSample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public IEnumerable EnumerateColors()
        {
            foreach (var color in typeof(Colors).GetProperties())
                yield return color;
        }

        public IEnumerable<string> GetColorNames()
        {
            foreach (PropertyInfo p
              in typeof(Colors).GetProperties(
              BindingFlags.Public | BindingFlags.Static))
            {
                yield return p.Name;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
