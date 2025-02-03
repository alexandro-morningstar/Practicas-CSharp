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

        public Patients getPatientById(int patientID)
        {
            Patients patient = new Patients();
            Genders patientGender = new Genders();
            try
            {
                using (SqlConnection getPatientConnection = new SqlConnection(app_connection))
                {
                    getPatientConnection.Open();

                    using (SqlCommand getPatientCommand = new SqlCommand("Get_Patient_Details", getPatientConnection))
                    {
                        getPatientCommand.CommandType = CommandType.StoredProcedure;
                        getPatientCommand.Parameters.AddWithValue("@patientid", patientID);

                        using (SqlDataReader patientReader = getPatientCommand.ExecuteReader())
                        {
                            if (patientReader.Read())
                            {
                                patient.PatientId = Convert.ToInt32(patientReader["PatientId"]);
                                patient.FirstName = Convert.ToString(patientReader["FirstName"]);
                                patient.MiddleName = Convert.ToString(patientReader["Middlename"]);
                                patient.LastName = Convert.ToString(patientReader["LastName"]);
                                patient.Age = Convert.ToInt32(patientReader["Age"]);
                                patient.AnotherDiseases = Convert.ToString(patientReader["AnotherDiseases"]);
                                patient.ContactNumber = Convert.ToString(patientReader["ContactNumber"]);
                                patient.Due = Convert.ToDecimal(patientReader["Due"]);
                                patient.RegistryDate = Convert.ToDateTime(patientReader["RegistryDate"]);
                                patient.IdGender = Convert.ToInt32(patientReader["IdGender"]);

                                patientGender.GenderId = Convert.ToInt32(patientReader["IdGender"]);
                                patientGender.Gender = Convert.ToString(patientReader["Gender"]);

                                patient.Gender = patientGender; //Join
                            }

                            return patient;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Diseases> Get_Patient_Diseases(int patientID)
        {
            List<Diseases> patientDiseases = new List<Diseases>();
            try
            {
                using (SqlConnection getDiseasesConnection = new SqlConnection(app_connection))
                {
                    getDiseasesConnection.Open();

                    using (SqlCommand getDiseasesCommand = new SqlCommand("Get_Patient_Diseases", getDiseasesConnection))
                    {
                        getDiseasesCommand.CommandType = CommandType.StoredProcedure;
                        getDiseasesCommand.Parameters.AddWithValue("@idpatient", patientID);

                        using (SqlDataReader diseaseReader = getDiseasesCommand.ExecuteReader())
                        {
                            while (diseaseReader.Read())
                            {
                                Diseases disease = new Diseases();

                                disease.DiseaseId = Convert.ToInt32(diseaseReader["DiseaseId"]);
                                disease.Disease = Convert.ToString(diseaseReader["Disease"]);

                                patientDiseases.Add(disease);
                            }

                            return patientDiseases;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeftEyesRx Get_LeftEye_Overview(int patientId)
        {
            LeftEyesRx leftEye = new LeftEyesRx();
            EyeOverview leftEyeOverview = new EyeOverview();

            try
            {
                using (SqlConnection getLeftEyeConnection = new SqlConnection(app_connection))
                {
                    getLeftEyeConnection.Open();

                    using (SqlCommand getLeftEyeCommand = new SqlCommand("Get_Patients_LeftEye", getLeftEyeConnection))
                    {
                        getLeftEyeCommand.CommandType = CommandType.StoredProcedure;
                        getLeftEyeCommand.Parameters.AddWithValue("@idpatientleft", patientId);

                        using (SqlDataReader leftEyeReader = getLeftEyeCommand.ExecuteReader())
                        {
                            if (leftEyeReader.Read())
                            {
                                leftEye.LeftEyeRxId = Convert.ToInt32(leftEyeReader["LeftEyeRxId"]);
                                leftEye.IdPatientLeft = Convert.ToInt32(leftEyeReader["IdPatientLeft"]);
                                leftEye.AxisLeft = Convert.ToInt32(leftEyeReader["AxisLeft"]);

                                leftEyeOverview.sphere = Convert.ToDecimal(leftEyeReader["SphereLeft"]);
                                leftEyeOverview.cylinder = Convert.ToDecimal(leftEyeReader["CylinderLeft"]);
                                leftEyeOverview.addition = Convert.ToDecimal(leftEyeReader["AdditionLeft"]);

                                leftEye.LeftEyeOverview = leftEyeOverview;
                            }
                            return leftEye;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RightEyesRx Get_RightEye_Overview(int patientID)
        {
            RightEyesRx rightEye = new RightEyesRx();
            EyeOverview rightEyeOverview = new EyeOverview();

            try
            {
                using (SqlConnection getRightEyeConnection = new SqlConnection(app_connection))
                {
                    getRightEyeConnection.Open();

                    using (SqlCommand getRightEyeCommand = new SqlCommand("Get_Patients_RightEye", getRightEyeConnection))
                    {
                        getRightEyeCommand.CommandType = CommandType.StoredProcedure;
                        getRightEyeCommand.Parameters.AddWithValue("@idpatientright", patientID);

                        using (SqlDataReader rightEyeReader = getRightEyeCommand.ExecuteReader())
                        {
                            if (rightEyeReader.Read())
                            {
                                rightEye.RightEyeRxId = Convert.ToInt32(rightEyeReader["RightEyeRxId"]);
                                rightEye.IdPatientRight = Convert.ToInt32(rightEyeReader["IdPatientRight"]);
                                rightEye.AxisRight = Convert.ToInt32(rightEyeReader["AxisRight"]);

                                rightEyeOverview.sphere = Convert.ToDecimal(rightEyeReader["SphereRight"]);
                                rightEyeOverview.cylinder = Convert.ToDecimal(rightEyeReader["CylinderRight"]);
                                rightEyeOverview.addition = Convert.ToDecimal(rightEyeReader["AdditionRight"]);

                                rightEye.RightEyeOverview = rightEyeOverview;
                            }

                            return rightEye;
                        }
                    }
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

        public bool HasOrder(int patientId)
        {
            try
            {
                using (SqlConnection hasOrdersConnection = new SqlConnection(app_connection))
                {
                    hasOrdersConnection.Open();
                    SqlCommand getIdsWithOrdersCommand = new SqlCommand("Get_Ids_With_Orders", hasOrdersConnection);
                    getIdsWithOrdersCommand.CommandType = CommandType.StoredProcedure;

                    getIdsWithOrdersCommand.Parameters.AddWithValue("@id", patientId);

                    SqlDataReader idsReader = getIdsWithOrdersCommand.ExecuteReader();
                    if (idsReader.Read())
                    {
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
                                addPatientDiseaseCommand.Parameters.Clear(); // Limpiar los parámetros antes de cada iteración

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

        public void PatientEdit(Patients patient)
        {
            try
            {
                using (SqlConnection sqlEditConnection = new SqlConnection(app_connection))
                {
                    sqlEditConnection.Open();

                    using (SqlCommand editCommand = new SqlCommand("Patient_Edit", sqlEditConnection))
                    {
                        editCommand.CommandType = CommandType.StoredProcedure;

                        editCommand.Parameters.AddWithValue("@patientid", patient.PatientId);
                        editCommand.Parameters.AddWithValue("@firstname", patient.FirstName);
                        editCommand.Parameters.AddWithValue("@middlename", patient.MiddleName);
                        editCommand.Parameters.AddWithValue("@lastname", patient.LastName);
                        editCommand.Parameters.AddWithValue("@age", patient.Age);
                        editCommand.Parameters.AddWithValue("@idgender", patient.IdGender);
                        editCommand.Parameters.AddWithValue("@anotherdiseases", patient.AnotherDiseases);
                        editCommand.Parameters.AddWithValue("@contactnumber", patient.ContactNumber);

                        editCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDiseases(List<int> DiseasesToDelete, List<int> DiseasesToAdd, int patientId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope()) // Existe la posibilidad de realizar muchos inserts y drops, por lo que para asegurarnos de que todo se ejecute correctamente usamos un transactionscope
                {
                    using (SqlConnection sqlUpdateDiseasesConnection = new SqlConnection(app_connection))
                    {
                        sqlUpdateDiseasesConnection.Open();

                        if (DiseasesToDelete.Count() > 0) //Comprobamos que existan elementos a eliminar
                        {
                            using (SqlCommand updateCommand = new SqlCommand("Drop_Disease", sqlUpdateDiseasesConnection))
                            {
                                updateCommand.CommandType = CommandType.StoredProcedure;

                                foreach (int idDisease in DiseasesToDelete)
                                {
                                    updateCommand.Parameters.Clear(); //Limpiar los parámetros antes de cada iteración

                                    updateCommand.Parameters.AddWithValue("@idpatient", patientId);
                                    updateCommand.Parameters.AddWithValue("@iddisease", idDisease);

                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        if (DiseasesToAdd.Count() > 0) //Comprobamos que existan elementos a eliminar
                        {
                            using (SqlCommand updateCommand = new SqlCommand("Add_Disease", sqlUpdateDiseasesConnection))
                            {
                                updateCommand.CommandType = CommandType.StoredProcedure;

                                foreach (int idDisease in DiseasesToAdd)
                                {
                                    updateCommand.Parameters.Clear();

                                    updateCommand.Parameters.AddWithValue("@idpatient", patientId);
                                    updateCommand.Parameters.AddWithValue("@iddisease", idDisease);

                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        scope.Complete(); //Si llegamos a este punto sin errores, concretamos la transacción.
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
                    while (diseasesReader.Read())
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

        /// <summary>
        /// Obtiene todos los registros de dioptrias para generar las historias clínicas (valores entre -30 a 30)
        /// </summary>
        /// <returns>Lista de objetos de tipo Diopters</returns>
        public List<Diopters> GetDioptersToMR()
        {
            List<Diopters> dioptersMR = new List<Diopters>();
            try
            {
                using (SqlConnection getDioptersConnection = new SqlConnection(app_connection))
                {
                    getDioptersConnection.Open();

                    SqlCommand getDioptersMRCommand = new SqlCommand("Get_Diopters_ToMR", getDioptersConnection);
                    getDioptersMRCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dioptersReader = getDioptersMRCommand.ExecuteReader();

                    while (dioptersReader.Read())
                    {
                        Diopters diopter = new Diopters();

                        diopter.DiopterId = Convert.ToInt32(dioptersReader["DiopterId"]);
                        diopter.DiopterValue = Convert.ToDecimal(dioptersReader["DiopterValue"]);

                        dioptersMR.Add(diopter);
                    }

                    return dioptersMR;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddMedicalRecord(LeftEyesRx lefteye, RightEyesRx righteye)
        {
            try
            {
                using (TransactionScope mrScope = new TransactionScope())
                {
                    using (SqlConnection addMRConnection = new SqlConnection(app_connection))
                    {
                        addMRConnection.Open();

                        using (SqlCommand addLeftEyeCommand = new SqlCommand("Add_LeftEye", addMRConnection))
                        {
                            addLeftEyeCommand.CommandType = CommandType.StoredProcedure;

                            addLeftEyeCommand.Parameters.AddWithValue("@idsphereleft", lefteye.IdSphereLeft);
                            addLeftEyeCommand.Parameters.AddWithValue("@idcylinderleft", lefteye.IdCylinderLeft);
                            addLeftEyeCommand.Parameters.AddWithValue("@axisleft", lefteye.AxisLeft);
                            addLeftEyeCommand.Parameters.AddWithValue("@additionleft", lefteye.AdditionLeft);
                            addLeftEyeCommand.Parameters.AddWithValue("@idpatientleft", lefteye.IdPatientLeft);

                            addLeftEyeCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand addRightEyeCommand = new SqlCommand("Add_RightEye", addMRConnection))
                        {
                            addRightEyeCommand.CommandType = CommandType.StoredProcedure;

                            addRightEyeCommand.Parameters.AddWithValue("@idsphereright", righteye.IdSphereRight);
                            addRightEyeCommand.Parameters.AddWithValue("@idcylinderright", righteye.IdCylinderRight);
                            addRightEyeCommand.Parameters.AddWithValue("@axisright", righteye.AxisRight);
                            addRightEyeCommand.Parameters.AddWithValue("@additionright", righteye.AdditionRight);
                            addRightEyeCommand.Parameters.AddWithValue("@idpatientright", righteye.IdPatientRight);

                            addRightEyeCommand.ExecuteNonQuery();
                        }

                        mrScope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
