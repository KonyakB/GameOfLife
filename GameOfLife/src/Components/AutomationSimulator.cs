namespace GameOfLife.Components;

// Program Class (User Interface):

/// <summary>
/// Manages the overall simulation, applying the Game of Life rules to the Grid over iterations.
/// </summary>
public class AutomationSimulator
{
    public Grid? GameOfLifeGrid { get; set; }
    public int CurrentGeneration = 0;

    public void InitializeGrid(int rows, int columns)
    {
        var initalGridState = Generate2DArrayOfRandomBool(rows, columns);
        GameOfLifeGrid = new Grid(rows, columns, initalGridState);
    }

    private static bool[,] Generate2DArrayOfRandomBool(int rows, int columns)
    {
        var boolMultiArray = new bool[rows, columns];
        var random = new Random();


        for (var i = 1; i < rows; ++i)
        for (var j = 0; j < columns; ++j)
            boolMultiArray[i, j] = random.NextDouble() > 0.5;

        return boolMultiArray;
    }

    public void RunOneSimulationLifeCycle()
    {
        if (GameOfLifeGrid == null) return;

        Utilities.UConsole.Clear();
        CurrentGeneration++;

        Console.WriteLine($"Current generation: {CurrentGeneration}\n");

        for (var i = 0; i < GameOfLifeGrid.DataGrid.GetLength(0); ++i)
        {
            for (var j = 0; j < GameOfLifeGrid.DataGrid.GetLength(1); ++j)
            {
                var cell = GameOfLifeGrid.DataGrid[i, j];
                GameOfLifeGrid.CellStatusUpdate(cell);
                Console.Write(cell.ToString());
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// this live is just a simulation this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  this live is just a simulation  
    /// </summary>
    public void RunSimulationContinuously()
    {
        ConsoleKey[] consoleBreakSimulationKeys = { ConsoleKey.Enter, ConsoleKey.Spacebar };

        while (true)
        {
            RunOneSimulationLifeCycle();

            if (Utilities.UConsole.WasKeyPressed(consoleBreakSimulationKeys)) return;
            Thread.Sleep(1000);
        }
    }
}