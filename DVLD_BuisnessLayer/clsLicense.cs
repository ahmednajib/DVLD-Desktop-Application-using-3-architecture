using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_BuisnessLayer;
using DVLD_DataAccessLayer;
using static DVLD_BuisnessLayer.clsLicense;

namespace DVLD_BuisnessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsDriver DriverInfo { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }
        public clsDetainedLicense DetainedInfo { set; get; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        }

        public clsLicense()
        {
            // Initialize properties with default values
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = string.Empty;
            this.PaidFees = 0.0f;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            // Set the mode to AddNew since this constructor is used for adding records
            this.Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, 
                            string notes, float paidFees, bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassInfo = clsLicenseClasses.Find(this.LicenseClass);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            // Set the mode to Update since this constructor is used for existing records
            this.Mode = enMode.Update;
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicensesData();
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (int) this.IssueReason, this.CreatedByUserID);
            
            return this.LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (int) this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense FindByLicenseID(int licenseID)
        {
            int applicationID = -1, driverID = -1, licenseClass = -1, createdByUserID = -1, issueReason = 1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now.AddYears(10);
            string notes = string.Empty;
            float paidFees = 0.0f;
            bool isActive = true;

            // Find the license by LicenseID and populate the properties
            if (clsLicenseData.FindLicenseByLicenseID(licenseID, ref applicationID, ref driverID, ref licenseClass, ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID, licenseClass, issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason) issueReason, createdByUserID);
            }
            else
            {
                return null; // Return null if no license is found
            }
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetLicensesByDriverID(DriverID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update; // Change mode to Update after successful addition
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            if((!this.IsActive) || (!this.IsLicenseExpired()))
            {
                return null;
            }

            clsApplication RenewApplication = new clsApplication();

            RenewApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            RenewApplication.ApplicationDate = DateTime.Now;
            RenewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            RenewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            RenewApplication.LastStatusDate = DateTime.Now;
            RenewApplication.PaidFees = clsApplicationTypes.Find((int) clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            RenewApplication.CreatedByUserID = CreatedByUserID;

            if(!RenewApplication.Save())
            {
                return null;
            }

            clsLicense NewLicens = new clsLicense();

            NewLicens.ApplicationID = RenewApplication.ApplicationID;
            NewLicens.DriverID = this.DriverID;
            NewLicens.LicenseClass = this.LicenseClass;
            NewLicens.IssueDate = DateTime.Now;
            NewLicens.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicens.Notes = Notes;
            NewLicens.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicens.IsActive = true;
            NewLicens.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicens.CreatedByUserID = CreatedByUserID;

            if(!NewLicens.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicens;
        }

        public clsLicense ReplaceLicenseForDamgedOrLost(clsApplication.enApplicationType applicationType, int CreatedByUserID)
        {
            if ((!this.IsActive) || (this.IsLicenseExpired()))
            {
                return null;
            }

            clsApplication RenewApplication = new clsApplication();

            RenewApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            RenewApplication.ApplicationDate = DateTime.Now;
            RenewApplication.ApplicationTypeID = (int)applicationType;
            RenewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            RenewApplication.LastStatusDate = DateTime.Now;
            RenewApplication.PaidFees = clsApplicationTypes.Find((int)applicationType).ApplicationFees;
            RenewApplication.CreatedByUserID = CreatedByUserID;

            if (!RenewApplication.Save())
            {
                return null;
            }

            clsLicense NewLicens = new clsLicense();

            NewLicens.ApplicationID = RenewApplication.ApplicationID;
            NewLicens.DriverID = this.DriverID;
            NewLicens.LicenseClass = this.LicenseClass;
            NewLicens.IssueDate = DateTime.Now;
            NewLicens.ExpirationDate = this.ExpirationDate;
            NewLicens.Notes = Notes;
            NewLicens.PaidFees = 0;
            NewLicens.IsActive = true;
            NewLicens.IssueReason = applicationType == clsApplication.enApplicationType.ReplaceLostDrivingLicense
                                                        ? clsLicense.enIssueReason.LostReplacement
                                                        :clsLicense.enIssueReason.DamagedReplacement;
            NewLicens.CreatedByUserID = CreatedByUserID;

            if (!NewLicens.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicens;
        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {
                return -1;
            }
            return detainedLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            if(!this.IsDetained)
            {
                ApplicationID = -1;
                return false;
            }

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);
        }
    }
}