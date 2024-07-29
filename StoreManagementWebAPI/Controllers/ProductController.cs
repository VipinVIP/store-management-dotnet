using Microsoft.AspNetCore.Mvc;
using StoreManagementWebAPI.Models;
using StoreManagementWebAPI.Services;


namespace StoreManagementWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private const string filePath = "D:\\Dot Net\\store-administration-app\\products.csv";
        private const string updatedFilePath = "D:\\Dot Net\\store-administration-app\\sales-report.csv";
        private IProductServices productServices;
        private ISalesServices salesServices;
        public ProductController(IProductServices _productServices,ISalesServices _salesServices)
        {
            productServices = _productServices;
            salesServices = _salesServices;

        }

        [HttpGet(Name = "GetProductList")]
        public ActionResult<ApiResponse> Get()
        {
            var response = new ApiResponse();
            try
            {   var productList= productServices.GetProducts(filePath);
                response.Data = productList;
                response.Count=productList.Count;
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

        [HttpPut]
        public ActionResult<ApiResponse> BuyProduct([FromBody] PurchaseRequest request)
        {
            var response = new ApiResponse();

            try
            {
                var productList = productServices.GetProducts(filePath);
                var product = productList.Find(p => p.Code == request.ProductId);
                if (product == null)
                {
                    response.Status = "failure";
                    response.Error = "Product not found.";
                    return BadRequest(response);
                }

                if (product.Stock < request.Quantity)
                {
                    response.Status = "failure";
                    response.Error = "Insufficient stock.";
                    return BadRequest(response);
                }

                product.Stock -= request.Quantity;
                productServices.UpdateProduct(product, filePath);
                salesServices.UpdateSalesReport(product, request.Quantity, updatedFilePath);
                response.Status = "success";
                response.Data = new
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Status = "failure";
                response.Error = ex.Message;

                return BadRequest(response);
            }
        }


        [HttpPost]
        public ActionResult<ApiResponse> AddProduct([FromBody] StockRequest request)
        {
            var response = new ApiResponse();
            var productList = productServices.GetProducts(filePath);
            Product product = productList.Find(p => p.Code == request.Code);
            if (product == null)
            {
                product = new Product(request.Code, request.Name??"", request.Price??0, request.Stock);
                product.Code=request.Code;
                productServices.UpdateProduct(product, filePath);
                response.Status = "success";
                response.Data = request;
                return Ok(response);
            }
            else
            {
                product.Stock += request.Stock;
                productServices.UpdateProduct(product, filePath);
                response.Status = "success";
                response.Data = new
                {
                    ProductId = request.Code,
                    Quantity = request.Stock,
                };
                return Ok(response);
            }
        }

        [HttpDelete]

        public ActionResult<ApiResponse> DeleteProduct([FromBody] DeleteRequest request)
        {
            var response = new ApiResponse();
            try {
                var productList = productServices.GetProducts(filePath);
                Product product = productList.Find(p => p.Code == request.ProductId);
                if (product == null)
                {
                    response.Status = "failure";
                    response.Error = "No Product with given productcode Found";
                    return BadRequest(response);

                }
                else
                {

                    productServices.DeleteProduct(product.Code, filePath);
                    response.Status = "success";
                    response.Data = new
                    {
                        ProductId = request.ProductId,
                        Quantity = 0
                    };
                    return Ok(response);
                }
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
