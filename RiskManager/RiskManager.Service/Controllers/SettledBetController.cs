using System.Collections.Generic;
using System.Web.Http;
using RiskManager.DomainLogic;
using RiskManager.Model;

namespace RiskManager.Service.Controllers
{
    public class SettledBetController : ApiController
    {
        private readonly SettledBetService _settledBetService;

        public SettledBetController(SettledBetService settledBetService)
        {
            _settledBetService = settledBetService;
        }

        public List<Bet> Get(int customerId)
        {
            return _settledBetService.GetBetsForCustomer(customerId);
        }
    }
}