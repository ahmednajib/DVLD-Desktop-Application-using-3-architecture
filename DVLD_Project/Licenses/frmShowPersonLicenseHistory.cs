using System;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.Local_Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowPersonLicenseHestory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Invalid Person ID provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}