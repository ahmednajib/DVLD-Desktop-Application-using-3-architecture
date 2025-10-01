using DVLD_BuisnessLayer;
using DVLD_Project.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;
        private int _LDLApplicationID = -1;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;

        public frmListTestAppointments(int LDLApplicationID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;
            _TestType = TestType;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    this.Text = "Vision Test";
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;
                case clsTestTypes.enTestType.WrittenTest:
                    this.Text = "Written Test";
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;
                case clsTestTypes.enTestType.StreetTest:
                    this.Text = "Street Test";
                    lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    break;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LDLApplicationID, _TestType);

            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                cmsTestAppointments.Enabled = true;

                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
            else
            {
                cmsTestAppointments.Enabled = false;
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLDLApplication localDrivingLicenseApplication = clsLDLApplication.FindByLocalDrivingAppLicationID(_LDLApplicationID);
            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(localDrivingLicenseApplication.DoesPassTestType(_TestType))
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm1 = new frmScheduleTest(_LDLApplicationID, _TestType);
            frm1.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
    }
}