using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using frushgah.Models;

namespace frushgah.Data
{
    public class frushgahContext : DbContext
    {
        public frushgahContext(DbContextOptions<frushgahContext> options)
            : base(options)
        {
        }

        // public DbSet<frushgah.Models.user>? user { get; set; } = default!;
        public DbSet<frushgah.Models.group>? group { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // modelBuilder.Entity<v_user>()
            // .ToView(nameof(v_user))
            // .HasNoKey();

            // modelBuilder.Entity<v_never>()
            // .ToView(nameof(v_never))
            // .HasNoKey();

            // modelBuilder.Entity<count_jensiat>()
            // .ToView(nameof(count_jensiat))
            // .HasNoKey();      
            
            // modelBuilder.Entity<count_khata>()
            // .ToView(nameof(count_khata))
            // .HasNoKey();




        }

    }
}
