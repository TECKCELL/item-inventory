using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class ImportViewModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual List<ScanViewModel> Scans { get; set; }
        //public virtual List<AssortViewModelcs> Assort { get; set; }
        public TypeImport Type { get; set; }
        public string Date { get; set; }
    }
}