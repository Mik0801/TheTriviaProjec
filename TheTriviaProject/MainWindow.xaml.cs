using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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

namespace TheTriviaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;
        private int currentQuestionIndex = 0;
        private int totalQuestions = 5;
        private HashSet<string> askedQuestions = new HashSet<string>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadQuestions();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "Data Source=C:\\Users\\mikaw\\source\\repos\\TheTriviaProject\\Final.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            dataAdapter = new SQLiteDataAdapter("SELECT * FROM GeneralKnowledgeQuestions", connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
        }

        private void LoadQuestions()
        {
            if (currentQuestionIndex < totalQuestions)
            {
                if (dataTable.Rows.Count > 0)
                {
                    // Shuffle the rows of the DataTable
                    ShuffleDataTableRows(dataTable);

                    DataRow row = dataTable.Rows[currentQuestionIndex];
                    question.Text = row["QuestionText"].ToString();
                    string[] options = new string[4];
                    options[0] = row["CorrectAnswer"].ToString();
                    options[1] = row["IncorrectAnswer1"].ToString();
                    options[2] = row["IncorrectAnswer2"].ToString();
                    options[3] = row["IncorrectAnswer3"].ToString();

                    // Shuffle the options array
                    RandomizeOptions(options);

                    // Assign shuffled options to answer buttons
                    ans1.Content = options[0];
                    ans2.Content = options[1];
                    ans3.Content = options[2];
                    ans4.Content = options[3];

                    currentQuestionIndex++;
                }
                else
                {
                    MessageBox.Show("No questions available");
                    this.Close();
                    // Handle the case when no questions are available in the DataTable
                }
            }
            else
            {
                MessageBox.Show("Trivia Ended");
                this.Close();
            }
        }

        // Method to shuffle the rows of the DataTable
        private void ShuffleDataTableRows(DataTable dataTable)
        {

            Random rnd = new Random();
            int n = dataTable.Rows.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                DataRow tempRow = dataTable.NewRow();
                tempRow.ItemArray = dataTable.Rows[k].ItemArray;
                dataTable.Rows[k].ItemArray = dataTable.Rows[n].ItemArray;
                dataTable.Rows[n].ItemArray = tempRow.ItemArray;
            }
        }
        public bool IsExist(string[] arr, string query)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == query)
                    return true;

            }
            return false;
        }



        private void RandomizeOptions(string[] options)
        {
            Random rnd = new Random();
            for (int i = options.Length - 1; i > 0; i--)
            {
                int index = rnd.Next(i + 1);
                // Swap elements
                string temp = options[i];
                options[i] = options[index];
                options[index] = temp;
            }
        }




        // Define a default background color for the button
        // Declare score variable outside of the method
        int score = 0;

        private void CheckAnswerAndLoadNext(object sender, RoutedEventArgs e)
        {
            scoreTextBlock.Text = score.ToString();
            Button clickedButton = sender as Button;
            string selectedAnswer = clickedButton.Content.ToString();
            DataRow currentQuestionRow = dataTable.Rows[currentQuestionIndex - 1];
            string correctAnswer = currentQuestionRow["CorrectAnswer"].ToString();

            if (selectedAnswer == correctAnswer)
            {
                // Increase score by 20 for correct answer
                score += 20;
                scoreTextBlock.Text = score.ToString();

                MessageBoxResult result = MessageBox.Show("Correct", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    question.Text = "";
                    LoadQuestions();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Incorrect", "Result", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    question.Text = "";
                    LoadQuestions();
                }
            }
        }
        private void CorrectAnswerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

    }
}