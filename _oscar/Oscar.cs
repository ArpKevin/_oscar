using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _oscar
{
    internal class Oscar
    {
        public string Azon { get; set; }
        public string Cim { get; set; }
        public int Ev { get; set; }
        public int Dij { get; set; }
        public int Jelol { get; set; }

        public Oscar(string dataLine)
        {
            string[] x = dataLine.Split('\t');

            Azon = x[0];
            Cim = x[1];
            Ev = int.Parse(x[2]);
            Dij = int.Parse(x[3]);
            Jelol = int.Parse(x[4]);
        }

        public override string ToString()
        {
            return $"Azonosító: {Azon}, Cím: {Cim}, Év: {Ev}, Díj: {Dij}, Jelölések száma: {Jelol}";
        }
    }
}
