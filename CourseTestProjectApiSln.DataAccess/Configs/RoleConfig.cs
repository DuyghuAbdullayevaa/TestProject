using CourseTestProjectApiSln.DataAccess.Configs.Base;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;





namespace CourseTestProjectApiSln.DataAccess.Configs
{
    public class RoleConfig : IEntityConfig<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
   
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(new Role[]
            {
                new Role
                {
                    Id = (int)RoleEnum.Admin,
                    Name = RoleEnum.Admin.ToString()
                },
                new Role
                {
                    Id = (int)RoleEnum.Teacher,
                    Name = RoleEnum.Teacher.ToString()
                },
                new Role
                {
                    Id = (int)RoleEnum.Student,
                    Name = RoleEnum.Student.ToString()
                }
            });

            base.Configure(builder);
        }
    }
}
