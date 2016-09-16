using System.Collections.Generic;
using System.Web.Http;
using RiskManager.DomainLogic;
using RiskManager.Model;

namespace RiskManager.Service.Controllers
{
    public class SettledBetsController : ApiController
    {
        private readonly SettledBetsService _settledBetsService;

        public SettledBetsController(SettledBetsService settledBetsService)
        {
            _settledBetsService = settledBetsService;
        }

        public List<Bet> Get(int customerId)
        {
            return _settledBetsService.GetBetsForCustomer(customerId);
        }
    }
}