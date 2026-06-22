using System;
using System.Collections.Generic;
using System.Text;
using FitElite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitElite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
