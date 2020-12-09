using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Cllient_app
{
    public partial class StudentOverviewForm : Form
    {
        UserInfo userInfo;
        OleDbConnection connection;

        private System.Data.DataSet dataSet;

        private List<String> subjectsIDs = new List<String>();

        public StudentOverviewForm(UserInfo userInfo, OleDbConnection connection)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;

            this.connection = connection;

            dataSet = new DataSet();

            reportsButton.Enabled = false;
            signOutButton.Enabled = false;

            initSubjectComboBoxes();

            initTimetable();
            refreshTimetable();

            initTasksTable();
            initMarksTable();

            initEventsTable();
            refreshEventsTable();

            showMarksForReportsTable();
            showMissedTasksTable();
            showFailedTasksTable();
            showCommentedTasksTable();
        }

        private void StudentOverviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void refreshTimetableButton_Click(object sender, EventArgs e)
        {
            refreshTimetable();
        }

        private void initTimetable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Timetable");
            tmpTable.Columns.Add("Date", typeof(DateTime));
            tmpTable.Columns.Add("Subject", typeof(String));
            tmpTable.Columns.Add("Teacher's last name", typeof(String));
            tmpTable.Columns.Add("Teacher's first name", typeof(String));
            tmpTable.Columns.Add("Classroom", typeof(String));
            tmpTable.Columns.Add("Building", typeof(String));
            tmpTable.Columns.Add("Conference", typeof(String));
            tmpTable.Columns.Add("Conference link", typeof(String));

            timetableGridView.DataSource = dataSet;
            timetableGridView.DataMember = "Timetable";
        }

        private void refreshTimetable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Timetable"].Clear();

            String strSQL = "EXEC GetClassesForStudent @studentID = " +
                userInfo.id +
                ", @isExam = " +
                examsCheckBox.Checked;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Timetable");
        }

        private void initSubjectComboBoxes()
        {
            String strSQL = "EXEC GetCourcesForStudent @studentID =  " +
                userInfo.id;

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            marksSubjectComboBox.Items.Clear();

            while (reader.Read())
            {
                marksSubjectComboBox.Items.Add(reader["Subject name"] + " [" + reader["Cource name"] + "]");
                tasksSubjectComboBox.Items.Add(reader["Subject name"] + " [" + reader["Cource name"] + "]");
                this.subjectsIDs.Add(reader["CourseID"].ToString());
            }
        }

        private void marksSubjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshMarksTable();
        }

        private void initMarksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Marks");
            tmpTable.Columns.Add("Mark", typeof(int));
            tmpTable.Columns.Add("Date", typeof(DateTime));
            tmpTable.Columns.Add("Task", typeof(String));
            tmpTable.Columns.Add("Description", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Deadline", typeof(DateTime));

            marksGridView.DataSource = dataSet;
            marksGridView.DataMember = "Marks";
        }

        private void refreshMarksTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Marks"].Clear();

            String strSQL = "EXEC GetMarksWithTasksForStudent @studentID = " +
                userInfo.id +
                ", @courceID = " +
                this.subjectsIDs[marksSubjectComboBox.SelectedIndex];

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Marks");
        }

        private void initTasksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Tasks");
            tmpTable.Columns.Add("TaskID", typeof(int));
            tmpTable.Columns.Add("Mark", typeof(int));
            tmpTable.Columns.Add("Task", typeof(String));
            tmpTable.Columns.Add("Description", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Deadline", typeof(DateTime));
            tmpTable.Columns.Add("Subject", typeof(String));
            tmpTable.Columns.Add("Cource", typeof(String));

            tasksGridView.DataSource = dataSet;
            tasksGridView.DataMember = "Tasks";
            tasksGridView.Columns[0].Visible = false;
        }

        private void refreshTasksTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Tasks"].Clear();

            String strSQL = "EXEC GetTasksWithMarksForStudent @studentID = " +
                userInfo.id +
                ", @courceID = " +
                this.subjectsIDs[tasksSubjectComboBox.SelectedIndex];

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Tasks");
        }

        private void showMarksForReportsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("MarksForReports");

            marksForReportsGridView.DataSource = dataSet;
            marksForReportsGridView.DataMember = "MarksForReports";

            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["MarksForReports"].Clear();

            String strSQL = "EXEC GetReportsWithMarksForStudent @studentID = " +
                userInfo.id;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "MarksForReports");
        }

        private void showMissedTasksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("MissedTasks");

            missedTasksGridView.DataSource = dataSet;
            missedTasksGridView.DataMember = "MissedTasks";

            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["MissedTasks"].Clear();

            String strSQL = "EXEC GetMissedTasksForStudent @studentID = " +
                userInfo.id;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "MissedTasks");
        }

        private void showFailedTasksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("FailedTasks");

            failedTasksGridView.DataSource = dataSet;
            failedTasksGridView.DataMember = "FailedTasks";

            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["FailedTasks"].Clear();

            String strSQL = "EXEC GetFailedTasksForStudent @studentID = " +
                userInfo.id;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "FailedTasks");
        }

        private void showCommentedTasksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("CommentedTasks");

            commentedTasksGridView.DataSource = dataSet;
            commentedTasksGridView.DataMember = "CommentedTasks";

            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["CommentedTasks"].Clear();

            String strSQL = "EXEC GetCommentedTasksForStudent @studentID = " +
                userInfo.id;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "CommentedTasks");
        }

        private void tasksSubjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTasksTable();
        }

        private void reportsButton_Click(object sender, EventArgs e)
        {
            if (tasksGridView.SelectedRows.Count > 0)
            {
                Form newForm = new StudentsTaskOverviewForm(this.userInfo, this.connection, Int32.Parse(tasksGridView.SelectedRows[0].Cells[0].Value.ToString()));
                newForm.Show();
            }
        }

        private void tasksGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (tasksGridView.SelectedRows.Count > 0)
            {
                reportsButton.Enabled = true;
            } 
            else
            {
                reportsButton.Enabled = false;
            }
        }

        private void initEventsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Events");
            tmpTable.Columns.Add("EventID", typeof(int));
            tmpTable.Columns.Add("Title", typeof(String));
            tmpTable.Columns.Add("Place", typeof(String));
            tmpTable.Columns.Add("Datetime", typeof(DateTime));
            tmpTable.Columns.Add("isMandatory", typeof(Boolean));

            eventsGridView.DataSource = dataSet;
            eventsGridView.DataMember = "Events";
            eventsGridView.Columns[0].Visible = false;
        }

        private void refreshEventsTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Events"].Clear();

            String strSQL = "EXEC GetFutureEventsForStudent @studentID = " +
                userInfo.id;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Events");
        }

        private void eventsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0)
            {
                int selectedRowIndex = eventsGridView.SelectedRows[0].Index;
                if (Boolean.Parse(eventsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()))
                {
                    signOutButton.Enabled = false;
                } 
                else
                {
                    signOutButton.Enabled = true;
                }
            }
        }

        private void signOutButton_Click(object sender, EventArgs e)
        {
            if (eventsGridView.SelectedRows.Count > 0)
            {
                int selectedRowIndex = eventsGridView.SelectedRows[0].Index;

                String strSQL = "DELETE FROM [Events-Students] WHERE EventID = ? AND StudentID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = eventsGridView.Rows[selectedRowIndex].Cells[0].Value.ToString();
                cmd.Parameters[1].Value = userInfo.id;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshEventsTable();
            }
        }
    }
}
