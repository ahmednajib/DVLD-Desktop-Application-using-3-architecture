using DVLD_BuisnessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DVLD_Project.Controls
{
    public partial class ctrlDashboardHome : UserControl
    {
        public event EventHandler AddPersonRequested;
        public event EventHandler AddUserRequested;
        public event EventHandler NewLocalLicenseApplicationRequested;
        public event EventHandler NewInternationalLicenseRequested;
        public event EventHandler DetainLicenseRequested;

        public ctrlDashboardHome()
        {
            InitializeComponent();
        }

        public void RefreshDashboard()
        {
            SetCard(lblTotalPeople, () => clsPerson.GetAllPeople().Rows.Count);
            SetCard(lblTotalUsers, () => clsUser.GetAllUsers().Rows.Count);
            SetCard(lblTotalDrivers, () => clsDriver.GetAllDrivers().Rows.Count);
            SetCard(lblLocalApplications, () => clsLDLApplication.GetAllLocalDrivingLicenseApplications().Rows.Count);
            SetCard(lblInternationalLicenses, () => clsInternationalLicense.GetAllInternationalLicenses().Rows.Count);
            SetCard(lblDetainedLicenses, GetActiveDetainedLicenseCount);
            SetCard(lblPendingApplications, GetPendingApplicationCount);
            SetCard(lblScheduledTests, GetOpenTestAppointmentCount);

            lblFooter.Text = "Updated " + DateTime.Now.ToString("dd MMM yyyy, hh:mm tt");
        }

        private void SetCard(Label valueLabel, Func<int?> loadValue)
        {
            try
            {
                int? value = loadValue();
                valueLabel.Text = value.HasValue ? value.Value.ToString("N0") : "N/A";
            }
            catch
            {
                valueLabel.Text = "N/A";
            }
        }

        private int? GetPendingApplicationCount()
        {
            DataTable applications = clsLDLApplication.GetAllLocalDrivingLicenseApplications();
            if (applications.Columns.Contains("Status"))
            {
                return applications.AsEnumerable()
                    .Count(row => string.Equals(Convert.ToString(row["Status"]), "New", StringComparison.OrdinalIgnoreCase));
            }

            if (applications.Columns.Contains("ApplicationStatus"))
            {
                return applications.AsEnumerable()
                    .Count(row => Convert.ToString(row["ApplicationStatus"]) == "1");
            }

            return null;
        }

        private int? GetActiveDetainedLicenseCount()
        {
            DataTable detainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            if (detainedLicenses.Columns.Contains("Is Released"))
            {
                return detainedLicenses.AsEnumerable()
                    .Count(row => !IsTrueValue(row["Is Released"]));
            }

            if (detainedLicenses.Columns.Contains("IsReleased"))
            {
                return detainedLicenses.AsEnumerable()
                    .Count(row => !IsTrueValue(row["IsReleased"]));
            }

            return detainedLicenses.Rows.Count;
        }

        private int? GetOpenTestAppointmentCount()
        {
            DataTable appointments = clsTestAppointment.GetAllTestAppointments();
            if (appointments.Columns.Contains("IsLocked"))
            {
                return appointments.AsEnumerable()
                    .Count(row => !IsTrueValue(row["IsLocked"]));
            }

            if (appointments.Columns.Contains("Is Locked"))
            {
                return appointments.AsEnumerable()
                    .Count(row => !IsTrueValue(row["Is Locked"]));
            }

            return appointments.Rows.Count;
        }

        private bool IsTrueValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            if (value is bool boolValue)
                return boolValue;

            string text = Convert.ToString(value);
            return text == "1" ||
                   string.Equals(text, "true", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(text, "yes", StringComparison.OrdinalIgnoreCase);
        }

        private void ctrlDashboardHome_Load(object sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddPersonRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnNewLocalApplication_Click(object sender, EventArgs e)
        {
            NewLocalLicenseApplicationRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnNewInternationalLicense_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicenseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
        }
    }
}
