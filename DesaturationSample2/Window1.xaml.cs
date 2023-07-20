using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using nCodeFxLib;

namespace DesaturationSample2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Activated += new EventHandler(Window1_Activated);
            this.Deactivated += new EventHandler(Window1_Deactivated);
        }

        void Window1_Deactivated(object sender, EventArgs e)
        {
            var anim = new SingleAnimation(1.0f, new Duration(new TimeSpan(0, 0, 1)),FillBehavior.HoldEnd);
            this.desaturationEffect.BeginAnimation(DesaturationEffect.DesaturationProperty,anim);
            this.activationState.Text = "Window DEACTIVATED!";
        }

        void Window1_Activated(object sender, EventArgs e)
        {
            var anim = new SingleAnimation(0f, new Duration(new TimeSpan(0, 0, 1)),FillBehavior.HoldEnd);
            this.desaturationEffect.BeginAnimation(DesaturationEffect.DesaturationProperty, anim);
            this.activationState.Text = "Window ACTIVATED!";
        }

    }
}
