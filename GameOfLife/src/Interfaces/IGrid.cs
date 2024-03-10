using GameOfLife.Components;

namespace GameOfLife.Interfaces;

public interface IGrid
{
    public void Neighbors(Cell[,] grid);

    public void CellStatusUpdate(Cell cell);
}