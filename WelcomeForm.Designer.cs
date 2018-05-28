namespace RelationMap
{
    partial class WelcomeForm
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
            this.btnTempGetStarted = new System.Windows.Forms.Button();
            this.lbMovies = new System.Windows.Forms.ListBox();
            this.tbTitleSearch = new System.Windows.Forms.TextBox();
            this.btnSaveToMyMovies = new System.Windows.Forms.Button();
            this.lbActors = new System.Windows.Forms.ListBox();
            this.btnFrmMain = new System.Windows.Forms.Button();
            this.btnCharacterFinder = new System.Windows.Forms.Button();
            this.personInfoTip1 = new RelationMap.Controls.PersonInfoTip();
            this.movieInfoTip1 = new RelationMap.Controls.MovieInfoTip();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTempGetStarted
            // 
            this.btnTempGetStarted.Location = new System.Drawing.Point(272, 12);
            this.btnTempGetStarted.Name = "btnTempGetStarted";
            this.btnTempGetStarted.Size = new System.Drawing.Size(161, 23);
            this.btnTempGetStarted.TabIndex = 0;
            this.btnTempGetStarted.Text = "Temp Get Started";
            this.btnTempGetStarted.UseVisualStyleBackColor = true;
            this.btnTempGetStarted.Click += new System.EventHandler(this.btnTempGetStarted_Click);
            // 
            // lbMovies
            // 
            this.lbMovies.FormattingEnabled = true;
            this.lbMovies.Location = new System.Drawing.Point(21, 65);
            this.lbMovies.Name = "lbMovies";
            this.lbMovies.Size = new System.Drawing.Size(391, 277);
            this.lbMovies.TabIndex = 1;
            this.lbMovies.SelectedIndexChanged += new System.EventHandler(this.lbMovies_SelectedIndexChanged);
            // 
            // tbTitleSearch
            // 
            this.tbTitleSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbTitleSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbTitleSearch.Location = new System.Drawing.Point(21, 12);
            this.tbTitleSearch.Name = "tbTitleSearch";
            this.tbTitleSearch.Size = new System.Drawing.Size(245, 20);
            this.tbTitleSearch.TabIndex = 3;
            // 
            // btnSaveToMyMovies
            // 
            this.btnSaveToMyMovies.Location = new System.Drawing.Point(525, 12);
            this.btnSaveToMyMovies.Name = "btnSaveToMyMovies";
            this.btnSaveToMyMovies.Size = new System.Drawing.Size(106, 23);
            this.btnSaveToMyMovies.TabIndex = 4;
            this.btnSaveToMyMovies.Text = "Save To Movies";
            this.btnSaveToMyMovies.UseVisualStyleBackColor = true;
            this.btnSaveToMyMovies.Click += new System.EventHandler(this.btnSaveToMyMovies_Click);
            // 
            // lbActors
            // 
            this.lbActors.FormattingEnabled = true;
            this.lbActors.Location = new System.Drawing.Point(200, 405);
            this.lbActors.Name = "lbActors";
            this.lbActors.Size = new System.Drawing.Size(212, 225);
            this.lbActors.TabIndex = 5;
            this.lbActors.SelectedIndexChanged += new System.EventHandler(this.lbActors_SelectedIndexChanged);
            // 
            // btnFrmMain
            // 
            this.btnFrmMain.Location = new System.Drawing.Point(678, 12);
            this.btnFrmMain.Name = "btnFrmMain";
            this.btnFrmMain.Size = new System.Drawing.Size(105, 23);
            this.btnFrmMain.TabIndex = 9;
            this.btnFrmMain.Text = "Show Main Form";
            this.btnFrmMain.UseVisualStyleBackColor = true;
            this.btnFrmMain.Click += new System.EventHandler(this.btnFrmMain_Click);
            // 
            // btnCharacterFinder
            // 
            this.btnCharacterFinder.Location = new System.Drawing.Point(247, 376);
            this.btnCharacterFinder.Name = "btnCharacterFinder";
            this.btnCharacterFinder.Size = new System.Drawing.Size(121, 23);
            this.btnCharacterFinder.TabIndex = 10;
            this.btnCharacterFinder.Text = "Character Finder";
            this.btnCharacterFinder.UseVisualStyleBackColor = true;
            this.btnCharacterFinder.Click += new System.EventHandler(this.btnCharacterFinder_Click);
            // 
            // personInfoTip1
            // 
            this.personInfoTip1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.personInfoTip1.Location = new System.Drawing.Point(441, 349);
            this.personInfoTip1.Name = "personInfoTip1";
            this.personInfoTip1.Size = new System.Drawing.Size(785, 278);
            this.personInfoTip1.TabIndex = 8;
            // 
            // movieInfoTip1
            // 
            this.movieInfoTip1.Location = new System.Drawing.Point(441, 65);
            this.movieInfoTip1.Name = "movieInfoTip1";
            this.movieInfoTip1.Size = new System.Drawing.Size(600, 278);
            this.movieInfoTip1.TabIndex = 2;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(53, 427);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnTransfer.TabIndex = 11;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // WelcomeForm
            // 
            this.AcceptButton = this.btnTempGetStarted;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 652);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnCharacterFinder);
            this.Controls.Add(this.btnFrmMain);
            this.Controls.Add(this.personInfoTip1);
            this.Controls.Add(this.lbActors);
            this.Controls.Add(this.btnSaveToMyMovies);
            this.Controls.Add(this.tbTitleSearch);
            this.Controls.Add(this.movieInfoTip1);
            this.Controls.Add(this.lbMovies);
            this.Controls.Add(this.btnTempGetStarted);
            this.Name = "WelcomeForm";
            this.Text = "Welcome Form";
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTempGetStarted;
        private System.Windows.Forms.ListBox lbMovies;
        private Controls.MovieInfoTip movieInfoTip1;
        private System.Windows.Forms.TextBox tbTitleSearch;
        private System.Windows.Forms.Button btnSaveToMyMovies;
        private System.Windows.Forms.ListBox lbActors;
        private Controls.PersonInfoTip personInfoTip1;
        private System.Windows.Forms.Button btnFrmMain;
        private System.Windows.Forms.Button btnCharacterFinder;
        private System.Windows.Forms.Button btnTransfer;
    }
}