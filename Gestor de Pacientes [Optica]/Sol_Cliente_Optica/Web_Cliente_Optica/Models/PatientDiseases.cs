using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
{
    public class PatientDiseases
    {
        public int PatientDiseaseId { get; set; }
        public int IdPatient { get; set; }
        public int IdDisease { get; set; }
    }
}