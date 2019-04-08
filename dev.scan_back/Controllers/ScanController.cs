using dev.scan_back.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
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
                //List<assort> Lassort = new List<assort>();
                //List<AssortViewModelcs> LassortViewModel = new List<AssortViewModelcs>();
                //Lassort = this.db.assort.Where(a => a.ImportId == import.Id).ToList();

                //List<SCANS> Lscans = this.db.Scans.Where(s => s.ImportId == import.Id).ToList();
                

                //if (Lscans.Count() > 0)
                //{
                //    foreach (SCANS scan in Lscans)
                //    {
                //        ScanViewModel scanViewModel = new ScanViewModel();
                //        LscanviewModel.Add(scanViewModel);
                //    }
                //}
               
                //if (Lassort.Count() > 0)
                //{
                //    foreach (assort assort in Lassort)
                //    {
                //        AssortViewModelcs assortViewModel = new AssortViewModelcs(assort,import.Id);
                //        LassortViewModel.Add(assortViewModel);
                //    }
                //}
                
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
            Import import = this.db.Import.OrderByDescending(d => d.Date).Where(c => c.Type == TypeImport.Scan).OrderByDescending(d=>d.Date).First();
            using (var context = new ScanDbContext())
            {

                 var ImportId = new SqlParameter("@ImportId", import.Id);

                LresultScan = context.Database
                    .SqlQuery<ResultScan>("GETScanResult @ImportId", ImportId)
                    .ToList();
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
            //foreach (var item in proc)
            //{
            //    SimulationViewModel sm = new SimulationViewModel();
            //    sm.Ean13 = item.Key;
            //    sm.label = item.First().Label;
            //    sm.StockReel = item.First().Quantity;
            //    sm.Price = item.First().Price;
            //    sm.OrderedQte = 0;
            //    sm.QteSales = 0;
            //    sm.M1 = 0;
            //    sm.M2 = 0;
            //    sm.M3 = 0;
            //    LSimulationViewModel.Add(sm);

            //}



            //List<assort> proc = this.db.assort.ToList();
            return PartialView("_simulator", LSimulationViewModel);
        }
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