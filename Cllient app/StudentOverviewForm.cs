using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cllient_app
{
    public partial class StudentOverviewForm : Form
    {
        UserInfo userInfo;

        public StudentOverviewForm(UserInfo userInfo)
        {
            InitializeComponent();

            this.userInfo = userInfo;
            userInfoLabel.Text = this.userInfo.lastName + " " + this.userInfo.firstName;
        }
    }
}
