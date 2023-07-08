using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using BindalBatteryAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkcs;

namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IParty party;
   
        public PartyController(IParty party)
        {
            this.party = party;           
        }

        [HttpGet]
        [ActionName("GetAllParty")]
        public async Task<ActionResult> GetAllParty()
        {
            try
            {
                var objParty = await party.GetAllPartyMaster();
                if (objParty != null && objParty.Count > 0)
                {
                    List<vwPartyMaster> list = new List<vwPartyMaster>();
                    foreach (Partymaster obj in objParty)
                    {
                        vwPartyMaster vwPartyMaster = new vwPartyMaster();
                        vwPartyMaster.PartyMasterId = obj.PartyMasterId;
                        vwPartyMaster.GracePeriod = obj.GracePeriod;
                        vwPartyMaster.Partyname = obj.Partyname;
                        vwPartyMaster.Mobile = obj.Mobile;
                        vwPartyMaster.place = obj.place;
                        list.Add(vwPartyMaster);
                    }
                    return Ok(list);
                }
                else
                {
                    return Ok("No Result found");
                }

            }
            catch (Exception ex)
            {

                return Ok("Request do not completed successfully.");
            }
        }


        [HttpGet]
        [ActionName("GetAllPartyName")]
        public async Task<ActionResult> GetAllPartyName()
        {
            try
            {
                var objParty = await party.GetAllPartyMaster();
                if (objParty != null && objParty.Count > 0)
                {
                    List<vwPartyMasterName> list = new List<vwPartyMasterName>();
                    foreach (Partymaster obj in objParty)
                    {
                        vwPartyMasterName vwPartyMaster = new vwPartyMasterName();
                    
                        vwPartyMaster.Partyname = obj.Partyname;
                 
                        list.Add(vwPartyMaster);
                    }
                    return Ok(list);
                }
                else
                {
                    return Ok("No Result found");
                }

            }
            catch (Exception ex)
            {

                return Ok("Request do not completed successfully.");
            }
        }

        [HttpPost]
        [ActionName("AutoCompleteParty")]
        public async Task<ActionResult> AutoCompleteParty(string vwparty)
        {
            try
            {
                var objParty = await party.AutoCompletePartyMaster(vwparty);
                if (objParty != null && objParty.Count > 0)
                {
                    List<vwPartyMasterName> list = new List<vwPartyMasterName>();
                    foreach (Partymaster obj in objParty)
                    {
                        vwPartyMasterName vwPartyMaster = new vwPartyMasterName();

                        vwPartyMaster.Partyname = obj.Partyname;
                        vwPartyMaster.PartyMasterId = obj.PartyMasterId;

                        list.Add(vwPartyMaster);
                    }
                    return Ok(list);
                }
                else
                {
                    return Ok("No Result found");
                }

            }
            catch (Exception ex)
            {

                return Ok("Request do not completed successfully.");
            }
        }


        [HttpPost]
        [ActionName("InsertParty")]
        public async Task<ActionResult> InsertParty([FromBody] vwPartyMaster vwPartyMaster)
        {
            try
            {
                Partymaster partymaster = new Partymaster();
                partymaster.Partyname = vwPartyMaster.Partyname;
                partymaster.Mobile = vwPartyMaster.Mobile;
                partymaster.GracePeriod = vwPartyMaster.GracePeriod;
                partymaster.place = vwPartyMaster.place;
                partymaster.CreateDate = DateTime.Now;
                partymaster.CreatedBy = "System";
                var result = await party.InsertPartyMaster(partymaster);
                if (result > 0)
                    return Ok("Party Inserted Successfully");
                else
                    return Ok("Party is not Inserted Successfully");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }

        }
        [HttpPost]
        [ActionName("DeleteParty")]
        public async Task<ActionResult> DeletParty([FromBody] vwPartyMaster vwPartyMaster)
        {
            try
            {   Partymaster partymaster = new Partymaster();
                partymaster.PartyMasterId = vwPartyMaster.PartyMasterId;
                party.DeletePartyMasterById(partymaster);
                return Ok("Party removed Successfully");                
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }

        }
    }
}
