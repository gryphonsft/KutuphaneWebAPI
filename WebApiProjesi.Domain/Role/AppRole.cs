using Microsoft.AspNetCore.Identity;
namespace WebApiProjesi.Domain.Role
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}
