namespace RiskManager.Service.Models
{
    public class CustomerRiskResponse
    {
        public Customer Customer { get; set; }
        public int SuccessRate { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Uri { get; set; }
    }
}