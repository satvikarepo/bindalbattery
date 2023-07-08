using BindalBatteryAPI.dbcontext;
using System.Threading.Tasks;

namespace BindalBatteryAPI.Contract
{
    public interface IReplace
    {
        public Task<List<Replace>> GetAllReplaceItem();
        public Task<Replace> GetReplaceItemId(int replaceId);        
        public void DeleteReplaceId(Replace replace);
        public Task<int> InsertReplaceItem(Replace replace);
        public Task<List<dbcontext.Sale>> GetBatterySrNo(string productid, string batterySerno);
    }
}
