using System;
using System.Data;
using System.Windows.Forms;
using DVLD_BuisnessLayer;

namespace DVLD_Project.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationTypes frmUpdate = new frmUpdateApplicationTypes((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frmUpdate.ShowDialog();
            frmListApplicationTypes_Load(null, null);
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationTypes.GetApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblNumberOfRecords.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 110;
            }
            else
            {
                MessageBox.Show("No Application Types Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}