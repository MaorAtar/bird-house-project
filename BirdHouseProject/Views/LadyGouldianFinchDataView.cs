using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Form for displaying and adding data for Lady Gouldian Finch.
    /// </summary>
    public partial class LadyGouldianFinchDataView : Form
    {
        // Fields
        private const string connection = "Data Source=**TO-DO**;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";

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
            showBirdPic(headColor, breastColor, bodyColor);
            // Fill the Chick inherited details
            speciesBox2.Text = species;
            subSpeciesBox2.Text = subSpecies;
            cageNumberBox2.Text = cageNumber;
            if (IsFSerialNumberValid(serialNumber.ToString()) == true)
            {
                fSerialBox2.Text = serialNumber.ToString();
                fSerialBox2.ReadOnly = true;
                fSerialBox2.BackColor = Color.White;
            }
            else
            {
                mSerialBox2.Text = serialNumber.ToString();
                mSerialBox2.ReadOnly = true;
                mSerialBox2.BackColor = Color.White;
            }
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
            SqlConnection connectionString = new SqlConnection(connection);
            connectionString.Open();
            string query = "Select * From LadyGouldianFinch WHERE F_Serial_Number = @serial_number Or M_Serial_Number = @serial_number";
            SqlCommand command = new SqlCommand(query, connectionString);
            command.Parameters.AddWithValue("@serial_number", serialNumber);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView.DataSource = dt;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                switch (column.DataPropertyName)
                {
                    case "Serial_Number":
                        column.HeaderText = "Serial Number";
                        break;
                    case "Sub_Species":
                        column.HeaderText = "Sub Species";
                        break;
                    case "Hatch_Date":
                        column.HeaderText = "Hatch Date";
                        break;
                    case "Cage_Number":
                        column.HeaderText = "Cage Number";
                        break;
                    case "F_Serial_Number":
                        column.HeaderText = "Father Serial Number";
                        break;
                    case "M_Serial_Number":
                        column.HeaderText = "Mother Serial Number";
                        break;
                    case "Head_Color":
                        column.HeaderText = "Head Color";
                        break;
                    case "Breast_Color":
                        column.HeaderText = "Breast Color";
                        break;
                    case "Body_Color":
                        column.HeaderText = "Body Color";
                        break;
                }
            }
            connectionString.Close();
        }

        /// <summary>
        /// Event handler for the Save button click event. Saves the data of a new Lady Gouldian Finch.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void saveBtn2_Click(object sender, EventArgs e)
        {
            SqlConnection connectionString = new SqlConnection(connection);

            // Hatch Date Validation
            if (!IsDateFormatValid(hatchDateBox2.Text))
            {
                hatchDateErrorProvider.SetError(hatchDateBox2, "Enter Date in DD/MM/YYYY Format");
                return;
            }
            else
            {
                hatchDateErrorProvider.SetError(hatchDateBox2, string.Empty);
            }
            // Gender Validation
            if (genderComboBox2.SelectedIndex == -1)
            {
                genderErrorProvider.SetError(genderComboBox2, "Invalid Gender");
                return;
            }
            else
            {
                genderErrorProvider.SetError(genderComboBox2, string.Empty);
            }
            // Father Serial Number Validation
            if (fSerialBox2.Text.Length != 6)
            {
                fsnErrorProvider.SetError(fSerialBox2, "Father Serial Number must be 6 digits long");
                return;
            }
            else if (!IsFSerialNumberInDB(fSerialBox2.Text))
            {
                fsnErrorProvider.SetError(fSerialBox2, "Father Serial Number does not exist in the system");
                return;
            }
            else if (!IsFSerialNumberValid(fSerialBox2.Text))
            {
                fsnErrorProvider.SetError(fSerialBox2, "Father Serial Number does not match to a Male bird Serial Number");
                return;
            }
            else if (!IsFMInSameCage(fSerialBox.Text, mSerialBox.Text))
            {
                fsnErrorProvider.SetError(fSerialBox, "The Chick father and mother have to be in the same cage");
                return;
            }
            else if (string.Equals(fSerialBox2.Text, mSerialBox2.Text))
            {
                fsnErrorProvider.SetError(fSerialBox2, "Father Serial Number and Mother Serial Number can't be the same");
                return;
            }
            else
            {
                fsnErrorProvider.SetError(fSerialBox2, string.Empty);
            }
            // Mother Serial Number Validation
            if (mSerialBox2.Text.Length != 6)
            {
                msnErrorProvider.SetError(mSerialBox2, "Mother Serial Number must be 6 digits long");
                return;
            }
            else if (!IsMSerialNumberInDB(mSerialBox2.Text))
            {
                msnErrorProvider.SetError(mSerialBox2, "Mother Serial Number does not exist in the system");
                return;
            }
            else if (!IsMSerialNumberValid(mSerialBox2.Text))
            {
                msnErrorProvider.SetError(mSerialBox2, "Mother Serial Number does not match to a Female bird Serial Number");
                return;
            }
            else if (string.Equals(mSerialBox2.Text, fSerialBox2.Text))
            {
                msnErrorProvider.SetError(mSerialBox2, "Mother Serial Number can't be the same as Father Serial Number");
            }
            else
            {
                msnErrorProvider.SetError(mSerialBox2, string.Empty);
            }

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
            SqlConnection connectionString = new SqlConnection(connection);
            string query = "Select *from LadyGouldianFinch order by Serial_number desc";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "LadyGouldianFinch");
            dataGridView.DataSource = dataSet.Tables[0];
        }

        /// <summary>
        /// Checks if a given father serial number is in the LadyGouldianFinch SQL table.
        /// </summary>
        /// <param name="f_serial_number"></param>
        /// <returns></returns>
        private bool IsFSerialNumberInDB(string f_serial_number)
        {
            bool flag = false;
            string query = "SELECT Serial_Number FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@f_serial_number", f_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        flag = true;
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return flag;
        }

        /// <summary>
        /// Checks if a given father serial number is valid.
        /// </summary>
        /// <param name="f_serial_number"></param>
        /// <returns>boolean</returns>
        private bool IsFSerialNumberValid(string f_serial_number)
        {
            bool flag = false;
            string temp;
            string query = "SELECT Gender FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@f_serial_number", f_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        temp = reader.GetString(0);
                        if (temp == "Male")
                        {
                            flag = true;
                        }
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return flag;
        }

        /// <summary>
        /// Checks if a given mother serial number is in the LadyGouldianFinch SQL table.
        /// </summary>
        /// <param name="m_serial_number"></param>
        /// <returns></returns>
        private bool IsMSerialNumberInDB(string m_serial_number)
        {
            bool flag = false;
            string query = "SELECT Serial_Number FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@m_serial_number", m_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        flag = true;
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return flag;
        }

        /// <summary>
        /// Checks if the given string is in a correct date format.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>boolean</returns>
        private static bool IsDateFormatValid(string input)
        {
            DateTime parsedDate;
            return DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate);
        }

        /// <summary>
        /// Checks if a given mother serial number is valid.
        /// </summary>
        /// <param name="m_serial_number"></param>
        /// <returns>boolean</returns>
        private bool IsMSerialNumberValid(string m_serial_number)
        {
            bool flag = false;
            string temp;
            string query = "SELECT Gender FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@m_serial_number", m_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        temp = reader.GetString(0);
                        if (temp == "Female")
                        {
                            flag = true;
                        }
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return flag;
        }

        /// <summary>
        /// Checks if the father and mother of the bird is located in the same cage.
        /// </summary>
        /// <param name="f_serial_number"></param>
        /// <param name="m_serial_number"></param>
        /// <returns></returns>
        private bool IsFMInSameCage(string f_serial_number, string m_serial_number)
        {
            string temp1 = null;
            string temp2 = null;
            string query1 = "SELECT Cage_Number FROM LadyGouldianFinch WHERE Serial_Number = @m_serial_number";
            string query2 = "SELECT Cage_Number FROM LadyGouldianFinch WHERE Serial_Number = @f_serial_number";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query1, sqlConnection))
                {
                    command.Parameters.AddWithValue("@m_serial_number", m_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        temp1 = reader.GetString(0);
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand command = new SqlCommand(query2, sqlConnection))
                {
                    command.Parameters.AddWithValue("@f_serial_number", f_serial_number);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        temp2 = reader.GetString(0);
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return string.Equals(temp1, temp2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Presentes the picture of the bird.
        /// </summary>
        /// <param name="headColor"></param>
        /// <param name="breastColor"></param>
        /// <param name="bodyColor"></param>
        private void showBirdPic(string headColor, string breastColor, string bodyColor)
        {
            if (headColor == "Red" && breastColor == "Purple" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RPG.png");
            else if (headColor == "Red" && breastColor == "Purple" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RPY.png");
            else if (headColor == "Red" && breastColor == "Purple" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RPB.png");
            else if (headColor == "Red" && breastColor == "Purple" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RPS.png");
            else if (headColor == "Red" && breastColor == "Lilac" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RLG.png");
            else if (headColor == "Red" && breastColor == "Lilac" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RLY.png");
            else if (headColor == "Red" && breastColor == "Lilac" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RLB.png");
            else if (headColor == "Red" && breastColor == "Lilac" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RLS.png");
            else if (headColor == "Red" && breastColor == "White" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RWG.png");
            else if (headColor == "Red" && breastColor == "White" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RWY.png");
            else if (headColor == "Red" && breastColor == "White" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RWB.png");
            else if (headColor == "Red" && breastColor == "White" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RWS.png");
            else if (headColor == "Black" && breastColor == "Purple" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BPG.png");
            else if (headColor == "Black" && breastColor == "Purple" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BPY.png");
            else if (headColor == "Black" && breastColor == "Purple" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BPB.png");
            else if (headColor == "Black" && breastColor == "Purple" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BPS.png");
            else if (headColor == "Black" && breastColor == "Lilac" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BLG.png");
            else if (headColor == "Black" && breastColor == "Lilac" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BLY.png");
            else if (headColor == "Black" && breastColor == "Lilac" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BLB.png");
            else if (headColor == "Black" && breastColor == "Lilac" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BLS.png");
            else if (headColor == "Black" && breastColor == "White" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BWG.png");
            else if (headColor == "Black" && breastColor == "White" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BWY.png");
            else if (headColor == "Black" && breastColor == "White" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BWB.png");
            else if (headColor == "Black" && breastColor == "White" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\BWS.png");
            else if (headColor == "Orange" && breastColor == "Purple" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OPG.png");
            else if (headColor == "Orange" && breastColor == "Purple" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OPY.png");
            else if (headColor == "Orange" && breastColor == "Purple" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OPB.png");
            else if (headColor == "Orange" && breastColor == "Purple" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OPS.png");
            else if (headColor == "Orange" && breastColor == "Lilac" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OLG.png");
            else if (headColor == "Orange" && breastColor == "Lilac" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OLY.png");
            else if (headColor == "Orange" && breastColor == "Lilac" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OLB.png");
            else if (headColor == "Orange" && breastColor == "Lilac" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OLS.png");
            else if (headColor == "Orange" && breastColor == "White" && bodyColor == "Green")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OWG.png");
            else if (headColor == "Orange" && breastColor == "White" && bodyColor == "Yellow")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OWY.png");
            else if (headColor == "Orange" && breastColor == "White" && bodyColor == "Blue")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OWB.png");
            else if (headColor == "Orange" && breastColor == "White" && bodyColor == "Silver")
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\OWS.png");
            else
                birdPictureBox.Image = Image.FromFile(@"Resources\Birds Pictures\RPG.png");
        }

        /// <summary>
        /// Exports the data in the datagrid to an excel file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            // Create columns in the DataTable based on the DataGridView columns
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Iterate through each DataGridView row and copy the cell values to the DataTable
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value;
                }

                dataTable.Rows.Add(dataRow);
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Woorkbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dataTable.Copy(), "Chicks");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Successfully exported the Chicks data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Closes the Bird Detail form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}