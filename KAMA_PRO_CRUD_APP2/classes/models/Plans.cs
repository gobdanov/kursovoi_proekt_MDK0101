using System.ComponentModel.DataAnnotations;

namespace KAMA_PRO_CRUD_APP.classes.models
{
    public class Plans
    {
        [Key]
        public string Name { get; set; }
        public DateTime created_at { get; set; }
    }
}
