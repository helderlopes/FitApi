public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public List<WorkoutStep>? WorkoutSteps { get; set; }
    public Athlete? Athlete { get; set; }
    public int? AthleteId { get; set; }
    public List<Activity>? Activities { get; set; }
}
