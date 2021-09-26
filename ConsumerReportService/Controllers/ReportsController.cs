using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQManages.Subscriber;
using ConsumerReportService.ServiceImplementation;

namespace ConsumerReportService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMemoryReportStorage _memoryReportStorage;

        public ReportsController(IMemoryReportStorage memoryReportStorage)
        {
            _memoryReportStorage = memoryReportStorage;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public IEnumerable<Report> Get()
        {
            return _memoryReportStorage.Get();
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
