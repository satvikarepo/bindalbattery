using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using Microsoft.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class Warranty : IWarranty
    {

        private readonly IRepository _repository;

        public Warranty(IRepository repository)
        {
            _repository = repository;
        }

        public async void DeleteWarantymasterById(Warantymaster Warantymaster)
        {
            _repository.Remove(Warantymaster);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Warantymaster>> GetAllWarantymaster()
        {
            Specification<Warantymaster> specification = new Specification<Warantymaster>();

            specification.Includes = ep => ep.Include(e => e.PartyMatser).Include(e => e.ProductMaster);           
            return await _repository.GetListAsync<Warantymaster>(specification);
        }

        public async Task<Warantymaster> GetWarantymasterById(int warrantyId)
        {
            return await _repository.GetByIdAsync<Warantymaster>(warrantyId);
        }

        public async Task<int> InsertWarantymaster(Warantymaster Warantymaster)
        {
            await _repository.AddAsync<Warantymaster>(Warantymaster);
            await _repository.SaveChangesAsync();

            return Warantymaster.WarrantyId;
        }

        public Warantymaster UpdateWarantymasterById(Warantymaster Warantymaster)
        {
            throw new NotImplementedException();
        }
    }
}
