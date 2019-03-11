using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.elements;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Remoting.Channels;


namespace ConsoleApp2
{
    class Field
    {
        public int ROWS;
        public int COLS;
        public Matrix gameField;
        public char[,] matrix;
        public Coord exitPoint = new Coord(1, 1);
        public Coord playerPoint = new Coord(-1,-1);
        public Coord enemyPoint = new Coord(-1,-1);
        public Field load_lvl(string name)
        {
            Field field = new Field();
            field = JsonConvert.DeserializeObject<Field>(File.ReadAllText(name));
            Ranges.setSize(new Coord(field.COLS,field.ROWS));
            field.gameField = new Matrix(new Emptiness());
            for (int i = 0; i < field.ROWS; i++)
            {
                for (int j = 0; j < field.COLS; j++)
                {
                    switch (field.matrix[i, j])
                    {
                        case '#': field.gameField[i, j] = new Wall(); break;
                        case 'a': field.gameField[i, j] = new Key('a'); break;
                        case 'A': field.gameField[i, j] = new Door('a'); break;
                        case 'b': field.gameField[i, j] = new Key('b'); break;
                        case 'B': field.gameField[i, j] = new Door('b'); break;
                        case 'c': field.gameField[i, j] = new Key('c'); break;
                        case 'C': field.gameField[i, j] = new Door('c'); break;
                        case '0': field.gameField[i, j] = new Coin(); break;
                        case 'x': field.gameField[i, j] = new Trap(); break;
                        default: field.gameField[i, j] = new Emptiness(); break;
                    }
                }
            }
            return field;
        }

        public char[,] convert()
        {
            int x = Ranges.getSize().x;
            int y = Ranges.getSize().y;
            matrix = new char[x, y];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    matrix[i, j] = gameField[i,j].sym;
                }
            }
            return matrix;
        }
        public void print(Player player,Enemy enemy, int difficult)
        {
            int x = Ranges.getSize().x;
            int y = Ranges.getSize().y;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    if (i == player.Coord.y && j == player.Coord.x)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(player.sym);
                    }
                    else{
                        if ((enemy.Coord.x == j && enemy.Coord.y == i && difficult == 1)||
                            (difficult == 2 && enemy.Coord.y >= player.Coord.y - 1 && enemy.Coord.y <= player.Coord.y + 1 &&
                             enemy.Coord.x >= player.Coord.x - 1 && enemy.Coord.x <= player.Coord.x + 1 && enemy.Coord.x == j && enemy.Coord.y == i))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(enemy.sym);
                        }
                        else
                        {
                            switch (difficult)
                            {
                                case 1:
                                    printSym(gameField[i, j].sym);
                                    break;
                                case 2:
                                    if (i >= player.Coord.y - 1 && i <= player.Coord.y + 1 &&
                                        j >= player.Coord.x - 1 && j <= player.Coord.x + 1)
                                        printSym(gameField[i, j].sym);
                                    else
                                        Console.Write(" ");
                                    break;
                            }
                        }
                    }

            }
                Console.WriteLine();
            }
            player.printBackpack(COLS);
        }

        void printSym(char s)
        {
            switch (s)
            {
                case '#':  Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(s);
                    break;
                case 'a': Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(s);
                    break;
                case 'A':  Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(s);
                    break;
                case 'b':  Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(s);
                    break; 
                case 'B':  Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(s);
                    break;
                case 'c':  Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(s);
                    break;
                case 'C':  Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(s);
                    break;
                case '0':  Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(s);
                    break;
                case 'x':  Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(s);
                    break;
                case ' ':
                    Console.Write(s);
                    break;
            }
        }

        public Coord destroyWall(Player player)
        {
            Coord tmp = new Coord(0,0);
            switch (player.Direct)
            {
                case Direction.UP:
                    if (!isBorder(new Coord(player.Coord.x,player.Coord.y - 1)))
                    {
                        tmp = new Coord(player.Coord.x, player.Coord.y - 1);
                        gameField[player.Coord.y - 1, player.Coord.x] = new Emptiness();
                    }
                    else
                        return new Coord(0,0);
                    break;
                case Direction.DOWN:
                    if (!isBorder(new Coord(player.Coord.x, player.Coord.y + 1)))
                    {
                        tmp = new Coord(player.Coord.x, player.Coord.y + 1);
                        gameField[player.Coord.y + 1, player.Coord.x] = new Emptiness();
                    }
                    else
                        return new Coord(0,0);
                    break;
                case Direction.LEFT:
                    if (!isBorder(new Coord(player.Coord.x - 1, player.Coord.y)))
                    {
                        tmp = new Coord(player.Coord.x - 1, player.Coord.y);
                        gameField[player.Coord.y, player.Coord.x - 1] = new Emptiness();
                    }
                    else
                        return new Coord(0,0);
                    break;
                case Direction.RIGHT:
                    if (!isBorder(new Coord(player.Coord.x + 1, player.Coord.y)))
                    {
                        tmp = new Coord(player.Coord.x + 1, player.Coord.y);
                        gameField[player.Coord.y, player.Coord.x + 1] = new Emptiness();
                    }
                    else
                        return new Coord(0,0);
                    break;
            }
            return tmp;
        }

        public bool isBorder(Coord coord)
        {
            return coord.x == 0 || coord.x == COLS - 1 ||
                   coord.y == 0 || coord.y == ROWS - 1;
            
        }

        public Matrix getField()
        {
            return gameField;
        }
    }

}
