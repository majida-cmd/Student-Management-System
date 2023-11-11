using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navbar
{
    internal class Niveau_scolaire
    {
        public String index_ns { get; set; }
        public String nom_ns { get; set; }
        public String idniveau { get; set; }
        public override string ToString()
        {
            return nom_ns;
        }
    }
}
