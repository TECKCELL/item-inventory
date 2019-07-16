using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace dev.scan_back.Models
{
    public enum Immobilisation
    {
        Immobilisation,
        Amortissement,
        Dotation
    }
    public enum Amortissement
    {
        Linéaire,
        Dégressif,
        Sans
    }
    public class ProductFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(Order = 0), ForeignKey("Operation")]
        public Guid OperationId { get; set; }
        public string RefImmo { get; set; }
        public string LabelImmo { get; set; }
        public string CLabelImmo { get; set; }
        public string CodeProduit { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string Matricule { get; set; }
        public string MotifOut { get; set; }
        public string Localisation { get; set; }
        public string Familly { get; set; }
        public string SFamilly { get; set; }
        public DateTime AcquiDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime OutDate { get; set; }
        public decimal PriceSes { get; set; }
        public decimal Amort { get; set; }
        public decimal Coeff { get; set; }
        public decimal Tva { get; set; }
        public decimal ValBuy { get; set; }
        public Int64 AccountNumber { get; set; }
        public Int64 NumInvent { get; set; }
        public Int64 NumFiche { get; set; }
        public virtual Operation Operation { get; set; }
        public Immobilisation Immobilisation { get; set; }
        public Amortissement Amortissement { get; set; }


    }
}