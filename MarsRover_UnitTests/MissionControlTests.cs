using DealerOn_Coding_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover_UnitTests
{
	[TestClass]
	public class MissionControlTests
	{
		[TestMethod]
		public void PlateauBoundsInputSuccess()
		{
			var mc = new MissionControl();
			var result = mc.CheckPlateauBoundsInput("5 5");

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void PlateauBoundsInputFailure()
		{
			var mc = new MissionControl();
			var result = mc.CheckPlateauBoundsInput("5");

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void RoverPositionInputSuccess()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverPositionInput("1 1 N");

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void RoverPositionInputFailure()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverPositionInput("1 1");

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void RoverPositionInputBoundsSuccess()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverPositionOutOfBounds(5, 5, "1 1 N");

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void RoverPositionInputBoundsFailure()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverPositionOutOfBounds(5, 5, "6 6 N");

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void CheckRoverInstructionsSuccess()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverInstructionsInput("LMRMLM");

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void CheckRoverInstructionsFailure()
		{
			var mc = new MissionControl();
			var result = mc.CheckRoverInstructionsInput("LMRXMLM");

			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void ExecuteRoverInstructionsTurnLeft()
		{
			var mc = new MissionControl();
			var rover = new Rover();
			rover.ZFacing = 2;

			mc.ExecuteRoverInstructions("L", rover);

			Assert.AreEqual(3, rover.ZFacing);
		}

		// I would then test right turning, a multi instruction execution, and a move
	}
}
