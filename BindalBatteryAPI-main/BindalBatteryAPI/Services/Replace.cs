using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class ReplaceSvc : IReplace
    {

        private readonly IRepository _repository;

        public ReplaceSvc(IRepository repository)
        {
            _repository = repository;
        }

        public async void DeleteReplaceId(dbcontext.Replace replace)
        {
            _repository.Remove(replace);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<dbcontext.Replace>> GetAllReplaceItem()
        {
            Specification<Replace> specification = new Specification<Replace>();
            specification.Includes = ep => ep.Include(e => e.Party).Include(e => e.Product).Include(e=>e.Sale);
            return await _repository.GetListAsync<dbcontext.Replace>(specification);
        }

        public async Task<dbcontext.Replace> GetReplaceItemId(int replaceId)
        {
            return await _repository.GetByIdAsync<dbcontext.Replace>(replaceId);
        }

        public async Task<int> InsertReplaceItem(dbcontext.Replace replace)
        {
            await _repository.AddAsync<dbcontext.Replace>(replace);
            await _repository.SaveChangesAsync();
            return replace.ReplaceId;
        }

        public async Task<List<dbcontext.Sale>> GetBatterySrNo(string productid, string batterySerno)
        {
            Specification<Productmaster> specification = new Specification<Productmaster>();
            specification.Conditions.Add(e=>e.ProductId == int.Parse( productid));
            var productList = await _repository.GetListAsync<Productmaster>(specification);
            List<Sale> sales = new List<Sale>();
            foreach(var productmaster in productList)
            {
                Specification<Sale> salespec = new Specification<Sale>();
                salespec.Conditions.Add(e=>e.SaleDate < DateTime.Now.AddDays(Convert.ToDouble(productmaster.GauranteePeriod)));
                var listsale = await _repository.GetListAsync<dbcontext.Sale>(salespec);
                sales.AddRange(listsale);
            }
            return sales;

        }
    }
}
