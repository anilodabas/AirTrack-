using Microsoft.EntityFrameworkCore;
using AirTrack.Entity.Account;



namespace AirTrack.Entity
{
    public class AirTrackContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlServer("Data source=(localdb)\\Anil;Database=AirBase8 ;Trusted_Connection=True;Integrated Security=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


      

    }
}
