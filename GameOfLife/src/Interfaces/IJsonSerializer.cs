using System.Text.Json;

namespace GameOfLife.Interfaces;

/// <summary>
/// Represents a JSON serializer.
/// </summary>
public interface IJsonSerializer
{
    /// <summary>
    /// Serializes an object to JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the object to be serialized.</typeparam>
    /// <param name="grid">The object to be serialized.</param>
    /// <returns>A JSON string representing the serialized object.</returns>
    string? Serialize<T>( T grid);

    /// <summary>
    /// Deserialize the specified JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>An object of type T if deserialization is successful; otherwise, null.</returns>
    T? Deserialize <T>(string? json);
}

/// <summary>
/// JsonSerializerAdapter class is an implementation of the IJsonSerializer interface for serializing and deserializing objects using JSON format.
/// </summary>
public class JsonSerializerAdapter : IJsonSerializer
{
    /// Provides options for serializing and deserializing JSON data.
    /// @remarks
    /// This interface is implemented by the `JsonSerializerAdapter` class.
    /// @see {@link JsonSerializerAdapter}
    /// /
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

    /// <summary>
    /// Serializes the specified object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="grid">The object to serialize.</param>
    /// <returns>A JSON string representation of the serialized object.</returns>
    public string? Serialize<T>(T grid)
    {
        return JsonSerializer.Serialize(grid, _options);
    }

    /// <summary>
    /// Deserializes the provided JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>An object of type T if the deserialization is successful, null otherwise.</returns>
    public T? Deserialize<T>(string? json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}