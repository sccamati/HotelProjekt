using HotelAPI.Models;
using HotelAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace IdentityAPI.Database
{
    public class DatabaseInitializer
    {
        public static void Seed(HotelService hotelService)
        {
            if (!hotelService.GetHotels().Any())
            {
                var hotels = new List<Hotel>()
                {
                    new Hotel()
                    {
                        Address = "Warszawska 4",
                        City = "Sieldce",
                        Name = "Mily hotel",
                        OwnerID = "123456789123456789123456",
                        Rooms = new List<Room>
                        {
                            new Room
                            {
                                BedForOne = 1,
                                BedForTwo = 2,
                                Description = "Elegancki pokoj",
                                NumberOfGuests = 5,
                                Price = 100,
                                Standard = STANDARD.Exclusive
                            },
                            new Room
                            {
                                BedForOne = 2,
                                BedForTwo = 2,
                                Description = "Zwykly pokoj",
                                NumberOfGuests = 6,
                                Price = 60,
                                Standard = STANDARD.Standard
                            }
                        }
                    },
                    new Hotel()
                    {
                        Address = "Warszawska 4",
                        City = "Warszawa",
                        Name = "Bardzo fajny hotel",
                        OwnerID = "123456789123456789123456",
                        Rooms = new List<Room>
                        {
                            new Room
                            {
                                BedForOne = 1,
                                BedForTwo = 2,
                                Description = "Elegancki pokoj",
                                NumberOfGuests = 5,
                                Price = 200,
                                Standard = STANDARD.Exclusive
                            },
                            new Room
                            {
                                BedForOne = 2,
                                BedForTwo = 2,
                                Description = "Zwykly pokoj",
                                NumberOfGuests = 6,
                                Price = 120,
                                Standard = STANDARD.Standard
                            }
                        }
                    },
                };

                hotels.ForEach(hotel => hotelService.CreateHotel(hotel));
            }
        }
    }
}
