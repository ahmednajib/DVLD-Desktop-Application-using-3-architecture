using System;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeId { get; set; }

        public string ApplicationTypeTitle { get; set; }

        public int ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            ApplicationTypeId = -1;
            ApplicationTypeTitle = string.Empty;
            ApplicationFees = 0;

            Mode = enMode.AddNew;
        }

        private clsApplicationTypes(int applicationTypeId, string applicationTypeTitle, int applicationFees)
        {
            ApplicationTypeId = applicationTypeId;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;

            Mode = enMode.Update;
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeId, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeId = clsApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);

            return (this.ApplicationTypeId != -1);
        }

        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = string.Empty;
            int ApplicationFees = 0;

            if (clsApplicationTypesData.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }
            return false;
        }
    }
}