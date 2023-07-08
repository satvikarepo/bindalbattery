using BindalBatteryAPI.dbcontext;

namespace BindalBatteryAPI.Contract
{
    public interface IWarranty
    {
        public Task<List<Warantymaster>> GetAllWarantymaster();
        public Task<Warantymaster> GetWarantymasterById(int warrantyId);

        public Warantymaster UpdateWarantymasterById(Warantymaster Warantymaster);

        public void DeleteWarantymasterById(Warantymaster Warantymaster);

        public Task<int> InsertWarantymaster(Warantymaster Warantymaster);
    }
}
