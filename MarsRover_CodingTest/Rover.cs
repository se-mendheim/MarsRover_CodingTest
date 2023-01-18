
namespace DealerOn_Coding_Test
{
    public class Rover
    {
        // plateau x bound set by the user
        private int _xBound;
        public int XBound
        {
            get { return _xBound; }
            set { _xBound = value; }
        }

        // plateau y bound set by the user
        private int _yBound;
        public int YBound
        {
            get { return _yBound; }
            set { _yBound = value; }
        }

        // rover x position initially set by the user and changed by actions
        private int _xPosition;
        public int XPosition
        {
            get { return _xPosition; }
            set { _xPosition = value; }
        }

        // rover y position initially set by the user and changed by actions
        private int _yPosition;
        public int YPosition
        {
            get { return _yPosition; }
            set { _yPosition = value; }
        }

        // rover z facing direction initially set by the user and changed by actions
        private int _zFacing;
        public int ZFacing
        {
            get { return _zFacing; }
            set { _zFacing = value; }
        }

        /// <summary>
        /// Method <c>TurnRight</c> turns the rover cardinally to the right one time (i.e. N -> E | W -> N)
        /// 
        /// NOTE: direction facing is calculated by numbers instead of characters (i.e. 0123 not NWSE)
        ///		0 = North | 1 = West | 2 = South | 3 = East
        ///	this is based on the "cardinalDirections" array declared in the "MissionControl" class
        /// </summary>
        public void TurnRight()
        {
            // if the rover is facing North (0) change the facing direction to East (3)
            if (_zFacing == 0)
                _zFacing = 3;
            // otherwise go down an index in the cardinalDirections array
            else
                _zFacing -= 1;
        }

		/// <summary>
		/// Method <c>TurnLeft</c> turns the rover cardinally to the left one time (i.e. N -> W | E -> N)
		/// 
		/// NOTE: direction facing is calculated by numbers instead of characters (i.e. 0123 not NWSE)
		///		0 = North | 1 = West | 2 = South | 3 = East
		///	this is based on the "cardinalDirections" array declared in the "MissionControl" class
		/// </summary>
		public void TurnLeft()
        {
            // if the rover is facing East (3) change the facing direction to North (0)
            if (_zFacing == 3)
                _zFacing = 0;
            // otherwise go up an index in the cardinalDirections array
            else
                _zFacing += 1;
        }

        /// <summary>
        /// Method <c>Move</c> moves the rover in an x or y direction depending on the facing direction of the rover
        /// i.e. if facing north move the rover +1 on the y axis | if facing west move the rover -1 on the x axis
        /// </summary>
        /// <returns>boolean - false if the rover runs into a plateau bound (below 0, 0 or above xBound, yBound), true otherwise</returns>
        public bool Move()
        {
            switch (_zFacing)
            {
                // facing North
                case 0:
                    // greater than the y bound set by the user
                    if (_yPosition + 1 > _yBound)
                    {
                        Console.WriteLine("Rover reached y-axis plateau upper bound. Stopping Rover.");
                        return false;
                    }
                    _yPosition += 1;
                    break;
                // facing West
                case 1:
                    // below 0
                    if (_xPosition - 1 < 0)
                    {
                        Console.WriteLine("Rover reached x-axis plateau lower bound. Stopping Rover.");
                        return false;
                    }
                    _xPosition -= 1;
                    break;
                // facing South
                case 2:
                    // below 0
                    if (_yPosition - 1 < 0)
                    {
                        Console.WriteLine("Rover reached y-axis plateau lower bound. Stopping Rover.");
                        return false;
                    }
                    _yPosition -= 1;
                    break;
                // facing East
                case 3:
                    // greater than the x bound set by the user
                    if (_xPosition + 1 > _xBound)
                    {
                        Console.WriteLine("Rover reached x-axis plateau upper bound. Stopping Rover.");
                        return false;
                    }
                    _xPosition += 1;
                    break;
            }

            return true;
        }

        public int[] SendUpdatedPosition()
        {
            return new int[] { XPosition, YPosition, ZFacing };
        }
    }
}
