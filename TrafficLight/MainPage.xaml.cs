using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TrafficLight.Objects;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TrafficLight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private Traffic _traffic;        // רמזור 
        private NinjaGIrl _ninjaGirl;  // דמות
        private NinjaBoy _ninjaBoy;    // דמות שנייה

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            _traffic.IsAuto = !_traffic.IsAuto;

            if (_traffic.IsAuto)        //כך משנים כתוביות ללחצן
                btnAuto.Content = "Stop";
            else
                btnAuto.Content = "Auto";

            btnManual.IsEnabled = !_traffic.IsAuto;
        }

        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            _traffic.SetState();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _traffic = new Traffic(elpRed, elpYellow, elpGreen);//בניית רמזור

            _ninjaGirl = new NinjaGIrl(imgNinjaGirl);
            _ninjaBoy = new NinjaBoy(imgNinjaBoy);

        }
    }
}
