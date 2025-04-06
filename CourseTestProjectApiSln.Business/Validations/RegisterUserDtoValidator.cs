using CourseTestProjectApiSln.Business.DTOs.User;
using FluentValidation;


namespace CourseTestProjectApiSln.Business.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(1, 50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");


            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password do not match.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Invalid phone number format.");
        }
    }
}
