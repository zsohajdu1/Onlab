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
        public string GameIcon { get; set; }
        public ICollection<string> MembersId { get; set; }
        public ICollection<string> MembersName { get; set; }
        public string CaptainId { get; set; }
        public ICollection<int> TournamentsId { get; set; }
        public ICollection<int> MatchesId { get; set; }
    }
}
