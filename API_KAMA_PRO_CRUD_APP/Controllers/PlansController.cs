using API_KAMA_PRO_CRUD_APP.Classes;
using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public ActionResult<Plans> Create([FromQuery] string token, [FromQuery] string name, [FromBody] List<Plan_linkto_Trailer> plan_content)
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
                bool flag = true;
                foreach (var p in db.Plans)
                {
                    if (p.Name == name)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    Plans plan = new Plans { Name = name };
                    db.Plans.Add(plan);

                    db.SaveChanges();

                    try
                    {
                        foreach (var i in plan_content)
                        {
                            db.Plan_linkto_Trailer.Add(i);
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // Любая другая ошибка
                        Console.WriteLine(ex.Message);
                    }



                    db.SaveChanges();

                    return Ok();
                }
                return BadRequest("план уже существует!");
            }
            else
            {
                return BadRequest($"Только админ может создавать план!");
            }
        }

        [HttpGet]
        public ActionResult<Plans> GetAll([FromQuery] string token)
        {
            try
            {
                JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неерный токен");
            }
            var plans = db.Plans.ToList();
            return Ok(plans);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] string token, [FromQuery] string name)
        {
            ClaimsPrincipal claims;
            try
            {
                claims = JwtTokenGenerator.Validate(token);
            }
            catch
            {
                return BadRequest("Неверный токен");
            }

            if (!claims.IsInRole("Admin"))
            {
                return BadRequest("Только админ может удалять план!");
            }

            var plan = db.Plans.FirstOrDefault(p => p.Name == name);
            if (plan == null)
            {
                return NotFound("План не найден");
            }

            try
            {
                db.Plans.Remove(plan);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ошибка при удалении плана");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ошибка при удалении плана");
            }

            return Ok();
        }
    }
}
