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

        // GET api/values
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
