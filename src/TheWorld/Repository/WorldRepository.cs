using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Business.Interfaces;
using TheWorld.Data;
using TheWorld.Models;

namespace TheWorld.Repository
{
    public class WorldRepository : IWorldRepository
    {

        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName, userName);
            newStop.Order = theTrip.Stops.Max(m => m.Order) + 1;
            theTrip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context
                    .Trips
                    .OrderBy(ob => ob.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context
                    .Trips
                    .Include(i => i.Stops)
                    .OrderBy(ob => ob.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context
                .Trips
                .Include(i => i.Stops)
                .Where(w => w.Name == tripName && w.UserName == userName)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserAllTripsWithStops(string name)
        {
            try
            {
                return _context
                    .Trips
                    .Include(i => i.Stops)
                    .OrderBy(ob => ob.Name)
                    .Where(w => w.UserName == name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
