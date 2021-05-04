using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> reservations;

        public ReservationService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ReservationDb"));
            var database = client.GetDatabase("ReservationDb");
            reservations = database.GetCollection<Reservation>("Reservations");
        }

        public List<Reservation> GetReservations() => reservations.Find(reservations => true).ToList();
        public Reservation GetReservation(string id) => reservations.Find<Reservation>(reservations => reservations.Id == id).FirstOrDefault();
        public List<Reservation> GetUserReservations(string userId) => reservations.Find(reservations => reservations.UserId == userId).ToList();
        public List<Reservation> GetOwnerReservations(string hotelId) => reservations.Find(reservations => reservations.HotelId == hotelId).ToList();

        public Reservation Create(Reservation reservation)
        {
            reservations.InsertOne(reservation);
            return reservation;
        }

        public void DeleteReservation(string id)
        {
            reservations.DeleteOne(r => r.Id == id);
        }
    }
}
