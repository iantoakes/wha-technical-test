using System;
using System.Collections.Generic;
using System.Linq;
using RiskManager.Model;

namespace RiskManager.DomainLogic
{
    public static class BettingAnalyser
    {
        internal static RiskCategory GetRiskCategoryForBet(Bet bet, IEnumerable<BettingProfile> bettingProfiles, uint successRate)
        {
            var bettingProfilesDictionary = bettingProfiles.ToDictionary(cbp => cbp.CustomerId);

            var bettingProfile = bettingProfilesDictionary[bet.CustomerId];

            var category = bettingProfile.SuccessRate >= successRate ? RiskCategory.UnusualWinRate : RiskCategory.None;
            category |= bet.Stake > bettingProfile.AverageBet * 10 ? RiskCategory.TenTimesAverage : category;
            category |= bet.Stake > bettingProfile.AverageBet * 30 ? RiskCategory.ThirtyTimesAverage : category;
            category |= bet.Prize > 1000 ? RiskCategory.ThousandDollarWin : category;

            return category;
        }

        internal static List<BettingProfile> ProfileCustomerBets(List<Bet> bets)
        {
            var profiles = bets.GroupBy(b => b.CustomerId)
                .Select(g =>
                {
                    var successRate = (int)(g.Count(b => b.Prize > 0) / (float)g.Count() * 100);

                    return new BettingProfile
                    {
                        CustomerId = g.Key,
                        AverageBet = (int) Math.Round(g.Average(b => b.Stake)),
                        SuccessRate = successRate
                    };
                }).ToList();

            return profiles;
        }        
    }
}