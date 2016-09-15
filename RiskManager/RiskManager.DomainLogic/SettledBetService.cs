using System.Collections.Generic;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class SettledBetService
    {
        private readonly ISettledBetRepository _settledBetRepository;

        public SettledBetService(ISettledBetRepository settledBetRepository)
        {
            _settledBetRepository = settledBetRepository;
        }

        public List<Bet> GetBetsForCustomer(int customerId)
        {
            return _settledBetRepository.GetAllBetsForCustomer(customerId);
        } 
    }
}
