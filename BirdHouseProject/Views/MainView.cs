using System;
using System.Windows.Forms;
using System.Media;

namespace BirdHouseProject.Views
{
    /// <summary>
    /// The main view of the Bird House application.
    /// </summary>
    public partial class MainView : Form, IMainView
    {
        // Fields
        SoundPlayer sound;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            sound = new SoundPlayer(@"Resources\Music\background-sound.wav");
            //sound.PlayLooping();
            birdBtn.Click += delegate { ShowLadyGouldianFinchView?.Invoke(this, EventArgs.Empty);
                Controls.Remove(panel2);
            };
            cageBtn.Click += delegate { ShowCageView?.Invoke(this, EventArgs.Empty);
                Controls.Remove(panel2);
            };
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
            Application.Exit();
        }

        private void bgMusicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bgMusicCheckBox.Checked)
            {
                sound.PlayLooping();
            }
            else
            {
                sound.Stop();
            }
        }

        private void mainBtn_Click(object sender, EventArgs e)
        {
            Controls.Add(panel2);
            panel2.Dock = DockStyle.Fill;
        }
    }
}
