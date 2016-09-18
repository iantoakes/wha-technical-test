using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLog;
using RiskManager.DomainLogic;
using RiskManager.Service.Models;

namespace RiskManager.Service.Controllers
{
    public class HighRiskBetsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly BettingRiskService _bettingRiskService;

        public HighRiskBetsController(ILogger logger, BettingRiskService bettingRiskService)
        {
            _logger = logger;
            _bettingRiskService = bettingRiskService;
        }

        public IEnumerable<HighRiskBetReponse> Get(uint successRate = 60)
        {
            _logger.Trace(() => $"Get called for successRate {successRate}");

            var risks = _bettingRiskService.FindHighRiskBets(successRate);
            _logger.Trace(() => $"{risks.Count} risky bets found");

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