using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Assemblers")]
    public class AssemblersController : ControllerBase
    {
        private DBContext db;

        public AssemblersController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Assemblers> GetAll()
        {
            var assemblers = db.Assemblers.ToList();
            return Ok(assemblers);
        }
    }
}
