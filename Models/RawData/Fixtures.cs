using FootballOdds.Models.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FootballOdds.Models.ResourceModels;
using System.Data.SqlClient;

namespace FootballOdds.Models.RawData
{
    public class Fixtures
    {
        public const string sourcePath = "source";
        public readonly string[] teamTitles = { "HomeTeam", "AwayTeam" };
        public readonly string[] matchTitles = { "Date", "HomeTeam", "AwayTeam", "FTHG", "FTAG", "FTR" };

        private readonly IHostingEnvironment _env;

        private readonly FootballOddsContext _context;

        private readonly CsvReader csvReader;

        private List<string> teams;

        private List<Dictionary<string, string>> matches;

        public Fixtures(IHostingEnvironment env, FootballOddsContext context)
        {
            _env = env;
            _context = context;
            csvReader = new CsvReader(_env);
            teams = new List<string>();
            matches = new List<Dictionary<string, string>>();
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
            var matchIndexes = GetMatchTitleIndexes(titles);

            foreach (var row in data)
            {
                if (count > 0)
                {
                    GetTeamsFromRow(row, teamIndexes);
                }
                
                count++;
            }

            var mappedTeams = AddTeams();
            
            foreach (var row in data)
            {
                if (count > 0)
                {
                    GetMatchesFromRow(row, matchIndexes, mappedTeams);
                }
            }
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
            catch (SqlException e)
            {
                Console.WriteLine("Sql exception. " + e.Message);
            }
            
        }

        private Dictionary<string, int> AddTeams()
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

            }

            var mappedTeams = _context.Team
                .Select(team => new { team.Id, team.Name })
                .ToDictionary(team => team.Name, team => team.Id);
            return mappedTeams;
        }

        private void GetMatchesFromRow(string[] row, Dictionary<int, string> matchTitleIndexes, Dictionary<string, int> mappedTeams)
        {
            var count = row.Count();
            var match = new Dictionary<string, string>();
            for (var i = 0; i < count; i++)
            {
                if (matchTitleIndexes.ContainsKey(i))
                {
                    var column = row[i];
                    var columnTitle = matchTitleIndexes[i];

                    if (teamTitles.Contains(columnTitle))
                    {
                        column = mappedTeams[column].ToString();
                    }

                    match.Add(columnTitle, column);
                }
            }
            matches.Add(match);
        }

        private Dictionary<int, string> GetMatchTitleIndexes(string[] titles)
        {
            var indexes = new Dictionary<int, string>();
            foreach (var title in matchTitles)
            {
                try
                {
                    var index = Array.IndexOf(titles, title);
                    indexes.Add(index, title);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Indexes wont match. " + e.Message);
                }
            }

            return indexes;
        }
    }
}
