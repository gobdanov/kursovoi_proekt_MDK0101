using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Packs_Controller : ControllerBase
    {
        private DBContext db;

        public Packs_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Packs> GetAll()
        {
            var packs = db.Packs.ToList();
            return Ok(packs);
        }
    }
}
