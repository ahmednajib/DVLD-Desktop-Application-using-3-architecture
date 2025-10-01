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
using DVLD_Project.Licenses.International_Licenses;
using DVLD_Project.Licenses.Local_Licenses;

namespace DVLD_Project.Applications.InternationalLicense
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;
        private int _CurrentLicenseID = -1;
        private clsLicense _SelectedLicenseInfo = null;

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblApplicationID.Text = "[???]";
            lblInternationalLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _CurrentLicenseID = obj;

            if(_CurrentLicenseID == -1)
            {
                btnIssueLicense.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                _ResetDefaultValues();

                return;
            }

            _SelectedLicenseInfo = ctrlLicenseInfoWithFilter1.SelectedLicenseInfo;
            lblLocalLicenseID.Text = _SelectedLicenseInfo.LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = false;

            //check the license class, person could not issue international license without having
            //normal license of class 3.

            if (_SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("Selected License Is not Active anymore.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Detained, please release it.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License Is Expired, please Renew it.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if person already have an active international license
            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(_SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("The selected license is expired. Please renew the license before issuing an International License.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicantPersonID = _SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = _SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = _SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();

            MessageBox.Show("International License Issued Successfully with ID=" + _InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frmShowHistory = new frmShowPersonLicenseHistory(_SelectedLicenseInfo.DriverInfo.PersonID);
            frmShowHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}