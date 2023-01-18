using System.Collections;
using System.Text.RegularExpressions;

namespace DealerOn_Coding_Test
{
	public class MissionControl
	{
		// array used to initially determine rover facing direction
		// array also used to print the rovers end direction to the user
		private char[] cardinalDirections = new char[] { 'N', 'W', 'S', 'E' };

		/// <summary>
		/// Method <c>MarsRoverMission</c> runs the mars rover mission.
		/// This method is a continuous loop that allows mission control to continue sending rovers to mars.
		/// This method assigns variables and calls other methods to run the rover
		/// </summary>
		public void MarsRoverMission()
		{
			// variable used to determine if user is inputting bounds for plateau
			bool firstInput = true;

			// x and y bounds for the plateau
			int xBound = -1;
			int yBound = -1;

			Dictionary<Rover, string> rovers = new Dictionary<Rover, string>();

			Console.WriteLine("Enter \"EXECUTE\" when finished");
			while (true)
			{
				// initialize a new rover
				var rover = new Rover();

				if (firstInput)
				{
					var plateauBoundsInput = PlateauBoundsInput();
					// checking if the user terminated the program
					if (plateauBoundsInput == null)
					{
						break;
					}

					var plateauBounds = plateauBoundsInput.Split();
					// set x and y bounds for the plateau
					xBound = int.Parse(plateauBounds[0]);
					yBound = int.Parse(plateauBounds[1]);
					firstInput = false;
					
				}

				// relay the x and y bounds of the plateau to the rover
				rover.XBound = xBound;
				rover.YBound = yBound;

				var roverPosition = RoverPositionInput(xBound, yBound);
				// checking if the user terminated the program
				if (roverPosition == null)
				{
					break;
				}

				
				// set x and y position and x facing direction
				rover.XPosition = Int32.Parse(roverPosition[0]);
				rover.YPosition = Int32.Parse(roverPosition[1]);
				// upper case the inputted direction to allow IndexOf to properly handle the character
				rover.ZFacing = Array.IndexOf(cardinalDirections, char.ToUpper(roverPosition[2][0]));


				var roverInstructions = RoverInstructionsInput();
				// checking if the user terminated the program
				if (roverInstructions == null)
				{
					break;
				}
				
				// add the rover and it's instructions to an arraylist
				rovers.Add(rover, roverInstructions);
			}

			// make sure at least one rover went on a mission
			if (rovers.Count >= 1)
			{
				foreach (var rover in rovers)
				{
					// execute the given instructions for the specified rover
					ExecuteRoverInstructions(rover.Value, rover.Key);

					var roverPosition = rover.Key.SendUpdatedPosition();

					// write to the user the position and z facing direction of the rover
					Console.WriteLine($"{roverPosition[0]} {roverPosition[1]} {cardinalDirections[roverPosition[2]]}");
				}
			}
			else
			{
				Console.WriteLine("No rovers were sent to Mars");
			}
			
		}

		/// <summary>
		/// Method <c>PleateauBoundsInput</c> gets the pleateau bounds from the user
		/// </summary>
		/// <returns>plateu bounds</returns>
		public string PlateauBoundsInput()
		{
			string plateauBoundsInput = "";

			// get the plateau bounds from the user
			while (true)
			{
				plateauBoundsInput = Console.ReadLine();
				// check if the user is finished with input
				if (plateauBoundsInput.ToUpper().Equals("EXECUTE"))
				{
					return null;
				}
				if (CheckPlateauBoundsInput(plateauBoundsInput))
				{
					break;
				}
			}

			return plateauBoundsInput;
		}

		/// <summary>
		/// Method <c>RoverPositionInput</c> gets the rover position and facing direction from the user
		/// </summary>
		/// <param name="xBound"> x bound of the plateau</param>
		/// <param name="yBound"> y bound of the plateau</param>
		/// <returns>Rover position and facing direction</returns>
		public string[] RoverPositionInput(int xBound, int yBound)
		{
			// placeholder variable which will be updated once the input is verified
			var roverPosition = new string[3];

			// get the rover position from the user
			while (true)
			{
				var roverPositionInput = Console.ReadLine();
				// check if the user is finished with input
				if (roverPositionInput.ToUpper().Equals("EXECUTE"))
				{
					return null;
				}
				if (CheckRoverPositionInput(roverPositionInput) && CheckRoverPositionOutOfBounds(xBound, yBound, roverPositionInput))
				{
					roverPosition = roverPositionInput.Split();
					break;
				}
			}

			return roverPosition;
		}

		/// <summary>
		/// Method <c>ReoverInstructionsInput</c> gets the rover instructions from the user
		/// </summary>
		/// <returns> Instructions for the rover to perform</returns>
		public string RoverInstructionsInput()
		{
			// placeholder variable which will be updated once the input is verified
			var roverInstructions = "";

				// get the rover action instructions from the user
				while (true)
				{
					var roverInstructionsInput = Console.ReadLine();
					// check if the user is finished with input
					if (roverInstructionsInput.ToUpper().Equals("EXECUTE"))
					{
						return null;
					}
					if (CheckRoverInstructionsInput(roverInstructionsInput))
					{
						// change the string to upper case for switch statement reading
						roverInstructions = roverInstructionsInput.ToUpper();
						break;
					}
				}
			return roverInstructions;
		}

		/// <summary>
		/// Method <c>PlateauBoundsInput</c> checks the input for the upper and lower bounds from the user
		/// This method will return false if the input does not match the correct format thus prompting the 
		/// user to re-enter the upper and lower bounds
		/// </summary>
		/// <returns>boolean - false if incorrectly formatted, true if correctly formatted</returns>
		public bool CheckPlateauBoundsInput(string plateauBounds)
		{
			// regex to make sure the input is in the form of: number number
			Regex correctBoundsRegex = new Regex(@"^(?:[+]{0,1}[\d]+(?:\.[\d]+)*\s*){2}$");

			if (!correctBoundsRegex.IsMatch(plateauBounds))
			{
				Console.WriteLine("Incorrect Formatting for plateau bounds. Please enter: number number");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Method <c>RoverPositionInput</c> checks the input for the rovers position and facing direction
		/// </summary>
		/// <param name="roverPosition"> input from the user definining the rover x, y position and facing direction</param>
		/// <returns>boolean - false if incorrectly formatted, true if correctly formatted</returns>
		public bool CheckRoverPositionInput(string roverPosition)
		{
			// regex to make sure the input is in the form of: number number (one character NESW) and non negative numbers
			Regex correctRoverPositionRegex = new Regex(@"^(?:[+\]{0,1}[\d]+(?:\.[\d]+)*\s*){2}[NWSEnwse]+$");

			if (!correctRoverPositionRegex.IsMatch(roverPosition))
			{
				Console.WriteLine("Incorrect Formatting for rover position. Please enter: number number (one of these characters: NWSE)");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Method <c>CheckRoverPositionOutOfBounds</c> checks if the rover is outside the x, y bounds of the plateau
		/// </summary>
		/// <param name="xBound"> x upper bound of the plateau</param>
		/// <param name="yBound"> y upper bound of the plateau</param>
		/// <param name="roverPosition"> input from the user defining the rover x, y position and facing direction</param>
		/// <returns>boolean - false if the rover is outside the x, y bounds, true otherwise</returns>
		public bool CheckRoverPositionOutOfBounds(int xBound, int yBound, string roverPosition)
		{
			var roverPositions = roverPosition.Trim().Split();
			// check if the x or y positon the user entered is outside the plateau bounds
			if (Int32.Parse(roverPositions[0]) > xBound || Int32.Parse(roverPositions[1]) > yBound)
			{
				Console.WriteLine("Rover x or y position out of plateau bounds");
				return false;
			}
			else if (roverPositions.Length > 3)
			{
				Console.WriteLine("Incorrect Formatting for rover position. Please enter: number number (one of these characters: NWSE)");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Method <c>RoverInstructionsInput</c> checks if the rover instructions are only using the correct letters
		/// </summary>
		/// <returns>boolean - false if user inputs anything other than a string of L, R, M, true otherwise</returns>
		public bool CheckRoverInstructionsInput(string roverInstructions)
		{
			// regex to remove all whitespaces (spaces, tabs, etc.) from user input
			// this allows the user to put in LMRM RMLM RMM
			var noWhiteSpaceRoverInstructions = Regex.Replace(roverInstructions, @"\s", "");
			// regex to make sure user input only contains the letters: LMR (also allows for lower case lmr)
			Regex correctRoverInstructionsRegex = new Regex("^[LMRlmr]+$");

			if (!correctRoverInstructionsRegex.IsMatch(noWhiteSpaceRoverInstructions))
			{
				Console.WriteLine("Incorrect Formatting for rover action instructions. Please enter a string containing only these letters: L, R, or M");
				return false;
			}
			return true;
		}


		/// <summary>
		/// Method <c>ExecuteRoverInstructions</c> has the rover execute each action provided by the user
		/// </summary>
		/// <param name="roverInstructions">string of rover instructions (L = turn left, R = turn right, M = move forward)</param>
		/// <param name="rover">the rover being controlled</param>
		public void ExecuteRoverInstructions(string roverInstructions, Rover rover)
		{
			// variable used to see if the rover has reached plateau bounds
			bool plateauBoundsReached = false;
			foreach (var roverAction in roverInstructions)
			{
				// if the user has reached the plateau bounds, stop the rover
				if (plateauBoundsReached)
				{
					break;
				}
				// switch statement to relay what action the rover should do next
				switch (roverAction)
				{
					// turn left
					case 'L':
						rover.TurnLeft();
						break;
					// turn right
					case 'R':
						rover.TurnRight();
						break;
					// move forward
					case 'M':
						// if statement to check if the rover has reached the bounds of the plateau
						if (rover.Move() == false)
						{
							// if so do not continue any actions and make sure the rover stops
							plateauBoundsReached = true;
							break;
						}
						break;
				}
			}
		}
	}
}
