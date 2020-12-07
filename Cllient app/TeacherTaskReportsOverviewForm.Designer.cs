
namespace Cllient_app
{
    partial class TeacherTaskReportsOverviewForm
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
            this.reportsGridView = new System.Windows.Forms.DataGridView();
            this.issuesGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.removeIssueButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.reportsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // reportsGridView
            // 
            this.reportsGridView.AllowUserToAddRows = false;
            this.reportsGridView.AllowUserToDeleteRows = false;
            this.reportsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportsGridView.Location = new System.Drawing.Point(15, 37);
            this.reportsGridView.Name = "reportsGridView";
            this.reportsGridView.ReadOnly = true;
            this.reportsGridView.Size = new System.Drawing.Size(773, 178);
            this.reportsGridView.TabIndex = 2;
            this.reportsGridView.SelectionChanged += new System.EventHandler(this.reportsGridView_SelectionChanged);
            // 
            // issuesGridView
            // 
            this.issuesGridView.AllowUserToDeleteRows = false;
            this.issuesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.issuesGridView.Location = new System.Drawing.Point(15, 260);
            this.issuesGridView.Name = "issuesGridView";
            this.issuesGridView.Size = new System.Drawing.Size(773, 178);
            this.issuesGridView.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Issues";
            // 
            // stateComboBox
            // 
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.IntegralHeight = false;
            this.stateComboBox.Location = new System.Drawing.Point(667, 10);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(121, 21);
            this.stateComboBox.TabIndex = 5;
            this.stateComboBox.SelectedIndexChanged += new System.EventHandler(this.stateComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(593, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Report state:";
            // 
            // removeIssueButton
            // 
            this.removeIssueButton.Location = new System.Drawing.Point(695, 230);
            this.removeIssueButton.Name = "removeIssueButton";
            this.removeIssueButton.Size = new System.Drawing.Size(93, 23);
            this.removeIssueButton.TabIndex = 7;
            this.removeIssueButton.Text = "Remove issue";
            this.removeIssueButton.UseVisualStyleBackColor = true;
            this.removeIssueButton.Click += new System.EventHandler(this.removeIssueButton_Click);
            // 
            // TeacherTaskReportsOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.removeIssueButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stateComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.issuesGridView);
            this.Controls.Add(this.reportsGridView);
            this.Name = "TeacherTaskReportsOverviewForm";
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.reportsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView reportsGridView;
        private System.Windows.Forms.DataGridView issuesGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button removeIssueButton;
    }
}