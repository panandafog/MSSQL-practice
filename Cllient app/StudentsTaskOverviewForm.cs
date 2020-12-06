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
        private UserInfo userInfo;
        private OleDbConnection connection;
        private int taskID;

        private System.Data.DataSet dataSet;

        private Boolean editingLastRow = false;

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

            deleteReportButton.Enabled = false;
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
            dataSet.Tables["Issues"].Clear();

            if (reportsGridView.SelectedRows.Count > 0)
            {
                if (reportsGridView.SelectedRows[0].Index < reportsGridView.Rows.Count - 1)
                {
                    System.Data.OleDb.OleDbDataAdapter adapter;

                    String strSQL = "EXEC GetIssuesOfReportForStudents @reportID = " +
                        reportsGridView.SelectedRows[0].Cells[3].Value.ToString();

                    adapter = new OleDbDataAdapter(strSQL, connection);
                    adapter.Fill(dataSet, "Issues");
                }
            }
        }

        private void reportsGridView_SelectionChanged(object sender, EventArgs e)
        {
           if (reportsGridView.SelectedRows.Count > 0)
            {
                refreshIssuesTable();

                if (reportsGridView.SelectedRows[0].Index >= reportsGridView.Rows.Count - 1)
                {
                    deleteReportButton.Enabled = false;
                }
                else
                {
                    deleteReportButton.Enabled = true;
                }
            }
        }

        private void reportsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (!editingLastRow)
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
            else
            {
                String newValue = reportsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                String strSQL = "INSERT INTO Reports (TaskID, StudentID, Link) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p3", OleDbType.VarChar, 50);

                cmd.Parameters[0].Value = taskID;
                cmd.Parameters[1].Value = userInfo.id;
                cmd.Parameters[2].Value = newValue;

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

        private void reportsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < reportsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }

        private void deleteReport(int index)
        {
            Console.WriteLine(index);

            String strSQL = "EXEC DeleteReport @reportID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = reportsGridView.Rows[index].Cells[3].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void deleteReportButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < reportsGridView.SelectedRows.Count)
            {
                deleteReport(reportsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshReportsTable();
        }
    }
}
