using BussinesTourProject.Classes;
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
using System.Threading;
using GameEngine.GameServices;
using Windows.Security.Authentication.OnlineId;
using System.Threading.Tasks;
using Windows.UI.Core;
using System.Runtime.CompilerServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BussinesTourProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        Random rnd = new Random();
        Player Player1 = new Player(name: "Player1");
        Player Player2 = new Player(name: "Player2");
        Player Player3 = new Player(name: "Player3");
        Player Player4 = new Player(name: "Player4");

        int currentDiceResult = 4;

        private DispatcherTimer timer;
        private int secondsElapsed;

        public GamePage()
        {
            this.InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Set interval to 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            secondsElapsed++;
            // Update UI with the elapsed time
            UpdateUITextBlock.Text = $"Seconds Elapsed: {secondsElapsed}";
        }

        private static void InitPlayer(Player player, Image imgPlayer, int[,] playerMatrixPositions)
        {
            player.SetPlayerPosition(playerMatrixPositions);
            player.Img = imgPlayer;
            Grid.SetRow(player.Img, player.PlayerPosition[0, 0]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, 0]);
             
        }


        public  static void ChangePlayerPositionAnimation(Player player, int diceResult)
        {
            int currentPosition = player.currentPosition + 1;
            player.ChangePlayerPosition(diceResult); // changing position of the player, and make sure that there is not overflow
            for (int i = 0; i < diceResult; i++)
            {

                while (currentPosition > (player.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= player.PlayerPosition.GetLength(1);
                }
                if (currentPosition >= 11 && currentPosition < 20)
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/Player1/RedCarBackward.png"));

                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);
                Task.Delay(50000);

                currentPosition++;

            }
        }
       
        int row = 30;
        int col = 3;
        private void btn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayEnter = (Button)sender;
            ((Image)btnPlayEnter.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/UsingButtons/" + ((Image)btnPlayEnter.Content).Name.Replace("img", "") + " (1).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }
        private void btn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayExit = (Button)sender;
            ((Image)btnPlayExit.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/UsingButtons/" + ((Image)btnPlayExit.Content).Name.Replace("img", "") + " (2).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));      
        }

        private void btnMovePlayer_Click(object sender, RoutedEventArgs e)
        {   
             ChangePlayerPositionAnimation(Player1, 3);
        }
        

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(imgPlayer, col++);
        }

        private void MovingPlayer()
        {
            
        }

        private async void DelayCode_Click(object sender, RoutedEventArgs e)
        {

            // Disable the button during the delay to prevent multiple clicks
            ((Button)sender).IsEnabled = false;
            int currentPosition = Player1.currentPosition + 1;
            Player1.ChangePlayerPosition(currentDiceResult); // changing position of the player, and make sure that there is not overflow
            // Delay for 2 seconds

            for (int i = 0; i < currentDiceResult; i++)
            {

                while (currentPosition > (Player1.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= Player1.PlayerPosition.GetLength(1);
                }
                if (currentPosition >= 11 && currentPosition < 20)
                    Player1.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/Player1/RedCarBackward.png"));

                Grid.SetRow(Player1.Img, Player1.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(Player1.Img, Player1.PlayerPosition[1, currentPosition]);

                currentPosition++;
                await Task.Delay(TimeSpan.FromSeconds(1));

            }

            // Code to execute after the delay
            // For example, show a message box

            // Re-enable the button after the delay
            ((Button)sender).IsEnabled = true;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            int[,] MatrixPositionPlayer1 = { { 80, 70, 62, 54, 46, 38, 30, 22,
                                               1, 1, 1, 1, 1, 1, 1, 1,
                                               14, 22, 30, 38, 46, 54, 62, 70,
                                               81, 81, 81, 81, 81, 81, 81 ,81},
                                               { 2, 2, 2, 2, 2, 2, 2, 2,
                                                 14, 18, 26, 34, 42, 50, 58, 66,
                                                 85, 85, 85, 85, 85, 85, 85, 85,
                                                 74, 66, 58, 50, 42, 34, 26, 18} };

            InitPlayer(Player1, imgPlayer, MatrixPositionPlayer1);
            //gameManager = new GameManager();
            //gameManager.Start();
        }

    }
}
