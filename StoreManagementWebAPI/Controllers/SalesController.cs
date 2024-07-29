using Microsoft.AspNetCore.Mvc;
using StoreManagementWebAPI.Models;
using StoreManagementWebAPI.Services;

namespace StoreManagementWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private const string filePath = "D:\\Dot Net\\store-administration-app\\sales-report.csv";
        private List<SalesRecord> products = new List<SalesRecord>();
        private ISalesServices salesServices;

        public SalesController(ISalesServices _salesServices)
        {
            salesServices = _salesServices;
        }

        [HttpGet(Name = "GetSalesReport")]
        public ActionResult<IEnumerable<SalesRecord>> Get()
        {
            var response = new ApiResponse();
            try
            {
                var selledItems = salesServices.GetSalesReport(filePath);
                response.Data = selledItems;
                response.Count = selledItems.Count;
                response.Status = "success";
                return Ok(response);

            }
            catch (Exception ex)
            {

                response.Status = "failure";
                response.Error = ex.Message;

                return BadRequest(response);
            }

        }
    }
}
