using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Models
{
    public class LeftEyesRx
    {
        public int LeftEyeRxId { get; set; }
        public int IdSphereLeft { get; set; }
        public int IdCylinderLeft { get; set; }
        public int AxisLeft { get; set; }
        public int AdditionLeft { get; set; }
        public int IdPatientLeft { get; set; }
        public EyeOverview LeftEyeOverview { get; set; }//Contiene los valores que se obtienen de las referencias (id) en las propiedades principales de esta clase (LeftEyesRx)
    }
}
