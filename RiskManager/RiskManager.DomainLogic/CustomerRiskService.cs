using System;
using System.Collections.Generic;
using System.Linq;
using RiskManager.Model;
using RiskManager.Repository;

namespace RiskManager.DomainLogic
{
    public class CustomerRiskService
    {
        private readonly ISettledBetRepository _settledBetRepository;

        public CustomerRiskService(ISettledBetRepository settledBetRepository)
        {
            _settledBetRepository = settledBetRepository;
        }

        public List<CustomerRisk> FindHighRiskCustomers(uint successRate)
        {
            if(successRate == 0 || successRate > 100)
                throw new ArgumentException("successRate must be a value between 1 and 100", nameof(successRate));

            var allBets = _settledBetRepository.GetAllBets();
            var customerRisks = allBets.GroupBy(b => b.CustomerId)
                .Select(g =>
                    new CustomerRisk
                    {
                        CustomerId = g.Key,
                        SuccessRate = (int) (g.Count(b => b.Prize > 0)/(float) g.Count()*100)
                    });
            return customerRisks.Where(cr => cr.SuccessRate >= successRate).ToList();
        } 
    }
}
