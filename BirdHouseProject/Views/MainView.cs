using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            birdBtn.Click += delegate { ShowLadyGouldianFinchView?.Invoke(this, EventArgs.Empty); };
            cageBtn.Click += delegate { ShowCageView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowLadyGouldianFinchView;
        public event EventHandler ShowCageView;

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void MainView_Load(object sender, EventArgs e)
        {

        }
    }
}
