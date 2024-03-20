using GameOfLife.Components;

namespace GameOfLife.Tests;

public class CellTests
{ 

    /// <summary>
    /// Checks if constructor sets IsAlive property status correctly
    /// </summary> 
    [Fact]
    public void Cell_Constructor_IsAlive_IsTrue()
    {
        //Arange
        Cell mockCell;
        //Assign
        mockCell = new Cell(true);
        //Assert
        Assert.True(mockCell.IsAlive);
    }

    /// <summary>
    /// Checks if constructor sets IsAlive property status correctly
    /// </summary> 
    [Fact]
    public void Cell_Constructor_IsAlive_IsFalse()
    {
        //Arange
        Cell mockCell;
        //Assign
        mockCell = new Cell(false);
        //Assert
        Assert.False(mockCell.IsAlive);
    }
    
    /// <summary>
    /// Checks if changing IsAlive property status is correct
    /// </summary>
    [Fact]
    public void Cell_IsAlive_IsTrue()
    {
        //Arange
        var mockCell = new Cell(false);
        //Assign
        mockCell.IsAlive = true;
        //Assert
        Assert.True(mockCell.IsAlive);
    }
    

    /// <summary>
    /// Checks if changing IsAlive property status is correct
    /// </summary>
    [Fact]
    public void Cell_IsAlive_IsFalse()
    {
        //Arange
        var mockCell = new Cell(true);
        //Assign
        mockCell.IsAlive = false;
        //Assert
        Assert.False(mockCell.IsAlive);
    }
    
    /// <summary>
    /// Checks if correctly adds one neighbor
    /// </summary>
    [Fact]
    public void AddNeighbor_AddsOneNeighbor_ReturnsTrue()
    {
        //Arrange
        var mockCell = new Cell(true);
        var mockCellNeighbor = new Cell(true);
        bool status;

        //Act
        status = mockCell.AddNeighbor(mockCellNeighbor);
        
        //Assert
        Assert.Single(mockCell.Neighbors);
        Assert.True(status);
    }

    /// <summary>
    /// Checks if correctly skips adding the same neighbor second time
    /// </summary>
    [Fact]
    public void AddNeighbor_AddsOneTheSameNeighborTwice_NeighborsCountIsOne_ReturnsFalse()
    {
        //Arrange
        var mockCell = new Cell(true);
        var mockCellNeighbor = new Cell(true);
        var mockCellNeigoborClone = mockCellNeighbor;
        bool status;

        //Act
         mockCell.AddNeighbor(mockCellNeighbor);
         status = mockCell.AddNeighbor(mockCellNeigoborClone);
        
        //Assert
        Assert.Single(mockCell.Neighbors);
        Assert.False(status);
    }
    
    /// <summary>
    /// Data for AddMultipleCells_AddsMultipleCells_ReturnsTrue() Method
    /// </summary>
    public static IEnumerable<object[]> TestAddMultipleNeighbors_AddsMultipleNeighbors_Data()
    {
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), new Cell(true), new Cell(true), new Cell(false),
            }, 4
        };
        
        yield return new object[]
        {
            new Cell[]
            {
            }, 0
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false)
            }, 1
        };
    }
    
    /// <summary>
    /// Tests if adding multiple neighbors is correct
    /// </summary>
    /// <param name="mockCellNeighbors">Array of cells</param>
    /// <param name="expectedNumOfNeighbors">Expected number of neighbors to add</param>
    [Theory]
    [MemberData(nameof(TestAddMultipleNeighbors_AddsMultipleNeighbors_Data))]
    public void AddMultipleNeighbors_AddsMultipleNeighbors_ReturnsTrue(
        Cell[] mockCellNeighbors, int expectedNumOfNeighbors
        )
    {
        var mockCell = new Cell(true);
        bool status;
        
        //Act
        status = mockCell.AddMultipleNeighbors(mockCellNeighbors);
        
        
        //Assert
        Assert.Equal(expectedNumOfNeighbors, mockCell.Neighbors.Count);
        Assert.True(status);
    }
    
    
    /// <summary>
    /// Data for AddMultipleCells_AddsMultipleCells_WithClones_ReturnsTrue() Method
    /// </summary>
    public static IEnumerable<object[]> TestAddMultipleNeighbors_AddsMultipleNeighbors_WithClones_Data()
    {
        Cell cell1 = new Cell(true);
        Cell cell2 = new Cell(false);
        
        
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), cell1, new Cell(true), cell1,
            }, 3
        };
        
        yield return new object[]
        {
            new Cell[]
            {
               cell1, cell1, cell1, cell1 
            }, 1
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), cell1, new Cell(true), cell1 ,cell2, cell2, new Cell(false),
            }, 5
        };
        yield return new object[]
        {
            new Cell[]
            {
                 cell1, cell1 ,cell2, cell2, cell1, cell2
            }, 2
        };
    }
    
    /// <summary>
    /// Tests when adding multiple neighbors with clones it detects them and skips them  
    /// </summary>
    /// <param name="mockCellNeighbors">Array of cells</param>
    /// <param name="expectedNumOfNeighbors">Expected number of neighbors to add after skipping repeated ones</param>
    [Theory]
    [MemberData(nameof(TestAddMultipleNeighbors_AddsMultipleNeighbors_WithClones_Data))]
    public void AddMultipleNeighbors_AddsMultipleNeighbors_WithClones_ReturnsTrue(
        Cell[] mockCellNeighbors, int expectedNumOfNeighbors
        )
    {
        var mockCell = new Cell(true);
        bool status;
        
        //Act
        status = mockCell.AddMultipleNeighbors(mockCellNeighbors);
        
        
        //Assert
        Assert.Equal(expectedNumOfNeighbors, mockCell.Neighbors.Count);
        Assert.False(status);
    }
    

    /// <summary>
    /// Data to test for ChecksNuberOfAliveNeighbors_AddsOneNeighbor
    /// </summary>
    public static IEnumerable<object[]> TestCheckNumberOfAliveNeighbors_AddNeighbor_Data()
    {
        yield return new object[] { new Cell(false), 0};
        yield return new object[] { new Cell(true),  1};
    }
    
    /// <summary>
    /// Check if after adding one neighbor, counting the number of alive cells is correct
    /// </summary>
    /// <param name="mockCellNeighbor">Cell</param>
    /// <param name="expectedNumberOfALiveNeighbors">Expected number of cells that is alive (should be 0 or 1) depending if uou passed alive cell</param>
    [Theory]
    [MemberData(nameof(TestCheckNumberOfAliveNeighbors_AddNeighbor_Data))]
    public void ChecksNumberOfAliveNeighbors_AddsOneNeighbor(
        Cell mockCellNeighbor, int expectedNumberOfALiveNeighbors
        )
    {
        //Arrange
        var mockCell = new Cell(true);

        //Act
       mockCell.AddNeighbor(mockCellNeighbor);
        
       //Assert
       Assert.Equal(expectedNumberOfALiveNeighbors, mockCell.NumOfAliveNeighbors);
    }
    
    /// <summary>
    ///Passes different cells, expects number of alive cells
    /// </summary>
    public static IEnumerable<object[]> TestCheckNumOfAliveNeighbors_AddsMultipleNeighbors_Data()
    {
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), new Cell(true), new Cell(true), new Cell(false),
            }, 2
        };
        
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), new Cell(false), new Cell(false), new Cell(false),
            }, 0
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), new Cell(false), new Cell(true), new Cell(false),
            }, 1
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(true)
            }, 1
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(true), new Cell(true), new Cell(true), new Cell(true),new Cell(true),
                new Cell(true),new Cell(true),new Cell(true),
            }, 8
        };
        yield return new object[]
        {
            new Cell[]
            {
                new Cell(false), new Cell(false), new Cell(false), new Cell(false),new Cell(false),
                new Cell(false),new Cell(false),new Cell(false),
            }, 0
        };
    }
    
    /// <summary>
    /// Checks if variable NumberOfAliveCells is Equal to the number of alive cells in the Neighbors List
    /// </summary>
    /// <param name="mockCellNeighbors">Array of Cells</param>
    /// <param name="expectedNumberOfALiveNeighbors">Expected number of alive cells</param>
    [Theory]
    [MemberData(nameof(TestCheckNumOfAliveNeighbors_AddsMultipleNeighbors_Data))]
    public void CheckNumOfAliveNeighbors_AddsMultipleNeighbors(
        Cell[] mockCellNeighbors, int expectedNumberOfALiveNeighbors 
        )
    {
        //Arrange
        var mockCell = new Cell(true);
        
        //Act
       mockCell.AddMultipleNeighbors(mockCellNeighbors);

       //Assert
       Assert.Equal(expectedNumberOfALiveNeighbors, mockCell.NumOfAliveNeighbors);
       
    }
        
}