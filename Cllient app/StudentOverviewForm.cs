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

            initSubjectComboBoxes();

            initTimetable();
            refreshTimetable();

            initTasksTable();
            initMarksTable();
        }

        private void StudentOverviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
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
            tmpTable.Columns.Add("Mark", typeof(int));
            tmpTable.Columns.Add("Task", typeof(String));
            tmpTable.Columns.Add("Description", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Deadline", typeof(DateTime));
            tmpTable.Columns.Add("Subject", typeof(String));
            tmpTable.Columns.Add("Cource", typeof(String));

            tasksGridView.DataSource = dataSet;
            tasksGridView.DataMember = "Tasks";
        }

        private void refreshTasksTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Tasks"].Clear();

            String strSQL = "EXEC GetTasksWithMarksForStudent @studentID = " +
                userInfo.id +
                ", @courceID = " +
                this.subjectsIDs[tasksSubjectComboBox.SelectedIndex];

            Console.WriteLine(strSQL);

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Tasks");
        }

        private void tasksSubjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTasksTable();
        }
    }
}
