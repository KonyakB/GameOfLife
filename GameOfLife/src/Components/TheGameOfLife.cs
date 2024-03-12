using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Provides a console interface for grid setup, stepping through generations,
/// visualization of the simulation, and saving/loading grid configurations
/// </summary>
public class TheGameOfLife
{
    private readonly AutomationSimulator _automationSimulator = new();

    private static readonly IJsonSerializer JsonSerializer = new JsonSerializerAdapter();
    private static readonly IFileStorage FileStorage = new FileStorage();
    private static readonly IJsonStorage JsonStorage = new JsonStorage(JsonSerializer, FileStorage);

    private static readonly List<string> UserOptions = new()
    {
        "1. Run ONE cycle of Game of Life -> go to the next generation",
        "2. Run the game continuously -> new generation will spawn every set interval of time",
        "3. Quit and save"
    };

    private const string Prompt = "Choose one option:";
    private const string PathToJsonFile = "./grid.json";

    public void Run()
    {
        Console.WriteLine("Welcome to the game of life!");

        // var jsonGrid = JsonStorage.LoadFromJson<Grid>(PathToJsonFile);
        //TODO: to fix - I'm too tired to figure out how Mark imagined saving stuff into json 
        Grid? jsonGrid = null;

        bool useJsonGrid = false;
        if (jsonGrid != null)
        {
            Console.WriteLine($"Successfully loaded grid form JSON file\n");

            useJsonGrid = Utilities.UConsole.GetUserBoolOption(
                "Do you want to use loaded grid?", null
            );

            if (useJsonGrid)
                _automationSimulator.GameOfLifeGrid = jsonGrid;
        }
        else if(!useJsonGrid)
        {
            GetUserInputRowsAndColumns();
        }


        while (true)
        {
            var userOption = Utilities.UConsole.GetUserOption(Prompt, UserOptions);

            switch (userOption)
            {
                case 0:
                    _automationSimulator.RunOneSimulationLifeCycle();
                    Utilities.UConsole.GetEnterConfirmation();
                    break;
                case 1:
                    _automationSimulator.RunSimulationContinuously();
                    break;
                case 2:
                    //TODO: to fix - I'm too tired to figure out how Mark imagined saving stuff into json 
                    // JsonStorage.SaveToJson(_automationSimulator.GameOfLifeGrid, PathToJsonFile);
                    return;
                default:
                    break;
            }
        }
    }

    private void GetUserInputRowsAndColumns()
    {
        string introduction = """

                          First you will have to insert your grid layout size!

                          Hint: for better looks make the number of columns as twice as big as number of rows
                          Hint: if the grid is too small it can stall, and all life will begone...

                          """;
        
        Console.WriteLine(Utilities.UConsole.WrapLine(introduction));
        
        var rows = Utilities.UConsole.GetUserInputAsNumericType<int>("Input the number of rows");
        var columns = Utilities.UConsole.GetUserInputAsNumericType<int>("Input the number of columns");

        _automationSimulator.InitializeGrid(rows, columns);
    }
}