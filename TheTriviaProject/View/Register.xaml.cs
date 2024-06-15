using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public Register(SharedViewModel SharedViewModels)
        {
            InitializeComponent();
            _sharedViewModel = SharedViewModels;
        }

        private void AddUserButton(object sender, RoutedEventArgs e)
        {
            if (!AreAllValuesFilled())
            {
                MessageBox.Show("Please fill all the fields & make sure passwords are equal");
                return;
            }
            else
            {
                try
                {
                    // Gather user input
                    string Email = EmaileClass.Text;
                    string FirstName = FirstNameClass.Text;
                    string LastName = SecomdNameClass.Text;
                    string Password = Passwordclass.Text;
                    int Age = int.Parse(AgeClass.Text);

                    // Database connection string
                    string connectionString = @"Data Source=C:\Users\mikaw\source\repos\TheTriviaProject\Final.db;Version=3;";
                  


                    // Insert data into the database
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Users (firstName, lastName, age, password, email,IsAdmin) VALUES (@FirstName, @LastName, @Age, @Password, @Email,@IsAdmin)";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", FirstName);
                            command.Parameters.AddWithValue("@LastName", LastName);
                            command.Parameters.AddWithValue("@Age", Age);
                            command.Parameters.AddWithValue("@Password", Password);
                            command.Parameters.AddWithValue("@Email", Email);
                            command.Parameters.AddWithValue("@IsAdmin", 0);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("User registered successfully!");

                    // Navigate back
                    if (NavigationService != null && NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Navigation service is unavailable or can't go back.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private bool AreAllValuesFilled()
        {
            if (string.IsNullOrEmpty(EmaileClass.Text) || string.IsNullOrEmpty(FirstNameClass.Text)
                || string.IsNullOrEmpty(SecomdNameClass.Text) || string.IsNullOrEmpty(Passwordclass.Text))
            {
                return false;
            }
            return true;
        }
        private void RestoreDefaultText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "FirstNameClass":
                        textBox.Text = "First Name";

                        break;
                    case "Passwordclass":
                        textBox.Text = "Password";
                        break;
                    case "SecomdNameClass":
                        textBox.Text = "Second Name";
                        break;
                    case "EmaileClass":
                        textBox.Text = "Email";
                        break;
                    case "AgeClass":
                        textBox.Text = "Age";
                        break;
                    case "ChildsFirstNameClass":
                        textBox.Text = "Child's First Name";
                        break;
                    case "ChildsSecondNameClass":
                        textBox.Text = "Child's Second Name";
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearTextBoxText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = string.Empty;
        }

    }
}
