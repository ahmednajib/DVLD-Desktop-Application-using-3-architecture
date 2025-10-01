using System;
using System.Windows.Forms;
using DVLD_BuisnessLayer;
using System.IO;
using DVLD_Project.Properties;

namespace DVLD_Project.Licenses
{
    public partial class ctrlLicenseInfoCard : UserControl
    {
        private int _LicenseID;
        private clsLicense _License;

        public ctrlLicenseInfoCard()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return _License; } }

        private void _ResetDefaultValue()
        {
            lblClass.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblFullName.Text = "[????]";
            lblGender.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblIsDetained.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblNotes.Text = "[????]";

            pbGender.Image = Resources.Man_32;
            pbPersonImage.Image = Resources.Male_512;
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.FindByLicenseID(_LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                _ResetDefaultValue();
                return;
            }

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo.ToString();

            if (_License.DriverInfo.PersonInfo.Gender == 0) 
            { 
                lblGender.Text = "Male";
                pbGender.Image = Resources.Man_32;
                pbPersonImage.Image = Resources.Male_512;
            }
            else 
            { 
                lblGender.Text = "Female";
                pbGender.Image = Resources.Woman_32;
                pbPersonImage.Image = Resources.Female_512;
            }

            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblNotes.Text = _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = clsDriver.FindByPersonID(_License.DriverInfo.PersonInfo.PersonID).DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {
                if(File.Exists(ImagePath))
                pbPersonImage.ImageLocation = _License.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
