using EnglishProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EnglishProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        int answer;

        public GamePage()
        {
            this.InitializeComponent();
        }

        private void btnSliderVertical_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void btn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayEnter = (Button)sender;
            ((Image)btnPlayEnter.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/ListOfButtons/" + ((Image)btnPlayEnter.Content).Name.Replace("img", "") + " (2).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);

        }

        private void btn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayExit = (Button)sender;
            ((Image)btnPlayExit.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/ListOfButtons/" + ((Image)btnPlayExit.Content).Name.Replace("img", "") + " (1).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void btnAnswer1_Click(object sender, RoutedEventArgs e)
        {
            answer = 1;
            CheckAnswer();
        }

        private void btnAnswer2_Click(object sender, RoutedEventArgs e)
        {
            answer = 2; 
            CheckAnswer();
        }

        private void btnAnswer3_Click(object sender, RoutedEventArgs e)
        {
            answer = 3;
            CheckAnswer();
        }

        private void btnAnswer4_Click(object sender, RoutedEventArgs e)
        {
            answer = 4;
            CheckAnswer();
        }
        private void btn_AnswerPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void btn_AnswerPointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }
        private void CheckAnswer()
        {
            if (answer != 3)
                Answer.InCorrectAnswer();
            if (Answer.IsLost())
                Frame.Navigate(typeof(EndGame));
            else
                Frame.Navigate(typeof(Question2));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
