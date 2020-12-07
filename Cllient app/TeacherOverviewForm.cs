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

        public TeacherOverviewForm(UserInfo userInfo, OleDbConnection connection)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;

            this.connection = connection;

            dataSet = new DataSet();

            initTimetable();
            refreshTimetable();
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
    }
}
