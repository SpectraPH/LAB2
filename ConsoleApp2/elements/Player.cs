using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.elements
{
    class Player : Element
    {
        private Coord coord ;
        private  List<char> backPack = new List<char>();
        private Direction direct;
        private int coinCount;

        public Player(int x, int y)
        {
            coord = new Coord(x,y);
            coinCount = 0;
            sym = '@';
        }

        public Coord Coord
        {
            get{
                return coord;
            }
        }

        public Direction Direct
        {
            get { return direct; }
        }

        public int CoinCount
        {
            get { return coinCount; }
        }

        public override void setCoord(int x, int y, Matrix gameField)
        {
            if (coord.y - 1 == y)
                direct = Direction.UP;
            else if (coord.y + 1 == y)
                direct = Direction.DOWN;
            else if (coord.x - 1 == x)
                direct = Direction.LEFT;
            else if (coord.x + 1 == x)
                direct = Direction.RIGHT;
            if (Ranges.inRange(new Coord(x, y)))
            {
                if (!(gameField[y,x] is Wall))
                {
                    if (gameField[y, x] is Coin)
                    {
                        coinCount++;
                        gameField[y, x] = new Emptiness();
                    }
                    
                    if (gameField[y,x] is Key)
                    {
                        backPack.Add(char.ToUpper(gameField[y,x].sym));
                        gameField[y, x] = new Emptiness();
                    }

                    if (!(gameField[y,x] is Door))
                    {
                        coord.x = x;
                        coord.y = y;
                    }
                    else if (backPack.Contains(gameField[y,x].sym))
                    {
                        coord.x = x;
                        coord.y = y;
                        gameField[y, x] = new Emptiness();
                    }
                }
            }
        }

        public List<char> getBackPack()
        {
            return backPack;
        }

        public void setBackPack(List<char> backpack)
        {
            this.backPack = backpack;
        }
        public void clearBackpack()
        {
            backPack.Clear();
        }
        
        public override char getElement()
        {
            return sym;
        }

        public void printBackpack(int COLS)
        {
            if (backPack.Count != 0)
            {
                int tmp = COLS / (backPack.Count + 1);
                for (int i = 0; i < backPack.Count; i++)
                {
                    for (int j = 0; j < tmp; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(backPack[i]);
                }
            }
            Console.WriteLine();
        }
    }
}
