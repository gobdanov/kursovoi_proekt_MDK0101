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


        //вывод всех сборок с возможностью фильтрации по параметрам:
        //вин-код, дата, вид прицепа
        [HttpGet]
        public ActionResult<Assemblages> Read(
            [FromQuery] string EAV = null,
            [FromQuery] DateOnly? date = null,
            [FromQuery] string trailer = null)
        {

            //получаем все сборки
            List<Assemblages> assemblages = db.Assemblages.ToList();

            //если надо с определенным VIN-кодом, то меняем список
            if (EAV != null)
            {
                assemblages = assemblages.Where(x => x.VIN == EAV).ToList();
            }
            //если надо с определенной датой, то меняем список
            if (date != null)
            {
                assemblages = assemblages.Where(x => x.Date_ == date).ToList();
            }
            //если надо с определенным прицепом, то меняем список
            if (trailer != null)
            {
                //получаем записи с тем прицепом, который указали
                List<Plan_linkto_Trailer> list_of_eav_trailers = db.Plan_linkto_Trailer.Where(x => x.Trailer == trailer).ToList();
                //работаем с другим спмском
                List<Assemblages> assemblages2 = assemblages;
                // , новый обнуляем
                assemblages = new List<Assemblages>();

                for (int i = 0; i < list_of_eav_trailers.Count; i++)
                {
                    for (int j = 0; j < assemblages2.Count; j++)
                    {
                        //если в конкретной записи списка определенных прицепов одного вида
                        //есть та же запись, что и в сборках
                        // то мы ее добавляем в выводимый список
                        if (list_of_eav_trailers[i].VIN == assemblages2[j].VIN)
                        {
                            Assemblages assemblage = new Assemblages()
                            {
                                Assembler = assemblages2[j].Assembler,
                                Date_ = assemblages2[j].Date_,
                                VIN = assemblages2[j].VIN,
                            };
                            assemblages.Add(assemblage);
                        }
                    }
                }
            }

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
                return BadRequest("ошибка");
            }
        }


        [HttpPost]
        public ActionResult<Assemblages> Create([FromBody] Assemblages assemblage)
        {
            try
            {
                db.Assemblages.Add(assemblage);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
