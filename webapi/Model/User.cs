using Microsoft.AspNetCore.Identity;

namespace webapi.Model
{
    public class User : IdentityUser
    {
        public ICollection<Team>? Teams { get; set; }
    }
}
