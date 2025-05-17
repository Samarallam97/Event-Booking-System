using System.ComponentModel.DataAnnotations;

namespace Website.API.DTOs.Identity;

public class AssignRoleDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string Role { get; set; }
}
