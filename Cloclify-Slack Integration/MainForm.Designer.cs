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
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.workspacesDropdown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // getRecordsButton
            // 
            this.getRecordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getRecordsButton.Location = new System.Drawing.Point(136, 388);
            this.getRecordsButton.Name = "getRecordsButton";
            this.getRecordsButton.Size = new System.Drawing.Size(250, 71);
            this.getRecordsButton.TabIndex = 0;
            this.getRecordsButton.Text = "Get Records";
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
            this.datePicker.Location = new System.Drawing.Point(136, 296);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(250, 20);
            this.datePicker.TabIndex = 2;
            // 
            // savePathTextBox
            // 
            this.savePathTextBox.Location = new System.Drawing.Point(136, 338);
            this.savePathTextBox.Name = "savePathTextBox";
            this.savePathTextBox.ReadOnly = true;
            this.savePathTextBox.Size = new System.Drawing.Size(214, 20);
            this.savePathTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Save path";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.BackgroundImage = global::Cloclify_Slack_Integration.Properties.Resources.folder_512;
            this.selectFolderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectFolderButton.Location = new System.Drawing.Point(356, 338);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(30, 20);
            this.selectFolderButton.TabIndex = 6;
            this.selectFolderButton.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // workspacesDropdown
            // 
            this.workspacesDropdown.FormattingEnabled = true;
            this.workspacesDropdown.Location = new System.Drawing.Point(136, 252);
            this.workspacesDropdown.Name = "workspacesDropdown";
            this.workspacesDropdown.Size = new System.Drawing.Size(250, 21);
            this.workspacesDropdown.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Workspace";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(398, 471);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workspacesDropdown);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.savePathTextBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.getRecordsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Form1";
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox workspacesDropdown;
    }
}

