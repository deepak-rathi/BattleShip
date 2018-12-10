using System;
using System.Collections.Generic;

namespace BattleShipGame
{
    internal class GamePlay : IGamePlay
    {
        internal event EventHandler<GamePlayArgs> GamePlayCompleted;

        private IGamePlayBoardService _gamePlayBoardService;
        private IBattleshipBoardService _battleshipBoardService;

        public GamePlay(IGamePlayBoardService gamePlayBoardService, IBattleshipBoardService battleshipBoardService)
        {
            _gamePlayBoardService = gamePlayBoardService;
            _battleshipBoardService = battleshipBoardService;
        }

        public void Start()
        {
            var battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            var players = _battleshipBoardService.GetPlayers();

            _gamePlayBoardService.GenerateGamePlayBoard(battleshipBoard, players);
            _gamePlayBoardService.PlayGame();
            _gamePlayBoardService.FindWinner();

            OnGamePlayCompleted(_gamePlayBoardService.GetGameOutput());
        }
                
        protected virtual void OnGamePlayCompleted(IEnumerable<string> output)
        {
            GamePlayCompleted?.Invoke(this, new GamePlayArgs() { Output = output });
        }
    }
}