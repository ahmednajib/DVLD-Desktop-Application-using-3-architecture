using DVLD_BuisnessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsTestTypes _TestType;
        public frmUpdateTestType(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestFees = Convert.ToInt32(txtFees.Text.Trim());

            if(_TestType.Save())
            {
                MessageBox.Show("Test Type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update Test Type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblID.Text = ((int)_TestTypeID).ToString();
                txtTitle.Text = _TestType.TestTypeTitle;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtFees.Text = _TestType.TestFees.ToString();
            }
            else
            {
                MessageBox.Show("Test Type not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(((TextBox)sender), "This Field cannot be empty");
                return;
            }
            else
            {
                errorProvider1.SetError(((TextBox)sender), null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
                // Allow backspace and delete keys
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}