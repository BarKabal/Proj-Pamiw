using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Infrastructure.Repositories
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<ApplyForm> ApplyForm { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Session> Session { get; set; }
    }
}
