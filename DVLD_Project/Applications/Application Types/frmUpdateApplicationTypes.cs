using DVLD_BuisnessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Application_Types
{
    public partial class frmUpdateApplicationTypes : Form
    {
        private int _ApplicationTypeID = -1;

        private clsApplicationTypes _ApplicationType;

        public frmUpdateApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(((TextBox)sender), "This Field cannot be empty");
                return;
            }
            else
            {
                errorProvider1.SetError(((TextBox)sender), null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToInt32(txtFees.Text.Trim());

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Application Type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Failed to update Application Type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);

            if (_ApplicationType != null)
            {
                lblID.Text = _ApplicationType.ApplicationTypeId.ToString();
                txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            }
            else
            {
                MessageBox.Show("Application Type not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
                // Allow backspace and delete keys
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}