using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.DataAccess.Models
{
    public class PaginationResponse<T>
    {
        public int TotalCount { get; init; }
        public IEnumerable<T> Data { get; init; }
        public PaginationResponse(int totalCount, IEnumerable<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}
