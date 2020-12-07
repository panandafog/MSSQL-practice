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
            tmpTable.Columns.Add("ReportID", typeof(int));
            tmpTable.Columns.Add("StudentID", typeof(int));
            tmpTable.Columns.Add("GroupID", typeof(int));
            tmpTable.Columns.Add("StateID", typeof(int));
            tmpTable.Columns.Add("First Name", typeof(String));
            tmpTable.Columns.Add("Last Name", typeof(String));
            tmpTable.Columns.Add("Group", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("Creation date", typeof(DateTime));
            tmpTable.Columns.Add("Last edit date", typeof(DateTime));
            tmpTable.Columns.Add("State", typeof(String));
 
            reportsGridView.DataSource = dataSet;
            reportsGridView.DataMember = "Reports";
            reportsGridView.Columns[0].Visible = false;
            reportsGridView.Columns[1].Visible = false;
            reportsGridView.Columns[2].Visible = false;
            reportsGridView.Columns[3].Visible = false;
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

        private void removeIssueButton_Click(object sender, EventArgs e)
        {

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

                    if (stateIDs[index] == Int32.Parse(reportsGridView.Rows[reportsGridView.SelectedRows[0].Index].Cells[3].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                stateComboBox.SelectedIndex = selectedIndex;
            }

            updatingStatesComboBox = false;
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
                cmd.Parameters[1].Value = Int32.Parse(reportsGridView.SelectedRows[0].Cells[0].Value.ToString());

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
}
