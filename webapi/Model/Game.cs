namespace webapi.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public String? Icon { get; set; }
        public ICollection<Team>? Teams { get; set; }
        public ICollection<Tournament>? Tournaments { get; set; }
    }
}
