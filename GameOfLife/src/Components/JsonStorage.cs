using GameOfLife.Interfaces;

namespace GameOfLife.Components;


public class JsonStorage(IJsonSerializer jsonSerializer, IFileStorage fileStorage) : IJsonStorage

{
    
    private const string FilePath = "grid.json";


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