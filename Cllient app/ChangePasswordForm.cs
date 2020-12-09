using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.OleDb;

namespace Cllient_app
{
    public partial class ChangePasswordForm : Form
    {
        OleDbConnection cn;

        public ChangePasswordForm()
        {
            InitializeComponent();

            accountTypeComboBox.Items.Clear();
            accountTypeComboBox.Items.Add("student");
            accountTypeComboBox.Items.Add("teacher");
            accountTypeComboBox.Items.Add("administrator");

            oldPasswordTextBox.PasswordChar = '*';
            newPasswordTextBox.PasswordChar = '*';

            cn = new OleDbConnection();
            cn.ConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Persist Security Info=True;Password=orangejuice_1101;User ID=SA;Initial Catalog=University";
            cn.Open();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string sSourceData = oldPasswordTextBox.Text;
            //Create a byte array from source data.
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string tmpHashString = Utility.ByteArrayToString(tmpHash);

            String strSQL = "";

            switch (accountTypeComboBox.SelectedIndex)
            {
                case 0:
                    strSQL = "EXEC SignInForStudent";
                    break;
                case 1:
                    strSQL = "EXEC SignInForTeacher";
                    break;
                case 2:
                    strSQL = "EXEC SignInForAdmin";
                    break;
            }

            strSQL += " @login = ?, @password = ?";
            OleDbCommand cmd = new OleDbCommand(strSQL, cn);

            cmd.Parameters.Add("@p1", OleDbType.VarChar, 50);
            cmd.Parameters.Add("@p2", OleDbType.VarChar, 50);

            cmd.Parameters[0].Value = loginTextBox.Text;
            cmd.Parameters[1].Value = tmpHashString;

            OleDbDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    UserInfo userInfo = new UserInfo();

                    userInfo.id = reader[0].ToString();
                    userInfo.firstName = reader[1].ToString();
                    userInfo.lastName = reader[2].ToString();

                    sSourceData = newPasswordTextBox.Text;
                    tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                    tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                    tmpHashString = Utility.ByteArrayToString(tmpHash);

                    strSQL = "";

                    switch (accountTypeComboBox.SelectedIndex)
                    {
                        case 0:
                            strSQL = "UPDATE Students SET Password = ? WHERE StudentID = ?";
                            break;
                        case 1:
                            strSQL = "UPDATE Teachers SET Password = ? WHERE TeacherID = ?";
                            break;
                        case 2:
                            strSQL = "UPDATE Adminstrators SET Password = ? WHERE AdminID = ?";
                            break;
                    }

                    cmd = new OleDbCommand(strSQL, cn);

                    cmd.Parameters.Add("@p1", OleDbType.VarChar, 50);
                    cmd.Parameters.Add("@p2", OleDbType.VarChar, 50);

                    cmd.Parameters[0].Value = tmpHashString;
                    cmd.Parameters[1].Value = userInfo.id;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password updated");
                    }
                    catch (OleDbException exc)
                    {
                        MessageBox.Show("Password updating failed");
                        MessageBox.Show(exc.ToString());
                    }

                    Form newForm = new SignInForm();

                    switch (accountTypeComboBox.SelectedIndex)
                    {
                        case 0:
                            newForm = new StudentOverviewForm(userInfo, cn);
                            break;
                        case 1:
                            newForm = new TeacherOverviewForm(userInfo, cn);
                            break;
                        case 2:
                            newForm = new DepartmentOverviewForm(userInfo, cn);
                            break;
                    }
                    newForm.Show();

                }
                else
                {
                    MessageBox.Show("Signing in failed");
                }
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cn.Close();
        }
    }
}
