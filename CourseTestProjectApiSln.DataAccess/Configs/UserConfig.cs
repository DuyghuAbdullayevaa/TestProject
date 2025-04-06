using CourseTestProjectApiSln.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;



namespace CourseTestProjectApiSln.DataAccess.Configs.Base
{
    public class UserConfig : IEntityConfig<User> {

        public override void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(256);


            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasAnnotation("Email", "true"); // You can customize this for more specific validation

            // Set Phone as required and limit its length (optional, based on your validation rules)
            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(15); // Assuming international phone number format

            // If necessary, index frequently used columns to improve performance
            builder.HasIndex(x => x.UserName).IsUnique();  // Ensure UserName is unique
            builder.HasIndex(x => x.Email).IsUnique();     // Ensure Email is unique

            builder.HasData(
               new User
               {
                   Id = 1,
                   UserName = "Duygu",
                   Password = "aMFoiWtsqnkCJ4PPAVQ0JaQ408BCoOWexsRJRwBez1mbp4CJrFfa3AHAYsTuLtV +", // Hash-lənmiş şifrə
                   Email = "dyg@course.com",
                   Phone = "+1234567890"
               }
           );

            base.Configure(builder);
        }
    }
}
