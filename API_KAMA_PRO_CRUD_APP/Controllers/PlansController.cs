using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Plans")]
    public class PlansController : ControllerBase
    {
        private DBContext db;

        public PlansController(DBContext context)
        {
            db = context;
        }

        [HttpPost]
        public ActionResult<Plans> Create([FromQuery] string name, [FromBody] List<Plan_linkto_Trailer> plan_content)
        {
            Plans plan = new Plans { Name = name };
            db.Plans.Add(plan);

            db.SaveChanges();

            foreach(var i in plan_content)
            {
                db.Plan_linkto_Trailer.Add(i);
            }

            db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult<Plans> GetAll()
        {
            var plans = db.Plans.ToList();
            return Ok(plans);
        }
    }
}
