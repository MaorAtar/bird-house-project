using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    public partial class CageDataView : Form
    {
        // Fields
        SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;");

        public CageDataView()
        {
            InitializeComponent();
        }

        public CageDataView(int cage_serial_number, double length, double width, double height, string material)
        {
            InitializeComponent();
            // Fill the current cage details
            cageSerialBox.Text = cage_serial_number.ToString();
            lengthBox.Text = length.ToString();
            widthBox.Text = width.ToString();
            heightBox.Text = height.ToString();
            materialBox.Text = material;
            showBirdsTable();
        }

        // Methods
        private void showBirdsTable()
        {
            try
            {
                // Open connection
                connectionString.Open();

                // Prepare the query with a WHERE clause that filters by cage_serial_number
                string query = "SELECT * FROM LadyGouldianFinch WHERE Cage_Number = @cage_serial_number";

                // Create a SqlCommand object with the query and the parameter
                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.Parameters.AddWithValue("@cage_serial_number", cageSerialBox.Text);

                // Create a SqlDataAdapter object with the command
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Fill a DataTable with the query results
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Set the DataGridView control's DataSource property to the DataTable
                birdsDataGridView.DataSource = dt;

                // Close the connection
                connectionString.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



    }
}
