using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        private string Password { get; set; } //El campo Password (idealmente) nunca es accesible públicamente.

        public bool IsAdmin { get; set; }

        //public Users(string password) //Constructor público (recibe password) que establece internamente el valor de Password.
        //{
        //    Password = password;
        //}
    }
}
