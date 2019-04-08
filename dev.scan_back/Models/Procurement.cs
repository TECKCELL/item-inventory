using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace dev.scan_back.Models
{
    public class Procurement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string Ean13 { get; set; }
        public int Quantity { get; set; } 
        public int LastQuantity { get; set; }
        public string Label { get; set; }
        public string categorie { get; set; }
       

    }
}