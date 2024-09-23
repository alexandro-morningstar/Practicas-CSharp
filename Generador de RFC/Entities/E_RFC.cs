using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class E_RFC
    {
        //--------- Read & Write Properties ---------
        public int IdRFC { get; set; }
        public string Name { get; set; }
        public string PaternalSurname { get; set; }
        public string MaternalSurname { get; set; }
        public DateTime BirthDate { get; set; }
        public string RFC { get; set; }

        //--------- Read-only Properties ---------

        public int GetIdRFC
        {
            get
            {
                return IdRFC;
            }
        }

        public string GetName
        {
            get
            {
                return Name;
            }
        }

        public string GetPaternalSurname
        {
            get
            {
                return PaternalSurname;
            }
        }

        public string GetMaternalSurname
        {
            get
            {
                return MaternalSurname;
            }
        }

        public DateTime GetBirthDate
        {
            get
            {
                return BirthDate;
            }
        }

        public string GetRFC
        {
            get
            {
                return RFC;
            }
        }
    }
}
