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

        [HttpPost]
        public ActionResult Update([FromQuery] string name, [FromQuery] int count)
        {
            Components component = db.Components.Where(x => x.Name == name).FirstOrDefault();

            if (component != null)
            {
                component.Quantity += count;
                db.SaveChanges();
            }
            return Ok(db.Components.Where(x =>x.Name == name));
        }

        [HttpGet]
        public ActionResult<Components> GetAll()
        {
            var components = db.Components.ToList();
            return Ok(components);
        }
    }
}
