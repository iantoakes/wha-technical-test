namespace RiskManager.Model
{
    public class BetProfile
    {
        public BetProfile(Bet bet, RiskCategory riskCategory, BettingProfile bettingProfile)
        {
            Bet = bet;
            RiskCategory = riskCategory;
            BettingProfile = bettingProfile;
        }

        public Bet Bet { get; set; }
        public RiskCategory RiskCategory { get; set; }
        public BettingProfile BettingProfile { get; set; }
    }
}
