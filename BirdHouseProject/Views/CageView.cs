using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// Represents a Windows Forms view for managing cages.
    /// </summary>
    public partial class CageView : Form, ICageView
    {

        // Fields
        private const string connection = "Data Source=**TO-DO**;Initial Catalog=BirdHouseProjectDb;Integrated Security=True;";
        private SqlConnection connectionString = new SqlConnection(connection);
        private CageDataView openCageDataViewForm;
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        // Constructor
        public CageView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            if (dataGridView1.Rows.Count == 1)
            {
                SingleObjectInDB();
            }
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
                else if (!ValidateCageSizeInputForChars(lenBox.Text))
                {
                    lengthErrorProvider.SetError(lenBox, "Length cannot contain letters");
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
                else if (!ValidateCageSizeInputForChars(widthBox.Text))
                {
                    widthErrorProvider.SetError(widthBox, "Width cannot contain letters");
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
                else if (!ValidateCageSizeInputForChars(heightBox.Text))
                {
                    heightErrorProvider.SetError(heightBox, "Height cannot contain letters");
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
            // Check if a CageDataView form is already open
            if (openCageDataViewForm != null && !openCageDataViewForm.IsDisposed)
            {
                openCageDataViewForm.BringToFront();
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int cage_serial_number = Convert.ToInt32(row.Cells[0].Value);
            double length = Convert.ToDouble(row.Cells[1].Value);
            double width = Convert.ToDouble(row.Cells[2].Value);
            double height = Convert.ToDouble(row.Cells[3].Value);
            string material = row.Cells[4].Value.ToString();

            openCageDataViewForm = new CageDataView(cage_serial_number, length, width, height, material);
            openCageDataViewForm.FormClosed += (s, args) => openCageDataViewForm = null; // Reset the reference when the form is closed
            openCageDataViewForm.Show();
        }

        /// <summary>
        /// Displays the single cage object (in the data base) details.
        /// </summary>
        private void SingleObjectInDB()
        {
            DataGridViewRow row = dataGridView1.Rows[0];
            int cage_serial_number = Convert.ToInt32(row.Cells[0].Value);
            double length = Convert.ToDouble(row.Cells[1].Value);
            double width = Convert.ToDouble(row.Cells[2].Value);
            double height = Convert.ToDouble(row.Cells[3].Value);
            string material = row.Cells[4].Value.ToString();

            CageDataView cageDataView = new CageDataView(cage_serial_number, length, width, height, material);
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
            for (int i = 0; i < size.Length; i++)
            {
                if (!char.IsDigit(size[i]))
                {
                    return false;
                }
            }
            if (!(Convert.ToInt32(size) >= 15 && Convert.ToInt32(size) <= 100))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates that the size of the cage input dosen't contain a letter.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private bool ValidateCageSizeInputForChars(string size)
        {
            foreach (char c in size)
            {
                if (Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
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
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Iterate through each DataGridView row and copy the cell values to the DataTable
            foreach (DataGridViewRow row in dataGridView1.Rows)
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
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using(XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dataTable.Copy(), "Cages");
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Successfully exported the Cages data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
