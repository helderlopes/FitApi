public class EquipmentService
{
    private readonly IRepository<Equipment> _repository;

    public EquipmentService(IRepository<Equipment> repository)
    {
        _repository = repository;
    }

    public async Task<EquipmentReadDto?> GetByIdAsync(int id)
    {
        var equipment = await _repository.GetByIdAsync(id);
        return equipment is null ? null : ToReadDto(equipment);
    }

    public async Task<List<EquipmentReadDto>> GetAllAsync()
    {
        var equipments = await _repository.GetAllAsync();
        return equipments.Select(e => ToReadDto(e)).ToList();
    }

    public async Task<EquipmentReadDto> CreateAsync(EquipmentCreateDto dto)
    {
        var equipment = new Equipment
        {
            Name = dto.Name,
            Type = dto.Type,
            Distance = dto.Distance,
            AthleteId = dto.AthleteId,
            Activities = dto.ActivityIds?.Select(id => new Activity { Id = id }).ToList() ?? new List<Activity>()
        };

        var created = await _repository.CreateAsync(equipment);

        return ToReadDto(created);
    }

    public async Task<bool> UpdateAsync(int id, EquipmentUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = dto.Name ?? existing.Name;
        existing.Type = dto.Type ?? existing.Type;
        existing.Distance = dto.Distance ?? existing.Distance;
        existing.AthleteId = dto.AthleteId ?? existing.AthleteId;
        if (dto.ActivityIds is not null)
        {
            existing.Activities = dto.ActivityIds.Select(id => new Activity { Id = id }).ToList();
        }

        await _repository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }

    private static EquipmentReadDto ToReadDto(Equipment e)
    {
        return new EquipmentReadDto
        {
            Id = e.Id,
            Name = e.Name,
            Type = e.Type,
            Distance = e.Distance,
            AthleteId = e.AthleteId,
            ActivityIds = e.Activities?.Select(e => e.Id).ToList(),
        };
    }
}
