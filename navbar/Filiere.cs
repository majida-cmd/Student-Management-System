using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navbar
{
    internal class Filiere
    {
        public String index { get; set; }
        public String nom { get; set; }
        public String idniveau { get; set; }
        public override string ToString()
        {
            return nom;
        }
    }
}
