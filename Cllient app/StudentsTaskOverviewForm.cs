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
    public partial class StudentsTaskOverviewForm : Form
    {
        UserInfo userInfo;
        OleDbConnection connection;
        int taskID;

        private System.Data.DataSet dataSet;

        public StudentsTaskOverviewForm(UserInfo userInfo, OleDbConnection connection, int taskID)
        {
            InitializeComponent();

            dataSet = new DataSet();

            this.userInfo = userInfo;
            this.connection = connection;
            this.taskID = taskID;

            initReportsTable();
            refreshReportsTable();

            initIssuesTable();
        }

        private void initReportsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Reports");
            tmpTable.Columns.Add("ReportID", typeof(int));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));

            reportsGridView.DataSource = dataSet;
            reportsGridView.DataMember = "Reports";
            reportsGridView.Columns[0].Visible = false;
        }

        private void refreshReportsTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Reports"].Clear();

            String strSQL = "EXEC GetReportsOfTaskForStudents @studentID = " +
                userInfo.id +
                ", @taskID = " +
                this.taskID;

            Console.WriteLine(strSQL);

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Reports");
        }

        private void initIssuesTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Issues");
            tmpTable.Columns.Add("IssueID", typeof(int));
            tmpTable.Columns.Add("ReportID", typeof(int));
            tmpTable.Columns.Add("Issue", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));

            issuesGridView.DataSource = dataSet;
            issuesGridView.DataMember = "Issues";
            issuesGridView.Columns[0].Visible = false;
            issuesGridView.Columns[1].Visible = false;
        }

        private void refreshIssuesTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetIssuesOfReportForStudents @reportID = " +
                reportsGridView.SelectedRows[0].Cells[0].Value.ToString();

            Console.WriteLine(strSQL);

            dataSet.Tables["Issues"].Clear();

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Issues");
        }

        private void reportsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (reportsGridView.SelectedRows.Count > 0)
            {
                Console.WriteLine(reportsGridView.SelectedRows.Count);
                refreshIssuesTable();
            }
        }
    }
}
