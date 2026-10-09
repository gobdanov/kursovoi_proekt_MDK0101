using KAMA_PRO_CRUD_APP.classes.models;
using Microsoft.AspNetCore.Mvc;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;
using API_KAMA_PRO_CRUD_APP.DTO;

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
        public async Task<ActionResult> Create([FromBody] DTO.DTO_AddAssemblage request)
        {
            // ---------- 1. Базовая валидация входных данных ----------
            if (request == null)
                return BadRequest("Пустой запрос");

            if (string.IsNullOrWhiteSpace(request.Plan))
                return BadRequest("Не указан план");

            if (string.IsNullOrWhiteSpace(request.Trailer))
                return BadRequest("Не указан прицеп");

            if (request.VINs == null || request.VINs.Count == 0)
                return BadRequest("Не указаны VIN-коды");

            if (request.AssemblerIds == null || request.AssemblerIds.Count == 0)
                return BadRequest("Не указаны сборщики");

            // ---------- 2. Проверка существования плана и прицепа ----------
            bool planExists = db.Plans.Any(p => p.Name == request.Plan);
            if (!planExists)
                return BadRequest($"План '{request.Plan}' не найден");

            bool trailerExists = db.Trailers.Any(t => t.Name == request.Trailer);
            if (!trailerExists)
                return BadRequest($"Прицеп '{request.Trailer}' не найден");

            // ---------- 3. Проверка сборщиков ----------
            List<int> existingAssemblers = db.Assemblers
                .Where(a => request.AssemblerIds.Contains(a.Id))
                .Select(a => a.Id)
                .ToList();

            if (existingAssemblers.Count != request.AssemblerIds.Distinct().Count())
            {
                var missing = request.AssemblerIds.Except(existingAssemblers).ToList();
                return BadRequest($"Сборщики не найдены: {string.Join(", ", missing)}");
            }

            // ---------- 4. Проверка VIN-ов ----------
            // 4.1. Все VIN должны существовать в plan_linkto_trailer
            List<Plan_linkto_Trailer> planLinks = db.Plan_linkto_Trailer
                .Where(x => request.VINs.Contains(x.VIN))
                .ToList();

            List<string> missingVINs = request.VINs.Except(planLinks.Select(x => x.VIN)).ToList();
            if (missingVINs.Count > 0)
                return BadRequest($"VIN-коды не найдены в плане: {string.Join(", ", missingVINs)}");

            // 4.2. Все VIN должны принадлежать указанному плану
            List<string> wrongPlanVINs = planLinks
                .Where(x => x.Plan != request.Plan)
                .Select(x => x.VIN)
                .ToList();
            if (wrongPlanVINs.Count > 0)
                return BadRequest($"VIN-коды не принадлежат плану '{request.Plan}': {string.Join(", ", wrongPlanVINs)}");

            // 4.3. Все VIN должны принадлежать указанному прицепу
            List<string> wrongTrailerVINs = planLinks
                .Where(x => x.Trailer != request.Trailer)
                .Select(x => x.VIN)
                .ToList();
            if (wrongTrailerVINs.Count > 0)
                return BadRequest($"VIN-коды не принадлежат прицепу '{request.Trailer}': {string.Join(", ", wrongTrailerVINs)}");

            // 4.4. Все VIN должны быть ещё не собраны (Ready == 0)
            List<string> alreadyReadyVINs = planLinks
                .Where(x => x.Ready)
                .Select(x => x.VIN)
                .ToList();
            if (alreadyReadyVINs.Count > 0)
                return BadRequest($"VIN-коды уже собраны: {string.Join(", ", alreadyReadyVINs)}");

            // 4.5. Страховка: VIN-ов не должно быть в assemblages
            List<string> alreadyInAssemblages = db.Assemblages
                .Where(a => request.VINs.Contains(a.VIN))
                .Select(a => a.VIN)
                .Distinct()
                .ToList();
            if (alreadyInAssemblages.Count > 0)
                return BadRequest($"VIN-коды уже есть в сборках: {string.Join(", ", alreadyInAssemblages)}");

            // ---------- 5. Проверка наличия комплектующих ----------
            // Сколько прицепов данного типа собираем
            int trailerCount = request.VINs.Count;

            // Какие компоненты и в каком количестве нужны на один такой прицеп
            List<Component_linkto_Trailer> componentsPerTrailer = db.Component_linkto_Trailer
                .Where(x => x.Trailer == request.Trailer)
                .ToList();

            if (componentsPerTrailer.Count == 0)
                return BadRequest($"Для прицепа '{request.Trailer}' не задана комплектация");

            // Проверяем каждый компонент
            List<string> componentNames = componentsPerTrailer.Select(c => c.Component).ToList();

            Dictionary<string, int> stock = db.Components
                .Where(c => componentNames.Contains(c.Name))
                .ToDictionary(c => c.Name, c => c.Quantity);

            List<string> notEnough = new List<string>();
            foreach (var cpt in componentsPerTrailer)
            {
                if (!stock.TryGetValue(cpt.Component, out int available))
                {
                    notEnough.Add($"{cpt.Component} (нет на складе)");
                    continue;
                }

                int needed = cpt.Quantity * trailerCount;
                if (available < needed)
                    notEnough.Add($"{cpt.Component} (нужно {needed}, есть {available})");
            }

            if (notEnough.Count > 0)
                return BadRequest($"Не хватает комплектующих: {string.Join("; ", notEnough)}");

            // ---------- 6. Создание сборок в транзакции ----------
            DateOnly date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

            // Уникальные сборщики (на случай дублей в запросе)
            List<int> assemblerIds = request.AssemblerIds.Distinct().ToList();

            // Уникальные VIN (на случай дублей в запросе)
            List<string> vins = request.VINs.Distinct().ToList();

            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                List<Assemblages> newAssemblages = new List<Assemblages>();

                foreach (int assemblerId in assemblerIds)
                {
                    foreach (string vin in vins)
                    {
                        newAssemblages.Add(new Assemblages
                        {
                            Assembler = assemblerId,
                            VIN = vin,
                            Date_ = date
                        });
                    }
                }

                db.Assemblages.AddRange(newAssemblages);
                await db.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Ошибка при создании сборок: {ex.Message}");
            }

            return Ok();
        }


        [HttpPost("old")]
        public ActionResult<Assemblages> Create_Old([FromBody] Assemblages assemblage)
        {
            try
            {
                //добавляем сборку
                db.Assemblages.Add(assemblage);

                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        public ActionResult<Assemblages> DeleteById(string vin)
        {
            try
            {
                List<Assemblages> assemblages = db.Assemblages.Where(x => x.VIN == vin).ToList();
                //добавляем сборку
                if (assemblages.Any())
                {
                    foreach (var assemblage in assemblages)
                    {
                        db.Assemblages.Remove(assemblage);
                    }
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound("сборка не найдена");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
