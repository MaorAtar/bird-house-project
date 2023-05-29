using System;
using System.Windows.Forms;
using System.Media;
using System.Data.SqlClient;
using OxyPlot;
using OxyPlot.Series;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// The main view of the Bird House application.
    /// </summary>
    public partial class MainView : Form, IMainView
    {
        // Fields
        private const string connectionString = "Data Source=**TO-DO**;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
        private SoundPlayer sound;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            InitializePieChart();
            sound = new SoundPlayer(@"Resources\Music\background-sound.wav");
            bgMusicCheckBox.Checked = true;
            birdBtn.Click += delegate
            {
                ShowLadyGouldianFinchView?.Invoke(this, EventArgs.Empty);
                homePanel.Hide();
            };
            cageBtn.Click += delegate
            {
                ShowCageView?.Invoke(this, EventArgs.Empty);
                homePanel.Hide();
            };
            birdsLabel.Text = GetAmountOfBirdsInDB().ToString();
            cagesLabel.Text = GetAmountOfCagesInDB().ToString();
        }

        /// <summary>
        /// Occurs when the "Show Lady Gouldian Finch View" button is clicked.
        /// </summary>
        public event EventHandler ShowLadyGouldianFinchView;

        /// <summary>
        /// Occurs when the "Show Cage View" button is clicked.
        /// </summary>
        public event EventHandler ShowCageView;

        /// <summary>
        /// Handles the click event of the "Close" button.
        /// Closes the form and exits the application.
        /// </summary>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the background music button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgMusicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bgMusicCheckBox.Checked)
            {
                sound.PlayLooping();
            }
            else
            {
                sound.Stop();
            }
        }

        /// <summary>
        /// Shows the home panel when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeBtn_Click(object sender, EventArgs e)
        {
            homePanel.Show();
            InitializePieChart();
            birdsLabel.Text = GetAmountOfBirdsInDB().ToString();
            cagesLabel.Text = GetAmountOfCagesInDB().ToString();
        }

        /// <summary>
        /// Creates a Pie Chart that shows the gouldian species details in the data base.
        /// </summary>
        private void InitializePieChart()
        {
            var plotModel = new PlotModel { Title = "Gouldian Species" };
            var pieSeries = new PieSeries();

            plotModel.TitleFont = "Arial";
            plotModel.TitleFontSize = 18;
            pieSeries.Slices.Add(new PieSlice("American Gouldian", GetAmountOfAmericanInDB()));
            pieSeries.Slices.Add(new PieSlice("European Gouldian", GetAmountOfEuropeanInDB()));
            pieSeries.Slices.Add(new PieSlice("Australian Gouldian", GetAmountOfAustralianInDB()));
            pieSeries.InnerDiameter = 0.2;
            plotModel.Series.Add(pieSeries);
            plotView1.Model = plotModel;
        }

        /// <summary>
        /// Gets the amout of bird objects in the database.
        /// </summary>
        /// <returns>int</returns>
        private int GetAmountOfBirdsInDB()
        {
            int count = 0;
            string tableName = "LadyGouldianFinch";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the amout of cage objects in the database.
        /// </summary>
        /// <returns>int</returns>
        private int GetAmountOfCagesInDB()
        {
            int count = 0;
            string tableName = "Cage";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the amout of Gouldian American bird objects in the database.
        /// </summary>
        /// <returns>int</returns>
        private int GetAmountOfAmericanInDB()
        {
            int count = 0;
            string tableName = "LadyGouldianFinch";
            string speciesColumnName = "Species";
            string targetSpecies = "Gouldian American";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {speciesColumnName} = @species";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@species", targetSpecies);
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the amout of Gouldian European bird objects in the database.
        /// </summary>
        /// <returns>int</returns>
        private int GetAmountOfEuropeanInDB()
        {
            int count = 0;
            string tableName = "LadyGouldianFinch";
            string speciesColumnName = "Species";
            string targetSpecies = "Gouldian European";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {speciesColumnName} = @species";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@species", targetSpecies);
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the amout of Gouldian Australian bird objects in the database.
        /// </summary>
        /// <returns>int</returns>
        private int GetAmountOfAustralianInDB()
        {
            int count = 0;
            string tableName = "LadyGouldianFinch";
            string speciesColumnName = "Species";
            string targetSpecies = "Gouldian Australian";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {speciesColumnName} = @species";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@species", targetSpecies);
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
    }
}