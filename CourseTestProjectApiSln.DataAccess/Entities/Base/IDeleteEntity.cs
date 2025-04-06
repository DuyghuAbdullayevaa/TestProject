using CourseTestProjectApiSln.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Entities.Base
{
    public interface IDeleteEntity : IUpdateEntity
    {
        public int? DeleteUserId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
