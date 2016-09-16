using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RiskManager.DomainLogic;
using RiskManager.Service.Models;

namespace RiskManager.Service.Controllers
{
    public class HighRiskBetsController : ApiController
    {
        private readonly BettingRiskService _bettingRiskService;

        public HighRiskBetsController(BettingRiskService bettingRiskService)
        {
            _bettingRiskService = bettingRiskService;
        }

        public IEnumerable<HighRiskBetReponse> Get(uint successRate = 60)
        {
            var risks = _bettingRiskService.FindHighRiskBets(successRate);
            return risks.Select(r => new HighRiskBetReponse
            {
                BetProfile = r,
                Customer = new Customer
                {
                    CustomerId = r.Bet.CustomerId,
                    Uri = Url.Link("DefaultApi", new {Controller = "unsettledbet", customerId = r.Bet.CustomerId })
                }
            }).ToList();
        }
    }
}