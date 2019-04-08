using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dev.scan_back.Models
{
    public enum TypeImport {Client = 1,Scan = 2}
    public class Import
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }

        [Column(Order = 0), ForeignKey("Client")]
        public int ClientId { get; set; }

        [JsonIgnore]
        public virtual List<SCANS> Scans { get; set; }

        [JsonIgnore]
        public virtual List<assort> Assort { get; set; }

        public TypeImport Type { get; set; }

        public DateTime Date { get; set; }
    }
}