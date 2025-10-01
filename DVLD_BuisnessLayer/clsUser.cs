﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BuisnessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }

        public int PersonID { get; set; }
        public clsPerson PersonInfo;

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            this.PersonInfo = clsPerson.Find(PersonID);
            UserName = userName;
            Password = password;
            IsActive = isActive;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.UserID = clsUserData.AddNewPerson(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static clsUser FindByUserID(int UserID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;

            return clsUserData.GetUserInfoByUserID(UserID, ref personID, ref userName, ref password, ref isActive)
                ? new clsUser(UserID, personID, userName, password, isActive)
                : null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int userID = -1;
            string userName = "", password = "";
            bool isActive = false;

            return clsUserData.GetUserInfoByPersonID(PersonID, ref userID, ref userName, ref password, ref isActive)
                ? new clsUser(userID, PersonID, userName, password, isActive)
                : null;
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref userID, ref personID, ref isActive))
            {
                return new clsUser(userID, personID, UserName, Password, isActive);
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
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update; // Change mode to Update after successful addition
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistByPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword);
        }
    }
}