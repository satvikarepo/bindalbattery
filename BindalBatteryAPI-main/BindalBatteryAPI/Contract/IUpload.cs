using System.Data;


namespace BindalBatteryAPI.Contract
{
    public interface IUpload
    {
        public DataTable BindExcelData(string filePath);
    }
}
