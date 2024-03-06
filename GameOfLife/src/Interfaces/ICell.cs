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
    /// How many neighbors there is in <see cref="Neighbors"/> List 
    /// Subsequently how many <c>Neighbors</c> current <see cref="Cell"/> has
    /// <seealso cref="List{T}"/> <seealso cref="int"/>
    /// </summary>
    /// <returns>
    /// Integer with number of neighbors
    /// </returns>
    public int NumOfNeighbors => Neighbors.Count;

    /// <summary>
    /// Method for adding ONE neighbor(<see cref="Cell"/>) to the List of <see cref="Neighbors"/>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// </summary>
    /// <param name="cell"><see cref="Cell"/> to add as a neighbor of this cell to the List of <see cref="Neighbors"/> of the current Cell</param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool AddNeighbor(Cell cell);
    
    /// <summary>
    /// Method for removing ONE neighbor(<see cref="Cell"/>) from a <see cref="Neighbors"/> List by it's <c>Cell</c> <see cref="Id"/>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// <seealso cref="Guid"/>
    /// </summary>
    /// <param name="cellId"><see cref="Guid"/> --> <see cref="Cell"/>'s <see cref="Id"/> to remove from <see cref="Neighbors"/> List of the current Cell </param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool RemoveNeighbor(Guid cellId);

    /// <summary>
    /// Method for adding multiple <see cref="Cell"/>'s to the <see cref="Neighbors"/> <c>List</c>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// <seealso cref="Cell"/> <seealso cref="Array"/>
    /// </summary>
    /// <param name="cells"><see cref="Cell"/> array, to add to the <see cref="Neighbors"/> of the current Cell</param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool AddMultipleNeighbors(Cell[] cells);
    /// <summary>
    /// Method for removing multiple <see cref="Cell"/>'s from the <see cref="Neighbors"/> <c>List</c>
    /// <seealso cref="List{T}"/> <seealso cref="Neighbors"/>
    /// <seealso cref="Cell"/> <seealso cref="Array"/>
    /// <seealso cref="Guid"/>
    /// </summary>
    /// <param name="cellIds"><see cref="Guid"/> array of <see cref="Id"/>'s of <see cref="Cell"/>'s
    /// to remove from the <see cref="Neighbors"/> of the current Cell</param>
    /// <returns><c>true</c> if successful, <c>false</c> if fail</returns>
    public bool RemoveMultipleNeighbors(Guid[] cellIds);
}
