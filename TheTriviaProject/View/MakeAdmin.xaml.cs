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
    /// Interaction logic for MakeAdmin.xaml
    /// </summary>
    public partial class MakeAdmin : Page
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public MakeAdmin(SharedViewModel _sharedViewModel)
        {
            InitializeComponent();
            this._sharedViewModel = _sharedViewModel;
            DataContext = _sharedViewModel;
        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(email))
            {
                string connectionString = @"Data Source=C:\Users\mikaw\source\repos\TheTriviaProject\Final.db;Version=3;";

                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "UPDATE Users SET IsAdmin = 1 WHERE Email = @Email";
                        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Email", email);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"User with email {email} is now an admin.");
                                EmailTextBox.Clear();
                                NavigationService.GoBack();
                            }
                            else
                            {
                                MessageBox.Show($"No user found with email {email}.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter an email.");
            }
        }

        private void GObacktomain(object sender, RoutedEventArgs e)
        {
            Login newreg = new Login(_sharedViewModel);
            NavigationService.Navigate(newreg);
        }
    }
}
