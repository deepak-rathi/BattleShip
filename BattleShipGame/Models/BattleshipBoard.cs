using System.Collections.Generic;

namespace BattleShipGame.Models
{
    internal class BattleshipBoard
    {
        public ushort Width { get; set; }
        public char HeightCharacter { get; set; }
        public ushort Height { get; set; }
        public IList<Player> Players { get; set; }
        public string CurrentInput { get; set; }
        public ushort CurrentInputShip { get; set; }
    }
}