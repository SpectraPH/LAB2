using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.elements
{
    class Key : Element
    {
        
        public Key(char sym)
        {
            this.sym = char.ToLower(sym);
            
        }

        public char getKey()
        {
            return sym;
        }
        
        public override char getElement()
        {
            return sym;
        }
    }
}
