using BindalBatteryAPI.dbcontext;

namespace BindalBatteryAPI.Contract
{
    public interface IProduct
    {
        public Task<List<Productmaster>> GetAllProductMaster();

        public Task<List<Productmaster>> GetAutocomleteProduct(string subprod);

        public Task<List<Productmaster>> GetAllProductMasterByBrnadName(string subprod, string batteryType);
        
        public Task<Productmaster> GetProductMasterById(int productId);             

        public void DeleteProductMasterById(Productmaster productmaster);

        public Task<int> InsertProductMaster(Productmaster productmaster);

    }
}
