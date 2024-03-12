using GameOfLife.Interfaces;

namespace GameOfLife.Components;

public class Grid : IGrid
{
    private int rows { get; set; }
    private int columns { get; set; }
    public Cell[,] DataGrid { get; set; }

    public Grid(int rows, int columns, bool[,] boolArray)
    {
        if (boolArray.GetLength(0) != rows || boolArray.GetLength(1) != columns)
            throw new ArgumentException("Provided row and column number does not match the provided matrix.");

        if (rows == 0 || columns == 0) throw new ArgumentException("Row or Column number is 0");

        if (boolArray == null) throw new ArgumentNullException("boolArray");

        this.rows = rows;
        this.columns = columns;
        var gridDetached = new Cell[rows, columns]; // grid w/o conections to neighbors


        for (var i = 0; i < rows; i++)
        for (var j = 0; j < columns; j++)
            switch (boolArray[i, j])
            {
                case false:
                    gridDetached[i, j] = new Cell(false);
                    break;
                case true:
                    gridDetached[i, j] = new Cell(true);
                    break;
            }

        Neighbors(gridDetached); //adding their neighbors to each cell
        DataGrid = gridDetached;
    }

    public void Neighbors(Cell[,] grid) //connects every cell to their neighbors
    {
        rows = grid.GetLength(0);
        columns = grid.GetLength(1);
        for (var i = 0; i < rows; i++)
        for (var j = 0; j < columns; j++)
        {
            var iTop = i > 0 ? i - 1 : rows - 1;
            var iBottom = i < rows - 1 ? i + 1 : 0;

            var jLeft = j > 0 ? j - 1 : columns - 1;
            var jRight = j < columns - 1 ? j + 1 : 0;

            //adding the 8 neighbors
            Cell[] neighbors =
            {
                grid[iTop, jLeft],
                grid[iTop, j],
                grid[iTop, jRight],
                grid[i, jLeft],
                grid[i, jRight],
                grid[iBottom, jLeft],
                grid[iBottom, j],
                grid[iBottom, jRight]
            };

            grid[i, j].AddMultipleNeighbors(neighbors);
        }
    }

    public void CellStatusUpdate(Cell cell)
    {
        switch (cell.NumOfAliveNeighbors)
        {
            case < 2 or > 3:
                cell.IsAlive = false;
                break;

            case 3:
                cell.IsAlive = true;
                break;
        }
    }
}