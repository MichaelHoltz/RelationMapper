namespace RelationMap
{
    partial class CharacterFinder
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
            this.cbMovie = new System.Windows.Forms.ComboBox();
            this.lblMovie = new System.Windows.Forms.Label();
            this.lbCharacters = new System.Windows.Forms.ListBox();
            this.lbActors = new System.Windows.Forms.ListBox();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.lblActors = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbMovie
            // 
            this.cbMovie.FormattingEnabled = true;
            this.cbMovie.Location = new System.Drawing.Point(12, 25);
            this.cbMovie.Name = "cbMovie";
            this.cbMovie.Size = new System.Drawing.Size(378, 21);
            this.cbMovie.TabIndex = 0;
            this.cbMovie.SelectedIndexChanged += new System.EventHandler(this.cbMovie_SelectedIndexChanged);
            // 
            // lblMovie
            // 
            this.lblMovie.AutoSize = true;
            this.lblMovie.Location = new System.Drawing.Point(12, 9);
            this.lblMovie.Name = "lblMovie";
            this.lblMovie.Size = new System.Drawing.Size(36, 13);
            this.lblMovie.TabIndex = 1;
            this.lblMovie.Text = "Movie";
            // 
            // lbCharacters
            // 
            this.lbCharacters.FormattingEnabled = true;
            this.lbCharacters.Location = new System.Drawing.Point(13, 90);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(332, 433);
            this.lbCharacters.TabIndex = 2;
            this.lbCharacters.SelectedIndexChanged += new System.EventHandler(this.lbCharacter_SelectedIndexChanged);
            // 
            // lbActors
            // 
            this.lbActors.FormattingEnabled = true;
            this.lbActors.Location = new System.Drawing.Point(362, 90);
            this.lbActors.Name = "lbActors";
            this.lbActors.Size = new System.Drawing.Size(332, 433);
            this.lbActors.TabIndex = 3;
            this.lbActors.SelectedIndexChanged += new System.EventHandler(this.lbActor_SelectedIndexChanged);
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Location = new System.Drawing.Point(12, 74);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(58, 13);
            this.lblCharacters.TabIndex = 4;
            this.lblCharacters.Text = "Characters";
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(359, 74);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(37, 13);
            this.lblActors.TabIndex = 5;
            this.lblActors.Text = "Actors";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(734, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(383, 541);
            this.propertyGrid1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CharacterFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 541);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.lblCharacters);
            this.Controls.Add(this.lbActors);
            this.Controls.Add(this.lbCharacters);
            this.Controls.Add(this.lblMovie);
            this.Controls.Add(this.cbMovie);
            this.Name = "CharacterFinder";
            this.Text = "CharacterFinder";
            this.Load += new System.EventHandler(this.CharacterFinder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMovie;
        private System.Windows.Forms.Label lblMovie;
        private System.Windows.Forms.ListBox lbCharacters;
        private System.Windows.Forms.ListBox lbActors;
        private System.Windows.Forms.Label lblCharacters;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
    }
}