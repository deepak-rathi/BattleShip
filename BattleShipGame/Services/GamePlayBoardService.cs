using BattleShipGame.Extensions;
using BattleShipGame.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame.Services
{
    internal class GamePlayBoardService : IGamePlayBoardService
    {
        private readonly IList<GamePlayBoard> _gamePlayBoards = new List<GamePlayBoard>();
        private readonly IList<IEnumerator> _targetEnumerators = new List<IEnumerator>();
        private readonly IList<string> _outputs = new List<string>();
        private int _totalTarget = 0;

        public void GenerateGamePlayBoard(BattleshipBoard battleshipBoard, IEnumerable<Player> players)
        {
            //Generate GamePlay Board
            foreach (var player in players)
            {
                var gamePlayBoard = new GamePlayBoard()
                {
                    Board = new int[battleshipBoard.Width + 1, battleshipBoard.Height + 1],
                    Target = player.TargetPositionString,
                    IsAllShipsDistroyed = false
                };
                //fill ships on each players board
                FillBoardWithShips(gamePlayBoard.Board, player.Battleships);
                _gamePlayBoards.Add(gamePlayBoard);

                //Get enumerators for target
                _targetEnumerators.Add(player.TargetPositionString.GetEnumerator());
                _totalTarget = _totalTarget + player.TargetPositionString.Count;
            }
        }

        public void FindWinner()
        {
            //compute if all ships are distroyed
            foreach (var gamePlayBoard in _gamePlayBoards)
            {
                gamePlayBoard.IsAllShipsDistroyed = gamePlayBoard.Board.Cast<int>().All(positionValue => positionValue == 0);
            }

            if (_gamePlayBoards.Count(board => board.IsAllShipsDistroyed) >= 1)
            {
                //one or more winners
                //since this code is written to support multiplayer battleship in future
                //outputs.Add(string.Format("Player {0} lost the battle", gamePlayBoards.First(board => board.IsAllShipsDistroyed).Id + 1));
                //but for now we only have two players
                var winner = _gamePlayBoards.First(board => board.IsAllShipsDistroyed).Id + 2;
                if (winner == 3)
                    _outputs.Add(string.Format("Player {0} won the battle", 1));
                else if (winner == 2)
                    _outputs.Add(string.Format("Player {0} won the battle", 2));
            }
            else
            {
                //no winners, call it a draw
                _outputs.Add("Battle was draw!");
            }
        }

        public void PlayGame()
        {
            while (_totalTarget > 0)
            {
                //GamePlayBoard enumerator
                var boardEnumerator = _gamePlayBoards.GetEnumerator();
                //first player board
                boardEnumerator.MoveNext();
                ushort playerNumber = 1;

                bool winnerPlayerTurn = true;

                var allPlayersTargets = _targetEnumerators.GetEnumerator();
                while (allPlayersTargets.MoveNext())
                {
                    var targetEnumerator = allPlayersTargets.Current;
                    if (boardEnumerator.MoveNext())
                    {
                        //second board or higher
                        Play(ref targetEnumerator, ref boardEnumerator, ref winnerPlayerTurn, playerNumber);
                    }
                    else
                    {
                        //last board reached, reset board to first
                        boardEnumerator = _gamePlayBoards.GetEnumerator();
                        boardEnumerator.MoveNext();

                        //first player baord
                        Play(ref targetEnumerator, ref boardEnumerator, ref winnerPlayerTurn, playerNumber);
                    }
                    playerNumber++;
                }
            }
        }

        private void Play(ref IEnumerator targetEnumerator, ref IEnumerator<GamePlayBoard> boardEnumerator, ref bool winnerPlayerTurn, ushort playerNumber)
        {
            while (winnerPlayerTurn)
            {
                //for each player in game
                if (targetEnumerator.MoveNext())
                {
                    if (UInt16.TryParse(((string)targetEnumerator.Current).Substring(1), out ushort y))
                    {
                        var previewValueOfTargetPosition = boardEnumerator.Current.Board[(ushort)(((string)targetEnumerator.Current).FirstCharUpper() - 64), y];
                        if (previewValueOfTargetPosition > 0)
                        {
                            //ship present at the target position
                            boardEnumerator.Current.Board[(ushort)(((string)targetEnumerator.Current).FirstCharUpper() - 64), y] -= 1;
                            _outputs.Add(string.Format("Player {0} fires a missile with target {1} which got {2}", playerNumber, (string)targetEnumerator.Current, "hit"));
                            winnerPlayerTurn = true;
                        }
                        else
                        {
                            //ship not present at the target position
                            _outputs.Add(string.Format("Player {0} fires a missile with target {1} which got {2}", playerNumber, (string)targetEnumerator.Current, "miss"));
                            winnerPlayerTurn = false;
                        }
                    }
                    _totalTarget--;
                }
                else
                {
                    _outputs.Add(string.Format("Player {0} has no more missile left to launch", playerNumber));
                    winnerPlayerTurn = false;
                }
            };
            winnerPlayerTurn = true;
        }

        public void FillBoardWithShips(int[,] board, IList<Battleship> battleships)
        {
            foreach (var ship in battleships)
            {
                int width = ship.Width;
                int height = ship.Height;
                int x = ship.PositionX;
                int y = ship.PositionY;
                while (width > 0 || height > 0)
                {
                    board[x, y] = (ushort)ship.Type;
                    width--;
                    if (width > 0)
                        y++;
                    height--;
                    if (height > 0)
                        x++;
                }
            }
        }

        public IEnumerable<string> GetGameOutput()
        {
            return _outputs;
        }
    }
}