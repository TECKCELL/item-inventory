using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class AssortViewModelcs
    {      
        //public int Id { get; set; }
        public string Ean13 { get; set; }
        public string Label { get; set; }
        //public decimal Price { get; set; }
        public int ImportId { get; set; }
        //public string categorie { get; set; }
        public int quantity { get; set; }

        //public AssortViewModelcs(assort assort, int id)
        //{           
        //    //this.Id = assort.Id;
        //    this.Ean13 = assort.Ean13;
        //    this.ImportId = id;
        //    this.Label = assort.Label;
        //    this.Price = assort.Price;
        //}
    }
}