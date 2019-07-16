using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dev.scan_back.Models
{
    public class OperationViewModel
    {
        public Guid Id { get; set; }
        public Int64 Number { get; set; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Téléchargement")]
        public HttpPostedFileBase[] files { get; set; }
        [Display(Name = "Début immobilisation")]
        public DateTime beginDate { get; set; }
        [Display(Name = "Fin immobilisation")]
        public DateTime endDate { get; set; }
    }
}