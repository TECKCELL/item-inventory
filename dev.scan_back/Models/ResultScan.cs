using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class ResultScan
    {
        public int Id { get; set; }
        public int importId { get; set; }
        public string Matricule { get; set; }
        public string Libellé { get; set; }
        public string catégorie { get; set; }
        public decimal Price { get; set; }
        public int Stockreel { get; set; }
        public int Stockthéorique { get; set; }
        public int demarque { get; set; }
        public decimal MtnDemarque { get; set; }

    }
}