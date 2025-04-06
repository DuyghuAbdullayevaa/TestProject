using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Entities.Base
{
    public interface IUpdateEntity : ICreateEntity
    {
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
