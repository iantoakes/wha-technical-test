using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLog;
using RiskManager.DomainLogic;
using RiskManager.Service.Models;

namespace RiskManager.Service.Controllers
{
    public class CustomerRiskController : ApiController
    {
        private readonly ILogger _logger;
        private readonly CustomerRiskService _customerRiskService;

        public CustomerRiskController(ILogger logger, CustomerRiskService customerRiskService)
        {
            _logger = logger;
            _customerRiskService = customerRiskService;
        }

        public IEnumerable<BettingProfileResponse> Get(uint successRate = 60)
        {
            _logger.Trace(() => $"Get called for successRate {successRate}");

            var highRiskCustomers = _customerRiskService.FindHighRiskCustomers(successRate);
            _logger.Trace(() => $"{highRiskCustomers.Count} risky customers found");

            return highRiskCustomers
                .Select(cr => new BettingProfileResponse
                {
                    BettingProfile = cr,
                    Customer = new Customer
                    {
                        CustomerId = cr.CustomerId,
                        Uri = Url.Link("DefaultApi", new {Controller = "settledbet", customerId = cr.CustomerId})
                    }
                }).ToList();
        }
    }
}