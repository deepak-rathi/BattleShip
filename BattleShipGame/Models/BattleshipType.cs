namespace BattleShipGame.Models
{
    public enum BattleshipType : ushort
    {
        TypeP = 1,  //Type P ships can be destroyed by a single hit in each of their cells
        TypeQ = 2   //Type Q ships require 2 hits of their cells
    }
}