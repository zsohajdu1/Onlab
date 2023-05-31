using Microsoft.AspNetCore.Identity;

namespace webapi.Model
{
    public class User : IdentityUser
    {
        public ICollection<Team>? Teams { get; set; }
        public ICollection<Team>? OwnedTeams { get; set; }
        public ICollection<Tournament>? Tournaments { get; set; }
        public ICollection<MemberApplication>? MemberApplications { get; set; }
    }
}
