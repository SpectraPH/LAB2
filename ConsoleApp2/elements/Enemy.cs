using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace ConsoleApp2.elements
{
    public class Enemy : Element
    {
        private Coord coord;
        private Direction direct;

        public Enemy(int x, int y)
        {
            coord = new Coord(x,y);
            direct = Direction.RIGHT;
            sym = 'X';
        }

        public Coord Coord => coord;

        public void move(Matrix matrix)
        {
            int x = coord.x;
            int y = coord.y;
            switch (direct)
            {
                case Direction.UP:
                    if(!(matrix[y--,x] is Wall) && Ranges.inRange(new Coord(x, y--)))
                        coord.y--;
                    else
                    {
                        changeDirect();
                        coord.y++;
                    }

                    break;
                case Direction.DOWN:
                    if(!(matrix[y++,x] is Wall) && Ranges.inRange(new Coord(x, y++)))
                        coord.y++;
                    else
                    {
                        changeDirect();
                        coord.y--;
                    }

                    break;
                case Direction.LEFT:
                    if(!(matrix[y,x - 1] is Wall))
                        coord.x--;
                    else
                    {
                        changeDirect();
                        move(matrix);
                    }

                    break;
                case Direction.RIGHT:
                    if(!(matrix[y,x + 1] is Wall))
                        coord.x++;
                    else
                    {
                        changeDirect();
                        move(matrix);
                    }

                    break;
            }
        }

        private void changeDirect()
        {
            switch (direct)
            {
                case Direction.UP:
                    direct = Direction.DOWN;
                    break;
                case Direction.DOWN:
                    direct = Direction.UP;
                    break;
                case Direction.LEFT:
                    direct = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    direct = Direction.LEFT;
                    break;
            }
        }

        public override char getElement()
        {
            return sym;
        }
    }
}