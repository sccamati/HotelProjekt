using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface IHotelService
    {
        //Hotels CRUD
        Task<bool> CreateHotel(Hotel hotel);
        
        Task<Hotel> UpdateHotel(Hotel hotel);
        
        Task<bool> DeleteHotel(string id);

        Task<Hotel> GetHotel(string id);

        Task<List<Hotel>> GetHotels();

        Task<List<Hotel>> GetOwnerHotel(string ownerId);

        //Rooms CRUD        
        Task<bool> CreateRoom(RoomHotelViewModel roomHotelViewModel);

        Task<bool> DeleteRoom(string hotelId, string roomId);

        Task<RoomHotelViewModel> GetRoom(string hotelId, string roomId);

        Task<List<Room>> GetRooms(string hotelId);

        Task<List<RoomHotelViewModel>> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, string standard, string dateStart, string dateEnd);
    }
}
