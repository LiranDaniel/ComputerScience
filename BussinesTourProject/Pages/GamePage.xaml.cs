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
using Windows.Security.Authentication.OnlineId;
using System.Threading.Tasks;
using Windows.UI.Core;
using System.Runtime.CompilerServices;
using Windows.Gaming.Input;
using System.Reflection.Metadata.Ecma335;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Xaml.Media.Animation;
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
        private int minutes;

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

            if (secondsElapsed == 60)
            {
                minutes++;
                secondsElapsed = 0;
            }
            // Update UI with the elapsed time
            if (secondsElapsed < 10)
                UpdateUITextBlock.Text = $"Timer : {minutes}:0{secondsElapsed}";
            else 
                UpdateUITextBlock.Text = $"Timer : {minutes}:{secondsElapsed}";
            if (minutes == 1 && secondsElapsed == 20)
                GameOver();
        }
        private void GameOver()
        {
            Frame.Navigate(typeof(SignInPage));
        }

        private static void InitPlayer(Player player, Image imgPlayer, int[,] playerMatrixPositions)
        {
            player.SetPlayerPosition(playerMatrixPositions);
            player.Img = imgPlayer;
            Grid.SetRow(player.Img, player.PlayerPosition[0, 0]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, 0]);
            player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarForward.png"));
        }

        public static void ChangePlayerPositionAnimation(Player player, int diceResult)
        {
            int currentPosition = player.currentPosition + 1;
            player.ChangePlayerPosition(1); // changing position of the player, and make sure that there is not overflow
            for (int i = 0; i < 1; i++)
            {

                while (currentPosition > (player.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= player.PlayerPosition.GetLength(1);
                }
                if (currentPosition == 8)
                {
                    player.ChangePlayerImageByEnumValue(1);
                }
                else if (currentPosition > 8 && currentPosition < 16)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarRight.png"));
                    Grid.SetColumnSpan(player.Img, 6);
                    Grid.SetRowSpan(player.Img, 3);
                }
                else if (currentPosition == 16)
                    player.ChangePlayerImageByEnumValue(2);
                else if (currentPosition > 16 && currentPosition < 24)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarBackward.png"));
                    Grid.SetColumnSpan(player.Img, 3);
                    Grid.SetRowSpan(player.Img, 5);
                }
                else if (currentPosition == 24)
                    player.ChangePlayerImageByEnumValue(3);
                else if (currentPosition > 24 && currentPosition < 31)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarLeft.png"));
                    Grid.SetColumnSpan(player.Img, 6);
                    Grid.SetRowSpan(player.Img, 3);
                }
                else if (currentPosition == 0)
                    player.ChangePlayerImageByEnumValue(0);
                else if (currentPosition > 0 && currentPosition < 8)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarForward.png"));
                    Grid.SetColumnSpan(player.Img, 3);
                    Grid.SetRowSpan(player.Img, 5);
                }

                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);
                Task.Delay(5000);

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
            ChangePlayerPositionAnimation(GameManager.arrayPlayers[0], 1);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            ChangePlayerPositionAnimation(GameManager.arrayPlayers[1], 1);
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GameManager.InitPlayers();
            GameManager.NextPlayer();

            InitPlayer(GameManager.arrayPlayers[0], imgPlayer, GameManager.MatrixPositionPlayer1);
            InitPlayer(GameManager.arrayPlayers[1], imgPlayer2, GameManager.MatrixPositionPlayer2);
            InitPlayer(GameManager.arrayPlayers[2], imgPlayer3, GameManager.MatrixPositionPlayer3);
            InitPlayer(GameManager.arrayPlayers[3], imgPlayer4, GameManager.MatrixPositionPlayer4);

            SetImageAndTextBlockForProperty();
        }
        private void SetImageAndTextBlockForProperty()
        {
            GameManager.ArrayMap[1]

        }

        private async void MoveToJail(Player player)
        {
            GridCards.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2));
            GridCards.Visibility = Visibility.Collapsed;
            player.currentPosition = 8;
            player.ChangePlayerImageByEnumValue(1);

            Grid.SetRow(player.Img, player.PlayerPosition[0, player.currentPosition]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, player.currentPosition]);
        }



        private async void btnRoll_Dice_Click(object sender, RoutedEventArgs e)
        {
            Player player = GameManager.currentPlayer;

            ((Button)sender).IsEnabled = false;
            ((Button)sender).Visibility = Visibility.Collapsed;
            imgDice1.Visibility = Visibility.Visible;
            imgDice2.Visibility = Visibility.Visible;
            int[] Result = GameManager.RollDice();
            await Task.Delay(TimeSpan.FromSeconds(2));
            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[0] + ").png"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[1] + ").png"));

            if (player.turnsStackJail != 0)
            {
                if (Result[0] == Result[1])
                {
                    player.turnsStackJail = 0;
                }
                else
                {
                    player.turnsStackJail--;
                    GameManager.NextPlayer();
                    ResetTheButtons(sender, e);
                    return;
                }
            }
            if (GameManager.currentTimesPlay == 3 && (Result[0] == Result[1]))
            {
                MoveToJail(GameManager.currentPlayer);
                GameManager.NextPlayer();
                ResetTheButtons(sender, e);
                return;
            }

            int currentDiceResult = Result[0] + Result[1];

            int currentPosition = player.currentPosition + 1;
            player.ChangePlayerPosition(currentDiceResult); // changing position of the player, and make sure that there is not overflow
            // Delay for 2 seconds

            for (int i = 0; i < currentDiceResult; i++)
            {
                while (currentPosition > (player.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= player.PlayerPosition.GetLength(1);
                }
                if (currentPosition == 0)
                    player.ChangePlayerImageByEnumValue(0);
                else if (currentPosition == 8)
                    player.ChangePlayerImageByEnumValue(1);
                else if (currentPosition == 16)
                    player.ChangePlayerImageByEnumValue(2);
                else if (currentPosition == 24)
                    player.ChangePlayerImageByEnumValue(3);
                else if (currentPosition > 8 && currentPosition < 16)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarRight.png"));
                    Grid.SetColumnSpan(player.Img, 6);
                    Grid.SetRowSpan(player.Img, 3);
                }
                else if (currentPosition > 16 && currentPosition < 24)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarBackward.png"));
                    Grid.SetColumnSpan(player.Img, 3);
                    Grid.SetRowSpan(player.Img, 5);
                }
                else if (currentPosition > 24 && currentPosition < 31)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarLeft.png"));
                    Grid.SetColumnSpan(player.Img, 6);
                    Grid.SetRowSpan(player.Img, 3);
                }
                else if (currentPosition > 0 && currentPosition < 8)
                {
                    player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.imgName + "/RedCarForward.png"));
                    Grid.SetColumnSpan(player.Img, 3);
                    Grid.SetRowSpan(player.Img, 5);
                }

                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);
                currentPosition++;
                await Task.Delay(TimeSpan.FromMilliseconds(500));

            }

            if (Result[0] == Result[1])
                GameManager.currentTimesPlay++;
            else
                GameManager.NextPlayer();

            ResetTheButtons(sender, e);

        }

        private void ResetTheButtons(object sender, RoutedEventArgs e)
        {
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;

            ((Button)sender).IsEnabled = true;
            ((Button)sender).Visibility = Visibility.Visible;

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
