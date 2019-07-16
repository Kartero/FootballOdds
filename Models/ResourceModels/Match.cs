using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballOdds.Models.ResourceModels
{
    public class Match
    {
        public int Id { get; set; }
        public int? Hometeam { get; set; }
        public int? Awayteam { get; set; }
        [Required]
        public int Homegoals { get; set; }
        [Required]
        public int Awaygoals { get; set; }
        [StringLength(5), Required]
        public string Result { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime MatchDay { get; set; }

        [ForeignKey("Hometeam")]
        public Team TeamHome { get; set; }
        [ForeignKey("Awayteam")]
        public Team TeamAway { get; set; }
    }
}
