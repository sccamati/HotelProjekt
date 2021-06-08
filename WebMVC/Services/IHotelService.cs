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

        Task<List<Hotel>> GetOwnerHotels(string ownerId);

        //Rooms CRUD        
        Task<Room> CreateRoom(string hotelId, Room room);

        Task<Hotel> DeleteRoom(string hotelId, int number);

        Task<Room> GetRoom(string hotelId, int number);

        Task<List<Room>> GetRooms(string hotelId);

        Task<List<RoomHotelViewModel>> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, string standard, string dateStart, string dateEnd);
    }
}
