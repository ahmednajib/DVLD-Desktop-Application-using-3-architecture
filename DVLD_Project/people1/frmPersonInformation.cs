using System;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmPersonInformation : Form
    {
        public frmPersonInformation(int PersonID)
        {
            InitializeComponent();
            ctrlPersonalInformation1.LoadPersonInformation(PersonID);
        }

        public frmPersonInformation(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonalInformation1.LoadPersonInformation(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}