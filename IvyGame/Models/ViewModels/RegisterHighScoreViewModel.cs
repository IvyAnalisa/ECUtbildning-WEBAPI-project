using IvyGame.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace IvyGame.Models.ViewModels
{
    public class RegisterHighScoreViewModel
    {
        [Display(Name = "Score")]
        public int Score { get; set; }
        //public virtual Game game { get; set; }
        public List<SelectListItem>? GameItems { get; set; }

        [Display(Name = "Datum")]

        public DateTime Date { get; set; }

        [Display(Name = " Player Name")]
        public string Player { get; set; }


    }
}
