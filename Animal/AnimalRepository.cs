using System.Data.SqlClient;

namespace WebApplication1.Animal;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] { "Name", "Description", "Category", "Area" }.Contains(orderBy)
            ? orderBy
            : "Name";
        using var command =
            new SqlCommand($"SELECT IdAnimal, Name, Description, Category, Area FROM ANIMAL ORDER BY {safeOrderBy}",
                connection);
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                Id = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!
            };
            animals.Add(animal);
        }

        return animals;
    }

    public bool CreateAnimal(CreateAnimalDTO dto)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command =
            new SqlCommand(
                "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)",
                connection);
        command.Parameters.AddWithValue("@Name", dto.Name);
        if (string.IsNullOrEmpty(dto.Description))
        {
            command.Parameters.AddWithValue("@Description", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Description", dto.Description);
        }
        command.Parameters.AddWithValue("@Category", dto.Category);
        command.Parameters.AddWithValue("@Area", dto.Area);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool UpdateAnimal(int id, UpdateAnimalDTO dto)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var command =
            new SqlCommand(
                "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category,Area = @Area WHERE IdAnimal = @IdAnimal",
                connection);
        command.Parameters.AddWithValue("@Name", dto.Name);
        if (string.IsNullOrEmpty(dto.Description))
        {
            command.Parameters.AddWithValue("@Description", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("@Description", dto.Description);
        }
        command.Parameters.AddWithValue("@Category", dto.Category);
        command.Parameters.AddWithValue("@Area", dto.Area);
        command.Parameters.AddWithValue("@IdAnimal", id);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var command = new SqlCommand($"DELETE FROM ANIMAL WHERE IdAnimal = @ID",connection);
        command.Parameters.AddWithValue("@ID", id);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
}