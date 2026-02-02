using Microsoft.AspNetCore.Identity;

namespace Persistence.Models
{
    public class AppUser: IdentityUser
    {
        string? Nombrecompleto { get; set; }
        string? Carrera { get; set;}
    }
    
}