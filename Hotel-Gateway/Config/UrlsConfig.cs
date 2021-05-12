using Hotel_Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Gateway.Config
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
        }
        public class HotelOperations
        {

        }

        public class UserOperations
        {

        }

    }
}
