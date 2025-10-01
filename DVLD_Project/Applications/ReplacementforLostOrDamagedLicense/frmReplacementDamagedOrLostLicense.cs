using System;
using System.Windows.Forms;
using DVLD_BuisnessLayer;
using DVLD.Classes;
using DVLD_Project.Licenses.Local_Licenses;
using DVLD_Project.Licenses;

namespace DVLD_Project.Applications.Replacement_for_Lost_or_damaged_License
{
    public partial class frmReplacementDamagedOrLostLicense : Form
    {
        private int _NewLicenseID = -1;
        private clsApplication.enApplicationType _ApplicationType = clsApplication.enApplicationType.ReplaceLostDrivingLicense;

        public frmReplacementDamagedOrLostLicense()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblApplicationID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
        }

        private void frmReplacementDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.txtLicenseIDFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                lblTitle.Text = "Replacement for Damaged License";
                this.Text = "Replacement Damaged License";
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees.ToString();
                _ApplicationType = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLostLicense.Checked)
            {
                lblTitle.Text = "Replacement for Lost License";
                this.Text = "Replacement Lost License";
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();
                _ApplicationType = clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = SelectedLicenseID != -1;

            if (SelectedLicenseID == -1)
            {
                btnReplaceLicense.Enabled = false;
                _ResetDefaultValues();
                return;
            }

            //Check if License Expired
            if (ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License was expiared on: " + clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)
                    + "Renew it.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplaceLicense.Enabled = false;
                return;
            }

            if(ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is Detained, Release it first.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplaceLicense.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplaceLicense.Enabled = false;
                return;
            }

            btnReplaceLicense.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory
                (ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnReplaceLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceLicenseForDamgedOrLost(_ApplicationType, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Replace the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID = " + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplaceLicense.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            gbReplacementFor.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}