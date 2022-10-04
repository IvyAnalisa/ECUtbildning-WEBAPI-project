using IvyGame.Models.Domain;

namespace IvyGame.Models.Domain
{
    public class GameDetailsViewModel
    {
        //public HighScore HighScore { get; set; }
        public Game Game { get; set; }
        public IEnumerable<Game> GameDetails { get; set; } = new List<Game>();
        public IEnumerable<HighScore> SortByScore { get; set; } = new List<HighScore>();
        // public IEnumerable<HighScore> TopScore { get; set; } = new List<HighScore>();
    }
}
