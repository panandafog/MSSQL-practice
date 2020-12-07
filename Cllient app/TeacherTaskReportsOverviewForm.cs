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
    public partial class TeacherTaskReportsOverviewForm : Form
    {
        UserInfo userInfo;
        int taskID;

        OleDbConnection connection;
        private System.Data.DataSet dataSet;

        private Boolean editingLastRow = false;
        private List<int> stateIDs;
        private List<String> states;

        private Boolean updatingStatesComboBox = false;

        public TeacherTaskReportsOverviewForm(UserInfo userInfo, OleDbConnection connection, int taskID)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            this.taskID = taskID;
            this.connection = connection;

            dataSet = new DataSet();

            stateIDs = new List<int>();
            states = new List<String>();

            initReportsTable();
            initIssuesTable();
            issuesGridView.ReadOnly = true;

            refreshReportsTable();


            // Get states

            String strSQL = "SELECT * FROM ReportStates";

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
 
                states.Add(reader["Name"].ToString());
                stateIDs.Add(Int32.Parse(reader["StateID"].ToString()));
            }
        }

        private void initReportsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Reports");
            tmpTable.Columns.Add("First Name", typeof(String));
            tmpTable.Columns.Add("Last Name", typeof(String));
            tmpTable.Columns.Add("Group", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));
            tmpTable.Columns.Add("State", typeof(String));
            tmpTable.Columns.Add("ReportID", typeof(int));
            tmpTable.Columns.Add("StudentID", typeof(int));
            tmpTable.Columns.Add("GroupID", typeof(int));
            tmpTable.Columns.Add("StateID", typeof(int));

            reportsGridView.DataSource = dataSet;
            reportsGridView.DataMember = "Reports";
            reportsGridView.Columns[7].Visible = false;
            reportsGridView.Columns[8].Visible = false;
            reportsGridView.Columns[9].Visible = false;
            reportsGridView.Columns[10].Visible = false;
        }

        private void refreshReportsTable()
        {
            System.Data.OleDb.OleDbDataAdapter adapter;

            dataSet.Tables["Reports"].Clear();

            String strSQL = "EXEC GetReportsOfTask @taskID = " +
                taskID;

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Reports");
        }

        private void removeIssue(int index)
        {
            String strSQL = "DELETE FROM ReportIssues WHERE IssueID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = issuesGridView.Rows[index].Cells[4].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeIssueButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < issuesGridView.SelectedRows.Count)
            {
                removeIssue(issuesGridView.SelectedRows[index].Index);
                index++;
            }

            refreshIssuesTable();
        }

        private void reportsGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingStatesComboBox = true;

            stateComboBox.Items.Clear();

            if (reportsGridView.SelectedRows.Count > 0)
            {
                stateComboBox.Items.Clear();

                int index = 0;
                int selectedIndex = -1;
                while (index < stateIDs.Count)
                {
                    stateComboBox.Items.Add(states[index]);

                    if (stateIDs[index] == Int32.Parse(reportsGridView.Rows[reportsGridView.SelectedRows[0].Index].Cells[10].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                stateComboBox.SelectedIndex = selectedIndex;

                issuesGridView.ReadOnly = false;
            }
            else
            {
                issuesGridView.ReadOnly = true;
            }

            updatingStatesComboBox = false;

            refreshIssuesTable();
        }

        private void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportsGridView.SelectedRows.Count > 0 && !updatingStatesComboBox)
            {
                String strSQL = "UPDATE Reports SET StateID = ? WHERE ReportID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = stateIDs[stateComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(reportsGridView.SelectedRows[0].Cells[7].Value.ToString());

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

        private void initIssuesTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Issues");
            tmpTable.Columns.Add("Issue", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));
            tmpTable.Columns.Add("IssueID", typeof(int));
            tmpTable.Columns.Add("ReportID", typeof(int));

            issuesGridView.DataSource = dataSet;
            issuesGridView.DataMember = "Issues";
            issuesGridView.Columns[4].Visible = false;
            issuesGridView.Columns[5].Visible = false;

            issuesGridView.Columns[2].ReadOnly = true;
            issuesGridView.Columns[3].ReadOnly = true;
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
                        reportsGridView.SelectedRows[0].Cells[7].Value.ToString();

                    adapter = new OleDbDataAdapter(strSQL, connection);
                    adapter.Fill(dataSet, "Issues");
                }
            }
        }

        private void issuesGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (issuesGridView.SelectedRows.Count > 0)
            {
                if (issuesGridView.SelectedRows[0].Index >= issuesGridView.Rows.Count - 1)
                {
                    removeIssueButton.Enabled = false;
                }
                else
                {
                    removeIssueButton.Enabled = true;
                }
            }
            else
            {
                removeIssueButton.Enabled = false;
            }
        }

        private void issuesGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = issuesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = issuesGridView.Columns[e.ColumnIndex].Name;

                switch (e.ColumnIndex)
                {
                    case 0:
                        columnName = "Description";
                        break;
                    case 1:
                        columnName = "Link";
                        break;
                }

                String strSQL = "UPDATE ReportIssues SET " + columnName + " = ? WHERE IssueID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(issuesGridView.Rows[e.RowIndex].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshIssuesTable();
            }
            else
            {
                String description = issuesGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String link = issuesGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                int reportID = Int32.Parse(issuesGridView.Rows[e.RowIndex].Cells[5].Value.ToString());

                String strSQL = "INSERT INTO ReportIssues (Description, Link, ReportID) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p3", OleDbType.Integer, 50);
 
                cmd.Parameters[0].Value = description;
                cmd.Parameters[1].Value = link;
                cmd.Parameters[2].Value = reportID;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshIssuesTable();
            }
        }

        private void issuesGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < issuesGridView.Rows.Count - 1)
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
