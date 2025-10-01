using System;
using System.Windows.Forms;

namespace DVLD_Project.Applications.localdrivinglicense
{
    public partial class frmLDLApplicationInfo : Form
    {
        private int _LDLApplicationID = -1;
        public frmLDLApplicationInfo(int lDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = lDLApplicationID;
        }

        private void frmLDLApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
