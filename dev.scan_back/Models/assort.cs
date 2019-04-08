using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace dev.scan_back.Models
{
    public class assort
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Ean13 { get; set; }
        [Required]
        public string Label { get; set; }
        public decimal Price { get; set; }
        public int ImportId { get; set; }
        public virtual Import Import { get; set; }
        public string categorie { get; set; }

    }
}