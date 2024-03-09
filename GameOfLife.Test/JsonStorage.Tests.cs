using Xunit;
using GameOfLife.Components;
using GameOfLife.Interfaces;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    /// <summary>
    /// Unit tests for the JsonStorage class.
    /// </summary>
    public class JsonStorageTests
    {
        /// <summary>
        /// Stub class implementing the <see cref="IJsonSerializer"/> interface.
        /// </summary>
        private class JsonSerializerStub : IJsonSerializer
        {
            /// <summary>
            /// Represents a JSON serialized object.
            /// </summary>
            public string SerializedObj { get; set; }

            /// <summary>
            /// Deserialize the specified JSON string into an object of type T.
            /// </summary>
            /// <typeparam name="T">The type of object to deserialize.</typeparam>
            /// <param name="json">The JSON string to deserialize.</param>
            /// <returns>An object of type T if deserialization is successful; otherwise, null.</returns>
            public T Deserialize<T>(string json)
            {
                return (T)(object)SerializedObj;
            }

            /// <summary>
            /// Serializes an object to JSON format.
            /// </summary>
            /// <typeparam name="T">The type of the object to be serialized.</typeparam>
            /// <param name="obj">The object to be serialized.</param>
            /// <returns>A JSON string representing the serialized object.</returns>
            public string Serialize<T>(T obj)
            {
                bool[,] grid = obj as bool[,];
                SerializedObj = SerializeGrid(grid);
                return SerializedObj;
            }

            /// <summary>
            /// Serializes a boolean grid into a JSON string.
            /// </summary>
            /// <param name="grid">The boolean grid to be serialized.</param>
            /// <returns>The JSON string representing the serialized grid.</returns>
            private string SerializeGrid(bool[,] grid)
            {
                string serialized = $"{{ \"rows\": {grid.GetLength(0)}, \"columns\": {grid.GetLength(1)}, \"grid\": [ ";
                
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    serialized += "[ ";
                    
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        serialized += $"{(grid[i, j] ? "true" : "false")}, ";
                    }
                    
                    serialized = serialized.TrimEnd(',', ' ');
                    serialized += "], ";
                }
                
                serialized = serialized.TrimEnd(',', ' ');
                serialized += "] }}";
                
                return serialized;
            }
        }

        /// <summary>
        /// Stub implementation of the <see cref="IFileStorage"/> interface.
        /// </summary>
        private class FileStorageStub : IFileStorage
        {
            /// <summary>
            /// Represents a component that provides functionality to save and load objects to/from JSON format.
            /// </summary>
            public string WrittenContent { get; private set; }

            /// <summary>
            /// Reads the content of a file at the given path.
            /// </summary>
            /// <param name="filePath">The path of the file to read.</param>
            /// <returns>The content of the file.</returns>
            public string Read(string filePath)
            {
                return WrittenContent;
            }

            /// <summary>
            /// Represents a component that provides functionality to save and load objects to/from JSON format.
            /// </summary>
            /// <typeparam name="T">The type of the object to save/load.</typeparam>
            public void Write(string filePath, string content)
            {
                WrittenContent = content;
            }
        }

        /// <summary>
        /// Saves an object to JSON format and writes it to a file using the specified file path.
        /// </summary>
        /// <typeparam name="T">The type of object to be serialized.</typeparam>
        /// <param name="grid">The object to be serialized and saved.</param>
        /// <param name="filePath">The path where the JSON file should be saved. If not specified, uses the default file path.</param>
        /// <returns>A string representation of the serialized object.</returns>
        public static IEnumerable<object[]> GridData()
        {
            yield return new object[]
            {
                "TestPath1",
                new bool[,]
                {
                    { false, false, true, false, false },
                    { false, false, false, true, false },
                    { false, true, true, true, false },
                    { false, false, false, false, false },
                }
            };
            yield return new object[]
            {
                "TestPath2",
                new bool[,]
                {
                    { true, true, false, true, false },
                    { false, true, false, true, true },
                    { true, false, true, false, false },
                    { false, true, false, true, false },
                }
            };
            yield return new object[]
            {
                "TestPath3",
                new bool[,]
                {
                    { true, true, true, true, true },
                    { true, true, true, true, true },
                    { false, false, false, false, false },
                    { false, false, false, false, false },
                }
            };
        }

        /// <summary>
        /// Saves a grid object to a JSON file at the specified file path.
        /// </summary>
        /// <typeparam name="T">The type of the grid object.</typeparam>
        /// <param name="grid">The grid object to be serialized to JSON.</param>
        /// <param name="filePath">The path of the JSON file to save.</param>
        /// <returns>The serialized JSON string.</returns>
        [Theory]
        [MemberData(nameof(GridData))]
        public void SaveToJson_MultipleGrids_ShouldSerializeGridAndWriteToFile(string filePath, bool[,] grid)
        {
            // Arrange
            var jsonSerializer = new JsonSerializerStub();
            var fileStorage = new FileStorageStub();
            var jsonStorage = new JsonStorage(jsonSerializer, fileStorage);

            // Act
            jsonStorage.SaveToJson(grid, filePath);

            // Assert
            var expectedJson = jsonSerializer.Serialize(grid);
            Assert.Equal(expectedJson, jsonSerializer.SerializedObj);
            Assert.Equal(expectedJson, fileStorage.WrittenContent);
        }


        /// <summary>
        /// Loads an object of type T from a JSON file.
        /// </summary>
        /// <typeparam name="T">The type of object to load.</typeparam>
        /// <param name="filePath">The path of the JSON file to load from.</param>
        /// <returns>An object of type T if the loading is successful; otherwise, null.</returns>
        [Fact]
        public void LoadFromJson_ShouldReadFromFileAndDeserializeJson()
        {
            // Arrange
            var expectedGridString = @"{ ""rows"": 4, ""columns"": 5, ""grid"": [
                                           [false, false, true, false, false],
                                           [false, false, false, true, false],
                                           [false, true, true, true, false],
                                           [false, false, false, false, false]
                                        ] }";
            var jsonSerializer = new JsonSerializerStub() { SerializedObj = expectedGridString };
            var fileStorage = new FileStorageStub();
            fileStorage.Write("TestPath", expectedGridString);
            var jsonStorage = new JsonStorage(jsonSerializer, fileStorage);

            // Act
            var actualGridString = jsonStorage.LoadFromJson<string>("TestPath");

            // Assert
            Assert.Equal(expectedGridString, actualGridString);
        }
    }
}