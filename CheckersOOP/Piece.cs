using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersOOP
{
    abstract public class Piece
    {
        private TeamColor _team;
        private PieceType _pieceType;
        private List<Move> _moves = [];
        private bool _friendlyFire = false;

        public TeamColor Team
        {
            get
            {
                return _team;
            }
            private set
            {
                if (!Enum.IsDefined(typeof(TeamColor), value))
                {
                    throw new ArgumentOutOfRangeException($"{value} Not a valid Team Color");
                }
                _team = value;
            }
        }

        public PieceType PieceType
        {
            get
            {
                return _pieceType;
            }
            private set
            {
                if(!Enum.IsDefined(typeof(PieceType), value))
                {
                    throw new ArgumentOutOfRangeException($"{value} Not a valid Piece Type");
                }

                _pieceType = value;
            }
        }

        public List<Move> Moves
        {
            get
            {
                return _moves;
            }
            private set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Moves cannot be null");
                }

                _moves = value;
            }
        }

        public bool FriendlyFire
        {
            get
            {
                return _friendlyFire;
            }
            private set
            {
                _friendlyFire = value;
            }
        }

        protected Piece(TeamColor team, PieceType pieceType, List<Move> moves, bool friendlyFire)
        {
            Team = team;
            PieceType = pieceType;
            Moves = moves;
            FriendlyFire = friendlyFire;
        }
    }

    public class StandardPiece(TeamColor team) : Piece(team, PieceType.Standard, [
        new Move(-1, -1),
        new Move(1, -1),
        new Capture(-2, -2, -1, -1),
        new Capture(2, -2, 1, -1)
    ], false);

    public class KingPiece(TeamColor team) : Piece(team, PieceType.King, [
        new Move(-1, -1),
        new Move(1, -1),
        new Move(1, 1),
        new Move(-1, 1),
        new Capture(-2, -2, -1, -1),
        new Capture(2, -2, 1, -1),
        new Capture(-2, 2, -1, 1),
        new Capture(2, 2, 1, 1)
    ],false);
}