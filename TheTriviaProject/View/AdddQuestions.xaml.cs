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
    /// Interaction logic for AdddQuestions.xaml
    /// </summary>
    public partial class AdddQuestions : Page
    {
        public AdddQuestions()
        {
            InitializeComponent();
        }

        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public AdddQuestions(SharedViewModel _sharedViewModel)
        {
            InitializeComponent();
            this._sharedViewModel = _sharedViewModel;

        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            string questionText = QuestionTextBox.Text;
            string correctAnswer = CorrectAnswerTextBox.Text;
            string wrongAnswer1 = WrongAnswer1TextBox.Text;
            string wrongAnswer2 = WrongAnswer2TextBox.Text;
            string wrongAnswer3 = WrongAnswer3TextBox.Text;
            string catagory = CategoryTextBox.Text;
            // Your connection string to the SQLite database
            string connectionString = @"Data Source=C:\Users\mikaw\source\repos\TheTriviaProject\Final.db;Version=3;";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Inserting data into the database
                    string sql = "INSERT INTO GeneralKnowledgeQuestions (Category, QuestionText, CorrectAnswer, IncorrectAnswer1,IncorrectAnswer2,IncorrectAnswer3) VALUES (@Category, @QuestionText,@CorrectAnswer, @IncorrectAnswer1, @IncorrectAnswer2,@IncorrectAnswer3)";
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {





                        int result = command.ExecuteNonQuery();
                        if (result > 0 && questionText != "Enter question text" && correctAnswer != "Enter correct answer" && wrongAnswer1 != "Enter wrong answer 1" && wrongAnswer2 != "Enter wrong answer 2" && wrongAnswer3 != "Enter wrong answer 3" && catagory != "Enter category")
                        {
                            if (wrongAnswer1 != wrongAnswer2 && wrongAnswer1 != wrongAnswer3)
                            {
                                command.Parameters.AddWithValue("@Category", catagory);
                                command.Parameters.AddWithValue("@QuestionText", questionText);
                                command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);
                                command.Parameters.AddWithValue("@IncorrectAnswer1", wrongAnswer1);
                                command.Parameters.AddWithValue("@IncorrectAnswer2", wrongAnswer2);
                                command.Parameters.AddWithValue("@IncorrectAnswer3", wrongAnswer3);
                                MessageBox.Show("Question added successfully!");
                                // Clearing the text boxes after successful insertion
                                QuestionTextBox.Text = "Enter question text";
                                CorrectAnswerTextBox.Text = "Enter correct answer";
                                WrongAnswer1TextBox.Text = "Enter wrong answer 1";
                                WrongAnswer2TextBox.Text = "Enter wrong answer 2";
                                WrongAnswer3TextBox.Text = "Enter wrong answer 3";
                                NavigationService.GoBack();

                            }


                        }
                        else
                        {

                            MessageBox.Show("Please enter all the infrometion for the question.");

                            MessageBox.Show("Failed to add question.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Controls.TextBox)sender).Text = string.Empty;
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            var textBox = (System.Windows.Controls.TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = (string)textBox.Tag;
            }
        }

        private void GObacktomain(object sender, RoutedEventArgs e)
        {
            Login newreg = new Login(_sharedViewModel);
            NavigationService.Navigate(newreg);
        }

      
    }
}
