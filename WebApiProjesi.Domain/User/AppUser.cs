using Microsoft.AspNetCore.Identity;

namespace WebApiProjesi.Domain.User
{
    public sealed class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            Id = Guid.CreateVersion7();
        }

        public string FullName { get; set; } = string.Empty;
    }
}
