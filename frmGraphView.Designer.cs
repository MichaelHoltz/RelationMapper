namespace RelationMap
{
    partial class frmGraphView
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pTopBar = new System.Windows.Forms.Panel();
            this.lblFranchises = new System.Windows.Forms.Label();
            this.cbFranchises = new System.Windows.Forms.ComboBox();
            this.lblStudios = new System.Windows.Forms.Label();
            this.cbStudios = new System.Windows.Forms.ComboBox();
            this.infoTip1 = new RelationMap.InfoTip();
            this.cbGateways = new System.Windows.Forms.CheckBox();
            this.cbLoaders = new System.Windows.Forms.CheckBox();
            this.cbTemplates = new System.Windows.Forms.CheckBox();
            this.lblSelectedNode = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbFlow = new System.Windows.Forms.GroupBox();
            this.rbBT = new System.Windows.Forms.RadioButton();
            this.rbRL = new System.Windows.Forms.RadioButton();
            this.rbTB = new System.Windows.Forms.RadioButton();
            this.rbLR = new System.Windows.Forms.RadioButton();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pTopBar.SuspendLayout();
            this.gbFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "FWDM - Dynamic Graph";
            // 
            // pTopBar
            // 
            this.pTopBar.Controls.Add(this.btnSearch);
            this.pTopBar.Controls.Add(this.tbSearch);
            this.pTopBar.Controls.Add(this.lblFranchises);
            this.pTopBar.Controls.Add(this.cbFranchises);
            this.pTopBar.Controls.Add(this.lblStudios);
            this.pTopBar.Controls.Add(this.cbStudios);
            this.pTopBar.Controls.Add(this.infoTip1);
            this.pTopBar.Controls.Add(this.cbGateways);
            this.pTopBar.Controls.Add(this.cbLoaders);
            this.pTopBar.Controls.Add(this.cbTemplates);
            this.pTopBar.Controls.Add(this.lblSelectedNode);
            this.pTopBar.Controls.Add(this.button2);
            this.pTopBar.Controls.Add(this.button1);
            this.pTopBar.Controls.Add(this.gbFlow);
            this.pTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTopBar.Location = new System.Drawing.Point(0, 0);
            this.pTopBar.Name = "pTopBar";
            this.pTopBar.Size = new System.Drawing.Size(1341, 114);
            this.pTopBar.TabIndex = 0;
            // 
            // lblFranchises
            // 
            this.lblFranchises.AutoSize = true;
            this.lblFranchises.Location = new System.Drawing.Point(896, 75);
            this.lblFranchises.Name = "lblFranchises";
            this.lblFranchises.Size = new System.Drawing.Size(58, 13);
            this.lblFranchises.TabIndex = 27;
            this.lblFranchises.Text = "Franchises";
            // 
            // cbFranchises
            // 
            this.cbFranchises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFranchises.FormattingEnabled = true;
            this.cbFranchises.Location = new System.Drawing.Point(899, 91);
            this.cbFranchises.Name = "cbFranchises";
            this.cbFranchises.Size = new System.Drawing.Size(188, 21);
            this.cbFranchises.TabIndex = 25;
            this.cbFranchises.SelectedValueChanged += new System.EventHandler(this.cbFranchises_SelectedValueChanged);
            // 
            // lblStudios
            // 
            this.lblStudios.AutoSize = true;
            this.lblStudios.Location = new System.Drawing.Point(713, 74);
            this.lblStudios.Name = "lblStudios";
            this.lblStudios.Size = new System.Drawing.Size(42, 13);
            this.lblStudios.TabIndex = 26;
            this.lblStudios.Text = "Studios";
            // 
            // cbStudios
            // 
            this.cbStudios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudios.FormattingEnabled = true;
            this.cbStudios.Location = new System.Drawing.Point(716, 90);
            this.cbStudios.Name = "cbStudios";
            this.cbStudios.Size = new System.Drawing.Size(177, 21);
            this.cbStudios.TabIndex = 24;
            this.cbStudios.SelectedValueChanged += new System.EventHandler(this.cbStudios_SelectedValueChanged);
            // 
            // infoTip1
            // 
            this.infoTip1.Location = new System.Drawing.Point(3, 31);
            this.infoTip1.Name = "infoTip1";
            this.infoTip1.Size = new System.Drawing.Size(590, 81);
            this.infoTip1.TabIndex = 10;
            // 
            // cbGateways
            // 
            this.cbGateways.AutoSize = true;
            this.cbGateways.Location = new System.Drawing.Point(613, 64);
            this.cbGateways.Name = "cbGateways";
            this.cbGateways.Size = new System.Drawing.Size(73, 17);
            this.cbGateways.TabIndex = 9;
            this.cbGateways.Text = "Gateways";
            this.cbGateways.UseVisualStyleBackColor = true;
            this.cbGateways.CheckedChanged += new System.EventHandler(this.ViewOptions_CheckedChanged);
            // 
            // cbLoaders
            // 
            this.cbLoaders.AutoSize = true;
            this.cbLoaders.Location = new System.Drawing.Point(613, 41);
            this.cbLoaders.Name = "cbLoaders";
            this.cbLoaders.Size = new System.Drawing.Size(64, 17);
            this.cbLoaders.TabIndex = 8;
            this.cbLoaders.Text = "Loaders";
            this.cbLoaders.UseVisualStyleBackColor = true;
            this.cbLoaders.CheckedChanged += new System.EventHandler(this.ViewOptions_CheckedChanged);
            // 
            // cbTemplates
            // 
            this.cbTemplates.AutoSize = true;
            this.cbTemplates.Location = new System.Drawing.Point(613, 18);
            this.cbTemplates.Name = "cbTemplates";
            this.cbTemplates.Size = new System.Drawing.Size(75, 17);
            this.cbTemplates.TabIndex = 7;
            this.cbTemplates.Text = "Templates";
            this.cbTemplates.UseVisualStyleBackColor = true;
            this.cbTemplates.CheckedChanged += new System.EventHandler(this.ViewOptions_CheckedChanged);
            // 
            // lblSelectedNode
            // 
            this.lblSelectedNode.AutoSize = true;
            this.lblSelectedNode.Location = new System.Drawing.Point(9, 9);
            this.lblSelectedNode.Name = "lblSelectedNode";
            this.lblSelectedNode.Size = new System.Drawing.Size(84, 13);
            this.lblSelectedNode.TabIndex = 6;
            this.lblSelectedNode.Text = "Selected Node: ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(863, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Sugiyama Layout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(863, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "MDS Layout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbFlow
            // 
            this.gbFlow.Controls.Add(this.rbBT);
            this.gbFlow.Controls.Add(this.rbRL);
            this.gbFlow.Controls.Add(this.rbTB);
            this.gbFlow.Controls.Add(this.rbLR);
            this.gbFlow.Location = new System.Drawing.Point(987, 12);
            this.gbFlow.Name = "gbFlow";
            this.gbFlow.Size = new System.Drawing.Size(272, 48);
            this.gbFlow.TabIndex = 1;
            this.gbFlow.TabStop = false;
            this.gbFlow.Text = "Flow Option";
            // 
            // rbBT
            // 
            this.rbBT.AutoSize = true;
            this.rbBT.Location = new System.Drawing.Point(183, 19);
            this.rbBT.Name = "rbBT";
            this.rbBT.Size = new System.Drawing.Size(39, 17);
            this.rbBT.TabIndex = 3;
            this.rbBT.Text = "BT";
            this.rbBT.UseVisualStyleBackColor = true;
            this.rbBT.CheckedChanged += new System.EventHandler(this.rbLR_CheckedChanged);
            // 
            // rbRL
            // 
            this.rbRL.AutoSize = true;
            this.rbRL.Location = new System.Drawing.Point(68, 19);
            this.rbRL.Name = "rbRL";
            this.rbRL.Size = new System.Drawing.Size(39, 17);
            this.rbRL.TabIndex = 2;
            this.rbRL.Text = "RL";
            this.rbRL.UseVisualStyleBackColor = true;
            this.rbRL.CheckedChanged += new System.EventHandler(this.rbLR_CheckedChanged);
            // 
            // rbTB
            // 
            this.rbTB.AutoSize = true;
            this.rbTB.Location = new System.Drawing.Point(127, 19);
            this.rbTB.Name = "rbTB";
            this.rbTB.Size = new System.Drawing.Size(39, 17);
            this.rbTB.TabIndex = 1;
            this.rbTB.Text = "TB";
            this.rbTB.UseVisualStyleBackColor = true;
            this.rbTB.CheckedChanged += new System.EventHandler(this.rbLR_CheckedChanged);
            // 
            // rbLR
            // 
            this.rbLR.AutoSize = true;
            this.rbLR.Checked = true;
            this.rbLR.Location = new System.Drawing.Point(23, 19);
            this.rbLR.Name = "rbLR";
            this.rbLR.Size = new System.Drawing.Size(39, 17);
            this.rbLR.TabIndex = 0;
            this.rbLR.TabStop = true;
            this.rbLR.Text = "LR";
            this.rbLR.UseVisualStyleBackColor = true;
            this.rbLR.CheckedChanged += new System.EventHandler(this.rbLR_CheckedChanged);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(726, 30);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(100, 20);
            this.tbSearch.TabIndex = 28;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(774, 57);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmGraphView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 679);
            this.Controls.Add(this.pTopBar);
            this.Name = "frmGraphView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FWDM Dynamic Machine Graph";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGraphView_Load);
            this.pTopBar.ResumeLayout(false);
            this.pTopBar.PerformLayout();
            this.gbFlow.ResumeLayout(false);
            this.gbFlow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pTopBar;
        private System.Windows.Forms.GroupBox gbFlow;
        private System.Windows.Forms.RadioButton rbTB;
        private System.Windows.Forms.RadioButton rbLR;
        private System.Windows.Forms.RadioButton rbRL;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbBT;
        private System.Windows.Forms.Label lblSelectedNode;
        private System.Windows.Forms.CheckBox cbGateways;
        private System.Windows.Forms.CheckBox cbLoaders;
        private System.Windows.Forms.CheckBox cbTemplates;
        private InfoTip infoTip1;
        private System.Windows.Forms.Label lblFranchises;
        private System.Windows.Forms.ComboBox cbFranchises;
        private System.Windows.Forms.Label lblStudios;
        private System.Windows.Forms.ComboBox cbStudios;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbSearch;
    }
}