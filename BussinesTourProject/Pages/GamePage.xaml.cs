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
using Windows.Security.Authentication.OnlineId;
using System.Threading.Tasks;
using Windows.UI.Core;
using System.Runtime.CompilerServices;
using Windows.Gaming.Input;
using System.Reflection.Metadata.Ecma335;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Xaml.Media.Animation;
using static BussinesTourProject.Classes.House;
using static BussinesTourProject.Classes.Player;
using System.Timers;
using Windows.Media.Core;
using Windows.Media.Playback;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BussinesTourProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private DispatcherTimer timerGame; // timer of the game
        private int secondsElapsed; // minute of the timer of the game
        private int minutes;        // seconds of the timer of the game

        /// <summary>
        /// Initiate the Components for the page
        /// </summary>
        public GamePage()
        {
            this.InitializeComponent();
            InitializeTimers();
        }

        /// <summary>
        /// Initiate the timers, adding to the timer function and defining the time to play
        /// </summary>
        private void InitializeTimers()
        {
            timerGame = new DispatcherTimer();
            timerGame.Interval = TimeSpan.FromSeconds(1); // Set interval to 1 second
            timerGame.Tick += TimerGame_Tick;
            timerGame.Start();

            GameManager.timerPlayers = new DispatcherTimer();
            GameManager.timerPlayers.Interval = TimeSpan.FromSeconds(15);
            GameManager.timerPlayers.Tick += TimerPlayers_Tick;
        }

        /// <summary>
        /// at every second this function is been calling and displays the time of the game,
        /// also when the times play is 20 mins then the game is end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerGame_Tick(object sender, object e)
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
            if (minutes == 20 && secondsElapsed == 0)
                GameOver();
        }

        /// <summary>
        /// when starting the timer after 5 seconds this function is calling,
        /// after that the function closing every opened UI. also stopping the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerPlayers_Tick(object sender, object e)
        {
            GameManager.UIBuyingHouseGrid.Visibility = Visibility.Collapsed;
            GameManager.UIBuyingStation.Visibility = Visibility.Collapsed;
            if (GameManager.UIJailOptions.Visibility == Visibility.Visible)
            {
                GameManager.UIJailOptions.Visibility = Visibility.Collapsed;
                GameManager.currentPlayer.turnsStackJail--;
            }
            GameManager.CheckIfDouble();
        }

        //Just navigate into different page
        private void GameOver()
        {
            Frame.Navigate(typeof(SignInPage));
        }


        /// <summary>
        /// This function is Initiating the data of a player from this page
        /// </summary>
        /// <param name="player"> refrence into a player object</param>
        /// <param name="imgPlayer"> refrence into the image of that player in the map</param>
        /// <param name="playerMatrixPositions"> refrence into the matrix of all the positions of the players</param>
        private static void InitPlayer(Player player, Image imgPlayer, int[,] playerMatrixPositions)
        {
            player.SetPlayerPosition(playerMatrixPositions);
            player.Img = imgPlayer;
            Grid.SetRow(player.Img, player.PlayerPosition[0, 0]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, 0]);
            player.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + player.playerNumber + "/" + player.imgName + "Forward.png"));
        }
        /*private void initPlayer(Player.state[] state, string name, string imgName, string playerNumber,
            int[,] matrixPositions, Image imgPlayer)
        {
           GameManager.
        }
        private static void InitPlayers()
        {
            GameManager.arrayPlayers =
            new Player[] {new Player(playerState: new state[4]{state.forward, state.right, state.backward, state.left}, 
            name:"Alexander Yanai", imgName:"TrackCar" , playerNumber:"Player1", matrixPlayerPosition: GameManager.MatrixPositionPlayer1),

            new Player(playerState: new state[4]{state.forward, state.right, state.backward,
            state.left}, name:"Daniel Shlomo", imgName:"YellowCar" , playerNumber:"Player2"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
            state.backward}, name:"Liran Daniel", imgName:"PurpleCar" , playerNumber:"Player3"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
                state.backward}, name:"Oleg woller", imgName:"BlueCar" , playerNumber:"Player4")};
        }
        */
        /// <summary>
        /// When you use your mouse and getting entered a buttons area than the function is Being called.
        /// This function is just changing the buttons image into some else image that show that you enterd the area
        /// also this function changing the mouse Cursor into hand shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayEnter = (Button)sender;
            ((Image)btnPlayEnter.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/UsingButtons/" + ((Image)btnPlayEnter.Content).Name.Replace("img", "") + " (1).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// When you use your mouse and leave the buttons area this function is being called
        /// This function is just changing the buttons image into some else image that show that you leaved the area
        /// also this function changing the mouse Cursor into hand shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button btnPlayExit = (Button)sender;
            ((Image)btnPlayExit.Content).Source = new BitmapImage(new Uri("ms-appx:/// " +
                "Assets/Buttons/UsingButtons/" + ((Image)btnPlayExit.Content).Name.Replace("img", "") + " (2).png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// When the pause button is being pressed the function is being called
        /// and its basickly just navigate you into the menupage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void InitMediaPlayers()
        {
            GameManager.SoundPlayer.IsLoopingEnabled = false;
            GameManager.SoundPlayer.Pause();
        }
        
        /// <summary>
        /// This function is Being called when you enter the page
        /// everthing it does is basickly just Initiate and definding few important things
        /// plyers Information, UI Grids and many other information on the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitMediaPlayers();

            GameManager.InitPlayers();
            InitPlayer(GameManager.arrayPlayers[0], imgPlayer, GameManager.MatrixPositionPlayer1);
            InitPlayer(GameManager.arrayPlayers[1], imgPlayer2, GameManager.MatrixPositionPlayer2);
            InitPlayer(GameManager.arrayPlayers[2], imgPlayer3, GameManager.MatrixPositionPlayer3);
            InitPlayer(GameManager.arrayPlayers[3], imgPlayer4, GameManager.MatrixPositionPlayer4);

            SetImageAndTextBlockForProperty();
            GameManager.UIBuyingHouseGrid = UIBuyingHouse;
            GameManager.UIJailOptions = UIJailOptions;
            GameManager.UIBuyingStation = UIBuyingStation;
            GameManager.UITax = UITax;
            GameManager.UIWorldChampion = UIWorldChampion;
            GameManager.UIWorldTour = UIWorldTour;
            GameManager.UIMessage = UIMessage;
            GameManager.UISellingProperty = UISellingProperty;
            GameManager.UICardGrid = GridCards;
            GameManager.ImgBuyingStation = imgBuyingStation;
            GameManager.arrayRadioButtonBuyingHouse[0] = optionHouse1RadioButton;
            GameManager.arrayRadioButtonBuyingHouse[1] = optionHouse2RadioButton;
            GameManager.arrayRadioButtonBuyingHouse[2] = optionHouse3RadioButton;
            GameManager.txtBlockBuyingHousePrice = txtBuyingPrice;
            GameManager.txtBlockTaxesPrice = txtTaxPrice;
            GameManager.txtBlockMessage = txtBlockMessage;
            GameManager.txtBlockTotalCostToPay = txtCostToPay;
            GameManager.txtCardGrid = txtCards;
            //GameManager.txtBlockBasic = txt
            SetPlayerTxtBlocks();
            GameManager.NextPlayer();
            GameManager.SetToggleState(false);
            Jail.txtRemaningRoundJail = txtRemaningRoundJail;
        }

        /// <summary>
        /// this function is definding the veriables that are saved in the GameManager class
        /// that we could get access to change the name of the player and their money
        /// </summary>
        private void SetPlayerTxtBlocks()
        {
            txtPlayer1Name.Text = GameManager.arrayPlayers[0].name;
            txtPlayer2Name.Text = GameManager.arrayPlayers[1].name;
            txtPlayer3Name.Text = GameManager.arrayPlayers[2].name;
            txtPlayer4Name.Text = GameManager.arrayPlayers[3].name;
            GameManager.arrayPlayers[0].txtMoney = txtPlayer1Money; 
            GameManager.arrayPlayers[1].txtMoney = txtPlayer2Money; 
            GameManager.arrayPlayers[2].txtMoney = txtPlayer3Money; 
            GameManager.arrayPlayers[3].txtMoney = txtPlayer4Money;
            GameManager.arrayPlayers[0].txtState = txtPlayer1Status;
            GameManager.arrayPlayers[1].txtState = txtPlayer2Status;
            GameManager.arrayPlayers[2].txtState = txtPlayer3Status;
            GameManager.arrayPlayers[3].txtState = txtPlayer4Status;     
        }

        /// <summary>
        /// Set the image and the priceOfRent of every square in the map
        /// </summary>
        private void SetImageAndTextBlockForProperty()
        {
            ((Property)GameManager.ArrayMap[1]).SetXamlObjects(imgGranadaHouse, txtRentGranada, tglbtnGranada);
            ((Property)GameManager.ArrayMap[2]).SetXamlObjects(imgSevilleHouse, txtRentSeville, tglbtnSeville);
            ((Property)GameManager.ArrayMap[3]).SetXamlObjects(imgMadridHouse, txtRentMadrid, tglbtnMadrid);
            ((Property)GameManager.ArrayMap[4]).SetXamlObjects(imgTrafficLight, txtRentTrafficLight, tglbtnTrafficLight);
            ((Property)GameManager.ArrayMap[5]).SetXamlObjects(imgHongKongHouse, txtRentHongKong, tglbtnHongKong);
            ((Property)GameManager.ArrayMap[6]).SetXamlObjects(imgBeijingHouse, txtRentBeijing, tglbtnBeijing);
            ((Property)GameManager.ArrayMap[7]).SetXamlObjects(imgShanghaiHouse, txtRentShanghai, tglbtnShanghai);

            ((Property)GameManager.ArrayMap[9]).SetXamlObjects(imgVeniceHouse, txtRentVenice, tglbtnVenice);
            ((Property)GameManager.ArrayMap[10]).SetXamlObjects(imgMilanHouse, txtRentMilan, tglbtnMilan);
            ((Property)GameManager.ArrayMap[11]).SetXamlObjects(imgRomeHouse, txtRentRome, tglbtnRome);
            ((Property)GameManager.ArrayMap[13]).SetXamlObjects(imgHamburgHouse, txtRentHamburg, tglbtnHamburg);
            ((Property)GameManager.ArrayMap[14]).SetXamlObjects(imgParking, txtRentParking, tglbtnParking);
            ((Property)GameManager.ArrayMap[15]).SetXamlObjects(imgBerlinHouse, txtRentBerlin, tglbtnBerlin);

            ((Property)GameManager.ArrayMap[17]).SetXamlObjects(imgLondonHouse, txtRentLondon, tglbtnLondon);
            ((Property)GameManager.ArrayMap[18]).SetXamlObjects(imgGasStation, txtRentGasStation, tglbtnGasStation);
            ((Property)GameManager.ArrayMap[19]).SetXamlObjects(imgSydneyHouse, txtRentSydney, tglbtnSydney);
            ((Property)GameManager.ArrayMap[21]).SetXamlObjects(imgChicagoHouse, txtRentChicago, tglbtnChicago);
            ((Property)GameManager.ArrayMap[22]).SetXamlObjects(imgLasVegasHouse, txtRentLasVegas, tglbtnLasVegas);
            ((Property)GameManager.ArrayMap[23]).SetXamlObjects(imgNewYorkHouse, txtRentNewYork, tglbtnNewYork);

            ((Property)GameManager.ArrayMap[25]).SetXamlObjects(imgStation, txtRentStation, tglbtnStation);
            ((Property)GameManager.ArrayMap[26]).SetXamlObjects(imgLyonHouse, txtRentLyon, tglbtnLyon);
            ((Property)GameManager.ArrayMap[27]).SetXamlObjects(imgParisHouse, txtRentParis, tglbtnParis);
            ((Property)GameManager.ArrayMap[29]).SetXamlObjects(imgOsakaHouse, txtRentOsaka, tglbtnOsaka);
            ((Property)GameManager.ArrayMap[31]).SetXamlObjects(imgTokyoHouse, txtRentTokyo, tglbtnTokyo);

        }

        /// <summary>
        /// moving the player into the jail Square
        /// </summary>
        /// <param name="player"></param>
        private async void MoveToJail(Player player)
        {
            GridJailCard.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2));
            GridJailCard.Visibility = Visibility.Collapsed;
            player.currentPosition = 8;
            player.ChangePlayerImageByEnumValue(1);

            Grid.SetRow(player.Img, player.PlayerPosition[0, player.currentPosition]);
            Grid.SetColumn(player.Img, player.PlayerPosition[1, player.currentPosition]);
            GameManager.IsDouble = false;
        }

        /// <summary>
        /// this function is being called when pressing the "Dice Roll" button.
        /// this function is basickly the "Main" function you can call it and everthing it does is to 
        /// roll the dice and calling other functions for landing dice result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnRoll_Dice_Click(object sender, RoutedEventArgs e)
        {
            GameManager.timerPlayers.Stop();
            GameManager.IsDouble = false;
            Player player = GameManager.currentPlayer;

            ((Button)sender).IsEnabled = false;
            ((Button)sender).Visibility = Visibility.Collapsed;
            imgDice1.Visibility = Visibility.Visible;
            imgDice2.Visibility = Visibility.Visible;
            int[] Result = GameManager.RollDice();
            GameManager.PlaySound("RollDiceSound.wav");
            await Task.Delay(TimeSpan.FromSeconds(1));
            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[0] + ").png"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[1] + ").png"));

            if (player.turnsStackJail != 0)
            {
                UIJailOptions.Visibility = Visibility.Visible;
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
                else
                    player.ChangePlayerImageByPosition(currentPosition);

                if (currentPosition == 0)
                {
                    player.amountOfMoney += 300_000;
                    player.txtMoney.Text = player.amountOfMoney.ToString("N0");
                }

                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);
                currentPosition++;
                await Task.Delay(TimeSpan.FromMilliseconds(350));

            }

            if (Result[0] == Result[1] && player.currentPosition != 8)
            {
                GameManager.currentTimesPlay++;
                GameManager.IsDouble = true;
            }
            else
                GameManager.IsDouble = false;

            ResetTheButtons(sender, e);

            GameManager.timerPlayers.Start();
            GameManager.Land();
        }

        /// <summary>
        /// Reset the image of the dice and make sure that roll dice button is being prepared to the next round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetTheButtons(object sender, RoutedEventArgs e)
        {
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;

            ((Button)sender).IsEnabled = true;
            ((Button)sender).Visibility = Visibility.Visible;

            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
        }

        /// <summary>
        /// When you use your mouse and getting entered a buttons area than the function is Being called.
        /// this function changing the mouse Cursor into hand shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PointerEnteredRegular(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// When you use your mouse and leave the buttons area this function is being called
        /// this function changing the mouse Cursor into hand shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PointerExitedRegular(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// When these function is being called that means that the current player tried to buy house,
        /// If he has enough money he will get that house, else nothing would happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuyHouse_Click(object sender, RoutedEventArgs e)
        {
            House currentHouse = (House)GameManager.ArrayMap[GameManager.currentPlayer.currentPosition];
            if (optionHouse1RadioButton.IsChecked == true)
            {
               if(GameManager.currentPlayer.amountOfMoney >= currentHouse.basicCostToBuy)
                    currentHouse.BuyProperty(1); 
               else
                    optionHouse1RadioButton.IsChecked = false;
            }
            else if (optionHouse2RadioButton.IsChecked == true)
            {
                if (GameManager.currentPlayer.amountOfMoney >= currentHouse.levelUpgradePrice + currentHouse.basicCostToBuy)
                    currentHouse.BuyProperty(2);
                else
                    optionHouse1RadioButton.IsChecked = false;

            }
            else if (optionHouse3RadioButton.IsChecked == true)
            {
                if (GameManager.currentPlayer.amountOfMoney >= (currentHouse.levelUpgradePrice * 2)+ currentHouse.basicCostToBuy)
                    currentHouse.BuyProperty(3);
                else
                    optionHouse1RadioButton.IsChecked = false;

            }
            
            UIBuyingHouse.Visibility = Visibility.Collapsed;
            GameManager.CheckIfDouble();
        }


        /// <summary>
        /// This function is being called when you are in jail and choosed the pay as option
        /// everything it does is basickly just makes you pay 200K dollars to leave the jail in the
        /// the current round, if you dont have the money then the error message will appear the you doesn't have eonugh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnJailPay_Click(object sender, RoutedEventArgs e)
        {
            if (GameManager.currentPlayer.amountOfMoney >= 200_000)
            {
                GameManager.PlaySound("Paying.wav");
                GameManager.currentPlayer.AmountOfMoneyChange(200_000);
                GameManager.currentPlayer.turnsStackJail = 0;
                UIJailOptions.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtJailErrorMoney.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// This function is being called when you are in jail and choose the roll dice as option
        /// everything it does is basickly just dice the roll and if there is a double result
        /// then in the next round you would be free
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnJailDiceRoll_Click(object sender, RoutedEventArgs e)
        {
            btnRoll_Dice.Visibility = Visibility.Collapsed;
            UIJailOptions.Visibility = Visibility.Collapsed;
            GameManager.currentPlayer.turnsStackJail--;

            imgDice1.Visibility = Visibility.Visible;
            imgDice2.Visibility = Visibility.Visible;
            int[] Result = GameManager.RollDice();
            await Task.Delay(TimeSpan.FromSeconds(2));
            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[0] + ").png"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/Dice(" + Result[1] + ").png"));
            GameManager.PlaySound("RollDiceSound.wav");
            await Task.Delay(TimeSpan.FromSeconds(1));

            if (Result[0] == Result[1])
                GameManager.currentPlayer.turnsStackJail = 0;
            GameManager.NextPlayer();
            btnRoll_Dice.Visibility = Visibility.Visible;

            imgDice1.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
            imgDice2.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Dice/DiceGif.gif"));
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// When you are in UI buying house and select and house then its display its price to buy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionHouse1RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            House currentHouse = (House)GameManager.ArrayMap[GameManager.currentPlayer.currentPosition];
            txtBuyingPrice.Text = $"Buy For: {currentHouse.basicCostToBuy.ToString("N0")}";
        }

        /// <summary>
        /// When you are in UI buying house and select and house then its display its price to buy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionHouse2RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            House currentHouse = (House)GameManager.ArrayMap[GameManager.currentPlayer.currentPosition];
            txtBuyingPrice.Text = $"Buy For: {(currentHouse.basicCostToBuy + currentHouse.levelUpgradePrice).ToString("N0")}";
        }

        /// <summary>
        /// When you are in UI buying house and select and house then its display its price to buy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionHouse3RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            House currentHouse = (House)GameManager.ArrayMap[GameManager.currentPlayer.currentPosition];
            txtBuyingPrice.Text = $"Buy For: {(currentHouse.basicCostToBuy + (currentHouse.levelUpgradePrice * 2)).ToString("N0")}";
        }

        /// <summary>
        /// this function is just buying for the current player the current station and then 
        /// display the UI of dicing roll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuyingStation_Click(object sender, RoutedEventArgs e)
        {
            ((Station)GameManager.ArrayMap[GameManager.currentPlayer.currentPosition]).BuyProperty();
            UIBuyingStation.Visibility = Visibility.Collapsed;
            GameManager.CheckIfDouble();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (GameManager.ToggleState)
            {
                // Deselect all other buttons if in radio button mode
                foreach (var child in (clickedButton.Parent as Panel).Children)
                {
                    if (child is ToggleButton button && button != clickedButton)
                    {
                        button.IsChecked = false;
                    }
                }
            }

            if(GameManager.IsSellingState)
                txtCurrentTotalAmountSelling.Text = $"{GameManager.CalculateTotalValueSelected()}$";
            
        }
        private  void btnSelecteChampionShip_Click(object sender, RoutedEventArgs e)
        {
            int positionSquare = GameManager.GetSquarePosition();
            GameManager.SetToggleState(false);
            GameManager.CheckIfDouble();
            if (positionSquare != -1)
            {
                positionSquare++;

                if (WorldChampion.PropertyHoldingWorldChampion != null)
                {
                    WorldChampion.PropertyHoldingWorldChampion.currentCostToPayRent = WorldChampion.PropertyHoldingWorldChampion.CalculatePayRentByLevel();

                    double normalCostToPayRent = WorldChampion.PropertyHoldingWorldChampion.currentCostToPayRent;
                    int timesDigits = 0;
                    while (normalCostToPayRent > 1000)
                    {
                        normalCostToPayRent = normalCostToPayRent / 1000;
                        timesDigits++;
                    }
                    if (timesDigits == 1)
                        WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{normalCostToPayRent}K";
                    else if (timesDigits == 2)
                        WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{normalCostToPayRent}M";
                    else
                        WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{normalCostToPayRent}";
                }
                WorldChampion.PropertyHoldingWorldChampion = GameManager.ArrayMap[positionSquare] as Property;

                // the property that have been selected the payrent price will be double 
                // into the current world champion value
                double newPrice = WorldChampion.PropertyHoldingWorldChampion.CalculatePayRentByLevel() * WorldChampion.WorldChampionTimes;
                WorldChampion.PropertyHoldingWorldChampion.currentCostToPayRent = (int)newPrice;

                int times = 0;
                while (newPrice > 1000)
                {
                    newPrice = newPrice / 1000;
                    times++;
                }
                if (times == 1)
                    WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{newPrice}K";
                else if (times == 2)
                    WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{newPrice}M";
                else
                    WorldChampion.PropertyHoldingWorldChampion.txtOfMoneyDisplayRent.Text = $"{newPrice}";

                WorldChampion.IncreaseWorldChampion();
                UIWorldChampion.Visibility = Visibility.Collapsed;

            }
            else
            {
                UIWorldChampion.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            UIWorldChampion.Visibility= Visibility.Collapsed;
            UIWorldTour.Visibility = Visibility.Collapsed;
            GameManager.NextPlayer();
        }

        private async void btnSelectSquareToFly_Click(object sender, RoutedEventArgs e)
        {

            int positionSquare = GameManager.GetSquarePosition();
          
            UIWorldTour.Visibility = Visibility.Collapsed;
            btnRoll_Dice.Visibility = Visibility.Collapsed;
            Player player = GameManager.currentPlayer;
            GameManager.SetToggleState(false);

            if (positionSquare == -1)
            {
                GameManager.NextPlayer();
                btnRoll_Dice.Visibility = Visibility.Visible;
                return;
            }
            
            positionSquare++;
            int currentPosition = player.currentPosition + 1;
            while (currentPosition != positionSquare + 1)
            {
                while (currentPosition > (player.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= player.PlayerPosition.GetLength(1);
                }
                if (currentPosition == 0)
                {
                    player.ChangePlayerImageByEnumValue(0);

                    player.amountOfMoney += 300_000;
                    player.txtMoney.Text = player.amountOfMoney.ToString("N0");
                }
                else if (currentPosition == 8)
                    player.ChangePlayerImageByEnumValue(1);
                else if (currentPosition == 16)
                    player.ChangePlayerImageByEnumValue(2);
                else if (currentPosition == 24)
                    player.ChangePlayerImageByEnumValue(3);
                else
                    player.ChangePlayerImageByPosition(currentPosition);

                Grid.SetRow(player.Img, player.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(player.Img, player.PlayerPosition[1, currentPosition]);
                currentPosition++;
                await Task.Delay(TimeSpan.FromMilliseconds(350));
            }
            player.currentPosition = positionSquare;
            btnRoll_Dice.Visibility = Visibility.Visible;
            GameManager.NextPlayer();
        }

        private void btnSellProperty_Click(object sender, RoutedEventArgs e)
        {
            foreach (object square in GameManager.arrayPlayers)
            {
                if (square is Property)
                {
                    if(((square as Property).ownerOfTheProperty == GameManager.currentPlayer) || ((square as Property).toggleButtonBlock.IsChecked == true))
                    {
                        (square as Property).SellProperty();
                    }
                }
            }



            UISellingProperty.Visibility = Visibility.Collapsed;
            GameManager.CheckIfDouble();
        }

        private void btnConfirmCard_Click(object sender, RoutedEventArgs e)
        {
            GridCards.Visibility = Visibility.Collapsed;
            GameManager.CheckIfDouble();

        }
    }
}
