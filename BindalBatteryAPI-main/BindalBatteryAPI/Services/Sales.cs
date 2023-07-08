using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class Sales : ISales
    {

        private readonly IRepository _repository;

        public Sales(IRepository repository)
        {
            _repository = repository;
        }

        public async void DeleteSalesById(Sale sales)
        {
            _repository.Remove(sales);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Sale>> GetAllSales()
        {
            Specification<Sale> specification = new Specification<Sale>();
            specification.Includes = ep => ep.Include(e => e.Party).Include(e => e.Product);
            return await _repository.GetListAsync<Sale>(specification);
        }

        public async Task<Sale> GetSalesById(int salesId)
        {
            return await _repository.GetByIdAsync<Sale>(salesId);
        }

        public async Task<int> InsertSales(Sale sales)
        {
            await _repository.AddAsync<Sale>(sales);
            await _repository.SaveChangesAsync();

            return sales.Id;
        }

        public Sale UpdateSalesById(Sale sales)
        {
            throw new NotImplementedException();
        }
    }
}
