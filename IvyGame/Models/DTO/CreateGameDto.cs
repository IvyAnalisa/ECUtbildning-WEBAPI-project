namespace IvyGame.Models.DTO
{
    public class CreateGameDto
    {
        public int GameId { get; set; }

        public string GameName { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }
        public Uri ImageUrl { get; set; }
    }
}
