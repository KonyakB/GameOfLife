using GameOfLife.Components;

namespace GameOfLife.Interfaces;

//TODO: come back here to remove methods/properties that were not used by others cause this interface/class, it's super bloated, but at least it was good practice

///<summary>
/// Interface for Cell in Game of Life
///</summary>
interface ICell
{
   /// <value>Property <c>Id</c> is cell's unique identification mostly used for removing it form the list of neighbors
   /// <seealso cref="Guid"/>
   /// </value>
    public Guid Id { get; set; }
   /// <value>Property <c>IsAlive</c> is a boolean stating if cell is alive(<c>true</c>) or not(<c>false</c>)
   /// <seealso cref="Boolean"/>
   /// </value>
    public bool IsAlive { get; set; }

   /// <summary>A list storing all the <see cref="Cell"/>'s that are <c>Neighbors</c> of the current Cell</summary>
    public List<Cell> Neighbors { get; set; }
    
    /// <summary>
    /// How many neighbors alive there is in <see cref="Neighbors"/> List 
    /// Subsequently how many <c>Neighbors</c> current <see cref="Cell"/> has
    /// <seealso cref="List{T}"/> <seealso cref="int"/>
    /// </summary>
    /// <returns>
    /// Integer with number of neighbors
    /// </returns>
    public int NumOfAliveNeighbors => Neighbors.Count((cell) => cell.IsAlive); //not sure if it's a good practice

    /// <summary>
    /// Method for adding ONE neighbor(<see cref="Cell"/>) to the List of <see cref="Neighbors"/>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// </summary>
    /// <param name="cell"><see cref="Cell"/> to add as a neighbor of this cell to the List of <see cref="Neighbors"/> of the current Cell</param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool AddNeighbor(Cell cell);
    

    /// <summary>
    /// Method for adding multiple <see cref="Cell"/>'s to the <see cref="Neighbors"/> <c>List</c>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// <seealso cref="Cell"/> <seealso cref="Array"/>
    /// </summary>
    /// <param name="cells"><see cref="Cell"/> array, to add to the <see cref="Neighbors"/> of the current Cell</param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool AddMultipleNeighbors(Cell[] cells);
}
