
namespace Cllient_app
{
    partial class StudentOverviewForm
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
            this.marksTabPage = new System.Windows.Forms.TabPage();
            this.marksSubjectComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.marksGridView = new System.Windows.Forms.DataGridView();
            this.tasksTabPage = new System.Windows.Forms.TabPage();
            this.tasksGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tasksSubjectComboBox = new System.Windows.Forms.ComboBox();
            this.mainTabControl.SuspendLayout();
            this.timetableTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timetableGridView)).BeginInit();
            this.marksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marksGridView)).BeginInit();
            this.tasksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksGridView)).BeginInit();
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
            this.mainTabControl.Controls.Add(this.marksTabPage);
            this.mainTabControl.Controls.Add(this.tasksTabPage);
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
            this.refreshTimetableButton.TabIndex = 2;
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
            this.examsCheckBox.TabIndex = 1;
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
            this.timetableGridView.TabIndex = 0;
            // 
            // marksTabPage
            // 
            this.marksTabPage.Controls.Add(this.marksGridView);
            this.marksTabPage.Controls.Add(this.label1);
            this.marksTabPage.Controls.Add(this.marksSubjectComboBox);
            this.marksTabPage.Location = new System.Drawing.Point(4, 22);
            this.marksTabPage.Name = "marksTabPage";
            this.marksTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.marksTabPage.Size = new System.Drawing.Size(768, 387);
            this.marksTabPage.TabIndex = 1;
            this.marksTabPage.Text = "Marks";
            this.marksTabPage.UseVisualStyleBackColor = true;
            // 
            // marksSubjectComboBox
            // 
            this.marksSubjectComboBox.FormattingEnabled = true;
            this.marksSubjectComboBox.Location = new System.Drawing.Point(59, 4);
            this.marksSubjectComboBox.Name = "marksSubjectComboBox";
            this.marksSubjectComboBox.Size = new System.Drawing.Size(308, 21);
            this.marksSubjectComboBox.TabIndex = 0;
            this.marksSubjectComboBox.SelectedIndexChanged += new System.EventHandler(this.marksSubjectComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Subject:";
            // 
            // marksGridView
            // 
            this.marksGridView.AllowUserToAddRows = false;
            this.marksGridView.AllowUserToDeleteRows = false;
            this.marksGridView.AllowUserToOrderColumns = true;
            this.marksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marksGridView.Location = new System.Drawing.Point(10, 31);
            this.marksGridView.Name = "marksGridView";
            this.marksGridView.ReadOnly = true;
            this.marksGridView.Size = new System.Drawing.Size(752, 350);
            this.marksGridView.TabIndex = 2;
            // 
            // tasksTabPage
            // 
            this.tasksTabPage.Controls.Add(this.tasksGridView);
            this.tasksTabPage.Controls.Add(this.label2);
            this.tasksTabPage.Controls.Add(this.tasksSubjectComboBox);
            this.tasksTabPage.Location = new System.Drawing.Point(4, 22);
            this.tasksTabPage.Name = "tasksTabPage";
            this.tasksTabPage.Size = new System.Drawing.Size(768, 387);
            this.tasksTabPage.TabIndex = 2;
            this.tasksTabPage.Text = "Tasks";
            this.tasksTabPage.UseVisualStyleBackColor = true;
            // 
            // tasksGridView
            // 
            this.tasksGridView.AllowUserToAddRows = false;
            this.tasksGridView.AllowUserToDeleteRows = false;
            this.tasksGridView.AllowUserToOrderColumns = true;
            this.tasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tasksGridView.Location = new System.Drawing.Point(10, 31);
            this.tasksGridView.MultiSelect = false;
            this.tasksGridView.Name = "tasksGridView";
            this.tasksGridView.ReadOnly = true;
            this.tasksGridView.Size = new System.Drawing.Size(752, 350);
            this.tasksGridView.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subject:";
            // 
            // tasksSubjectComboBox
            // 
            this.tasksSubjectComboBox.FormattingEnabled = true;
            this.tasksSubjectComboBox.Location = new System.Drawing.Point(59, 4);
            this.tasksSubjectComboBox.Name = "tasksSubjectComboBox";
            this.tasksSubjectComboBox.Size = new System.Drawing.Size(308, 21);
            this.tasksSubjectComboBox.TabIndex = 3;
            this.tasksSubjectComboBox.SelectedIndexChanged += new System.EventHandler(this.tasksSubjectComboBox_SelectedIndexChanged);
            // 
            // StudentOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.userInfoLabel);
            this.Name = "StudentOverviewForm";
            this.Text = "Student Overview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentOverviewForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.timetableTabPage.ResumeLayout(false);
            this.timetableTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timetableGridView)).EndInit();
            this.marksTabPage.ResumeLayout(false);
            this.marksTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marksGridView)).EndInit();
            this.tasksTabPage.ResumeLayout(false);
            this.tasksTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userInfoLabel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage timetableTabPage;
        private System.Windows.Forms.TabPage marksTabPage;
        private System.Windows.Forms.DataGridView timetableGridView;
        private System.Windows.Forms.Button refreshTimetableButton;
        private System.Windows.Forms.CheckBox examsCheckBox;
        private System.Windows.Forms.ComboBox marksSubjectComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView marksGridView;
        private System.Windows.Forms.TabPage tasksTabPage;
        private System.Windows.Forms.DataGridView tasksGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tasksSubjectComboBox;
    }
}