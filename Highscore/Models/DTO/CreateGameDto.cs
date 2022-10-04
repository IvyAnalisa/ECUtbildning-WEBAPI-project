using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highscore.Models.DTO
{
    internal class CreateGameDto
    {
        public int GameId { get; set; }

        public string GameName { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }
        public Uri ImageUrl { get; set; }
    }
}
