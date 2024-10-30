using Data_Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Models;

namespace Business_Users
{
    public class B_Login
    {
        public D_Login dataLoginTools = new D_Login();

        public Users B_LoginValidation(string username, string password)
        {
            return dataLoginTools.D_LoginValidation(username, password);
        }
    }
}
