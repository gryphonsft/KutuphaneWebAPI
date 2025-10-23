using Microsoft.AspNetCore.Identity;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.User
{
    public sealed class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            Id = Guid.CreateVersion7();
        }

        public string FullName { get; set; } = string.Empty;

        public ICollection<Borrow> Borrows { get; set; }
    }
}
