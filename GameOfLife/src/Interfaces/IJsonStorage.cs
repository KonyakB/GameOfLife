

namespace GameOfLife.Interfaces;

/// <summary>
/// Represents an interface for saving and loading objects to/from JSON format.
/// </summary>
public interface IJsonStorage
{
    /// <summary>
    /// Saves an object to JSON format and writes it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object to be saved.</typeparam>
    /// <param name="grid">The object to be saved.</param>
    /// <param name="filePath">The file path to save the JSON data. If not specified, the default file path "grid.json" will be used.</param>
    /// <returns>The JSON string representing the serialized object.</returns>
    public string SaveToJson<T>(T grid, string filePath);

    /// *Summary:**
    public T? LoadFromJson<T>(string filePath );
}