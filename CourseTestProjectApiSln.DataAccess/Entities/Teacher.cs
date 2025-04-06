﻿using CourseTestProjectApiSln.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Entities
{

    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

    }
}
