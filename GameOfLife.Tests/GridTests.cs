using GameOfLife.Components;

namespace GameOfLife.Tests;

public class GridTests
{
    [Fact]
    public void Check_Constructor_If_Rows_And_Columns_Match()
    {
        bool[,] mockBoolArray = new bool[4, 5];
        
        var mockGrid = () => new Grid(3,4,mockBoolArray);
        Assert.Throws<ArgumentException>(mockGrid);
    }
}