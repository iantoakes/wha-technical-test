using System;
using System.Collections.Generic;
using System.Linq;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class BettingRiskService
    {
        private readonly ISettledBetRepository _settledBetRepository;
        private readonly IUnsettledBetRepository _unsettledBetRepository;

        public BettingRiskService(ISettledBetRepository settledBetRepository, IUnsettledBetRepository unsettledBetRepository)
        {
            _settledBetRepository = settledBetRepository;
            _unsettledBetRepository = unsettledBetRepository;
        }

        public List<BetProfile> FindHighRiskBets(uint successRate)
        {
            if (successRate == 0 || successRate > 100)
                throw new ArgumentException("successRate must be a value between 1 and 100", nameof(successRate));

            var unsettledBets = _unsettledBetRepository.GetAllBets();
            var settledBets = _settledBetRepository.GetAllBets();

            var profiles = BettingAnalyser.ProfileCustomerBets(settledBets);

            var highRiskBets = unsettledBets
                .Where(b => BettingAnalyser.GetRiskCategoryForBet(b, profiles, successRate) != RiskCategory.None)
                .Select(b =>
                {
                    var riskCategoryForBet = BettingAnalyser.GetRiskCategoryForBet(b, profiles, successRate);
                    var bettingProfile = profiles.First(bp => bp.CustomerId == b.CustomerId);
                    return new BetProfile(b, riskCategoryForBet, bettingProfile);
                })
                .ToList();

            return highRiskBets;
        }
    }
}
