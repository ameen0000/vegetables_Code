using Azure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using LoginAndVegitable.Utilities;
using LoginAndVegitable.Models;
using LoginAndVegitable.services.contract;

namespace LoginAndVegitable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthservice _authservice;
        public AuthController(IAuthservice userService)
        {
            _authservice = userService;

        }
        [HttpPost]

        public IActionResult postCustomerDetails(userreaponse userapi)
        {
            ResponseApi<userreaponse> responce = new ResponseApi<userreaponse>();

            try
            {
                userreaponse newdeatil = _authservice.Addlist(userapi);
                responce = new ResponseApi<userreaponse>
                {
                    status = true,
                    msg = "Added",
                    Value = newdeatil
                };
                return StatusCode(StatusCodes.Status200OK, responce);

            }
            catch (Exception ex)
            {
                responce = new ResponseApi<userreaponse>(); responce.msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responce);

            }
        }

        [HttpPost("verify")]
        public ActionResult<userreaponse> CheckUser([FromBody] userreaponse loginApi)
        {
            try
            {
                var responses = this._authservice.Verify(loginApi);
                if (string.IsNullOrWhiteSpace(loginApi.UserName) || string.IsNullOrWhiteSpace(loginApi.Password))
                {
                    return BadRequest(responses);
                }
                if (responses == "verification failed" || responses == "Wrong Password")
                {
                    return BadRequest(responses);
                }
                return Ok(responses);
            }
            catch (Exception ex)
            {
                var response = new ResponseApi<userreaponse>(); response.msg = ex.Message;
                return BadRequest(response);

            }

        }
        [HttpGet]

        public IActionResult getapi()
        {
            ResponseApi<List<userreaponse>> response = new ResponseApi<List<userreaponse>>();
            try
            {
                List<userreaponse> pricelist = _authservice.get();
                response = new ResponseApi<List<userreaponse>> { msg = "price generated", Value = pricelist };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<userreaponse>> { msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);


            }

        }


    }
}








