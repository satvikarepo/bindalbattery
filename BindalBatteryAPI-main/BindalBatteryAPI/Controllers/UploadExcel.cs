using BindalBatteryAPI.Contract;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Data;
//using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Path = System.IO.Path;
using MySql.Data.MySqlClient;


namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadExcel : ControllerBase
    {
        private readonly IUpload upload;
       // private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
        private readonly ILoggerManager loggerManager;
        public UploadExcel(IUpload _upload, IConfiguration _configuration, ILoggerManager _loggerManager)
        {
            this.upload = _upload;
           // this.webHostEnvironment = webHostEnvironment;
            this.configuration = _configuration;
            this.loggerManager = _loggerManager;
        }

        [HttpPost]
        [ActionName("UploadExcel")]
        public IActionResult OnPostMyUploader(IFormFile MyUploader)
        {
            try
            {
                if (MyUploader != null)
                {

                    //string uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "mediaUpload");
                    loggerManager.LogInfo("Inside file uploader : ");
                    string uploadsFolder = "mediaUpload";
                    if (!Directory.Exists(uploadsFolder))
                    {
                        loggerManager.LogInfo("Inside create directory");
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };
                    string dataFileName = Path.GetFileName(MyUploader.FileName);

                    string extension = Path.GetExtension(dataFileName);

                    if (!allowedExtsnions.Contains(extension))
                        throw new Exception("Sorry! This file is not allowed,  make sure that file having extension as either.xls or.xlsx is uploaded.");

                    string filePath = Path.Combine(uploadsFolder, MyUploader.FileName);
                    loggerManager.LogInfo("Inside file uploader : " + filePath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        MyUploader.CopyTo(fileStream);
                    }
                    DataTable dt = upload.BindExcelData(filePath);
                    loggerManager.LogInfo("Data Table Created");

                    loggerManager.LogInfo("Before databalse file uploader : ");
                    string constr = configuration.GetConnectionString("ProdbindalConnStr");
                    MySqlConnection con = new MySqlConnection(constr);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_InsertSalesStg", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.UpdatedRowSource = UpdateRowSource.None;

                    cmd.Parameters.Add("?BrandName", MySqlDbType.String).SourceColumn = "BrandName";
                    cmd.Parameters.Add("?BatteryType", MySqlDbType.String).SourceColumn = "BatteryType";
                    cmd.Parameters.Add("?PartyId", MySqlDbType.String).SourceColumn = "PartyId";                    
                    cmd.Parameters.Add("?BatterySrNo", MySqlDbType.String).SourceColumn = "BatterySrNo";
                    cmd.Parameters.Add("?SaleDate", MySqlDbType.String).SourceColumn = "SaleDate";



                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.InsertCommand = cmd;
                    da.UpdateBatchSize = 500;
                    int records = da.Update(dt);
                    con.Close();

                    return new ObjectResult(new { status = "success" });
                }
                return new ObjectResult(new { status = "fail" });
            }catch(Exception ex)
            {
                loggerManager.LogInfo("Inside catch");
                loggerManager.LogInfo(ex.Message);
                loggerManager.LogInfo(ex.ToString());
                throw ;
            }

        }

    }
}
