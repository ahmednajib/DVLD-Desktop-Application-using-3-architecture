using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DVLD_BuisnessLayer;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID {  get; set; }

        public int PersonID { get; set; }

        public int CreatedByUserID {  get; set; }

        public DateTime CreatedDate { get; set; }

        public clsPerson PersonInfo;

        public clsDriver() 
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            this.Mode = enMode.AddNew;
        }

        private clsDriver (int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
            this.PersonInfo = clsPerson.Find(this.PersonID);

            this.Mode = enMode.Update;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDriversData();
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);

            return this.DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            
            if (clsDriverData.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null; // Return null if no driver is found
            }
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindDriverByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null; // Return null if no driver is found
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update; // Change mode to Update after successful addition
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
    }
}
