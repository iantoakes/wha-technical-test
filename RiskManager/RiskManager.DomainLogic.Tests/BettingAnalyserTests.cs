using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskManager.Model;

namespace RiskManager.DomainLogic.Tests
{
    [TestClass]
    public class BettingAnalyserTests
    {
        [TestMethod]
        public void ProfileCustomerBets_Returns_ExpectedResult()
        {
            var bets = new List<Bet>
            {
                new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                new Bet {CustomerId = 1, Stake = 5, Prize = 0},
            };

            var profile = BettingAnalyser.ProfileCustomerBets(bets).First();

            Assert.AreEqual(1, profile.CustomerId);
            Assert.AreEqual(5, profile.AverageBet);
            Assert.AreEqual(66, profile.SuccessRate);
        }

        [TestMethod]
        public void GetRiskCategoryForBet_Identifies_HighSuccessRate()
        {
            var bets = new List<Bet>
            {
                new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                new Bet {CustomerId = 1, Stake = 5, Prize = 0},
            };

            var profiles = BettingAnalyser.ProfileCustomerBets(bets);
            var riskCategory = BettingAnalyser.GetRiskCategoryForBet(new Bet { CustomerId = 1, Stake = 5, Prize = 10 }, profiles, 60);

            Assert.IsTrue(riskCategory.HasFlag(RiskCategory.UnusualWinRate));
        }

        [TestMethod]
        public void GetRiskCategoryForBet_Identifies_StakeGreaterThan_TenTimesAverageBet()
        {
            var bets = new List<Bet>
            {
                new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                new Bet {CustomerId = 1, Stake = 5, Prize = 0},
            };

            var profiles = BettingAnalyser.ProfileCustomerBets(bets);
            var riskCategory = BettingAnalyser.GetRiskCategoryForBet(new Bet { CustomerId = 1, Stake = 51, Prize = 10 }, profiles, 60);

            Assert.IsTrue(riskCategory.HasFlag(RiskCategory.TenTimesAverage));
        }
    }
}
