namespace Mandelbrot_WF
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_SaveImages = new System.Windows.Forms.CheckBox();
            this.textBox_Res_x = new System.Windows.Forms.TextBox();
            this.label_Res_x = new System.Windows.Forms.Label();
            this.textBox_Res_y = new System.Windows.Forms.TextBox();
            this.label_Res_y = new System.Windows.Forms.Label();
            this.textBox_SaveLocation = new System.Windows.Forms.TextBox();
            this.label_ZoomPercent = new System.Windows.Forms.Label();
            this.textBox_ZoomPercent = new System.Windows.Forms.TextBox();
            this.label_XCenter = new System.Windows.Forms.Label();
            this.textBox_XCenter = new System.Windows.Forms.TextBox();
            this.label_YCenter = new System.Windows.Forms.Label();
            this.textBox_YCenter = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 938);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(964, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Click += new System.EventHandler(this.toolStripProgressBar1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(0, 54);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(720, 720);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(884, 884);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // checkBox_SaveImages
            // 
            this.checkBox_SaveImages.AutoSize = true;
            this.checkBox_SaveImages.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox_SaveImages.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox_SaveImages.Location = new System.Drawing.Point(91, 34);
            this.checkBox_SaveImages.Name = "checkBox_SaveImages";
            this.checkBox_SaveImages.Size = new System.Drawing.Size(80, 17);
            this.checkBox_SaveImages.TabIndex = 4;
            this.checkBox_SaveImages.Text = "checkBox1";
            this.checkBox_SaveImages.UseVisualStyleBackColor = false;
            this.checkBox_SaveImages.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox_Res_x
            // 
            this.textBox_Res_x.Location = new System.Drawing.Point(341, 30);
            this.textBox_Res_x.Name = "textBox_Res_x";
            this.textBox_Res_x.Size = new System.Drawing.Size(58, 20);
            this.textBox_Res_x.TabIndex = 5;
            this.textBox_Res_x.TextChanged += new System.EventHandler(this.textBox_Res_x_TextChanged);
            // 
            // label_Res_x
            // 
            this.label_Res_x.AutoSize = true;
            this.label_Res_x.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Res_x.Location = new System.Drawing.Point(268, 34);
            this.label_Res_x.Name = "label_Res_x";
            this.label_Res_x.Size = new System.Drawing.Size(67, 13);
            this.label_Res_x.TabIndex = 6;
            this.label_Res_x.Text = "Image Width";
            // 
            // textBox_Res_y
            // 
            this.textBox_Res_y.Location = new System.Drawing.Point(481, 30);
            this.textBox_Res_y.Name = "textBox_Res_y";
            this.textBox_Res_y.Size = new System.Drawing.Size(63, 20);
            this.textBox_Res_y.TabIndex = 7;
            this.textBox_Res_y.TextChanged += new System.EventHandler(this.textBox_Res_y_TextChanged);
            // 
            // label_Res_y
            // 
            this.label_Res_y.AutoSize = true;
            this.label_Res_y.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Res_y.Location = new System.Drawing.Point(405, 33);
            this.label_Res_y.Name = "label_Res_y";
            this.label_Res_y.Size = new System.Drawing.Size(70, 13);
            this.label_Res_y.TabIndex = 8;
            this.label_Res_y.Text = "Image Height";
            // 
            // textBox_SaveLocation
            // 
            this.textBox_SaveLocation.Location = new System.Drawing.Point(177, 30);
            this.textBox_SaveLocation.Name = "textBox_SaveLocation";
            this.textBox_SaveLocation.Size = new System.Drawing.Size(85, 20);
            this.textBox_SaveLocation.TabIndex = 9;
            this.textBox_SaveLocation.TextChanged += new System.EventHandler(this.textBox_SaveLocation_TextChanged);
            // 
            // label_ZoomPercent
            // 
            this.label_ZoomPercent.AutoSize = true;
            this.label_ZoomPercent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_ZoomPercent.Location = new System.Drawing.Point(550, 33);
            this.label_ZoomPercent.Name = "label_ZoomPercent";
            this.label_ZoomPercent.Size = new System.Drawing.Size(85, 13);
            this.label_ZoomPercent.TabIndex = 10;
            this.label_ZoomPercent.Text = "Zoom % / Frame";
            // 
            // textBox_ZoomPercent
            // 
            this.textBox_ZoomPercent.Location = new System.Drawing.Point(641, 30);
            this.textBox_ZoomPercent.Name = "textBox_ZoomPercent";
            this.textBox_ZoomPercent.Size = new System.Drawing.Size(36, 20);
            this.textBox_ZoomPercent.TabIndex = 11;
            this.textBox_ZoomPercent.TextChanged += new System.EventHandler(this.textBox_ZoomPercent_TextChanged);
            // 
            // label_XCenter
            // 
            this.label_XCenter.AutoSize = true;
            this.label_XCenter.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_XCenter.Location = new System.Drawing.Point(683, 34);
            this.label_XCenter.Name = "label_XCenter";
            this.label_XCenter.Size = new System.Drawing.Size(17, 13);
            this.label_XCenter.TabIndex = 12;
            this.label_XCenter.Text = "X:";
            // 
            // textBox_XCenter
            // 
            this.textBox_XCenter.Location = new System.Drawing.Point(706, 30);
            this.textBox_XCenter.Name = "textBox_XCenter";
            this.textBox_XCenter.Size = new System.Drawing.Size(67, 20);
            this.textBox_XCenter.TabIndex = 13;
            this.textBox_XCenter.TextChanged += new System.EventHandler(this.textBox_XCenter_TextChanged);
            // 
            // label_YCenter
            // 
            this.label_YCenter.AutoSize = true;
            this.label_YCenter.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_YCenter.Location = new System.Drawing.Point(779, 34);
            this.label_YCenter.Name = "label_YCenter";
            this.label_YCenter.Size = new System.Drawing.Size(17, 13);
            this.label_YCenter.TabIndex = 14;
            this.label_YCenter.Text = "Y:";
            // 
            // textBox_YCenter
            // 
            this.textBox_YCenter.Location = new System.Drawing.Point(802, 30);
            this.textBox_YCenter.Name = "textBox_YCenter";
            this.textBox_YCenter.Size = new System.Drawing.Size(63, 20);
            this.textBox_YCenter.TabIndex = 15;
            this.textBox_YCenter.TextChanged += new System.EventHandler(this.textBox_YCenter_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(884, 960);
            this.Controls.Add(this.textBox_YCenter);
            this.Controls.Add(this.label_YCenter);
            this.Controls.Add(this.textBox_XCenter);
            this.Controls.Add(this.label_XCenter);
            this.Controls.Add(this.textBox_ZoomPercent);
            this.Controls.Add(this.label_ZoomPercent);
            this.Controls.Add(this.textBox_SaveLocation);
            this.Controls.Add(this.label_Res_y);
            this.Controls.Add(this.textBox_Res_y);
            this.Controls.Add(this.label_Res_x);
            this.Controls.Add(this.textBox_Res_x);
            this.Controls.Add(this.checkBox_SaveImages);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 999);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mandelbrot";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.CheckBox checkBox_SaveImages;
        private System.Windows.Forms.TextBox textBox_Res_x;
        private System.Windows.Forms.Label label_Res_x;
        private System.Windows.Forms.TextBox textBox_Res_y;
        private System.Windows.Forms.Label label_Res_y;
        private System.Windows.Forms.TextBox textBox_SaveLocation;
        private System.Windows.Forms.Label label_ZoomPercent;
        private System.Windows.Forms.TextBox textBox_ZoomPercent;
        private System.Windows.Forms.Label label_XCenter;
        private System.Windows.Forms.TextBox textBox_XCenter;
        private System.Windows.Forms.Label label_YCenter;
        private System.Windows.Forms.TextBox textBox_YCenter;
    }
}

