
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
            this.marksGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.marksSubjectComboBox = new System.Windows.Forms.ComboBox();
            this.tasksTabPage = new System.Windows.Forms.TabPage();
            this.reportsButton = new System.Windows.Forms.Button();
            this.tasksGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tasksSubjectComboBox = new System.Windows.Forms.ComboBox();
            this.eventsTabPage = new System.Windows.Forms.TabPage();
            this.signOutButton = new System.Windows.Forms.Button();
            this.eventsGridView = new System.Windows.Forms.DataGridView();
            this.marksForReportsTabPage = new System.Windows.Forms.TabPage();
            this.marksForReportsGridView = new System.Windows.Forms.DataGridView();
            this.missedTasksTabPage = new System.Windows.Forms.TabPage();
            this.missedTasksGridView = new System.Windows.Forms.DataGridView();
            this.failedTasksTabPage = new System.Windows.Forms.TabPage();
            this.failedTasksGridView = new System.Windows.Forms.DataGridView();
            this.commentedTasksTabPage = new System.Windows.Forms.TabPage();
            this.commentedTasksGridView = new System.Windows.Forms.DataGridView();
            this.mainTabControl.SuspendLayout();
            this.timetableTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timetableGridView)).BeginInit();
            this.marksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marksGridView)).BeginInit();
            this.tasksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksGridView)).BeginInit();
            this.eventsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventsGridView)).BeginInit();
            this.marksForReportsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marksForReportsGridView)).BeginInit();
            this.missedTasksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.missedTasksGridView)).BeginInit();
            this.failedTasksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.failedTasksGridView)).BeginInit();
            this.commentedTasksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentedTasksGridView)).BeginInit();
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
            this.mainTabControl.Controls.Add(this.eventsTabPage);
            this.mainTabControl.Controls.Add(this.marksForReportsTabPage);
            this.mainTabControl.Controls.Add(this.missedTasksTabPage);
            this.mainTabControl.Controls.Add(this.failedTasksTabPage);
            this.mainTabControl.Controls.Add(this.commentedTasksTabPage);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Subject:";
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
            // tasksTabPage
            // 
            this.tasksTabPage.Controls.Add(this.reportsButton);
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
            // reportsButton
            // 
            this.reportsButton.Location = new System.Drawing.Point(687, 361);
            this.reportsButton.Name = "reportsButton";
            this.reportsButton.Size = new System.Drawing.Size(75, 23);
            this.reportsButton.TabIndex = 6;
            this.reportsButton.Text = "Reports";
            this.reportsButton.UseVisualStyleBackColor = true;
            this.reportsButton.Click += new System.EventHandler(this.reportsButton_Click);
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
            this.tasksGridView.Size = new System.Drawing.Size(752, 324);
            this.tasksGridView.TabIndex = 5;
            this.tasksGridView.SelectionChanged += new System.EventHandler(this.tasksGridView_SelectionChanged);
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
            // eventsTabPage
            // 
            this.eventsTabPage.Controls.Add(this.signOutButton);
            this.eventsTabPage.Controls.Add(this.eventsGridView);
            this.eventsTabPage.Location = new System.Drawing.Point(4, 22);
            this.eventsTabPage.Name = "eventsTabPage";
            this.eventsTabPage.Size = new System.Drawing.Size(768, 387);
            this.eventsTabPage.TabIndex = 3;
            this.eventsTabPage.Text = "Events";
            this.eventsTabPage.UseVisualStyleBackColor = true;
            // 
            // signOutButton
            // 
            this.signOutButton.Location = new System.Drawing.Point(690, 361);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(75, 23);
            this.signOutButton.TabIndex = 1;
            this.signOutButton.Text = "Sign out";
            this.signOutButton.UseVisualStyleBackColor = true;
            this.signOutButton.Click += new System.EventHandler(this.signOutButton_Click);
            // 
            // eventsGridView
            // 
            this.eventsGridView.AllowUserToAddRows = false;
            this.eventsGridView.AllowUserToDeleteRows = false;
            this.eventsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventsGridView.Location = new System.Drawing.Point(3, 3);
            this.eventsGridView.Name = "eventsGridView";
            this.eventsGridView.ReadOnly = true;
            this.eventsGridView.Size = new System.Drawing.Size(762, 352);
            this.eventsGridView.TabIndex = 0;
            this.eventsGridView.SelectionChanged += new System.EventHandler(this.eventsGridView_SelectionChanged);
            // 
            // marksForReportsTabPage
            // 
            this.marksForReportsTabPage.Controls.Add(this.marksForReportsGridView);
            this.marksForReportsTabPage.Location = new System.Drawing.Point(4, 22);
            this.marksForReportsTabPage.Name = "marksForReportsTabPage";
            this.marksForReportsTabPage.Size = new System.Drawing.Size(768, 387);
            this.marksForReportsTabPage.TabIndex = 4;
            this.marksForReportsTabPage.Text = "Marks for reports";
            this.marksForReportsTabPage.UseVisualStyleBackColor = true;
            // 
            // marksForReportsGridView
            // 
            this.marksForReportsGridView.AllowUserToAddRows = false;
            this.marksForReportsGridView.AllowUserToDeleteRows = false;
            this.marksForReportsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marksForReportsGridView.Location = new System.Drawing.Point(3, 3);
            this.marksForReportsGridView.Name = "marksForReportsGridView";
            this.marksForReportsGridView.ReadOnly = true;
            this.marksForReportsGridView.Size = new System.Drawing.Size(762, 381);
            this.marksForReportsGridView.TabIndex = 0;
            // 
            // missedTasksTabPage
            // 
            this.missedTasksTabPage.Controls.Add(this.missedTasksGridView);
            this.missedTasksTabPage.Location = new System.Drawing.Point(4, 22);
            this.missedTasksTabPage.Name = "missedTasksTabPage";
            this.missedTasksTabPage.Size = new System.Drawing.Size(768, 387);
            this.missedTasksTabPage.TabIndex = 5;
            this.missedTasksTabPage.Text = "Missed tasks";
            this.missedTasksTabPage.UseVisualStyleBackColor = true;
            // 
            // missedTasksGridView
            // 
            this.missedTasksGridView.AllowUserToAddRows = false;
            this.missedTasksGridView.AllowUserToDeleteRows = false;
            this.missedTasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.missedTasksGridView.Location = new System.Drawing.Point(3, 3);
            this.missedTasksGridView.Name = "missedTasksGridView";
            this.missedTasksGridView.ReadOnly = true;
            this.missedTasksGridView.Size = new System.Drawing.Size(762, 381);
            this.missedTasksGridView.TabIndex = 0;
            // 
            // failedTasksTabPage
            // 
            this.failedTasksTabPage.Controls.Add(this.failedTasksGridView);
            this.failedTasksTabPage.Location = new System.Drawing.Point(4, 22);
            this.failedTasksTabPage.Name = "failedTasksTabPage";
            this.failedTasksTabPage.Size = new System.Drawing.Size(768, 387);
            this.failedTasksTabPage.TabIndex = 6;
            this.failedTasksTabPage.Text = "Failed tasks";
            this.failedTasksTabPage.UseVisualStyleBackColor = true;
            // 
            // failedTasksGridView
            // 
            this.failedTasksGridView.AllowUserToAddRows = false;
            this.failedTasksGridView.AllowUserToDeleteRows = false;
            this.failedTasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.failedTasksGridView.Location = new System.Drawing.Point(3, 3);
            this.failedTasksGridView.Name = "failedTasksGridView";
            this.failedTasksGridView.ReadOnly = true;
            this.failedTasksGridView.Size = new System.Drawing.Size(762, 381);
            this.failedTasksGridView.TabIndex = 1;
            // 
            // commentedTasksTabPage
            // 
            this.commentedTasksTabPage.Controls.Add(this.commentedTasksGridView);
            this.commentedTasksTabPage.Location = new System.Drawing.Point(4, 22);
            this.commentedTasksTabPage.Name = "commentedTasksTabPage";
            this.commentedTasksTabPage.Size = new System.Drawing.Size(768, 387);
            this.commentedTasksTabPage.TabIndex = 7;
            this.commentedTasksTabPage.Text = "Commented tasks";
            this.commentedTasksTabPage.UseVisualStyleBackColor = true;
            // 
            // commentedTasksGridView
            // 
            this.commentedTasksGridView.AllowUserToAddRows = false;
            this.commentedTasksGridView.AllowUserToDeleteRows = false;
            this.commentedTasksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commentedTasksGridView.Location = new System.Drawing.Point(3, 3);
            this.commentedTasksGridView.Name = "commentedTasksGridView";
            this.commentedTasksGridView.ReadOnly = true;
            this.commentedTasksGridView.Size = new System.Drawing.Size(762, 381);
            this.commentedTasksGridView.TabIndex = 2;
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
            this.eventsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventsGridView)).EndInit();
            this.marksForReportsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marksForReportsGridView)).EndInit();
            this.missedTasksTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.missedTasksGridView)).EndInit();
            this.failedTasksTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.failedTasksGridView)).EndInit();
            this.commentedTasksTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commentedTasksGridView)).EndInit();
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
        private System.Windows.Forms.Button reportsButton;
        private System.Windows.Forms.TabPage eventsTabPage;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.DataGridView eventsGridView;
        private System.Windows.Forms.TabPage marksForReportsTabPage;
        private System.Windows.Forms.DataGridView marksForReportsGridView;
        private System.Windows.Forms.TabPage missedTasksTabPage;
        private System.Windows.Forms.DataGridView missedTasksGridView;
        private System.Windows.Forms.TabPage failedTasksTabPage;
        private System.Windows.Forms.DataGridView failedTasksGridView;
        private System.Windows.Forms.TabPage commentedTasksTabPage;
        private System.Windows.Forms.DataGridView commentedTasksGridView;
    }
}