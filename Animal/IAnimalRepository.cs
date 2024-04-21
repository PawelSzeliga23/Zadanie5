namespace WebApplication1.Animal;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int id, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int id);
}