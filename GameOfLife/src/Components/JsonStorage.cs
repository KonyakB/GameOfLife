using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Represents a component that provides functionality to save and load objects to/from JSON format.
/// </summary>
public class JsonStorage(IJsonSerializer jsonSerializer, IFileStorage fileStorage) : IJsonStorage

{
    /// <summary>
    /// Represents the path of the JSON file.
    /// </summary>
    private const string FilePath = "grid.json";

    /// <summary>
    /// Saves an object to JSON format and writes it to a file.
    /// </summary>
    /// <param name="grid">The object to be saved.</param>
    /// <param name="filePath">The file path to save the JSON data. If not specified, the default file path "grid.json" will be used.</param>
    /// <returns>The JSON string representing the serialized object.</returns>
    public string? SaveToJson(Grid grid, string filePath = FilePath)
    {
        var flatGrid = FlattenGrid(grid.DataGrid);
    
        // Create a data structure that includes the flattened grid and its dimensions
        if (grid.DataGrid != null)
        {
            var dataForJson = new 
            {
                Width = grid.DataGrid.GetLength(0),
                Height = grid.DataGrid.GetLength(1),
                FlatGrid = flatGrid
            };

            var jsonString = jsonSerializer.Serialize(dataForJson);
            fileStorage.Write(filePath, jsonString);
            return jsonString;
        }

        return null;
    }

    /// <summary>
    /// Loads an object of type T from a JSON file.
    /// </summary>
    /// <param name="filePath">The path of the JSON file to load from.</param>
    /// <returns>An object of type T if the loading is successful; otherwise, null.</returns>
    public Grid? LoadFromJson(string filePath)
    {
        var json = fileStorage.Read(filePath);
    
        // validate json string before deserialize
        // if (string.IsNullOrWhiteSpace(json)) 
        // {
        //     Console.WriteLine($"No content or invalid content in file: {filePath}");
        //     return null;
        // }
        //
        // var dataFromJson = jsonSerializer.Deserialize<dynamic>(json);
        //
        // if (dataFromJson == null ||
        //     dataFromJson?.Width == null ||
        //     dataFromJson?.Height == null ||
        //     dataFromJson?.FlatGrid == null)
        // {
        //     Console.WriteLine("Invalid JSON structure.");
        //     return null;
        // }
        //
        // bool[,] cells = UnflattenGrid(dataFromJson.FlatGrid.ToObject<Cell[]>(), (int)dataFromJson.Width, (int)dataFromJson.Height);
        // Grid grid = new Grid((int)dataFromJson.Width, (int)dataFromJson.Height, cells);
        // return grid;
        return null;
    }
    
    
    // Flattens a 2D array to a 1D array
    private Cell[] FlattenGrid(Cell[,]? grid)
    {
        if (grid == null)
            return Array.Empty<Cell>();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        Cell[] flatGrid = new Cell[width * height];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                flatGrid[(i * width) + j] = grid[i, j];
            }
        }

        return flatGrid;
    }
    
    private Cell[,] UnflattenGrid(Cell[] flatGrid, int width, int height)
    {
        Cell[,] grid = new Cell[width, height];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                grid[i, j] = flatGrid[(i * width) + j];
            }
        }

        return grid;
    }
}