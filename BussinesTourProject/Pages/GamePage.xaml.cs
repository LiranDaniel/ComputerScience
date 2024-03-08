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
using Windows.Gaming.Input;
using System.Reflection.Metadata.Ecma335;
using Windows.ApplicationModel.VoiceCommands;

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
        private Player currentPlayer;
        private int currentTimesPlay = 1;

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

        private static void InitPlayer(Player player, Image imgPlayer, int[,] playerMatrixPositions, string name)
        {
            player.SetPlayerPosition(playerMatrixPositions);
            player.Img = imgPlayer;
            Grid.SetRow(player.Img, player.PlayerPosition[0, 0]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, 0]);
            player.name = name;
            
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
             ChangePlayerPositionAnimation(Player1, 1);
        }
        

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(imgPlayer, col++);
        }

        private void MovingPlayer()
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            int[,] MatrixPositionPlayer1 = { { 79, 69, 61, 53, 45, 37, 29, 21,
                                               0, 0, 0, 0, 0, 0, 0, 0,
                                               14, 22, 30, 38, 46, 54, 62, 70,
                                               93, 93, 93, 93, 93, 93, 93, 93},
                                               { 5, 5, 6, 6, 7, 7, 7, 7,
                                                 10, 18, 26, 34, 42, 50, 58, 66,
                                                 85, 85, 85, 85, 85, 85, 85, 85,
                                                 74, 66, 58, 50, 42, 34, 26, 18} };

            InitPlayer(Player1, imgPlayer, MatrixPositionPlayer1, "Player1");
            currentPlayer = Player1;
            Player[] arrayPlayers = {Player1, Player2, Player3, Player4};
        }
        private void NextPlayer()
        {

        }

        private async void MoveToJail(Player player)
        {
            GridCards.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2));
            GridCards.Visibility = Visibility.Collapsed
                ;
            player.currentPosition = 8;
            currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + currentPlayer.name + "/RedCarRight.png"));
            Grid.SetColumnSpan(currentPlayer.Img, 5);
            Grid.SetRowSpan(currentPlayer.Img, 4);

            Grid.SetRow(currentPlayer.Img, currentPlayer.PlayerPosition[0, player.currentPosition]);
            Grid.SetColumn(currentPlayer.Img, currentPlayer.PlayerPosition[1, player.currentPosition]);

        }
        private async void btnRoll_Dice_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            ((Button)sender).Visibility = Visibility.Collapsed;
            imgDice1.Visibility = Visibility.Visible;
            imgDice2.Visibility = Visibility.Visible;
            int[] Result = Map.RollDice();
            await Task.Delay(TimeSpan.FromSeconds(2));
            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[0] + ").png"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[0] + ").png"));

            int currentDiceResult = Result[0] + Result[0];

            int currentPosition = currentPlayer.currentPosition + 1;
            currentPlayer.ChangePlayerPosition(currentDiceResult); // changing position of the player, and make sure that there is not overflow
            // Delay for 2 seconds

            for (int i = 0; i < currentDiceResult; i++)
            {

                while (currentPosition > (currentPlayer.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= currentPlayer.PlayerPosition.GetLength(1);
                }
                if (currentPosition >= 8 && currentPosition < 16)
                {
                    currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + currentPlayer.name + "/RedCarRight.png"));
                    Grid.SetColumnSpan(currentPlayer.Img, 5);
                    Grid.SetRowSpan(currentPlayer.Img, 4);
                }
                else if (currentPosition >= 16 && currentPosition < 24)
                {
                    currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + currentPlayer.name + "/RedCarBackward.png"));
                    Grid.SetColumnSpan(currentPlayer.Img, 2);
                    Grid.SetRowSpan(currentPlayer.Img, 6);
                }
                else if (currentPosition >= 24 && currentPosition < 31)
                {
                    currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + currentPlayer.name + "/RedCarLeft.png"));
                    Grid.SetColumnSpan(currentPlayer.Img, 5);
                    Grid.SetRowSpan(currentPlayer.Img, 4);
                }
                else if (currentPosition >= 0 && currentPosition < 8)
                {
                    currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + currentPlayer.name + "/RedCarForward.png"));
                    Grid.SetColumnSpan(currentPlayer.Img, 2);
                    Grid.SetRowSpan(currentPlayer.Img, 6);
                }

                Grid.SetRow(currentPlayer.Img, currentPlayer.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(currentPlayer.Img, currentPlayer.PlayerPosition[1, currentPosition]);
                currentPosition++;
                await Task.Delay(TimeSpan.FromMilliseconds(200));

            }
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;
            ((Button)sender).IsEnabled = true;
            if (currentTimesPlay >= 3)
            {
                MoveToJail(currentPlayer);
                NextPlayer();
                currentPlayer = Player1;
            }
            else if(Result[0] == Result[0])
            {
                currentTimesPlay++;
                ((Button)sender).Visibility = Visibility.Visible;
            }  
            else
                NextPlayer();

            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
        }

        private void btnRoll_Dice_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void btnRoll_Dice_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }
    }
}
