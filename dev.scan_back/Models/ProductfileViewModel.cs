using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dev.scan_back.Models
{
    
    public class ProductfileViewModel
    {
        public Guid OperationId { get; set; }
        public string RefImmo { get; set; }
        [Display(Name = "Libellé du bien")]
        public string LabelImmo { get; set; }
        [Display(Name = "Complément libellé")]
        public string CLabelImmo { get; set; }
        [Display(Name = "code produit")]
        public string CodeProduit { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Fournisseur")]
        public string Supplier { get; set; }
        [Display(Name = "Matricule")]
        public string Matricule { get; set; }
        [Display(Name = "Motif de sortie")]
        public string MotifOut { get; set; }
        [Display(Name = "Localisation")]
        public string Localisation { get; set; }
        [Display(Name = "Famille")]
        public string Familly { get; set; }
        [Display(Name = "Sous-Famille")]
        public string SFamilly { get; set; }
        [Display(Name = "Date d'acquisition")]
        public DateTime AcquiDate { get; set; }
        [Display(Name = "Date de mise en service")]
        public DateTime ServiceDate { get; set; }
        [Display(Name = "Date de sortie")]
        public DateTime OutDate { get; set; }
        [Display(Name = "Prix de session")]
        public decimal PriceSes { get; set; }
        [Display(Name = "Amortissement")]
        public decimal Amort { get; set; }
        [Display(Name = "Coefficient applicable")]
        public decimal Coeff { get; set; }
        [Display(Name = "Tva")]
        public decimal Tva { get; set; }
        [Display(Name = "Valeur d'achat")]
        public decimal ValBuy { get; set; }
        [Display(Name = "Numéro de compte")]
        public Int64 AccountNumber { get; set; }
        [Display(Name = "Numéro d'inventaire")]
        public string NumInvent { get; set; }
        [Display(Name = "Numéro de fiche")]
        public string NumFiche { get; set; }
        public int immobilisationId { get; set; }
        public IEnumerable<SelectListItem> Immobilisation { get { return Enum.GetValues(typeof(Immobilisation)).OfType<Immobilisation>().ToList().Select((immo, index) => new SelectListItem { Text = immo.ToString(), Value = index.ToString() }); } }
        public int AmortissementId { get; set; }
        public Amortissement amortissement { get; set; }

    }
    
}