using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskManager.Repository.IntegrationTests
{
    [TestClass]
    public class SettledRepositoryFixture
    {
        [TestMethod]
        public void GetAll_Returns_AllBets()
        {
            var repository = new SettledBetRepository();

            var allBets = repository.GetAllBets();

            Assert.IsTrue(allBets.Count > 0);
        }
    }
}
