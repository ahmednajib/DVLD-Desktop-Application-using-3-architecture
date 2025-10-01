using DVLD_BuisnessLayer;
using DVLD_Project.Licenses.International_Licenses;
using DVLD_Project.Licenses.Local_Licenses;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Applications.InternationalLicense
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dtInternationalLicenseApplications;
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication newApplicationForm = new frmNewInternationalLicenseApplication();
            newApplicationForm.ShowDialog();
            // Refresh the list after adding a new application
            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 125;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 125;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 120;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 125;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 130;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }

            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            // Show person information form
            frmPersonInformation frm = new frmPersonInformation(PersonID);
            
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show international license info form
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            // Show person licenses history form
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);

            frm.ShowDialog();
        }
    }
}