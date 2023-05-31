using webapi.Model;

namespace webapi.DTO
{
    public class TeamQueryDTO
    {
        public string? Name { get; set; }
        public TeamStatus? Status { get; set; }
        public int? GameId { get; set; }
    }
}