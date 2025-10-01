using DVLD_BuisnessLayer;
using DVLD_Project.Licenses.Local_Licenses;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Drivers
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtAllDrivers; 
        
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblNumberOfRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtAllDrivers.DefaultView.RowFilter = "";
            lblNumberOfRecords.Text = dgvDrivers.Rows.Count.ToString();

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecords.Text = dgvDrivers.Rows.Count.ToString();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 100;

                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 100;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 315;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 140;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 120;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonInformation frmPersonInfo = new frmPersonInformation((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frmPersonInfo.ShowDialog();

            // Refresh the data grid view after showing the person information
            frmListDrivers_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frmLicenseHistory = new frmShowPersonLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frmLicenseHistory.ShowDialog();

            // Refresh the data grid view after showing the license history
            frmListDrivers_Load(null, null);
        }
    }
}