namespace RelationMap.HelperForms
{
    partial class frmStudioGroupMaker
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
            this.lbProductionCompanies = new System.Windows.Forms.ListBox();
            this.lblProductionCompanies = new System.Windows.Forms.Label();
            this.lblStudioGroups = new System.Windows.Forms.Label();
            this.lbStudioGroups = new System.Windows.Forms.ListBox();
            this.btnCreateStudioGroup = new System.Windows.Forms.Button();
            this.pbMasterLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMasterLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbProductionCompanies
            // 
            this.lbProductionCompanies.FormattingEnabled = true;
            this.lbProductionCompanies.Location = new System.Drawing.Point(12, 59);
            this.lbProductionCompanies.Name = "lbProductionCompanies";
            this.lbProductionCompanies.Size = new System.Drawing.Size(270, 407);
            this.lbProductionCompanies.TabIndex = 31;
            this.lbProductionCompanies.TabStop = false;
            this.lbProductionCompanies.SelectedIndexChanged += new System.EventHandler(this.lbProductionCompanies_SelectedIndexChanged);
            // 
            // lblProductionCompanies
            // 
            this.lblProductionCompanies.AutoSize = true;
            this.lblProductionCompanies.Location = new System.Drawing.Point(9, 43);
            this.lblProductionCompanies.Name = "lblProductionCompanies";
            this.lblProductionCompanies.Size = new System.Drawing.Size(113, 13);
            this.lblProductionCompanies.TabIndex = 32;
            this.lblProductionCompanies.Text = "Production Companies";
            // 
            // lblStudioGroups
            // 
            this.lblStudioGroups.AutoSize = true;
            this.lblStudioGroups.Location = new System.Drawing.Point(596, 43);
            this.lblStudioGroups.Name = "lblStudioGroups";
            this.lblStudioGroups.Size = new System.Drawing.Size(74, 13);
            this.lblStudioGroups.TabIndex = 34;
            this.lblStudioGroups.Text = "Studio Groups";
            // 
            // lbStudioGroups
            // 
            this.lbStudioGroups.FormattingEnabled = true;
            this.lbStudioGroups.Location = new System.Drawing.Point(599, 59);
            this.lbStudioGroups.Name = "lbStudioGroups";
            this.lbStudioGroups.Size = new System.Drawing.Size(188, 407);
            this.lbStudioGroups.TabIndex = 33;
            this.lbStudioGroups.TabStop = false;
            // 
            // btnCreateStudioGroup
            // 
            this.btnCreateStudioGroup.Location = new System.Drawing.Point(364, 177);
            this.btnCreateStudioGroup.Name = "btnCreateStudioGroup";
            this.btnCreateStudioGroup.Size = new System.Drawing.Size(141, 23);
            this.btnCreateStudioGroup.TabIndex = 35;
            this.btnCreateStudioGroup.Text = "CreateStudioGroup";
            this.btnCreateStudioGroup.UseVisualStyleBackColor = true;
            // 
            // pbMasterLogo
            // 
            this.pbMasterLogo.BackColor = System.Drawing.Color.White;
            this.pbMasterLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbMasterLogo.Location = new System.Drawing.Point(364, 12);
            this.pbMasterLogo.Name = "pbMasterLogo";
            this.pbMasterLogo.Size = new System.Drawing.Size(45, 45);
            this.pbMasterLogo.TabIndex = 36;
            this.pbMasterLogo.TabStop = false;
            // 
            // frmStudioGroupMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 533);
            this.Controls.Add(this.pbMasterLogo);
            this.Controls.Add(this.btnCreateStudioGroup);
            this.Controls.Add(this.lblStudioGroups);
            this.Controls.Add(this.lbStudioGroups);
            this.Controls.Add(this.lblProductionCompanies);
            this.Controls.Add(this.lbProductionCompanies);
            this.Name = "frmStudioGroupMaker";
            this.Text = "Studio Collection Maker";
            this.Load += new System.EventHandler(this.frmStudioCollectionMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMasterLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbProductionCompanies;
        private System.Windows.Forms.Label lblProductionCompanies;
        private System.Windows.Forms.Label lblStudioGroups;
        private System.Windows.Forms.ListBox lbStudioGroups;
        private System.Windows.Forms.Button btnCreateStudioGroup;
        private System.Windows.Forms.PictureBox pbMasterLogo;
    }
}