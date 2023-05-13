using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Form for displaying and adding data for Lady Gouldian Finch.
    /// </summary>
    public partial class LadyGouldianFinchDataView : Form
    {
        // Constructors
        public LadyGouldianFinchDataView(int serialNumber, string species, string subSpecies,
    string hatchDate, string gender, string cageNumber, int fSerialNumber, int mSerialNumber,
    string headColor, string breastColor, string bodyColor)
        {
            InitializeComponent();
            showChickTable(serialNumber);
            // Fill the current bird details
            serialBox.Text = serialNumber.ToString();
            speciesBox.Text = species;
            subSpeciesBox.Text = subSpecies;
            hatchDateBox.Text = hatchDate;
            genderBox.Text = gender;
            cageNumberBox.Text = cageNumber;
            fSerialBox.Text = fSerialNumber.ToString();
            mSerialBox.Text = mSerialNumber.ToString();
            headColorBox.Text = headColor;
            breastColorBox.Text = breastColor;
            bodyColorBox.Text = bodyColor;
            // Fill the Chick inherited details
            speciesBox2.Text = species;
            subSpeciesBox2.Text = subSpecies;
            cageNumberBox2.Text = cageNumber;
            fSerialBox2.Text = serialNumber.ToString();
            // Add Chick
            addChickBtn.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                RefreshBirdList();
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Add new bird";
            };
            // Cancel
            cancelBtn2.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            };
            tabControl1.TabPages.Remove(tabPage2);
        }

        // Getters & Setters
        public string LadyGouldianFinchSerialNumber { get => serialBox2.Text; set => serialBox2.Text = value; }
        public string LadyGouldianFinchSpecies { get => speciesBox2.Text; set => speciesBox2.Text = value; }
        public string LadyGouldianFinchSubSpecies { get => subSpeciesBox2.Text; set => subSpeciesBox2.Text = value; }
        public string LadyGouldianFinchHatchDate { get => hatchDateBox2.Text; set => hatchDateBox2.Text = value; }
        public string LadyGouldianFinchGender { get => genderComboBox2.Text.ToString(); set => genderComboBox2.Text = value; }
        public string LadyGouldianFinchCageNumber { get => cageNumberBox2.Text; set => cageNumberBox2.Text = value; }
        public int LadyGouldianFinchFSerialNumber { get => Convert.ToInt32(fSerialBox2.Text); set => fSerialBox2.Text = Convert.ToString(value); }
        public int LadyGouldianFinchMSerialNumber { get => Convert.ToInt32(mSerialBox2.Text); set => mSerialBox2.Text = Convert.ToString(value); }

        // Events
        public event EventHandler AddNewEvent;
        public event EventHandler CancelEvent;

        /// <summary>
        /// Populates the DataGridView with the chick table data for the given serial number.
        /// </summary>
        /// <param name="serialNumber">The serial number to filter the chick table.</param>
        private void showChickTable(int serialNumber)
        {
            SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;
Integrated Security=True;");
            connectionString.Open();
            string query = "Select * From LadyGouldianFinch WHERE F_Serial_Number = @serial_number Or M_Serial_Number = @serial_number";
            SqlCommand command = new SqlCommand(query, connectionString);
            command.Parameters.AddWithValue("@serial_number", serialNumber);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView.DataSource = dt;
            connectionString.Close();
        }

        /// <summary>
        /// Event handler for the Save button click event. Saves the data of a new Lady Gouldian Finch.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void saveBtn2_Click(object sender, EventArgs e)
        {
            SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;
Integrated Security=True;");
            using (var connection = connectionString)
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into LadyGouldianFinch values (@species, @sub_species, @hatch_date, @gender, " +
                    "@cage_number, @f_serial_number, @m_serial_number, @head_color, @breast_color, @body_color)";
                command.Parameters.Add("@species", SqlDbType.NVarChar).Value = LadyGouldianFinchSpecies;
                command.Parameters.Add("@sub_species", SqlDbType.NVarChar).Value = LadyGouldianFinchSubSpecies;
                command.Parameters.Add("@hatch_date", SqlDbType.NVarChar).Value = LadyGouldianFinchHatchDate;
                command.Parameters.Add("@gender", SqlDbType.NVarChar).Value = LadyGouldianFinchGender;
                command.Parameters.Add("@cage_number", SqlDbType.NVarChar).Value = LadyGouldianFinchCageNumber;
                command.Parameters.Add("@f_serial_number", SqlDbType.Int).Value = LadyGouldianFinchFSerialNumber;
                command.Parameters.Add("@m_serial_number", SqlDbType.Int).Value = LadyGouldianFinchMSerialNumber;
                command.Parameters.Add("@head_color", SqlDbType.NVarChar).Value = ChickHeadColor();
                command.Parameters.Add("@breast_color", SqlDbType.NVarChar).Value = ChickBreastColor();
                command.Parameters.Add("@body_color", SqlDbType.NVarChar).Value = ChickBodyColor();
                command.ExecuteNonQuery();
            }
            RefreshBirdList();
            Close();
        }

        /// <summary>
        /// Determines the head color of a Lady Gouldian Finch using CalcChickHeadColor function.
        /// </summary>
        /// <returns>The head color of the chick.</returns>
        private string ChickHeadColor()
        {
            string f_head_color = null;
            string m_head_color = null;

            string connection = "Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
            string query1 = "SELECT Head_Color FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";
            string query2 = "SELECT Head_Color FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command1 = new SqlCommand(query1, sqlConnection))
                {
                    command1.Parameters.AddWithValue("@f_serial_number", LadyGouldianFinchFSerialNumber);
                    sqlConnection.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        f_head_color = reader1.GetString(0);
                    }
                    reader1.Close();
                }
                using (SqlCommand command2 = new SqlCommand(query2, sqlConnection))
                {
                    command2.Parameters.AddWithValue("@m_serial_number", LadyGouldianFinchMSerialNumber);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        reader2.Read();
                        m_head_color = reader2.GetString(0);
                    }
                    reader2.Close();
                }
                sqlConnection.Close();
            }
            return CalcChickHeadColor(f_head_color, m_head_color);
        }

        /// <summary>
        /// Calculates the head color of a Lady Gouldian Finch chick based on the head colors of the parents and the chick's gender.
        /// </summary>
        /// <param name="parent1HeadColor">The head color of the first parent.</param>
        /// <param name="parent2HeadColor">The head color of the second parent.</param>
        /// <param name="chickGender">The gender of the chick.</param>
        /// <returns>The head color of the chick.</returns>
        private string CalcChickHeadColor(string f_head_color, string m_head_color)
        {
            string chick_gender = LadyGouldianFinchGender;
            if((f_head_color == "Red" && m_head_color == "Red") || 
                (f_head_color == "Red" && m_head_color == "Black") ||
                (f_head_color == "Red" && m_head_color == "Orange") ||
                (f_head_color == "Black" && m_head_color == "Red" && chick_gender == "Male") ||
                (f_head_color == "Black" && m_head_color == "Orange" && chick_gender == "Male") ||
                (f_head_color == "Orange" && m_head_color == "Red") ||
                (f_head_color == "Orange" && m_head_color == "Black"))
            {
                return "Red";
            }
            else if ((f_head_color == "Black" && m_head_color == "Black") || 
                (f_head_color == "Black" && m_head_color == "Red" && chick_gender == "Female") ||
                (f_head_color == "Black" && m_head_color == "Orange" && chick_gender == "Female"))
            {
                return "Black";
            }
            else if (f_head_color == "Orange" && m_head_color == "Orange")
            {
                return "Orange";
            }
            return f_head_color;
        }

        /// <summary>
        /// Calculates the breast color of a Lady Gouldian Finch chick using CalcChickBreastColor function.
        /// </summary>
        private string ChickBreastColor()
        {
            string f_breast_color = null;
            string m_breast_color = null;

            string connection = "Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
            string query1 = "SELECT Breast_Color FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";
            string query2 = "SELECT Breast_Color FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command1 = new SqlCommand(query1, sqlConnection))
                {
                    command1.Parameters.AddWithValue("@f_serial_number", LadyGouldianFinchFSerialNumber);
                    sqlConnection.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        f_breast_color = reader1.GetString(0);
                    }
                    reader1.Close();
                }
                using (SqlCommand command2 = new SqlCommand(query2, sqlConnection))
                {
                    command2.Parameters.AddWithValue("@m_serial_number", LadyGouldianFinchMSerialNumber);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        reader2.Read();
                        m_breast_color = reader2.GetString(0);
                    }
                    reader2.Close();
                }
                sqlConnection.Close();
            }
            return CalcChickBreastColor(f_breast_color, m_breast_color);
        }

        /// <summary>
        /// Calculates the breast color of a Lady Gouldian Finch chick based on the breast colors of the parents and the chick's gender.
        /// </summary>
        /// <param name="f_breast_color">The breast color of the first parent.</param>
        /// <param name="m_breast_color">The breast color of the second parent.</param>
        /// <returns>The breast color of the chick.</returns>
        private string CalcChickBreastColor(string f_breast_color, string m_breast_color)
        {
            if ((f_breast_color == "Purple" && m_breast_color == "Purple") || 
                (f_breast_color == "Purple" && m_breast_color == "Lilac") ||
                (f_breast_color == "Purple" && m_breast_color == "White") ||
                (f_breast_color == "Lilac" && m_breast_color == "Purple") ||  
                (f_breast_color == "White" && m_breast_color == "Purple"))
            {
                return "Purple";
            }
            else if((f_breast_color == "Lilac" && m_breast_color == "Lilac") || 
                (f_breast_color == "Lilac" && m_breast_color == "White") || 
                (f_breast_color == "White" && m_breast_color == "Lilac"))
            {
                return "Lilac";
            }
            else if (f_breast_color == "White" && m_breast_color == "White")
            {
                return "White";
            }
            return f_breast_color;
        }

        /// <summary>
        /// Calculates the body color of a Lady Gouldian Finch using CalcChickBodyColor function.
        /// </summary>
        private string ChickBodyColor()
        {
            string f_body_color = null;
            string m_body_color = null;

            string connection = "Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
            string query1 = "SELECT Body_Color FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";
            string query2 = "SELECT Body_Color FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command1 = new SqlCommand(query1, sqlConnection))
                {
                    command1.Parameters.AddWithValue("@f_serial_number", LadyGouldianFinchFSerialNumber);
                    sqlConnection.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        f_body_color = reader1.GetString(0);
                    }
                    reader1.Close();
                }
                using (SqlCommand command2 = new SqlCommand(query2, sqlConnection))
                {
                    command2.Parameters.AddWithValue("@m_serial_number", LadyGouldianFinchMSerialNumber);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        reader2.Read();
                        m_body_color = reader2.GetString(0);
                    }
                    reader2.Close();
                }
                sqlConnection.Close();
            }
            return CalcChickBodyColor(f_body_color, m_body_color);
        }

        /// <summary>
        /// Calculates the body color of a Lady Gouldian Finch chick based on the body colors of the parents and the chick's gender.
        /// </summary>
        /// <param name="f_body_color">The body color of the first parent.</param>
        /// <param name="m_body_color">The body color of the second parent.</param>
        /// <returns>The body color of the chick.</returns>
        private string CalcChickBodyColor(string f_body_color, string m_body_color)
        {
            string chick_gender = LadyGouldianFinchGender;
            
            if ((f_body_color == "Green" && m_body_color == "Green") ||
                (f_body_color == "Green" && m_body_color == "Yellow") ||
                (f_body_color == "Green" && m_body_color == "Blue") ||
                (f_body_color == "Green" && m_body_color == "Silver") ||
                (f_body_color == "Blue" && m_body_color == "Green") ||
                (f_body_color == "Blue" && m_body_color == "Yellow") ||
                (f_body_color == "Blue" && m_body_color == "Silver") ||
                (f_body_color == "Yellow" && m_body_color == "Green" && chick_gender == "Male") ||
                (f_body_color == "Yellow" && m_body_color == "Blue" && chick_gender == "Male") ||
                (f_body_color == "Yellow" && m_body_color == "Silver" && chick_gender == "Male") ||
                (f_body_color == "Silver" && m_body_color == "Green" && chick_gender == "Male") ||
                (f_body_color == "Silver" && m_body_color == "Blue" && chick_gender == "Male") ||
                (f_body_color == "Silver" && m_body_color == "Yellow" && chick_gender == "Male"))
            {
                return "Green";
            }
            else if((f_body_color == "Yellow" && m_body_color == "Green" && chick_gender == "Female") ||
                (f_body_color == "Yellow" && m_body_color == "Yellow") ||
                (f_body_color == "Yellow" && m_body_color == "Blue" && chick_gender == "Female") ||
                (f_body_color == "Yellow" && m_body_color == "Silver" && chick_gender == "Female"))
            {
                return "Yellow";
            }
            else if((f_body_color == "Blue" && m_body_color == "Blue"))
            {
                return "Blue";
            }
            else if ((f_body_color == "Silver" && m_body_color == "Green" && chick_gender == "Female") ||
                (f_body_color == "Silver" && m_body_color == "Blue" && chick_gender == "Female") ||
                (f_body_color == "Silver" && m_body_color == "Silver") ||
                (f_body_color == "Silver" && m_body_color == "Yellow" && chick_gender == "Female"))
            {
                return "Silver";
            }
            return f_body_color;
        }

        /// <summary>
        /// Refreshes the bird list displayed in the dataGridView1 control.
        /// Retrieves the latest data from the database table "LadyGouldianFinch"
        /// and updates the dataGridView1 with the retrieved data.
        /// </summary>
        private void RefreshBirdList()
        {
            SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;
Integrated Security=True;");
            string query = "Select *from LadyGouldianFinch order by Serial_number desc";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "LadyGouldianFinch");
            dataGridView.DataSource = dataSet.Tables[0];
        }
    }
}