using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Windows;

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
        public ActionResult Update([FromQuery] string token, [FromQuery] string name, [FromQuery] int count)
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
                Components component = db.Components.Where(x => x.Name == name).FirstOrDefault();

                if (component != null)
                {
                    component.Quantity += count;
                    db.SaveChanges();
                }
                return Ok(db.Components.Where(x => x.Name == name));
            }
            else
            {
                return BadRequest($"Только админ может явно обновлять кол-во комплектующих!");
            }
        }

        [HttpGet]
        public ActionResult<Components> GetAll([FromQuery] string token)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            var components = db.Components.ToList();
            return Ok(components);
        }
    }
}
