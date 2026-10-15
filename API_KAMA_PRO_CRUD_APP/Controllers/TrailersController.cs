using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Trailers")]
    public class TrailersController : ControllerBase
    {
        private DBContext db;

        public TrailersController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Trailers> GetAll([FromQuery] string token)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            var trailers = db.Trailers.ToList();
            return Ok(trailers);
        }
    }
}
