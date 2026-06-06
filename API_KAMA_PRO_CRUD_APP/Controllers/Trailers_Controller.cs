using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Trailers_Controller : ControllerBase
    {
        private DBContext db;

        public Trailers_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Trailers> GetAll()
        {
            var trailers = db.Trailers.ToList();
            return Ok(trailers);
        }
    }
}
