
using IvyGame.Models.Domain;

namespace IvyGame.Models.ViewModels
{
    public class GameViewModel
    {

        public string GameId { get; set; }
        public virtual Game Game { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }

        public string? Genre { get; set; }
        public Uri? ImageUrl { get; set; }

        //public virtual ICollection<Player> Players { get; set; }
        //Player
        public string Player { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
