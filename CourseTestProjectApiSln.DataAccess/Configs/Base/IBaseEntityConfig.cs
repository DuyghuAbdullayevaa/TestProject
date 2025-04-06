using CourseTestProjectApiSln.DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CourseTestProjectApiSln.DataAccess.Configs.Base
{
    public class IBaseEntityConfig<T> : IDeleteConfig<T> where T : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
