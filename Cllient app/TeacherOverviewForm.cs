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
    public partial class TeacherOverviewForm : Form
    {
        UserInfo userInfo;
        OleDbConnection connection;

        private System.Data.DataSet dataSet;

        private List<String> subjectsIDs = new List<String>();

        private Boolean editingLastRow = false;

        public TeacherOverviewForm(UserInfo userInfo, OleDbConnection connection)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;

            this.connection = connection;

            dataSet = new DataSet();

            initTimetable();
            refreshTimetable();

            initSubjectComboBox();

            initTasksTable();
        }

        private void TeacherOverviewForm_FormClosing(object sender, FormClosingEventArgs e)
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

            String strSQL = "EXEC GetClassesForTeacher @teacherID = " +
                userInfo.id +
                ", @isExam = " +
                examsCheckBox.Checked;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Timetable");
        }

        private void initSubjectComboBox()
        {
            String strSQL = "EXEC GetCourcesForTeacher @teacherID =  " +
                userInfo.id;

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            tasksSubjectComboBox.Items.Clear();

            while (reader.Read())
            {
                tasksSubjectComboBox.Items.Add(reader["Subject name"] + " [" + reader["Cource name"] + "]");
                this.subjectsIDs.Add(reader["CourseID"].ToString());
            }
        }

        private void tasksSubjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tasksGridView.ReadOnly = false;
            tasksGridView.AllowUserToAddRows = true;

            refreshTasksTable();
        }

        private void initTasksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Tasks");
            tmpTable.Columns.Add("TaskID", typeof(int));
            tmpTable.Columns.Add("CourceID", typeof(int));
            tmpTable.Columns.Add("Title", typeof(String));
            tmpTable.Columns.Add("Description", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Deadline", typeof(DateTime));
            tmpTable.Columns.Add("isExam", typeof(Boolean));
            tasksGridView.DataSource = dataSet;
            tasksGridView.DataMember = "Tasks";

            tasksGridView.Columns[0].Visible = false;
            tasksGridView.Columns[1].Visible = false;
        }

        private void refreshTasksTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Tasks"].Clear();

            String strSQL = "EXEC GetTasksInCource @courceID = " +
                this.subjectsIDs[tasksSubjectComboBox.SelectedIndex];

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Tasks");
        }

        private void tasksGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = tasksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = tasksGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Tasks SET " + columnName + " = ? WHERE TaskID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                switch (e.ColumnIndex)
                {
                    case 2:
                    case 3:
                    case 4:
                        Console.WriteLine("str");
                        cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                        break;
                    case 5:
                        Console.WriteLine("date");
                        cmd.Parameters.Add("@p1", OleDbType.Date, 50);
                        break;
                    case 6:
                        Console.WriteLine("bool");
                        cmd.Parameters.Add("@p1", OleDbType.Boolean, 50);
                        break;
                    default:
                        Console.WriteLine("str");
                        cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                        break;
                }

                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                Console.WriteLine(tasksGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                cmd.Parameters[1].Value = Int32.Parse(tasksGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTasksTable();
            }
        }

        private void tasksGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < tasksGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }
    }
}
