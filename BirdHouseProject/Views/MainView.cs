using System;
using System.Windows.Forms;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// The main view of the Bird House application.
    /// </summary>
    public partial class MainView : Form, IMainView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            birdBtn.Click += delegate { ShowLadyGouldianFinchView?.Invoke(this, EventArgs.Empty); };
            cageBtn.Click += delegate { ShowCageView?.Invoke(this, EventArgs.Empty); };
        }

        /// <summary>
        /// Occurs when the "Show Lady Gouldian Finch View" button is clicked.
        /// </summary>
        public event EventHandler ShowLadyGouldianFinchView;

        /// <summary>
        /// Occurs when the "Show Cage View" button is clicked.
        /// </summary>
        public event EventHandler ShowCageView;

        /// <summary>
        /// Handles the click event of the "Close" button.
        /// Closes the form and exits the application.
        /// </summary>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
