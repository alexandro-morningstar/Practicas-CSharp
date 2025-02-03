using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cliente_Optica.Models
{
    public class DiseasesUpdate
    {
        public List<int> previous {  get; set; }
        public List<int> current { get; set; }
    }
}