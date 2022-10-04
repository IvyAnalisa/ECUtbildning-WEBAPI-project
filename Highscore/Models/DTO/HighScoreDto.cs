using Highscore.Models;

namespace Highscore
{
    internal class HighScoreDto
    {


        public int Score { get; set; }
        public string Player { get; set; }
        public DateTime Date { get; set; }
        public Game? Game { get; set; }
        public int GameId { get; set; }
    }
}