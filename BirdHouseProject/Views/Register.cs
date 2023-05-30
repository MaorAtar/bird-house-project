using System;
using System.Windows.Forms;
using static System.Text.RegularExpressions.Regex;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.IO;
using System.Configuration;
using BirdHouseProject.Presenters;
using System.Runtime.InteropServices;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// The registration form of the Bird House application.
    /// </summary>
    public partial class Register : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        public Register()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 10;
            txtConPassword.PasswordChar = '*';
            txtConPassword.MaxLength = 10;
        }

        /// <summary>
        /// Handles the click event of the "Register" button.
        /// Performs registration process by saving user data to an Excel file.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = Path.GetFullPath(@"Resources\Login Excel Files\Users.xlsx");
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = null;
            Worksheet ws = null;

            try
            {
                IsFileOpen(filePath);
                wb = excel.Workbooks.Open(filePath);
                ws = wb.Worksheets[1];

                // Read input fields
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtConPassword.Text;
                string id = idUser.Text;

                // Validate input
                if (!ValidateUsername(username))
                {
                    return;
                }

                if (!ValidatePassword(password))
                {
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                if (!ValidateId(id))
                {
                    return;
                }

                // Find the last row with data
                int lastRow = ws.Cells.Find("*", System.Reflection.Missing.Value,
                                  System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                  XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious,
                                  false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                // Write the new user's data to the next row
                if (ValidateUsername(username) && ValidatePassword(password) && password == confirmPassword)
                {
                    Range userRange = ws.Range["A" + (lastRow + 1).ToString()];
                    userRange.Value = password;

                    Range passwordRange = ws.Range["B" + (lastRow + 1).ToString()];
                    passwordRange.Value = username;

                    Range idRange = ws.Range["C" + (lastRow + 1).ToString()];
                    idRange.Value = id;

                    wb.Save();
                    MessageBox.Show("Registration Successful!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Clean up Excel objects
                if (ws != null)
                    Marshal.ReleaseComObject(ws);

                if (wb != null)
                {
                    wb.Close(SaveChanges: false);
                    Marshal.ReleaseComObject(wb);
                }

                if (excel != null)
                {
                    excel.Quit();
                    Marshal.ReleaseComObject(excel);
                }
            }

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MainView view = new MainView();
            new MainPresenter(view, sqlConnectionString);
            view.Show(); // Show the MainView as a dialog box
            this.Hide(); // Hide the Login form
            Close();
        }

        /// <summary>
        /// Checks if a file is already opened.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void IsFileOpen(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                }
            }
            catch (IOException) {}
        }

        /// <summary>
        /// Validates the username.
        /// The username must contain between 6 and 8 characters.
        /// It can only contain numbers or letters in English.
        /// It cannot contain more than 2 digits.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns><c>true</c> if the username is valid; otherwise, <c>false</c>.</returns>
        private bool ValidateUsername(string username)
        {
            // Validate length
            if (username.Length < 6 || username.Length > 8)
            {
                MessageBox.Show("Username must contain between 6 and 8 characters");
                return false;
            }
            int numofdigit = 0;

            for (int i = 0; i < username.Length; i++)
            {
                if (char.IsDigit(username[i]))
                {
                    numofdigit++;
                }
                if ((!char.IsDigit(username[i]) && (!IsMatch(username[i].ToString(), @"^[a-zA-Z]+$"))))
                {
                    MessageBox.Show("The username can only contain numbers or letters in English!");
                    return false;
                }
            }

            if (numofdigit > 2)
            {
                MessageBox.Show("The username cannot contain more than 2 digits!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the password.
        /// The password must contain between 8 and 10 characters.
        /// It must have at least one digit, one letter, and one special character from the set: '-', '?', '#', '$', '_'.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns><c>true</c> if the password is valid; otherwise, <c>false</c>.</returns>
        private bool ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 10)
            {
                MessageBox.Show("Password must contain between 8 and 10 characters");
                return false;
            }
            char[] specialChars = new char[] { '-', '?', '#', '$', '_', '@', '!', '%', '^', '&', '*', '=', '+' };
            int numofdigit = 0;
            int numofletter = 0;
            int numofspeical = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i]))
                {
                    numofdigit++;
                }
                if (char.IsLetter(password[i]))
                {
                    numofletter++;
                }
                if (Array.IndexOf(specialChars, password[i]) >= 0)
                {
                    numofspeical++;
                }

            }

            if (numofdigit < 1)
            {
                MessageBox.Show("The password must contain at least one number!");
                return false;
            }
            if (numofletter < 1)
            {
                MessageBox.Show("The Password cannot contain less than 1 letter!");
                return false;
            }
            if (numofspeical < 1)
            {
                MessageBox.Show("The password must contain at least one speical letter!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the ID number.
        /// The ID number must contain 9 digits.
        /// </summary>
        /// <param name="id">The ID number to validate.</param>
        /// <returns><c>true</c> if the ID number is valid; otherwise, <c>false</c>.</returns>
        private bool ValidateId(string id)
        {
            if (id.Length != 9)
            {
                MessageBox.Show("ID number must contain 9 digits!");
                return false;
            }
            for (int i = 0; i < id.Length; i++)
            {
                if (!(char.IsDigit(id[i])))
                {
                    MessageBox.Show("ID number must contain digits!");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Handles the click event of the "Exit" label.
        /// Exits the application.
        /// </summary>
        private void label5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}