using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic.Tests
{
    [TestClass]
    public class BettingRiskServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindHighRiskCustomers_Throws_ArgumentException_On_ZeroSuccessRate()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            var unsettledBetRepository = new Mock<IUnsettledBetRepository>();

            var service = new BettingRiskService(settledBetRepository.Object, unsettledBetRepository.Object);

            service.FindHighRiskBets(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindHighRiskCustomers_Throws_ArgumentException_When_SuccessRate_Gt_100()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            var unsettledBetRepository = new Mock<IUnsettledBetRepository>();

            var service = new BettingRiskService(settledBetRepository.Object, unsettledBetRepository.Object);

            service.FindHighRiskBets(101);
        }

        [TestMethod]
        public void FindHighRiskBets_FindsBets_Where_CustomerHasHighSuccessRate()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            var unsettledBetRepository = new Mock<IUnsettledBetRepository>();

            settledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                });

            unsettledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10}
                });

            var service = new BettingRiskService(settledBetRepository.Object, unsettledBetRepository.Object);

            var result = service.FindHighRiskBets(60).First();
            Assert.IsTrue(result.RiskCategory.HasFlag(RiskCategory.UnusualWinRate));
        }

        [TestMethod]
        public void FindHighRiskBets_FindsBets_Where_StakeGreaterThan_TenTimesAverageBet()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            var unsettledBetRepository = new Mock<IUnsettledBetRepository>();

            settledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                });

            unsettledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 51, Prize = 10}
                });

            var service = new BettingRiskService(settledBetRepository.Object, unsettledBetRepository.Object);

            var result = service.FindHighRiskBets(60).First();
            Assert.IsTrue(result.RiskCategory.HasFlag(RiskCategory.TenTimesAverage));
        }
    }
}
