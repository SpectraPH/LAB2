using System.CodeDom;
using System.Collections;
using System.Runtime.CompilerServices;

namespace ConsoleApp2
{
    public static class Ranges  //static
    {
        private static Coord size;


        public static void setSize(Coord size_)
        {
            size = size_;
        }

        public static Coord getSize()
        {
            return size;
        }

        public static bool inRange(Coord coord)
        {
            return coord.x >= 0 && coord.x < size.x &&
                   coord.y >= 0 && coord.y < size.y;
        }
    }
}