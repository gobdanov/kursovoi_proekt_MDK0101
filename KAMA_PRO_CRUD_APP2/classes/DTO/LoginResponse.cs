using KAMA_PRO_CRUD_APP.classes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMA_PRO_CRUD_APP2.classes.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public Assemblers Assembler { get; set; }
    }
}
