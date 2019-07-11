using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballOdds.Models.Helpers
{
    public class CsvReader
    {
        private readonly IHostingEnvironment _env;

        public CsvReader(IHostingEnvironment env)
        {
            _env = env;
        }

        public string[] GetFiles(string path)
        {
            string contentRootPath = _env.ContentRootPath;
            string[] paths = { contentRootPath, path };
            string directory = Path.Combine(paths);
            string[] files = Directory.GetFiles(directory, "*.csv");

            return files;
        }

        public List<string[]> Read(string file)
        {
            List<string[]> data = new List<string[]>();
            using (var reader = new StreamReader(@file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    data.Add(values);
                }
            }

            return data;
        }
    }
}
