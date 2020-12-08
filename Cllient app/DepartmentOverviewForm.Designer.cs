
namespace Cllient_app
{
    partial class DepartmentOverviewForm
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
            this.studentsTabPage = new System.Windows.Forms.TabPage();
            this.groupsTabPage = new System.Windows.Forms.TabPage();
            this.teachersTabPage = new System.Windows.Forms.TabPage();
            this.courcesTabPage = new System.Windows.Forms.TabPage();
            this.timetableTabPage = new System.Windows.Forms.TabPage();
            this.classroomsTabPage = new System.Windows.Forms.TabPage();
            this.buildingsTabPage = new System.Windows.Forms.TabPage();
            this.studentsGridView = new System.Windows.Forms.DataGridView();
            this.studentsGroupComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.removeStudentButton = new System.Windows.Forms.Button();
            this.removeTeacherButton = new System.Windows.Forms.Button();
            this.teachersGridView = new System.Windows.Forms.DataGridView();
            this.groupsGridView = new System.Windows.Forms.DataGridView();
            this.removeGroupButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.courcesForGroupGridView = new System.Windows.Forms.DataGridView();
            this.removeCourceForGroupButton = new System.Windows.Forms.Button();
            this.addCourceForGroupComboBox = new System.Windows.Forms.ComboBox();
            this.addCourceForGroupButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.programOfGroupComboBox = new System.Windows.Forms.ComboBox();
            this.mainTabControl.SuspendLayout();
            this.studentsTabPage.SuspendLayout();
            this.groupsTabPage.SuspendLayout();
            this.teachersTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.courcesForGroupGridView)).BeginInit();
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
            this.mainTabControl.Controls.Add(this.studentsTabPage);
            this.mainTabControl.Controls.Add(this.groupsTabPage);
            this.mainTabControl.Controls.Add(this.teachersTabPage);
            this.mainTabControl.Controls.Add(this.courcesTabPage);
            this.mainTabControl.Controls.Add(this.timetableTabPage);
            this.mainTabControl.Controls.Add(this.classroomsTabPage);
            this.mainTabControl.Controls.Add(this.buildingsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(776, 413);
            this.mainTabControl.TabIndex = 1;
            // 
            // studentsTabPage
            // 
            this.studentsTabPage.Controls.Add(this.removeStudentButton);
            this.studentsTabPage.Controls.Add(this.label1);
            this.studentsTabPage.Controls.Add(this.studentsGroupComboBox);
            this.studentsTabPage.Controls.Add(this.studentsGridView);
            this.studentsTabPage.Location = new System.Drawing.Point(4, 22);
            this.studentsTabPage.Name = "studentsTabPage";
            this.studentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.studentsTabPage.Size = new System.Drawing.Size(768, 387);
            this.studentsTabPage.TabIndex = 0;
            this.studentsTabPage.Text = "Students";
            this.studentsTabPage.UseVisualStyleBackColor = true;
            // 
            // groupsTabPage
            // 
            this.groupsTabPage.Controls.Add(this.label3);
            this.groupsTabPage.Controls.Add(this.programOfGroupComboBox);
            this.groupsTabPage.Controls.Add(this.addCourceForGroupButton);
            this.groupsTabPage.Controls.Add(this.addCourceForGroupComboBox);
            this.groupsTabPage.Controls.Add(this.removeCourceForGroupButton);
            this.groupsTabPage.Controls.Add(this.courcesForGroupGridView);
            this.groupsTabPage.Controls.Add(this.label2);
            this.groupsTabPage.Controls.Add(this.removeGroupButton);
            this.groupsTabPage.Controls.Add(this.groupsGridView);
            this.groupsTabPage.Location = new System.Drawing.Point(4, 22);
            this.groupsTabPage.Name = "groupsTabPage";
            this.groupsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.groupsTabPage.Size = new System.Drawing.Size(768, 387);
            this.groupsTabPage.TabIndex = 1;
            this.groupsTabPage.Text = "Groups";
            this.groupsTabPage.UseVisualStyleBackColor = true;
            // 
            // teachersTabPage
            // 
            this.teachersTabPage.Controls.Add(this.removeTeacherButton);
            this.teachersTabPage.Controls.Add(this.teachersGridView);
            this.teachersTabPage.Location = new System.Drawing.Point(4, 22);
            this.teachersTabPage.Name = "teachersTabPage";
            this.teachersTabPage.Size = new System.Drawing.Size(768, 387);
            this.teachersTabPage.TabIndex = 2;
            this.teachersTabPage.Text = "Teachers";
            this.teachersTabPage.UseVisualStyleBackColor = true;
            // 
            // courcesTabPage
            // 
            this.courcesTabPage.Location = new System.Drawing.Point(4, 22);
            this.courcesTabPage.Name = "courcesTabPage";
            this.courcesTabPage.Size = new System.Drawing.Size(768, 387);
            this.courcesTabPage.TabIndex = 3;
            this.courcesTabPage.Text = "Cources";
            this.courcesTabPage.UseVisualStyleBackColor = true;
            // 
            // timetableTabPage
            // 
            this.timetableTabPage.Location = new System.Drawing.Point(4, 22);
            this.timetableTabPage.Name = "timetableTabPage";
            this.timetableTabPage.Size = new System.Drawing.Size(768, 387);
            this.timetableTabPage.TabIndex = 4;
            this.timetableTabPage.Text = "Timetable";
            this.timetableTabPage.UseVisualStyleBackColor = true;
            // 
            // classroomsTabPage
            // 
            this.classroomsTabPage.Location = new System.Drawing.Point(4, 22);
            this.classroomsTabPage.Name = "classroomsTabPage";
            this.classroomsTabPage.Size = new System.Drawing.Size(768, 387);
            this.classroomsTabPage.TabIndex = 5;
            this.classroomsTabPage.Text = "Classrooms";
            this.classroomsTabPage.UseVisualStyleBackColor = true;
            // 
            // buildingsTabPage
            // 
            this.buildingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.buildingsTabPage.Name = "buildingsTabPage";
            this.buildingsTabPage.Size = new System.Drawing.Size(768, 387);
            this.buildingsTabPage.TabIndex = 6;
            this.buildingsTabPage.Text = "Buildings";
            this.buildingsTabPage.UseVisualStyleBackColor = true;
            // 
            // studentsGridView
            // 
            this.studentsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.studentsGridView.Location = new System.Drawing.Point(0, 3);
            this.studentsGridView.Name = "studentsGridView";
            this.studentsGridView.Size = new System.Drawing.Size(762, 354);
            this.studentsGridView.TabIndex = 0;
            this.studentsGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.studentsGridView_CellBeginEdit);
            this.studentsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.studentsGridView_CellEndEdit);
            this.studentsGridView.SelectionChanged += new System.EventHandler(this.studentsGridView_SelectionChanged);
            // 
            // studentsGroupComboBox
            // 
            this.studentsGroupComboBox.FormattingEnabled = true;
            this.studentsGroupComboBox.Location = new System.Drawing.Point(48, 365);
            this.studentsGroupComboBox.Name = "studentsGroupComboBox";
            this.studentsGroupComboBox.Size = new System.Drawing.Size(121, 21);
            this.studentsGroupComboBox.TabIndex = 1;
            this.studentsGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.studentsGroupComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Group:";
            // 
            // removeStudentButton
            // 
            this.removeStudentButton.Location = new System.Drawing.Point(687, 363);
            this.removeStudentButton.Name = "removeStudentButton";
            this.removeStudentButton.Size = new System.Drawing.Size(75, 23);
            this.removeStudentButton.TabIndex = 3;
            this.removeStudentButton.Text = "Remove";
            this.removeStudentButton.UseVisualStyleBackColor = true;
            this.removeStudentButton.Click += new System.EventHandler(this.removeStudentButton_Click);
            // 
            // removeTeacherButton
            // 
            this.removeTeacherButton.Location = new System.Drawing.Point(689, 363);
            this.removeTeacherButton.Name = "removeTeacherButton";
            this.removeTeacherButton.Size = new System.Drawing.Size(75, 23);
            this.removeTeacherButton.TabIndex = 5;
            this.removeTeacherButton.Text = "Remove";
            this.removeTeacherButton.UseVisualStyleBackColor = true;
            this.removeTeacherButton.Click += new System.EventHandler(this.removeTeacherButton_Click);
            // 
            // teachersGridView
            // 
            this.teachersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teachersGridView.Location = new System.Drawing.Point(2, 3);
            this.teachersGridView.Name = "teachersGridView";
            this.teachersGridView.Size = new System.Drawing.Size(762, 354);
            this.teachersGridView.TabIndex = 4;
            this.teachersGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.teachersGridView_CellBeginEdit);
            this.teachersGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.teachersGridView_CellEndEdit);
            this.teachersGridView.SelectionChanged += new System.EventHandler(this.teachersGridView_SelectionChanged);
            // 
            // groupsGridView
            // 
            this.groupsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsGridView.Location = new System.Drawing.Point(6, 3);
            this.groupsGridView.Name = "groupsGridView";
            this.groupsGridView.Size = new System.Drawing.Size(759, 164);
            this.groupsGridView.TabIndex = 5;
            this.groupsGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.groupsGridView_CellBeginEdit);
            this.groupsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.groupsGridView_CellEndEdit);
            this.groupsGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.groupsGridView_DefaultValuesNeeded);
            this.groupsGridView.SelectionChanged += new System.EventHandler(this.groupsGridView_SelectionChanged);
            // 
            // removeGroupButton
            // 
            this.removeGroupButton.Location = new System.Drawing.Point(690, 173);
            this.removeGroupButton.Name = "removeGroupButton";
            this.removeGroupButton.Size = new System.Drawing.Size(75, 23);
            this.removeGroupButton.TabIndex = 6;
            this.removeGroupButton.Text = "Remove";
            this.removeGroupButton.UseVisualStyleBackColor = true;
            this.removeGroupButton.Click += new System.EventHandler(this.removeGroupButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cources";
            // 
            // courcesForGroupGridView
            // 
            this.courcesForGroupGridView.AllowUserToAddRows = false;
            this.courcesForGroupGridView.AllowUserToDeleteRows = false;
            this.courcesForGroupGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.courcesForGroupGridView.Location = new System.Drawing.Point(6, 202);
            this.courcesForGroupGridView.Name = "courcesForGroupGridView";
            this.courcesForGroupGridView.ReadOnly = true;
            this.courcesForGroupGridView.Size = new System.Drawing.Size(759, 152);
            this.courcesForGroupGridView.TabIndex = 8;
            this.courcesForGroupGridView.SelectionChanged += new System.EventHandler(this.courcesForGroupGridView_SelectionChanged);
            // 
            // removeCourceForGroupButton
            // 
            this.removeCourceForGroupButton.Location = new System.Drawing.Point(690, 360);
            this.removeCourceForGroupButton.Name = "removeCourceForGroupButton";
            this.removeCourceForGroupButton.Size = new System.Drawing.Size(75, 23);
            this.removeCourceForGroupButton.TabIndex = 9;
            this.removeCourceForGroupButton.Text = "Remove";
            this.removeCourceForGroupButton.UseVisualStyleBackColor = true;
            this.removeCourceForGroupButton.Click += new System.EventHandler(this.removeCourceForGroupButton_Click);
            // 
            // addCourceForGroupComboBox
            // 
            this.addCourceForGroupComboBox.FormattingEnabled = true;
            this.addCourceForGroupComboBox.Location = new System.Drawing.Point(6, 360);
            this.addCourceForGroupComboBox.Name = "addCourceForGroupComboBox";
            this.addCourceForGroupComboBox.Size = new System.Drawing.Size(121, 21);
            this.addCourceForGroupComboBox.TabIndex = 10;
            // 
            // addCourceForGroupButton
            // 
            this.addCourceForGroupButton.Location = new System.Drawing.Point(133, 360);
            this.addCourceForGroupButton.Name = "addCourceForGroupButton";
            this.addCourceForGroupButton.Size = new System.Drawing.Size(75, 23);
            this.addCourceForGroupButton.TabIndex = 11;
            this.addCourceForGroupButton.Text = "Add cource";
            this.addCourceForGroupButton.UseVisualStyleBackColor = true;
            this.addCourceForGroupButton.Click += new System.EventHandler(this.addCourceForGroupButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Program:";
            // 
            // programOfGroupComboBox
            // 
            this.programOfGroupComboBox.FormattingEnabled = true;
            this.programOfGroupComboBox.Location = new System.Drawing.Point(563, 175);
            this.programOfGroupComboBox.Name = "programOfGroupComboBox";
            this.programOfGroupComboBox.Size = new System.Drawing.Size(121, 21);
            this.programOfGroupComboBox.TabIndex = 12;
            this.programOfGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.programOfGroupComboBox_SelectedIndexChanged);
            // 
            // DepartmentOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.userInfoLabel);
            this.Name = "DepartmentOverviewForm";
            this.Text = "Department Overview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DepartmentOverviewForm_FormClosing);
            this.mainTabControl.ResumeLayout(false);
            this.studentsTabPage.ResumeLayout(false);
            this.studentsTabPage.PerformLayout();
            this.groupsTabPage.ResumeLayout(false);
            this.groupsTabPage.PerformLayout();
            this.teachersTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.studentsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.courcesForGroupGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userInfoLabel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage studentsTabPage;
        private System.Windows.Forms.TabPage groupsTabPage;
        private System.Windows.Forms.Button removeStudentButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox studentsGroupComboBox;
        private System.Windows.Forms.DataGridView studentsGridView;
        private System.Windows.Forms.TabPage teachersTabPage;
        private System.Windows.Forms.TabPage courcesTabPage;
        private System.Windows.Forms.TabPage timetableTabPage;
        private System.Windows.Forms.TabPage classroomsTabPage;
        private System.Windows.Forms.TabPage buildingsTabPage;
        private System.Windows.Forms.Button removeTeacherButton;
        private System.Windows.Forms.DataGridView teachersGridView;
        private System.Windows.Forms.Button removeCourceForGroupButton;
        private System.Windows.Forms.DataGridView courcesForGroupGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button removeGroupButton;
        private System.Windows.Forms.DataGridView groupsGridView;
        private System.Windows.Forms.Button addCourceForGroupButton;
        private System.Windows.Forms.ComboBox addCourceForGroupComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox programOfGroupComboBox;
    }
}