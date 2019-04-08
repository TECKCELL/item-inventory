using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace dev.scan_back.Models
{
    public class SCANS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 0), ForeignKey("Import")]
        public int ImportId { get; set; }
        [Required]
        public string Ean13 { get; set; }
        public virtual Import Import { get; set; }
      
    }
}