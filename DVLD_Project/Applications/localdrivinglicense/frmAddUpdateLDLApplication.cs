using System;
using System.Data;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD_BuisnessLayer;


namespace DVLD_Project.Licenses.LocalDrivingLicense
{
    public partial class frmAddUpdateLDLApplication : Form
    {
        private enum enMode { AddNew = 1, Update = 2 };
        private enMode Mode;

        private int _LDLApplicationID;

        clsLDLApplication _LDLApplication;

        public frmAddUpdateLDLApplication()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateLDLApplication(int ApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID = ApplicationID;
            Mode = enMode.Update;
        }

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.GetLicenseClassesData();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LDLApplication = new clsLDLApplication();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClasses.SelectedIndex = 2; // Default class
                lblApplicationFees.Text = clsApplicationTypes.Find((int) clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _LoadData()
        {
            _LDLApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(_LDLApplicationID);

            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_LDLApplication == null)
            {
                MessageBox.Show("This form will be closed because No Local Driving License Application with ID = " + _LDLApplicationID);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LDLApplication.ApplicantPersonID);
            lblLDLApplicationID.Text = _LDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LDLApplication.ApplicationDate.ToShortDateString();
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(_LDLApplication.LicenseClassInfo.ClassName);
            lblApplicationFees.Text = _LDLApplication.ApplicationTypeInfo.ApplicationFees.ToString();
            lblCreatedByUser.Text = _LDLApplication.CreatedByUserInfo.UserName;
        }

        private void frmAddUpdateLDLApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (Mode == enMode.Update)
            {
                _LoadData();
            }
        } 
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLDLApplication.SelectedTab = tcLDLApplication.TabPages["tpApplicationInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLDLApplication.SelectedTab = tcLDLApplication.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClasses.Find(cbLicenseClasses.Text).ClassID;

            if(clsLDLApplication.IsPersonHasActiveApplication(ctrlPersonCardWithFilter1.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID))
            {
                MessageBox.Show("This person already has an active application for a driving license in this class.", "Active Application Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(clsLDLApplication.IsPersonAlreadyHasLicense(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("This person already has an active License in this class.", "Active License Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _LDLApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LDLApplication.ApplicationDate = DateTime.Now;
            _LDLApplication.ApplicationTypeID = 1;
            _LDLApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LDLApplication.LastStatusDate = DateTime.Now;
            _LDLApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LDLApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LDLApplication.LicenseClassID = LicenseClassID;

            if (_LDLApplication.SaveLDLApplication())
            {
                lblLDLApplicationID.Text = _LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmAddUpdateLDLApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}