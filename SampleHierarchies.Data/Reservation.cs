namespace SampleHierarchies.Data;

/// <summary>
/// Animal base class with basic implementations.
/// </summary>
public class Reservation 
{
    public string? GuestName { get; set; }
    public string? ReservationNr { get; set; }
    public int Room { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}
