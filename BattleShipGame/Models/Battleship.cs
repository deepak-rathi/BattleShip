using System;

namespace BattleShipGame.Models
{
    internal class Battleship : ICloneable
    {
        public BattleshipType Type { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public char HeightCharacter { get; set; }
        public ushort PositionX { get; set; }
        public ushort PositionY { get; set; }
        public string PositionString { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}