using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CheckersOOP
{
    public class Move
    {
        private int _targetX;
        private int _targetY;
        private MoveType _moveType;

        public int TargetX
        {
            get
            {
                return _targetX;
            }
            private set
            {
                TargetX = value;
            }
        }

        public int TargetY
        {
            get
            {
                return _targetY;
            }
            private set
            {
                TargetY = value;
            }
        }

        public MoveType MoveType
        {
            get
            {
                return _moveType;
            }
            private set
            {
                if(!Enum.IsDefined(typeof(MoveType), value))
                {
                    throw new ArgumentOutOfRangeException("Not a valid move type");
                }

                MoveType = value;
            }
        }

       public Move(int targetX, int targetY)
       {
            TargetX = targetX;
            TargetY = targetY;
            MoveType = MoveType.Standard;
       }

        protected Move(int targetX, int targetY, MoveType moveType)
        {
            TargetX = targetX;
            TargetY = targetY;
            MoveType = moveType;
        }

        
    }

    public class Capture : Move
    {
        private int _captureX;
        private int _capturedY;

        public int CaptureX
        {
            get
            {
                return _captureX;
            }
            private set
            {
                CaptureX = value;
            }
        }

        public int CaptureY
        {
            get
            {
                return _capturedY;
            }
            private set
            {
                CaptureY = value;
            }
        }

        public Capture(int targetX, int targetY, int captureX, int captureY) : base(targetX, targetY, MoveType.Capture) 
        {
            CaptureX = captureX;
            CaptureY = captureY;
        }

    }
}
