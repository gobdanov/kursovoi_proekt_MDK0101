using System.ComponentModel.DataAnnotations;

namespace KAMA_PRO_CRUD_APP.classes.models
{
    public class Components
    {
        [Key]
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
