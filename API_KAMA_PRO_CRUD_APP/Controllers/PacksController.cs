using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Packs")]
    public class PacksController : ControllerBase
    {
        private DBContext db;

        public PacksController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Packs> GetAll()
        {
            var packs = db.Packs.ToList();
            return Ok(packs);
        }
    }
}
