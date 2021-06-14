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
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ReservationDb"));
            var database = client.GetDatabase("ReservationDb");

            _reservations = database.GetCollection<Reservation>("Reservations");
        }

        public List<Reservation> GetReservations() => _reservations.Find(reservations => true).ToList();
        public Reservation GetReservation(string id) => _reservations.Find<Reservation>(reservations => reservations.Id == id).FirstOrDefault();
        public List<Reservation> GetUserReservations(string userId) => _reservations.Find(reservations => reservations.UserId == userId).ToList();
        public List<Reservation> GetOwnersReservations(string hotelId) => _reservations.Find(reservations => reservations.HotelId == hotelId).ToList();

        public Reservation Create(Reservation reservation)
        {
            reservation.ReservationDate = DateTime.Now;
            reservation.Price *= (reservation.EndDate - reservation.StartDate).Days;
            _reservations.InsertOne(reservation);
            return reservation;
        }

        public ReplaceOneResult Update(string id, Reservation reservation)
        {
            return _reservations.ReplaceOne(r => r.Id == id, reservation);
        }

        public DeleteResult Delete(string id)
        {
            return _reservations.DeleteOne(r => r.Id == id);
        }

    }
}
