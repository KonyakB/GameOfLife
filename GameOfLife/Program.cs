// See https://aka.ms/new-console-template for more information

using GameOfLife.Components;

/// <summary>
/// Main class, it just evokes <see cref="TheGameOfLife"/> class instance
/// moved all the required logic(in assignment description) inside of Program to the <see cref="TheGameOfLife"/> class
/// </summary>
internal class Program
{
    public static void Main(string[] args)
    {
        TheGameOfLife theGameOfLife = new();

        theGameOfLife.Run();
    }
}