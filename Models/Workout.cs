public class Workout
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string SportType { get; set; }
    public required List<WorkoutStep> WorkoutSteps { get; set; }
    public required Athlete Athlete { get; set; }
    public required int AthleteId { get; set; }
    public List<Activity>? Activities { get; set; }
}
