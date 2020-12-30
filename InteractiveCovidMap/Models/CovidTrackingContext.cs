using Microsoft.EntityFrameworkCore;

namespace CovidTracking.Models
{
    public class CovidTrackingContext : DbContext
    {
        public CovidTrackingContext(DbContextOptions<CovidTrackingContext> options) : base(options) { }

        public DbSet<CurrentState> CurrentStates { get; set; }

        public DbSet<StateCodeName> StateCodeNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrentState>(currentState =>
            {
                currentState.HasKey(cs => cs.CurrentStateId);
                currentState.HasIndex(cs => cs.CurrentStateId);
                currentState.HasIndex(cs => cs.Fips);
            });

            modelBuilder.Entity<StateCodeName>(stateCodeName =>
            {
                stateCodeName.HasKey(scn => scn.StateCodeNameId);
                stateCodeName.HasIndex(scn => scn.StateCodeNameId);
                stateCodeName.HasIndex(scn => scn.Fips);
            });
        }
    }
}