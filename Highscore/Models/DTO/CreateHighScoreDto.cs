using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highscore.Models.DTO
{
    internal class CreateHighScoreDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public string Player { get; set; }
        public virtual Game? Game { get; set; }//Set ? back to app work
        public int GameId { get; set; }
        public string? GameName { get; internal set; }
        //public string GameName { get; set; }

    }
}
