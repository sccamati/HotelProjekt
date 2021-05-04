﻿using HotelAPI.Models;
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
        public Hotel CreateHotel(Hotel hotel)
        {
            hotels.InsertOne(hotel);
            return hotel;
        }
        public List<Hotel> GetHotels() => hotels.Find(hotel => true).ToList();
        public Hotel GetHotel(string id) => hotels.Find<Hotel>(hotel => hotel.Id.Equals(id)).FirstOrDefault();


        public Room CreateRoom(string hotelId, Room room)
        {
            var itemFilter = Builders<Hotel>.Filter.Eq(h => h.Id, hotelId);
            var updateBuilder = Builders<Hotel>.Update.AddToSet(h => h.Rooms, room);

            hotels.UpdateOneAsync(itemFilter, updateBuilder, new UpdateOptions() { IsUpsert = true }).Wait();

            return room;
        }
        public List<Room> GetRooms(string hotelId) => hotels.Find(h => h.Id.Equals(hotelId)).FirstOrDefault().Rooms.ToList();

        public Room GetRoom(string hotelId, int number) => hotels.Find(h => h.Id.Equals(hotelId)).FirstOrDefault().Rooms.Find(r => r.Number == number);

        public List<Room> GetFiltredRooms(string city, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
        {
            List<Hotel> hs = new List<Hotel>();
            
            if (!String.IsNullOrEmpty(city))
                hs = hotels.Find(h => h.City.Equals(city)).ToList();
            else
                hs = hotels.Find(h => true).ToList();
            
            List<Room> rooms = new List<Room>();
            
            foreach (var h in hs)
                foreach (var r in h.Rooms)
                    rooms.Add(r);

            if (bedForOne > 0)
                rooms = rooms.Where(r => r.BedForOne == bedForOne).ToList();

            if (bedForTwo > 0)
                rooms = rooms.Where(r => r.BedForTwo == bedForTwo).ToList();

            if (price > 0)
                rooms = rooms.Where(r => r.Price == price).ToList();

            if (bedForOne > 0)
                rooms = rooms.Where(r => r.BedForOne == bedForOne).ToList();

            if (standard == 0)
                rooms = rooms.Where(r => r.Standard == STANDARD.Standard).ToList();
            else if (standard == 1)
                rooms = rooms.Where(r => r.Standard == STANDARD.Exclusive).ToList();

            return rooms;
        }

        public List<Hotel> GetOwnerHotels(string ownerId) => hotels.Find(h => h.OwnerID.Equals(ownerId)).ToList();
    }
}