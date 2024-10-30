using Data_Users;
using Newtonsoft.Json;
using Patient_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Users
{
    public class B_App_In  //Clase que contiene reglas para entradas de datos [POST/PUT] ¿Se incluirá aqui DELETE?
    {
        /// <summary>
        /// Instancia de capa de datos con métodos para el manejo de la applicación.
        /// </summary>
        D_App dataAppTools = new D_App();

        /// <summary>
        /// Los un campo es null para el usuario (de acuerdo a la lógica del negocio) se inserta una string vacía en su lugar.
        /// </summary>
        /// <param name="newPatient">Objeto Patients</param>
        public void B_AddPatient(Patients newPatient, string listOfDiseases)
        {
            //Campos null convertir a strings vacios
            if (newPatient.MiddleName == null)
            {
                newPatient.MiddleName = "";
            }

            if (newPatient.AnotherDiseases == null)
            {
                newPatient.AnotherDiseases = "";
            }

            if (newPatient.ContactNumber == null)
            {
                newPatient.ContactNumber = "S/N";
            }

            //Trimmear strings para evitar espacios al inicio y final.
            newPatient.FirstName.Trim();
            newPatient.MiddleName.Trim();
            newPatient.LastName.Trim();

            //La deuda siempre inicia en cero.
            newPatient.Due = 0;

            //Convertir campos a string
            Convert.ToString(newPatient.ContactNumber);

            //Desearizar la lista de padecimientos
            int[] diseaseIds = JsonConvert.DeserializeObject<int[]>(listOfDiseases);

            /*Si la lista de padecimientos viene vacia, llamamos al método que únicamente registra un paciente
             de lo contrario, llamamos al método que agrega al paciente y los padecimientos.*/
            if (diseaseIds.Length > 0)
            {
                dataAppTools.AddPatientWithDiseases(newPatient, diseaseIds);
            }
            else
            {
                dataAppTools.AddPatient(newPatient); //Llamamos al método para agregar usuarios con los campos checkeados y la deduda en cero (y no null).
            }
        }

        /// <summary>
        /// Instancia un objeto de la capa de datos para agregar nuevos padecimientos a un paciente existente.
        /// </summary>
        /// <param name="patientId">int: Id del paciente que se está editando</param>
        /// <param name="diseaseIds">Array de ids de padecimientos.</param>
        //public void B_AddPatientDisease(int patientId, int[] diseaseIds)
        //{
        //    foreach (int diseaseId in diseaseIds)
        //    {
        //        try
        //        {
        //            dataAppTools.AddPatientDisease(patientId, diseaseId);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
    }
}
