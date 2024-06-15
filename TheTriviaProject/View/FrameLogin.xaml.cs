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
    /// Interaction logic for FrameLogin.xaml
    /// </summary>
    public partial class FrameLogin : Page
    {
      
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public FrameLogin()
        {
            InitializeComponent();
            _sharedViewModel = new SharedViewModel();


            _userViewModel = new UserViewModel(_sharedViewModel);

            DataContext = _userViewModel;
            mainFrame.Navigate(new Login(_sharedViewModel));


        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {


        }
    }
}
