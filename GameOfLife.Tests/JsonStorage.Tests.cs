// Purpose: Provide tests for the JsonStorage class.
// TODO: Can't currently run the tests, the tests don't appear in the test explorer.
using Xunit;
using Moq; // Install-Package Moq -Version 4.16.1
using GameOfLife.Components;
using GameOfLife.Interfaces;

namespace GameOfLife.Tests
{
    /// <summary>
    /// Contains unit tests for the JsonStorage class.
    /// </summary>
    public class JsonStorageTests
    {
        /// <summary>
        /// Serializes the given grid into JSON format and writes it to a file.
        /// </summary>
        /// <param name="grid">The grid to serialize and write.</param>
        /// <param name="filePath">The path of the file where the JSON should be written.</param>
        /// <returns>The JSON string that was written to the file.</returns>
        [Fact]
        public void SaveToJson_ShouldSerializeGridAndWriteToFile()
        {
            // Arrange
            var expectedJson = "{}";
            var grid = new Cell[,] { };
            var jsonSerializerMock = new Mock<IJsonSerializer>();
            jsonSerializerMock.Setup(js => js.Serialize(grid)).Returns(expectedJson);
            var fileStorageMock = new Mock<IFileStorage>();
            var jsonStorage = new JsonStorage(jsonSerializerMock.Object, fileStorageMock.Object);

            // Act
            var actualJson = jsonStorage.SaveToJson(grid, "TestPath");

            // Assert
            fileStorageMock.Verify(fs => fs.Write("TestPath", expectedJson), Times.Once);
            Assert.Equal(expectedJson, actualJson);
            jsonSerializerMock.Verify(js => js.Serialize(grid), Times.Once);
        }


        /// <summary>
        /// Method to load a grid from a JSON file.
        /// It reads the content of the file at the specified path, deserialize the JSON into a grid, and returns the grid.
        /// </summary>
        /// <param name="path">The path of the JSON file to read from</param>
        /// <returns>The grid deserialized from the JSON file</returns>
        [Fact]
        public void LoadFromJson_ShouldReadFromFileAndDeserializeJson()
        {
            // Arrange
            var expectedJson = "{}";
            var expectedGrid = new Cell[,] { };
            var jsonSerializerMock = new Mock<IJsonSerializer>();
            jsonSerializerMock.Setup(js => js.Deserialize<Cell>(expectedJson)).Equals(expectedGrid);
            
            var fileStorageMock = new Mock<IFileStorage>();
            fileStorageMock.Setup(fs => fs.Read("TestPath")).Returns(expectedJson);
            var jsonStorage = new JsonStorage(jsonSerializerMock.Object, fileStorageMock.Object);

            // Act
            var actualGrid = jsonStorage.LoadFromJson<Cell[,]>("TestPath");

            // Assert
            fileStorageMock.Verify(fs => fs.Read("TestPath"), Times.Once);
            Assert.Equal(expectedGrid, actualGrid);
            jsonSerializerMock.Verify(js => js.Deserialize<string>(expectedJson), Times.Once);
        }
    }
}