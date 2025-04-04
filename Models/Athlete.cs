public class Athlete
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public List<Equipment>? Equipments { get; set; }
    public List<Activity>? Activities { get; set; }
    public List<Workout>? Workouts { get; set; }
}