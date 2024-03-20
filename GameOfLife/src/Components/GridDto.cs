namespace GameOfLife.Components;

public class GridDto 
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Cell> FlatGrid { get; set; }
}