using BindalBatteryAPI.Contract;
using BindalBatteryAPI.Model;
using BindalBatteryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BindalBatteryAPI.dbcontext;
using Org.BouncyCastle.Math.EC;
using MySqlX.XDevAPI.Common;

namespace BindalBatteryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WarrantyController : ControllerBase
    {
        
        private readonly IWarranty warranty;

        public WarrantyController(IWarranty warranty)
        {
            this.warranty = warranty;         
        }

        [HttpGet]
        [ActionName("GetAllWarranty")]
        public async Task<ActionResult> GetAllWarranty()
        {
            Thread.Sleep(5000);
            var warrantyObj = await warranty.GetAllWarantymaster();
            List<vwWarranty> listwarramty = new List<vwWarranty>();
            foreach(Warantymaster warranty in warrantyObj)
                {
                vwWarranty obj = new vwWarranty();
                obj.GracePeriod = warranty.GracePeriod;
                obj.PartyName = warranty.PartyMatser.Partyname;
                obj.ProductType = warranty.ProductMaster.BatteryType;
                obj.WarrantyId= warranty.WarrantyId;
                obj.PartyMatserId = warranty.PartyMatserId;
                obj.ProductId= warranty.ProductId;
                listwarramty.Add(obj); 
            }
            return Ok(listwarramty);
        }
        [HttpPost]
        [ActionName("InsertWarranty")]
        public async Task<ActionResult> InsertWarranty([FromBody] vwWarranty vwWarranty)
        {
            try
            {
                Warantymaster warantymaster = new Warantymaster();
                warantymaster.ProductId = vwWarranty.ProductId;
                warantymaster.PartyMatserId = vwWarranty.PartyMatserId;
                warantymaster.GracePeriod = vwWarranty.GracePeriod;
                var result = await warranty.InsertWarantymaster(warantymaster);
                if (result > 0)
                    return Ok("Warranty Inserted Successfully");
                else
                    return Ok("Warranty is not Inserted Successfully");

            }catch(Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }
        }
        [HttpPost]
        [ActionName("DeleteWarranty")]
        public async Task<ActionResult> DeleteWarranty([FromBody] vwWarranty vwwarrantymaster)
        {
            try
            {
                Warantymaster warantymaster = new Warantymaster();
                warantymaster.WarrantyId = vwwarrantymaster.WarrantyId;
                warranty.DeleteWarantymasterById(warantymaster);
                return Ok("Warranty removed Successfully");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }

        }

    }
}
