using DVLD.Classes;
using DVLD_Project.Applications.Application_Types;
using DVLD_Project.Applications.InternationalLicense;
using DVLD_Project.Applications.localdrivinglicense;
using DVLD_Project.Applications.Replacement_for_Lost_or_damaged_License;
using DVLD_Project.Drivers;
using DVLD_Project.Licenses.DetainLicense;
using DVLD_Project.Licenses.LocalDrivingLicense;
using DVLD_Project.Licenses.Renew_License;
using DVLD_Project.Login;
using DVLD_Project.Tests.TestTypes;
using DVLD_Project.Users;
using System;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmMain : Form
    {
        frmLogin _LoginForm;
        private bool isLoggingOut = false;

        public frmMain(frmLogin LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
        }

        private void tsmiPeopleManagement_Click(object sender, EventArgs e)
        {
            Form managePeopleForm = new frmListPeople();
            managePeopleForm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLoggingOut = true;
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            this.Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLoggingOut)
            {
                // If the form is closed without logging out, we exit the application.
                // This is to ensure that the application does not remain running in the background.
                Application.Exit();
            }
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLDLApplications frm = new frmListLDLApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLDLApplications frm = new frmListLDLApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLDLApplication frm = new frmRenewLDLApplication();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementDamagedOrLostLicense frm = new frmReplacementDamagedOrLostLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
    }
}