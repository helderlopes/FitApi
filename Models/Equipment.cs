public class Equipment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public double Distance { get; set; } 
    public required Athlete Athlete { get; set; }
    public required int AthleteId { get; set; }
    public required Activity Activity { get; set; }
    public required int ActivityId { get; set; }
}