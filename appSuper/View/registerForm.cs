using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appSuper
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            //loginForm loginForm = new loginForm();
            //loginForm.Show();

            this.Close();
        }
    }
}
