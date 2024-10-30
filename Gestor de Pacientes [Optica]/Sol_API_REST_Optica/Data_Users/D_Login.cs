using Patient_Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Models;

namespace Data_Users
{
    public class D_Login //Esta clase contiene todos los métodos relacionados con el Log In de usuarios.
    {
        /// <summary>
        /// Cadena de conexión a SQL para realizar el LogIn de usuarios y administrador.
        /// </summary>
        private string login_connection = ConfigurationManager.ConnectionStrings["SQL_Optica"].ConnectionString;

        /// <summary>
        /// Busca alguna coincidencia en la tabla Users comparando con Username y Password:
        /// Siempre retorna un objeto Users ya sea vacío o con datos (el cliente maneja las acciones dependiendo del resultado).
        /// </summary>
        /// <param name="username">string con el username</param>
        /// <param name="password">string con la contraseña</param>
        /// <returns></returns>
        public Users D_LoginValidation(string username, string password) 
        {
            //SqlConnection sqlLoginConnection = new SqlConnection(login_connection);
            Users user = new Users();

            using (SqlConnection sqlLoginConnection = new SqlConnection(login_connection))
            {
                try
                {
                    sqlLoginConnection.Open();

                    SqlCommand logInCommand = new SqlCommand("Get_Existing_User", sqlLoginConnection);
                    logInCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    logInCommand.Parameters.AddWithValue("@username", username);
                    logInCommand.Parameters.AddWithValue("@password", password);

                    SqlDataReader usersReader = logInCommand.ExecuteReader();
                    if (usersReader.Read())
                    {
                        user.UserId = Convert.ToInt32(usersReader["UserId"]);
                        user.FullName = Convert.ToString(usersReader["FullName"]);
                        user.Username = Convert.ToString(usersReader["Username"]);
                        user.IsAdmin = Convert.ToBoolean(usersReader["IsAdmin"]);

                        return user;
                    }

                    else return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } //using aplica automáticamente un .Dispose() al objeto en uso (SqlConnection) en este punto.
        }
    }
}
