using IvyGame.Models.Domain;


namespace IvyGame.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public Game Game { get; set; }
        public HighScore HighScore { get; set; }
        public IEnumerable<HighScore> DisplayOnHomePage { get; set; } = new List<HighScore>();
        //  public IEnumerable<HighScore> SortByScore { get; set; } = new List<HighScore>();

    }
}
