using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Security.Claims;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Plan_linkto_Trailer")]
    public class Plan_linkto_TrailerController : ControllerBase
    {
        private DBContext db;

        public Plan_linkto_TrailerController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Plan_linkto_Trailer> GetAll([FromQuery] string token)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            var plan_linkto_trailer = db.Plan_linkto_Trailer.ToList();
            return Ok(plan_linkto_trailer);
        }
    }
}
