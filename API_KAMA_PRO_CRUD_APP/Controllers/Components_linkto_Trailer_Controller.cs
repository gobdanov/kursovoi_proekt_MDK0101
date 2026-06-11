using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Components_linkto_Trailer")]
    public class Components_linkto_Trailer_Controller : ControllerBase
    {
        private DBContext db;

        public Components_linkto_Trailer_Controller(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Component_linkto_Trailer> GetAll()
        {
            var component_linkto_trailer = db.Component_linkto_Trailer.ToList();
            return Ok(component_linkto_trailer);
        }
    }
}
