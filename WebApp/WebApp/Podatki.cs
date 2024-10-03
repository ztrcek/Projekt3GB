using Microsoft.EntityFrameworkCore;

namespace WebApp
{
    public class Podatki: DbContext
    {
        //podatke iz razredov bo pisalo v podatkovno bazo
        public Podatki(DbContextOptions<Podatki> options): base(options)
        {

        }

        public virtual DbSet<Uporabnik> Uporabniki { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Uporabnik>()
                .HasIndex(u => u.ime)
                .IsUnique(); //ime se ne ponavlja
        }

    }
}
