using IvyGame.Models.Domain;

namespace IvyGame.Models.DTO
{
    public class HighScoreDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public string Player { get; set; }
        public virtual Game? Game { get; set; }//Set ? back to app work
        public int GameId { get; set; }
    }
}
