using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animal;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpDelete]
    public IActionResult DeleteAnimal([FromQuery] int id)
    {
        var success = _animalService.DeleteAnimal(id);
        return success ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }

    [HttpPut]
    public IActionResult UpdateAnimal([FromQuery] int id, [FromBody] UpdateAnimalDTO dto)
    {
        var success = _animalService.UpdateAnimal(id, dto);
        return success ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }
}