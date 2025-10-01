using DVLD_BuisnessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frmUpdate = new frmUpdateTestType((clsTestTypes.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frmUpdate.ShowDialog();
            frmListTestTypes_Load(null, null);
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestTypes.GetTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblNumberOfRecords.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 70;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 150;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 500;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
            else
            {
                MessageBox.Show("No Test Types Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}