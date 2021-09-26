using System.Collections.Generic;

namespace ConsumerReportService.ServiceImplementation
{
    public interface IMemoryReportStorage
    {
        void Add(Report report);
        IEnumerable<Report> Get();
    }
}