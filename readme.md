# Group Project: Conway's Game of Life Simulator

- Preamble: In 1970, mathematician John Horton Conway introduced the Game of Life, a deceptively simple cellular automaton governed by just a few rules of life, death, and birth for cells on a grid. Initially a playful mathematical exploration, the Game of Life rapidly captured imaginations with its mesmerizing patterns and unexpected depth.

- At its core, Conway's Game of Life is a deceptively simple experiment in emergence and computability. From a handful of rules governing the life and death of cells on a grid, unpredictable complexity blossoms. Patterns evolve, exhibiting behaviors reminiscent of living organisms: self-replication, organized movement, and even rudimentary computation.

### Historical Impact: Beyond its theoretical significance, the Game of Life found practical applications over the years:

- Modeling: Its ability to simulate dynamic systems made it a tool for modeling phenomena such as population growth, urban patterns, and even the behavior of fundamental particles. For instance, cellular automata similar to the Game of Life have been used to simulate the spread of wildfires, aiding in the development of prediction models.
- Artificial Life: The Game of Life spurred the field of artificial life, where researchers investigate the creation and simulation of life-like behaviors in computational systems. It served as an early inspiration for self-replicating patterns and evolving systems within computer simulations.
- Computational Implications: Remarkably, Conway's Game of Life has been proven to be Turing-complete. This means it has the same computational power as any modern computer, capable in theory of performing any calculation. For instance, researchers have constructed intricate patterns within the game that can function as logic gates and complex computational structures, demonstrating its capacity to emulate any computer program. This highlights the surprising power that lies within even the simplest sets of well-defined rules.
- Objective: Develop a console application that simulates Conway's Game of Life, emphasizing SOLID principles for well-structured, maintainable, and testable code. Users should be able to define an initial grid configuration and observe the evolution of patterns over time. Data persistence will be achieved through JSON files, allowing users to maintain their grid configurations across sessions. The project requires collaborative coding using Git and ensures code quality through unit testing with xUnit.

## Required Components:

- ICell Interface: Defines the properties and behaviors of a single cell in the grid (state, neighbors).
- IGrid Interface: Specifies methods for representing the grid of cells (updating states, getting cell states).
- IStorage Interface Specifies methods for loading and saving the grid.
- Cell Class: Implements the ICell interface, representing an individual cell with its state (alive/dead).
- Grid Class: Implements the IGrid interface, managing a 2D array of cells and the logic for updating cell states in each generation according to Conway's Game of Life rules.
- JsonStorage Class: Implements the IStorage interface. Handles the saving and loading of grid data to and from JSON files.
- AutomatonSimulator Class: Manages the overall simulation, applying the Game of Life rules to the Grid over iterations.
- Program Class (User Interface): Provides a console interface for grid setup, stepping through generations, visualization of the simulation, and saving/loading grid configurations.

## Technical Requirements:

###  Game of Life Rules: Accurately implement the rules of Conway's Game of Life:
- Any live cell with fewer than two live neighbors dies, as if by underpopulation.
- Any live cell with two or three live neighbors lives on to the next generation.
- Any live cell with more than three live neighbors dies, as if by overpopulation.
- Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
- A neighbor is considered any of the eight cells around the current cell, unless it's an edge case (e.g., for the top row, we will consider that there are no neighbors above it).
### Console Visualization:
- Represent live cells with the character 'O' and dead cells with a period ('.').
- Display the grid in a rectangular format.
### SOLID Design: Adhere to SOLID principles. Here's how they might manifest:
- Single Responsibility: Each class (Cell, Grid, AutomatonSimulator, JsonStorage) has a clear purpose.
- Open/Closed: By implementing the IStorage interface, the simulator could be extended to use different persistence formats (e.g., XML) without changing core simulation logic.
- Liskov Substitution: Any subclass of IGrid could be used by the simulator.
- Interface Segregation: Classes interact through well-defined interfaces (ICell, IGrid, IStorage).
- Dependency Inversion: Higher-level classes depend on abstractions (interfaces), not concrete persistence implementations.
### JSON Data Persistence:
- Implement with System.Text.Json to effectively manage grid data.
Exception Handling and Input Validation: 
- Implement robust error handling and user input validation.
### Git for Collaborative Coding:
- Utilize Git for managing collaborative development.
### xUnit Testing:
- Utilize xUnit for thorough unit testing of grid updates, cell behavior, rule applications, and JSON persistence.
## Workflow

- Welcome: The application presents a welcome message.

- Setup Method: The application prompts the user to choose their grid setup:
    "1. Create a new random grid"
    "2. Load grid from JSON file"
 
- New Grid Setup (If Option 1 is chosen):

    - Grid Dimensions: The application prompts for dimensions:
        - "Enter the number of rows for the grid (4-100): "
        - "Enter the number of columns for the grid (4-100): "
    - Grid Initialization: Initializes the grid with a random pattern of living and dead cells.
     
-  Load from File (If Option 2 is chosen):

    - File Selection: The application asks the user to provide the path to a JSON file.
    - File Validation: The application validates the file format and ensures the dimensions match any size limits set.
- Simulation Start:

    -  The application displays the initial grid state (whether created new or loaded from file).
     
- Simulation Loop:

    - Prompt: The application displays the current generation number and prompts the user to:
        - Press 'N' to advance to the next generation.
        - Press 'S' to save the current grid state to a file.
        - Press 'X' to exit the simulation.
         
- Input: The application takes user input.
- Action:
    - If 'N', the AutomatonSimulator calculates the next generation and displays the updated grid.
    - If 'S', the JsonStorage saves the current state to a JSON file.
    - If 'X', the simulation terminates.

## Example Grid from Generation 0 to 1

### Initial Grid (Generation 0)

```
..O..
...O.
.OOO.
.....
```
- 'O' represents a live cell.
- '.' represents a dead cell.
Next Generation (Generation 1)

Let's apply the rules of Conway's Game of Life:

```
.....
.O.O.
..OO.
..O..
```
### JSON File for the Example Grid (Generation 0):

```{
"rows": 4,  
"columns": 5,  
"grid": [
            [false, false, true, false, false],
            [false, false, false, true, false],
            [false, true, true, true, false],
            [false, false, false, false, false],
        ]
}```
### Submission

- GitHub Link: Provide the project's GitHub link. Include MaxDKaos and vzastrow if the repository is private. Ensure the repository contains all necessary source code, unit tests, and documentation.
Design Explanation: Include a brief README that outlines how your design decisions reflect SOLID principles.
