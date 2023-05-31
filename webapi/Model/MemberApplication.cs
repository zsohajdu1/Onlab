namespace webapi.Model
{
    public class MemberApplication
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Team Team { get; set; }
        public bool IsCompleted { get; set; }
    }
}
