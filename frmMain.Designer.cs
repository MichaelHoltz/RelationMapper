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
            this.lblActors = new System.Windows.Forms.Label();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.tbActor = new System.Windows.Forms.TextBox();
            this.tbCharacter = new System.Windows.Forms.TextBox();
            this.cbStudios = new System.Windows.Forms.ComboBox();
            this.lblStudioGroups = new System.Windows.Forms.Label();
            this.lblFranchises = new System.Windows.Forms.Label();
            this.cbFranchises = new System.Windows.Forms.ComboBox();
            this.btnShowGraph = new System.Windows.Forms.Button();
            this.btnCharacterEditor = new System.Windows.Forms.Button();
            this.lblProductionCompanies = new System.Windows.Forms.Label();
            this.cbProductionCompanies = new System.Windows.Forms.ComboBox();
            this.lbProductionCompanies = new System.Windows.Forms.ListBox();
            this.btnStudioGroupMaker = new System.Windows.Forms.Button();
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
            this.lbActors.Location = new System.Drawing.Point(942, 252);
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
            this.lbCharacters.Location = new System.Drawing.Point(769, 252);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(167, 407);
            this.lbCharacters.TabIndex = 5;
            this.lbCharacters.SelectedValueChanged += new System.EventHandler(this.lbCharacters_SelectedValueChanged);
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(939, 213);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(37, 13);
            this.lblActors.TabIndex = 7;
            this.lblActors.Text = "Actors";
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Location = new System.Drawing.Point(766, 213);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(58, 13);
            this.lblCharacters.TabIndex = 8;
            this.lblCharacters.Text = "Characters";
            // 
            // tbActor
            // 
            this.tbActor.Location = new System.Drawing.Point(942, 229);
            this.tbActor.Name = "tbActor";
            this.tbActor.Size = new System.Drawing.Size(167, 20);
            this.tbActor.TabIndex = 8;
            this.tbActor.TextChanged += new System.EventHandler(this.tbActor_TextChanged);
            // 
            // tbCharacter
            // 
            this.tbCharacter.Location = new System.Drawing.Point(769, 229);
            this.tbCharacter.Name = "tbCharacter";
            this.tbCharacter.Size = new System.Drawing.Size(167, 20);
            this.tbCharacter.TabIndex = 7;
            this.tbCharacter.TextChanged += new System.EventHandler(this.tbCharacter_TextChanged);
            // 
            // cbStudios
            // 
            this.cbStudios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudios.FormattingEnabled = true;
            this.cbStudios.Location = new System.Drawing.Point(228, 109);
            this.cbStudios.Name = "cbStudios";
            this.cbStudios.Size = new System.Drawing.Size(177, 21);
            this.cbStudios.TabIndex = 1;
            this.cbStudios.SelectedValueChanged += new System.EventHandler(this.cbStudios_SelectedValueChanged);
            // 
            // lblStudioGroups
            // 
            this.lblStudioGroups.AutoSize = true;
            this.lblStudioGroups.Location = new System.Drawing.Point(225, 93);
            this.lblStudioGroups.Name = "lblStudioGroups";
            this.lblStudioGroups.Size = new System.Drawing.Size(71, 13);
            this.lblStudioGroups.TabIndex = 21;
            this.lblStudioGroups.Text = "StudioGroups";
            // 
            // lblFranchises
            // 
            this.lblFranchises.AutoSize = true;
            this.lblFranchises.Location = new System.Drawing.Point(758, 39);
            this.lblFranchises.Name = "lblFranchises";
            this.lblFranchises.Size = new System.Drawing.Size(58, 13);
            this.lblFranchises.TabIndex = 23;
            this.lblFranchises.Text = "Franchises";
            // 
            // cbFranchises
            // 
            this.cbFranchises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFranchises.FormattingEnabled = true;
            this.cbFranchises.Location = new System.Drawing.Point(761, 55);
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
            this.btnCharacterEditor.Location = new System.Drawing.Point(850, 205);
            this.btnCharacterEditor.Name = "btnCharacterEditor";
            this.btnCharacterEditor.Size = new System.Drawing.Size(75, 23);
            this.btnCharacterEditor.TabIndex = 27;
            this.btnCharacterEditor.Text = "Edit";
            this.btnCharacterEditor.UseVisualStyleBackColor = true;
            this.btnCharacterEditor.Click += new System.EventHandler(this.btnCharacterEditor_Click);
            // 
            // lblProductionCompanies
            // 
            this.lblProductionCompanies.AutoSize = true;
            this.lblProductionCompanies.Location = new System.Drawing.Point(225, 180);
            this.lblProductionCompanies.Name = "lblProductionCompanies";
            this.lblProductionCompanies.Size = new System.Drawing.Size(113, 13);
            this.lblProductionCompanies.TabIndex = 29;
            this.lblProductionCompanies.Text = "Production Companies";
            // 
            // cbProductionCompanies
            // 
            this.cbProductionCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductionCompanies.FormattingEnabled = true;
            this.cbProductionCompanies.Location = new System.Drawing.Point(228, 196);
            this.cbProductionCompanies.Name = "cbProductionCompanies";
            this.cbProductionCompanies.Size = new System.Drawing.Size(177, 21);
            this.cbProductionCompanies.TabIndex = 28;
            // 
            // lbProductionCompanies
            // 
            this.lbProductionCompanies.FormattingEnabled = true;
            this.lbProductionCompanies.Location = new System.Drawing.Point(224, 240);
            this.lbProductionCompanies.Name = "lbProductionCompanies";
            this.lbProductionCompanies.Size = new System.Drawing.Size(270, 407);
            this.lbProductionCompanies.TabIndex = 30;
            this.lbProductionCompanies.TabStop = false;
            // 
            // btnStudioGroupMaker
            // 
            this.btnStudioGroupMaker.Location = new System.Drawing.Point(411, 147);
            this.btnStudioGroupMaker.Name = "btnStudioGroupMaker";
            this.btnStudioGroupMaker.Size = new System.Drawing.Size(111, 23);
            this.btnStudioGroupMaker.TabIndex = 31;
            this.btnStudioGroupMaker.Text = "Edit Studio Groups";
            this.btnStudioGroupMaker.UseVisualStyleBackColor = true;
            this.btnStudioGroupMaker.Click += new System.EventHandler(this.btnStudioGroupMaker_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 794);
            this.Controls.Add(this.btnStudioGroupMaker);
            this.Controls.Add(this.lbProductionCompanies);
            this.Controls.Add(this.lblProductionCompanies);
            this.Controls.Add(this.cbProductionCompanies);
            this.Controls.Add(this.btnCharacterEditor);
            this.Controls.Add(this.btnShowGraph);
            this.Controls.Add(this.lblFranchises);
            this.Controls.Add(this.cbFranchises);
            this.Controls.Add(this.lblStudioGroups);
            this.Controls.Add(this.cbStudios);
            this.Controls.Add(this.tbCharacter);
            this.Controls.Add(this.tbActor);
            this.Controls.Add(this.lblCharacters);
            this.Controls.Add(this.lblActors);
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
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.Label lblCharacters;
        private System.Windows.Forms.TextBox tbActor;
        private System.Windows.Forms.TextBox tbCharacter;
        private System.Windows.Forms.ComboBox cbStudios;
        private System.Windows.Forms.Label lblStudioGroups;
        private System.Windows.Forms.Label lblFranchises;
        private System.Windows.Forms.ComboBox cbFranchises;
        private System.Windows.Forms.Button btnShowGraph;
        private System.Windows.Forms.Button btnCharacterEditor;
        private System.Windows.Forms.Label lblProductionCompanies;
        private System.Windows.Forms.ComboBox cbProductionCompanies;
        private System.Windows.Forms.ListBox lbProductionCompanies;
        private System.Windows.Forms.Button btnStudioGroupMaker;
    }
}

