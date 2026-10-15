using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Components_linkto_Trailer")]
    public class Components_linkto_TrailerController : ControllerBase
    {
        private DBContext db;

        public Components_linkto_TrailerController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Component_linkto_Trailer> GetAll([FromQuery] string token)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            var component_linkto_trailer = db.Component_linkto_Trailer.ToList();
            return Ok(component_linkto_trailer);
        }
    }
}
