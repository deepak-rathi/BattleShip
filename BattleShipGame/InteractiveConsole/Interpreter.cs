using BattleShipGame.Constants;
using BattleShipGame.Extensions;
using BattleShipGame.Models;
using System;
using System.Linq;

namespace BattleShipGame.InteractiveConsole
{
    internal class Interpreter
    {
        private IBattleshipBoardService _battleshipBoardService;
        private IGamePlayBoardService _gamePlayBoardService;

        private InputValidator _inputValidator;
        private BattleshipBoard _battleshipBoard;
        private GamePlay _gamePlay;
        
        public Interpreter(IBattleshipBoardService battleshipBoardService, IGamePlayBoardService gamePlayBoardService)
        {
            _battleshipBoardService = battleshipBoardService;
            _battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            _gamePlayBoardService = gamePlayBoardService;

            _inputValidator = new InputValidator(battleshipBoardService);
            _inputValidator.InputValidated += InputValidator_InputValidated;

            _gamePlay = new GamePlay(_gamePlayBoardService, _battleshipBoardService);
            _gamePlay.GamePlayCompleted += GamePlay_GamePlayCompleted;
        }

        public ICommand Parse(string commandString)
        {
            // Parse string and create Command
            var commandParts = commandString.Split(' ').ToList();
            var commandName = commandParts[0];
            var args = commandParts.Skip(1).ToList(); // the arguments is after the command

            switch (commandName)
            {
                case AppConstants.Exit: return new ExitCommand();

                default:
                    if (string.Compare(args.First(), AppConstants.Exit, true) == 0)
                        return new ExitCommand();

                    _inputValidator.Validate(commandName + ' ', args);
                    return new ContinueCommand();
            }
        }

        private void InputValidator_InputValidated(object sender, InputValidatorArgs e)
        {
            if (!e.IsInputValid)
            {
                Console.WriteLine(AppConstants.InvalidInputErrorMessage);
                Console.Write(e.Key.ReplaceUnderscoreWithBlankSpace());
            }
            else
            {
                switch (e.Key)
                {
                    case AppConstants.BattleAreaWidthAndHeight:
                        Console.Write(AppConstants.NumberOfBattleship.ReplaceUnderscoreWithBlankSpace());
                        _battleshipBoard.CurrentInput = AppConstants.NumberOfBattleship;
                        break;

                    case AppConstants.NumberOfBattleship:

                        Console.Write(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2.ReplaceUnderscoreWithBlankSpace().Replace(":", AppConstants.ForExampleShipForPlayer1));
                        _battleshipBoard.CurrentInput = AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2;
                        _battleshipBoard.CurrentInputShip--;
                        break;

                    case AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2:
                        if (_battleshipBoard.CurrentInputShip >= 1)
                        {
                            Console.Write(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2.ReplaceUnderscoreWithBlankSpace().Replace(":", AppConstants.ForExampleShipForPlayer2));
                            //BattleshipBoard.CurrentInput = "Enter_type_of_battleship_its_dimensions(width_and_height)_and_coordinates_for_player1_and_player2: ";
                            _battleshipBoard.CurrentInputShip--;
                        }
                        else
                        {
                            Console.Write(AppConstants.SequenceOfMissileTargetLocationPlayer1.ReplaceUnderscoreWithBlankSpace().Replace(":", AppConstants.ForExampleTargetForPlayer1));
                            _battleshipBoard.CurrentInput = AppConstants.SequenceOfMissileTargetLocationPlayer1;
                        }
                        break;

                    case AppConstants.SequenceOfMissileTargetLocationPlayer1:
                        Console.Write(AppConstants.SequenceOfMissileTargetLocationPlayer1.ReplaceUnderscoreWithBlankSpace().Replace(":", AppConstants.ForExampleTargetForPlayer2));
                        _battleshipBoard.CurrentInput = AppConstants.SequenceOfMissileTargetLocationPlayer2;
                        break;

                    case AppConstants.SequenceOfMissileTargetLocationPlayer2:
                        _battleshipBoard.CurrentInput = AppConstants.BattleAreaWidthAndHeight;
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine(AppConstants.Output);
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine();
                        //show output
                        _gamePlay.Start();
                        break;

                    default:
                        //something went wrong
                        foreach (var player in _battleshipBoard.Players)
                        {
                            player.Battleships.Clear();
                            player.TargetPositionString.Clear();
                        }
                        Console.Write(AppConstants.BattleAreaWidthAndHeight.ReplaceUnderscoreWithBlankSpace());
                        _battleshipBoard.CurrentInput = AppConstants.BattleAreaWidthAndHeight;
                        break;
                };
            }
        }

        private void GamePlay_GamePlayCompleted(object sender, GamePlayArgs e)
        {
            foreach (var outputString in e.Output)
                Console.WriteLine(outputString);
        }
    }
}