using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using BindalBatteryAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReplaceController : ControllerBase
    {
        
        private readonly IReplace svcreplace;

        public ReplaceController(IReplace replace)
        {
            this.svcreplace = replace;             
        }

        [HttpGet]
        [ActionName("GetAllReplace")]
        public async Task<ActionResult> GetAllReplace()
        {
           
            var listreplace = await svcreplace.GetAllReplaceItem();
            List<vwReplace> listvwreplace = new List<vwReplace>();
            foreach (Replace objreplace in listreplace)
            {
                vwReplace objvwreplace = new vwReplace();
                objvwreplace.ReplacementDate = objreplace.ReplacementDate.ToString("dd/MM/yyyy");
                objvwreplace.ReplaceId = objreplace.ReplaceId;
                objvwreplace.PartyName = objreplace.Party.Partyname;
                objvwreplace.BatterType = objreplace.Product.BatteryType;
                objvwreplace.SaleDate = objreplace.Sale.SaleDate.ToString("dd/MM/yyyy");
                objvwreplace.NewSrNo = objreplace.NewSrNo;
                objvwreplace.OkdSrNo = objreplace.Sale.BatterySrNo;
                listvwreplace.Add(objvwreplace);
            }
            return Ok(listvwreplace);
        }

        [HttpPost]
        [ActionName("InsertReplaceItem")]
        public async Task<ActionResult> InsertReplaceItem([FromBody] vwReplace vwReplace )
        {
            try
            {
                Replace replace = new Replace();
                replace.ReplacementDate = Convert.ToDateTime(vwReplace.ReplacementDate);
                replace.SaleId = vwReplace.SaleId;
                replace.NewSrNo = vwReplace.NewSrNo;
                replace.PartyId= vwReplace.PartyId;
                replace.ProductId = vwReplace.Productid;
                await svcreplace.InsertReplaceItem(replace);
                return Ok("Item added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest("Request do not complete!");
            }

        }

        [HttpGet]
        [ActionName("GetBatterySerNo")]
        public async Task<ActionResult> GetBatterySerNo(string productid , string batterySerno)
        {
            var listsales = await svcreplace.GetBatterySrNo(productid, batterySerno);
            List<vwSalesSub> listsubsales = new List<vwSalesSub>();
            foreach (Sale sale in listsales)
            {
                vwSalesSub vwSalesSub = new vwSalesSub();
                vwSalesSub.Id = sale.Id;
                vwSalesSub.SaleDate = sale.SaleDate.ToString("dd/MM/yyyy");
                vwSalesSub.BatterSrNo = sale.BatterySrNo;
                listsubsales.Add(vwSalesSub);
            }
            return Ok(listsubsales);
        }

        [HttpPost]
        [ActionName("DeleteReplaceItem")]
        public   async Task<ActionResult> DeleteReplaceItem([FromBody] vwReplaceSub vwReplace)
        {
            try
            { 
                Replace replace = new Replace();
                replace.ReplaceId = vwReplace.ReplaceId;
                 svcreplace.DeleteReplaceId(replace);
                return Ok("Item deleted successfull!");
            }
            catch (Exception ex) {
                return BadRequest("Request do not complete!");
            }
            
        }

    }
}
