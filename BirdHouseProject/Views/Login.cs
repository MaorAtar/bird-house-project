using BirdHouseProject.Presenters;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            readExcel();
        }

        private void readExcel()
        {
            try
            {
                // Open the Excel workbook
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excel.Workbooks.Open(@"C:\Users\maora\OneDrive\Desktop\BirdHouseProject\BirdHouseProject\Excel Files\Users.xlsx");
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
                            MessageBox.Show("Login successful!");
                            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            MainView view = new MainView();
                            new MainPresenter(view, sqlConnectionString);
                            view.Show(); // Show the MainView as a dialog box
                            this.Hide(); // Hide the Login form
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password");
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                        return;
                    }
                    
                }
                MessageBox.Show("Username not found");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
                // Close the Excel workbook
                workbook.Close();
                excel.Quit();

                // Close the Excel workbook
                workbook.Close();
                excel.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }


        private void Login_Load_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            new Register().Show();
            this.Hide();
        }

        
    }
}
