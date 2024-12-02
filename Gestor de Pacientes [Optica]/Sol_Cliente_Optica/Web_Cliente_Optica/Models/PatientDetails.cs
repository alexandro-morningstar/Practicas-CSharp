using System.Collections.Generic;

namespace Web_Cliente_Optica.Models
{
    public class PatientDetails
    {
        public Patients Patient { get; set; }
        public List<Diseases> Diseases { get; set; }
        public LeftEyesRx LeftEyeRx { get; set; }
        public RightEyesRx RightEyeRx { get; set; }
        
    }
}