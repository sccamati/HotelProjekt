using WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationAsync(string id);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<bool> DeleteReservationAsync(string id);
        Task<bool> CreateReservationAsync(Reservation reservation);
        //Task<Reservation> UpdateReservationAsync(string id, Reservation reservation);
        Task<List<Reservation>> GetUsersReservations();
        Task<List<Reservation>> GetUsersReservations(string id);
        Task<List<Reservation>> GetOwnersReservations(string id);
        Task<List<Reservation>> GetRoomsReservations(string hotelId, string roomId);
    }
}
