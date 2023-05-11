using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BirdHouseProject.Models;
using BirdHouseProject.Views;
using BirdHouseProject.Presenters;
using BirdHouseProject.Repositories;
using System.Configuration;


namespace BirdHouseProject
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());    
        }
    }
}
