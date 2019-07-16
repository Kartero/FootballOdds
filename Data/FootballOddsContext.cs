using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FootballOdds.Models.ResourceModels;

namespace FootballOdds.Models
{
    public class FootballOddsContext : DbContext
    {
        public FootballOddsContext (DbContextOptions<FootballOddsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasIndex(team => team.Name)
                .IsUnique();

            //modelBuilder.Entity<Match>()
            //    .HasIndex(m => new
            //    {
            //        m.Hometeam,
            //        m.Awayteam,
            //        m.MatchDay
            //    });
        }

        public DbSet<FootballOdds.Models.ResourceModels.Team> Team { get; set; }
        public DbSet<FootballOdds.Models.ResourceModels.Match> Match { get; set; }
    }
}
