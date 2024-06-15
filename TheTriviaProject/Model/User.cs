using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTriviaProject.Model
{
    public class User 
    {
        private string firstName;
        private string lastName;
        private string eMail;
        private int age;
        private string password;

        public string Email
        {
            get
            {
                return eMail;
            }
            set
            {
                eMail = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }


        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                // Check if the new value is different to avoid recursion
                if (age != value)
                {
                    age = value;
                    // Optionally, you can add additional logic here
                    // or raise property changed event if this is part of a data-bound object
                }
            }
        }






        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
