using BindalBatteryAPI.Contract;
using BindalBatteryAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        
   
        public UserController(IUser _user)
        {
            this.user = _user;            
            
        }

        [HttpGet]
        [ActionName("GetAllUser")]
        public async Task<ActionResult> GetAllUser()
        {
            try
            {
                List<VwApplicationuser> Listuser = new List<VwApplicationuser>();
                var objapplicationuser = await user.GetAllUsers();

                foreach (var item in objapplicationuser)
                {
                    VwApplicationuser vwApplicationuser = new VwApplicationuser();
                    vwApplicationuser.PartyName = item.PartyName;
                    vwApplicationuser.ApplicationUserId = item.ApplicationUserId;
                    vwApplicationuser.role = item.role;
                    vwApplicationuser.IsApproved = item.IsApproved;
                    vwApplicationuser.Place=item.Place;
                    vwApplicationuser.Address = item.Address;
                    vwApplicationuser.Mobile = item.Mobile;
                    vwApplicationuser.UserType= item.UserType;
                    vwApplicationuser.ApprovedDesc = item.IsApproved == 1 ? "Approved" : "Not approved";

                    Listuser.Add(vwApplicationuser);
                }
                return Ok(Listuser);
            }
            catch (Exception ex)
            {
                return Ok("Request not completed successfull.");
            }
        }

        [HttpPost]
        [ActionName("GetUserById")]
        public async Task<ActionResult> GetUserById([FromBody] VwApplicationuser applicationuser)
        {
            try
            {
                var objapplicationuser = await user.GetUsersById(applicationuser.ApplicationUserId);
                VwApplicationuser vwApplicationuser = new VwApplicationuser();
                vwApplicationuser.PartyName = objapplicationuser.PartyName;
                vwApplicationuser.ApplicationUserId = objapplicationuser.ApplicationUserId;
                vwApplicationuser.role = objapplicationuser.role;
                return Ok(vwApplicationuser);
            }
            catch (Exception ex) {
                return Ok("Request not completed successfull.");
            }
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<ActionResult> Login([FromBody]VwApplicationuser applicationuser)
        {
            try
            {
                var objapplicationuser =  await user.Login(applicationuser.Mobile, applicationuser.Passcode);

                if (objapplicationuser == null)
                {
                    return Unauthorized("Invalid username/Password.");
                }

                return objapplicationuser.IsApproved==1 ? 
                    Ok(objapplicationuser) :
                    Unauthorized("You are not approved to login. Please contact your admin.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong. Please try after sometime");
            }
        }

        [HttpPost]
        [ActionName("ResgisterUser")]
        public async Task<ActionResult> InsertUser(VwApplicationuser applicationuser)
        {
            try
            {
                var existingUser = await user.GetUserByMobile(applicationuser.Mobile);
                if (existingUser != null)
                {
                    return Conflict(existingUser);
                }

                dbcontext.Applicationuser applicationuser1=  new dbcontext.Applicationuser();
                applicationuser1.PartyName = applicationuser.PartyName;
                applicationuser1.Passcode= applicationuser.Passcode;
                applicationuser1.Place  = applicationuser.Place;
                applicationuser1.Mobile= applicationuser.Mobile;
                applicationuser1.CreateDate = applicationuser.CreateDate;
                applicationuser1.IsActive   = applicationuser.IsActive;
                applicationuser1.role = applicationuser.role;
                applicationuser1.Address = applicationuser.Address;
                applicationuser1.UserType = applicationuser.UserType;
                applicationuser1.IsApproved = 0;

                var applicationuserid = await user.InsertUser(applicationuser1);
                if (applicationuserid > 0)
                {
                    return Ok("User added successfully");
                }
                throw new Exception("Error occured at server");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at server");
            }
        }
       
        [HttpPost]
        [ActionName("ChangePassword")]
        public async Task<ActionResult> ChangePassword(VwApplicationuser applicationuser)
        {
            try
            {
                if(string.IsNullOrEmpty(applicationuser.Passcode) || applicationuser.Mobile == null)
                {
                    return BadRequest("Mobile and PassCode is required but not supplied.");
                }

                dbcontext.Applicationuser applicationuser1 = new()
                {
                    Passcode = applicationuser.Passcode,
                    Mobile = applicationuser.Mobile
                };
                var applicationuserid = await user.UpdatePassword(applicationuser1);
                if(applicationuserid ==-1)
                {
                    return NotFound("User not found.");
                }
                if (applicationuserid == -2)
                {
                    return BadRequest("Incorrect password.");
                }
                if (applicationuserid > 0)
                {
                    return Ok("Password changed successfully");
                }
                throw new Exception("Error occured at server");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at server");
            }
        }

        [HttpPatch]
        [ActionName("ApproveUser")]
        public async Task<ActionResult> ApproveUser(VwApplicationuser applicationuser)
        {
            try
            {
                if (string.IsNullOrEmpty(applicationuser.Passcode) || applicationuser.Mobile == null)
                {
                    return BadRequest("Mobile and PassCode is required but not supplied.");
                }

                dbcontext.Applicationuser applicationuser1 = new()
                {
                    Passcode = applicationuser.Passcode,
                    Mobile = applicationuser.Mobile,
                    IsApproved=1
                };
                var resp = await user.UpdateUser(applicationuser1);
                if (resp == -1)
                {
                    return NotFound("User not found.");
                }
                if (resp > 0)
                {
                    return Ok("User approved successfully");
                }
                throw new Exception("Error occured at server");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at server");
            }
        }

        [HttpPatch]
        [ActionName("LockUser")]
        public async Task<ActionResult> LockUser(VwApplicationuser applicationuser)
        {
            try
            {
                if (applicationuser.Mobile == null)
                {
                    return BadRequest("Mobile and PassCode is required but not supplied.");
                }

                dbcontext.Applicationuser applicationuser1 = new()
                {
                    Passcode = string.Empty,
                    Mobile = applicationuser.Mobile,
                    IsApproved = 0
                };
                var resp = await user.UpdateUser(applicationuser1);
                if (resp == -1)
                {
                    return NotFound("User not found.");
                }
                if (resp > 0)
                {
                    return Ok("User locked successfully");
                }
                throw new Exception("Error occured at server");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured at server");
            }
        }
    }
}
