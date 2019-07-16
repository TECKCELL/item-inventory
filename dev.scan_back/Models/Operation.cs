using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace dev.scan_back.Models
{
    public class Operation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Int64 Number { get; set; }
        [JsonIgnore]
        public virtual List<ProductFile> productFiles { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime RecordDate { get; set; }



    }
}