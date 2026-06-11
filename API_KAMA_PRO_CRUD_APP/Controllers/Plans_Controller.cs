using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Plans")]
    public class Plans_Controller : ControllerBase
    {
        private DBContext db;

        public Plans_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Plans> GetAll()
        {
            var plans = db.Plans.ToList();
            return Ok(plans);
        }
    }
}
