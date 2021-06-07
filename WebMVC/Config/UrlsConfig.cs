using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Config
{
    public class UrlsConfig
    {
        public class ReservationOperations
        {
            public static string GetById(string id) => $"https://localhost:44343/api/Reservation/{id}";
            public static string Get() => $"https://localhost:44343/api/Reservation";
            public static string Delete(string id) => $"https://localhost:44343/api/Reservation/{id}";
            public static string Create() => $"https://localhost:44343/api/Reservation/";
            public static string Update() => $"https://localhost:44343/api/Reservation/";
            public static string GetUsersRes(string id) => $"https://localhost:44343/api/Reservation/UsersRes/{id}";
        }
        public class HotelOperations
        {
            public static string CreateHotel() => $"https://localhost:44316/api/Hotel/";
            public static string UpdateHotel(string id) => $"https://localhost:44316/api/Hotel/{id}";
            public static string DeleteHotel(string id) => $"https://localhost:44316/api/Hotel/{id}";
            public static string GetHotel(string id) => $"https://localhost:44316/api/Hotel/{id}";
            public static string GetHotels() => $"https://localhost:44316/api/Hotel/";
            public static string GetOwnerHotels(string ownerId) => $"https://localhost:44316/api/Hotel/ownerHotels/{ownerId}";
            public static string CreateRoom(string id) => $"https://localhost:44316/api/Hotel/room/{id}";
            public static string DeleteRoom(string id, int number) => $"https://localhost:44316/api/Hotel/room/{id}&{number}";
            public static string GetRoom(string id, int number) => $"https://localhost:44316/api/Hotel/room/{id}&{number}";
            public static string GetRooms(string id) => $"https://localhost:44316/api/Hotel/rooms/all/{id}";
            public static string GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
                => $"https://localhost:44316/api/Hotel/rooms/filtred{city}&{phrase}&{bedForOne}&{bedForTwo}&{numberOfGuests}&{price}&{standard}";
        }

        public class UserOperations
        {
            public static string GetById(string id) => $"https://localhost:44308/api/User/{id}";
            public static string Get() => $"https://localhost:44308/api/User/";
            public static string Delete(string id) => $"https://localhost:44308/api/User/{id}";
            public static string Create() => $"https://localhost:44308/api/User/";
            public static string Update() => $"https://localhost:44308/api/User/";
        }

        public class AuthorizeOperations
        {
            public static string LogIn() => $"https://localhost:44308/api/Identity/";
        }
    }
}
