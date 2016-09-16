using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskManager.Repository.IntegrationTests
{
    [TestClass]
    public class UsettledRepositoryFixture
    {
        [TestMethod]
        public void GetAll_Returns_AllBets()
        {
            var repository = new UnsettledBetRepository();

            var allBets = repository.GetAllBets();

            Assert.IsTrue(allBets.Count > 0);
        }
    }
}
