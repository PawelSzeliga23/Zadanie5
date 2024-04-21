namespace WebApplication1.Animal;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int id, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int id);
}