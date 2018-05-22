namespace RelationMap.Controls
{
    partial class MovieInfoTip
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
            this.pbPoster = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRunTime = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.flpProductionCompanies = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPoster
            // 
            this.pbPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPoster.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbPoster.Location = new System.Drawing.Point(0, 0);
            this.pbPoster.Name = "pbPoster";
            this.pbPoster.Size = new System.Drawing.Size(119, 149);
            this.pbPoster.TabIndex = 0;
            this.pbPoster.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(120, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblRunTime
            // 
            this.lblRunTime.AutoSize = true;
            this.lblRunTime.Location = new System.Drawing.Point(120, 21);
            this.lblRunTime.Name = "lblRunTime";
            this.lblRunTime.Size = new System.Drawing.Size(50, 13);
            this.lblRunTime.TabIndex = 2;
            this.lblRunTime.Text = "RunTime";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Location = new System.Drawing.Point(120, 37);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(51, 13);
            this.lblRevenue.TabIndex = 3;
            this.lblRevenue.Text = "Revenue";
            // 
            // flpProductionCompanies
            // 
            this.flpProductionCompanies.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpProductionCompanies.Location = new System.Drawing.Point(119, 99);
            this.flpProductionCompanies.Name = "flpProductionCompanies";
            this.flpProductionCompanies.Size = new System.Drawing.Size(329, 50);
            this.flpProductionCompanies.TabIndex = 4;
            // 
            // MovieInfoTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpProductionCompanies);
            this.Controls.Add(this.lblRevenue);
            this.Controls.Add(this.lblRunTime);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbPoster);
            this.DoubleBuffered = true;
            this.Name = "MovieInfoTip";
            this.Size = new System.Drawing.Size(448, 149);
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPoster;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRunTime;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.FlowLayoutPanel flpProductionCompanies;
    }
}
