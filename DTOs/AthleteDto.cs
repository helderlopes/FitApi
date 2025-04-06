public class AthleteReadDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public List<int>? EquipmentIds { get; set; }
    public List<int>? ActivityIds { get; set; }
    public List<int>? WorkoutIds { get; set; }
}

public class AthleteCreateDto
{
    public required string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public List<int>? EquipmentIds { get; set; }
    public List<int>? ActivityIds { get; set; }
    public List<int>? WorkoutIds { get; set; }
}

public class AthleteUpdateDto
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public List<int>? EquipmentIds { get; set; }
    public List<int>? ActivityIds { get; set; }
    public List<int>? WorkoutIds { get; set; }
}