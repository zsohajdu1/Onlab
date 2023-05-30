using webapi.Model;

namespace webapi.DTO
{
    public class TeamDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeamStatus Status { get; set; }
        public string TeamGame { get; set; }
        public string? LftDescription { get; set; }
        public ICollection<String> MembersId { get; set; }
        //public String CaptainId { get; set; }
        public ICollection<int> TournamentsId { get; set; }
        public ICollection<int> MatchesId { get; set; }
    }
}
