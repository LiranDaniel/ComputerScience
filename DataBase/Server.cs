using DataBase.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private async static void PlayErrorSound()
        {
            MediaElement soundPlayer = new MediaElement();
            soundPlayer.Source = new Uri("ms-appx:///Assets/Music/sad-hamster.wav"); // Replace with your audio file path
            soundPlayer.Play();
        }

        public async static void SignUp(TextBox mailTestBlox,TextBox nameTestBlox, PasswordBox passwordTextBlox)
        {
            string email = mailTestBlox.Text;
            string password = passwordTextBlox.Password;
            string name = nameTestBlox.Text;
            // Connect to SQLite database
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
                            var dialog = new MessageDialog("User with this email already exists.");
                            PlayErrorSound();
                            await dialog.ShowAsync();
                            return; // Exit the method
                        }
                    }

                    // User doesn't exist, proceed with signup
                    string insertCommand = $"INSERT INTO [User] (UserName,UserPassword,UserMail) VALUES ('{name}','{password}','{email}')";
                    using (SqliteCommand command = new SqliteCommand(insertCommand, connection))
                        command.ExecuteNonQuery();

                }
                catch (SqliteException ex)
                {
                    // Handle SQLite exceptions
                    var dialog = new MessageDialog("SQLite Error: " + ex.Message);
                    PlayErrorSound();
                    await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    var dialog = new MessageDialog("Error: " + ex.Message);
                    PlayErrorSound();
                    await dialog.ShowAsync();
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
