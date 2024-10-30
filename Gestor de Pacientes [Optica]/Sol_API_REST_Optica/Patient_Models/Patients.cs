using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Models
{
    public class Patients
    {
        public int PatientId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "El nombre(s) no puede exceder los 30 caracteres")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "El nombre(s) no puede exceder los 30 caracteres")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "El nombre(s) no puede exceder los 30 caracteres")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Range(1, 150, ErrorMessage = "La edad no puede ser menor a 1 ni mayor a 150")]
        public int Age { get; set; }

        [Required]
        public int IdGender { get; set; }

        [DataType(DataType.Text)]
        public string AnotherDiseases { get; set; }

        public string ContactNumber { get; set; }

        [DataType(DataType.Currency)]
        public decimal Due { get; set; }

        public DateTime RegistryDate { get; set; }

        public Genders Gender { get; set; } //Referencia a la tabla de generos? INNER JOIN?
    }
}
