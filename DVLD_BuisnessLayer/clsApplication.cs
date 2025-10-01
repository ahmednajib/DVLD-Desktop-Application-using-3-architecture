using System;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsApplication
    {
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsApplication(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID,
            enApplicationStatus applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicantPersonInfo = clsPerson.Find(applicantPersonID);
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo = clsApplicationTypes.Find(applicationTypeID);
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);

            Mode = enMode.Update;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            DateTime applicationDate = DateTime.Now, lastStatusDate = DateTime.Now;
            int applicantPersonID = -1, applicationTypeID = -1, createdByUserID = -1, status = 1;
            float paidFees = 0;

            if (clsApplicationData.GetBasedApplicationInfoByApplicationID(ApplicationID, ref applicantPersonID, ref applicationDate, ref applicationTypeID,
                    ref status, ref lastStatusDate, ref paidFees, ref createdByUserID))
            {
                return new clsApplication(ApplicationID, applicantPersonID, applicationDate, applicationTypeID, (enApplicationStatus)status, lastStatusDate, paidFees, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                                    (Byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return this.ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                                    (Byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool DeleteApplication()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }
            return false;
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public static bool IsApplicationExists(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }
    }
}