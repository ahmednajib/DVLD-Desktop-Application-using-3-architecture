using System;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        private int _InternationalLicenseID = -1;

        public frmShowInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            if(_InternationalLicenseID != -1)
            {
                ctrlInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
            }
        }
    }
}