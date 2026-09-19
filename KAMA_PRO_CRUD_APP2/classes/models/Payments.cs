using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMA_PRO_CRUD_APP2.classes.models
{
    public class Payments
    {
        public int Id { get; set; }
        public int Assembler { get; set; }
        public int Hours { get; set; }
        public DateOnly Date_ { get; set; }
    }
}
