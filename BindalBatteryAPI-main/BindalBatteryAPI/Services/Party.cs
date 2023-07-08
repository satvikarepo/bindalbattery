using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using MySqlX.XDevAPI.Common;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class Party : IParty
    {
        private readonly IRepository _repository;

        public Party(IRepository repository)
        {
            _repository = repository;
        }
    
        public async void DeletePartyMasterById(Partymaster partyMaster)
        {
            _repository.Remove(partyMaster);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Partymaster>> GetAllPartyMaster()
        {
           return await _repository.GetListAsync<Partymaster>();
        }

        public async Task<List<Partymaster>> AutoCompletePartyMaster(string subpartname)
        {
          List<Partymaster> listpartymaster = await _repository.GetListAsync<Partymaster>();
          // var finalList1 = listpartymaster.Where(x => x.Partyname.StartsWith(subpartname, StringComparison.OrdinalIgnoreCase)).ToList();
           var finalList = listpartymaster.Where(x => x.Partyname.StartsWith(subpartname, StringComparison.OrdinalIgnoreCase)).Select(x => new Partymaster
            {
               Partyname= x.Partyname == null ? "": x.Partyname,
              PartyMasterId =   x.PartyMasterId == null ? 0 : x.PartyMasterId
            }).ToList(); ;

            return finalList; 
          
        }
        public async Task<Partymaster> GetPartyMasterById(int partyId)
        {
            return await _repository.GetByIdAsync<Partymaster>(partyId);
        }

        public async Task<int> InsertPartyMaster(Partymaster partyMaster)
        {
            await _repository.AddAsync<Partymaster>(partyMaster);
            await _repository.SaveChangesAsync();

            return partyMaster.PartyMasterId;
        }

        public Partymaster UpdatePartyMasterById(Partymaster productmaster)
        {
            throw new NotImplementedException();
        }
    }
}
