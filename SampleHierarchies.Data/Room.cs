namespace SampleHierarchies.Data;

/// <summary>
/// Room class.
/// </summary>
public class Room
{
    /// <summary>
    /// Properties
    /// </summary>
    #region Properties
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public double PricePerNight { get; set; }

    #endregion // Ctor
}
