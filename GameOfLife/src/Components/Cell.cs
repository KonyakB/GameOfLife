using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Class <c>Cell</c> represents one Single Cell in Game of Life
/// <seealso cref="ICell"/>
/// <seealso cref="Boolean"/>
/// </summary>
/// <param name="isAlive">when generating grid pass <see cref="Boolean"/> value deciding whether the
/// cell <see cref="IsAlive"/> or not</param>
/// <example>
/// 
/// <code>ICell MyCell= new Cell(true);
/// MyCell.AddMultipleNeighbors([...]);
/// Console.WriteLine(MyCell.NumOfAliveNeighbors);
/// </code>
/// </example>
public class Cell(bool isAlive): ICell
{
    public Guid Id { get; set; } = new Guid();
    public bool IsAlive { get; set; } = isAlive;
    public List<Cell> Neighbors { get; set; } = new List<Cell>();
    public int NumOfAliveNeighbors => Neighbors.Count((cell) => cell.IsAlive);
    
    public bool AddNeighbor(Cell cell)
    {
        if (Neighbors.Contains(cell)) return false;
        
        Neighbors.Add(cell);
        return true;
    }

    public bool AddMultipleNeighbors(Cell[] cells)
    {
        var wasSuccessful = true;
        foreach (var cell in cells)
        {
            var status = AddNeighbor(cell);

            //it could be if(!status) but I think that way is more readable
            if (status == false) wasSuccessful = false;
        }

        return wasSuccessful;
        
        //side note: you can use LINQ expression which makes it all a one-liner !!!
        // return cells.Select(AddNeighbor).All(status => status != false);
    }
}
