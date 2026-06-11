using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;

namespace API_KAMA_PRO_CRUD_APP.Controllers
{
    [ApiController]
    [Route("api/Assemblages")]
    public class AssemblagesController : ControllerBase
    {
        private DBContext db;

        public AssemblagesController(DBContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<Assemblages> Read()
        {
            var assemblages = db.Assemblages.ToList();
            return Ok(assemblages);
        }

        [HttpGet("{VIN}")]
        public ActionResult<Assemblages> Read(string VIN)
        {
            //получаем сборку
            var assemblage = db.Assemblages.Where(x => x.VIN == VIN).First();

            //получаем сборщиков
            List<int> AssemblersId = new List<int>();

            foreach (var i in db.Assemblages) // не делать перебор каждого, делать максимум перебор 12 назад и 12 вперед
            {
                if (i.VIN == VIN)
                {
                    AssemblersId.Add(i.Assembler);
                }
            }

            // получаем план и прицеп
            string Plan = "";
            string Trailer = "";
            foreach (var i in db.Plan_linkto_Trailer)
            {
                if (i.VIN == VIN)
                {
                    Plan = i.Plan;
                    Trailer = i.Trailer;
                }
            }
            return Ok(assemblages);
        }


        [HttpPost]
        public ActionResult<Assemblages> Create([FromBody] Assemblages assemblage)
        {
            db.Assemblages.Add(assemblage);
            db.SaveChanges();
            return Ok();
        }
    }
}
