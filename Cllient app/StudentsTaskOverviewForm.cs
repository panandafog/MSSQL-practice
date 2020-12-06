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
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));
            tmpTable.Columns.Add("ReportID", typeof(int));

            reportsGridView.DataSource = dataSet;
            reportsGridView.DataMember = "Reports";
            reportsGridView.Columns[3].Visible = false;

            reportsGridView.Columns[0].ReadOnly = false;
            reportsGridView.Columns[1].ReadOnly = true;
            reportsGridView.Columns[2].ReadOnly = true;
        }

        private void refreshReportsTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Reports"].Clear();

            String strSQL = "EXEC GetReportsOfTaskForStudents @studentID = " +
                userInfo.id +
                ", @taskID = " +
                this.taskID;

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
                reportsGridView.SelectedRows[0].Cells[3].Value.ToString();

            dataSet.Tables["Issues"].Clear();

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Issues");
        }

        private void reportsGridView_SelectionChanged(object sender, EventArgs e)
        {
           if (reportsGridView.SelectedRows.Count > 0)
            {
                refreshIssuesTable();
            }
        }

        private void reportsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            String newValue = reportsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            String strSQL = "UPDATE Reports SET Link = ? WHERE ReportID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.VarChar, 50);
            cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = newValue;
            cmd.Parameters[1].Value = Int32.Parse(reportsGridView.Rows[e.RowIndex].Cells[3].Value.ToString());

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }

            refreshReportsTable();
        }
    }
}
