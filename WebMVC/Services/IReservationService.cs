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
        Task<Reservation> DeleteReservationAsync(string id);
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        //Task<Reservation> UpdateReservationAsync(string id, Reservation reservation);
    }
}
