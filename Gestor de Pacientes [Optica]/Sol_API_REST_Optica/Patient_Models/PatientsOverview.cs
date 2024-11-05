using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Models
{
    public class PatientsOverview
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal Due { get; set; }
        public bool HasMedicalRecord { get; set; }
        public bool HasOrders { get; set; }

        private string _fullName { get; set; }
        public string FullName
        {
            get
            {
                if (_fullName == null)
                {
                    if (MiddleName != null)
                    {
                        _fullName = $"{FirstName} {MiddleName} {LastName}";
                    }
                    else
                    {
                        _fullName = $"{FirstName} {LastName}";
                    }
                }
                return _fullName;
            }
        }
    }
}
