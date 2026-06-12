using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;

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


        [HttpGet("{Date}")]
        public ActionResult<Assemblages> Read(string Date)
        {
            try
            {
                //получаем дату сборки
                DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(Date));

                //получаем винкода всех прицепов, которые собирали за этот день
                List<string> VINS = new List<string>(db.Assemblages.Where(x => x.Date_ == date).Select(x => x.VIN).Distinct());

                List<int> assemblers = new List<int>();
                string trailer = "";
                string plan = "";
                List<Concrete_Assemblage_Date> assemblage_concrete = new List<Concrete_Assemblage_Date>();

                //выбираем вин конкретный (перебор){
                foreach (var i in VINS)
                {
                    // выбираем прицеп
                    trailer = db.Plan_linkto_Trailer.Where(x => x.VIN == i).Select(x => x.Trailer).First();

                    //выбираем сборщиков, которые собирали эти прицепы в LIST
                    assemblers = db.Assemblages.Where(x => x.VIN == i && x.Date_ == date).Select(x => x.Assembler).ToList();

                    // выбираем план
                    plan = db.Plan_linkto_Trailer.Where(x => x.VIN == i).Select(x => x.Plan).First();

                    //создаем объект 
                    Concrete_Assemblage_Date assemblage = new Concrete_Assemblage_Date
                    {
                        Trailer = trailer,
                        Assemblers = assemblers,
                        Comments = "тест",
                        Plan = plan,
                        Nameplate = i
                    };

                    assemblage_concrete.Add(assemblage);
                }
                return Ok(assemblage_concrete);
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok("ошибка");
            }
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
