using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RiskManager.DomainLogic;
using RiskManager.Model;
using RiskManager.Service.Models;

namespace RiskManager.Service.Controllers
{
    public class CustomerRiskController : ApiController
    {
        private readonly CustomerRiskService _customerRiskService;

        public CustomerRiskController(CustomerRiskService customerRiskService)
        {
            _customerRiskService = customerRiskService;
        }

        public IEnumerable<CustomerRiskResponse> Get(uint successRate)
        {
            return _customerRiskService.FindHighRiskCustomers(successRate)
                .Select(cr => new CustomerRiskResponse
                {
                    SuccessRate = cr.SuccessRate,
                    Customer = new Customer
                    {
                        CustomerId = cr.CustomerId,
                        Uri = Url.Link("DefaultApi", new {Controller = "settledbet", customerId = cr.CustomerId})
                    }
                }).ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}