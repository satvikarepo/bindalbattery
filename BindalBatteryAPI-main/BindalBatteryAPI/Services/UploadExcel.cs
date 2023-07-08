using MySql.Data.MySqlClient;
using System.Data;
using System.Web;
using System.IO;
using ClosedXML.Excel;
using BindalBatteryAPI.Contract;

namespace BindalBatteryAPI.Services
{
    public class UploadExcel : IUpload
    {
        public DataTable BindExcelData(string filePath)
        {
            try
            {
                //string filePath = "";
                IXLWorksheet workSheet = null;
                DataTable dt = new DataTable("ExcelData");

                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    workSheet = workBook.Worksheet(1);

                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                }
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                return dt;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
           
        }

    }
}
