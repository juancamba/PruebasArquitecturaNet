using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProduct(int productId)
        {
            if (productId != 1)
            {
                return BadRequest("Product not found");
            }

            return new ProductDto()
            {
                Id = 1,
                ProductName = "ApelGuach"

            };
        }
    }
}
