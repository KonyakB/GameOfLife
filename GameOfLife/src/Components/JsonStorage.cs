

using GameOfLife.Interfaces;

namespace GameOfLife.Components;

/// <summary>
/// Represents a component that provides functionality to save and load objects to/from JSON format.
/// </summary>
public class JsonStorage(IJsonSerializer jsonSerializer, IFileStorage fileStorage) : IJsonStorage
{
    /// <summary>
    /// Represents the path of the JSON file.
    /// </summary>
    private const string FilePath = "grid.json";

    /// <summary>
    /// Saves an object to JSON format and writes it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object to be saved.</typeparam>
    /// <param name="grid">The object to be saved.</param>
    /// <param name="filePath">The file path to save the JSON data. If not specified, the default file path "grid.json" will be used.</param>
    /// <returns>The JSON string representing the serialized object.</returns>
    public string SaveToJson<T>(T grid, string filePath = FilePath)
    {
        var jsonString = jsonSerializer.Serialize(grid);
        fileStorage.Write(filePath, jsonString);
        return jsonString;
    }

    /// <summary>
    /// Loads an object of type T from a JSON file.
    /// </summary>
    /// <typeparam name="T">The type of object to load.</typeparam>
    /// <param name="filePath">The path of the JSON file to load from.</param>
    /// <returns>An object of type T if the loading is successful; otherwise, null.</returns>
    public T? LoadFromJson <T>(string filePath)
    {
        var json = fileStorage.Read(filePath);
        return jsonSerializer.Deserialize<T>(json);
    }
}