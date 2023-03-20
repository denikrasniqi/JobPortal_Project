using Microsoft.AspNetCore.Identity;

namespace Job_Portal.Data.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Address { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public string? Experience { get; set; } = null!;
    public string? Education { get; set; } = null!;
    public string? Skills { get; set; } = null!;
    public string? Projects { get; set; } = null!;
    public string? Facebook { get; set; } = null!;
    public string? Twitter { get; set; } = null!;
    public string? Linkedin { get; set; } = null!;
    public string? GitHub { get; set; } = null!;
    public DateTime? Birthday { get; set; } = null!;

}

