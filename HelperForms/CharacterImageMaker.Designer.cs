namespace RelationMap.HelperForms
{
    partial class CharacterImageMaker
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
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.btnMakeThumbNail = new System.Windows.Forms.Button();
            this.pbDest = new System.Windows.Forms.PictureBox();
            this.pbSourceProfile = new System.Windows.Forms.PictureBox();
            this.lblThumbnail = new System.Windows.Forms.Label();
            this.lblProfile = new System.Windows.Forms.Label();
            this.pbOfficialProfile = new System.Windows.Forms.PictureBox();
            this.rbCircle = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.btnMakeProfile = new System.Windows.Forms.Button();
            this.cbTestImages = new System.Windows.Forms.ComboBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lbWidth = new System.Windows.Forms.Label();
            this.lbHeight = new System.Windows.Forms.Label();
            this.lblRadius = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMouseNormal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSourceProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOfficialProfile)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(812, 40);
            this.vScrollBar1.Maximum = 1000;
            this.vScrollBar1.Minimum = -1000;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(19, 813);
            this.vScrollBar1.TabIndex = 20;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(12, 844);
            this.hScrollBar1.Maximum = 1000;
            this.hScrollBar1.Minimum = -1000;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(800, 19);
            this.hScrollBar1.TabIndex = 19;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // btnMakeThumbNail
            // 
            this.btnMakeThumbNail.Location = new System.Drawing.Point(878, 131);
            this.btnMakeThumbNail.Name = "btnMakeThumbNail";
            this.btnMakeThumbNail.Size = new System.Drawing.Size(100, 23);
            this.btnMakeThumbNail.TabIndex = 18;
            this.btnMakeThumbNail.Text = "Make Thumbnail";
            this.btnMakeThumbNail.UseVisualStyleBackColor = true;
            this.btnMakeThumbNail.Click += new System.EventHandler(this.btnMakeThumbNail_Click);
            // 
            // pbDest
            // 
            this.pbDest.BackColor = System.Drawing.Color.White;
            this.pbDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbDest.Location = new System.Drawing.Point(878, 174);
            this.pbDest.Name = "pbDest";
            this.pbDest.Size = new System.Drawing.Size(100, 100);
            this.pbDest.TabIndex = 17;
            this.pbDest.TabStop = false;
            // 
            // pbSourceProfile
            // 
            this.pbSourceProfile.BackColor = System.Drawing.Color.Silver;
            this.pbSourceProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbSourceProfile.Location = new System.Drawing.Point(12, 40);
            this.pbSourceProfile.Margin = new System.Windows.Forms.Padding(0);
            this.pbSourceProfile.Name = "pbSourceProfile";
            this.pbSourceProfile.Size = new System.Drawing.Size(800, 800);
            this.pbSourceProfile.TabIndex = 16;
            this.pbSourceProfile.TabStop = false;
            this.pbSourceProfile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseDown);
            this.pbSourceProfile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseMove);
            this.pbSourceProfile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseUp);
            // 
            // lblThumbnail
            // 
            this.lblThumbnail.AutoSize = true;
            this.lblThumbnail.Location = new System.Drawing.Point(880, 157);
            this.lblThumbnail.Name = "lblThumbnail";
            this.lblThumbnail.Size = new System.Drawing.Size(56, 13);
            this.lblThumbnail.TabIndex = 21;
            this.lblThumbnail.Text = "Thumbnail";
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(875, 320);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(71, 13);
            this.lblProfile.TabIndex = 23;
            this.lblProfile.Text = "Official Profile";
            // 
            // pbOfficialProfile
            // 
            this.pbOfficialProfile.BackColor = System.Drawing.Color.White;
            this.pbOfficialProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOfficialProfile.Location = new System.Drawing.Point(873, 337);
            this.pbOfficialProfile.Name = "pbOfficialProfile";
            this.pbOfficialProfile.Size = new System.Drawing.Size(185, 278);
            this.pbOfficialProfile.TabIndex = 22;
            this.pbOfficialProfile.TabStop = false;
            // 
            // rbCircle
            // 
            this.rbCircle.AutoSize = true;
            this.rbCircle.Checked = true;
            this.rbCircle.Location = new System.Drawing.Point(993, 134);
            this.rbCircle.Name = "rbCircle";
            this.rbCircle.Size = new System.Drawing.Size(51, 17);
            this.rbCircle.TabIndex = 24;
            this.rbCircle.TabStop = true;
            this.rbCircle.Text = "Circle";
            this.rbCircle.UseVisualStyleBackColor = true;
            // 
            // rbRectangle
            // 
            this.rbRectangle.AutoSize = true;
            this.rbRectangle.Location = new System.Drawing.Point(993, 294);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(74, 17);
            this.rbRectangle.TabIndex = 25;
            this.rbRectangle.Text = "Rectangle";
            this.rbRectangle.UseVisualStyleBackColor = true;
            // 
            // btnMakeProfile
            // 
            this.btnMakeProfile.Location = new System.Drawing.Point(873, 294);
            this.btnMakeProfile.Name = "btnMakeProfile";
            this.btnMakeProfile.Size = new System.Drawing.Size(105, 23);
            this.btnMakeProfile.TabIndex = 26;
            this.btnMakeProfile.Text = "Make Profile";
            this.btnMakeProfile.UseVisualStyleBackColor = true;
            this.btnMakeProfile.Click += new System.EventHandler(this.btnMakeProfile_Click);
            // 
            // cbTestImages
            // 
            this.cbTestImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTestImages.FormattingEnabled = true;
            this.cbTestImages.Items.AddRange(new object[] {
            "SquareTestsm",
            "SquareTest",
            "SquareTestlg",
            "TallTestsm",
            "TallTest",
            "TallTestlg",
            "WideTestsm",
            "WideTest",
            "WideTestlg"});
            this.cbTestImages.Location = new System.Drawing.Point(856, 22);
            this.cbTestImages.Name = "cbTestImages";
            this.cbTestImages.Size = new System.Drawing.Size(211, 21);
            this.cbTestImages.TabIndex = 27;
            this.cbTestImages.SelectedIndexChanged += new System.EventHandler(this.cbTestImages_SelectedIndexChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(930, 647);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(38, 13);
            this.lblStart.TabIndex = 28;
            this.lblStart.Text = "SX,SY";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(1071, 816);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(41, 13);
            this.lblEnd.TabIndex = 29;
            this.lblEnd.Text = "EX, EY";
            // 
            // lbWidth
            // 
            this.lbWidth.AutoSize = true;
            this.lbWidth.Location = new System.Drawing.Point(933, 816);
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Size = new System.Drawing.Size(35, 13);
            this.lbWidth.TabIndex = 30;
            this.lbWidth.Text = "Width";
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Location = new System.Drawing.Point(834, 724);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(38, 13);
            this.lbHeight.TabIndex = 31;
            this.lbHeight.Text = "Height";
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(1052, 647);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(40, 13);
            this.lblRadius.TabIndex = 32;
            this.lblRadius.Text = "Radius";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblMouseNormal);
            this.panel1.Location = new System.Drawing.Point(933, 663);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 150);
            this.panel1.TabIndex = 33;
            // 
            // lblMouseNormal
            // 
            this.lblMouseNormal.AutoSize = true;
            this.lblMouseNormal.Location = new System.Drawing.Point(13, 61);
            this.lblMouseNormal.Name = "lblMouseNormal";
            this.lblMouseNormal.Size = new System.Drawing.Size(42, 13);
            this.lblMouseNormal.TabIndex = 29;
            this.lblMouseNormal.Text = "MX,MY";
            // 
            // CharacterImageMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 872);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.lbHeight);
            this.Controls.Add(this.lbWidth);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.cbTestImages);
            this.Controls.Add(this.btnMakeProfile);
            this.Controls.Add(this.rbRectangle);
            this.Controls.Add(this.rbCircle);
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.pbOfficialProfile);
            this.Controls.Add(this.lblThumbnail);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.btnMakeThumbNail);
            this.Controls.Add(this.pbDest);
            this.Controls.Add(this.pbSourceProfile);
            this.DoubleBuffered = true;
            this.Name = "CharacterImageMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CharacterImageMaker";
            this.Load += new System.EventHandler(this.CharacterImageMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSourceProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOfficialProfile)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnMakeThumbNail;
        private System.Windows.Forms.PictureBox pbDest;
        private System.Windows.Forms.PictureBox pbSourceProfile;
        private System.Windows.Forms.Label lblThumbnail;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.PictureBox pbOfficialProfile;
        private System.Windows.Forms.RadioButton rbCircle;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.Button btnMakeProfile;
        private System.Windows.Forms.ComboBox cbTestImages;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lbWidth;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMouseNormal;
    }
}