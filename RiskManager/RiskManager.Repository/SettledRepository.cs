using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using RiskManager.Model;

namespace RiskManager.Repository
{
    public class SettledRepository
    {
        public List<Bet> GetAllBets()
        {
            var asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.FullName);
            using (var x = File.OpenText(Path.Combine(path, @"Data\Settled.csv")))
            {
                var csv = new CsvReader(x, new CsvConfiguration() {Maps = { }});
                csv.Configuration.RegisterClassMap<BetCsvClassMap>();
                return csv.GetRecords<Bet>().ToList();
            }
        }

        public class BetCsvClassMap : CsvClassMap<Bet>
        {
            public BetCsvClassMap()
            {
                Map(m => m.CustomerId).Name("Customer");
                Map(m => m.EventId).Name("Event");
                Map(m => m.ParticipantId).Name("Participant");
                Map(m => m.Stake);
                Map(m => m.Prize).Name("Win");
            }
        }
    }
}
