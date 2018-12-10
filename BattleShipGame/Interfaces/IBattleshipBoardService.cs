using BattleShipGame.Models;
using System.Collections.Generic;

namespace BattleShipGame
{
    internal interface IBattleshipBoardService
    {
        IEnumerable<Player> GetPlayers();

        BattleshipBoard GetBattleshipBoard();
    }
}