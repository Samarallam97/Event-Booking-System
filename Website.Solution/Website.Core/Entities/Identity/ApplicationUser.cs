using Microsoft.AspNetCore.Identity;
namespace Website.Core.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
	public string FullNameAR { get; set; }

}
