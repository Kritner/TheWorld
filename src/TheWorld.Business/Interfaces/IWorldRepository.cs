﻿using System.Collections.Generic;
using TheWorld.Models;

namespace TheWorld.Business.Interfaces
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName);
        void AddStop(string tripName, Stop newStop);
    }
}