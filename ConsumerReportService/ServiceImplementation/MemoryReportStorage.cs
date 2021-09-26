﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerReportService.ServiceImplementation
{
    public class MemoryReportStorage:IMemoryReportStorage
    {
        private readonly IList<Report> reports = new List<Report>();

        public void Add(Report report)
        {
            reports.Add(report);
        }

        public IEnumerable<Report> Get()
        {
            return reports;
        }
    }
}
