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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BussinesTourProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        Thread mainThread = Thread.CurrentThread;
      //  Thread ThreadMovingPlayer = new Thread(new ParameterizedThreadStart(ChangePlayer4PositionAnimation));

        Random rnd = new Random();   
        Player Player1 = new Player(name: "Player1");
        Player Player2 = new Player(name: "Player2");
        Player Player3 = new Player(name: "Player3");
        Player Player4 = new Player(name: "Player4");


        int currentDiceResult = 4;

        Thread ThreadPlayer1Moving;

        public GamePage()
        {
            this.InitializeComponent();

            int[,] MatrixPositionPlayer1 = { { 10, 20, 30, 40, 50 }, { 5, 5, 5, 5, 5 } };
            Player1.SetPlayerPosition(MatrixPositionPlayer1);
            Player1.Img = imgPlayer;

            //int firstPlayer = rnd.Next(0, 4);

            ThreadPlayer1Moving = new Thread(() => ChangePlayerPositionAnimation(Player1, 4));
            // Thread ThreadPlayer2Moving = new Thread(() => ChangePlayerPositionAnimation(Player2, currentDiceResult));
            // Thread ThreadPlayer3Moving = new Thread(() => ChangePlayerPositionAnimation(Player3, currentDiceResult));
            // Thread ThreadPlayer4Moving = new Thread(() => ChangePlayerPositionAnimation(Player4, currentDiceResult));

            /*Thread[] ThreadsMovingPlayers = { new Thread(() => ChangePlayerPositionAnimation(Player1, currentDiceResult)),
                                new Thread(() => ChangePlayerPositionAnimation(Player2, currentDiceResult)),
                                new Thread(() => ChangePlayerPositionAnimation(Player3, currentDiceResult)),
                                new Thread(() => ChangePlayerPositionAnimation(Player4, currentDiceResult))};*/
        }

        public static void ChangePlayerPositionAnimation(Player player, int diceResult)
        {
            int currentPosition = player.currentPosition + 1;
            player.ChangePlayerPosition(diceResult); // changing position of the player, and make sure that there is not overflow
            for (int i = 0; i < diceResult; i++)
            {

                while (currentPosition > (player.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= player.PlayerPosition.GetLength(1);
                }
                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);

                currentPosition++;

                Thread.Sleep(100);
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
            ThreadPlayer1Moving.Start();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(imgPlayer, col++);
        }

        private void MovingPlayer()
        {
            
        }





    }
}
