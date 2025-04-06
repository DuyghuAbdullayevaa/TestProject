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
    public class ICreateConfig<T> : IEntityConfig<T> where T : class, ICreateEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreateDate).IsRequired();
        }
    }
}
