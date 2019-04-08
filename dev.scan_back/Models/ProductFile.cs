using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace dev.scan_back.Models
{
    public class ProductFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string RefImmo { get; set; }
        public string Code { get; set; }
        public string NumFiche { get; set; }
        public string LabelImmo { get; set; }
        public string Matricule { get; set; }
        public DateTime AcquiDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal Amort { get; set; }
        public Int64 ValBrutImmo { get; set; }
        public Int64 ValBrutElOut { get; set; }
        public Int64 CumulOpenExer { get; set; }
        public Int64 ExcerDotation { get; set; }
        public Int64 ValTotal { get; set; }
        public Int64 RelativeAmount { get; set; }
        public Int64 CumulAmort { get; set; }
        public Int64 ValResid { get; set; }
        public Int64 ValElemOut { get; set; }

    }
}