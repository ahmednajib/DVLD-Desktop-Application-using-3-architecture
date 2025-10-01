using DVLD_BuisnessLayer;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.Local_Licenses;
using DVLD_Project.Licenses.LocalDrivingLicense;
using DVLD_Project.Tests;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Applications.localdrivinglicense
{
    public partial class frmListLDLApplications : Form
    {
        private static DataTable _dtAllApplications;
        public frmListLDLApplications()
        {
            InitializeComponent();
        }

        private void frmListLDLApplications_Load(object sender, EventArgs e)
        {
            _dtAllApplications = clsLDLApplication.GetAllApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllApplications;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 200;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 110;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 240;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 140;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Test";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 80;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 120;

            }
            else
            {
                MessageBox.Show("No applications found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtAllApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with numbers not string.
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplicationInfo frm =
                        new frmLDLApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            //refresh
            frmListLDLApplications_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            //refresh
            frmListLDLApplications_Load(null, null);
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication();
            frm.ShowDialog();

            //refresh
            frmListLDLApplications_Load(null, null);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLDLApplication LocalDrivingLicenseApplication =
                clsLDLApplication.FindByLocalDrivingAppLicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLDLApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLDLApplication LocalDrivingLicenseApplication =
                clsLDLApplication.FindByLocalDrivingAppLicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLDLApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication LocalDrivingLicenseApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(LDLApplicationID);

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application found with ID = " + LDLApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool IsLicenseExist = LocalDrivingLicenseApplication.IsLicenseIssued();

            editToolStripMenuItem.Enabled = (!IsLicenseExist && (LocalDrivingLicenseApplication.ApplicationStatus == clsLDLApplication.enApplicationStatus.New));
            DeleteApplicationToolStripMenuItem.Enabled = (!IsLicenseExist && (LocalDrivingLicenseApplication.ApplicationStatus == clsLDLApplication.enApplicationStatus.New));
            CancelApplicaitonToolStripMenuItem.Enabled = (!IsLicenseExist && (LocalDrivingLicenseApplication.ApplicationStatus == clsLDLApplication.enApplicationStatus.New));
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (!IsLicenseExist && (TotalPassedTests == 3));
            showLicenseToolStripMenuItem.Enabled = IsLicenseExist && !(LocalDrivingLicenseApplication.ApplicationStatus == clsLDLApplication.enApplicationStatus.Cancelled);


            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = ((!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsLDLApplication.enApplicationStatus.New));

            if (ScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(
                LDLApplicationID, clsTestTypes.enTestType.VisionTest);
            frm.ShowDialog();
            //refresh the form again.
            frmListLDLApplications_Load(null, null);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if (!clsLDLApplication.DoesPassTestType(LDLApplicationID, clsTestTypes.enTestType.VisionTest))
            {
                MessageBox.Show("You must pass the Vision Test before scheduling the Written Test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmListTestAppointments frm = new frmListTestAppointments(
                LDLApplicationID, clsTestTypes.enTestType.WrittenTest);
            frm.ShowDialog();
            //refresh the form again.
            frmListLDLApplications_Load(null, null);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if(!clsLDLApplication.DoesPassTestType(LDLApplicationID, clsTestTypes.enTestType.VisionTest) 
                ||
                !clsLDLApplication.DoesPassTestType(LDLApplicationID, clsTestTypes.enTestType.WrittenTest))
            {
                MessageBox.Show("You must pass the Vision Test and Written Test before scheduling the Street Test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmListTestAppointments frm = new frmListTestAppointments(
                LDLApplicationID, clsTestTypes.enTestType.StreetTest);
            frm.ShowDialog();
            //refresh the form again.
            frmListLDLApplications_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueLicenseFirstTime frm = new frmIssueLicenseFirstTime(LDLApplicationID);
            frm.ShowDialog();
            //refresh the form again.
            frmListLDLApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLDLApplication.FindByLocalDrivingAppLicationID(LDLApplicationID).GetActiveLicenseID();

            if( LicenseID != -1) 
            {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication localDrivingLicenseApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}