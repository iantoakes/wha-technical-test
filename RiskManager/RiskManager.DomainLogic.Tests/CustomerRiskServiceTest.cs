using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var repository = new Mock<ISettledBetRepository>();
            var service = new CustomerRiskService(repository.Object);

            service.FindHighRiskCustomers(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindHighRiskCustomers_Throws_ArgumentException_When_SuccessRate_Gt_100()
        {
            var repository = new Mock<ISettledBetRepository>();
            var service = new CustomerRiskService(repository.Object);

            service.FindHighRiskCustomers(101);
        }
    }
}
