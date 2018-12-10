using System.Collections.Generic;

namespace BattleShipGame.Models
{
    internal class Player
    {
        public IList<Battleship> Battleships { get; set; } = new List<Battleship>();
        public IList<string> TargetPositionString { get; set; } = new List<string>();
    }
}