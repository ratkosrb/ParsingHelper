namespace ParsingHelper
{
    partial class Form1
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
            this.txtWppDirectory = new System.Windows.Forms.TextBox();
            this.lblWppDirectory = new System.Windows.Forms.Label();
            this.btnBrowseWpp = new System.Windows.Forms.Button();
            this.btnBrowseSniffs = new System.Windows.Forms.Button();
            this.lblSniffsDirectory = new System.Windows.Forms.Label();
            this.txtSniffsDirectory = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lblMode = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.clmVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFileCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReverseSelection = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWppDirectory
            // 
            this.txtWppDirectory.Location = new System.Drawing.Point(12, 29);
            this.txtWppDirectory.Name = "txtWppDirectory";
            this.txtWppDirectory.Size = new System.Drawing.Size(376, 20);
            this.txtWppDirectory.TabIndex = 0;
            // 
            // lblWppDirectory
            // 
            this.lblWppDirectory.AutoSize = true;
            this.lblWppDirectory.Location = new System.Drawing.Point(12, 9);
            this.lblWppDirectory.Name = "lblWppDirectory";
            this.lblWppDirectory.Size = new System.Drawing.Size(80, 13);
            this.lblWppDirectory.TabIndex = 1;
            this.lblWppDirectory.Text = "WPP Directory:";
            // 
            // btnBrowseWpp
            // 
            this.btnBrowseWpp.Location = new System.Drawing.Point(394, 26);
            this.btnBrowseWpp.Name = "btnBrowseWpp";
            this.btnBrowseWpp.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseWpp.TabIndex = 2;
            this.btnBrowseWpp.Text = "Browse";
            this.btnBrowseWpp.UseVisualStyleBackColor = true;
            this.btnBrowseWpp.Click += new System.EventHandler(this.btnBrowseWpp_Click);
            // 
            // btnBrowseSniffs
            // 
            this.btnBrowseSniffs.Location = new System.Drawing.Point(394, 72);
            this.btnBrowseSniffs.Name = "btnBrowseSniffs";
            this.btnBrowseSniffs.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSniffs.TabIndex = 5;
            this.btnBrowseSniffs.Text = "Browse";
            this.btnBrowseSniffs.UseVisualStyleBackColor = true;
            this.btnBrowseSniffs.Click += new System.EventHandler(this.btnBrowseSniffs_Click);
            // 
            // lblSniffsDirectory
            // 
            this.lblSniffsDirectory.AutoSize = true;
            this.lblSniffsDirectory.Location = new System.Drawing.Point(12, 55);
            this.lblSniffsDirectory.Name = "lblSniffsDirectory";
            this.lblSniffsDirectory.Size = new System.Drawing.Size(81, 13);
            this.lblSniffsDirectory.TabIndex = 4;
            this.lblSniffsDirectory.Text = "Sniffs Directory:";
            // 
            // txtSniffsDirectory
            // 
            this.txtSniffsDirectory.Location = new System.Drawing.Point(12, 75);
            this.txtSniffsDirectory.Name = "txtSniffsDirectory";
            this.txtSniffsDirectory.Size = new System.Drawing.Size(376, 20);
            this.txtSniffsDirectory.TabIndex = 3;
            this.txtSniffsDirectory.Leave += new System.EventHandler(this.txtSniffsDirectory_Leave);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmVersion,
            this.columnFileCount});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 101);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(458, 259);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(14, 370);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(37, 13);
            this.lblMode.TabIndex = 7;
            this.lblMode.Text = "Mode:";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "Individual",
            "Mass"});
            this.cmbMode.Location = new System.Drawing.Point(57, 367);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(90, 21);
            this.cmbMode.TabIndex = 8;
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(395, 365);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 9;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // clmVersion
            // 
            this.clmVersion.Text = "Version";
            this.clmVersion.Width = 300;
            // 
            // columnFileCount
            // 
            this.columnFileCount.Text = "File Count";
            this.columnFileCount.Width = 130;
            // 
            // btnReverseSelection
            // 
            this.btnReverseSelection.Location = new System.Drawing.Point(282, 365);
            this.btnReverseSelection.Name = "btnReverseSelection";
            this.btnReverseSelection.Size = new System.Drawing.Size(107, 23);
            this.btnReverseSelection.TabIndex = 10;
            this.btnReverseSelection.Text = "Reverse Selection";
            this.btnReverseSelection.UseVisualStyleBackColor = true;
            this.btnReverseSelection.Click += new System.EventHandler(this.btnReverseSelection_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(197, 367);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(79, 20);
            this.txtName.TabIndex = 11;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(153, 370);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 393);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnReverseSelection);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnBrowseSniffs);
            this.Controls.Add(this.lblSniffsDirectory);
            this.Controls.Add(this.txtSniffsDirectory);
            this.Controls.Add(this.btnBrowseWpp);
            this.Controls.Add(this.lblWppDirectory);
            this.Controls.Add(this.txtWppDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Parsing Helper for Handicapped People";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWppDirectory;
        private System.Windows.Forms.Label lblWppDirectory;
        private System.Windows.Forms.Button btnBrowseWpp;
        private System.Windows.Forms.Button btnBrowseSniffs;
        private System.Windows.Forms.Label lblSniffsDirectory;
        private System.Windows.Forms.TextBox txtSniffsDirectory;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.ColumnHeader clmVersion;
        private System.Windows.Forms.ColumnHeader columnFileCount;
        private System.Windows.Forms.Button btnReverseSelection;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
    }
}

