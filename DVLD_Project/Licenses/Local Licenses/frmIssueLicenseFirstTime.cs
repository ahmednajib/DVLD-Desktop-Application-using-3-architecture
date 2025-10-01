using System;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD_BuisnessLayer;

namespace DVLD_Project.Licenses.Local_Licenses
{
    public partial class frmIssueLicenseFirstTime : Form
    {
        private int _LDLApplicationID;
        private clsLDLApplication _LDLApplication;

        public frmIssueLicenseFirstTime(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = _LDLApplication.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIssueLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LDLApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(_LDLApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _LDLApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LDLApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LDLApplication.GetActiveLicenseID();

            if (LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
        }
    }
}
