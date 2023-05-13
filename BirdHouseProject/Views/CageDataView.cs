using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents a Windows Forms view for displaying cage data and associated birds.
    /// </summary>
    public partial class CageDataView : Form
    {
        // Fields
        SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;");

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
            showBirdsTable();
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
            birdsDataGridView.DataSource = dt;
            connectionString.Close();
        }
    }
}