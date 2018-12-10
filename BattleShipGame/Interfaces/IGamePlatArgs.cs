using System.Collections.Generic;

namespace BattleShipGame
{
    public interface IGamePlatArgs
    {
        IEnumerable<string> Output { get; set; }
    }
}