using CovidTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace CovidTracking.Data
{
    public class CovidTrackingContext : DbContext
    {
        public CovidTrackingContext(DbContextOptions<CovidTrackingContext> options) : base(options) { }

        public DbSet<CurrentState> CurrentStates { get; set; }

        public DbSet<StateCodeName> StateCodeNames { get; set; }

        public DbSet<CountyCodeName> CountyCodeNames { get; set; }

        public DbSet<CountyDatePositive> CountyDatePositives { get; set; }

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

            modelBuilder.Entity<CountyCodeName>(countyCodeName =>
            {
                countyCodeName.HasKey(ccn => ccn.CountyCodeNameId);
                countyCodeName.HasIndex(ccn => ccn.CountyCodeNameId);
                countyCodeName.HasIndex(ccn => ccn.CountyFips);
                countyCodeName.HasIndex(ccn => ccn.StateFips);
                countyCodeName.HasMany(ccn => ccn.CountyDatePositives)
                              .WithOne(ccn => ccn.CountyCodeName)
                              .HasForeignKey(ccn => ccn.CountyCodeNameId);
            });

            modelBuilder.Entity<CountyDatePositive>(countyDatePositive => {
                countyDatePositive.HasKey(cdp => cdp.CountyDatePositiveId);
                countyDatePositive.HasIndex(cdp => cdp.CountyDatePositiveId);
                countyDatePositive.HasIndex(cdp => cdp.CountyCodeNameId);
                countyDatePositive.HasOne(cdp => cdp.CountyCodeName)
                                  .WithMany(cdp => cdp.CountyDatePositives)
                                  .HasForeignKey(cdp => cdp.CountyCodeNameId);
            });
        }
    }
}