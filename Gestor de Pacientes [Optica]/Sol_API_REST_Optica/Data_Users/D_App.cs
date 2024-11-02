using Patient_Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Runtime.InteropServices;

namespace Data_Users
{
    public class D_App //Esta clase contiene todos los métodos solicitados dentro de la app.
    {
        /// <summary>
        /// Cadena de conexión a SQL para realizar el LogIn de usuarios y administrador.
        /// </summary>
        private string app_connection = ConfigurationManager.ConnectionStrings["SQL_Optica"].ConnectionString;

        /// <summary>
        /// Obtiene todos los registros de la tabla Patients (Todos los pacientes).
        /// </summary>
        /// <returns>Lista de tipo Patients</returns>
        public List<Patients> getAllPatients()
        {
            List<Patients> allPatients = new List<Patients>();

            try
            {
                using (SqlConnection sqlDashboardConnection = new SqlConnection(app_connection))
                {
                    sqlDashboardConnection.Open();
                    SqlCommand getPatientsCommand = new SqlCommand("Get_All_Patients", sqlDashboardConnection);
                    getPatientsCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader patientReader = getPatientsCommand.ExecuteReader();
                    while (patientReader.Read())
                    {
                        Patients patient = new Patients();
                        Genders patientGender = new Genders();

                        patient.PatientId = Convert.ToInt32(patientReader["PatientId"]);
                        patient.FirstName = Convert.ToString(patientReader["FirstName"]);
                        patient.MiddleName = Convert.ToString(patientReader["MiddleName"]);
                        patient.LastName = Convert.ToString(patientReader["LastName"]);
                        patient.Age = Convert.ToInt32(patientReader["Age"]);
                        patient.IdGender = Convert.ToInt32(patientReader["IdGender"]);
                        patient.AnotherDiseases = Convert.ToString(patientReader["AnotherDiseases"]);
                        patient.ContactNumber = Convert.ToString(patientReader["ContactNumber"]);
                        patient.Due = Convert.ToDecimal(patientReader["Due"]);
                        patient.RegistryDate = Convert.ToDateTime(patientReader["RegistryDate"]);

                        patientGender.GenderId = Convert.ToInt32(patientReader["GenderId"]); //Llenamos el objeto Gender
                        patientGender.Gender = Convert.ToString(patientReader["Gender"]);

                        patient.Gender = patientGender; //Inner Join (le asignamos el valor del objeto gender, al que se hace referencia en la definicion del objeto)

                        allPatients.Add(patient);
                    }

                    return allPatients;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Comprueba que existan registros de historia clínica (en ambos ojos) relacionados al usuario con ID especificado.
        /// True == Posee registros asociados.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>bool</returns>
        public bool HasMR(int patientId)
        {
            try
            {
                using (SqlConnection sqlMRConnection = new SqlConnection(app_connection))
                {
                    sqlMRConnection.Open();
                    SqlCommand getIdsWithMRConnection = new SqlCommand("Get_Ids_WithMR", sqlMRConnection);
                    getIdsWithMRConnection.CommandType = CommandType.StoredProcedure;

                    getIdsWithMRConnection.Parameters.AddWithValue("@id", patientId);

                    SqlDataReader idsReader = getIdsWithMRConnection.ExecuteReader();
                    if (idsReader.Read())
                    { //Si hay algo que leer (si existe coincidencia de registros de historia clinica en ambos ojos) es true
                        return true;
                    }
                    
                    else { return false; }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agregar (registra) un nuevo paciente en la base de datos.
        /// </summary>
        /// <param name="newPatient"></param>
        public void AddPatient(Patients newPatient)
        {
            try
            {
                using (SqlConnection sqlAddConnection = new SqlConnection(app_connection))
                {
                    sqlAddConnection.Open();

                    SqlCommand addPatientCommand = new SqlCommand("Create_Patient", sqlAddConnection);
                    addPatientCommand.CommandType = CommandType.StoredProcedure;

                    addPatientCommand.Parameters.AddWithValue("@firstname", newPatient.FirstName);
                    addPatientCommand.Parameters.AddWithValue("@middlename", newPatient.MiddleName);
                    addPatientCommand.Parameters.AddWithValue("@lastname", newPatient.LastName);
                    addPatientCommand.Parameters.AddWithValue("@age", newPatient.Age);
                    addPatientCommand.Parameters.AddWithValue("@idgender", newPatient.IdGender);
                    addPatientCommand.Parameters.AddWithValue("@anotherdiseases", newPatient.AnotherDiseases);
                    addPatientCommand.Parameters.AddWithValue("@contactnumber", newPatient.ContactNumber);
                    addPatientCommand.Parameters.AddWithValue("@due", newPatient.Due);
                    addPatientCommand.Parameters.AddWithValue("@registrydate", DateTime.Now);

                    addPatientCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agrega (registra) un nuevo paciente en la base de datos.
        /// Agrega (registra) todos los padecimientos asociados al paciente que se está dando de alta.
        /// </summary>
        /// <param name="newPatient">Patients: Objeto Patients</param>
        /// <param name="diseaseIds">int[]: Lista de IDs (int) para registrar padecimientos en dbo.PatientDiseases</param>
        public void AddPatientWithDiseases(Patients newPatient, int[] diseaseIds)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlAddConnection = new SqlConnection(app_connection))
                    {
                        sqlAddConnection.Open(); //Abrimos la conexión en general para todas las operaciones.

                        int currentID; //Id del paciente actual.

                        //Primer operación: Agregar al paciente.
                        using (SqlCommand addPatientCommand = new SqlCommand("Create_Patient_With_Diseases", sqlAddConnection))
                        {
                            addPatientCommand.CommandType = CommandType.StoredProcedure;

                            //Especificamos los valores de los parámetros
                            addPatientCommand.Parameters.AddWithValue("@firstname", newPatient.FirstName);
                            addPatientCommand.Parameters.AddWithValue("@middlename", newPatient.MiddleName);
                            addPatientCommand.Parameters.AddWithValue("@lastname", newPatient.LastName);
                            addPatientCommand.Parameters.AddWithValue("@age", newPatient.Age);
                            addPatientCommand.Parameters.AddWithValue("@idgender", newPatient.IdGender);
                            addPatientCommand.Parameters.AddWithValue("@anotherdiseases", newPatient.AnotherDiseases);
                            addPatientCommand.Parameters.AddWithValue("@contactnumber", newPatient.ContactNumber);
                            addPatientCommand.Parameters.AddWithValue("@due", newPatient.Due);
                            addPatientCommand.Parameters.AddWithValue("@registrydate", DateTime.Now);

                            //Agregamos un parámetro de salida para capturar el ID generado para el paciente actual.
                            SqlParameter outputIdParameter = new SqlParameter("@sqlCurrentId", SqlDbType.Int) //Especificación del parametro ("@nombre", Tipo.Int)
                            {
                                Direction = ParameterDirection.Output //Especificamos que es de salida.
                            };
                            addPatientCommand.Parameters.Add(outputIdParameter); //Agregamos el parámetro.

                            //Ejecución del storedprocedure
                            addPatientCommand.ExecuteNonQuery();

                            //Obtenemos (del parametro de salida) el ID generado.
                            currentID = (int)outputIdParameter.Value;
                        }

                        //Segunda operación: Agrega los padecimientos asociados al paciente.
                        using (SqlCommand addPatientDiseaseCommand = new SqlCommand("Create_Patient_Disease", sqlAddConnection))
                        {
                            addPatientDiseaseCommand.CommandType = CommandType.StoredProcedure;

                            foreach (int idDisease in diseaseIds) //Puede haber más de un padecimiento en la lista, generamos un registro por cada uno.
                            {

                                addPatientDiseaseCommand.Parameters.AddWithValue("@idpatient", currentID);
                                addPatientDiseaseCommand.Parameters.AddWithValue("@iddisease", idDisease);

                                addPatientDiseaseCommand.ExecuteNonQuery();
                            }
                        }

                        //Completamos la transacción si todo ocurre según lo previsto.
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Retorna una lista con todas los objetos/registros Disease encontrados en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos de tipo Diseases</returns>
        public List<Diseases> GetDiseases()
        {
            List<Diseases> allDiseases = new List<Diseases>();
            try
            {
                using (SqlConnection sqlGetDiseasesConnection = new SqlConnection(app_connection))
                {
                    sqlGetDiseasesConnection.Open();

                    SqlCommand getDiseasesCommand = new SqlCommand("Get_All_Diseases", sqlGetDiseasesConnection);
                    getDiseasesCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader diseasesReader = getDiseasesCommand.ExecuteReader();
                    while(diseasesReader.Read())
                    {
                        Diseases disease = new Diseases();

                        disease.DiseaseId = Convert.ToInt32(diseasesReader["DiseaseId"]);
                        disease.Disease = Convert.ToString(diseasesReader["Disease"]);

                        allDiseases.Add(disease);
                    }
                    return allDiseases;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
