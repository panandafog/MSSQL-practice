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
using System.Security.Cryptography;

namespace Cllient_app
{
    public partial class DepartmentOverviewForm : Form
    {
        UserInfo userInfo;
        OleDbConnection connection;

        private System.Data.DataSet dataSet;

        private Boolean editingLastRow = false;
        private Boolean updatingStudentGroupComboBox = false;

        private List<int> groupIDs;
        private List<String> groups;

        public DepartmentOverviewForm(UserInfo userInfo, OleDbConnection connection)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;

            dataSet = new DataSet();

            groupIDs = new List<int>();
            groups = new List<String>();

            this.connection = connection;

            initStudentsTable();
            refreshStudentsTable();

            initTeachersTable();
            refreshTeachersTable();

            removeStudentButton.Enabled = false;
            removeTeacherButton.Enabled = false;

            // Get groups

            String strSQL = "SELECT * FROM Groups";

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                groups.Add(reader["Name"].ToString());
                groupIDs.Add(Int32.Parse(reader["GroupID"].ToString()));
            }
        }


        private void DepartmentOverviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void initStudentsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Students");
            tmpTable.Columns.Add("First name", typeof(String));
            tmpTable.Columns.Add("Last name", typeof(String));
            tmpTable.Columns.Add("Email", typeof(String));
            tmpTable.Columns.Add("Login", typeof(String));
            tmpTable.Columns.Add("Group", typeof(String));
            tmpTable.Columns.Add("StudentID", typeof(int));
            tmpTable.Columns.Add("GroupID", typeof(int));

            studentsGridView.DataSource = dataSet;
            studentsGridView.DataMember = "Students";
            studentsGridView.Columns[5].Visible = false;
            studentsGridView.Columns[6].Visible = false;

            studentsGridView.Columns[4].ReadOnly = true;
        }

        private void refreshStudentsTable()
        {
            dataSet.Tables["Students"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetStudentsWithGroups";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Students");
        }

        private void initTeachersTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Teachers");
            tmpTable.Columns.Add("First name", typeof(String));
            tmpTable.Columns.Add("Last name", typeof(String));
            tmpTable.Columns.Add("Email", typeof(String));
            tmpTable.Columns.Add("Login", typeof(String));
            tmpTable.Columns.Add("TeacherID", typeof(int));

            teachersGridView.DataSource = dataSet;
            teachersGridView.DataMember = "Teachers";
            teachersGridView.Columns[4].Visible = false;
        }

        private void refreshTeachersTable()
        {
            dataSet.Tables["Teachers"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "SELECT FirstName AS [First name], LastName AS [Last name], Email, Login, TeacherID FROM Teachers";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Teachers");
        }

        private void studentsGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingStudentGroupComboBox = true;

            studentsGroupComboBox.Items.Clear();

            if (studentsGridView.SelectedRows.Count > 0)
            {
                int index = 0;
                int selectedIndex = -1;
                while (index < groupIDs.Count)
                {
                    studentsGroupComboBox.Items.Add(groups[index]);

                    if (groupIDs[index] == Int32.Parse(studentsGridView.Rows[studentsGridView.SelectedRows[0].Index].Cells[6].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                studentsGroupComboBox.SelectedIndex = selectedIndex;

                removeStudentButton.Enabled = true;
            }
            else
            {
                removeStudentButton.Enabled = false;
            }

            updatingStudentGroupComboBox = false;
        }

        private void studentsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < studentsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;

                studentsGroupComboBox.Items.Clear();

                updatingStudentGroupComboBox = true;
                int index = 0;
                while (index < groupIDs.Count)
                {
                    studentsGroupComboBox.Items.Add(groups[index]);
                    index++;
                }
                studentsGroupComboBox.SelectedIndex = 0;

                removeStudentButton.Enabled = true;
                updatingStudentGroupComboBox = false;
            }
        }

        private void studentsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = studentsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = studentsGridView.Columns[e.ColumnIndex].Name;

                switch (e.ColumnIndex)
                {
                    case 0:
                        columnName = "FirstName";
                        break;
                    case 1:
                        columnName = "LastName";
                        break;
                    default:
                        break;
                }

                String strSQL = "UPDATE Students SET " + columnName + " = ? WHERE StudentID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(studentsGridView.Rows[e.RowIndex].Cells[5].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshStudentsTable();
            }
            else
            {
                String fName = studentsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String lName = studentsGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                String email = studentsGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                String login = studentsGridView.Rows[e.RowIndex].Cells[3].Value.ToString();

                string sSourceData = "1234";
                byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                String password = Utility.ByteArrayToString(tmpHash);

                String strSQL = "INSERT INTO Students (FirstName, LastName, Email, Login, Password, GroupID) VALUES (?, ?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p3", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p4", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p5", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p6", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = fName;
                cmd.Parameters[1].Value = lName;
                cmd.Parameters[2].Value = email;
                cmd.Parameters[3].Value = login;
                cmd.Parameters[4].Value = password;
                cmd.Parameters[5].Value = groupIDs[studentsGroupComboBox.SelectedIndex]; ;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshStudentsTable();
            }
        }

        private void removeStudent(int index)
        {
            String strSQL = "DELETE FROM Students WHERE StudentID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = studentsGridView.Rows[index].Cells[5].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeStudentButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < studentsGridView.SelectedRows.Count)
            {
                removeStudent(studentsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshStudentsTable();
        }

        private void studentsGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studentsGridView.SelectedRows.Count > 0 && !updatingStudentGroupComboBox)
            {
                String strSQL = "UPDATE Students SET GroupID = ? WHERE StudentID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = groupIDs[studentsGroupComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(studentsGridView.SelectedRows[0].Cells[5].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshStudentsTable();
            }
        }

        private void teachersGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < teachersGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }

        private void teachersGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = teachersGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = teachersGridView.Columns[e.ColumnIndex].Name;

                switch (e.ColumnIndex)
                {
                    case 0:
                        columnName = "FirstName";
                        break;
                    case 1:
                        columnName = "LastName";
                        break;
                    default:
                        break;
                }

                String strSQL = "UPDATE Teachers SET " + columnName + " = ? WHERE TeacherID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(teachersGridView.Rows[e.RowIndex].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTeachersTable();
            }
            else
            {
                String fName = teachersGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String lName = teachersGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                String email = teachersGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                String login = teachersGridView.Rows[e.RowIndex].Cells[3].Value.ToString();

                string sSourceData = "1234";
                byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                String password = Utility.ByteArrayToString(tmpHash);

                String strSQL = "INSERT INTO Teachers (FirstName, LastName, Email, Login, Password) VALUES (?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p3", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p4", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p5", OleDbType.VarWChar, 50);

                cmd.Parameters[0].Value = fName;
                cmd.Parameters[1].Value = lName;
                cmd.Parameters[2].Value = email;
                cmd.Parameters[3].Value = login;
                cmd.Parameters[4].Value = password;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTeachersTable();
            }
        }

        private void teachersGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (teachersGridView.SelectedRows.Count > 0)
            {
                removeTeacherButton.Enabled = true;
            }
            else
            {
                removeTeacherButton.Enabled = false;
            }
        }

        private void removeTeacher(int index)
        {
            String strSQL = "DELETE FROM Teachers WHERE TeacherID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = teachersGridView.Rows[index].Cells[4].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeTeacherButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < teachersGridView.SelectedRows.Count)
            {
                removeTeacher(teachersGridView.SelectedRows[index].Index);
                index++;
            }

            refreshTeachersTable();
        }
    }
}
