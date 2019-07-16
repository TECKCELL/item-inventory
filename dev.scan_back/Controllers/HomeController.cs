using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Diagnostics;
using dev.scan_back.Tools;
using dev.scan_back.Models;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace dev.scan_back.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private ScanDbContext db = new ScanDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadFiles()
        {
            return View();
        }
        public ActionResult UploadFilesClient()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AdminImmo(OperationViewModel operationViewModel)
        {
             
            return View("Operation", operationViewModel);
        }
        
        public ActionResult AdminImmo()
        {
            OperationViewModel operationViewModel = new OperationViewModel();
            return View("Operation",operationViewModel);
        }
        [HttpPost]
        public  async Task<ActionResult> Create(OperationViewModel operationViewModel)
        {
            await importImmo(operationViewModel.files[0]);
            using (var context = new ScanDbContext())
            {

                var beginDate = new SqlParameter("@beginDate", operationViewModel.beginDate);
                var endDate = new SqlParameter("@endDate", operationViewModel.endDate);

               var operations = await Task.Run( ()=>context.Database
                   .SqlQuery<Operation>("ImportImmo @beginDate, @endDate", beginDate, endDate).ToList());
                    
            }
            List<OperationViewModel> operationViewModels = new List<OperationViewModel>();
            foreach (var operation in db.Operation.ToList().OrderByDescending(o => o.RecordDate))
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
            return await Task.Run(() => RedirectToAction("DisplayOperation", "Scan"));
        }

        private async Task importImmo(HttpPostedFileBase file)
        {

            await UploadImmoFile(file);
        }

        private Task UploadImmoFile(HttpPostedFileBase file)
        {

            
                var InputFileName = Path.GetFileName(file.FileName);
                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                //Save file to server folder  
                file.SaveAs(ServerSavePath);
                //assigning file uploaded status to ViewBag for showing message to user.  

            
            return Task.CompletedTask;
        }
       
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {
            //DataTable dt = ExcelReader.excelToTable(@"C:\\Tableau immo sda SEPT 18", "SEPT 18");
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = "import réussi"; //files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;
        
            processInfo = new ProcessStartInfo("Sqlcmd.exe", "-i C:\\Users\\teki\\source\\repos\\dev.scan_back\\dev.scan_back\\UploadedFiles\\SCAN_IMPORT.sql");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;
            return View();
        }
        [HttpPost]
        public ActionResult UploadFilesClient(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = "import réussi"; //files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("Sqlcmd.exe", "-i C:\\Users\\teki\\source\\repos\\dev.scan_back\\dev.scan_back\\UploadedFiles\\Client_Import.sql ");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;
            return View();
        }
    }
}
