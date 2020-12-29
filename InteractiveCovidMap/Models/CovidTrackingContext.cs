using Microsoft.EntityFrameworkCore;

namespace CovidTracking.Models
{
    public class CovidTrackingContext : DbContext
    {
        public CovidTrackingContext(DbContextOptions<CovidTrackingContext> options) : base(options) { }
        
        public DbSet<CurrentState> CurrentStates { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrentState>(currentState =>
            {
                currentState.HasKey(cs => cs.CurrentStateId);
                currentState.HasIndex(cs => cs.CurrentStateId);
            });
        }
    }
}