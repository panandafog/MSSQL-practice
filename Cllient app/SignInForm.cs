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
using System.Text;

using System.Data.OleDb;

namespace Cllient_app
{
    public partial class SignInForm : Form
    {
        OleDbConnection cn;

        public SignInForm()
        {
            InitializeComponent();

            cn = new OleDbConnection();
            cn.ConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Persist Security Info=True;Password=orangejuice_1101;User ID=SA;Initial Catalog=University";
            cn.Open();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            string sSourceData = passwordTextBox.Text;
            //Create a byte array from source data.
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string tmpHashString = Utility.ByteArrayToString(tmpHash);

            String strSQL = "EXEC SignInForAdmin @login = ?, @password = ?";
            OleDbCommand cmdIC = new OleDbCommand(strSQL, cn);

            cmdIC.Parameters.Add("@p1", OleDbType.VarChar, 50);
            cmdIC.Parameters.Add("@p2", OleDbType.VarChar, 50);

            cmdIC.Parameters[0].Value = loginTextBox.Text;
            cmdIC.Parameters[1].Value = tmpHashString;

            try
            {
                cmdIC.ExecuteNonQuery();
                MessageBox.Show("Signed in succesfully");
            }
            catch (OleDbException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cn.Close();
        }
    }
}
