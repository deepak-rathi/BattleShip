using System.Collections.Generic;

namespace BattleShipGame
{
    public interface IInputValidator
    {
        void Validate(string key, IList<string> input);
    }
}