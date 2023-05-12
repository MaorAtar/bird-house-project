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
            connectionString.Open();
            string query = "SELECT * FROM LadyGouldianFinch WHERE Cage_Number = @cage_serial_number";
            SqlCommand command = new SqlCommand(query, connectionString);
            command.Parameters.AddWithValue("@cage_serial_number", cageSerialBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            birdsDataGridView.DataSource = dt;
            connectionString.Close();
        }
    }
}

