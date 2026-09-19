using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Payments")]
    public class PaymentsController : ControllerBase
    {

        DBContext db;
        public PaymentsController(DBContext context)
        {
            db = context;
        }

        [HttpGet("get")]
        public ActionResult getPaymentById([FromQuery] int id)
        {
            Assemblers assembler = db.Assemblers.Where(x => x.Id == id).FirstOrDefault();
            if (assembler == null)
            {
                return BadRequest("такого сборщика нет");
            }
            else
            {
                List<Payments> paymentsList = db.Payments.Where(x => x.Assembler == assembler.Id).ToList();
                return Ok(paymentsList);
            }
        }
    }
}
