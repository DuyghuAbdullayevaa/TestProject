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
    public class StudentConfig : IBaseEntityConfig<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
