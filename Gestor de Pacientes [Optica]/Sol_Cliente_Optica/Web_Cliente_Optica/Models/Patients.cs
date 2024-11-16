using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
{
    public class Patients
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int IdGender { get; set; }
        public string AnotherDiseases { get; set; }
        public string ContactNumber { get; set; }
        public decimal Due { get; set; }
        public DateTime RegistryDate { get; set; }
        public Genders Gender { get; set; }
    }
}