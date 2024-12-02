using Business_Users;
using Patient_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Users_Models;

namespace API_REST_Optica.Controllers
{
    public class ValuesController : ApiController
    {
        //Instancias de la capa de negocio.
        public B_Login businessLoginTools = new B_Login();
        public B_App_In businessAppInTools = new B_App_In();
        public B_App_Out businessAppOutTools = new B_App_Out();

        /// <summary>
        /// Haya encontrado una coincidencia o no, retorna un usuario (lleno o vacío) para su manejo en el lado del cliente.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Objeto de tipo Users</returns>
        [HttpGet]
        public Users Login(string username, string password)
        {
            try
            {
                Users user = businessLoginTools.B_LoginValidation(username, password);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la vista previa de todos los registros de pacientes en objetos PatientOverview previamente ensamblados en la capa de negocio.
        /// </summary>
        /// <returns>Lista de objetos PatientsOverview</returns>
        [HttpGet]
        [Route("api/Values/PatientsOverview")]
        public List<PatientsOverview> GetPatientsOverview()
        {
            List<PatientsOverview> allPatientsOverview = new List<PatientsOverview>();

            try
            {
                allPatientsOverview = businessAppOutTools.B_GetAllPatientsOverview();
                return allPatientsOverview;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de pacientes que tienen coincidencia (Nombre(s) y/o Apellido(s)) con la string ingresada.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Lista de objetos de tipo PatientsOverview</returns>
        [HttpGet]
        [Route("api/Values/Search")]
        public List<PatientsOverview> Search(string searchString)
        {
            List<PatientsOverview> searchedPatientsOverview = new List<PatientsOverview>();

            try
            {
                searchedPatientsOverview = businessAppOutTools.B_SearchPatients(searchString);
                return searchedPatientsOverview;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Solicitud POST para el registro de nuevo paciente.
        /// </summary>
        /// <param name="newPatient"></param>
        /// <param name="listOfDiseases"></param>
        [HttpPost]
        public void AddPatient(Patients newPatient, string listOfDiseases)
        {
            try
            {
                businessAppInTools.B_AddPatient(newPatient, listOfDiseases);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Solicitud GET para obtener una lista con todos los posibles padecimientos más comunes (se usa para la dropdownlist en el formulario de registro de paciente).
        /// </summary>
        /// <returns>Lista de objetos de tipo Diseases</returns>
        [HttpGet]
        [Route("api/Values/GetDiseases")]
        public List<Diseases> GetDiseases()
        {
            List<Diseases> allDiseases = new List<Diseases>();
            try
            {
                return allDiseases = businessAppOutTools.B_GetDiseases();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/Values/GetDioptersMR")]
        public List<Diopters> GetDioptersMR()
        {
            List<Diopters> dioptersMR = new List<Diopters>();
            try
            {
                return dioptersMR = businessAppOutTools.B_GetDioptersToMR();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/Values/AddMedicalRecord")]
        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            try
            {
                businessAppInTools.B_AddMedicalRecord(medicalRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/Values/PatientEdit")]
        public void PatientEdit(Patients patient)
        {
            try
            {
                businessAppInTools.B_PatientEdit(patient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/Values/GetPatientDetails")]
        public PatientDetails GetPatientDetails(int patientID)
        {
            PatientDetails patientDetails = new PatientDetails();
            try
            {
                return patientDetails = businessAppOutTools.Get_Patient_Details(patientID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //[HttpPost]
        //[Route("api/Values/AddPatientDisease")]
        //public void AddPatientDisease(int patientId, int[] diseaseIds)
        //{
        //    try
        //    {
        //        businessAppInTools.B_AddPatientDisease(patientId, diseaseIds);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
