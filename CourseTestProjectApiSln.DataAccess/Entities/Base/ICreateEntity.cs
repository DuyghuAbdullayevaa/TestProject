using CourseTestProjectApiSln.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Entities.Base
{
    public interface ICreateEntity : IEntity
    {
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
