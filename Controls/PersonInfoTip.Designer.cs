namespace RelationMap.Controls
{
    partial class PersonInfoTip
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbProfile = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.lblPlaceOfBirth = new System.Windows.Forms.Label();
            this.lblBiography = new System.Windows.Forms.Label();
            this.pbRole = new System.Windows.Forms.PictureBox();
            this.pbRoleProfile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoleProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // pbProfile
            // 
            this.pbProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbProfile.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbProfile.Location = new System.Drawing.Point(0, 0);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(185, 276);
            this.pbProfile.TabIndex = 1;
            this.pbProfile.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(187, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 16);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.Red;
            this.lblRole.Location = new System.Drawing.Point(187, 2);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(41, 16);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Role";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Location = new System.Drawing.Point(187, 38);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(45, 13);
            this.lblBirthday.TabIndex = 4;
            this.lblBirthday.Text = "Birthday";
            // 
            // lblPlaceOfBirth
            // 
            this.lblPlaceOfBirth.AutoSize = true;
            this.lblPlaceOfBirth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPlaceOfBirth.Location = new System.Drawing.Point(187, 54);
            this.lblPlaceOfBirth.Name = "lblPlaceOfBirth";
            this.lblPlaceOfBirth.Size = new System.Drawing.Size(70, 13);
            this.lblPlaceOfBirth.TabIndex = 5;
            this.lblPlaceOfBirth.Text = "Place of Birth";
            this.lblPlaceOfBirth.Click += new System.EventHandler(this.lblPlaceOfBirth_Click);
            // 
            // lblBiography
            // 
            this.lblBiography.AutoEllipsis = true;
            this.lblBiography.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBiography.Location = new System.Drawing.Point(185, 81);
            this.lblBiography.Name = "lblBiography";
            this.lblBiography.Size = new System.Drawing.Size(413, 195);
            this.lblBiography.TabIndex = 7;
            this.lblBiography.Text = "Bio";
            // 
            // pbRole
            // 
            this.pbRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRole.Location = new System.Drawing.Point(537, 3);
            this.pbRole.Name = "pbRole";
            this.pbRole.Size = new System.Drawing.Size(60, 60);
            this.pbRole.TabIndex = 8;
            this.pbRole.TabStop = false;
            this.pbRole.Click += new System.EventHandler(this.pbRole_Click);
            // 
            // pbRoleProfile
            // 
            this.pbRoleProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRoleProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRoleProfile.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbRoleProfile.Location = new System.Drawing.Point(598, 0);
            this.pbRoleProfile.Name = "pbRoleProfile";
            this.pbRoleProfile.Size = new System.Drawing.Size(185, 276);
            this.pbRoleProfile.TabIndex = 9;
            this.pbRoleProfile.TabStop = false;
            // 
            // PersonInfoTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pbRole);
            this.Controls.Add(this.lblBiography);
            this.Controls.Add(this.lblPlaceOfBirth);
            this.Controls.Add(this.lblBirthday);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbProfile);
            this.Controls.Add(this.pbRoleProfile);
            this.DoubleBuffered = true;
            this.Name = "PersonInfoTip";
            this.Size = new System.Drawing.Size(783, 276);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoleProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbProfile;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.Label lblPlaceOfBirth;
        private System.Windows.Forms.Label lblBiography;
        private System.Windows.Forms.PictureBox pbRole;
        private System.Windows.Forms.PictureBox pbRoleProfile;
    }
}
