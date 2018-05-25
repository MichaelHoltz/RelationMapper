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
            this.lblCharacters = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.flpThumbNails = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPossibleImages = new System.Windows.Forms.Label();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Location = new System.Drawing.Point(12, 74);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(58, 13);
            this.lblCharacters.TabIndex = 4;
            this.lblCharacters.Text = "Characters";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Search Google";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flpThumbNails
            // 
            this.flpThumbNails.AutoScroll = true;
            this.flpThumbNails.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.flpThumbNails.AutoScrollMinSize = new System.Drawing.Size(650, 450);
            this.flpThumbNails.AutoSize = true;
            this.flpThumbNails.Location = new System.Drawing.Point(391, 90);
            this.flpThumbNails.MaximumSize = new System.Drawing.Size(700, 450);
            this.flpThumbNails.MinimumSize = new System.Drawing.Size(700, 450);
            this.flpThumbNails.Name = "flpThumbNails";
            this.flpThumbNails.Size = new System.Drawing.Size(700, 450);
            this.flpThumbNails.TabIndex = 8;
            // 
            // lblPossibleImages
            // 
            this.lblPossibleImages.AutoSize = true;
            this.lblPossibleImages.Location = new System.Drawing.Point(388, 74);
            this.lblPossibleImages.Name = "lblPossibleImages";
            this.lblPossibleImages.Size = new System.Drawing.Size(73, 13);
            this.lblPossibleImages.TabIndex = 9;
            this.lblPossibleImages.Text = "Image Search";
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(544, 25);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(149, 23);
            this.btnLoadFromFile.TabIndex = 10;
            this.btnLoadFromFile.Text = "LoadImagesfrom file";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1121, 97);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CharacterFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 636);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.lblPossibleImages);
            this.Controls.Add(this.flpThumbNails);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCharacters);
            this.Controls.Add(this.lbCharacters);
            this.Controls.Add(this.lblMovie);
            this.Controls.Add(this.cbMovie);
            this.DoubleBuffered = true;
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
        private System.Windows.Forms.Label lblCharacters;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flpThumbNails;
        private System.Windows.Forms.Label lblPossibleImages;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.Button button2;
    }
}