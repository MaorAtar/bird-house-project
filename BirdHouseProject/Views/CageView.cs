using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents a Windows Forms view for managing cages.
    /// </summary>
    public partial class CageView : Form, ICageView
    {
        // Fields
        SqlConnection connectionString = new SqlConnection(@"Data Source=MAOR-ATAR-LAPTO;Initial Catalog=BirdHouseProjectDb;
Integrated Security=True;");
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        // Constructor
        public CageView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            closeBtn.Click += delegate { this.Close(); };
        }

        /// <summary>
        /// Associates and raises events for the view.
        /// </summary>
        private void AssociateAndRaiseViewEvents()
        {
            // Search
            searchBtn.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            searchBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                RefreshCageList();
            };
            // Add New
            addnewBtn.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                RefreshCageList();
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Add new cage";
            };
            // Edit
            editBtn.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                RefreshCageList();
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Edit cage";
            };
            // Save changes
            saveBtn.Click += delegate
            {
                // User input validation
                // Length Validation
                if (string.IsNullOrEmpty(lenBox.Text))
                {
                    lengthErrorProvider.SetError(lenBox, "Cage Length can't be empty");
                    return;
                }
                else if (!ValidateCageSizeInput(lenBox.Text))
                {
                    lengthErrorProvider.SetError(lenBox, "Length must be in range of 15-100 cm");
                    return;
                }
                else
                {
                    lengthErrorProvider.SetError(lenBox, string.Empty);
                }
                // Width Validation
                if (string.IsNullOrEmpty(widthBox.Text))
                {
                    widthErrorProvider.SetError(widthBox, "Cage Width can't be empty");
                    return;
                }
                else if (!ValidateCageSizeInput(widthBox.Text))
                {
                    widthErrorProvider.SetError(widthBox, "Width must be in range of 15-100 cm");
                    return;
                }
                else
                {
                    widthErrorProvider.SetError(widthBox, string.Empty);
                }
                // Height Validation
                if (string.IsNullOrEmpty(heightBox.Text))
                {
                    heightErrorProvider.SetError(heightBox, "Cage Height can't be empty");
                    return;
                }
                else if (!ValidateCageSizeInput(heightBox.Text))
                {
                    heightErrorProvider.SetError(heightBox, "Height must be in range of 15-100 cm");
                    return;
                }
                else
                {
                    heightErrorProvider.SetError(heightBox, string.Empty);
                }
                // Material Validation
                if (materialComboBox.SelectedIndex == -1)
                {
                    materialErrorProvider.SetError(materialComboBox, "Invalid Material Type");
                    return;
                }
                else
                {
                    materialErrorProvider.SetError(materialComboBox, string.Empty);
                }
                SaveEvent?.Invoke(this, EventArgs.Empty);
                RefreshCageList();
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Message);
            };
            // Cancel
            cancelBtn.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
            };
            // Delete
            deleteBtn.Click += delegate
            {

                var result = MessageBox.Show("Are you sure you want to delete the selected cage?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    RefreshCageList();
                    MessageBox.Show(Message);
                }
            };
        }

        // Properties
        public string CageSerialNumber { get => serialBox.Text; set => serialBox.Text = value; }
        public string CageLength { get => lenBox.Text; set => lenBox.Text = value; }
        public string CageWidth { get => widthBox.Text; set => widthBox.Text = value; }
        public string CageHeight { get => heightBox.Text; set => heightBox.Text = value; }
        public string CageMaterial { get => materialComboBox.Text.ToString(); set => materialComboBox.Text = value; }

        public string SearchValue { get => searchBox.Text; set => searchBox.Text = value; }
        public bool IsEdit { get => isEdit; set => isEdit = value; }
        public bool IsSuccessful { get => isSuccessful; set => isSuccessful = value; }
        public string Message { get => message; set => message = value; }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        /// <summary>
        /// Sets the data source for the cage list.
        /// </summary>
        public void SetCageBindingSource(BindingSource cageList)
        {
            dataGridView1.DataSource = cageList;
        }

        // Singleton pattern (open a single form instance)
        private static CageView instance;

        /// <summary>
        /// Retrieves the singleton instance of the CageView form.
        /// </summary>
        /// <param name="parentContainer">The parent container form.</param>
        /// <returns>The singleton instance of the CageView form.</returns>
        public static CageView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new CageView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }

        /// <summary>
        /// Handles the cell content click event in the DataGridView control.
        /// Retrieves the selected cage data and opens a CageDataView form to display the details.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the selected bird data
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int cage_serial_number = Convert.ToInt32(row.Cells[0].Value);
            double length = Convert.ToDouble(row.Cells[1].Value);
            double width = Convert.ToDouble(row.Cells[2].Value);
            double height = Convert.ToDouble(row.Cells[3].Value);
            string material = row.Cells[4].Value.ToString();

            // Create a new instance of the details form and pass the selected bird data
            CageDataView cageDataView = new CageDataView(cage_serial_number, length, width, height, material);

            // Show the details form
            cageDataView.Show();
        }

        /// <summary>
        /// Refreshes the cage list displayed in the dataGridView1 control.
        /// Retrieves the latest data from the database table "Cage"
        /// and updates the dataGridView1 with the retrieved data.
        /// </summary>
        private void RefreshCageList()
        {
            string query = "Select *from Cage order by Serial_number desc";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionString);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Cage");
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        /// <summary>
        /// Validates the size of the cage inputs.
        /// </summary>
        /// <returns>Boolean</returns>
        private bool ValidateCageSizeInput(string size)
        {
            return Convert.ToInt32(size) >= 15 && Convert.ToInt32(size) <= 100;
        }
    }
}
