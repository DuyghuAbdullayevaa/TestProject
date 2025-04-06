using CourseTestProjectApiSln.DataAccess.Configs.Base;
using CourseTestProjectApiSln.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Configs
{
    public class TeacherConfig : IBaseEntityConfig<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {


            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);


            base.Configure(builder);
        }
    }
}
