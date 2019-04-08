using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dev.scan_back.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Label { get; set; }

        [JsonIgnore]
        public virtual List<Import> Imports { get; set; }

    }
}