using System.Collections.Generic;
using RiskManager.Model;

namespace RiskManager.Repository
{
    public interface ISettledBetRepository
    {
        List<Bet> GetAllBets();
    }
}
