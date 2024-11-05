using Data_Users;
using Patient_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Users
{
    public class B_App_Out//Clase que contiene las reglas de negocio para salida/obtención de datos [GET] ¿Se incluirá aquí DELETE?
    {
        /// <summary>
        /// Instancia de capa de datos con métodos para el manejo de la applicación.
        /// </summary>
        D_App dataAppTools = new D_App();

        /// <summary>
        /// Retorna una lista de pacientes únicamente con la información necesaria para el dashboard:
        /// Nombre(s), Apellidos, Deuda y si poseen o no registros de historia clinica asociados a él.
        /// </summary>
        /// <returns>Lista con objetos de tipo PatientsOverview</returns>
        public List<PatientsOverview> B_GetAllPatientsOverview() //Evitamos mandar mucha info innecesaria para reducir carga y seguridad (no viaja toda la info de todos los clientes cada que accedemos al dashboard)
        {
            List<Patients> allPatients = dataAppTools.getAllPatients();
            List<PatientsOverview> allPatientsOverview = new List<PatientsOverview>();

            foreach (Patients patient in allPatients)
            {
                PatientsOverview patientOverview = new PatientsOverview();

                patientOverview.PatientId = patient.PatientId;
                patientOverview.FirstName = patient.FirstName;
                patientOverview.MiddleName = patient.MiddleName;
                patientOverview.LastName = patient.LastName;
                patientOverview.Due = patient.Due;
                patientOverview.HasMedicalRecord = dataAppTools.HasMR(patient.PatientId); //HasMR (Medical Record) retorna true or false
                patientOverview.HasOrders = dataAppTools.HasOrder(patient.PatientId); //HasOrder retorna true or false

                allPatientsOverview.Add(patientOverview);
            }

            return allPatientsOverview;
        }
        
        /// <summary>
        /// Obtiene y retorna una lista con los pacientes que sí tuvieron coincidencia (en nombre(s) y apellido(s) con la string de busqueda.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>lista filtrada con objetos de tipo PatientsOverview</returns>
        public List<PatientsOverview> B_SearchPatients(string searchString)
        {
            List<PatientsOverview> allPatientsOverview = B_GetAllPatientsOverview();
            List<PatientsOverview> searchedPatientsOverview = new List<PatientsOverview>();
            try
            {
                foreach (PatientsOverview patient in allPatientsOverview)
                {
                    if (patient.FullName.ToLower().Contains(searchString.ToLower()))
                    {
                        searchedPatientsOverview.Add(patient);
                    }
                }

                return searchedPatientsOverview;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los padecimientos registrados para mostrar en un dropdownlist.
        /// </summary>
        /// <returns>Lista de tipo Diseases</returns>
        public List<Diseases> B_GetDiseases()
        {
            try
            {
                List<Diseases> allDiseases = dataAppTools.GetDiseases();
                return allDiseases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Capa de negocio: Obtiene todos los registros de dioptrias para generar las historias clínicas (valores entre -30 a 30).
        /// </summary>
        /// <returns>Lista de objetos de tipo Diopters</returns>
        public List<Diopters> B_GetDioptersToMR()
        {
            try
            {
                List<Diopters> dioptersMR = dataAppTools.GetDioptersToMR();
                return dioptersMR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PatientDetails Get_Patient_Details(int patientID)
        {
            try
            {
                PatientDetails patientDetails = new PatientDetails()
                {
                    Patient = dataAppTools.getPatientById(patientID),
                    Diseases = dataAppTools.Get_Patient_Diseases(patientID),
                    LeftEyeRx = dataAppTools.Get_LeftEye_Overview(patientID),
                    RightEyeRx = dataAppTools.Get_RightEye_Overview(patientID)
                };

                return patientDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
