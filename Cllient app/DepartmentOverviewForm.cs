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
        private Boolean updatingGroupProgramComboBox = false;
        private Boolean updatingCourceComboBoxes = false;
        private Boolean updatingBuildingForClassroomComboBox = false;
        private Boolean updatingTimetableComboBoxes = false;

        private List<int> groupIDs;
        private List<String> groups;

        private List<int> programIDs;
        private List<String> programs;

        private List<int> courceIDs;
        private List<String> cources;

        private List<int> teacherIDs;
        private List<String> teachers;

        private List<int> subjectIDs;
        private List<String> subjects;

        private List<int> buildingIDs;
        private List<String> buildings;

        private List<int> classroomIDs;
        private List<String> classrooms;

        private List<int> conferenceIDs;
        private List<String> conferences;

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

            teacherIDs = new List<int>();
            teachers = new List<String>();

            subjectIDs = new List<int>();
            subjects = new List<String>();

            buildingIDs = new List<int>();
            buildings = new List<String>();

            classroomIDs = new List<int>();
            classrooms = new List<String>();

            conferenceIDs = new List<int>();
            conferences = new List<String>();

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

            initCourcesTable();
            refreshCourcesTable();

            initBuildingsTable();
            refreshBuildingsTable();

            initConferencesTable();
            refreshConferencesTable();

            initClassroomsTable();
            refreshClassroomsTable();

            initTimetable();
            refreshTimetable();

            removeStudentButton.Enabled = false;
            removeTeacherButton.Enabled = false;
            removeGroupButton.Enabled = false;
            removeCourceForGroupButton.Enabled = false;
            removeSubjectButton.Enabled = false;
            removeCourceButton.Enabled = false;
            removeBuildingButton.Enabled = false;
            removeConferenceButton.Enabled = false;
            timetableRemoveButton.Enabled = false;

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

            // Get teachers

            strSQL = "SELECT * FROM Teachers";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                teachers.Add(reader["FirstName"].ToString() + " " + reader["LastName"].ToString());
                teacherIDs.Add(Int32.Parse(reader["TeacherID"].ToString()));
            }

            // Get subjects

            strSQL = "SELECT * FROM Subjects";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                subjects.Add(reader["Name"].ToString());
                subjectIDs.Add(Int32.Parse(reader["SubjectID"].ToString()));
            }

            // Get buildings

            strSQL = "SELECT * FROM Buildings";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                buildings.Add(reader["Name"].ToString());
                buildingIDs.Add(Int32.Parse(reader["BuildingID"].ToString()));
            }

            // Get classrooms

            strSQL = "EXEC GetDetailedClassrooms";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            classrooms.Add("-");
            classroomIDs.Add(-1);

            while (reader.Read())
            {
                classrooms.Add(reader["Name"].ToString());
                classroomIDs.Add(Int32.Parse(reader["ClassroomID"].ToString()));
            }

            // Get conferences

            strSQL = "EXEC GetDetailedConferences";

            command = new OleDbCommand(strSQL, connection);
            reader = command.ExecuteReader();

            conferences.Add("-");
            conferenceIDs.Add(-1);

            while (reader.Read())
            {
                conferences.Add(reader["Name"].ToString());
                conferenceIDs.Add(Int32.Parse(reader["ConferenceID"].ToString()));
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

        private void initCourcesTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Cources");
            tmpTable.Columns.Add("Cource", typeof(String));
            tmpTable.Columns.Add("Subject", typeof(String));
            tmpTable.Columns.Add("Teacher's first name", typeof(String));
            tmpTable.Columns.Add("Teacher's last name", typeof(String));
            tmpTable.Columns.Add("CourseID", typeof(int));
            tmpTable.Columns.Add("TeacherID", typeof(int));
            tmpTable.Columns.Add("SubjectID", typeof(int));

            courcesGridView.DataSource = dataSet;
            courcesGridView.DataMember = "Cources";

            courcesGridView.Columns[4].Visible = false;
            courcesGridView.Columns[5].Visible = false;
            courcesGridView.Columns[6].Visible = false;

            courcesGridView.Columns[1].ReadOnly = true;
            courcesGridView.Columns[2].ReadOnly = true;
            courcesGridView.Columns[3].ReadOnly = true;
        }

        private void refreshCourcesTable()
        {
            dataSet.Tables["Cources"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetCources";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Cources");
        }

        private void initBuildingsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Buildings");
            tmpTable.Columns.Add("Name", typeof(String));
            tmpTable.Columns.Add("Address", typeof(String));
            tmpTable.Columns.Add("BuildingID", typeof(int));

            buildingsGridView.DataSource = dataSet;
            buildingsGridView.DataMember = "Buildings";
            buildingsGridView.Columns[2].Visible = false;
        }

        private void refreshBuildingsTable()
        {
            dataSet.Tables["Buildings"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "SELECT * FROM Buildings";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Buildings");
        }

        private void initConferencesTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Conferences");
            tmpTable.Columns.Add("Name", typeof(String));
            tmpTable.Columns.Add("Link", typeof(String));
            tmpTable.Columns.Add("ConferenceID", typeof(int));

            conferencesGridView.DataSource = dataSet;
            conferencesGridView.DataMember = "Conferences";
            conferencesGridView.Columns[2].Visible = false;
        }

        private void refreshConferencesTable()
        {
            dataSet.Tables["Conferences"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "SELECT * FROM Conferences";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Conferences");
        }

        private void initClassroomsTable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Classrooms");
            tmpTable.Columns.Add("Number", typeof(int));
            tmpTable.Columns.Add("Floor", typeof(int));
            tmpTable.Columns.Add("Building", typeof(String));
            tmpTable.Columns.Add("ClassroomID", typeof(int));
            tmpTable.Columns.Add("BuildingID", typeof(int));

            classroomsGridView.DataSource = dataSet;
            classroomsGridView.DataMember = "Classrooms";
            classroomsGridView.Columns[3].Visible = false;
            classroomsGridView.Columns[4].Visible = false;

            classroomsGridView.Columns[2].ReadOnly = true;
        }

        private void refreshClassroomsTable()
        {
            dataSet.Tables["Classrooms"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetClassroomsWithBuildings";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Classrooms");
        }

        private void initTimetable()
        {
            DataTable tmpTable;
            tmpTable = dataSet.Tables.Add("Timetable");
            tmpTable.Columns.Add("Datetime", typeof(DateTime));
            tmpTable.Columns.Add("Cource", typeof(String));
            tmpTable.Columns.Add("Classroom", typeof(String));
            tmpTable.Columns.Add("Conference", typeof(String));
            tmpTable.Columns.Add("isExam", typeof(Boolean));
            tmpTable.Columns.Add("isRemote", typeof(Boolean));
            tmpTable.Columns.Add("ClassID", typeof(int));
            tmpTable.Columns.Add("ClassroomID", typeof(int));
            tmpTable.Columns.Add("ConferenceID", typeof(int));
            tmpTable.Columns.Add("CourceID", typeof(int));

            timetableGridView.DataSource = dataSet;
            timetableGridView.DataMember = "Timetable";

            timetableGridView.Columns[6].Visible = false;
            timetableGridView.Columns[7].Visible = false;
            timetableGridView.Columns[8].Visible = false;
            timetableGridView.Columns[9].Visible = false;

            timetableGridView.Columns[1].ReadOnly = true;
            timetableGridView.Columns[2].ReadOnly = true;
            timetableGridView.Columns[3].ReadOnly = true;
        }

        private void refreshTimetable()
        {
            dataSet.Tables["Timetable"].Clear();

            System.Data.OleDb.OleDbDataAdapter adapter;

            String strSQL = "EXEC GetClasses";

            adapter = new OleDbDataAdapter(strSQL, connection);
            adapter.Fill(dataSet, "Timetable");
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
                cmd.Parameters[5].Value = groupIDs[studentsGroupComboBox.SelectedIndex];

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

        private void courcesGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < courcesGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;

                teacherForCourceComboBox.Items.Clear();
                subjectForCourceComboBox.Items.Clear();

                updatingCourceComboBoxes = true;
                int index = 0;
                while (index < teacherIDs.Count)
                {
                    teacherForCourceComboBox.Items.Add(teachers[index]);
                    index++;
                }
                teacherForCourceComboBox.SelectedIndex = 0;

                index = 0;
                while (index < subjectIDs.Count)
                {
                    subjectForCourceComboBox.Items.Add(subjects[index]);
                    index++;
                }
                subjectForCourceComboBox.SelectedIndex = 0;

                removeCourceButton.Enabled = true;
                updatingCourceComboBoxes = false;
            }
        }

        private void courcesGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = courcesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = courcesGridView.Columns[e.ColumnIndex].Name;

                switch (e.ColumnIndex)
                {
                    case 0:
                        columnName = "Name";
                        break;
                    default:
                        break;
                }

                String strSQL = "UPDATE Cources SET " + columnName + " = ? WHERE CourseID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(courcesGridView.Rows[e.RowIndex].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshCourcesTable();
            }
            else
            {
                String name = courcesGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                String strSQL = "INSERT INTO Cources (Name, SubjectID, TeacherID) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p3", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = name;
                cmd.Parameters[1].Value = subjectIDs[subjectForCourceComboBox.SelectedIndex];
                cmd.Parameters[2].Value = teacherIDs[teacherForCourceComboBox.SelectedIndex];

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshCourcesTable();
            }
        }

        private void courcesGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingCourceComboBoxes = true;

            teacherForCourceComboBox.Items.Clear();
            subjectForCourceComboBox.Items.Clear();

            if (courcesGridView.SelectedRows.Count > 0)
            {
                int index = 0;
                int selectedIndex1 = -1;
                while (index < teacherIDs.Count)
                {
                    teacherForCourceComboBox.Items.Add(teachers[index]);

                    if (teacherIDs[index] == Int32.Parse(courcesGridView.SelectedRows[0].Cells[5].Value.ToString()))
                    {
                        selectedIndex1 = index;
                    }
                    index++;
                }
                teacherForCourceComboBox.SelectedIndex = selectedIndex1;

                index = 0;
                int selectedIndex2 = -1;
                while (index < subjectIDs.Count)
                {
                    subjectForCourceComboBox.Items.Add(subjects[index]);

                    if (subjectIDs[index] == Int32.Parse(courcesGridView.SelectedRows[0].Cells[6].Value.ToString()))
                    {
                        selectedIndex2 = index;
                    }
                    index++;
                }
                subjectForCourceComboBox.SelectedIndex = selectedIndex2;

                removeCourceButton.Enabled = true;
            }
            else
            {
                removeCourceButton.Enabled = false;

            }
            updatingCourceComboBoxes = false;
        }

        private void courcesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void removeCource(int index)
        {
            String strSQL = "DELETE FROM Cources WHERE CourseID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = courcesGridView.Rows[index].Cells[4].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeCourceButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < courcesGridView.SelectedRows.Count)
            {
                removeCource(courcesGridView.SelectedRows[index].Index);
                index++;
            }

            refreshCourcesTable();
        }

        private void teacherForCourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (courcesGridView.SelectedRows.Count > 0 && !updatingCourceComboBoxes)
            {
                String strSQL = "UPDATE Cources SET TeacherID = ? WHERE CourseID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = teacherIDs[teacherForCourceComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(courcesGridView.SelectedRows[0].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshCourcesTable();
            }
        }

        private void subjectForCourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (courcesGridView.SelectedRows.Count > 0 && !updatingCourceComboBoxes)
            {
                String strSQL = "UPDATE Cources SET SubjectID = ? WHERE CourseID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = subjectIDs[subjectForCourceComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(courcesGridView.SelectedRows[0].Cells[4].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshCourcesTable();
            }
        }

        private void buildingsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < buildingsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }

        private void buildingsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = buildingsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = buildingsGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Buildings SET " + columnName + " = ? WHERE BuildingID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(buildingsGridView.Rows[e.RowIndex].Cells[2].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshBuildingsTable();
            }
            else
            {
                String name = buildingsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String address = buildingsGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                String strSQL = "INSERT INTO Buildings (Name, Address) VALUES (?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);

                cmd.Parameters[0].Value = name;
                cmd.Parameters[1].Value = address;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshBuildingsTable();
            }
        }

        private void buildingsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (buildingsGridView.SelectedRows.Count > 0)
            {
                removeBuildingButton.Enabled = true;
            }
            else
            {
                removeBuildingButton.Enabled = false;
            }
        }

        private void removeBuilding(int index)
        {
            String strSQL = "DELETE FROM Buildings WHERE BuildingID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = buildingsGridView.Rows[index].Cells[2].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeBuildingButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < buildingsGridView.SelectedRows.Count)
            {
                removeBuilding(buildingsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshBuildingsTable();
        }

        private void conferencesGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < conferencesGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;
            }
        }

        private void conferencesGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = conferencesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = conferencesGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Conferences SET " + columnName + " = ? WHERE ConferenceID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(conferencesGridView.Rows[e.RowIndex].Cells[2].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshConferencesTable();
            }
            else
            {
                String name = conferencesGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String link = conferencesGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                String strSQL = "INSERT INTO Conferences (Name, Link) VALUES (?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.VarWChar, 50);

                cmd.Parameters[0].Value = name;
                cmd.Parameters[1].Value = link;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshConferencesTable();
            }
        }

        private void conferencesGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (conferencesGridView.SelectedRows.Count > 0)
            {
                removeConferenceButton.Enabled = true;
            }
            else
            {
                removeConferenceButton.Enabled = false;
            }
        }

        private void removeConference(int index)
        {
            String strSQL = "DELETE FROM Conferences WHERE ConferenceID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = conferencesGridView.Rows[index].Cells[2].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeConferenceButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < conferencesGridView.SelectedRows.Count)
            {
                removeConference(conferencesGridView.SelectedRows[index].Index);
                index++;
            }

            refreshConferencesTable();
        }

        private void classroomsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < classroomsGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;

                buildingForClassroomComboBox.Items.Clear();

                updatingBuildingForClassroomComboBox = true;
                int index = 0;
                while (index < buildingIDs.Count)
                {
                    buildingForClassroomComboBox.Items.Add(buildings[index]);
                    index++;
                }
                buildingForClassroomComboBox.SelectedIndex = 0;

                removeClassroomButton.Enabled = true;
                updatingBuildingForClassroomComboBox = false;
            }
        }

        private void classroomsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = classroomsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = classroomsGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Classrooms SET " + columnName + " = ? WHERE ClassroomID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.VarWChar, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = Int32.Parse(classroomsGridView.Rows[e.RowIndex].Cells[3].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshClassroomsTable();
            }
            else
            {
                int number = Int32.Parse(classroomsGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                int floor = Int32.Parse(classroomsGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                String strSQL = "INSERT INTO Classrooms (Number, Floor, BuildingID) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p3", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = number;
                cmd.Parameters[1].Value = floor;
                cmd.Parameters[2].Value = buildingIDs[buildingForClassroomComboBox.SelectedIndex];

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshClassroomsTable();
            }
        }

        private void classroomsGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingBuildingForClassroomComboBox = true;

            buildingForClassroomComboBox.Items.Clear();

            if (classroomsGridView.SelectedRows.Count > 0)
            {
                int index = 0;
                int selectedIndex = -1;
                while (index < buildingIDs.Count)
                {
                    buildingForClassroomComboBox.Items.Add(buildings[index]);

                    if (buildingIDs[index] == Int32.Parse(classroomsGridView.Rows[classroomsGridView.SelectedRows[0].Index].Cells[4].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                buildingForClassroomComboBox.SelectedIndex = selectedIndex;

                removeBuildingButton.Enabled = true;
            }
            else
            {
                removeBuildingButton.Enabled = false;
            }

            updatingBuildingForClassroomComboBox = false;
        }

        private void buildingForClassroomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classroomsGridView.SelectedRows.Count > 0 && !updatingBuildingForClassroomComboBox)
            {
                String strSQL = "UPDATE Classrooms SET BuildingID = ? WHERE ClassroomID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = buildingIDs[buildingForClassroomComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(classroomsGridView.SelectedRows[0].Cells[3].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshClassroomsTable();
            }
        }

        private void removeClassroom(int index)
        {
            String strSQL = "DELETE FROM Classrooms WHERE ClassroomID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = classroomsGridView.Rows[index].Cells[3].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void removeClassroomButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < classroomsGridView.SelectedRows.Count)
            {
                removeClassroom(classroomsGridView.SelectedRows[index].Index);
                index++;
            }

            refreshClassroomsTable();
        }

        private void classroomsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = 0;
            e.Row.Cells[1].Value = 0;
        }

        private void timetableGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < timetableGridView.Rows.Count - 1)
            {
                editingLastRow = false;
            }
            else
            {
                editingLastRow = true;

                timetableConferenceComboBox.Items.Clear();
                timetableClassroomComboBox.Items.Clear();
                timetableCourceComboBox.Items.Clear();

                updatingTimetableComboBoxes = true;
                int index = 0;
                while (index < conferenceIDs.Count)
                {
                    timetableConferenceComboBox.Items.Add(conferences[index]);
                    index++;
                }
                timetableConferenceComboBox.SelectedIndex = 0;

                index = 0;
                while (index < classroomIDs.Count)
                {
                    timetableClassroomComboBox.Items.Add(classrooms[index]);
                    index++;
                }
                timetableClassroomComboBox.SelectedIndex = 0;

                index = 0;
                while (index < courceIDs.Count)
                {
                    timetableCourceComboBox.Items.Add(cources[index]);
                    index++;
                }
                timetableCourceComboBox.SelectedIndex = 0;

                timetableRemoveButton.Enabled = true;
                updatingTimetableComboBoxes = false;
            }
        }

        private void timetableGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editingLastRow)
            {

                String newValue = timetableGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                String columnName = timetableGridView.Columns[e.ColumnIndex].Name;

                String strSQL = "UPDATE Classes SET " + columnName + " = ? WHERE ClassID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                switch (e.ColumnIndex)
                {
                    case 0:
                        cmd.Parameters.Add("@p1", OleDbType.Date, 50);
                        break;
                    default:
                        cmd.Parameters.Add("@p1", OleDbType.Boolean, 50);
                        break;
                }

                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = newValue;
                cmd.Parameters[1].Value = timetableGridView.Rows[e.RowIndex].Cells[6].Value.ToString();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTimetable();
            }
            else
            {
                String datetime = timetableGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String isExam = timetableGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                String isRemote = timetableGridView.Rows[e.RowIndex].Cells[5].Value.ToString();

                String strSQL = "INSERT INTO Classes (Datetime, CourceID, ClassroomID, ConferenceID, isExam, isRemote) VALUES (?, ?, ?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Date, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p3", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p4", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p5", OleDbType.Boolean, 50);
                cmd.Parameters.Add("@p6", OleDbType.Boolean, 50);

                cmd.Parameters[0].Value = datetime;
                cmd.Parameters[1].Value = courceIDs[timetableCourceComboBox.SelectedIndex];
                if (timetableClassroomComboBox.SelectedIndex == 0)
                {
                    cmd.Parameters[2].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[2].Value = classroomIDs[timetableClassroomComboBox.SelectedIndex];
                }
                if (timetableConferenceComboBox.SelectedIndex == 0)
                {
                    cmd.Parameters[3].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[3].Value = conferenceIDs[timetableConferenceComboBox.SelectedIndex];
                }
                cmd.Parameters[4].Value = isExam;
                cmd.Parameters[5].Value = isRemote;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTimetable();
            }
        }

        private void timetableGridView_SelectionChanged(object sender, EventArgs e)
        {
            updatingTimetableComboBoxes = true;

            timetableCourceComboBox.Items.Clear();
            timetableClassroomComboBox.Items.Clear();
            timetableConferenceComboBox.Items.Clear();

            if (timetableGridView.SelectedRows.Count > 0)
            {

                int index = 0;
                int selectedIndex = -1;
                while (index < courceIDs.Count)
                {
                    timetableCourceComboBox.Items.Add(cources[index]);

                    if (courceIDs[index] == Int32.Parse(timetableGridView.SelectedRows[0].Cells[9].Value.ToString()))
                    {
                        selectedIndex = index;
                    }
                    index++;
                }
                timetableCourceComboBox.SelectedIndex = selectedIndex;

                index = 0;
                selectedIndex = -1;
                while (index < classroomIDs.Count)
                {
                    timetableClassroomComboBox.Items.Add(classrooms[index]);

                    if (timetableGridView.SelectedRows[0].Cells[7].Value.ToString() == "")
                    {
                        selectedIndex = 0;
                    }
                    else
                    {
                        if (classroomIDs[index] == Int32.Parse(timetableGridView.SelectedRows[0].Cells[7].Value.ToString()))
                        {
                            selectedIndex = index;
                        }
                    }
                    index++;
                }
                timetableClassroomComboBox.SelectedIndex = selectedIndex;

                index = 0;
                selectedIndex = -1;
                while (index < conferenceIDs.Count)
                {
                    timetableConferenceComboBox.Items.Add(conferences[index]);

                    if (timetableGridView.SelectedRows[0].Cells[8].Value.ToString() == "")
                    {
                        selectedIndex = 0;
                    }
                    else
                    {
                        if (conferenceIDs[index] == Int32.Parse(timetableGridView.SelectedRows[0].Cells[8].Value.ToString()))
                        {
                            selectedIndex = index;
                        }
                    }
                    index++;
                }
                timetableConferenceComboBox.SelectedIndex = selectedIndex;

                timetableRemoveButton.Enabled = true;
            }
            else
            {
                timetableRemoveButton.Enabled = false;
            }
            updatingTimetableComboBoxes = false;
        }

        private void timetableGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = DateTime.Today;
            e.Row.Cells[4].Value = false;
            e.Row.Cells[5].Value = false;
        }

        private void timetableClassroomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timetableGridView.SelectedRows.Count > 0 && !updatingTimetableComboBoxes)
            {
                String strSQL = "UPDATE Classes SET ClassroomID = ? WHERE ClassID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                if (timetableClassroomComboBox.SelectedIndex == 0)
                {
                    cmd.Parameters[0].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[0].Value = classroomIDs[timetableClassroomComboBox.SelectedIndex];
                }

                cmd.Parameters[1].Value = Int32.Parse(timetableGridView.SelectedRows[0].Cells[6].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTimetable();
            }
        }

        private void timetableConferenceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timetableGridView.SelectedRows.Count > 0 && !updatingTimetableComboBoxes)
            {
                String strSQL = "UPDATE Classes SET ConferenceID = ? WHERE ClassID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                if (timetableConferenceComboBox.SelectedIndex == 0)
                {
                    cmd.Parameters[0].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[0].Value = conferenceIDs[timetableConferenceComboBox.SelectedIndex];
                }
                cmd.Parameters[1].Value = Int32.Parse(timetableGridView.SelectedRows[0].Cells[6].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTimetable();
            }
        }

        private void timetableCourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timetableGridView.SelectedRows.Count > 0 && !updatingTimetableComboBoxes)
            {
                String strSQL = "UPDATE Classes SET CourceID = ? WHERE ClassID = ?";
                OleDbCommand cmd = new OleDbCommand(strSQL, connection);

                cmd.Parameters.Add("@p1", OleDbType.Integer, 50);
                cmd.Parameters.Add("@p2", OleDbType.Integer, 50);

                cmd.Parameters[0].Value = courceIDs[timetableCourceComboBox.SelectedIndex];
                cmd.Parameters[1].Value = Int32.Parse(timetableGridView.SelectedRows[0].Cells[6].Value.ToString());

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                refreshTimetable();
            }
        }

        private void removeClass(int index)
        {
            String strSQL = "DELETE FROM Classes WHERE ClassID = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);

            cmd.Parameters.Add("@p1", OleDbType.Integer, 50);

            cmd.Parameters[0].Value = timetableGridView.Rows[index].Cells[7].Value;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void timetableRemoveButton_Click(object sender, EventArgs e)
        {
            int index = 0;

            while (index < timetableGridView.SelectedRows.Count)
            {
                removeClass(timetableGridView.SelectedRows[index].Index);
                index++;
            }

            refreshTimetable();
        }
    }
}
