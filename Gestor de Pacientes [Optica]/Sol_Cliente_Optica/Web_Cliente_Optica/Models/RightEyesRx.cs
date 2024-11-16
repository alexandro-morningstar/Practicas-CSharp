using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
{
    public class RightEyesRx
    {
        public int RightEyeRxId { get; set; }
        public int IdSphereRight { get; set; }
        public int IdCylinderRight { get; set; }
        public int AxisRight { get; set; }
        public int AdditionRight { get; set; }
        public int IdPatientRight { get; set; }
        public EyeOverview RightEyeOverview { get; set; } //Contiene los valores que se obtienen de las referencias (id) en las propiedades principales de esta clase (RightEyesRx)

    }
}