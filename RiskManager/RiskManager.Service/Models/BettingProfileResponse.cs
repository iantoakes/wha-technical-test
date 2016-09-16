using RiskManager.Model;

namespace RiskManager.Service.Models
{
    public class BettingProfileResponse
    {
        public BettingProfile BettingProfile { get; set; }

        public Customer Customer { get; set; }
    }
}