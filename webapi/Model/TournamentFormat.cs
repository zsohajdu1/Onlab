﻿namespace webapi.Model
{
    public abstract class TournamentFormat
    {
        public int Id { get; set; }
        public Tournament Tournament { get; set; }
        public ICollection<Match>? Matches { get; set; }
    }
}
