using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.elements
{
    class Door : Element
    {
        
        public Door(char sym)
        {
            this.sym = char.ToUpper(sym);           
        }


        public override char getElement()
        {
            return sym;
        }
    }

}
