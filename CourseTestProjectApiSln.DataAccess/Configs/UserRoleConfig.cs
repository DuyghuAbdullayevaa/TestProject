using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CourseTestProjectApiSln.DataAccess.Configs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Define composite primary key
            builder.HasKey(x => new { x.UserID, x.RoleID });

            // Define the relationship between UserRole and User
            builder.HasOne(x => x.User)  // UserRole has one User
                .WithMany(x => x.UserRoles)  // A User can have many UserRoles
                .HasForeignKey(x => x.UserID) // Define the foreign key in UserRole
                .OnDelete(DeleteBehavior.NoAction);  // Define delete behavior (cascade delete)

            // Define the relationship between UserRole and Role
            builder.HasOne(x => x.Role)  // UserRole has one Role
                .WithMany(x => x.UserRoles)  // A Role can have many UserRoles
                .HasForeignKey(x => x.RoleID) // Define the foreign key in UserRole
                .OnDelete(DeleteBehavior.NoAction);  // Define delete behavior (cascade delete)

            builder.HasData(
              new UserRole { UserID = 1, RoleID = (int)RoleEnum.Admin }
             
          );

            // Optionally, you can add additional constraints if needed
        }
    }
}
