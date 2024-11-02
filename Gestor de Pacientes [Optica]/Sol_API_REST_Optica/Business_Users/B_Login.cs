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
        /// <summary>
        /// Instancia de la capa de datos con el método para validar el inicio del sesión.
        /// </summary>
        public D_Login dataLoginTools = new D_Login();

        /// <summary>
        /// Valida las credenciales de inicio de sesión recibidas por la capa de API contra la capa de datos.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Objeto de tipo Users</returns>
        public Users B_LoginValidation(string username, string password)
        {
            return dataLoginTools.D_LoginValidation(username, password);
        }
    }
}
