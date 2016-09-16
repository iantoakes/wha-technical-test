using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic.Tests
{
    [TestClass]
    public class CustomerRiskServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindHighRiskCustomers_Throws_ArgumentException_On_ZeroSuccessRate()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();

            var service = new CustomerRiskService(settledBetRepository.Object);

            service.FindHighRiskCustomers(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindHighRiskCustomers_Throws_ArgumentException_When_SuccessRate_Gt_100()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            var service = new CustomerRiskService(settledBetRepository.Object);

            service.FindHighRiskCustomers(101);
        }

        [TestMethod]
        public void FindHighRiskCustomer_Finds_CustomersOverSuccessRate()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            settledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                });

            var service = new CustomerRiskService(settledBetRepository.Object);
            var result = service.FindHighRiskCustomers(60);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void FindHighRiskCustomer_DoesntFind_CustomersUnderSuccessRate()
        {
            var settledBetRepository = new Mock<ISettledBetRepository>();
            settledBetRepository.Setup(m => m.GetAllBets())
                .Returns(new List<Bet>
                {
                    new Bet {CustomerId = 1, Stake = 5, Prize = 10},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                    new Bet {CustomerId = 1, Stake = 5, Prize = 0},
                });

            var service = new CustomerRiskService(settledBetRepository.Object);
            var result = service.FindHighRiskCustomers(60);
            Assert.AreEqual(0, result.Count);
        }
    }
}
