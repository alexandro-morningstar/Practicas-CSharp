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
        /// Nombre(s), Apellidos y Deuda.
        /// </summary>
        /// <returns>List<PatientsOverview></returns>
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

                allPatientsOverview.Add(patientOverview);
            }

            return allPatientsOverview;
        }

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
    }
}
