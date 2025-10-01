using System;
using System.Windows.Forms;
using DVLD_BuisnessLayer;


namespace DVLD_Project.Users
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private int _UserID = -1;
        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }

        public clsUser SelectedUserInfo
        {
            get { return _User; }
        }

        private void _LoadDefaultInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[?????]";
            lblUserName.Text = "[?????]";
            lblIsActive.Text = "[?????]";
        }

        private void _LoadUserInfo()
        {
            _UserID = _User.UserID;
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            ctrlPersonCard1.LoadPersonInformation(_User.PersonID);
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(UserID);

            if (_User == null)
            {
                _LoadDefaultInfo();
                MessageBox.Show($"User with ID={UserID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadUserInfo();
        }
    }
}
