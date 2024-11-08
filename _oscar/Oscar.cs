using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _oscar
{
    internal class Oscar
    {
        public Oscar(string azon, string cim, int ev, int dij, int jelol)
        {
            Azon = azon;
            Cim = cim;
            Ev = ev;
            Dij = dij;
            Jelol = jelol;
        }

        public string Azon { get; set; }
        public string Cim { get; set; }
        public int Ev { get; set; }
        public int Dij { get; set; }
        public int Jelol { get; set; }
        public override string ToString()
        {
            return $"Azonosító: {Azon}, Cím: {Cim}, Év: {Ev}, Díj: {Dij}, Jelölések száma: {Jelol}";
        }
    }
}
