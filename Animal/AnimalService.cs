namespace WebApplication1.Animal;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Description))
        {
            dto.Description = null;
        }

        return _animalRepository.CreateAnimal(dto);
    }

    public bool UpdateAnimal(int id, UpdateAnimalDTO dto)
    {
        return _animalRepository.UpdateAnimal(id, dto);
    }

    public bool DeleteAnimal(int id)
    {
        return _animalRepository.DeleteAnimal(id);
    }
}