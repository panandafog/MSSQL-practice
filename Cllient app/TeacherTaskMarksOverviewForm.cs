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
    public partial class TeacherTaskMarksOverviewForm : Form
    {
        UserInfo userInfo;
        int taskID;

        private List<int> groupIDs;

        OleDbConnection connection;
        private System.Data.DataSet dataSet;

        private Boolean editingLastRow = false;

        public TeacherTaskMarksOverviewForm(UserInfo userInfo, OleDbConnection connection, int taskID)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            this.taskID = taskID;
            this.connection = connection;

            dataSet = new DataSet();
            groupIDs = new List<int>();

            initGroupsComboBox();
            initMarksTable();

            removeMarkButton.Enabled = false;
        }

        private void initGroupsComboBox()
        {
            String strSQL = "EXEC GetGroupsHavingTask @taskID = " +
                taskID;

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            groupComboBox.Items.Clear();

            while (reader.Read())
            {
                groupComboBox.Items.Add(reader["Name"]);
                groupIDs.Add(Int32.Parse(reader["GroupID"].ToString()));
            }
        }

        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshMarksTable();
        }

        private void initMarksTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Marks");
            tmpTable.Columns.Add("First name", typeof(String));
            tmpTable.Columns.Add("Last name", typeof(String));
            tmpTable.Columns.Add("Mark", typeof(int));
            tmpTable.Columns.Add("Date", typeof(DateTime));
            tmpTable.Columns.Add("StudentID", typeof(int));
            tmpTable.Columns.Add("MarkID", typeof(int));

            marksGridView.DataSource = dataSet;
            marksGridView.DataMember = "Marks";
            marksGridView.Columns[4].Visible = false;
            marksGridView.Columns[5].Visible = false;

            marksGridView.Columns[0].ReadOnly = true;
            marksGridView.Columns[1].ReadOnly = true;
            marksGridView.Columns[3].ReadOnly = true;
        }

        private void refreshMarksTable()
        {
            dataSet.Tables["Marks"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetMarksForTaskInGroup @taskID = " +
                taskID + ", @groupID = " + groupIDs[groupComboBox.SelectedIndex];

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Marks");
        }

        private void removeMarkButton_Click(object sender, EventArgs e)
        {
            if (marksGridView.SelectedRows.Count > 0)
            {
                String strSQL = "DELETE FROM Marks WHERE MarkID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = Int32.Parse(marksGridView.SelectedRows[0].Cells[5].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshMarksTable();
            }
        }

        private void marksGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (marksGridView.SelectedRows.Count > 0)
            {
                removeMarkButton.Enabled = true;
            } 
            else
            {
                removeMarkButton.Enabled = false;
            }
        }

        private void marksGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            String newValue = marksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            String strSQL = "EXEC UpdateOrInsertMark @taskID = ?, @studentID = ?, @mark = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
            cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
            cmd.Parameters.Add("@p3", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = taskID;
            cmd.Parameters[1].Value = Int32.Parse(marksGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
            cmd.Parameters[2].Value = Int32.Parse(newValue);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }

            refreshMarksTable();
        }
    }
}
