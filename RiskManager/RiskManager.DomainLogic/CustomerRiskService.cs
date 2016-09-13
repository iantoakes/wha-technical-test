using System;
using System.Collections.Generic;
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

            return new List<CustomerRisk>();
        } 
    }
}
