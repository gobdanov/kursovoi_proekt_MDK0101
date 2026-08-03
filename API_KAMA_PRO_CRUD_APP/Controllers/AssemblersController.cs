using API_KAMA_PRO_CRUD_APP.Classes;
using API_KAMA_PRO_CRUD_APP.DTO;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

        [HttpGet("Salary")]
        public ActionResult<Assemblers> GetSalary([FromQuery] int id)
        {

            //проверяем существует ли сборщик с таким id
            Assemblers assembler = db.Assemblers.Where(x => x.Id == id).FirstOrDefault();

            if (assembler != null)
            {

                DateTime[] first_and_last_days = helpful_class.getWeek();

                //ищеи все сборки, которые собирал сборщик в течение недели
                List<Assemblages> assemblages_with_assembler = db.Assemblages
                    .Where(x => x.Assembler == assembler.Id)
                    .Where(x => x.Date_ >= DateOnly.FromDateTime(first_and_last_days[0])
                    && x.Date_ <= DateOnly.FromDateTime(first_and_last_days[1])).ToList();

                List<Assemblages> ALL_assemblages = db.Assemblages
                    .Where(x => x.Date_ >= DateOnly.FromDateTime(first_and_last_days[0])
                    && x.Date_ <= DateOnly.FromDateTime(first_and_last_days[1])).ToList();

                //матрица для запоминания прицепа и кол-ва сборщиков
                List<helpful_class> matrix = new List<helpful_class>();

                for (int i = 0; i < assemblages_with_assembler.Count; i++)
                {
                    int count = 0;

                    for (int j = 0; j < ALL_assemblages.Count; j++)
                    {
                        //если собирали один и тот же прицеп, то
                        if (assemblages_with_assembler[i].VIN == ALL_assemblages[j].VIN)
                        {
                            //увеличиваем кол-во
                            count++;
                        }
                    }
                    //создаём строку матрицы
                    helpful_class help = new helpful_class
                    {
                        count = count,
                        Trailer_Vin = assemblages_with_assembler[i].VIN
                    };
                    //добавляем запись
                    matrix.Add(help);
                }

                //перебираем каждую строку в матрице
                for (int i = 0; i < matrix.Count; i++)
                {
                    //по VIN - коду определяем прицеп
                    var trailer = db.Plan_linkto_Trailer
                        .Where(x => x.VIN == matrix[i].Trailer_Vin)
                        .Select(x => x.Trailer)
                        .First();
                    matrix[i].Trailer_Vin = trailer.ToString();
                }

                int allSum = 0;
                for (int i = 0; i < matrix.Count; i++)
                {
                    //переприсваиваем значение - вместо кол-ва, присваиваем сумму, которую полуит сборщик за конкретный прицеп, с учетом других сборщиков
                    matrix[i].count = helpful_class.price_list[matrix[i].Trailer_Vin] / matrix[i].count;
                    // в переменную прибавляем эту сумму. переменная является общей суммой.
                    allSum += matrix[i].count;
                }
                return Ok(matrix);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpGet]
        public ActionResult<Assemblers> Read()
        {
            var assemblers = db.Assemblers.ToList();
            return Ok(assemblers);
        }


        [HttpPost]
        [Route("Login")]
        public ActionResult<Assemblers> Login([FromBody] DTO_Assembler_Login Login)
        {
            string hash_password = SHA256_class.GetSha256Hash(Login.password);

            var assembler = db.Assemblers.Where(x => x.Username == Login.username && x.Pwd == hash_password).FirstOrDefault();
            if (assembler != null)
            {
                return Ok(assembler);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public ActionResult<Assemblers> Create([FromBody] DTO_Assembler assembler)
        {
            if (db.Assemblers.Where(x => x.Username == assembler.Username).FirstOrDefault() == null)
            {
                Assemblers assembler_ready = new Assemblers
                {
                    Name = assembler.Name,
                    Surname = assembler.Surname,
                    Lastname = assembler.Lastname,
                    Username = assembler.Username,
                    Pwd = SHA256_class.GetSha256Hash(assembler.Pwd)
                };
                db.Assemblers.Add(assembler_ready);
                db.SaveChanges();
                return Ok(assembler_ready);
            }
            else
            {
                return BadRequest("такой пользователь уже есть!");
            }
        }

        [HttpDelete]
        public ActionResult<Assemblers> Delete([FromQuery] string username)
        {
            Assemblers assembler = db.Assemblers.Where(x => x.Username == username).First();
            if (assembler != null)
            {
                db.Assemblers.Remove(assembler);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }

    public class helpful_class
    {
        public string Trailer_Vin { get; set; }
        public int count { get; set; }

        public static DateTime[] getWeek()
        {
            DateTime[] first_and_last = new DateTime[2];

            DayOfWeek DayOfWeek = DateTime.Now.DayOfWeek;
            if (DayOfWeek == DayOfWeek.Monday)
            {
                first_and_last[0] = DateTime.Now.Date;
                first_and_last[1] = DateTime.Now.Date.AddDays(6);
            }
            else if (DayOfWeek == DayOfWeek.Tuesday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-1);
                first_and_last[1] = DateTime.Now.Date.AddDays(5);
            }
            else if (DayOfWeek == DayOfWeek.Wednesday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-2);
                first_and_last[1] = DateTime.Now.Date.AddDays(5);
            }
            else if (DayOfWeek == DayOfWeek.Thursday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-3);
                first_and_last[1] = DateTime.Now.Date.AddDays(4);
            }
            else if (DayOfWeek == DayOfWeek.Friday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-4);
                first_and_last[1] = DateTime.Now.Date.AddDays(3);
            }
            else if (DayOfWeek == DayOfWeek.Saturday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-5);
                first_and_last[1] = DateTime.Now.Date.AddDays(2);
            }
            else if (DayOfWeek == DayOfWeek.Sunday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-6);
                first_and_last[1] = DateTime.Now.Date.AddDays(1);
            }
            return first_and_last;
        }

        public static Dictionary<string, int> price_list = new Dictionary<string, int>
        {
            { "KAMA PRO 2012", 330 },
     { "KAMA PRO 2013", 330 },
    { "KAMA TRAIL 2013", 330 },
    { "KAMA PRO 2513", 350 },
    { "KAMA TRAIL 2513", 350 },
    { "KAMA PRO 2515", 350 },
    { "KAMA PRO 3015", 360 },
    { "KAMA PRIME 3015", 360 },
    { "KAMA PRO 3015 (2ОСИ)", 360 },
    { "KAMA PRIME 3015 (2ОСИ)", 360 },
    { "KAMA PRO 3515", 370 },
    { "KAMA PRIME 3515", 370 },
    { "KAMA PRO 3515 (2ОСИ)", 370 },
    { "KAMA PRIME 3515 (2ОСИ)", 370 },
    { "KAMA WATER", 330 }
        };
    }
}
