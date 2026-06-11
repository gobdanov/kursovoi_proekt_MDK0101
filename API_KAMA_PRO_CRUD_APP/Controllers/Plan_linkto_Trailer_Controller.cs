using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Plan_linkto_Trailer")]
    public class Plan_linkto_Trailer_Controller : ControllerBase
    {
        private DBContext db;

        public Plan_linkto_Trailer_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Plan_linkto_Trailer> GetAll()
        {
            var plan_linkto_trailer = db.Plan_linkto_Trailer.ToList();
            return Ok(plan_linkto_trailer);
        }
    }
}
