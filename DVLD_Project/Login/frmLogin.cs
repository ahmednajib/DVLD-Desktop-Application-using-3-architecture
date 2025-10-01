using DVLD.Classes;
using DVLD_BuisnessLayer;
using DVLD_PresentationLayer.Global_Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), clsHashing.ComputeHash(txtPassword.Text.Trim()));

            //check if the user is found or not.
            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                frmMain frm = new frmMain(this);
                this.Hide();
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName can not be empty.");
            }
            else
            {
                errorProvider1.SetError(txtUserName, string.Empty);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Password can not be empty.");
            }
            else
            {
                errorProvider1.SetError(txtUserName, string.Empty);
            }
        }
    }
}