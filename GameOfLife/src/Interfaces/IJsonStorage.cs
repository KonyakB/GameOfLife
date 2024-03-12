

using GameOfLife.Components;

namespace GameOfLife.Interfaces;

/// <summary>
/// Represents an interface for saving and loading objects to/from JSON format.
/// </summary>
public interface IJsonStorage
{
    /// <summary>
    /// Saves an object to JSON format and writes it to a file.
    /// </summary>
    /// <param name="grid">The object to be saved.</param>
    /// <param name="filePath">The file path to save the JSON data. If not specified, the default file path "grid.json" will be used.</param>
    /// <returns>The JSON string representing the serialized object.</returns>
    public string? SaveToJson(Grid grid, string filePath);


    /// <summary>
    /// Loads an object of type T from a JSON file.
    /// </summary>
    /// <param name="filePath">The path of the JSON file to load from.</param>
    /// <returns>An object of type T if the loading is successful; otherwise, null.</returns>
    public Grid? LoadFromJson(string filePath );
}