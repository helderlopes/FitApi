using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

class FitDb : DbContext
{
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutStep> WorkoutSteps => Set<WorkoutStep>();

    public FitDb(DbContextOptions<FitDb> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>()
        .HasOne(a => a.Equipment)
        .WithOne(e => e.Activity)
        .HasForeignKey<Equipment>(e => e.ActivityId)
        .IsRequired(false);

        modelBuilder.Entity<Workout>()
        .HasMany(w => w.Activities)
        .WithOne(a => a.Workout)
        .HasForeignKey(a => a.WorkoutId)
        .IsRequired(false);

        modelBuilder.Entity<Athlete>()
        .HasMany(a => a.Equipments)
        .WithOne(e => e.Athlete)
        .HasForeignKey(e => e.AthleteId)
        .IsRequired();

        modelBuilder.Entity<Athlete>()
        .HasMany(a => a.Activities)
        .WithOne(a => a.Athlete)
        .HasForeignKey(a => a.AthleteId)
        .IsRequired();

        modelBuilder.Entity<Athlete>()
        .HasMany(a => a.Workouts)
        .WithOne(w => w.Athlete)
        .HasForeignKey(w => w.AthleteId)
        .IsRequired();

         modelBuilder.Entity<Workout>()
        .HasMany(w => w.WorkoutSteps)
        .WithOne(ws => ws.Workout)
        .HasForeignKey(ws => ws.WorkoutId)
        .IsRequired();
    }
}