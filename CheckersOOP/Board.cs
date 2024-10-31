using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersOOP
{
    abstract public class Board
    {
        #region Data Members

        //Const
        private const int _minHeight = 1;
        private const int _minWidth = 1;

        //Data Members
        private int _width;
        private int _height;
        private Piece?[,]? _tiles;
        private TeamColor? _team;
        #endregion

        #region Properties
        public int Width
        {
            get
            {
                return _width;
            }
            private set
            {
                if (value < _minWidth)
                {
                    throw new ArgumentOutOfRangeException("Board width must be a minimum of 1");
                }
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            private set
            {
                if (value < _minHeight)
                {
                    throw new ArgumentOutOfRangeException("Board height must a minimum of 1");
                }

                _height = value;
            }
        }

        public Piece?[,]? Tiles
        {
            get
            {
                return _tiles;
            }
            private set
            {
                if (_tiles == null)
                {
                    throw new ArgumentNullException("Tiles cannot be null");
                }
                _tiles = value;
            }
        }

        public TeamColor?  PlayerTeam
        {
            get
            {
                return _team;
            }
            set
            {
                if(_team != null)
                {
                    throw new InvalidOperationException("Team cannot be reassigned");
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Team cannot be null");
                }

                if(!Enum.IsDefined(typeof(TeamColor), value))
                {
                    throw new ArgumentOutOfRangeException("Not a valid team");
                }

                _team = value;
            }
        }
        #endregion

        #region Constructor
        protected Board(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Piece?[Width, Height];
        }
        #endregion

        #region Method
        public bool InBounds(int col, int row)
        {
            bool inBounds = true;

            if(col < 0 || col >= Height)
            {
                inBounds = false;
            }
            else if(row < 0 || row >= Width)
            {
                inBounds = false;
            }
            return inBounds;
            
        }

        public void MovePiece(int originCol, int originRow, Move move)
        {
            if(Tiles == null)
            {
                throw new ArgumentNullException("Board does not exist");
            }

            int targetRow;
            int targetCol;

            #region Direction Handling
            if(PlayerTeam == null)
            {
                throw new ArgumentNullException("Player Team cannot be null");
            }
            
            if(Tiles[originCol, originRow]?.Team == PlayerTeam)
            {
                targetRow = originRow - move.TargetY;
                targetCol = originCol + move.TargetX;
            }
            else
            {
                targetRow = originRow + move.TargetY;
                targetCol = originCol - move.TargetX;
            }
            #endregion

            #region Boundary Validation
            if(!InBounds(targetCol, targetRow))
            {
                throw new ArgumentOutOfRangeException("Invalid move");
            }


            if(!InBounds(originCol, originRow))
            {
                throw new ArgumentOutOfRangeException("Starting position invalid");
            }


            #endregion

            #region Movement Logic
            if (Tiles[originCol, originRow] == null)
            {
                throw new ArgumentNullException("Invalid piece selection");
            }

            if (Tiles[targetCol, targetRow] != null) 
            {
                throw new InvalidOperationException("Cannot move onto a populated tile");
            }

            #endregion

            #region Movement
            Tiles[originCol, originRow] = Tiles[targetCol, targetRow];
            Tiles[originCol, originRow] = null;
            #endregion
        }

        public void CapturePiece(int originCol, int originRow, Capture capture)
        {
            if(Tiles == null)
            {
                throw new ArgumentNullException("Board does not exist");
            }

            int targetRow;
            int targetCol;
            int captureRow;
            int captureCol;

            if(PlayerTeam == null)
            {
                throw new ArgumentNullException("Player Team cannot be null");
            }

            if (Tiles[originCol,originRow]?.Team == PlayerTeam)
            {
                targetRow = originRow - capture.TargetY;
                targetCol = originCol + capture.TargetX;
                captureRow = originRow - capture.CaptureY;
                captureCol = originCol + capture.CaptureX;
            }
            else
            {
                targetRow = originRow + capture.TargetX;
                targetCol = originCol - capture.TargetY;
                captureRow = originRow + capture.CaptureY;
                captureCol = originCol - capture.CaptureX;
            }

            #region Boundary Validation
            if(!InBounds(captureCol, captureRow))
            {
                throw new ArgumentOutOfRangeException("Invalid capture move");
            }

            if(!InBounds(targetCol, targetRow))
            {
                throw new ArgumentOutOfRangeException("Invalid movement");
            }

            if(!InBounds(originCol, originRow))
            {
                throw new ArgumentOutOfRangeException("Starting position invalid");
            }
            #endregion

            #region Movement Logic
            //target tile cannot be occupied unless it is also the capture tile
            //the capture tile cannot be null 
            //the origin tile and capture tile cannot be the same team unless friendly fire is activated
            //origin tile cannot be null
            #endregion

            #region Movement
            //Remove the capture tile move the origin to target tile unoccupy origin tile.
            #endregion
        }

        public abstract void SetUpPieces();

        #endregion
    }
}
