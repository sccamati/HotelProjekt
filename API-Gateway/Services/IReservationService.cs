using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationAsync(string id);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> DeleteReservationAsync(string id);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
    }
}
