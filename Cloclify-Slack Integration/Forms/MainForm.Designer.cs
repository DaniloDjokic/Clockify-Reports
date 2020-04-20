namespace Cloclify_Slack_Integration
{
    partial class MainForm
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
            this.getRecordsButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.savePathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.workspacesDropdown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.allProjectsListBox = new System.Windows.Forms.ListBox();
            this.selectedProjectsListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // getRecordsButton
            // 
            this.getRecordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getRecordsButton.Location = new System.Drawing.Point(12, 403);
            this.getRecordsButton.Name = "getRecordsButton";
            this.getRecordsButton.Size = new System.Drawing.Size(204, 71);
            this.getRecordsButton.TabIndex = 0;
            this.getRecordsButton.Text = "Get Report";
            this.getRecordsButton.UseVisualStyleBackColor = true;
            this.getRecordsButton.Click += new System.EventHandler(this.getRecordsButton_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(12, 9);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(86, 31);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "label1";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(199, 114);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(250, 20);
            this.datePicker.TabIndex = 2;
            // 
            // savePathTextBox
            // 
            this.savePathTextBox.Location = new System.Drawing.Point(199, 157);
            this.savePathTextBox.Name = "savePathTextBox";
            this.savePathTextBox.ReadOnly = true;
            this.savePathTextBox.Size = new System.Drawing.Size(214, 20);
            this.savePathTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Save path";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.BackgroundImage = global::Cloclify_Slack_Integration.Properties.Resources.folder_512;
            this.selectFolderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectFolderButton.Location = new System.Drawing.Point(419, 157);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(30, 20);
            this.selectFolderButton.TabIndex = 6;
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // workspacesDropdown
            // 
            this.workspacesDropdown.FormattingEnabled = true;
            this.workspacesDropdown.Location = new System.Drawing.Point(199, 69);
            this.workspacesDropdown.Name = "workspacesDropdown";
            this.workspacesDropdown.Size = new System.Drawing.Size(250, 21);
            this.workspacesDropdown.TabIndex = 7;
            this.workspacesDropdown.SelectedIndexChanged += new System.EventHandler(this.workspacesDropdown_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Workspace";
            // 
            // allProjectsListBox
            // 
            this.allProjectsListBox.FormattingEnabled = true;
            this.allProjectsListBox.Location = new System.Drawing.Point(12, 229);
            this.allProjectsListBox.Name = "allProjectsListBox";
            this.allProjectsListBox.Size = new System.Drawing.Size(204, 160);
            this.allProjectsListBox.TabIndex = 9;
            this.allProjectsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.allProjectsListBox_MouseDoubleClick);
            // 
            // selectedProjectsListBox
            // 
            this.selectedProjectsListBox.FormattingEnabled = true;
            this.selectedProjectsListBox.Location = new System.Drawing.Point(243, 229);
            this.selectedProjectsListBox.Name = "selectedProjectsListBox";
            this.selectedProjectsListBox.Size = new System.Drawing.Size(206, 160);
            this.selectedProjectsListBox.TabIndex = 10;
            this.selectedProjectsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectedProjectsListBox_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "All Projects";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(255, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Selected Projects";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(213, 403);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(236, 71);
            this.statusLabel.TabIndex = 13;
            this.statusLabel.Text = "Status";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(461, 498);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.selectedProjectsListBox);
            this.Controls.Add(this.allProjectsListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workspacesDropdown);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.savePathTextBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.getRecordsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getRecordsButton;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.TextBox savePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox workspacesDropdown;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox selectedProjectsListBox;
        private System.Windows.Forms.ListBox allProjectsListBox;
    }
}

