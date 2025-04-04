public class WorkoutStep
{
    public int Order { get; set; }
    public required string Type { get; set; }
    public TimeSpan Duration { get; set; }
    public int? TargetMinHeartRate { get; set; }
    public int? TargetMaxHeartRate { get; set; }
    public int? TargetMinPower { get; set; }
    public int? TargetMaxPower { get; set; }
    public required Workout Workout { get; set; }
}
