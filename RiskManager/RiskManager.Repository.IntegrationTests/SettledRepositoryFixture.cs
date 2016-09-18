using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;

namespace RiskManager.Repository.IntegrationTests
{
    [TestClass]
    public class SettledRepositoryFixture
    {
        [TestMethod]
        public void GetAll_Returns_AllBets()
        {
            var repository = new SettledBetRepository(new Mock<ILogger>().Object);

            var allBets = repository.GetAllBets();

            Assert.IsTrue(allBets.Count > 0);
        }
    }
}
