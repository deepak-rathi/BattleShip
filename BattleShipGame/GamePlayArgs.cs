using System;
using System.Collections.Generic;

namespace BattleShipGame
{
    internal class GamePlayArgs : EventArgs, IGamePlatArgs
    {
        public IEnumerable<string> Output { get; set; }
    }
}