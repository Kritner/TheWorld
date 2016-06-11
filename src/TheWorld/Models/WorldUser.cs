using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace TheWorld.Data
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}