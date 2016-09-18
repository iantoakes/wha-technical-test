using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;

namespace RiskManager.Repository.IntegrationTests
{
    [TestClass]
    public class UsettledRepositoryFixture
    {
        [TestMethod]
        public void GetAll_Returns_AllBets()
        {
            var repository = new UnsettledBetRepository(new Mock<ILogger>().Object);

            var allBets = repository.GetAllBets();

            Assert.IsTrue(allBets.Count > 0);
        }
    }
}
