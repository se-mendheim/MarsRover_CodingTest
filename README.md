# MarsRover_CodingTest

This project implements PROBLEM ONE on the DealerOn Development_Candidate_Coding_Test_v5. The problem in question has NASA landing rovers on a Mars plateau and sending the rovers instructions. The problem can be seen below:

> NASA intends to land robotic rovers on Mars to explore a particularly curious-looking plateau. The rovers must
> navigate this rectangular plateau in a way so that their on board cameras can get a complete image of the
> surrounding terrain to send back to Earth.
> A simple two-dimensional coordinate grid is mapped to the plateau to aid in rover navigation. Each point on the grid is
> represented by a pair of numbers X Y which correspond to the number of points East or North, respectively, from the
> origin. The origin of the grid is represented by 0 0 which corresponds to the southwest corner of the plateau. 0 1 is
> the point directly north of 0 0, 1 1 is the point immediately east of 0 1, etc. A rover’s current position and heading are
> represented by a triple X Y Z consisting of its current grid position X Y plus a letter Z corresponding to one of the four
> cardinal compass points, N E S W. For example, 0 0 N indicates that the rover is in the very southwest corner of the
> plateau, facing north.
> NASA remotely controls rovers via instructions consisting of strings of letters. Possible instruction letters are L, R,
> and M. L and R instruct the rover to turn 90 degrees left or right, respectively (without moving from its current spot),
> while M instructs the rover to move forward one grid point along its current heading.
> Your task is write an application that takes the test input (instructions from NASA) and provides the expected output
> (the feedback from the rovers to NASA). Each rover will move in series, i.e. the next rover will not start moving until
> the one preceding it finishes.


## Project Structure
- NASA 
  - 
  - This is the startup class that initializes mission control and has mission control start the Mars rover mission.
- Mission Control
  - 
  - This class is the hub for getting user input (and verifying it's format) and sending commands to the individual rovers.
- Rover
  -
  - This class is the rover sent onto the Mars plateau. The rover keeps track of it's (x, y) position and facing direction. It will turn right/move and move forward when told by Mission Control to do so.
- Test Suite
  - 



## Brief Explination
The structure I used is rather simple, at first I placed everything into NASA and utilized "static void Main(string[] args)" to get a working solution. After that I restructured the project to be an object oriented approach (for readability, testing, debugging, etc.). I created two new objects "MissionControl" and "Rover". In my head I was picturing a satalite above Mars (MissionControl) and the Rovers down on the plateau performing actions. I wanted MissionControl to have very little access to the rover except by the three differnt commands (Turn Left, Turn Right, Move Forward). MissionControl also had the responsibility of sending correctly input commands to the mars rover, so I utilized regex statements to verify the commands being sent to the rover are in the correct format. If the command was not in the correct format the system would tell MissionControl to retry and enter a properly formatted command. Then MissionControl could send a series of rover movement instructions to the rover which the Rover class would handle instead of MissionControl.

Here Rover recieves the (x, y) bounds of the plateau, it's current position and facing direction, and the actions it is supposed to enact. The rover goes through these commands one by one (MissionControl only sends one command to the rover at a time). If the rover runs into an (x, y) bound it stops immediatly and sends a message back to mission control that it has stopped. Otherwise the rover will report back to mission control that it is finished and MissionControl logs the position and facing direction of the rover.


## Assumptions
I assumed a few things during the building of this project.
1. **Users will input incorrectly formatted commands (most likely on accident) that I need to account for**
   - To account for this I implemented a system on each user input, verify the command is in the correct format and does not exceed any bounds previously set. If it does, have the user reenter the command.
2. **Cardinal compass points should be represented 0 1 2 3 (0 = North, 1 = West, 2 = South, 3 = East)**
   - Initially I had the rover compute it's cardinal direction through a series of if statements/a switch statement. Quickly I realized how redundant the code was and how easily it could be simplified. I assigned each cardinal direction to a number and performed a simple +/- calculation to determine the rover's facing direction. On the edge cases of 0 and 3 I added a single if statement.
