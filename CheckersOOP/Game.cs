using System.Security.AccessControl;
using System.Security.Cryptography;

namespace CheckersOOP
{
    abstract public class Game
    {
        #region Data Members
        private TeamColor _firstToMove;
        private TeamColor _currentMove;
        private TeamColor _playerTeam;
        private int _turnNumber;
        private Board _board;
        #endregion

        //protected sets
        #region Properties
        public TeamColor FirstToMove
        {
            get
            {
                return _firstToMove;
            }
            protected set
            {
                if (!Enum.IsDefined(typeof(TeamColor), value))
                {
                    throw new ArgumentOutOfRangeException("Invalid Team Color");
                }
                _firstToMove = value;
            }

        }

        public TeamColor CurrentMove
        {
            get
            {
                return _currentMove;
            }
            protected set
            {
                if (!Enum.IsDefined(typeof(TeamColor), value))
                {
                    throw new ArgumentOutOfRangeException("Invalid Team Color");
                }
                _currentMove = value;
            }

        }

        public TeamColor PlayerTeam
        {
            get
            {
                return _playerTeam;
            }
            protected set
            {
                if (!Enum.IsDefined(typeof(TeamColor), value))
                {
                    throw new ArgumentOutOfRangeException("Invalid Team Color");
                }
                _playerTeam = value;
            }
        }

        public int TurnNumber
        {
            get
            {
                return _turnNumber;
            }
            protected set
            {
                _turnNumber = value;
            }
        }

        //private set
        public Board? Board
        {
            get
            {
                return _board;
            }
            private set
            {
               ArgumentNullException.ThrowIfNull(value, nameof(Board));
            }
        }
        #endregion

        #region Constructor

        protected Game(TeamColor playerTeam, TeamColor firstToMove, Board board)
        {
            FirstToMove = firstToMove;
            CurrentMove = firstToMove;
            PlayerTeam = playerTeam;
            TurnNumber = 1;

            Board = board;
            Board.PlayerTeam = PlayerTeam;
            Board.SetUpPieces();

        }
        #endregion

        #region Abstract Methods
        public abstract GameState State();

        public abstract void NextTurn();
        #endregion

        public class StandardGame(TeamColor playerTeam) : Game(playerTeam, TeamColor.Dark, new StandardBoard())
        {
            public override GameState State()
            {
                ArgumentNullException.ThrowIfNull(Board, nameof(Board));
                ArgumentNullException.ThrowIfNull(Board.Tiles, nameof(Board.Tiles));

                bool blackPiecePresent = false;
                bool whitePiecePresent = false;

                foreach(Piece? piece in Board.Tiles)
                {
                    if(piece != null)
                    {
                        if(piece.Team == TeamColor.Dark)
                        {
                            blackPiecePresent = true;
                        }
                        else
                        {
                            whitePiecePresent = true;
                        }
                    }
                    
                    if(blackPiecePresent && whitePiecePresent)
                    {
                        return GameState.Ongoing;
                    }
                }

                #region Piece Check

                if (blackPiecePresent)
                {
                    if(PlayerTeam == TeamColor.Dark)
                    {
                        return GameState.Win;
                    }

                    return GameState.Lose;
                }

                if(whitePiecePresent)
                {
                    if(PlayerTeam == TeamColor.Light)
                    {
                        return GameState.Win;
                    }

                    return GameState.Lose;
                }

                return GameState.Draw;
                #endregion
            }

            public override void NextTurn()
            {
                if(CurrentMove != FirstToMove)
                {
                    CurrentMove = FirstToMove;
                    TurnNumber++;
                }
                else if (CurrentMove == TeamColor.Dark) 
                {
                    CurrentMove = TeamColor.Light;
                }
                else
                {
                    CurrentMove = TeamColor.Dark;
                }
            }
        }

        public void PlayerTurn(Piece piece, Move move)
        {
            //TODO
            //check if the piece belongs to the correct team

            //check if the piece has the move in it's list

            //attempt the move through board and catch any problems
            //if problems throw exception with userfriendly exception

            //check gamestate is ongoing
                //call next turn
                //call CPU Turn
        }

        private void CPUTurn()
        {
            //TODO
            //Move dictated by AI you create here

            //check game state is ongoing
                //NextTurn()
        }
    }
}
