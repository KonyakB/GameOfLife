
// -ICell Interface: Defines the properties and behaviors of a single cell in the grid (state, neighbors).
namespace GameOfLife.Interface;

interface ICell
{
    public Guid CellId { get; set; }

    bool _isAlive { get; set; }
    public string State { get; set; }

    public List<Cell> Neighbors { get; set; }
    public int NumOfNeighbors => Neighbors.Count;

    public bool AddNeighbor(Cell cell);
    public bool RemoveNeighbor(Guid CellId);

    public bool AddMultipleNeighbors(Cell[] cells);
    public bool RemoveMultipleNeighbors(Guid[] CellIds);

}
