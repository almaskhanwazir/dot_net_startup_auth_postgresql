using FluentValidation;
using LearningPlatformBackend.Entities;

namespace LearningPlatformBackend.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required");
        }
    }
}
