using System.Collections.Generic;
using NLog;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class SettledBetsService
    {
        private readonly ILogger _logger;
        private readonly ISettledBetRepository _settledBetRepository;

        public SettledBetsService(ILogger logger, ISettledBetRepository settledBetRepository)
        {
            _logger = logger;
            _settledBetRepository = settledBetRepository;
        }

        public List<Bet> GetBetsForCustomer(int customerId)
        {
            _logger.Info(() => $"GetBetsForCustomer called for customer {customerId}");

            return _settledBetRepository.GetAllBetsForCustomer(customerId);
        } 
    }
}
