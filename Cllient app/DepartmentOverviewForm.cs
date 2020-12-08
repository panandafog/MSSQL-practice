﻿using System;
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
        private Boolean updatingGroupProgramComboBox = false;

        private List<int> groupIDs;
        private List<String> groups;

        private List<int> programIDs;
        private List<String> programs;

        private List<int> courceIDs;
        private List<String> cources;

        public DepartmentOverviewForm(UserInfo userInfo, OleDbConnection connection)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;

            dataSet = new DataSet();

            groupIDs = new List<int>();
            groups = new List<String>();

            programIDs = new List<int>();
            programs = new List<String>();

            courceIDs = new List<int>();
            cources = new List<String>();

            this.connection = connection;

            initStudentsTable();
            refreshStudentsTable();

            initTeachersTable();
            refreshTeachersTable();

            initGroupsTable();
            refreshGroupsTable();

            initCourcesForGroupTable();

            initSubjectsTable();
            refreshSubjectsTable();

            removeStudentButton.Enabled = false;
            removeTeacherButton.Enabled = false;
            removeGroupButton.Enabled = false;
            removeCourceForGroupButton.Enabled = false;

            addCourceForGroupButton.Enabled = false;

            // Get groups

            String strSQL = "SELECT * FROM Groups";

            OleDbCommand command = new OleDbCommand(strSQL, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                groups.Add(reader["Name"].ToString());
                groupIDs.Add(Int32.Parse(reader["GroupID"].ToString()));
            }

            // Get programs

            strSQL = "SELECT * FROM Programs";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                programs.Add(reader["Name"].ToString());
                programIDs.Add(Int32.Parse(reader["ProgramID"].ToString()));
            }

            // Get cources

            strSQL = "SELECT * FROM Cources";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                cources.Add(reader["Name"].ToString());
                courceIDs.Add(Int32.Parse(reader["CourseID"].ToString()));
                addCourceForGroupComboBox.Items.Add(cources.Last<string>());
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

        private void initGroupsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Groups");
            tmpTable.Columns.Add("Group", typeof(String));
            tmpTable.Columns.Add("Year", typeof(int));
            tmpTable.Columns.Add("Program", typeof(String));
            tmpTable.Columns.Add("ProgramID", typeof(int));
            tmpTable.Columns.Add("GroupID", typeof(int));

            groupsGridView.DataSource = dataSet;
            groupsGridView.DataMember = "Groups";
            groupsGridView.Columns[3].Visible = false;
            groupsGridView.Columns[4].Visible = false;

            groupsGridView.Columns[3].ReadOnly = true;
        }

        private void refreshGroupsTable()
        {
            dataSet.Tables["Groups"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetGroupsWithPrograms";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Groups");
        }

        private void initCourcesForGroupTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("CourcesForGroup");
            tmpTable.Columns.Add("Cource name", typeof(String));
            tmpTable.Columns.Add("Subject", typeof(String));
            tmpTable.Columns.Add("Teacher's first name", typeof(String));
            tmpTable.Columns.Add("Teacher's last name", typeof(String));
            tmpTable.Columns.Add("CourseID", typeof(int));

            courcesForGroupGridView.DataSource = dataSet;
            courcesForGroupGridView.DataMember = "CourcesForGroup";

            courcesForGroupGridView.Columns[4].Visible = false;
        }

        private void refreshCourcesForGroupTable()
        {
            if (groupsGridView.SelectedRows.Count > 0)
            {
                dataSet.Tables["CourcesForGroup"].Clear();

                System.Data.OleDb.OleDbDataAdapter adapter;

                String strSQL = "EXEC GetCourcesForGroup @groupID = " + groupsGridView.SelectedRows[0].Cells[4].Value.ToString();

                adapter = new OleDbDataAdapter(strSQL, connection);
                adapter.Fill(dataSet, "CourcesForGroup");
            }
        }

        private void initSubjectsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Subjects");
            tmpTable.Columns.Add("Name", typeof(String));
            tmpTable.Columns.Add("SubjectID", typeof(int));

            subjectsGridView.DataSource = dataSet;
            subjectsGridView.DataMember = "Subjects";
            subjectsGridView.Columns[1].Visible = false;
        }

        private void refreshSubjectsTable()
        {
            dataSet.Tables["Subjects"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "SELECT * FROM Subjects";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Subjects");
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

        private void groupsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < groupsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;

                programOfGroupComboBox.Items.Clear();

                updatingGroupProgramComboBox = true;
                int index = 0;
                while (index < programIDs.Count)
                {
                    programOfGroupComboBox.Items.Add(programs[index]);
                    index++;
                }
                programOfGroupComboBox.SelectedIndex = 0;

                removeGroupButton.Enabled = true;
                updatingGroupProgramComboBox = false;
            }
        }

        private void groupsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = groupsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = groupsGridView.Columns[e.ColumnIndex].Name;

                switch (e.ColumnIndex)
                {
                    case 0:
                        columnName = "Name";
                        break;
                    default:
                        break;
                }

                String strSQL = "UPDATE Groups SET " + columnName + " = ? WHERE GroupID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(groupsGridView.Rows[e.RowIndex].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshGroupsTable();
            }
            else
            {
                String name = groupsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                int year = Int32.Parse(groupsGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                String strSQL = "INSERT INTO Groups (Name, Year, ProgramID) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p3", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = name;
                cmd.Parameters[1].Value = year;
                cmd.Parameters[2].Value = programIDs[programOfGroupComboBox.SelectedIndex]; ;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshGroupsTable();
            }
        }

        private void groupsGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingGroupProgramComboBox = true;

            programOfGroupComboBox.Items.Clear();

            if (groupsGridView.SelectedRows.Count > 0)
            {
                int index = 0;
                int selectedIndex = -1;
                while (index < programIDs.Count)
                {
                    programOfGroupComboBox.Items.Add(programs[index]);

                    if (programIDs[index] == Int32.Parse(groupsGridView.Rows[groupsGridView.SelectedRows[0].Index].Cells[3].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                programOfGroupComboBox.SelectedIndex = selectedIndex;

                removeGroupButton.Enabled = true;
                addCourceForGroupButton.Enabled = true;

                refreshCourcesForGroupTable();
            }
            else
            {
                removeGroupButton.Enabled = false;
                addCourceForGroupButton.Enabled = false;
            }
            updatingGroupProgramComboBox = false;
        }

        private void removeGroup(int index)
        {
            String strSQL = "DELETE FROM Groups WHERE GroupID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = groupsGridView.Rows[index].Cells[4].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeGroupButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < groupsGridView.SelectedRows.Count)
            {
                removeGroup(groupsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshGroupsTable();
        }

        private void programOfGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupsGridView.SelectedRows.Count > 0 && !updatingGroupProgramComboBox)
            {
                String strSQL = "UPDATE Groups SET ProgramID = ? WHERE GroupID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = programIDs[programOfGroupComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(groupsGridView.SelectedRows[0].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshGroupsTable();
            }
        }

        private void courcesForGroupGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (courcesForGroupGridView.SelectedRows.Count > 0)
            {
                removeCourceForGroupButton.Enabled = true;
            }
            else
            {
                removeCourceForGroupButton.Enabled = false;
            }
        }

        private void groupsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = 1;
        }

        private void removeCourceForGroup(int index)
        {
            String strSQL = "DELETE FROM [Groups-Cources] WHERE GroupID = ? AND CourceID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
            cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = groupsGridView.SelectedRows[0].Cells[4].Value;
            cmd.Parameters[1].Value = courcesForGroupGridView.Rows[index].Cells[4].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeCourceForGroupButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < courcesForGroupGridView.SelectedRows.Count)
            {
                removeCourceForGroup(courcesForGroupGridView.SelectedRows[index].Index);
                index++;
            }

            refreshCourcesForGroupTable();
        }

        private void addCourceForGroupButton_Click(object sender, EventArgs e)
        {
            String strSQL = "INSERT INTO [Groups-Cources] (GroupID, CourceID) VALUES (?, ?)";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
            cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = groupsGridView.SelectedRows[0].Cells[4].Value;
            cmd.Parameters[1].Value = courceIDs[addCourceForGroupComboBox.SelectedIndex];

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
               // MessageBox.Show(exc.ToString());
            }

            refreshCourcesForGroupTable();
        }

        private void subjectsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < subjectsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }

        private void subjectsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = subjectsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = subjectsGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Subjects SET " + columnName + " = ? WHERE SubjectID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(subjectsGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshSubjectsTable();
            }
            else
            {
                String name = subjectsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();


                String strSQL = "INSERT INTO Subjects (Name) VALUES (?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);

                cmd.Parameters[0].Value = name;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshSubjectsTable();
            }
        }

        private void subjectsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (subjectsGridView.SelectedRows.Count > 0)
            {
                removeSubjectButton.Enabled = true;
            }
            else
            {
                removeSubjectButton.Enabled = false;
            }
        }

        private void removeSubject(int index)
        {
            String strSQL = "DELETE FROM Subjects WHERE SubjectID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = subjectsGridView.Rows[index].Cells[1].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeSubjectButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < subjectsGridView.SelectedRows.Count)
            {
                removeSubject(subjectsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshSubjectsTable();
        }
    }
}
