using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navbar
{
    internal class Ville
    {
        public String index { get; set; }
        public String nom { get; set; }
        public String idpays { get; set; }
        public override string ToString()
        {
            return nom;
        }
    }
}
