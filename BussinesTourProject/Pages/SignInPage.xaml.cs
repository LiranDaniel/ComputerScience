﻿using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BussinesTourProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            this.InitializeComponent();
        }

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
        /// Just navigate you into the menu page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SliderVertical_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Check if the informations are correct and if it is then log in
        /// also display the appropiate pop up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if(Server.IsConnected(tboxMail, tboxPassword))
            {
                Frame.Navigate(typeof(MenuPage));

                var dialog = new MessageDialog("You have successfully logged in");
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("You have entered an invalid username or password");
                await dialog.ShowAsync();
            }


        }

        /// <summary>
        /// Just navigate you into the sign up page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }

        /// <summary>
        /// Just navigate you into the restore password page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestorePassword_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestorePasswordPage));
        }
    }
}

