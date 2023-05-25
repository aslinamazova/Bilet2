using System.ComponentModel.DataAnnotations;

namespace Bizland.Areas.Admin.ViewModels;

public class CreateTeamVM
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    public string Profession { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}      
