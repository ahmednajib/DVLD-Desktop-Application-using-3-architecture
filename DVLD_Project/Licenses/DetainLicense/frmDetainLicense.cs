using DVLD.Classes;
using DVLD_Project.Licenses.Local_Licenses;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.DetainLicense
{
    public partial class frmDetainLicense : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.txtLicenseIDFocus();
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void _ResetDefaultValues()
        {
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
            {
                btnDetain.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                txtFineFees.Enabled = false;
                _ResetDefaultValues();

                return;
            }

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            if (ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license is already detained.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(txtFineFees.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Please enter the fine fees.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _DetainID = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text.Trim()), clsGlobal.CurrentUser.UserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Failed to detain the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
            btnDetain.Text = "Detained";
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory
                (ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}