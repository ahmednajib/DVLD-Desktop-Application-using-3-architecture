using System;
using System.Windows.Forms;
using DVLD_BuisnessLayer;

namespace DVLD_Project.Applications.localdrivinglicense
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LDLApplicationID;
        private clsLDLApplication _LDLApplication;
        private int _LicenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LDLApplicationID; }
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LDLApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(LocalDrivingLicenseApplicationID);
            if (_LDLApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LDLApplication = clsLDLApplication.FindByApplicationID(ApplicationID);
            if (_LDLApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LDLApplication.GetActiveLicenseID();

            lblLocalDrivingLicenseApplicationID.Text = _LDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = _LDLApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _LDLApplication.GetPassedTestCount().ToString() + "/3";
            ctrlBaseApplicationInfo1.LoadApplicationInfo(_LDLApplication.ApplicationID);
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LDLApplicationID = -1;
            ctrlBaseApplicationInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }
    }
}