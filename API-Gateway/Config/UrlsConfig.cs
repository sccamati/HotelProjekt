using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway.Config
{
    public class UrlsConfig
    {
        public class ReservationOperations
        {
            public static string GetById(string id) => $"api/ReservationController/Get/{id}";
            public static string Get() => $"api/ReservationController/Get";
            public static string Delete(string id) => $"api/ReservationController/Delete/{id}";
            public static string Create(Reservation reservation) => $"api/ReservationController/Create/{reservation}";
            public static string Update(string id, Reservation reservation) => $"api/ReservationController/Put/";
        }
        public class HotelOperations
        {

        }

        public string Reservations;
    }
}
