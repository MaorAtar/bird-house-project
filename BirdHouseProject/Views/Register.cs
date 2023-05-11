using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Text.RegularExpressions.Regex;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace BirdHouseProject.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\maora\\OneDrive\\Desktop\\BirdHouseProject\\BirdHouseProject\\Excel Files\\Users.xlsx";
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb;
            Worksheet ws;

            wb = excel.Workbooks.Open(filePath);
            ws = wb.Worksheets[1];

            // Read input fields
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConPassword.Text;
            string id = idUser.Text;

            // Find the last row with data
            int lastRow = ws.Cells.Find("*", System.Reflection.Missing.Value,
                              System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                              XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious,
                              false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

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
            // Write the new user's data to the next row
            Range userRange = ws.Range["A" + (lastRow + 1).ToString()];
            userRange.Value = password;

            Range passwordRange = ws.Range["B" + (lastRow + 1).ToString()];
            passwordRange.Value = username;

            wb.Save();
            wb.Close();
            MessageBox.Show("Registration successful!");
            new MainView().Show();
            this.Close();
        }


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
        private bool ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 10)
            {
                MessageBox.Show("Password must contain between 8 and 10 characters");
                return false;
            }
            char[] specialChars = new char[] { '-', '?', '#', '$', '_' };
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

            if (numofdigit != 1)
            {
                MessageBox.Show("The password must contain one number!");
                return false;
            }
            if (numofletter < 1)
            {
                MessageBox.Show("The Password cannot contain less than 1 letter!");
                return false;
            }
            if (numofspeical != 1)
            {
                MessageBox.Show("The password must contain one speical letter!");
                return false;
            }

            return true;
        }

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

        private void label5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        
    }
}
