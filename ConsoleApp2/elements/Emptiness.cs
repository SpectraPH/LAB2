using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.elements
{
    class Emptiness : Element
    {
       
        public Emptiness()
        {
            sym = ' ';
        }

        public override char getElement()
        {
            return sym;
        }
    }
}
