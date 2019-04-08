using System.ComponentModel.DataAnnotations;
using System.Web;

namespace dev.scan_back.Models
{
    public class FileModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Téléchargement")]
        public HttpPostedFileBase[] files { get; set; }

    }
}