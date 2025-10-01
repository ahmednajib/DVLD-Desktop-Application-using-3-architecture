using System;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsLicenseClasses
    {
        public enum enMode {AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDiscription { get; set; }
        public int MinimumAllowedAge  { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.ClassID = -1;
            this.ClassName = string.Empty;
            this.ClassDiscription = string.Empty;
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            Mode = enMode.AddNew;
        }

        public clsLicenseClasses(int classID, string className, string classDiscription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            this.ClassID = classID;
            this.ClassName = className;
            this.ClassDiscription = classDiscription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;

            Mode = enMode.Update;
        }

        public static DataTable GetLicenseClassesData()
        {
            return clsLicenseClassesData.GetLicenseClassesData();
        }

        private bool _AddNewLicenseClass()
        {
            this.ClassID = clsLicenseClassesData.AddNewLicenseClass(this.ClassName, this.ClassDiscription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
           
            return (this.ClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassesData.UpdateLicenseClass(this.ClassID, this.ClassName, this.ClassDiscription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public static clsLicenseClasses Find(int ClassID)
        {
            int minimumAllowedAge = 0, DefaultValidityLength = 0;
            float classFees = 0;
            string className = "", classDescription = "";

            if (clsLicenseClassesData.GetLicenseClassInfoByID(ClassID, ref className, ref classDescription, ref minimumAllowedAge, ref DefaultValidityLength, ref classFees))
            {
                return new clsLicenseClasses(ClassID, className, classDescription, minimumAllowedAge, DefaultValidityLength, classFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClasses Find(string ClassName)
        {
            int minimumAllowedAge = 0, DefaultValidityLength = 0, classID = -1;
            float classFees = 0;
            string classDescription = "";

            if (clsLicenseClassesData.GetLicenseClassInfoByName(ClassName, ref classID, ref classDescription, ref minimumAllowedAge, ref DefaultValidityLength, ref classFees))
            {
                return new clsLicenseClasses(classID, ClassName, classDescription, minimumAllowedAge, DefaultValidityLength, classFees);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }
            return false;
        }
    }
}
