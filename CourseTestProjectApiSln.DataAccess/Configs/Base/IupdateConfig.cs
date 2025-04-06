using CourseTestProjectApiSln.DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.DataAccess.Configs.Base
{
    public class IUpdateConfig<T> : ICreateConfig<T> where T : class, IUpdateEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.UpdateDate).IsRequired(false);
        }
    }
}
