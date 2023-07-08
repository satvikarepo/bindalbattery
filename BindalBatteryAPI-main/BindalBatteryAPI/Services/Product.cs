using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class Product : IProduct
    {
        private readonly IRepository _repository;

        public Product(IRepository repository)
        {
            _repository = repository;
        }

        public async void  DeleteProductMasterById(Productmaster product)
        {
            _repository.Remove(product);
            await _repository.SaveChangesAsync();

        }

         public async Task<List<Productmaster>> GetAllProductMasterByBrnadName(string subproduct, string batteryType)
        {

            Specification<Productmaster> specification = new Specification<Productmaster>();
            specification.Conditions.Add(e => e.BrandName.Equals(subproduct, StringComparison.OrdinalIgnoreCase));
            specification.Conditions.Add(e => e.BatteryType.Contains(batteryType, StringComparison.OrdinalIgnoreCase));
            List<Productmaster> listproductmaster = await _repository.GetListAsync<Productmaster>(specification);


            //List<Productmaster> listproductmaster = await _repository.GetListAsync<Productmaster>(e=>e.BrandName.Equals(batteryType, StringComparison.OrdinalIgnoreCase)
            //    && e.BatteryType.Contains(batteryType, StringComparison.OrdinalIgnoreCase)); 
            return listproductmaster;
        }

        public async Task<List<Productmaster>> GetAutocomleteProduct(string subproduct)
        {
            List<Productmaster> listproductmaster = await _repository.GetListAsync<Productmaster>();
            
            var finalList = listproductmaster.Where(x => x.BatteryType.Contains(subproduct, StringComparison.OrdinalIgnoreCase)).Select(x => new Productmaster
                   {
                BrandName = x.BatteryType,
                ProductId = x.ProductId
            }).ToList(); 

            return finalList;

        }

        public async Task<List<Productmaster>> GetAllProductMaster()
        {
           return await _repository.GetListAsync<Productmaster>();
        }

        public async Task<Productmaster> GetProductMasterById(int productId)
        {
            return await _repository.GetByIdAsync<Productmaster>(productId);
        }

        public async Task<int> InsertProductMaster(Productmaster productmaster)
        {
            await _repository.AddAsync<Productmaster>(productmaster);
            await _repository.SaveChangesAsync();
            return productmaster.ProductId;
        }

        public Productmaster UpdateProductMasterById(Productmaster productmaster)
        {
            throw new NotImplementedException();
        }
    }
}
