using DVLD.Classes;
using DVLD_BuisnessLayer;
using DVLD_Project.Tests.Controls;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestTypes.enTestType _TestType;

        private clsTest _Test;
        public frmTakeTest(int AppointmentID, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();

            _AppointmentID = AppointmentID;
            _TestType = TestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load_1(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestType;

            ctrlScheduledTest1.LoadInfo(_AppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int _TestID = ctrlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                _Test = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
              )
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}