using ReservationAPI.Models;
using ReservationAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReservationAPI.Database
{
    public class DatabaseInitializer
    {
        public static void Seed(ReservationService reservationService)
        {
            if (!reservationService.GetReservations().Any())
            {
                var users = new List<Reservation>()
                {
                    new Reservation()
                    {
                        UserId = "123456789123456789123451",
                        RoomId = "1",
                        HotelId = "123456789012345678901234",
                        Price = 200,
                        StartDate = System.DateTime.Now.AddDays(4),
                        EndDate = System.DateTime.Now.AddDays(6),
                        ReservationDate = System.DateTime.Now
                    },
                    new Reservation()
                    {
                        UserId = "123456789123456789123451",
                        RoomId = "2",
                        HotelId = "123456789012345678901234",
                        Price = 400,
                        StartDate = System.DateTime.Now.AddDays(12),
                        EndDate = System.DateTime.Now.AddDays(15),
                        ReservationDate = System.DateTime.Now
                    },
                    new Reservation()
                    {
                        UserId = "123456789123456789123451",
                        RoomId = "1",
                        HotelId = "123456789012345678901236",
                        Price = 400,
                        StartDate = System.DateTime.Now.AddDays(8),
                        EndDate = System.DateTime.Now.AddDays(10),
                        ReservationDate = System.DateTime.Now
                    },
                };

                users.ForEach(user => reservationService.Create(user));
            }
        }
    }
}
