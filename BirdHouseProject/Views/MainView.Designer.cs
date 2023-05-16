
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cageBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bgMusicCheckBox = new System.Windows.Forms.CheckBox();
            this.birdBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(127)))), ((int)(((byte)(114)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(21, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 237);
            this.panel3.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(68)))), ((int)(((byte)(57)))));
            this.label2.Location = new System.Drawing.Point(51, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Place";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BirdHouseProject.Properties.Resources.bird_house;
            this.pictureBox1.Location = new System.Drawing.Point(23, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 147);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(68)))), ((int)(((byte)(57)))));
            this.label1.Location = new System.Drawing.Point(23, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bird House";
            // 
            // cageBtn
            // 
            this.cageBtn.FlatAppearance.BorderSize = 0;
            this.cageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cageBtn.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cageBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(68)))), ((int)(((byte)(57)))));
            this.cageBtn.Image = global::BirdHouseProject.Properties.Resources.cage;
            this.cageBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cageBtn.Location = new System.Drawing.Point(0, 331);
            this.cageBtn.Name = "cageBtn";
            this.cageBtn.Size = new System.Drawing.Size(250, 50);
            this.cageBtn.TabIndex = 5;
            this.cageBtn.Text = "Cage";
            this.cageBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cageBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(127)))), ((int)(((byte)(114)))));
            this.panel1.Controls.Add(this.bgMusicCheckBox);
            this.panel1.Controls.Add(this.cageBtn);
            this.panel1.Controls.Add(this.birdBtn);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 714);
            this.panel1.TabIndex = 45;
            // 
            // bgMusicCheckBox
            // 
            this.bgMusicCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.bgMusicCheckBox.AutoSize = true;
            this.bgMusicCheckBox.Location = new System.Drawing.Point(72, 672);
            this.bgMusicCheckBox.Name = "bgMusicCheckBox";
            this.bgMusicCheckBox.Size = new System.Drawing.Size(92, 30);
            this.bgMusicCheckBox.TabIndex = 14;
            this.bgMusicCheckBox.Text = "Stop Music";
            this.bgMusicCheckBox.UseVisualStyleBackColor = true;
            this.bgMusicCheckBox.CheckedChanged += new System.EventHandler(this.bgMusicCheckBox_CheckedChanged);
            // 
            // birdBtn
            // 
            this.birdBtn.FlatAppearance.BorderSize = 0;
            this.birdBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.birdBtn.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.birdBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(68)))), ((int)(((byte)(57)))));
            this.birdBtn.Image = global::BirdHouseProject.Properties.Resources.bullfinch;
            this.birdBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.birdBtn.Location = new System.Drawing.Point(0, 275);
            this.birdBtn.Name = "birdBtn";
            this.birdBtn.Size = new System.Drawing.Size(250, 50);
            this.birdBtn.TabIndex = 4;
            this.birdBtn.Text = "Bird";
            this.birdBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.birdBtn.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(68)))), ((int)(((byte)(57)))));
            this.closeBtn.Image = global::BirdHouseProject.Properties.Resources.power_button;
            this.closeBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeBtn.Location = new System.Drawing.Point(976, 12);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(43, 42);
            this.closeBtn.TabIndex = 14;
            this.closeBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.closeBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(253, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1031, 67);
            this.panel2.TabIndex = 46;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(127)))), ((int)(((byte)(114)))));
            this.ClientSize = new System.Drawing.Size(1284, 714);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainView";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cageBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button birdBtn;
        private System.Windows.Forms.CheckBox bgMusicCheckBox;
    }
}