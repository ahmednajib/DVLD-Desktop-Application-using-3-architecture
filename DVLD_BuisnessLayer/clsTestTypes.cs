using System;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public enTestType ID { set; get; }

        public string TestTypeTitle { get; set; }

        public string TestTypeDescription { get; set; }

        public int TestFees { get; set; }
        
        public clsTestTypes()
        {
            this.ID = enTestType.VisionTest;
            this.TestTypeTitle = string.Empty;
            this.TestTypeDescription = string.Empty;
            this.TestFees = 0;

            Mode = enMode.AddNew;
        }

        private clsTestTypes(enTestType ID, string testTypeDescription, string testTypeTitle, int testFees)
        {
            this.ID = ID;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestFees = testFees;

            Mode = enMode.Update;
        }

        public static DataTable GetTestTypes()
        {
            return clsTestTypesData.GetTestTypes();
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int) this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestFees);
        }

        private bool _AddNewTestType()
        {
            this.ID = (enTestType) clsTestTypesData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestFees);

            return (this.TestTypeTitle != "");
        }

        public static clsTestTypes Find(enTestType TestTypeID)
        {
            string TestTypeTitle = string.Empty, TestTypeDescription = "";
            int TestFees = 0;

            if (clsTestTypesData.GetTestTypeInfoByID( (int) TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
                return new clsTestTypes(TestTypeID, TestTypeDescription, TestTypeTitle, TestFees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }
            return false;
        }
    }
}