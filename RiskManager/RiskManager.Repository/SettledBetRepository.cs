﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using RiskManager.Model;

namespace RiskManager.Repository
{
    public class SettledBetRepository : ISettledBetRepository
    {
        private readonly ILogger _logger;

        public SettledBetRepository(ILogger logger)
        {
            _logger = logger;
        }

        public List<Bet> GetAllBets()
        {
            _logger.Trace(() => "GetAllBets called");
            return ReadDataFile();
        }

        public List<Bet> GetAllBetsForCustomer(int customerId)
        {
            _logger.Trace(() => $"GetAllBets called for {customerId}");
            return ReadDataFile().Where(b => b.CustomerId == customerId).ToList();
        }

        private static List<Bet> ReadDataFile()
        {
            var asm = Assembly.GetExecutingAssembly();
            string path = new Uri(Path.GetDirectoryName(asm.CodeBase)).LocalPath;
            using (var reader = File.OpenText(Path.Combine(path, @"Data\Settled.csv")))
            {
                var csv = new CsvReader(reader, new CsvConfiguration() {Maps = {}});
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
