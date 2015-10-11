using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{


        public class User
        {

            private int mUser_ID;
            private string mUsername;
            private string mPassword;
            private string mLogin;

            public int User_ID
            {
                get { return mUser_ID; }
                set { mUser_ID = value; }
            }
            public string Username
            {
                get { return mUsername; }
                set { mUsername = value; }
            }
            public string Password
            {
                get { return mPassword; }
                set { mPassword = value; }
            }
            public string Login
            {
                get { return mLogin; }
                set { mLogin = value; }
            }

            public User() { }
            public User(int oID, string n, string p, string e)
            {
                User_ID = oID;
                Username = n;
                Password = p;
                Login = e;
            }
        }

    }

