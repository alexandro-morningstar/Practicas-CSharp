using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Models
{
    public class PatientDetails
    {
        public Patients Patient { get; set; }
        public List<Diseases> Diseases { get; set; }
        public LeftEyesRx LeftEyeRx { get; set; }
        public RightEyesRx RightEyeRx { get; set;}
    }
}
