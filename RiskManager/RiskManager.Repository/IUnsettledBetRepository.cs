using System.Collections.Generic;
using RiskManager.Model;

namespace RiskManager.Repository
{
    public interface IUnsettledBetRepository
    {
        List<Bet> GetAllBets();
    }
}