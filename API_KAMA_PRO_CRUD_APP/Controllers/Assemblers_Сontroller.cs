using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Assemblers_Сontroller : ControllerBase
    {
        private DBContext db;

        public Assemblers_Сontroller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<Assemblers> GetAll()
        {
            var assemblers = db.Assemblers.ToList();
            return Ok(assemblers);
        }
    }
}
