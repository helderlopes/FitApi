public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Distance { get; set; } 
    public Athlete? Athlete { get; set; }
    public int? AthleteId { get; set; }
    public List<Activity>? Activities { get; set; }
}