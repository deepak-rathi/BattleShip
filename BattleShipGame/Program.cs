using BattleShipGame.InteractiveConsole;
using BattleShipGame.Services;
using System;

namespace BattleShipGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Battleship game!");
            Console.WriteLine("There are two type of ships - type P and type Q. Type P ships can be destroyed by a single hit in each of their cells and type Q ships require 2 hits in each of their cells.");
            Console.WriteLine("A ship is considered destroyed when all of its cells are destroyed. The player who destroys all the ships of other player first wins the game.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Rules:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1 <= Width of Battle area(M') <= 9");
            Console.WriteLine("A <= Height of Battle area(N') <= Z");
            Console.WriteLine("1 <= Number of battleships <= M'*N'");
            Console.WriteLine("Type of ship = {'P','Q'}");
            Console.WriteLine("1 <= Width of battleship <= M'");
            Console.WriteLine("A <= Height of battleship <= N'");
            Console.WriteLine("1 <= X coordinate of ship <= M'");
            Console.WriteLine("A <= Y coordinate of ship <= N'");
            Console.WriteLine("Note: Use space ' ' to seperate your input.");
            Console.WriteLine("Type 'EXIT' to quit the game.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

            Console.Write("Enter width and height of battle area (for example: 5 E): ");

            //TODO:Implement a IOC for dependency injection and object life management
            var battleshipBoardService = new BattleshipBoardService();
            var gamePlayBoardService = new GamePlayBoardService();
            var interpreter = new Interpreter(battleshipBoardService, gamePlayBoardService);

            var exit = false;
            while (exit == false)
            {
                var command = interpreter.Parse(battleshipBoardService.GetCurrentInput() + Console.ReadLine());
                exit = command.Execute();
            }
        }
    }
}