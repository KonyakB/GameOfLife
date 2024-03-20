namespace GameOfLife.Interfaces;

/// <summary>
/// Specifies methods for reading and writing files.
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Writes the specified content to a file at the given path.
    /// </summary>
    /// <param name="path">The path of the file to write to.</param>
    /// <param name="content">The content to write to the file.</param>
    void Write(string path, string? content);

    /// <summary>
    /// Reads the content of a file at the given path.
    /// </summary>
    /// <param name="path">The path of the file to read.</param>
    /// <returns>The content of the file.</returns>
    string? Read(string path);
}

/// <summary>
/// Writes the specified content to a file.
/// </summary>
/// <param name="path">The path to the file.</param>
/// <param name="content">The content to write.</param>
public class FileStorage : IFileStorage
{
    /// <summary>
    /// Writes the specified content to the file at the given path.
    /// </summary>
    /// <param name="path">The path of the file to write.</param>
    /// <param name="content">The content to write to the file.</param>
    public void Write(string path, string? content)
    {
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Reads the content of a file specified by the given path.
    /// </summary>
    /// <param name="path">The path of the file to be read.</param>
    /// <returns>The content of the file as a string.</returns>
    public string? Read(string path)
    {
        return File.ReadAllText(path);
    }
}