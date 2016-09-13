namespace RiskManager.Model
{
    public class Bet
    {
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public int ParticipantId  { get; set; }
        public int Stake { get; set; }
        public int Prize { get; set; }
    }
}
