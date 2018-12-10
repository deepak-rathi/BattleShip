using BattleShipGame.Models;
using System.Collections.Generic;

namespace BattleShipGame
{
    interface IGamePlayBoardService
    {
        void FillBoardWithShips(int[,] board, IList<Battleship> battleships);
        void GenerateGamePlayBoard(BattleshipBoard battleshipBoard, IEnumerable<Player> players);
        void PlayGame();
        void FindWinner();
        IEnumerable<string> GetGameOutput();
    }
}