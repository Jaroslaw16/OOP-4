namespace SampleHierarchies.Data
{
    // Hotel class
    public class Hotel
    {
        /// <summary>
        ///  Ctor and Properties
        /// </summary>
        public List<Room> Rooms { get ; set; } = new List<Room>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        #region Public Methods

        // Method to create a reservation
        public void MakeReservation(string guestName, int? rooms, DateTime? checkInDate, DateTime? checkOutDate)
        {
            try
            {
                DataUpdate();

                var availableRoom = Rooms
                    .Where(room => room.IsAvailable == true)
                    .Where(room => room.Capacity >= rooms)
                    .ToList();

                if (availableRoom.Count == 0) { Console.WriteLine("\n All rooms are full."); return; }

                Console.WriteLine("---------------------");
                Console.WriteLine("Availability rooms:");
                for (int i = 0; i < availableRoom.Count; i++)
                {

                    Console.WriteLine($"{i}. Room number: {availableRoom[i].RoomNumber}, room capacity: {availableRoom[i].Capacity}, Price per night: {availableRoom[i].PricePerNight}");
                }
                Console.WriteLine("Choose room: ");

                if (!int.TryParse(Console.ReadLine(), out int roomId)) { Console.WriteLine("Invalid room"); throw new InvalidDataException(); }
                if (roomId > Rooms.Count) { Console.WriteLine("Invalid room"); throw new InvalidDataException(); }

                Reservation reservation = new()
                {
                    GuestName = guestName,
                    Room = availableRoom[roomId].RoomNumber,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
                };

                if (Reservations == null || Reservations.Count == 0)
                {
                    reservation.ReservationNr = $"R100";
                }
                else
                {
                    reservation.ReservationNr = $"R{int.Parse(Reservations.Last().ReservationNr.Substring(1,3)) + 1}";
                }
               
                Rooms[Rooms.FindIndex(room => room.RoomNumber == reservation.Room)].IsAvailable = false;
                Reservations.Add(reservation);

                DataService.Write(this, "data.json");

                Console.WriteLine($"Your reservation has been successfully completed. Reservation number: {reservation.ReservationNr}");
            }
            catch
            {
                    
            }
            
        }

        // Method to check availability rooms
        public List<Room> CheckAvailability()
        {
            var hotel = DataService.Read("data.json");

            var availableRooms = hotel.Rooms
                .Where(room => room.IsAvailable == true)
                .ToList();

            return availableRooms;
        }
        // Method to cancel a reservation
        public void CancelReservation(string reservationNr)
        {
            try
            {
                DataUpdate();

                var reservationToCancel = Reservations
                        .Where(reservation => reservation.ReservationNr == reservationNr)
                        .FirstOrDefault();
                if (reservationToCancel == null) { Console.WriteLine("No reservation found"); throw new Exception(); }

                Reservations.RemoveAt(Reservations.FindIndex(reservation => reservation.ReservationNr == reservationNr));
                Rooms[Rooms.FindIndex(room => room.RoomNumber == reservationToCancel.Room)].IsAvailable = true;
                DataService.Write(this, "data.json");
                Console.WriteLine("Reservation was successfully canceled");
            }
            catch 
            {
                Console.WriteLine("Error while canceling reservation");
            }

        }
        #endregion // Public Methods

        #region Private Methods

        // Method for update Rooms and Reservations
        private void DataUpdate()
        {
            Rooms = DataService.Read("data.json").Rooms ?? new List<Room>();
            Reservations = DataService.Read("data.json").Reservations ?? new List<Reservation>();
        }

        #endregion // Private Methods
    }
}
