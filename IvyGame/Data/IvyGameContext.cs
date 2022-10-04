

using IvyGame.Models.Domain;
using Microsoft.EntityFrameworkCore;
using IvyGame.Models.DTO;

namespace IvyGame.Data
{
    public class IvyGameContext : DbContext
    {
        public DbSet<HighScore> HighScore { get; set; }
        public DbSet<Game> Game { get; set; }
        public IvyGameContext(DbContextOptions<IvyGameContext> options)

            : base(options) { }
        public DbSet<IvyGame.Models.DTO.HighScoreDto>? HighScoreDto { get; set; }
    }
}
