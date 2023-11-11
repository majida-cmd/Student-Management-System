using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navbar
{
    internal class Niveau
    {
        public String index_niveau { get; set; }
        public String nom_niveau { get; set; }
        public override string ToString()
        {
            return nom_niveau;
        }
    }
}
