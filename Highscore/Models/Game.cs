using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highscore.Models
{
    class Game
    {
        internal static readonly string gameName;

        public int GameId { get; set; }

        [MaxLength(100)]
        public string GameName { get; set; }

        public string Year { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }
        public Uri ImageUrl { get; set; }
    }
}
