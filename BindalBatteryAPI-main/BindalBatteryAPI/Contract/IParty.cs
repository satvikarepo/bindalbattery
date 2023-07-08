using BindalBatteryAPI.dbcontext;

namespace BindalBatteryAPI.Contract
{
    public interface IParty
    {

        public Task<List<Partymaster>> GetAllPartyMaster();

        public Task<List<Partymaster>> AutoCompletePartyMaster(string subpartname); 
        public Task<Partymaster> GetPartyMasterById(int partyId);

        public Partymaster UpdatePartyMasterById(Partymaster productmaster);

        public void DeletePartyMasterById(Partymaster productmaster);

        public Task <int> InsertPartyMaster(Partymaster productmaster);
    }
}
