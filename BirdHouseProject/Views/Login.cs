using BirdHouseProject.Presenters;
using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// The login form of the Bird House application.
    /// </summary>
    public partial class Login : Form
    {
        private bool passwordVisible = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 10;
        }

        /// <summary>
        /// Handles the click event of the "Login" button.
        /// Reads and validates the entered username and password from an Excel file.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            readExcel();
        }

        /// <summary>
        /// Reads the usernames and passwords from an Excel file and performs login validation.
        /// </summary>
        private void readExcel()
        {
            try
            {
                // Open the Excel workbook
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                string filePath = Path.GetFullPath(@"Resources\Login Excel Files\Users.xlsx");
                Workbook workbook = excel.Workbooks.Open(filePath);
                Worksheet worksheet = workbook.Worksheets[1];

                // Get the range of cells containing the usernames and passwords
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
                object[,] usernameRange = worksheet.Range["Username"].Value2;
                object[,] passwordRange = worksheet.Range["Password"].Value2;

                // Iterate through the usernames and compare with the entered username
                for (int i = 1; i <= usernameRange.GetLength(0); i++)
                {
                    if (txtUsername.Text == usernameRange[i, 1]?.ToString())
                    {
                        // Username found, check password
                        if (passwordRange != null && passwordRange[i, 1] != null && txtPassword.Text == passwordRange[i, 1].ToString())
                        {
                            MessageBox.Show("Login Successful!");
                            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            MainView view = new MainView();
                            new MainPresenter(view, sqlConnectionString);
                            view.Show(); // Show the MainView as a dialog box
                            this.Hide(); // Hide the Login form
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Password");
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                        return;
                    }
                }

                // Username not found
                MessageBox.Show("Username not found");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();

                // Close the Excel workbook
                workbook.Close();
                excel.Quit();
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excel);
                worksheet = null;
                workbook = null;
                excel = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the click event of the "Exit" label.
        /// Exits the application.
        /// </summary>
        private void label5_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Handles the click event of the "Clear" label.
        /// Clears the username and password fields.
        /// </summary>
        private void label4_Click_1(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        /// <summary>
        /// Handles the click event of the "Register" label.
        /// Shows the registration form and hides the login form.
        /// </summary>
        private void label6_Click_1(object sender, EventArgs e)
        {
            new Register().Show();
            Hide();
        }

        /// <summary>
        /// handles show password/hide password button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPassBtn_Click(object sender, EventArgs e)
        {
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = true;
                passwordVisible = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                passwordVisible = true;
            }
        }
    }
}