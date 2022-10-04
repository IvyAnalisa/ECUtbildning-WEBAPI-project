using System.ComponentModel.DataAnnotations;

namespace IvyGame.Models.Domain
{
    public class Game
    {
        public int GameId { get; set; }

        [MaxLength(100)]
        public string GameName { get; set; }

        public string? Year { get; set; }
        public string? Description { get; set; }

        public string? Genre { get; set; }
        public Uri? ImageUrl { get; set; }
    }
}
