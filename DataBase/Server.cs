using DataBase.DataBaserManager;
using DataBase.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



namespace DataBase
{
    public static class Server
    {

        private static string dbPath = ApplicationData.Current.LocalFolder.Path;
        private static string connectString = "Filename=" + dbPath + "\\DBGame.db";

        
        // Method to play the sound for a specific duration
        static async void PlaySoundForDuration(TimeSpan duration)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/sad-hamster.wav")); // Replace with your audio file path
            mediaPlayer.Play();

            // Wait for the specified duration
            await Task.Delay(duration);

            // Stop the playback after the specified duration
            mediaPlayer.Pause();
            mediaPlayer.Dispose(); // Dispose the MediaPlayer instance
        }


        private async static void PopUpMessage(string message)
        {
            var dialog = new MessageDialog(message);
            PlaySoundForDuration(TimeSpan.FromSeconds(5));
            await dialog.ShowAsync();
        }

        public  static void SignUp(string email,string name,
            string password, string passwordConfirm)
        {
            // Connect to SQLite database

            if(password != passwordConfirm)
            {
                PopUpMessage("Password do not match");
                return;
            }
            else if (!CheckValidation.IsMailValid(email))
            {
                PopUpMessage("Your mail is not valid");
                return; // Exit the method
            }
            else if(!CheckValidation.IsPasswordValid(password))
            {
                PopUpMessage("Your password is not valid");
                return; // Exit the method
            }
            else if (name == "")
            {
                PopUpMessage("User name can not be null");
                return;
            }


            using (SqliteConnection connection = new SqliteConnection(connectString))
            {
                try
                {
                    connection.Open();

                    // Check if the user already exists
                    string query = "SELECT COUNT(*) FROM User WHERE UserMail = @Email";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // User already exists, show error message
                            PopUpMessage("User with this email already exists.");
                            return; // Exit the method
                        }
                    }

                    // User doesn't exist, proceed with signup
                    string insertCommand = $"INSERT INTO [User] (UserName,UserPassword,UserMail) VALUES ('{name}','{password}','{email}')";
                    using (SqliteCommand command = new SqliteCommand(insertCommand, connection))
                        command.ExecuteNonQuery();

                    PopUpMessage("Account have been created");

                }
                catch (SqliteException ex)
                {
                    // Handle SQLite exceptions
                    PopUpMessage("SQLite Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    PopUpMessage("Error: " + ex.Message);
                }
            }
        }



        public static bool IsConnected(TextBox mailTestBlox, PasswordBox passwordTextBlox)
        {
            string email = mailTestBlox.Text;
            string password = passwordTextBlox.Password;

            // Connect to SQLite database
            using (SqliteConnection connection = new SqliteConnection(connectString))
            {
                connection.Open();

                // Query to check if user exists and password is correct
                string query = "SELECT COUNT(*) FROM User WHERE UserMail = @Email AND UserPassword = @Password";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        // User authenticated successfully, proceed with your action
                        // For example, navigate to another page

                        return true;
                    }
                    else
                    {

                        return false;
                        // Invalid credentials, show error message to the user
                        // For example: ErrorMessageTextBlock.Text = "Invalid email or password.";
                    }
                }
            }
        }

        public static bool RestorePassword(string mail, string password, string confirmPassword) 
        {

            if ((password != confirmPassword) || (!CheckValidation.IsMailValid(mail)) || (!CheckValidation.IsPasswordValid(password)))
                return false;

            using (SqliteConnection connection = new SqliteConnection(connectString))
            {
                connection.Open();

                // Query to check if user exists and password is correct
                string query = "SELECT COUNT(*) FROM User WHERE UserMail = @Email";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", mail);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        // User authenticated successfully, proceed with your action
                        // For example, navigate to another page
                        query = "UPDATE User SET UserPassword = @NewPassword WHERE UserMail = @UserMail";
                        using (SqliteCommand commandSet = new SqliteCommand(query, connection))
                        {
                            commandSet.Parameters.AddWithValue("@UserMail", mail);
                            commandSet.Parameters.AddWithValue("@NewPassword", password);

                            commandSet.ExecuteNonQuery();

                        }
                        return true;
                    }
                    else
                    {

                        return false;
                        // Invalid credentials, show error message to the user
                        // For example: ErrorMessageTextBlock.Text = "Invalid email"
                    }
                }

                
            }
        }
        /*
        //הפעולה מבצעת שאילתה
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        public static int? ValidateUser(string userName, string userPassword)
        {
            string query = $"SELECT UserId FROM [User] WHERE UserName='{userName}' AND UserPassword='{userPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return null;
            }
        }

        public static User AddNewUser(string name, string password, string mail)
        {
            int? userId = ValidateUser(name, password); // בדיקה אם המשתמש כבר נמצא במאגר
            if (userId != null) // המשתמש כבר קיים - לשלוח להתחברות במקום הרשמה
                return null;
            // אם המשכנו, זאת אומרת המשתמש בעל הנתונים שהזין לא נמצא במאגר
            //User מסיפים את נתוניו האישיים של המשתמש שהזין לטבלת 
            string query = $"INSERT INTO [User] (UserName,UserPassword,UserMail) VALUES ('{name}','{password}','{mail}')";
            Execute(query);
            userId = ValidateUser(name, password); //User של המשתמש לאחר הוספתו לטבלת UserId קבלת 
                                                   //-------------------------------------------
            AddGameData(userId.Value); //הוספת נתוני ברירת מחדל 
            AddUserProduct(userId.Value);
            return GetUser(userId.Value);
        }

        */
    }
}
