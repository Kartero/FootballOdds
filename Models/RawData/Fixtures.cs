using FootballOdds.Models.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FootballOdds.Models.ResourceModels;

namespace FootballOdds.Models.RawData
{
    public class Fixtures
    {
        public const string sourcePath = "source";
        public readonly string[] teamTitles = { "HomeTeam", "AwayTeam" };

        private readonly IHostingEnvironment _env;

        private readonly FootballOddsContext _context;

        private readonly CsvReader csvReader;

        private List<string> teams;

        public Fixtures(IHostingEnvironment env, FootballOddsContext context)
        {
            _env = env;
            _context = context;
            csvReader = new CsvReader(_env);
            teams = new List<string>();
        }

        public string[] ReadAll()
        {
            var files = GetFiles();
            foreach (var file in files)
            {
                var data = csvReader.Read(file);
                if (data.Count > 0)
                {
                    FetchData(data);
                }
            }

            return teams.ToArray();
        }

        private string[] GetFiles()
        {
            return csvReader.GetFiles(sourcePath);
        }

        private string[] GetTitles(List<string[]> data)
        {
            return data[0];
        }

        private void FetchData(List<string[]> data)
        {
            int count = 0;
            var titles = GetTitles(data);
            var teamIndexes = TeamIndexes(titles);
            
            foreach (var row in data)
            {
                if (count > 0)
                {
                    GetTeamsFromRow(row, teamIndexes);
                }
                
                count++;
            }

            AddTeams();
        }

        private int[] TeamIndexes(string[] titles)
        {
            var index1 = Array.IndexOf(titles, teamTitles[0]);
            var index2 = Array.IndexOf(titles, teamTitles[1]);
            return new int[2]{ index1, index2 };
        }

        private void GetTeamsFromRow(string[] row, int[] teamIndexes)
        {
            try
            {
                var team1 = row[teamIndexes[0]];
                var team2 = row[teamIndexes[1]];

                if (!teams.Contains(team1))
                {
                    teams.Add(team1);
                }
                if (!teams.Contains(team2))
                {
                    teams.Add(team2);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Something wrong with a row or team indexes. " + e.Message);
            }
            
        }

        private bool AddTeams()
        {
            var existingTeams = _context.Team
                .Select(team => team.Name)
                .ToList();

            teams = teams.Except(existingTeams).ToList();

            if (teams.Count > 0)
            {
                foreach (var team in teams)
                {
                    _context.Add(new Team
                    {
                        Name = team
                    });
                }
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
