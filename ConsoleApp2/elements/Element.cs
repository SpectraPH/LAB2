using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2;

namespace ConsoleApp2
{
     public abstract class Element
    {
        public char sym;
         

        public virtual void setCoord(int x, int y, Matrix gameField){}
        
        abstract public char getElement();
    }
}

