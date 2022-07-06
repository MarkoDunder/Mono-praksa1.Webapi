using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.Common
{
    public class Paging
    {
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }

        public Paging(int pageNumber, int resultsPerPage)
        {
            PageNumber = pageNumber;
            ResultsPerPage = resultsPerPage;
        }

        public Paging() { }
    }
}
