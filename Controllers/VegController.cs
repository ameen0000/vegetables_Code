using LoginAndVegitable.Models;
using LoginAndVegitable.services.contract;
using LoginAndVegitable.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Vegetable_NamesList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VegController : ControllerBase
    {

        private readonly IVegeServices _veg;
        public VegController(IVegeServices veg)
        {
            _veg = veg;
        }



        [HttpGet]
        public IActionResult get()
        {
            ResponseApi<List<VegNamesForlist>> response = new ResponseApi<List<VegNamesForlist>>();
            try
            {
                List<VegNamesForlist> vegtablenew = _veg.GetVegs();
                response = new ResponseApi<List<VegNamesForlist>> { Value = vegtablenew };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<VegNamesForlist>>();
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
