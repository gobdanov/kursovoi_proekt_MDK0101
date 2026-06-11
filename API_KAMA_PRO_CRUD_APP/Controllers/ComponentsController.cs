using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Components")]
    public class ComponentsController : ControllerBase
    {
        private DBContext db;

        public ComponentsController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Components> GetAll()
        {
            var components = db.Components.ToList();
            return Ok(components);
        }
    }
}
