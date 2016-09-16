using System.Collections.Generic;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class SettledBetsService
    {
        private readonly ISettledBetRepository _settledBetRepository;

        public SettledBetsService(ISettledBetRepository settledBetRepository)
        {
            _settledBetRepository = settledBetRepository;
        }

        public List<Bet> GetBetsForCustomer(int customerId)
        {
            return _settledBetRepository.GetAllBetsForCustomer(customerId);
        } 
    }
}
