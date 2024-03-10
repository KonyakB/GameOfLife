using GameOfLife.Interfaces;

namespace GameOfLife.Components;

public class Grid : IGrid
{
    private int rows { get; set; }
    private int columns { get; set; }
    private Cell[,] grid { get; set; }
    
    public Grid(int rows, int columns, bool[][] boolarray)
    {
        this.rows = rows;
        this.columns = columns;
        Cell[,] gridDetached = new Cell[rows, columns]; // grid w/o conections to neighbors
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                switch (boolarray[i][j])
                {
                    case false:
                        gridDetached[i, j].IsAlive = false;
                        break;
                    case true:
                        gridDetached[i, j].IsAlive = true;
                        break;
                } 
            }
        }
        
        Neighbors(gridDetached); //adding their neighbors to each cell
        grid = gridDetached;
    }

    public void Neighbors(Cell[,] grid) //connects every cell to their neighbors
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int iTop = (i > 0) ? i - 1 : rows - 1;
                int iBottom = (i < rows - 1) ? i + 1 : 0;
                
                int jLeft = (j > 0) ? j - 1 : columns - 1;
                int jRight = (j < columns - 1) ? j + 1 : 0;

                //adding the 8 neighbors
                
                Cell[] neighbors =
                {
                    grid[jLeft,iTop],
                    grid[j,iTop],
                    grid[jRight,iTop],
                    grid[jLeft,i],
                    grid[jRight,i],
                    grid[jLeft,iBottom],
                    grid[j,iBottom],
                    grid[jRight,iBottom]
                };

                grid[i, j].AddMultipleNeighbors(neighbors);
            }
        }
    }

    public void CellStatusUpdate(Cell cell)
    {
        switch (cell.NumOfAliveNeighbors)
        { 
            case var expression when (cell.NumOfAliveNeighbors < 2) || (cell.NumOfAliveNeighbors > 3): 
                cell.IsAlive = false; 
                break;
            
            case 3: 
                cell.IsAlive = true; 
                break;
        }
    }
}