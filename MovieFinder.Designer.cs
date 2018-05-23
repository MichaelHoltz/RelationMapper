namespace RelationMap
{
    partial class MovieFinder
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
            this.btnFind = new System.Windows.Forms.Button();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbMoviePoster = new System.Windows.Forms.PictureBox();
            this.lbMovies = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnUpdateMovie = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblProductionCompanies = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.movieInfoTip1 = new RelationMap.Controls.MovieInfoTip();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoviePoster)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(25, 67);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(25, 39);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(322, 20);
            this.tbTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(29, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            // 
            // pbMoviePoster
            // 
            this.pbMoviePoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbMoviePoster.Location = new System.Drawing.Point(348, 110);
            this.pbMoviePoster.Name = "pbMoviePoster";
            this.pbMoviePoster.Size = new System.Drawing.Size(342, 436);
            this.pbMoviePoster.TabIndex = 3;
            this.pbMoviePoster.TabStop = false;
            // 
            // lbMovies
            // 
            this.lbMovies.FormattingEnabled = true;
            this.lbMovies.Location = new System.Drawing.Point(25, 110);
            this.lbMovies.Name = "lbMovies";
            this.lbMovies.Size = new System.Drawing.Size(317, 433);
            this.lbMovies.TabIndex = 4;
            this.lbMovies.SelectedIndexChanged += new System.EventHandler(this.lbMovies_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(731, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(452, 543);
            this.propertyGrid1.TabIndex = 5;
            // 
            // btnUpdateMovie
            // 
            this.btnUpdateMovie.Location = new System.Drawing.Point(475, 81);
            this.btnUpdateMovie.Name = "btnUpdateMovie";
            this.btnUpdateMovie.Size = new System.Drawing.Size(137, 23);
            this.btnUpdateMovie.TabIndex = 7;
            this.btnUpdateMovie.Text = "Update Movie";
            this.btnUpdateMovie.UseVisualStyleBackColor = true;
            this.btnUpdateMovie.Click += new System.EventHandler(this.btnUpdateMovie_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(32, 574);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(191, 147);
            this.listBox1.TabIndex = 9;
            // 
            // lblProductionCompanies
            // 
            this.lblProductionCompanies.AutoSize = true;
            this.lblProductionCompanies.Location = new System.Drawing.Point(36, 559);
            this.lblProductionCompanies.Name = "lblProductionCompanies";
            this.lblProductionCompanies.Size = new System.Drawing.Size(113, 13);
            this.lblProductionCompanies.TabIndex = 10;
            this.lblProductionCompanies.Text = "Production Companies";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Location = new System.Drawing.Point(40, 743);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(57, 13);
            this.lblRevenue.TabIndex = 11;
            this.lblRevenue.Text = "Revenue: ";
            // 
            // movieInfoTip1
            // 
            this.movieInfoTip1.Location = new System.Drawing.Point(303, 574);
            this.movieInfoTip1.Name = "movieInfoTip1";
            this.movieInfoTip1.Size = new System.Drawing.Size(448, 149);
            this.movieInfoTip1.TabIndex = 12;
            // 
            // MovieFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 807);
            this.Controls.Add(this.movieInfoTip1);
            this.Controls.Add(this.lblRevenue);
            this.Controls.Add(this.lblProductionCompanies);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnUpdateMovie);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.lbMovies);
            this.Controls.Add(this.pbMoviePoster);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.btnFind);
            this.DoubleBuffered = true;
            this.Name = "MovieFinder";
            this.Text = "Manual Movie Finder";
            this.Load += new System.EventHandler(this.MovieFinder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMoviePoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbMoviePoster;
        private System.Windows.Forms.ListBox lbMovies;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnUpdateMovie;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblProductionCompanies;
        private System.Windows.Forms.Label lblRevenue;
        private Controls.MovieInfoTip movieInfoTip1;
    }
}