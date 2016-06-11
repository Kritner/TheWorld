using System.Collections.Generic;
using TheWorld.Models;

namespace TheWorld.Business.Interfaces
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName, string userName);
        void AddStop(string tripName, string userName, Stop newStop);
        IEnumerable<Trip> GetUserAllTripsWithStops(string name);
    }
}