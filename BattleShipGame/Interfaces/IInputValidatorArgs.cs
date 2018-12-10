using System.Collections.Generic;

namespace BattleShipGame
{
    internal interface IInputValidatorArgs
    {
        string Key { get; set; }
        IList<string> Input { get; set; }
        bool IsInputValid { get; set; }
    }
}