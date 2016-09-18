using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class CustomerRiskService
    {
        private readonly ILogger _logger;
        private readonly ISettledBetRepository _settledBetRepository;

        public CustomerRiskService(ILogger logger, ISettledBetRepository settledBetRepository)
        {
            _logger = logger;
            _settledBetRepository = settledBetRepository;
        }

        public List<BettingProfile> FindHighRiskCustomers(uint successRate)
        {
            _logger.Trace(() => $"FindHighRiskCustomers called for {successRate}");

            if (successRate == 0 || successRate > 100)
                throw new ArgumentException("successRate must be a value between 1 and 100", nameof(successRate));

            var settledBets = _settledBetRepository.GetAllBets();
            var profiles = BettingAnalyser.ProfileCustomerBets(settledBets);
            return profiles.Where(p => p.SuccessRate >= successRate).ToList();
        }
    }
}
