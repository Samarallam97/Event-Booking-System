using System.ComponentModel.DataAnnotations;

namespace Website.API.DTOs.Identity;

public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; }
}
