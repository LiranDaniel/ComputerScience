﻿using BussinesTourProject.Classes;
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
        private DispatcherTimer timer;
        private int secondsElapsed;
        int col = 3;

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
             ChangePlayerPositionAnimation(GameManager.currentPlayer, 1);
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
                                               0, 8, 8, 8, 8, 8, 8, 8,
                                               14, 22, 30, 38, 46, 54, 62, 70,
                                               94, 83, 83, 83, 83, 83, 83, 83},
                                               { 8, 8, 8, 8, 8, 8, 8, 8,
                                                 20, 36, 52, 68, 84, 100, 116, 132,
                                                 175, 169, 169, 169, 169, 169, 169, 169,
                                                 148, 132, 116, 100, 84, 68, 52, 36} };

            GameManager.NextPlayer();
            InitPlayer(GameManager.currentPlayer, imgPlayer, MatrixPositionPlayer1, "Player1") ;
        }
        

        private async void MoveToJail(Player player)
        {
            GridCards.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2));
            GridCards.Visibility = Visibility.Collapsed;
            player.currentPosition = 8;
            GameManager.currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + GameManager.currentPlayer.name + "/RedCarRight.png"));
            Grid.SetColumnSpan(GameManager.currentPlayer.Img, 6);
            Grid.SetRowSpan(GameManager.currentPlayer.Img, 3);

            Grid.SetRow(GameManager.currentPlayer.Img, GameManager.currentPlayer.PlayerPosition[0, player.currentPosition]);
            Grid.SetColumn(GameManager.currentPlayer.Img, GameManager.currentPlayer.PlayerPosition[1, player.currentPosition]);
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
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[1] + ").png"));

            int currentDiceResult = Result[0] + Result[1];  

            int currentPosition = GameManager.currentPlayer.currentPosition + 1;
            GameManager.currentPlayer.ChangePlayerPosition(currentDiceResult); // changing position of the player, and make sure that there is not overflow
            // Delay for 2 seconds

            for (int i = 0; i < currentDiceResult; i++)
            {

                while (currentPosition > (GameManager.currentPlayer.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= GameManager.currentPlayer.PlayerPosition.GetLength(1);
                }
                if (currentPosition >= 8 && currentPosition < 16)
                {
                    GameManager.currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + GameManager.currentPlayer.name + "/RedCarRight.png"));
                    Grid.SetColumnSpan(GameManager.currentPlayer.Img, 6);
                    Grid.SetRowSpan(GameManager.currentPlayer.Img, 3);
                }
                else if (currentPosition >= 16 && currentPosition < 24)
                {
                    GameManager.currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + GameManager.currentPlayer.name + "/RedCarBackward.png"));
                    Grid.SetColumnSpan(GameManager.currentPlayer.Img, 3);
                    Grid.SetRowSpan(GameManager.currentPlayer.Img, 5);
                }
                else if (currentPosition >= 24 && currentPosition < 31)
                {
                    GameManager.currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + GameManager.currentPlayer.name + "/RedCarLeft.png"));
                    Grid.SetColumnSpan(GameManager.currentPlayer.Img, 6);
                    Grid.SetRowSpan(GameManager.currentPlayer.Img, 3);
                }
                else if (currentPosition >= 0 && currentPosition < 8)
                {
                        GameManager.currentPlayer.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + GameManager.currentPlayer.name + "/RedCarForward.png"));
                    Grid.SetColumnSpan(GameManager.currentPlayer.Img, 3);
                    Grid.SetRowSpan(GameManager.currentPlayer.Img, 5);
                }

                Grid.SetRow(GameManager.currentPlayer.Img, GameManager.currentPlayer.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(GameManager.currentPlayer.Img, GameManager.currentPlayer.PlayerPosition[1, currentPosition]);
                currentPosition++;
                await Task.Delay(TimeSpan.FromMilliseconds(300));

            }
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;
            ((Button)sender).IsEnabled = true;
            if (GameManager.currentTimesPlay >= 3)
            {
                MoveToJail(GameManager.currentPlayer);
                GameManager.NextPlayer();
            }
            else if(Result[0] == Result[1])
            {
                GameManager.currentTimesPlay++;
                ((Button)sender).Visibility = Visibility.Visible;
            }  
            else
                GameManager.NextPlayer();

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
