using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Entities.Base
{
    public abstract class BaseEntity : IDeleteEntity
    {
        public int Id { get; init; }

        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int? DeleteUserId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
