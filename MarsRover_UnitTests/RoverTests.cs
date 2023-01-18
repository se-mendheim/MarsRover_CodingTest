using DealerOn_Coding_Test;

namespace MarsRover_UnitTests
{
	[TestClass]
	public class RoverTests
	{

		#region Rover Turning

		[TestMethod]
		public void TurnRightTest()
		{
			var rover = new Rover();
			rover.ZFacing = 3;

			rover.TurnRight();

			Assert.AreEqual(2, rover.ZFacing);
		}

		[TestMethod]
		public void TurnRightMinTest()
		{
			var rover = new Rover();
			rover.ZFacing = 0;

			rover.TurnRight();

			Assert.AreEqual(3, rover.ZFacing);
		}

		[TestMethod]
		public void TurnLeftTest()
		{
			var rover = new Rover();
			rover.ZFacing = 1;

			rover.TurnLeft();

			Assert.AreEqual(2, rover.ZFacing);
		}

		[TestMethod]
		public void TurnLeftMaxTest()
		{
			var rover = new Rover();
			rover.ZFacing = 3;

			rover.TurnLeft();

			Assert.AreEqual(0, rover.ZFacing);
		}

		#endregion

		#region Rover Moving

		[TestMethod]
		public void RoverMoveNorth()
		{
			var rover = new Rover();
			rover.XPosition = 0;
			rover.XBound = 100;
			rover.YPosition = 0;
			rover.YBound = 100;
			rover.ZFacing = 0;

			rover.Move();

			Assert.AreEqual(1, rover.YPosition);

		}

		[TestMethod]
		public void RoverMoveWest()
		{
			var rover = new Rover();
			rover.XPosition = 1;
			rover.XBound = 100;
			rover.YPosition = 1;
			rover.YBound = 100;
			rover.ZFacing = 1;

			rover.Move();

			Assert.AreEqual(0, rover.XPosition);
		}

		[TestMethod]
		public void RoverMoveSouth()
		{
			var rover = new Rover();
			rover.XPosition = 1;
			rover.XBound = 100;
			rover.YPosition = 1;
			rover.YBound = 100;
			rover.ZFacing = 2;

			rover.Move();

			Assert.AreEqual(0, rover.YPosition);
		}

		[TestMethod]
		public void RoverMoveEast()
		{
			var rover = new Rover();
			rover.XPosition = 0;
			rover.XBound = 100;
			rover.YPosition = 0;
			rover.YBound = 100;
			rover.ZFacing = 3;

			rover.Move();

			Assert.AreEqual(1, rover.XPosition);
		}

		[TestMethod]
		public void RoverUpperYBound()
		{
			var rover = new Rover();
			rover.XPosition = 0;
			rover.XBound = 1;
			rover.YPosition = 1;
			rover.YBound = 1;
			rover.ZFacing = 0;

			rover.Move();

			// assert that the rovers position did not change
			Assert.AreEqual(1, rover.YPosition);
		}

		[TestMethod]
		public void RoverLowerYBound()
		{
			var rover = new Rover();
			rover.XPosition = 0;
			rover.XBound = 1;
			rover.YPosition = 0;
			rover.YBound = 1;
			rover.ZFacing = 2;

			rover.Move();

			Assert.AreEqual(0, rover.YPosition);
		}

		// At this point I would continue to test x upper and lower limit but for the sake of redundancy I shall not.

		#endregion

	}

}