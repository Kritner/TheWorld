using System.Collections.Generic;
using TheWorld.Models;

namespace TheWorld.Business.Interfaces
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
    }
}