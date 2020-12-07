
namespace Cllient_app
{
    partial class TeacherOverviewForm
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
            this.userInfoLabel = new System.Windows.Forms.Label();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.timetableTabPage = new System.Windows.Forms.TabPage();
            this.refreshTimetableButton = new System.Windows.Forms.Button();
            this.examsCheckBox = new System.Windows.Forms.CheckBox();
            this.timetableGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mainTabControl.SuspendLayout();
            this.timetableTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timetableGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // userInfoLabel
            // 
            this.userInfoLabel.AutoSize = true;
            this.userInfoLabel.Location = new System.Drawing.Point(12, 428);
            this.userInfoLabel.Name = "userInfoLabel";
            this.userInfoLabel.Size = new System.Drawing.Size(80, 13);
            this.userInfoLabel.TabIndex = 0;
            this.userInfoLabel.Text = "Surname Name";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.timetableTabPage);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(776, 413);
            this.mainTabControl.TabIndex = 1;
            // 
            // timetableTabPage
            // 
            this.timetableTabPage.Controls.Add(this.refreshTimetableButton);
            this.timetableTabPage.Controls.Add(this.examsCheckBox);
            this.timetableTabPage.Controls.Add(this.timetableGridView);
            this.timetableTabPage.Location = new System.Drawing.Point(4, 22);
            this.timetableTabPage.Name = "timetableTabPage";
            this.timetableTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.timetableTabPage.Size = new System.Drawing.Size(768, 387);
            this.timetableTabPage.TabIndex = 0;
            this.timetableTabPage.Text = "Timetable";
            this.timetableTabPage.UseVisualStyleBackColor = true;
            // 
            // refreshTimetableButton
            // 
            this.refreshTimetableButton.Location = new System.Drawing.Point(687, 358);
            this.refreshTimetableButton.Name = "refreshTimetableButton";
            this.refreshTimetableButton.Size = new System.Drawing.Size(75, 23);
            this.refreshTimetableButton.TabIndex = 5;
            this.refreshTimetableButton.Text = "Refresh";
            this.refreshTimetableButton.UseVisualStyleBackColor = true;
            this.refreshTimetableButton.Click += new System.EventHandler(this.refreshTimetableButton_Click);
            // 
            // examsCheckBox
            // 
            this.examsCheckBox.AutoSize = true;
            this.examsCheckBox.Location = new System.Drawing.Point(6, 362);
            this.examsCheckBox.Name = "examsCheckBox";
            this.examsCheckBox.Size = new System.Drawing.Size(57, 17);
            this.examsCheckBox.TabIndex = 4;
            this.examsCheckBox.Text = "Exams";
            this.examsCheckBox.UseVisualStyleBackColor = true;
            // 
            // timetableGridView
            // 
            this.timetableGridView.AllowUserToAddRows = false;
            this.timetableGridView.AllowUserToDeleteRows = false;
            this.timetableGridView.AllowUserToOrderColumns = true;
            this.timetableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timetableGridView.Location = new System.Drawing.Point(6, 6);
            this.timetableGridView.Name = "timetableGridView";
            this.timetableGridView.ReadOnly = true;
            this.timetableGridView.Size = new System.Drawing.Size(756, 346);
            this.timetableGridView.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TeacherOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.userInfoLabel);
            this.Name = "TeacherOverviewForm";
            this.Text = "Teacher Overview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeacherOverviewForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.timetableTabPage.ResumeLayout(false);
            this.timetableTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timetableGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userInfoLabel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage timetableTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button refreshTimetableButton;
        private System.Windows.Forms.CheckBox examsCheckBox;
        private System.Windows.Forms.DataGridView timetableGridView;
    }
}