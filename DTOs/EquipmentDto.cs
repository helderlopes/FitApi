public class EquipmentReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Distance { get; set; } 
    public int? AthleteId { get; set; }
    public List<int>? ActivityIds { get; set; }
}

public class EquipmentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Distance { get; set; } 
    public int? AthleteId { get; set; }
    public List<int>? ActivityIds { get; set; }
}

public class EquipmentUpdateDto
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public double? Distance { get; set; } 
    public int? AthleteId { get; set; }
    public List<int>? ActivityIds { get; set; }
}
