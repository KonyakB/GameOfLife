using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Class <c>Cell</c> represents one Single Cell in Game of Life
/// <seealso cref="ICell"/>
/// <seealso cref="Boolean"/>
/// </summary>
/// <param name="isAlive">when generating grid pass <see cref="Boolean"/> value deciding whether the
/// cell <see cref="IsAlive"/> or not</param>
public class Cell(bool isAlive): ICell
{
    public Guid Id { get; set; } = new Guid();
    public bool IsAlive { get; set; } = isAlive;
    public List<Cell> Neighbors { get; set; } = new List<Cell>();
    public bool AddNeighbor(Cell cell)
    {
        if (Neighbors.Contains(cell)) return false;
        
        Neighbors.Add(cell);
        return true;
    }

    public bool RemoveNeighbor(Guid cellId)
    {
        var cell =  Neighbors.Find((cell) => cell.Id == cellId);
        
        if (cell == null) return false;
        
        Neighbors.Remove(cell);
        return true;
    }

    public bool AddMultipleNeighbors(Cell[] cells)
    {
        foreach (var cell in cells)
        {
            var status = AddNeighbor(cell);

            //it could be if(!status) but I think that way is more readable
            if (status == false) return false;
        }

        return true;
        
        //side note: you can use LINQ expression which makes it all a one-liner !!!
        // return cells.Select(AddNeighbor).All(status => status != false);
    }

    public bool RemoveMultipleNeighbors(Guid[] cellIds)
    {
        foreach (var cellId in cellIds)
        {
            var status = RemoveNeighbor(cellId);

            if (status == false) return false;
        }

        return true;
    }
}
