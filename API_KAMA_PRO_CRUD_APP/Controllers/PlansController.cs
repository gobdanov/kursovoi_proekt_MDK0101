using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<Plans> Create([FromQuery] string name, [FromBody] List<Plan_linkto_Trailer> plan_content)
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

        [HttpGet]
        public ActionResult<Plans> GetAll()
        {
            var plans = db.Plans.ToList();
            return Ok(plans);
        }
    }
}
