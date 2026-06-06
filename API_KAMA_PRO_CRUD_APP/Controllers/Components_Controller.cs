using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Components_Controller : ControllerBase
    {
        private DBContext db;

        public Components_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Components> GetAll()
        {
            var components = db.Components.ToList();
            return Ok(components);
        }
    }
}
