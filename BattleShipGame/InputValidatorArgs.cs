using System;
using System.Collections.Generic;

namespace BattleShipGame
{
    internal class InputValidatorArgs : EventArgs, IInputValidatorArgs
    {
        public string Key { get; set; }
        public IList<string> Input { get; set; }
        public bool IsInputValid { get; set; }
    }
}