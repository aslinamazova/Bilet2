using System.ComponentModel.DataAnnotations;

namespace Bizland.Models;

public class Team
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string Profession { get; set; }
    [Required]
    public bool IsDeleted { get; set; } = default;
    [Required]
    public string ImagePath { get; set; }
}
