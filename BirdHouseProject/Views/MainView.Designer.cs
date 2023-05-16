
namespace BirdHouseProject.Views
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainBtn = new System.Windows.Forms.Button();
            this.cageBtn = new System.Windows.Forms.Button();
            this.bgMusicCheckBox = new System.Windows.Forms.CheckBox();
            this.birdBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(249)))), ((int)(((byte)(204)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.mainPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1282, 1053);
            this.panel2.TabIndex = 47;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(228)))), ((int)(((byte)(199)))));
            this.panel3.Controls.Add(this.closeBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(220, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1062, 64);
            this.panel3.TabIndex = 47;
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
            this.closeBtn.Location = new System.Drawing.Point(982, 1);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(68, 62);
            this.closeBtn.TabIndex = 18;
            this.closeBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(228)))), ((int)(((byte)(199)))));
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.mainBtn);
            this.mainPanel.Controls.Add(this.cageBtn);
            this.mainPanel.Controls.Add(this.bgMusicCheckBox);
            this.mainPanel.Controls.Add(this.birdBtn);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(220, 1053);
            this.mainPanel.TabIndex = 46;
            // 
            // mainBtn
            // 
            this.mainBtn.BackColor = System.Drawing.Color.Transparent;
            this.mainBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mainBtn.FlatAppearance.BorderSize = 0;
            this.mainBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainBtn.Location = new System.Drawing.Point(-1, 306);
            this.mainBtn.Name = "mainBtn";
            this.mainBtn.Size = new System.Drawing.Size(220, 67);
            this.mainBtn.TabIndex = 1;
            this.mainBtn.Text = "Home";
            this.mainBtn.UseVisualStyleBackColor = false;
            // 
            // cageBtn
            // 
            this.cageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cageBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.cageBtn.FlatAppearance.BorderSize = 0;
            this.cageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cageBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cageBtn.ForeColor = System.Drawing.Color.Black;
            this.cageBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cageBtn.Location = new System.Drawing.Point(0, 236);
            this.cageBtn.Name = "cageBtn";
            this.cageBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cageBtn.Size = new System.Drawing.Size(218, 64);
            this.cageBtn.TabIndex = 28;
            this.cageBtn.Text = "Cages List";
            this.cageBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cageBtn.UseVisualStyleBackColor = true;
            // 
            // bgMusicCheckBox
            // 
            this.bgMusicCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.bgMusicCheckBox.AutoSize = true;
            this.bgMusicCheckBox.FlatAppearance.BorderSize = 0;
            this.bgMusicCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bgMusicCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("bgMusicCheckBox.Image")));
            this.bgMusicCheckBox.Location = new System.Drawing.Point(11, 1002);
            this.bgMusicCheckBox.Name = "bgMusicCheckBox";
            this.bgMusicCheckBox.Size = new System.Drawing.Size(38, 38);
            this.bgMusicCheckBox.TabIndex = 22;
            this.bgMusicCheckBox.UseVisualStyleBackColor = true;
            // 
            // birdBtn
            // 
            this.birdBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.birdBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.birdBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.birdBtn.FlatAppearance.BorderSize = 0;
            this.birdBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.birdBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.birdBtn.ForeColor = System.Drawing.Color.Black;
            this.birdBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.birdBtn.Location = new System.Drawing.Point(0, 172);
            this.birdBtn.Name = "birdBtn";
            this.birdBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.birdBtn.Size = new System.Drawing.Size(218, 64);
            this.birdBtn.TabIndex = 27;
            this.birdBtn.Text = "Birds List";
            this.birdBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.birdBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 172);
            this.panel1.TabIndex = 26;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(36, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 141);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1282, 1053);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainView";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button cageBtn;
        private System.Windows.Forms.CheckBox bgMusicCheckBox;
        private System.Windows.Forms.Button birdBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mainBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}