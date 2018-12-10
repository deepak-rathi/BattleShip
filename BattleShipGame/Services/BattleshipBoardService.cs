using BattleShipGame.Constants;
using BattleShipGame.Models;
using System.Collections.Generic;

namespace BattleShipGame.Services
{
    internal class BattleshipBoardService : IBattleshipBoardService
    {
        private BattleshipBoard _battleshipBoard;

        public BattleshipBoardService(BattleshipBoard battleshipBoard)
        {
            _battleshipBoard = battleshipBoard;
        }

        public BattleshipBoardService()
        {
            _battleshipBoard = new BattleshipBoard();
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _battleshipBoard.Players;
        }

        public string GetCurrentInput()
        {
            return _battleshipBoard.CurrentInput;
        }

        public BattleshipBoard GetBattleshipBoard()
        {
            if (_battleshipBoard.Players == null)
            {
                _battleshipBoard.Players = GeneratePlayers();
            }

            if (string.IsNullOrEmpty(_battleshipBoard.CurrentInput))
                _battleshipBoard.CurrentInput = AppConstants.BattleAreaWidthAndHeight;

            return _battleshipBoard;
        }

        private IList<Player> GeneratePlayers()
        {
            return new List<Player>() { new Player() { }, new Player() { } };
        }
    }
}