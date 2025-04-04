using Microsoft.EntityFrameworkCore;

class AthleteDb : DbContext
{
    public AthleteDb(DbContextOptions<AthleteDb> options)
        : base(options) { }

    public DbSet<Athlete> Athletes => Set<Athlete>();
}