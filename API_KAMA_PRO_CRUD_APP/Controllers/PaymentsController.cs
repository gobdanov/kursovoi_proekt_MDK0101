using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes.DTO;
using KAMA_PRO_CRUD_APP2.classes.models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public ActionResult getPaymentById([FromQuery] string token, [FromQuery] int id)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
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

        [HttpPost]
        public ActionResult createPayment([FromQuery] string token, [FromBody] DTO_Payment payment_dto)
        {
ClaimsPrincipal claims;
            try
            {
                claims = JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            if (claims.IsInRole("Admin"))
            {
                if (payment_dto != null)
                {
                    Payments payment = new Payments
                    {
                        Assembler = payment_dto.Assembler,
                        Hours = payment_dto.Hours,
                        Date_ = DateOnly.FromDateTime(DateTime.Now)
                    };

                    db.Payments.Add(payment);
                    db.SaveChanges();

                    return Ok(payment);
                }
                else
                {
                    return BadRequest("такого сборщика нет");
                }
            }
            else
            {
                return BadRequest($"Только админ может назначать почасовку!");
            }
        }
    }
}
