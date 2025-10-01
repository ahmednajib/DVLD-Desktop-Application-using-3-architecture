using System;
using System.Data;
using DVLD_DataAccessLayer;


namespace DVLD_BuisnessLayer
{
    public class clsLDLApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }

        public clsLDLApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;

            Mode = enMode.AddNew;
        }

        private clsLDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)
            :
            base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus,
                 LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);

            Mode = enMode.Update;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLDLApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLDLApplicationData.AddNewLocalDrivingLicenseApplication
                (this.ApplicationID, this.LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLDLApplicationData.UpdateLocalDrivingLicenseApplication
                (this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public static clsLDLApplication FindByLocalDrivingAppLicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLDLApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLDLApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;
        }

        public static clsLDLApplication FindByApplicationID(int ApplicationID)
        {
            // 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLDLApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLDLApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;
        }

        public bool SaveLDLApplication()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
            {
                return false;
            }

            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLDLApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.DeleteApplication();
            return IsBaseApplicationDeleted;
        }

        public static bool IsPersonHasActiveApplication(int PersonID, enApplicationType ApplicationType, int ClassID)
        {
            //This method will return true if the person has a local driving license application
            return clsLDLApplicationData.IsPersonHasActiveApplication(PersonID, (int)ApplicationType, ClassID);

        }

        public static bool IsPersonAlreadyHasLicense(int PersonID, int ClassID)
        {
            //This method will return true if the person already has a local driving license in this class
            return clsLDLApplicationData.IsPersonAlreadyHasLicense(PersonID, ClassID);
        }

        public bool IsLicenseIssued()
        {
            //This method will return true if the license is issued for this application
            return GetActiveLicenseID() != -1;
        }

        public int GetActiveLicenseID()
        {
            //This method will return the active license ID for the person in this class
            return clsLDLApplicationData.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public bool DoesPassTestType(clsTestTypes.enTestType TestType)
        {
            return clsLDLApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int) TestType);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestType)
        {
            return clsLDLApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
       
        public bool DoesAttendTestBefore(clsTestTypes.enTestType TestType)
        {
            //This method will return true if the person has attended the test before
            return clsLDLApplicationData.DoesAttendTestBefore(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public int TrialsperTestType(clsTestTypes.enTestType TestType)
        {
            return clsLDLApplicationData.TrialsperTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {

            return clsLDLApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLDLApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now driver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }
            else
                return -1;
        }

    }
}