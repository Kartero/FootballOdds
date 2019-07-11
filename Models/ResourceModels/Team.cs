using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballOdds.Models.ResourceModels
{
    public class Team
    {
        public int Id { get; set; }

        [StringLength(60), Required]
        public string Name { get; set; }
    }
}
