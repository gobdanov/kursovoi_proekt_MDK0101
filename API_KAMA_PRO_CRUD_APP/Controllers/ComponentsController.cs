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

        [HttpPost("delete")]
        public ActionResult Delete([FromQuery] string token, [FromQuery] string name)
        {
            ClaimsPrincipal claims;
            try { claims = JwtTokenGenerator.Validate(token); }
            catch { return BadRequest("Неверный токен"); }

            if (!claims.IsInRole("Admin"))
                return BadRequest("Только админ может удалять комплектующие!");

            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Не указано имя компонента");

            var component = db.Components.FirstOrDefault(x => x.Name == name);
            if (component == null)
                return BadRequest("Такого компонента нет");

            db.Components.Remove(component);
            db.SaveChanges();
            return Ok(component);
        }

        [HttpPost("create")]
        public ActionResult Create([FromQuery] string token, [FromBody] Components comp)
        {
            ClaimsPrincipal claims;
            try { claims = JwtTokenGenerator.Validate(token); }
            catch { return BadRequest("Неверный токен"); }

            if (!claims.IsInRole("Admin"))
                return BadRequest("Только админ может добавлять комплектующие!");

            if (string.IsNullOrWhiteSpace(comp?.Name) || comp.Quantity < 0)
                return BadRequest("Введены неверные данные!");

            if (db.Components.Any(x => x.Name == comp.Name))
                return BadRequest("Такой компонент уже есть");

            db.Components.Add(comp);
            db.SaveChanges();
            return Ok(comp);
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

        [HttpGet("get")]
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
