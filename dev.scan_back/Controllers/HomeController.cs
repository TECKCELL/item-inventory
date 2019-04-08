using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Diagnostics;
using dev.scan_back.Tools;
using System.Data;

namespace dev.scan_back.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
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
