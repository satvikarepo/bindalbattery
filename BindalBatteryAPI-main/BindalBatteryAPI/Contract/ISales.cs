using BindalBatteryAPI.dbcontext;

namespace BindalBatteryAPI.Contract
{
    public interface ISales
    {
        public Task<List<Sale>> GetAllSales();
        public Task<Sale> GetSalesById(int salesId);

        public Sale UpdateSalesById(Sale sales);

        public void DeleteSalesById(Sale sales);

        public Task<int> InsertSales(Sale sales);

    }
}
