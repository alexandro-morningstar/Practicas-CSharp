using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        private string Password { get; set; } //El campo Password (idealmente) nunca es accesible públicamente.

        public bool IsAdmin { get; set; }

        //public Users(string password) //Constructor público (recibe password) que establece internamente el valor de Password.
        //{
        //    Password = password;
        //}
    }
}