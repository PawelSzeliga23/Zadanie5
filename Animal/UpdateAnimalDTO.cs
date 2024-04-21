using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Animal;

public class UpdateAnimalDTO
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Area { get; set; }
}