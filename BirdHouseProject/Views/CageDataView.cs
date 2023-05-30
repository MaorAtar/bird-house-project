using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents a Windows Forms view for displaying cage data and associated birds.
    /// </summary>
    public partial class CageDataView : Form
    {
        // Fields
        private const string connection = "Data Source=**TO-DO**;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
        private SqlConnection connectionString = new SqlConnection(connection);

        /// <summary>
        /// Initializes a new instance of the CageDataView class.
        /// </summary>
        public CageDataView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the CageDataView class with the specified cage details.
        /// </summary>
        /// <param name="cage_serial_number">The serial number of the cage.</param>
        /// <param name="length">The length of the cage.</param>
        /// <param name="width">The width of the cage.</param>
        /// <param name="height">The height of the cage.</param>
        /// <param name="material">The material of the cage.</param>
        public CageDataView(int cage_serial_number, double length, double width, double height, string material)
        {
            InitializeComponent();
            // Fill the current cage details
            cageSerialBox.Text = cage_serial_number.ToString();
            lengthBox.Text = length.ToString();
            widthBox.Text = width.ToString();
            heightBox.Text = height.ToString();
            materialBox.Text = material;
            cageNumberLabel.Text = "#" + cage_serial_number.ToString();
            showBirdsTable();
            showCagePic(material);
        }

        /// <summary>
        /// Displays the birds table associated with the current cage.
        /// </summary>
        private void showBirdsTable()
        {
            connectionString.Open();
            string query = "SELECT * FROM LadyGouldianFinch WHERE Cage_Number = @cage_serial_number";
            SqlCommand command = new SqlCommand(query, connectionString);
            command.Parameters.AddWithValue("@cage_serial_number", cageSerialBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            birdsDataGrid.DataSource = dt;
            foreach (DataGridViewColumn column in birdsDataGrid.Columns)
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
        /// Displays the cage picture following the cage material.
        /// </summary>
        /// <param name="material"></param>
        private void showCagePic(string material)
        {
            if (material == "Steel")
                cagePictureBox.Image = Image.FromFile(@"Resources\Cages Pictures\SteelCage.jpg");
            else if (material == "Wood")
                cagePictureBox.Image = Image.FromFile(@"Resources\Cages Pictures\WoodCage.jpg");
            else if (material == "Plastic")
                cagePictureBox.Image = Image.FromFile(@"Resources\Cages Pictures\PlasticCage.jpg");
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
            foreach (DataGridViewColumn column in birdsDataGrid.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Iterate through each DataGridView row and copy the cell values to the DataTable
            foreach (DataGridViewRow row in birdsDataGrid.Rows)
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
                        MessageBox.Show("Successfully exported the Chick data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Closes the Cage Detail form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}