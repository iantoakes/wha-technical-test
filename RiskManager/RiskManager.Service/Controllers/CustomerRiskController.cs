using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RiskManager.DomainLogic;
using RiskManager.Service.Models;

namespace RiskManager.Service.Controllers
{
    public class CustomerRiskController : ApiController
    {
        private readonly CustomerRiskService _customerRiskService;

        public CustomerRiskController(CustomerRiskService customerRiskService)
        {
            _customerRiskService = customerRiskService;
        }

        public IEnumerable<BettingProfileResponse> Get(uint successRate = 60)
        {
            return _customerRiskService.FindHighRiskCustomers(successRate)
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