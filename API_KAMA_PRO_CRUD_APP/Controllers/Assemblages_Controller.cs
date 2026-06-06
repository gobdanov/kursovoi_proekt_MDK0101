using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Assemblages_Controller : ControllerBase
    {
        private DBContext db;

        public Assemblages_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Assemblages> GetAll()
        {
            var assemblages = db.Assemblages.ToList();
            return Ok(assemblages);
        }
    }
}
