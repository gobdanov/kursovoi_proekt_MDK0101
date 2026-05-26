using System.ComponentModel.DataAnnotations;

namespace KAMA_PRO_CRUD_APP.classes.models
{
    public class Plan_linkto_Trailer
    {
        public string Plan { get; set; }
        public string Trailer { get; set; }
        [Key]
        public string VIN { get; set; }
        public bool Ready { get; set; }
        public int Pack { get; set; }
    }
}
