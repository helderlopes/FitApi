public class CreateAthleteDto
{
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public List<int>? Equipments { get; set; }
    public List<int>? Activities { get; set; }
    public List<int>? Workouts { get; set; }
}