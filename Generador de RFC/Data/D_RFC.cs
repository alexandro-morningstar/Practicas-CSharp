using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class D_RFC
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sql_RFCDB"].ConnectionString;

        /*------------------------------ NUEVOS REGISTROS ------------------------------*/
        public void CreateUser(E_RFC user, string rfc) //Recibe los datos del usuario y la cadena correspondiente al RFC (calculado en la capa de Negocio)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("add_user", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", user.Name.ToUpper());
                command.Parameters.AddWithValue("@paternalsurname", user.PaternalSurname.ToUpper());
                if (user.MaternalSurname != null)
                {
                    command.Parameters.AddWithValue("@maternalsurname", user.MaternalSurname.ToUpper());
                }
                else
                {
                    command.Parameters.AddWithValue("@maternalsurname", ' '); // Si el apellido materno es nulo, introducimos en blanco
                }
                command.Parameters.AddWithValue("@birthdate", user.BirthDate);
                command.Parameters.AddWithValue("@rfc", rfc.ToUpper());

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // --------- MARCAR LOS REGISTROS IGUALES --------- PARTE DE NUEVOS REGISTROS ---------
        public bool GetRepeatedUser(E_RFC user, string rfc)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            bool isRepeated = false;

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("get_repeated", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("paternalsurname", user.PaternalSurname);
                if (user.MaternalSurname != null)
                {

                    command.Parameters.AddWithValue("@maternalsurname", user.MaternalSurname);
                }
                else
                {
                    command.Parameters.AddWithValue("@maternalsurname", ' ');
                }
                command.Parameters.AddWithValue("@birthdate", user.BirthDate);
                command.Parameters.AddWithValue("@rfc", rfc);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isRepeated = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return isRepeated;

        }

        /*------------------------------ RETORNO DE ULTIMO REGISTRO ------------------------------*/
        public string GetLastRFC()
        {
            string rfc;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("get_last_rfc", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    rfc = reader["rfc"].ToString();
                }
                else
                {
                    throw new Exception("Algo ha salido mal y no se encontraron registros");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return rfc;
        }

        /*------------------------------ RETORNO DE TODOS LOS REGISTROS ------------------------------*/

        public List<E_RFC> GetAllUsers()
        {
            List<E_RFC> users = new List<E_RFC>();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("get_all_users", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    E_RFC user = new E_RFC();

                    user.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    user.Name = Convert.ToString(reader["name"]);
                    user.PaternalSurname = Convert.ToString(reader["paternalSurname"]);
                    user.MaternalSurname = Convert.ToString(reader["maternalSurname"]);
                    user.BirthDate = Convert.ToDateTime(reader["birthDate"]);
                    user.RFC = Convert.ToString(reader["rfc"]);

                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return users;
        }

        public int CountUsers()
        {
            int count = 0;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("get_all_users", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count++;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        /*------------------------------ RETORNO DE UN SOLO REGISTRO POR ID ------------------------------*/

        public E_RFC GetByID(int id)
        {
            E_RFC user = new E_RFC();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("get_by_id", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    user.Name = Convert.ToString(reader["name"]);
                    user.PaternalSurname = Convert.ToString(reader["paternalSurname"]);
                    user.MaternalSurname = Convert.ToString(reader["maternalSurname"]);
                    user.BirthDate = Convert.ToDateTime(reader["birthDate"]);
                    user.RFC = Convert.ToString(reader["rfc"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        /*------------------------------ ELIMINAR UN REGISTRO POR ID ------------------------------*/
        public void DeleteById(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("delete_by_id", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /*------------------------------ EDITAR UN REGISTRO POR ID ------------------------------*/
        public void Edit(E_RFC user, string rfc)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("edit_user", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", user.IdRFC);
                command.Parameters.AddWithValue("@name", user.Name.ToUpper());
                command.Parameters.AddWithValue("@paternalsurname", user.PaternalSurname.ToUpper());
                if (user.MaternalSurname != null)
                {
                    command.Parameters.AddWithValue("@maternalsurname", user.MaternalSurname.ToUpper());
                }
                else
                {
                    command.Parameters.AddWithValue("@maternalsurname", ' '); // Si el apellido materno es nulo, introducimos en blanco
                }
                command.Parameters.AddWithValue("@birthdate", user.BirthDate);
                command.Parameters.AddWithValue("@rfc", rfc.ToUpper());

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /*------------------------------ BUSCAR REGISTROS POR COINCIDENCIA DE TEXTO ------------------------------*/
        public List<E_RFC> Search(string text)
        {
            List<E_RFC> users = new List<E_RFC>();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("search", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@text", text);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    E_RFC user = new E_RFC();

                    user.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    user.Name = Convert.ToString(reader["name"]);
                    user.PaternalSurname = Convert.ToString(reader["paternalSurname"]);
                    user.MaternalSurname = Convert.ToString(reader["maternalSurname"]);
                    user.BirthDate = Convert.ToDateTime(reader["birthDate"]);
                    user.RFC = Convert.ToString(reader["rfc"]);

                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return users;
        }
    }
}
