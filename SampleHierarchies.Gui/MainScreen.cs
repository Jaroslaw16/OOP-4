using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using System.Data;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    /// 
    private readonly Hotel _hotel = new();

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Check Availability");
            Console.WriteLine("2. Make reservation");
            Console.WriteLine("3. Cancel reservation");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.CheckAvailability:
                        CheckAvailability();
                        break;
                    case MainScreenChoices.MakeReservation:
                        MakeResersavation();
                        break;
                    case MainScreenChoices.CancelReservation:
                        CancelReservation();
                        break;
                    case MainScreenChoices.Exit:
                        Console.WriteLine("Goodbye.");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    private void CheckAvailability()
    {
        List<Room>? rooms = _hotel.CheckAvailability();
        if (rooms == null || rooms.Count == 0) { Console.WriteLine("All rooms are full"); return;}
        Console.WriteLine("Availability rooms:");
        foreach (var room in rooms)
        {
            Console.WriteLine($"Room number: {room.RoomNumber}, room capacity: {room.Capacity}, Price per night: {room.PricePerNight}");
        }
    }
    private void MakeResersavation()
    {
        try
        {
            Console.Write("Write guest name: ");
            string? guestName = Console.ReadLine() ?? "Anonymous";
            Console.Write("Write rooms: ");
            int? rooms = int.Parse(Console.ReadLine() ?? "");
            if (rooms ==  null ||  rooms <= 0) { Console.WriteLine("Rooms cannot be less than 1"); throw new InvalidDataException(); }
            Console.Write("Check in date (yyyy-mm-dd): ");
            DateTime? checkInDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Check out date (yyyy-mm-dd): ");
            DateTime? checkOutDate = DateTime.Parse(Console.ReadLine());
            if (checkInDate == null || checkOutDate == null)  { throw new InvalidDataException(); }
            if (checkInDate >= checkOutDate) { Console.WriteLine("Check in date and check out date cannot be less than a day") ; throw new InvalidDataException(); }
            if (checkInDate < DateTime.Now) { Console.WriteLine("Check in date must be no earlier than today "); throw new InvalidDataException(); }
            _hotel.MakeReservation(guestName, rooms, checkInDate, checkOutDate);
        }
        catch
        {
            Console.WriteLine("Invalid input. Try again.");
        }


    }
    private void CancelReservation()
    {
        Console.Write("Write your reservation nr: ");
        string? reservationNr = Console.ReadLine() ?? ""; 
        Console.WriteLine("Are you sure you want to cancel the reservation? Yes/No");
        if (Console.ReadLine()?.ToLower() != "yes") return;
        _hotel.CancelReservation(reservationNr);
    }

    #endregion // Private Methods 
}