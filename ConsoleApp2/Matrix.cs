using ConsoleApp2.elements;

namespace ConsoleApp2
{
    public class Matrix
    {
        private Element[,] matrix;

        public Matrix(Element element)
        {
            matrix = new Element[Ranges.getSize().x,Ranges.getSize().y];
        }

        public Element this[int y, int x]
        {
            get
            {
                if (Ranges.inRange(new Coord(x, y)))
                    return matrix[x, y];
                else
                {
                    return null;
                }
            }
            set { matrix[x, y] = value; }
        }
    }
}