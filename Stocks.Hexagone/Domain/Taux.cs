using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Hexagone.Domain
{
   public class Taux
    {
        public double TauxNonImporter { get; private set; } = 0.2;
        public double TauxImporter { get; private set; } = 0.055;
    }
}
