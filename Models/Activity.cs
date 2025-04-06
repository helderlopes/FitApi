public class Activity
{
    public int Id { get; set; }
    public Athlete? Athlete { get; set; }
    public int AthleteId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public TimeSpan MovingTime { get; set; }
    public TimeSpan ElapsedTime { get; set; }
    public int TotalElevationGain { get; set; }
    public double Distance { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public double? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public double? AveragePower { get; set; }
    public int? MaxPower { get; set; }
    public Workout? Workout { get; set; } 
    public int? WorkoutId { get; set; }
    public Equipment? Equipment { get; set; }
    public int? EquipmentId { get; set;}
}
