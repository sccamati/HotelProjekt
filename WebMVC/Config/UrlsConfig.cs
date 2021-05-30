﻿using System;
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

        }

        public class UserOperations
        {
            public static string GetById(string id) => $"https://localhost:44308/api/User/{id}";
            public static string Get() => $"https://localhost:44308/api/User";
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
