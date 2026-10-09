namespace KAMA_PRO_CRUD_APP2.DTO
{
    public class DTO_AddAssemblage
    {
        public string Plan { get; set; }
        public string Trailer { get; set; }
        public List<string> VINs { get; set; }
        public List<int> AssemblerIds { get; set; }
        public DateOnly? Date { get; set; } // если null — берём сегодня
    }
}