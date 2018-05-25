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
            this.pbProfile = new System.Windows.Forms.PictureBox();
            this.lblThumbnail = new System.Windows.Forms.Label();
            this.lblProfile = new System.Windows.Forms.Label();
            this.pbOfficialProfile = new System.Windows.Forms.PictureBox();
            this.rbCircle = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.btnMakeProfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOfficialProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(478, 40);
            this.vScrollBar1.Minimum = -100;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(19, 695);
            this.vScrollBar1.TabIndex = 20;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(12, 738);
            this.hScrollBar1.Minimum = -100;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(463, 19);
            this.hScrollBar1.TabIndex = 19;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // btnMakeThumbNail
            // 
            this.btnMakeThumbNail.Location = new System.Drawing.Point(512, 139);
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
            this.pbDest.Location = new System.Drawing.Point(512, 182);
            this.pbDest.Name = "pbDest";
            this.pbDest.Size = new System.Drawing.Size(100, 100);
            this.pbDest.TabIndex = 17;
            this.pbDest.TabStop = false;
            // 
            // pbProfile
            // 
            this.pbProfile.BackColor = System.Drawing.Color.White;
            this.pbProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbProfile.Location = new System.Drawing.Point(12, 40);
            this.pbProfile.Margin = new System.Windows.Forms.Padding(0);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(463, 695);
            this.pbProfile.TabIndex = 16;
            this.pbProfile.TabStop = false;
            this.pbProfile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseDown);
            this.pbProfile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseMove);
            this.pbProfile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbProfile_MouseUp);
            // 
            // lblThumbnail
            // 
            this.lblThumbnail.AutoSize = true;
            this.lblThumbnail.Location = new System.Drawing.Point(514, 165);
            this.lblThumbnail.Name = "lblThumbnail";
            this.lblThumbnail.Size = new System.Drawing.Size(56, 13);
            this.lblThumbnail.TabIndex = 21;
            this.lblThumbnail.Text = "Thumbnail";
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(509, 328);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(71, 13);
            this.lblProfile.TabIndex = 23;
            this.lblProfile.Text = "Official Profile";
            // 
            // pbOfficialProfile
            // 
            this.pbOfficialProfile.BackColor = System.Drawing.Color.White;
            this.pbOfficialProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbOfficialProfile.Location = new System.Drawing.Point(507, 345);
            this.pbOfficialProfile.Name = "pbOfficialProfile";
            this.pbOfficialProfile.Size = new System.Drawing.Size(185, 278);
            this.pbOfficialProfile.TabIndex = 22;
            this.pbOfficialProfile.TabStop = false;
            // 
            // rbCircle
            // 
            this.rbCircle.AutoSize = true;
            this.rbCircle.Checked = true;
            this.rbCircle.Location = new System.Drawing.Point(627, 142);
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
            this.rbRectangle.Location = new System.Drawing.Point(627, 302);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(74, 17);
            this.rbRectangle.TabIndex = 25;
            this.rbRectangle.Text = "Rectangle";
            this.rbRectangle.UseVisualStyleBackColor = true;
            // 
            // btnMakeProfile
            // 
            this.btnMakeProfile.Location = new System.Drawing.Point(507, 302);
            this.btnMakeProfile.Name = "btnMakeProfile";
            this.btnMakeProfile.Size = new System.Drawing.Size(105, 23);
            this.btnMakeProfile.TabIndex = 26;
            this.btnMakeProfile.Text = "Make Profile";
            this.btnMakeProfile.UseVisualStyleBackColor = true;
            this.btnMakeProfile.Click += new System.EventHandler(this.btnMakeProfile_Click);
            // 
            // CharacterImageMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 775);
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
            this.Controls.Add(this.pbProfile);
            this.DoubleBuffered = true;
            this.Name = "CharacterImageMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CharacterImageMaker";
            this.Load += new System.EventHandler(this.CharacterImageMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOfficialProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnMakeThumbNail;
        private System.Windows.Forms.PictureBox pbDest;
        private System.Windows.Forms.PictureBox pbProfile;
        private System.Windows.Forms.Label lblThumbnail;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.PictureBox pbOfficialProfile;
        private System.Windows.Forms.RadioButton rbCircle;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.Button btnMakeProfile;
    }
}