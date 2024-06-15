using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using TheTriviaProject.Model;


namespace TheTriviaProject.ViewModel
{
    public class UserViewModel
    {
        private SharedViewModel _sharedViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
            LoadUsersFromDatabase(); // פעולה הקורא לטבלת המשתמשים
        }
        public void RefreshUsers()
        {
            LoadUsersFromDatabase(); //פעולה הקורא לטבלת המשתמשים
        }

        
        private void LoadUsersFromDatabase()
        {
            try
            {

                // Update the path to your actual database file
                string connectionString = @"Data Source=C:\Users\mikaw\source\repos\TheTriviaProject\Final.db;Version=3;";


                var usersList = new List<User>();

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users ";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersList.Add(new User
                                {
                                    Age = Convert.ToInt32(reader["Age"]),
                                    FirstName = Convert.ToString(reader["FirstName"]),
                                    LastName = Convert.ToString(reader["LastName"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    Password = Convert.ToString(reader["Password"]),
                                });
                            }
                        }
                    }
                }

                _sharedViewModel.UsersList = usersList; // Update the SharedViewModel's UsersList
                OnPropertyChanged(nameof(Users)); // Notify that the Users list has been updated
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading Users: {ex.Message}");
            }
        }


        public List<User> Users
        {
            get { return _sharedViewModel.UsersList; }
        }
    }
}
