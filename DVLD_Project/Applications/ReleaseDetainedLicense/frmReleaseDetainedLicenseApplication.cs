using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD_BuisnessLayer;
using DVLD_Project.Licenses.Local_Licenses;

namespace DVLD_Project.Licenses.DetainLicense
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

            ctrlLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void _ResetDefaultValues()
        {
            lblApplicationFees.Text = "[$$$$]";
            lblApplicationID.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblDetainDate.Text = "[??/??/????]";
            lblDetainID.Text = "[???]";
            lblFineFees.Text = "[$$$$]";
            lblFineFees.Text = "[$$$$]";
            lblLicenseID.Text = "[????]";
            lblTotalFees.Text = "[$$$$]";
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
            {
                btnRelease.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                _ResetDefaultValues();

                return;
            }

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            if (!ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license is not Detained.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblCreatedByUser.Text = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;
            lblDetainID.Text = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;
            
            bool IsReleased = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            if (!IsReleased)
            {
                MessageBox.Show("Failed to release the detained license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ApplicationID.ToString();
            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
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
    }
}