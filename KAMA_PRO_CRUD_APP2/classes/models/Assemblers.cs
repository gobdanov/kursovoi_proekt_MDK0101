

using System.ComponentModel.DataAnnotations;

namespace KAMA_PRO_CRUD_APP.classes.models
{
    public class Assemblers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
    }
}
