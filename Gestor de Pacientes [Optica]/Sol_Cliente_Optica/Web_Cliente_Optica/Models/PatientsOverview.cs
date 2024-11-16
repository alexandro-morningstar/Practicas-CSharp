using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
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
    }
}