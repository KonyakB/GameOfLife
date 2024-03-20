using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Represents a class that provides methods for saving and loading objects to/from JSON format.
/// </summary>
public class JsonStorage(IJsonSerializer jsonSerializer, IFileStorage fileStorage) : IJsonStorage

{
    /// grid data.
    private const string FilePath = "grid.json";


    /// <summary>
    /// Saves a grid object to JSON format and writes it to a file.
    /// </summary>
    /// <param name="grid">The grid object to be saved.</param>
    /// <param name="filePath">The file path to save the JSON data. If not specified, the default file path "grid.json" will be used.</param>
    /// <returns>The JSON string representing the serialized grid object.</returns>
    public string? SaveToJson(Grid grid, string filePath = FilePath)
    {
        var flatGrid = FlattenGrid(grid.DataGrid);
    
        // Create a data structure that includes the flattened grid and its dimensions
        if (grid.DataGrid == null) return null;
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


    /// <summary>
    /// Loads a grid from a JSON file.
    /// </summary>
    /// <param name="filePath">The path of the JSON file to load from.</param>
    /// <returns>The loaded grid if successful; otherwise, null.</returns>
    public Grid? LoadFromJson(string filePath)
    {
        var json = fileStorage.Read(filePath);

        if (string.IsNullOrWhiteSpace(json)) return null;
    
        var dataFromJson = jsonSerializer.Deserialize<GridDto>(json);
        var flatGrid = dataFromJson?.FlatGrid;

        if (flatGrid == null) return null;
        var cells = UnflattenGrid(flatGrid, dataFromJson!.Width, dataFromJson.Height);

        return new Grid(dataFromJson.Width, dataFromJson.Height, cells);

    }


    /// <summary>
    /// Flattens a 2D grid into a 1D array of cells.
    /// </summary>
    /// <param name="grid">The 2D grid to flatten.</param>
    /// <returns>The flattened grid as a 1D array of cells.</returns>
    private Cell[] FlattenGrid(Cell[,]? grid)
    {
        if (grid == null)
            return Array.Empty<Cell>();

        int rows = grid.GetLength(0);
        int columns = grid.GetLength(1);
        Cell[] flatGrid = new Cell[rows * columns];

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                flatGrid[(i * columns) + j] = grid[i, j]; // change here as well
            }
        }

        return flatGrid;
    }

    /// <summary>
    /// Unflattens a flat grid into a two-dimensional grid.
    /// </summary>
    /// <param name="flatGrid">The flat grid to unflatten.</param>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <returns>The unflattened two-dimensional grid.</returns>
    private bool[,] UnflattenGrid(List<Cell> flatGrid, int width, int height)
    {
        bool[,] grid = new bool[width, height];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                grid[i, j] = flatGrid[(i * width) + j].IsAlive;
            }
        }

        return grid;
    } 
}