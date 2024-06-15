using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheTriviaProject.ViewModel;

namespace TheTriviaProject.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
       
        
            private SharedViewModel _sharedViewModel;
            private UserViewModel _userViewModel;
            private MediaPlayer mediaPlayer = new MediaPlayer();

            public Login(SharedViewModel sharedViewModel)
            {
                InitializeComponent();

                _sharedViewModel = sharedViewModel;
               
            }

            private void SignUpBtn_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    // Handle sign-up or navigate to the registration window
                    Register registerPage = new Register(_sharedViewModel);
                    NavigationService.Navigate(registerPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
            {
                // Clear the text when the TextBox gets focus
                UserName.Text = "";
            }
            private void Page_Loaded(object sender, RoutedEventArgs e)
            {
                // Set focus to the UserName TextBox
                UserName.Focus();
            }




            private void LoginBtn_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    // Get the entered email and password
                    string enteredEmail = UserName.Text;
                    string enteredPassword = Passwordclass.Password;
                    bool isAdmin = false;

                    // Updated connection string with your database path
                    string connectionString = @"Data Source=C:\Users\mikaw\source\repos\TheTriviaProject\Final.db;Version=3;";
                    bool userFound = false; // Flag to indicate if user was found

                    using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // SQL command to search for user
                        string sql = "SELECT * FROM User WHERE Email = @Email AND Password = @Password LIMIT 1";
                        using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Email", enteredEmail);
                            command.Parameters.AddWithValue("@Password", enteredPassword);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read()) // If a user is found
                                {
                                    userFound = true;
                                    isAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                                    // Set the flag to true
                                }
                            }
                        }
                    }

                    // Check if a match was found and load the main page
                    if (userFound)
                    {

                        // User found, create and show the MainWithFrame window

                        if (isAdmin)
                        {
                            Admin adminPage = new Admin();
                            NavigationService.Navigate(adminPage);

                        }
                        else
                        {
                            MainWindow mainPage = new MainWindow();
                            // Assuming _sharedViewModel is already defined
                            NavigationService.Navigate(mainPage);

                        }
                        // Now, navigate the Frame within MainWithFrame to MainPage



                    }

                    else
                    {
                        // No match found, display an error message
                        MessageBox.Show("Invalid email or password. Please try again." + enteredEmail);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

        private void Loginclick(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
