using HotelAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HotelAPI.Services
{
    public class HotelService
    {
        private readonly IMongoCollection<Hotel> hotels;

        public HotelService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("HotelsDb"));
            var database = client.GetDatabase("HotelsDb");
            hotels = database.GetCollection<Hotel>("Hotels");
        }

        //Hotels CRUD

        public Hotel CreateHotel(Hotel hotel)
        {
            if (hotel.Rooms == null)
            {
                hotel.Rooms = new List<Room>();
            }
            hotels.InsertOne(hotel);
            return hotel;
        }

        public ReplaceOneResult UpdateHotel(Hotel hotel) => hotels.ReplaceOne(h => h.Id == hotel.Id, hotel);

        public DeleteResult DeleteHotel(string id) => hotels.DeleteOne(h => h.Id == id);

        public Hotel GetHotel(string id) => hotels.Find<Hotel>(hotel => hotel.Id.Equals(id)).FirstOrDefault();

        public List<Hotel> GetHotels() => hotels.Find(hotel => true).ToList();

        public List<Hotel> GetOwnerHotel(string ownerId) => hotels.Find(h => h.OwnerID.Equals(ownerId)).ToList();

        //Rooms CRUD        

        public RoomHotelViewModel CreateRoom(RoomHotelViewModel roomHotelViewModel)
        {
            var itemFilter = Builders<Hotel>.Filter.Eq(h => h.Id, roomHotelViewModel.HotelId);
            var updateBuilder = Builders<Hotel>.Update.AddToSet(h => h.Rooms, roomHotelViewModel.Room);

            hotels.UpdateOneAsync(itemFilter, updateBuilder, new UpdateOptions() { IsUpsert = true }).Wait();

            return roomHotelViewModel;
        }

        public Hotel DeleteRoom(string hotelId, string roomId)
        {
            var itemFilter = Builders<Hotel>.Filter.Eq(h => h.Id, hotelId);
            var updateBuilder = Builders<Hotel>.Update.PullFilter(h => h.Rooms, Builders<Room>.Filter.Eq("Id", roomId));
            var filter = new BsonDocument("username", "bodrum");
            hotels.UpdateOneAsync(itemFilter, updateBuilder, new UpdateOptions() { IsUpsert = true }).Wait();

            return hotels.Find<Hotel>(hotel => hotel.Id.Equals(hotelId)).FirstOrDefault();
        }

        public RoomHotelViewModel GetRoom(string hotelId, string roomId) {
            var room = hotels.Find(h => h.Id.Equals(hotelId)).FirstOrDefault().Rooms.Find(r => r.Id == roomId);
            RoomHotelViewModel roomHotelViewModel = new RoomHotelViewModel { HotelId = hotelId, Room = room };
            return roomHotelViewModel;
        }

        public List<Room> GetRooms(string hotelId) => hotels.Find(h => h.Id.Equals(hotelId)).FirstOrDefault().Rooms.ToList();

        public List<Room> GetFiltredRooms()
        {
            List<Hotel> hs = new List<Hotel>();

            hs = hotels.Find(h => true).ToList();

            List<Room> rooms = new List<Room>();

            foreach (var h in hs)
            rooms.AddRange(h.Rooms);

            return rooms;
        }

        public List<RoomHotelViewModel> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, string standard)
        {
            List<Hotel> hs = new List<Hotel>();

            if (!String.IsNullOrEmpty(city))
                hs = hotels.Find(h => h.City.Equals(city)).ToList();
            else
                hs = hotels.Find(h => true).ToList();

            if (!String.IsNullOrEmpty(phrase))
                hs = hs.Where(h => h.Name.Contains(phrase)).ToList();

            List<RoomHotelViewModel> roomHotelViewModels = new List<RoomHotelViewModel>();
            
            foreach (var h in hs)
                foreach (var r in h.Rooms)
                {
                    RoomHotelViewModel roomHotelViewModel = new RoomHotelViewModel { HotelId = h.Id, Room = r };
                    roomHotelViewModels.Add(roomHotelViewModel);
                }

            if (bedForOne > 0)
                roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.BedForOne == bedForOne).ToList();

            if (bedForTwo > 0)
                roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.BedForTwo == bedForTwo).ToList();

            if (price > 0)
                roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.Price == price).ToList();

            if (bedForOne > 0)
                roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.BedForOne == bedForOne).ToList();

            if (!String.IsNullOrEmpty(standard))
            {
                if (standard == "Standard")
                    roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.Standard == STANDARD.Standard).ToList();
                else
                    roomHotelViewModels = roomHotelViewModels.Where(r => r.Room.Standard == STANDARD.Exclusive).ToList();
            }
            return roomHotelViewModels;
        }
    }
}