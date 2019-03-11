using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.elements;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace ConsoleApp2
{
    class MainClass
    {
        static void Main(string[] args)
        {
            int difficult = 1 ;
            int menu = 1;
            string level;
            bool tmp;
            ConsoleKeyInfo key;
            Field gameField = new Field();
            do
            {
                Console.Clear();
                Console.Write("New Game"); if(menu == 1) Console.WriteLine(" <-"); else Console.WriteLine();
                Console.Write("Choose difficult");if(menu == 2) Console.WriteLine(" <-"); else Console.WriteLine();
                Console.Write("Exit Game");if(menu == 3) Console.WriteLine(" <-"); else Console.WriteLine();
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (menu > 1) menu--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (menu < 3) menu++;
                        break;
                    case ConsoleKey.Enter:
                        switch (menu)
                        {
                            case 1:
                                Console.Clear();
                                Console.Write("Enter your name ");
                                Console.ReadLine();
                                int lvl = 1;
                                do
                                {
                                    Console.Clear();
                                    Console.Write("Level 1"); if (lvl == 1) Console.WriteLine(" <-"); else Console.WriteLine();
                                    Console.Write("Level 2"); if (lvl == 2) Console.WriteLine(" <-"); else Console.WriteLine();
                                    Console.Write("Return"); if (lvl == 3) Console.WriteLine(" <-"); else Console.WriteLine();
                                    key = Console.ReadKey();
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.UpArrow:
                                            if (lvl > 1) lvl--;
                                            break;
                                        case ConsoleKey.DownArrow:
                                            if (lvl < 3) lvl++;
                                            break;
                                        case ConsoleKey.Enter:
                                            switch (lvl)
                                            {
                                                case 1:
                                                    level = "lvl_1.json";
                                                    game();
                                                    break;
                                                case 2:
                                                    level = "lvl_2.json";
                                                    game();
                                                    break;
                                                case 3:
                                                    lvl = 0;
                                                    break;
                                            }
                                            break;
                                    }
                                } while (lvl != 0);
                                break;
                            case 2:
                                tmp = true;
                                do
                                {
                                    Console.Clear();
                                    Console.Write("Easy"); if (difficult == 1) Console.WriteLine(" <-"); else Console.WriteLine();
                                    Console.Write("Hard"); if (difficult == 2) Console.WriteLine(" <-"); else Console.WriteLine();
                                    key = Console.ReadKey();
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.UpArrow:
                                            if (difficult > 1) difficult--;
                                            break;
                                        case ConsoleKey.DownArrow:
                                            if (difficult < 2) difficult++;
                                            break;
                                        case ConsoleKey.Enter:
                                            tmp = false;
                                            break;
                                    }
                                } while (tmp);

                                break;
                            case 3:
                                return;
                            
                        }
                        break;
                }
            } while (true);
            

            void game()
            {
                gameField = gameField.load_lvl(level);
                Player player = new Player(gameField.playerPoint.x, gameField.playerPoint.y);
                Enemy enemy = new Enemy(gameField.enemyPoint.x,gameField.enemyPoint.y);
                int min = 0;
                int sec = 0;
                int ms = 0;
                int turnCount = 0;
                bool isWin = true;
                Coord stack = new Coord(0,0);
                bool isDestroyed = false;
                do
                {
                    Console.Clear();
                    gameField.print(player, enemy, difficult);
                    if ((player.Coord.x == enemy.Coord.x &&
                        player.Coord.y == enemy.Coord.y) ||
                        gameField.gameField[player.Coord.y,player.Coord.x] is Trap)
                    {
                        isWin = false;
                        break;
                    }

                    if (Console.KeyAvailable)
                    {
                        key = Console.ReadKey();
                        int x = player.Coord.x;
                        int y = player.Coord.y;
                        switch (key.Key)
                        {
                            case ConsoleKey.UpArrow:
                                player.setCoord(x, y - 1, gameField.getField());
                                break;
                            case ConsoleKey.DownArrow:
                                player.setCoord(x, y + 1, gameField.getField());
                                break;
                            case ConsoleKey.LeftArrow:
                                player.setCoord(x - 1, y, gameField.getField());
                                break;
                            case ConsoleKey.RightArrow:
                                player.setCoord(x + 1, y, gameField.getField());
                                break;
                            case ConsoleKey.E:
                                if (isDestroyed == false)
                                {
                                    stack = gameField.destroyWall(player);
                                    isDestroyed = true;
                                }
                                break;
                        }

                        turnCount++;
                    }
                    Console.WriteLine($"{min} : {sec}");
                    ms++;
                    if (ms == 10)
                    {
                        sec++;
                        ms = 0;
                        if (sec % 2 == 0 && isDestroyed)
                        {
                                gameField.gameField[stack.y, stack.x] = new Wall();
                                isDestroyed = false;
                        }
                        enemy.move(gameField.getField());
                    }
                    if (sec == 60)
                    {
                        min++;
                        sec = 0;
                    }
                    
                    System.Threading.Thread.Sleep(50);
                } while (!(gameField.exitPoint.x == player.Coord.x && gameField.exitPoint.y == player.Coord.y));
                
                Console.Clear();
                gameField.print(player, enemy, 1);
                if (isWin)
                {
                    Console.WriteLine("You Win!");
                    Console.WriteLine($"You score : {player.CoinCount}");
                    Console.WriteLine($"Number of Steps : {turnCount}");
                }
                else
                {
                    Console.WriteLine("You lose!");
                    Console.WriteLine($"You score : {player.CoinCount}");
                    Console.WriteLine($"Number of Steps : {turnCount}");
                }
                player.clearBackpack();
                Console.Read();
            }
        }
    }
}
