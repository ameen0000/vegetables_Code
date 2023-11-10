using LoginAndVegitable.Models;
using LoginAndVegitable.services.contract;
using LoginAndVegitable.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndVegitable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPrice _price;

        public PriceController(IPrice price)
        {
            _price = price;
        }

        [HttpGet]

        public IActionResult Get()
        {
            ResponseApi<List<priceresponse>> response = new ResponseApi<List<priceresponse>>();
            try
            {
                List<priceresponse> pricelist = _price.GetPrices();
                response = new ResponseApi<List<priceresponse>> { msg = "price generated", Value = pricelist };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<priceresponse>> { msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);


            }

        }
        [HttpPost]
        public IActionResult postprice(PriceRequest priceApi)
        {

            ResponseApi<PriceRequest> response = new ResponseApi<PriceRequest>();
            try
            {
                PriceRequest newDetail = _price.postprice(priceApi);
                response = new ResponseApi<PriceRequest> { msg="posted",Value=newDetail };
               return StatusCode(StatusCodes.Status200OK,response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<PriceRequest>();
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }


    }
}
