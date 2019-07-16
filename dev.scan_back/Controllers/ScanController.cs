using dev.scan_back.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace dev.scan_back.Controllers
{
    public class ScanController : Controller
    {
        private ScanDbContext db = new ScanDbContext();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
        // GET: Scan

        public ActionResult Diff()
        {
            List<Import> imports = new List<Import>();
            List<ImportViewModel> Limports = new List<ImportViewModel>();
            imports = this.db.Import.ToList();
            List<ScanViewModel> LscanviewModel = new List<ScanViewModel>();
            using (var context = new ScanDbContext())
            {


                LscanviewModel = context.Database
                    .SqlQuery<ScanViewModel>("GETScanViewModel")
                    .ToList();
            }
            foreach (Import import in imports)
            {
               
                ImportViewModel importViewModel = new ImportViewModel()
                {
                    Id = import.Id,
                    ClientId = import.ClientId,
                    //Assort = LassortViewModel,
                    Scans = LscanviewModel.Where(i=>i.importID == import.Id).ToList(),
                    Date = import.Date.ToShortDateString(),
                    Type = import.Type

                };
                Limports.Add(importViewModel);
            }
            

            return View("Import", Limports);
        }
        public ActionResult DisplayOperation()
        {
            List<OperationViewModel> operationViewModels = new List<OperationViewModel>();
            foreach (var operation in db.Operation.ToList().OrderByDescending(o=>o.RecordDate))
            {
                OperationViewModel operationView = new OperationViewModel()
                {
                    Id = operation.Id,
                    beginDate = operation.beginDate,
                    endDate = operation.endDate,
                    Number = operation.Number
                };
                operationViewModels.Add(operationView);
            }

            return View("OperationHisto", operationViewModels);
        }
        public ActionResult DisplayProduct(Guid OperationId)
        {
            List<ProductfileViewModel> productfiles = new List<ProductfileViewModel>();
            foreach (var product in db.ProductFile.ToList().Where(o=>o.OperationId == OperationId))
            {
                ProductfileViewModel productfileViewModel = new ProductfileViewModel()
                {
                    AccountNumber = product.AccountNumber,
                    AcquiDate = product.AcquiDate,
                    Amort = product.Amort,
                    amortissement = product.Amortissement,
                    LabelImmo = product.LabelImmo,
                    CLabelImmo = product.CLabelImmo,
                    CodeProduit = product.CodeProduit,
                    Coeff = product.Coeff,
                    Description = product.Description,
                    Supplier = product.Supplier,
                    Matricule = product.Matricule,
                    MotifOut = product.MotifOut,
                    Localisation = product.Localisation,
                    Familly = product.Familly,
                    SFamilly = product.SFamilly,
                    ServiceDate = product.ServiceDate,
                    OutDate = product.OutDate,
                    PriceSes = product.PriceSes,
                    Tva = product.Tva,
                    ValBuy = product.ValBuy,
                    NumInvent = product.NumInvent.ToString(),
                    NumFiche = product.NumFiche.ToString(),
                    immobilisationId = (int)Enum.Parse(typeof(Immobilisation), Enum.GetName(typeof(Immobilisation), product.Immobilisation)) ,
                    OperationId = product.Operation.Id  
                };
                productfiles.Add(productfileViewModel);
            }

            return  View("DisplayProduct", productfiles);
        }
        public ActionResult DisplayCounting(Guid OperationId) {

            List<CountingProductFileViewModel> LCounting = new List<CountingProductFileViewModel>();

            if (OperationId != Guid.Empty)
            {
               
                using (var context = new ScanDbContext())
                {

                    var operationId = new SqlParameter("@OperationId", OperationId);

                    LCounting = context.Database
                        .SqlQuery<CountingProductFileViewModel>("GetCountingProductFile @OperationId", operationId)
                        .ToList();
                }
            }


            return View(LCounting);


        }
        [HttpGet]
        public ActionResult GetProductCard(Int64 number)
        {
            
            ProductFile product = db.ProductFile.Where(o => o.NumFiche == number).First();
            
                ProductfileViewModel productfile = new ProductfileViewModel()
                {
                    AccountNumber = product.AccountNumber,
                    AcquiDate = product.AcquiDate,
                    Amort = product.Amort,
                    amortissement = product.Amortissement,
                    LabelImmo = product.LabelImmo,
                    CLabelImmo = product.CLabelImmo,
                    CodeProduit = product.CodeProduit,
                    Coeff = product.Coeff,
                    Description = product.Description,
                    Supplier = product.Supplier,
                    Matricule = product.Matricule,
                    MotifOut = product.MotifOut,
                    Localisation = product.Localisation,
                    Familly = product.Familly,
                    SFamilly = product.SFamilly,
                    ServiceDate = product.ServiceDate,
                    OutDate = product.OutDate,
                    PriceSes = product.PriceSes,
                    Tva = product.Tva,
                    ValBuy = product.ValBuy,
                    NumInvent = product.NumInvent.ToString(),
                    NumFiche = product.NumFiche.ToString(),
                    immobilisationId = (int)Enum.Parse(typeof(Immobilisation), Enum.GetName(typeof(Immobilisation), product.Immobilisation)),
                    OperationId = product.Operation.Id
                };

            return View("_productCard", productfile);
        }
        [HttpPost]
        public ActionResult SaveProduct(ProductfileViewModel productFilemodel)
        {
            Random rand = new Random();
            Int64 Number = Int64.Parse(productFilemodel.NumInvent);
            Operation operation = db.Operation.Where(o => o.Number == Number).First();
            ProductFile product = new ProductFile()
            {
                AccountNumber = productFilemodel.AccountNumber,
                AcquiDate = productFilemodel.AcquiDate,
                Amort = productFilemodel.Amort,
                Amortissement = productFilemodel.amortissement,
                LabelImmo = productFilemodel.LabelImmo,
                CLabelImmo = productFilemodel.CLabelImmo,
                CodeProduit = productFilemodel.CodeProduit, //productFilemodel.CLabelImmo.Substring(0, 3) + LongRandom(10000000000, Int64.MaxValue, rand),
                Coeff = productFilemodel.Coeff,
                Description = productFilemodel.Description,
                Supplier = productFilemodel.Supplier,
                Matricule = productFilemodel.Matricule,
                MotifOut = productFilemodel.MotifOut,
                Localisation = productFilemodel.Localisation,
                Familly = productFilemodel.Familly,
                SFamilly = productFilemodel.SFamilly,
                ServiceDate = productFilemodel.ServiceDate,
                OutDate = productFilemodel.OutDate,
                PriceSes = productFilemodel.PriceSes,
                Tva = productFilemodel.Tva,
                ValBuy = productFilemodel.ValBuy,
                NumInvent = operation.Number,
                NumFiche = Int64.Parse(productFilemodel.NumFiche),//LongRandom(10000000000, Int64.MaxValue, rand),
                Immobilisation = (Immobilisation)Enum.Parse(typeof(Immobilisation), Enum.GetName(typeof(Immobilisation), productFilemodel.immobilisationId)),
                Operation = operation,
                OperationId = operation.Id


            };
            db.ProductFile.Add(product);
            db.SaveChanges();
           return RedirectToAction("DisplayOperation");

        }
      public ActionResult Details(int Id)
        {
            List<ScanViewModel> LscanviewModel = new List<ScanViewModel>();
            using (var context = new ScanDbContext())
            {

                LscanviewModel = context.Database
                    .SqlQuery<ScanViewModel>("GETScanViewModel")
                    .ToList();
            }
            Import import = this.db.Import.ToList().Where(i=>i.Id == Id).FirstOrDefault();
            List<ScanViewModel> scanViewModels = LscanviewModel.Where(s => s.importID == Id).ToList();
            return View("Details", scanViewModels);
        }
        public ActionResult DeleteImport(int Id)
        {
            var import = new Import { Id = Id };
            List<ScanViewModel> LscanviewModel = new List<ScanViewModel>();
            using (var context = new ScanDbContext())
            {

                LscanviewModel = context.Database
                    .SqlQuery<ScanViewModel>("GETScanViewModel")
                    .ToList();
            }
            List<ScanViewModel> scanViewModels = LscanviewModel.Where(s => s.importID == Id).ToList();
            ImportViewModel importmodel = new ImportViewModel()
            {
                Id = import.Id,
                ClientId = import.ClientId,
                Scans = scanViewModels,
                Type = import.Type,
                Date = import.Date.ToShortDateString()
            };
            db.Entry(import).State = EntityState.Deleted;
            db.SaveChanges();

            return Json(importmodel);

        }
        public ActionResult DisplayResult()
        {
            List<ResultScan> LresultScan = new List<ResultScan>();
            List<assort> Lassort = new List<assort>();
            if (this.db.Import.Count()>0)
            {
                Import import = this.db.Import.OrderByDescending(d => d.Date).Where(c => c.Type == TypeImport.Scan).OrderByDescending(d => d.Date).First();
                using (var context = new ScanDbContext())
                {

                    var ImportId = new SqlParameter("@ImportId", import.Id);

                    LresultScan = context.Database
                        .SqlQuery<ResultScan>("GETScanResult @ImportId", ImportId)
                        .ToList();
                }
            }
          
            
            return View(LresultScan);
        }

        public ActionResult ShowCustomerData()
        {
            List<Procurement> procurement = this.db.Procurement.ToList();
            return PartialView("_Procurement", procurement);
        }
        
        public ActionResult selectByCategorie(string categorie)
        {
            List<ResultScan> LresultScan = new List<ResultScan>();
            List<assort> Lassort = new List<assort>();
            
            Import import = this.db.Import.OrderByDescending(d => d.Date).Where(c => c.Type == TypeImport.Scan).OrderByDescending(d => d.Date).First();
            using (var context = new ScanDbContext())
            {

                var ImportId = new SqlParameter("@ImportId", import.Id);

                LresultScan = context.Database
                    .SqlQuery<ResultScan>("GETScanResult @ImportId", ImportId)
                    .ToList();
            }
            List<ResultScan> resultCategorie = LresultScan.Where(c => c.catégorie == categorie).ToList();

            return Json(resultCategorie, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult simulator()
        {
            //var importClient = db.Import.OrderByDescending(d => d.Date).Where(c => c.Type == TypeImport.Client).First();
            //var proc = db.assort.Where(a => a.ImportId == importClient.Id).GroupBy(e => e.Ean13);
            List<AssortViewModelcs> Lassort= new List<AssortViewModelcs>();
            Import import = this.db.Import.OrderByDescending(d => d.Date).Where(c => c.Type == TypeImport.Client).OrderByDescending(d => d.Date).First();
            var proc = db.Procurement.ToList().GroupBy(e=>e.Ean13);
            using (var context = new ScanDbContext())
            {
                Lassort = context.Database
                    .SqlQuery<AssortViewModelcs>("GETSimulator")
                    .ToList();
            }

            List<SimulationViewModel> LSimulationViewModel = new List<SimulationViewModel>();
            foreach (var item in Lassort)
            {
                SimulationViewModel sm = new SimulationViewModel();
                sm.Ean13 = item.Ean13;
                sm.label = item.Label;
                sm.StockReel = item.quantity;
                //sm.Price = item.Price;
                sm.OrderedQte = 0;
                sm.QteSales = 0;
                sm.M1 = 0;
                sm.M2 = 0;
                sm.M3 = 0;
                LSimulationViewModel.Add(sm);
            }
        
            return PartialView("_simulator", LSimulationViewModel);
        }
        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
        //public ActionResult ProductFile()
        //{


        //}
        //[System.Web.Http.HttpGet]

        //public ActionResult openFoodFact(string Ean13)
        //{
        //    var client = new HttpClient();
        //    var query = "https://fr.openfoodfacts.org/api/v0/produit/" + Ean13 + ".json";
        //    var jsonObject = await client.GetAsync(query);
        //    return Json(jsonObject, JsonRequestBehavior.AllowGet);
        //}
    }
}