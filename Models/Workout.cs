public class Workout
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string SportType { get; set; }
    public required List<WorkoutStep> Steps { get; set; }
    public required Athlete Athlete { get; set; }
}
