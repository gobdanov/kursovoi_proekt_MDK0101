using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using API_KAMA_PRO_CRUD_APP.DTO;

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
        public ActionResult<Assemblers> Read()
        {
            var assemblers = db.Assemblers.ToList();
            return Ok(assemblers);
        }
        [HttpPost]
        public ActionResult<Assemblers> Create([FromBody] DTO_Assembler assembler)
        {
            Assemblers assembler_ready = new Assemblers
            {
                Name = assembler.Name,
                Surname = assembler.Surname,
                Lastname = assembler.Lastname,
                Username = assembler.Username,
                Pwd = assembler.Pwd
            };
            db.Assemblers.Add(assembler_ready);
            db.SaveChanges();
            return Ok(assembler_ready);
        }
    }
}
