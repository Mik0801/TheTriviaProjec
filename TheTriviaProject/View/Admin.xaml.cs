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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private SQLiteConnection connection;
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public Admin(SharedViewModel _sharedViewModel)
        {
            InitializeComponent();
            this._sharedViewModel = _sharedViewModel;
        }




        private void MakeUserAdminClick(object sender, RoutedEventArgs e)
        {
            MakeAdmin newreg = new MakeAdmin(_sharedViewModel);
            NavigationService.Navigate(newreg);
        }

        private void AddQuestionClick(object sender, RoutedEventArgs e)
        {
            AdddQuestions newreg = new AdddQuestions(_sharedViewModel);
            NavigationService.Navigate(newreg);
        }

        private void PlayGameClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWithFrame = new MainWindow();
            mainWithFrame.Show();

        }
        public Admin()
        {
            InitializeComponent();
        }
    }
}
