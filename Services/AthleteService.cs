public class AthleteService
{
    private readonly IRepository<Athlete> _repository;

    public AthleteService(IRepository<Athlete> repository)
    {
        _repository = repository;
    }

    public async Task<AthleteReadDto?> GetByIdAsync(int id)
    {
        var athlete = await _repository.GetByIdAsync(id);
        return athlete is null ? null : ToReadDto(athlete);
    }

    public async Task<List<AthleteReadDto>> GetAllAsync()
    {
        var athletes = await _repository.GetAllAsync();
        return athletes.Select(a => ToReadDto(a)).ToList();
    }

    public async Task<AthleteReadDto> CreateAsync(AthleteCreateDto dto)
    {
        var athlete = new Athlete
        {
            Name = dto.Name,
            Age = dto.Age,
            Weight = dto.Weight,
            Height = dto.Height,
            Equipments = dto.EquipmentIds?.Select(id => new Equipment { Id = id }).ToList(),
            Activities = dto.ActivityIds?.Select(id => new Activity { Id = id }).ToList(),
            Workouts = dto.WorkoutIds?.Select(id => new Workout { Id = id }).ToList(),
        };

        var created = await _repository.CreateAsync(athlete);
        return ToReadDto(created);
    }

    public async Task<bool> UpdateAsync(int id, AthleteUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = dto.Name ?? existing.Name;
        existing.Age = dto.Age ?? existing.Age;
        existing.Weight = dto.Weight ?? existing.Weight;
        existing.Height = dto.Height ?? existing.Height;
        existing.Equipments = dto.EquipmentIds?.Select(id => new Equipment { Id = id }).ToList();
        existing.Activities = dto.ActivityIds?.Select(id => new Activity { Id = id }).ToList();
        existing.Workouts = dto.WorkoutIds?.Select(id => new Workout { Id = id }).ToList();

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

    private static AthleteReadDto ToReadDto(Athlete a)
    {
        return new AthleteReadDto
        {
            Id = a.Id,
            Name = a.Name,
            Age = a.Age,
            Weight = a.Weight,
            Height = a.Height,
            EquipmentIds = a.Equipments?.Select(e => e.Id).ToList(),
            ActivityIds = a.Activities?.Select(e => e.Id).ToList(),
            WorkoutIds = a.Workouts?.Select(e => e.Id).ToList(),
        };
    }
}