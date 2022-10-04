using IvyGame.Models.Domain;

namespace IvyGame.Models.DTO
{
    public class CreateHighScoreDto
    {
        public Game Game { get; set; }
        public HighScore HighScore { get; set; }
        public IEnumerable<HighScore> HighScoresDto { get; set; } = new List<HighScore>();
    }
}
