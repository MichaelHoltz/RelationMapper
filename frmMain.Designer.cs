namespace RelationMap
{
    partial class frmMain
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
            this.btnLoadFranchise = new System.Windows.Forms.Button();
            this.lbMovies = new System.Windows.Forms.ListBox();
            this.lbActors = new System.Windows.Forms.ListBox();
            this.lblMovies = new System.Windows.Forms.Label();
            this.btnSaveUniverse = new System.Windows.Forms.Button();
            this.lbCharacters = new System.Windows.Forms.ListBox();
            this.btnAddToMovie = new System.Windows.Forms.Button();
            this.lblActors = new System.Windows.Forms.Label();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.tbActor = new System.Windows.Forms.TextBox();
            this.tbCharacter = new System.Windows.Forms.TextBox();
            this.tbMovieTitle = new System.Windows.Forms.TextBox();
            this.lblMovieName = new System.Windows.Forms.Label();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.tbReleaseYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStudios = new System.Windows.Forms.ComboBox();
            this.lblStudios = new System.Windows.Forms.Label();
            this.lblFranchises = new System.Windows.Forms.Label();
            this.cbFranchises = new System.Windows.Forms.ComboBox();
            this.btnShowGraph = new System.Windows.Forms.Button();
            this.btnCharacterEditor = new System.Windows.Forms.Button();
            this.btnFindMovie = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadFranchise
            // 
            this.btnLoadFranchise.Location = new System.Drawing.Point(12, 12);
            this.btnLoadFranchise.Name = "btnLoadFranchise";
            this.btnLoadFranchise.Size = new System.Drawing.Size(125, 23);
            this.btnLoadFranchise.TabIndex = 0;
            this.btnLoadFranchise.Text = "Load Universe";
            this.btnLoadFranchise.UseVisualStyleBackColor = true;
            this.btnLoadFranchise.Click += new System.EventHandler(this.btnLoadFranchise_Click);
            // 
            // lbMovies
            // 
            this.lbMovies.FormattingEnabled = true;
            this.lbMovies.Location = new System.Drawing.Point(15, 240);
            this.lbMovies.Name = "lbMovies";
            this.lbMovies.Size = new System.Drawing.Size(188, 407);
            this.lbMovies.TabIndex = 1;
            this.lbMovies.TabStop = false;
            this.lbMovies.SelectedValueChanged += new System.EventHandler(this.lbMovies_SelectedValueChanged);
            // 
            // lbActors
            // 
            this.lbActors.FormattingEnabled = true;
            this.lbActors.Location = new System.Drawing.Point(392, 240);
            this.lbActors.Name = "lbActors";
            this.lbActors.Size = new System.Drawing.Size(167, 407);
            this.lbActors.TabIndex = 2;
            this.lbActors.SelectedValueChanged += new System.EventHandler(this.lbActors_SelectedValueChanged);
            // 
            // lblMovies
            // 
            this.lblMovies.AutoSize = true;
            this.lblMovies.Location = new System.Drawing.Point(16, 224);
            this.lblMovies.Name = "lblMovies";
            this.lblMovies.Size = new System.Drawing.Size(41, 13);
            this.lblMovies.TabIndex = 3;
            this.lblMovies.Text = "Movies";
            // 
            // btnSaveUniverse
            // 
            this.btnSaveUniverse.Location = new System.Drawing.Point(1068, 12);
            this.btnSaveUniverse.Name = "btnSaveUniverse";
            this.btnSaveUniverse.Size = new System.Drawing.Size(147, 23);
            this.btnSaveUniverse.TabIndex = 4;
            this.btnSaveUniverse.Text = "Save The Universe";
            this.btnSaveUniverse.UseVisualStyleBackColor = true;
            this.btnSaveUniverse.Click += new System.EventHandler(this.btnSaveUniverse_Click);
            // 
            // lbCharacters
            // 
            this.lbCharacters.FormattingEnabled = true;
            this.lbCharacters.Location = new System.Drawing.Point(219, 240);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(167, 407);
            this.lbCharacters.TabIndex = 5;
            this.lbCharacters.SelectedValueChanged += new System.EventHandler(this.lbCharacters_SelectedValueChanged);
            // 
            // btnAddToMovie
            // 
            this.btnAddToMovie.Enabled = false;
            this.btnAddToMovie.Location = new System.Drawing.Point(578, 212);
            this.btnAddToMovie.Name = "btnAddToMovie";
            this.btnAddToMovie.Size = new System.Drawing.Size(105, 23);
            this.btnAddToMovie.TabIndex = 9;
            this.btnAddToMovie.Text = "Add to Movie";
            this.btnAddToMovie.UseVisualStyleBackColor = true;
            this.btnAddToMovie.Click += new System.EventHandler(this.btnAddToMovie_Click);
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(389, 201);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(37, 13);
            this.lblActors.TabIndex = 7;
            this.lblActors.Text = "Actors";
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Location = new System.Drawing.Point(216, 201);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(58, 13);
            this.lblCharacters.TabIndex = 8;
            this.lblCharacters.Text = "Characters";
            // 
            // tbActor
            // 
            this.tbActor.Location = new System.Drawing.Point(392, 217);
            this.tbActor.Name = "tbActor";
            this.tbActor.Size = new System.Drawing.Size(167, 20);
            this.tbActor.TabIndex = 8;
            this.tbActor.TextChanged += new System.EventHandler(this.tbActor_TextChanged);
            // 
            // tbCharacter
            // 
            this.tbCharacter.Location = new System.Drawing.Point(219, 217);
            this.tbCharacter.Name = "tbCharacter";
            this.tbCharacter.Size = new System.Drawing.Size(167, 20);
            this.tbCharacter.TabIndex = 7;
            this.tbCharacter.TextChanged += new System.EventHandler(this.tbCharacter_TextChanged);
            // 
            // tbMovieTitle
            // 
            this.tbMovieTitle.Location = new System.Drawing.Point(622, 69);
            this.tbMovieTitle.Name = "tbMovieTitle";
            this.tbMovieTitle.Size = new System.Drawing.Size(167, 20);
            this.tbMovieTitle.TabIndex = 4;
            this.tbMovieTitle.TextChanged += new System.EventHandler(this.tbMovieTitle_TextChanged);
            // 
            // lblMovieName
            // 
            this.lblMovieName.AutoSize = true;
            this.lblMovieName.Location = new System.Drawing.Point(619, 53);
            this.lblMovieName.Name = "lblMovieName";
            this.lblMovieName.Size = new System.Drawing.Size(59, 13);
            this.lblMovieName.TabIndex = 16;
            this.lblMovieName.Text = "Movie Title";
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.Enabled = false;
            this.btnAddMovie.Location = new System.Drawing.Point(809, 64);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(105, 23);
            this.btnAddMovie.TabIndex = 6;
            this.btnAddMovie.Text = "Add  Movie";
            this.btnAddMovie.UseVisualStyleBackColor = true;
            this.btnAddMovie.Click += new System.EventHandler(this.btnAddMovie_Click);
            // 
            // tbReleaseYear
            // 
            this.tbReleaseYear.Location = new System.Drawing.Point(622, 112);
            this.tbReleaseYear.Name = "tbReleaseYear";
            this.tbReleaseYear.Size = new System.Drawing.Size(167, 20);
            this.tbReleaseYear.TabIndex = 5;
            this.tbReleaseYear.TextChanged += new System.EventHandler(this.tbReleaseYear_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Release Year";
            // 
            // cbStudios
            // 
            this.cbStudios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudios.FormattingEnabled = true;
            this.cbStudios.Location = new System.Drawing.Point(12, 111);
            this.cbStudios.Name = "cbStudios";
            this.cbStudios.Size = new System.Drawing.Size(177, 21);
            this.cbStudios.TabIndex = 1;
            this.cbStudios.SelectedValueChanged += new System.EventHandler(this.cbStudios_SelectedValueChanged);
            // 
            // lblStudios
            // 
            this.lblStudios.AutoSize = true;
            this.lblStudios.Location = new System.Drawing.Point(9, 95);
            this.lblStudios.Name = "lblStudios";
            this.lblStudios.Size = new System.Drawing.Size(42, 13);
            this.lblStudios.TabIndex = 21;
            this.lblStudios.Text = "Studios";
            // 
            // lblFranchises
            // 
            this.lblFranchises.AutoSize = true;
            this.lblFranchises.Location = new System.Drawing.Point(192, 96);
            this.lblFranchises.Name = "lblFranchises";
            this.lblFranchises.Size = new System.Drawing.Size(58, 13);
            this.lblFranchises.TabIndex = 23;
            this.lblFranchises.Text = "Franchises";
            // 
            // cbFranchises
            // 
            this.cbFranchises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFranchises.FormattingEnabled = true;
            this.cbFranchises.Location = new System.Drawing.Point(195, 112);
            this.cbFranchises.Name = "cbFranchises";
            this.cbFranchises.Size = new System.Drawing.Size(188, 21);
            this.cbFranchises.TabIndex = 2;
            this.cbFranchises.SelectedValueChanged += new System.EventHandler(this.cbFranchises_SelectedValueChanged);
            // 
            // btnShowGraph
            // 
            this.btnShowGraph.Location = new System.Drawing.Point(1081, 107);
            this.btnShowGraph.Name = "btnShowGraph";
            this.btnShowGraph.Size = new System.Drawing.Size(117, 23);
            this.btnShowGraph.TabIndex = 24;
            this.btnShowGraph.Text = "Show Graph";
            this.btnShowGraph.UseVisualStyleBackColor = true;
            this.btnShowGraph.Click += new System.EventHandler(this.btnShowGraph_Click);
            // 
            // btnCharacterEditor
            // 
            this.btnCharacterEditor.Location = new System.Drawing.Point(300, 193);
            this.btnCharacterEditor.Name = "btnCharacterEditor";
            this.btnCharacterEditor.Size = new System.Drawing.Size(75, 23);
            this.btnCharacterEditor.TabIndex = 27;
            this.btnCharacterEditor.Text = "Edit";
            this.btnCharacterEditor.UseVisualStyleBackColor = true;
            this.btnCharacterEditor.Click += new System.EventHandler(this.btnCharacterEditor_Click);
            // 
            // btnFindMovie
            // 
            this.btnFindMovie.Location = new System.Drawing.Point(622, 27);
            this.btnFindMovie.Name = "btnFindMovie";
            this.btnFindMovie.Size = new System.Drawing.Size(75, 23);
            this.btnFindMovie.TabIndex = 28;
            this.btnFindMovie.Text = "Find Movie";
            this.btnFindMovie.UseVisualStyleBackColor = true;
            this.btnFindMovie.Click += new System.EventHandler(this.btnFindMovie_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 794);
            this.Controls.Add(this.btnFindMovie);
            this.Controls.Add(this.btnCharacterEditor);
            this.Controls.Add(this.btnShowGraph);
            this.Controls.Add(this.lblFranchises);
            this.Controls.Add(this.cbFranchises);
            this.Controls.Add(this.lblStudios);
            this.Controls.Add(this.cbStudios);
            this.Controls.Add(this.tbReleaseYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMovieTitle);
            this.Controls.Add(this.lblMovieName);
            this.Controls.Add(this.btnAddMovie);
            this.Controls.Add(this.tbCharacter);
            this.Controls.Add(this.tbActor);
            this.Controls.Add(this.lblCharacters);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.btnAddToMovie);
            this.Controls.Add(this.lbCharacters);
            this.Controls.Add(this.btnSaveUniverse);
            this.Controls.Add(this.lblMovies);
            this.Controls.Add(this.lbActors);
            this.Controls.Add(this.lbMovies);
            this.Controls.Add(this.btnLoadFranchise);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RelationMaper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFranchise;
        private System.Windows.Forms.ListBox lbMovies;
        private System.Windows.Forms.ListBox lbActors;
        private System.Windows.Forms.Label lblMovies;
        private System.Windows.Forms.Button btnSaveUniverse;
        private System.Windows.Forms.ListBox lbCharacters;
        private System.Windows.Forms.Button btnAddToMovie;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.Label lblCharacters;
        private System.Windows.Forms.TextBox tbActor;
        private System.Windows.Forms.TextBox tbCharacter;
        private System.Windows.Forms.TextBox tbMovieTitle;
        private System.Windows.Forms.Label lblMovieName;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.TextBox tbReleaseYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStudios;
        private System.Windows.Forms.Label lblStudios;
        private System.Windows.Forms.Label lblFranchises;
        private System.Windows.Forms.ComboBox cbFranchises;
        private System.Windows.Forms.Button btnShowGraph;
        private System.Windows.Forms.Button btnCharacterEditor;
        private System.Windows.Forms.Button btnFindMovie;
    }
}

