using RiskManager.Model;

namespace RiskManager.Service.Models
{
    public class HighRiskBetReponse
    {
        public BetProfile BetProfile { get; set; }
        public Customer Customer { get; set; }
    }
}