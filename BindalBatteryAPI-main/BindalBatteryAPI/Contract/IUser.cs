using BindalBatteryAPI.dbcontext;

namespace BindalBatteryAPI.Contract
{
    public interface IUser
    {
        public Task<List<Applicationuser>> GetAllUsers();

        public Task<Applicationuser> GetUsersById(int applicationuserId);

        public Task<Applicationuser> Login(long? mobilenno, string passcode);

        public void DeleteById(Applicationuser applicationuser);

        public Task<int> InsertUser(Applicationuser applicationuser);
        Task<Applicationuser?> GetUserByMobile(long? mobileno);
        Task<int> UpdatePassword(Applicationuser applicationuser);

        Task<int> UpdateUser(Applicationuser applicationuser);

    }
}
