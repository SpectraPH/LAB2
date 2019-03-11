using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.elements
{
    class Wall : Element
    {
        public Wall()
        {
            sym = '#';
        }

        public override char getElement()
        {
            return sym;
        }
    }
}
