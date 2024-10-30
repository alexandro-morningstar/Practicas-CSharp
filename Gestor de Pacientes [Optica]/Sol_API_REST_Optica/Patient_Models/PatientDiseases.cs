using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Models
{
    public class PatientDiseases
    {
        public int PatientDiseaseId { get; set; }
        public int IdPatient { get; set; }
        public int IdDisease { get; set; }
    }
}
