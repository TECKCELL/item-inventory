using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class SimulationViewModel
    {
        public int Id { get; set; }
        public string Ean13 { get; set; }
        public string label { get; set; }
        public decimal Price { get; set; }
        public int StockReel { get; set; }
        public int OrderedQte { get; set; }
        public int QteSales { get; set; }
        public int M1 { get; set; }
        public int M2 { get; set; }
        public int M3 { get; set; }

        public SimulationViewModel() { }

        public SimulationViewModel(int id)
        {

        }

    }
    
}