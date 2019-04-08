using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class ScanViewModel
    {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string Libellé { get; set; }
        public int Quantité { get; set; }
        public int importID { get; set; }

        public ScanViewModel() { }
        
    }   
}