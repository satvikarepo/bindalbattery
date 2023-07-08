using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using BindalBatteryAPI.Model;
using BindalBatteryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly ISales sales;
    
        public SalesController(ISales _sales)
        {
            this.sales = _sales;         
        }

        [HttpGet]
        [ActionName("GetAllSales")]
        public async Task<ActionResult> GetAllSales()
        {
            Thread.Sleep(5000);
            var listsales = await sales.GetAllSales();
            List<vwSales> listvwsales = new List<vwSales>();
            foreach (Sale sale in listsales)
            {
                vwSales objsales = new vwSales();
                objsales.Id= sale.Id;
                objsales.PartyId= sale.Party.PartyMasterId;
                objsales.ProductId = sale.Product.ProductId;
                objsales.PartyName = sale.Party.Partyname;
                objsales.BrandName = sale.Product.BrandName;
                objsales.BatteryType= sale.Product.BatteryType;
                objsales.SaleDate = sale.SaleDate.ToString("dd/MM/yyyy");
                objsales.BatterySrNo = sale.BatterySrNo;
                listvwsales.Add(objsales);
            }
            return Ok(listvwsales);
        }

        [HttpPost]
        [ActionName("InsertSales")]
        public async Task<ActionResult> InsertSales([FromBody] vwSales vwsales)
        {
            try
            {
                Sale sale = new Sale();
                sale.PartyId = vwsales.PartyId;
                sale.BatterySrNo= vwsales.BatterySrNo;
                sale.SaleDate = Convert.ToDateTime(vwsales.SaleDate);
                sale.ProductId = vwsales.ProductId;
                sale.CreateDate = DateTime.Now.Date;
                sale.CreatedBy = "System";
                var result = await sales.InsertSales(sale);
                if (result > 0)
                    return Ok("Sale Inserted Successfully");
                else
                    return Ok("Sale is not Inserted Successfully");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }
        }

        [HttpPost]
        [ActionName("DeleteSale")]
        public  async Task<ActionResult> DeleteSale([FromBody] vwSalesSub vwsales)
        {
            try
            {
                Sale sale = new Sale();
                sale.Id = vwsales.Id;
                sales.DeleteSalesById(sale);
                return Ok("Item deleted successfully!");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }
        }
    }
}
