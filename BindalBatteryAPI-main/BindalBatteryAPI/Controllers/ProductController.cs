using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using BindalBatteryAPI.Model;
using BindalBatteryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Data.SqlTypes;
using System.IO;

namespace BindalBatteryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly IProduct product;
        private readonly ILoggerManager loggerManager;
        public ProductController(IProduct product, ILoggerManager _loggerManager)
        {
            this.product = product;
            loggerManager = _loggerManager;
        }


        [HttpGet]
        [ActionName("GetAllProduct")]
        public async Task<ActionResult> GetAllProducr()
        {
            try
            {
                var objProduct = await product.GetAllProductMaster();
                if (objProduct != null && objProduct.Count > 0)
                {
                    List<vwProductMaster> list = new List<vwProductMaster>();
                    foreach (Productmaster obj in objProduct)
                    {
                        vwProductMaster vwPartyMaster = new vwProductMaster();
                        vwPartyMaster.ProductId = obj.ProductId;
                        vwPartyMaster.GauranteePeriod = obj.GauranteePeriod;
                        vwPartyMaster.BrandName = obj.BrandName;
                        vwPartyMaster.BatteryType = obj.BatteryType;
                        list.Add(vwPartyMaster);
                    }
                    loggerManager.LogInfo("fetched all product");
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
        //


        [HttpGet]
        [ActionName("GetProductTypeAutocomplete")]
        public async Task<ActionResult> GetProductTypeAutocomplete(string brandname, string batteryType)
        {
            try
            {
                var objProduct = await product.GetAllProductMasterByBrnadName(brandname, batteryType);
                if (objProduct != null && objProduct.Count > 0)
                {
                    List<vwProductMaster> listproduct = new List<vwProductMaster>();
                    foreach (var obj in objProduct)
                    {
                        vwProductMaster vwProductMaster = new vwProductMaster();
                        vwProductMaster.ProductId = obj.ProductId;
                        vwProductMaster.BatteryType = obj.BatteryType;
                        listproduct.Add(vwProductMaster);
                    }
                    return Ok(listproduct);
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
        [ActionName("GetAllProductName")]
        public async Task<ActionResult> GetAllProductName(string brandname)
        {
            try
            {
                var objProduct = await product.GetAllProductMaster();
                if (objProduct != null && objProduct.Count > 0)
                {
                    List<vwProductMaster> list = new List<vwProductMaster>();
                    foreach (var  obj in objProduct.Where(x=>x.BrandName.Contains(brandname,StringComparison.OrdinalIgnoreCase)).Select(x => x.BrandName).Distinct().ToList()) 
                    {
                        
                        vwProductMaster vwProductMaster = new vwProductMaster();
                        vwProductMaster.BrandName = obj;
                        list.Add(vwProductMaster);
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
        [ActionName("GetProductAutocomplete")]
        public async Task<ActionResult> GetAutocomleteProduct(string subproduct)
        {
            try
            {
                var objParty = await product.GetAutocomleteProduct(subproduct);
                if (objParty != null && objParty.Count > 0)
                {
                    List<vwProductMaster> list = new List<vwProductMaster>();
                    foreach (Productmaster obj in objParty)
                    {
                        vwProductMaster vwProductMaster = new vwProductMaster();

                        vwProductMaster.BrandName = obj.BrandName;
                        vwProductMaster.ProductId = obj.ProductId;

                        list.Add(vwProductMaster);
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
        [ActionName("InsertProduct")]
        public async Task<ActionResult> InsertProduct([FromBody] vwProductMaster vwproductmaster)
        {
            try
            {
                Productmaster productmaster = new Productmaster();
                productmaster.GauranteePeriod = vwproductmaster.GauranteePeriod;
                productmaster.BatteryType = vwproductmaster.BatteryType;
                productmaster.BrandName = vwproductmaster.BrandName;

                var result = await product.InsertProductMaster(productmaster);
                if (result > 0)
                    return Ok("Product Inserted Successfully");
                else
                    return Ok("Product is not Inserted Successfully");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }

        }
        [HttpPost]
        [ActionName("DeleteProduct")]
        public async Task<ActionResult> DeletProduct([FromBody] vwProductMaster vwproductMaster)
        {
            try
            {
                Productmaster productmaster = new Productmaster();
                productmaster.ProductId = vwproductMaster.ProductId;
                product.DeleteProductMasterById(productmaster);
                return Ok("Prodct removed Successfully");
            }
            catch (Exception ex)
            {
                return Ok("Request do not completed successfully.");
            }

        }
    }
}
