using GameOfLife.Components;
using GameOfLife.Interfaces;

namespace GameOfLife.Tests
{
    /// <summary>
    /// Contains unit tests for the JsonStorage class.
    /// </summary>
    public class JsonStorageTests
    {
        /// Represents an interface for saving and loading objects to/from JSON format.
        /// /
        private readonly IJsonStorage _jsonStorage;

        /// <summary>
        /// Represents a class that provides methods for testing the JsonStorage class.
        /// </summary>
        public JsonStorageTests()
        {
            var fileStorage = new FileStorage();
            var jsonSerializer = new JsonSerializerAdapter();
            _jsonStorage = new JsonStorage(jsonSerializer, fileStorage);
        }

        /// <summary>
        /// Saves the provided grid to a JSON file at the specified file path.
        /// </summary>
        /// <param name="grid">The grid to be saved.</param>
        /// <param name="filePath">The file path where the JSON file should be saved.</param>
        /// <returns>Returns the path of the saved JSON file.</returns>
        [Theory]
        [InlineData(new bool[] { true, false, true }, 1)]
        [InlineData(new bool[] { false, true, false, true, false }, 1)]
        [InlineData(new bool[] { true, false, true, false, true, false, true }, 1)]
        [InlineData(new bool[] { false, false, false, false, false, false, false }, 7)]
        [InlineData(new bool[] { true, true, true, true, true, true, true }, 7)]
        public void SaveToJsonTest(bool[] cellStates, int width)
        {
            // Arrange
            string _testJsonPath = Path.Combine(System.IO.Path.GetTempPath(), Path.GetRandomFileName());
            int height = cellStates.Length / width;

            bool[,] cellsOrig = new bool[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cellsOrig[i, j] = cellStates[i * width + j];
                }
            }

            Grid grid = new Grid(height, width, cellsOrig);

            // Act

            // Save grid to JSON
            _jsonStorage.SaveToJson(grid, _testJsonPath);

            // Assert if saved JSON is not empty
            Assert.True(new FileInfo(_testJsonPath).Length > 0);
        }

        /// <summary>
        /// Loads a grid from a JSON file.
        /// </summary>
        /// <param name="cellStates">The array of cell states.</param>
        /// <param name="width">The width of the grid.</param>
        [Theory]
        [InlineData(new bool[] { false, false, false, false, false, false, false }, 7)]
        [InlineData(new bool[] { true, true, true, true, true, true, true }, 7)]
        public void LoadFromJsonTest(bool[] cellStates, int width)
        {
            // Arrange
            string _testJsonPath = Path.Combine(System.IO.Path.GetTempPath(), Path.GetRandomFileName());
            int height = cellStates.Length / width;

            bool[,] cellsOrig = new bool[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cellsOrig[i, j] = cellStates[i * width + j];
                }
            }

            Grid grid = new Grid(height, width, cellsOrig);

            // Act

            // Save grid to JSON
            _jsonStorage.SaveToJson(grid, _testJsonPath);

            // Load grid from JSON
            Grid loadedGrid = _jsonStorage.LoadFromJson(_testJsonPath);

            // Assert

            // Check dimensions of grids
            Assert.Equal(grid.Rows, loadedGrid.Rows);
            Assert.Equal(grid.Columns, loadedGrid.Columns);

            // Check that grids are equal
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    Assert.Equal(grid.DataGrid[i, j].IsAlive, loadedGrid.DataGrid[i, j].IsAlive);
                }
            }
        }
    }
}