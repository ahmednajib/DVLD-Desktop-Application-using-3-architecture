using DVLD_BuisnessLayer;
using DVLD.Classes;
using System;
using System.Windows.Forms;
using DVLD_Project.Licenses.Local_Licenses;

namespace DVLD_Project.Licenses.Renew_License
{
    public partial class frmRenewLDLApplication : Form
    {
        private int _NewLicenseID = -1;
        
        public frmRenewLDLApplication()
        {
            InitializeComponent();
        }

        private void ResetDefaultValues()
        {
            lblExpirationDate.Text = "[??/??/????]";
            lblApplicationID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblLicenseFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblRenewedLicenseID.Text = "[???]";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewLDLApplication_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.txtLicenseIDFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;

            lblExpirationDate.Text = "[??/??/????]";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = SelectedLicenseID != -1;

            if (SelectedLicenseID == -1)
            {
                btnRenewLicense.Enabled = false;

                ResetDefaultValues();
                return;
            }

            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            int DefaultValidityLength = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            txtNotes.Text = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;

            //Check if License Expired
            if(!ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)
                    ,"Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is Detained, Release it first.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID = " + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
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
    }
}