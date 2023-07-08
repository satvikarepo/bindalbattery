using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using System;
using TanvirArjel.EFCore.GenericRepository;

namespace BindalBatteryAPI.Services
{
    public class User : IUser
    {

        private readonly IRepository _repository;
        private readonly ILoggerManager _logger;

        public User(IRepository repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;   
        }

        public async void DeleteById(Applicationuser applicationuser)
        {
            _repository.Remove(applicationuser);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Applicationuser>> GetAllUsers()
        {
            return await _repository.GetListAsync<Applicationuser>();
        }

        public async Task<Applicationuser> GetUsersById(int applicationuserId)
        {
            try
            {
                  return await _repository.GetByIdAsync<Applicationuser>(applicationuserId);
            }
            catch(Exception ex) {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }

        public async Task<Applicationuser> Login(long? mobileno, string passcode)
        {
            try
            {
                var objuser =  await _repository.GetListAsync<Applicationuser>(e => e.Mobile.Equals(mobileno) 
                && e.Passcode.Equals(passcode));

                if (objuser != null && objuser.Count > 0)
                {
                    return objuser[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }

        public async Task<Applicationuser?> GetUserByMobile(long? mobileno)
        {
            try
            {
                var objuser = await _repository.GetListAsync<Applicationuser>(e => e.Mobile.Equals(mobileno));
                if (objuser != null && objuser.Count > 0)
                {
                    return objuser[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }

        public async Task<int> InsertUser(Applicationuser applicationuser)
        {
            try
            {
                

                await _repository.AddAsync<Applicationuser>(applicationuser);
                await _repository.SaveChangesAsync();

                return applicationuser.ApplicationUserId;
            }catch(Exception ex)
            {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }
        public async Task<int> UpdatePassword(Applicationuser applicationuser)
        {
            try
            {
                var objuser = await _repository.GetAsync<Applicationuser>(e => e.Mobile.Equals(applicationuser.Mobile));
                if (objuser == null)
                {
                    return -1;
                }
                else if(objuser.Passcode != applicationuser.Passcode)
                {
                    return -2;
                }
                objuser.Passcode = applicationuser.Passcode;
                await _repository.SaveChangesAsync();

                return applicationuser.ApplicationUserId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }

        public async Task<int> UpdateUser(Applicationuser applicationuser)
        {
            try
            {
                var objuser = await _repository.GetAsync<Applicationuser>(e => e.Mobile.Equals(applicationuser.Mobile));
                if (objuser == null)
                {
                    return -1;
                }
                objuser.Passcode = applicationuser.Passcode;
                objuser.IsApproved = applicationuser.IsApproved;
                await _repository.SaveChangesAsync();

                return objuser.ApplicationUserId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Here is error message from the controller." + ex.StackTrace);
                _logger.LogError("Here is error message from the controller." + ex.InnerException);
                throw ex;
            }
        }
    }
}
