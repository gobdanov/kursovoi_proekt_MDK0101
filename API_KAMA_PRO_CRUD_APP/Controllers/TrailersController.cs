using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<Trailers> GetAll()
        {
            var trailers = db.Trailers.ToList();
            return Ok(trailers);
        }
    }
}
